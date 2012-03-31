﻿using System;
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
        public User currentUser;
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
                    this.Text = currentUser.login + " - PHOTOLOLO";
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