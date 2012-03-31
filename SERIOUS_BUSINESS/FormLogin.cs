using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows.Forms;
namespace SERIOUS_BUSINESS
{
    public partial class FormLogin : Form
    {
        bool rememberChanged = false;
        public User usr;
        public FormLogin()
        {
            InitializeComponent();
            System.Data.SqlClient.SqlConnection dbConnection = new System.Data.SqlClient.SqlConnection(res.Settings.dbConn_DataSource +
                                                                                                       "AttachDbFilename = " + res.Dynamic.path_app_res + "Database1.mdf;" +
                                                                                                       res.Settings.dbConn_IntSecurity);
            try
            {
                dbConnection.Open();
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
                usr = new User(tb_login.Text, (int)Access_Modifiers.acc_director);
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

        private void cb_remember_CheckedChanged(object sender, EventArgs e)
        {
            rememberChanged = true;
        }

    }
}
