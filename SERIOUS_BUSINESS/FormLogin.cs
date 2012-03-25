using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace SERIOUS_BUSINESS
{
    public partial class FormLogin : Form
    {
        public SERIOUS_BUSINESS.User resUser = new User("");
        public FormLogin()
        {
            InitializeComponent();
            try
            {
                StreamReader sr = new StreamReader("../res/history.txt");
                string saving;

                if (!sr.EndOfStream)
                {
                    saving = sr.ReadLine();
                    if (saving == "1")
                        cb_remember.Checked = true;
                    else
                        cb_remember.Checked = false;
                }

                if (!sr.EndOfStream) tb_login.Text = sr.ReadLine();
                sr.Close();
                tb_passwd.TabIndex = 1;
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
                resUser.name = tb_login.Text;
                resUser.passwd = tb_passwd.Text;
                try
                {
                    StreamWriter sw = new StreamWriter("../res/history.txt", false);
                    if (cb_remember.Checked)
                    {
                        sw.WriteLine("1");
                        sw.WriteLine(resUser.name);
                    }
                    else
                    {
                        sw.WriteLine("0");
                    }
                    sw.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
