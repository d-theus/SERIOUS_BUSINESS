using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Resources;
using Microsoft.Win32;
using System.Security;
using System.Data.EntityClient;

namespace SERIOUS_BUSINESS
{
    static class Program
    {
        static private string pwd = "";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#region Writing current pwd & usr to registry, ask for DB creation
            Regex sep = new Regex("bin");
            pwd = sep.Split(Application.ExecutablePath)[0];
            if (!RegistryInteractor.SubkeyExists())
            {
                RegistryInteractor.CreateSubkey();
                RegistryInteractor.WriteToReg("Last User", "");

                #region Deployment
                if (MessageBox.Show("Похоже, это первый запуск приложения. Создать базу данных? В неё будет занесен пока единственный пользователь admin (пароль 'admin') и категории параметров товаров по умлочанию: Наименование, Цена закупки и Цена продажи.", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        res.Model1Container database = new res.Model1Container();
                        if (database.DatabaseExists())
                            database.DeleteDatabase();

                        database.CreateDatabase();
                        database.SaveChanges();

                        res.Appointment adm = res.Appointment.CreateAppointment(0, (short)accessModifiers.acc_adm, "Полный");
                        database.AddToAppointmentSet(res.Appointment.CreateAppointment(0, (short)accessModifiers.acc_none, "Нет"));
                        database.AddToAppointmentSet(res.Appointment.CreateAppointment(0, (short)accessModifiers.acc_stock, "Склад"));
                        database.AddToAppointmentSet(res.Appointment.CreateAppointment(0, (short)accessModifiers.acc_ord, "Заказы"));
                        database.AddToAppointmentSet(adm);

                        res.Employee admin = res.Employee.CreateEmployee(0, "admin", "admin", "admin", 0);
                        admin.Appointment = adm;
                        database.AddToEmployeeSet(admin);

                        database.AddToParameterCategorySet(res.ParameterCategory.CreateParameterCategory(0, "Наименование", 1));
                        database.AddToParameterCategorySet(res.ParameterCategory.CreateParameterCategory(0, "Цена закупки", 2));
                        database.AddToParameterCategorySet(res.ParameterCategory.CreateParameterCategory(0, "Цена продажи", 2));

                        database.SaveChanges();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Произошла ошибка при создании базы данных: \n"+exc.Message);
                    }
                }
                #endregion
            }
            RegistryInteractor.WriteToReg("Root Directory", pwd);
#endregion


            FormMain formMain = new FormMain();
            formMain.Hide();
            Application.Run(formMain);
        }
    }
}
