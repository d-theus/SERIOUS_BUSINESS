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
    public partial class FormEditEmplSet : Form
    {
        res.Model1Container database;
        IQueryable<res.Employee> Employees;
        IQueryable<res.Appointment> Appointments;
        res.Employee selEmpl, curEmpl;
        public FormEditEmplSet(res.Employee _curEmpl)
        {
            InitializeComponent();

            database = new res.Model1Container(RegistryInteractor.GetFromReg("Connection String"));
            Employees = from emp in database.EmployeeSet select emp;
            Appointments = from app in database.AppointmentSet select app;

            this.curEmpl = _curEmpl;

            cb_app_fill();
        }

        public FormEditEmplSet(res.Employee _preselEmpl, res.Employee _curEmpl)
            : this(_curEmpl)
        {
            selEmpl = _preselEmpl;
            selEmpl_Changed();
        }

        private void tb_search_TextChanged(object sender, EventArgs e)
        {
            btn_search.Enabled = !tb_search.Text.Equals("");
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            if (Employees.Any(emp => emp.login == (string)tb_search.Text))
            {
                selEmpl = Employees.Single(emp => emp.login == (string)tb_search.Text);
                if (selEmpl == null)
                {
                    MessageBox.Show("Поиск не дал результатов");
                    return;
                }
                selEmpl_Changed();
            }
            else
            {
                selEmpl = null;
                lbl_id.Text = "";
                lbl_name.Text = "";
                MessageBox.Show("Поиск не дал результатов");
            }
        }

        private void selEmpl_Changed()
        {
            if (selEmpl != null)
            {
                lbl_name.Text = selEmpl.name;
                lbl_id.Text = selEmpl.id.ToString();
                cb_app.Text = "";
                cb_app.SelectedText = selEmpl.Appointment.name;
                if (selEmpl.login == "admin" || selEmpl.id == curEmpl.id)
                {
                    btc_accept.Enabled = false;
                    cb_app.Enabled = false;
                    MessageBox.Show("У администраторского или своего аккаунта нельзя менять доступ");
                }
                else
                {
                    btc_accept.Enabled = true;
                    cb_app.Enabled = true;
                }
            }

        }

        private void cb_app_fill()
        {
            cb_app.DataSource = Appointments.ToArray<res.Appointment>();
            cb_app.DisplayMember = "name";
            cb_app.ValueMember = "id";
        }

        private void btc_accept_Click(object sender, EventArgs e)
        {
            if (cb_app.Text != "" && selEmpl != null)
            {
                #region confirmation
                DialogResult cfn = MessageBox.Show(string.Format("Вы действительно хотите сменить доступ сотруднику {0} на '{1}'", selEmpl.name, cb_app.Text), "Внимание", MessageBoxButtons.YesNo);
                if (cfn == DialogResult.No) return;
                #endregion
                #region change_acc_mod
            lbl_try_change_accm:
                Employees.Single(emp => emp.id == this.selEmpl.id).aptID = (int)cb_app.SelectedValue;
                try
                {
                    database.SaveChanges();
                }
                catch (Exception exc)
                {
                    DialogResult dlgres = MessageBox.Show("Произошла ошибка при смене доступа:\n" + exc.Message, "Системная ошибка", MessageBoxButtons.RetryCancel);
                    switch (dlgres)
                    {
                        case DialogResult.Cancel:
                            return;
                        case DialogResult.Retry:
                            goto lbl_try_change_accm;
                    }
                }
                #endregion
            }
            else
            {
                MessageBox.Show("Выберите сотрудника и новый тип доступа");
            }
        }
    }
}
