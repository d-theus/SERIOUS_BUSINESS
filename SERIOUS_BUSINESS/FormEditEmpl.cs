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
    public partial class FormEditEmpl : Form
    {
        public FormEditEmpl()
        {
            InitializeComponent();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
