using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SERIOUS_BUSINESS
{
    public partial class FormNewEmpl : Form
    {
        res.Model1Container database;
        res.Employee resEmpl;
        IQueryable<res.Employee> Employees;
        public FormNewEmpl()
        {
            InitializeComponent();
            database = new res.Model1Container(RegistryInteractor.GetFromReg("Connection String"));
            Employees = from emp in database.EmployeeSet select emp;

            #region event_bindings
            tb_login.TextChanged += new EventHandler(Idata_Changed);
            tb_name.TextChanged += new EventHandler(Idata_Changed);
            rb_ord.CheckedChanged += new EventHandler(Idata_Changed);
            rb_store.CheckedChanged += new EventHandler(Idata_Changed);
            #endregion
        }

        private void Idata_Changed(object sender, EventArgs e)
        {
            btn_ok.Enabled = tb_name.Text != "" && tb_login.Text != "" && (rb_ord.Checked || rb_store.Checked);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            int sel_apt_id = -1;
            string str_acc = "";
            if (rb_ord.Checked)
            {
                sel_apt_id = (from app in database.AppointmentSet where app.name == "Заказы" select app).Single().id;
                str_acc = "к заказам";
            }
            else
            {
                sel_apt_id = (from app in database.AppointmentSet where app.name == "Склад" select app).Single().id;
                str_acc = "к складу";
            }
            resEmpl = res.Employee.CreateEmployee(-1, tb_name.Text, tb_login.Text, tb_login.Text, sel_apt_id);
#region confirmation
            DialogResult cnd = MessageBox.Show(string.Format("Добавить пользователя с никнеймом {0} \n ФИО {1} \n и доступом {2} \n в базу данных?", resEmpl.login, resEmpl.name, str_acc), "Внимание", MessageBoxButtons.YesNo);
            if (cnd == DialogResult.No) return; 
	#endregion
#region check_if_exist
            if (Employees.Any(emp => emp.login == resEmpl.login))
            {
                MessageBox.Show("Такой логин уже занят");
                return;
            }
            #endregion
#region addition
        lbl_try_add_user:
            try
            {
                database.AddObject("EmployeeSet", resEmpl);
                database.SaveChanges();
            }
            catch (Exception exc)
            {
                DialogResult dlgres = MessageBox.Show("Не удалось добавить пользователя в базу данных:" + exc.Message, "Системная ошибка", MessageBoxButtons.RetryCancel);
                switch (dlgres)
                {
                    case DialogResult.Cancel:
                        return;
                    case DialogResult.Retry:
                        goto lbl_try_add_user;
                }
            } 
            #endregion
        }
    }
}
