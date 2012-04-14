using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
                    this.currentUser = formLogin.usr;
                    //this.Text = Settings.app_title +  " - " + currentUser.login;
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
            FormEditEmplOne emplDlg = new FormEditEmplOne(this.currentUser);
            emplDlg.ShowDialog();
        }

        private void оформитьНовыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEditOrder formOrder = new FormEditOrder();
            formOrder.ShowDialog();
        }

        private void оформитьПоступлениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormIntake formIntake = new FormIntake();
            formIntake.ShowDialog();
        }

        private void категорииИХарактеристикиТоваровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEditCategories formCat = new FormEditCategories();
            formCat.ShowDialog();
        }

        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNewEmpl newEmpl = new FormNewEmpl();
            newEmpl.ShowDialog();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEditEmplSet formEmplSet = new FormEditEmplSet();
            formEmplSet.ShowDialog();
        }

    }
}
