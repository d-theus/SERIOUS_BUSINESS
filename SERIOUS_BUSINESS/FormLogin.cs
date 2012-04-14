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
        bool rememberChanged = false;
        public res.Employee usr;
        public FormLogin()
        {
            InitializeComponent();
            System.Data.SqlClient.SqlConnection dbConnection = new System.Data.SqlClient.SqlConnection(res.Settings.dbConn_DataSource +
                                                                                                      "AttachDbFilename = " + res.Dynamic.path_app_res + "Database1.mdf;" +
                                                                                                       res.Settings.dbConn_IntSecurity);
            try
            {
                dbConnection.Open();
#region Retrieving last user from registry
                RegistryKey readKey = Registry.LocalMachine.OpenSubKey(@"software\\"+res.Settings.app_title+@"\");
                string loadString = (string)readKey.GetValue("Last User");
                readKey.Close();
#endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        private void btn_enter_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tb_login.Text) && !String.IsNullOrEmpty(tb_passwd.Text))
            {
                if(rememberChanged)
                {
                    try
                    {
                        #region sql query : get password from db and compare, create emp instance if success

                        SqlCommand sql_countByLogin = new SqlCommand("SELECT COUNT(*) FROM [DATABASE].[dbo].[EmployeeSet] WHERE [login] = '" + tb_login.Text + "'", dbConnection);
                        if(sql_countByLogin.ExecuteScalar().ToString().Equals("0"))
                        {
                            MessageBox.Show("Нет такого пользователя в базе данных, проверьте правильность ввода логина");
                            return;
                        }
                        else
                        {
                            SqlCommand sql_GetEmployee = new SqlCommand("SELECT [id],[login],[name],[password], [aptID] FROM [DATABASE].[dbo].[EmployeeSet] WHERE [login] = '" + tb_login.Text + "'", dbConnection);
                            SqlDataReader sql_drd = sql_GetEmployee.ExecuteReader();
                            sql_drd.Read();
                            if (tb_passwd.Text.ToString().Equals(sql_drd["password"]))
                            {
                                employee = res.Employee.CreateEmployee((int)sql_drd["id"], sql_drd["name"].ToString(), sql_drd["login"].ToString(), sql_drd["password"].ToString(), (int)sql_drd["aptID"]);
                            }
                            else
                            {
                                MessageBox.Show("Неверный пароль");
                                sql_drd.Close();
                                return;
                            }
                            sql_drd.Close();
                        }
                        #endregion
                try
                {

#region Saving current user to registry
                    RegistryKey openKey = Registry.LocalMachine.OpenSubKey(@"software\\"+res.Settings.app_title+@"\");
                    openKey.SetValue("Last User", tb_login.Text.ToString());
                    openKey.Close();

#endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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
            dbConnection.Close();
            if (DialogResult != DialogResult.OK)
            {
                DialogResult = DialogResult.Abort;
            }
        }

        private void cb_remember_CheckedChanged(object sender, EventArgs e)
        {
            rememberChanged = true;
        }

    }
}
