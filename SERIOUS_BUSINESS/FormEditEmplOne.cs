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
    public partial class FormEditEmplOne : Form
    {
        private res.Model1Container database;
        private res.Employee Empl;
        private IQueryable<res.Employee> Employees;
        public FormEditEmplOne(ref res.Employee empl)
        {
            InitializeComponent();
            database = new res.Model1Container(RegistryInteractor.GetFromReg("Connection String"));
            Employees = from emp in database.EmployeeSet select emp;
            this.Empl = empl;
            lbl_username.Text = Empl.login;
            btn_loginChange.Enabled = false;

            #region event_bindings
            Empl.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.user_prop_changed);
            database.SavingChanges += new System.EventHandler(this.database_changing);
            #endregion
        }

        private void tb_login_TextChanged(object sender, EventArgs e)
        {
            btn_loginChange.Enabled = !((string)tb_login.Text).Equals("") && tb_login.Text != "admin";
        }

        private void btn_loginChange_Click(object sender, EventArgs e)
        {
            string login_change_to = (string)tb_login.Text;
#region confirmation
            DialogResult cfn = MessageBox.Show(string.Format("Вы действительно хотите сменить логин с '{0}' на '{1}'", this.Empl.login, login_change_to), "Внимание", MessageBoxButtons.YesNo);
            if (cfn == DialogResult.No) return;
#endregion
#region check_for_same_login
            if (Employees.Any(emp => emp.login.Equals(login_change_to)))
            {
                MessageBox.Show("Такой логин уже использован", "Ошибка");
                return;
            }
#endregion
#region change_login
    lbl_try_change_login:
            Employees.Single(emp => emp.id == this.Empl.id).login = (string)tb_login.Text;
            try
            {
                database.SaveChanges();
            }
            catch (Exception exc)
            {
                DialogResult dlgres = MessageBox.Show("Произошла ошибка при смене логина:\n"+exc.Message, "Системная ошибка", MessageBoxButtons.RetryCancel);
                switch (dlgres)
                {
                    case DialogResult.Cancel:
                        return;
                    case DialogResult.Retry:
                        goto lbl_try_change_login;
                }
            }

#endregion
        }

        private void btn_passwdChange_Click(object sender, EventArgs e)
        {
            #region confirmation
            DialogResult cfn = MessageBox.Show("Вы действительно хотите сменить пароль?", "Внимание", MessageBoxButtons.YesNo);
            if (cfn == DialogResult.No) return;
            #endregion

            string old_pass = (string)tb_oldPass.Text;
            string n1_pass = (string)tb_newPass1.Text;
            string n2_pass = (string)tb_newPass2.Text;

            #region check_if_properly_filled
            if (old_pass != Empl.password)
            {
                MessageBox.Show("Неправильно введен старый пароль", "Ошибка");
                return;
            }
            if (n1_pass != n2_pass)
            {
                MessageBox.Show("Введенные новые пароли не совпадают", "Ошибка");
                return;
            }
            else if (n1_pass == "")
            {
                MessageBox.Show("Введенные новые пароли пусты", "Ошибка");
                return;
            }
            #endregion
            #region changing_password
        lbl_try_change_passwd:
            Employees.Single(emp => emp.id == this.Empl.id).password = (string)tb_newPass2.Text;
            try
            {
                database.SaveChanges();
            }
            catch (Exception exc)
            {
                DialogResult dlgres = MessageBox.Show("Произошла ошибка при смене пароля:\n" + exc.Message, "Системная ошибка", MessageBoxButtons.RetryCancel);
                switch (dlgres)
                {
                    case DialogResult.Cancel:
                        return;
                    case DialogResult.Retry:
                        goto lbl_try_change_passwd;
                }
            }

            #endregion
        }

        private void user_prop_changed(Object sender, EventArgs e)
        {
            lbl_username.Text = Empl.login;
        }

        private void database_changing(Object sender, EventArgs e)
        {
            Empl.login = Employees.Single(emp => emp.id == this.Empl.id).login;
            Empl.password = Employees.Single(emp => emp.id == this.Empl.id).password;
        }
    }
}
