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
namespace SERIOUS_BUSINESS
{
    public partial class FormLogin : Form
    {
        bool rememberChanged = false;
        public res.Employee employee;
        private SqlConnection dbConnection = null;
        public FormLogin()
        {
            InitializeComponent();
            dbConnection = new SqlConnection(res.Settings.dbConn_ConnStr);
            try
            {
                dbConnection.Open();
                #region resources

                ResXResourceReader rr = new ResXResourceReader(res.Dynamic.path_app_res + "Dynamic.resx");
                var enr = rr.GetEnumerator();
                while (enr.MoveNext()) 
                {
                    string key = enr.Entry.Key.ToString();
                    string val = enr.Entry.Value.ToString();
                    if (key.Equals("last_user"))
                    {
                        tb_login.Text = val;
                        if (val.Length != 0)
                            cb_remember.Checked = true;
                    }
                }
            #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

                        #region History file interacting
                        ResXResourceSet rs = new ResXResourceSet(res.Dynamic.path_app_res + "Dynamic.resx");
                        ResXResourceWriter rw = new ResXResourceWriter(res.Dynamic.path_app_res + "Dynamic.resx");
                        var enr = rs.GetEnumerator();
                        List<ResXDataNode> nodes = new List<ResXDataNode>();
                        while (enr.MoveNext())
                        {
                            if (!(enr.Entry.Key.ToString().Equals("last_user")))
                                nodes.Add(new ResXDataNode(enr.Entry.Key.ToString(), enr.Entry.Value.ToString()));
                            else
                                if (cb_remember.Checked)
                                    nodes.Add(new ResXDataNode(enr.Entry.Key.ToString(), tb_login.Text));
                                else
                                    nodes.Add(new ResXDataNode(enr.Entry.Key.ToString(), enr.Entry.Value.ToString()));
                        }
                        foreach (var entry in nodes)
                        {
                            rw.AddResource(entry);
                        }
                        rw.Close();
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
