using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SERIOUS_BUSINESS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormMain formMain = new FormMain();
            formMain.Hide();
            Application.Run(formMain);
        }
    }
}
