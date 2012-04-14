using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SERIOUS_BUSINESS.res;

namespace SERIOUS_BUSINESS
{
    public partial class FormMain : Form
    {
        public res.Employee currentUser;
        public short currentAccess = 0;
        private SqlConnection dbConnection = null;
        public FormMain()
        {
            InitializeComponent();
            this.Hide();
            FormLogin formLogin = new FormLogin();
            DialogResult res = formLogin.ShowDialog();
            switch (res)
            {
                case DialogResult.OK:
                    this.Enabled = true;
                    this.currentUser = formLogin.employee;
                    this.Text = Settings.app_title +  " - " + currentUser.login;
                    this.currentUser = formLogin.usr;
                    break;
                default:
                    MessageBox.Show("Окно авторизации было закрыто");
                    break;
            }
            #region DB - get acc modifier
            try
            {
                dbConnection = new SqlConnection(SERIOUS_BUSINESS.res.Settings.dbConn_ConnStr);
                dbConnection.Open();
                SqlCommand sqlGetAccessMod = new SqlCommand("SELECT [accessModifier] FROM dbo.AppointmentSet WHERE id ="+currentUser.aptID.ToString(), dbConnection);
                currentAccess = (short)sqlGetAccessMod.ExecuteScalar();
                dbConnection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            #endregion

            #region example filling dgv
            string sql_str_getEmployees = @"
SELECT EmployeeSet.name AS [Сотрудник], AppointmentSet.name AS [Текущий доступ] FROM 
EmployeeSet
INNER JOIN
AppointmentSet
ON EmployeeSet.aptID = AppointmentSet.id";
            SqlDataAdapter tableContents = new SqlDataAdapter(sql_str_getEmployees, dbConnection);
            DataSet ds = new DataSet();
            tableContents.Fill(ds);

            DGV.DataSource = ds.Tables[0];
            DGV.Refresh();
            #endregion
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
//MENU
        #region Account settings
        private void редактированиеПароляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEditEmplOne emplDlg = new FormEditEmplOne(this.currentUser);
            emplDlg.ShowDialog();
        }
#endregion
        #region Orders
        private void оформитьНовыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentAccess == (int)accessModifiers.acc_mgr || currentAccess == (int)accessModifiers.acc_adm)
            {
                FormEditOrder formOrder = new FormEditOrder();
                formOrder.ShowDialog();
            }
            else
            {
                MessageBox.Show("Ваш уровень доступа не позволяет совершить это действие","Ошибка аутентификации", MessageBoxButtons.OK);
            }
        }
        #endregion
        #region Store
        private void оформитьПоступлениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentAccess == (int)accessModifiers.acc_store || currentAccess == (int)accessModifiers.acc_adm)
            {
                FormIntake formIntake = new FormIntake();
                formIntake.ShowDialog();
            }
            else
            {
                MessageBox.Show("Ваш уровень доступа не позволяет совершить это действие", "Ошибка аутентификации", MessageBoxButtons.OK);
            }
        }

        private void категорииИХарактеристикиТоваровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentAccess == (int)accessModifiers.acc_store || currentAccess == (int)accessModifiers.acc_adm)
            {
                FormEditCategories formCat = new FormEditCategories();
                formCat.ShowDialog();
            }
            else
            {
                MessageBox.Show("Ваш уровень доступа не позволяет совершить это действие", "Ошибка аутентификации", MessageBoxButtons.OK);
            }
        }
#endregion
        #region Employees
        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentAccess == (int)accessModifiers.acc_adm)
            {
                FormNewEmpl newEmpl = new FormNewEmpl();
                newEmpl.ShowDialog();
            }
            else
            {
                MessageBox.Show("Ваш уровень доступа не позволяет совершить это действие", "Ошибка аутентификации", MessageBoxButtons.OK);
            }
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEditEmplSet formEmplSet = new FormEditEmplSet();
            formEmplSet.ShowDialog();
        }
        #endregion
    }
}
