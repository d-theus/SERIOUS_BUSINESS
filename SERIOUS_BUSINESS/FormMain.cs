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
    public partial class FormMain : Form
    {
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
                    break;
                default:
                    MessageBox.Show("Окно авторизации было закрыто");
                    break;
            }
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void редактированиеПароляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEditEmpl emplDlg = new FormEditEmpl();
            emplDlg.ShowDialog();
        }

    }
}
