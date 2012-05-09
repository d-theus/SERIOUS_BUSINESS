using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
namespace SERIOUS_BUSINESS
{
    public partial class FormLogin : Form
    {
        private res.Model1Container database;
        public res.Employee usr;
        public FormLogin()
        {
            InitializeComponent();
            database = new res.Model1Container();
            try
            {
                #region Retrieving last user from registry
                tb_login.Text = RegistryInteractor.GetFromReg("Last User");
                if (tb_login.Text.ToString().Length != 0)
                {
                    cb_remember.Checked = true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка взаимодействия с реестром");
            }
        }

        private void btn_enter_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tb_login.Text) && !String.IsNullOrEmpty(tb_passwd.Text))
            {
                try
                {
                    #region compare entered login data with data in DB

                    if ((from emp in database.EmployeeSet where emp.login == tb_login.Text select emp).Count() == 0)
                    {
                        MessageBox.Show("Нет такого пользователя в базе данных, проверьте правильность ввода логина");
                        return;
                    }
                    else
                    {
                        if ((from emp in database.EmployeeSet where emp.login == tb_login.Text select emp).Single().password != tb_passwd.Text)
                        {
                            MessageBox.Show("Неверный пароль");
                            return;
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n Будет осуществлен выход, обратитесь к системному администратору", "Ошибка базы данных", MessageBoxButtons.OK);
                    Application.Exit();
                }

                #region get empl from DB
                usr = (from emp in database.EmployeeSet where emp.login == tb_login.Text select emp).Single();

                #endregion
                if (cb_remember.Checked)
                {
                    #region Saving current user to registry
                    RegistryInteractor.WriteToReg("Last User", tb_login.Text);
                    #endregion
                }
                DialogResult = DialogResult.OK;
                return;
            }
            else
            {
                MessageBox.Show("Логин и пароль не должны быть пустыми");
                return;
            }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
            {
                DialogResult = DialogResult.Abort;
            }
        }
    }
}
