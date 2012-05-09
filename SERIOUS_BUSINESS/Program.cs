using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Resources;
using Microsoft.Win32;
using System.Security;

namespace SERIOUS_BUSINESS
{
    static class Program
    {
        static private string pwd = "";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#region Writing current pwd & usr to registry
            Regex sep = new Regex("bin");
            pwd = sep.Split(Application.ExecutablePath)[0];
            if (!RegistryInteractor.SubkeyExists())
            {
                RegistryInteractor.CreateSubkey();

                RegistryInteractor.WriteToReg("Root Directory", pwd);
                RegistryInteractor.WriteToReg("Last User", "");
            }
            else
            {
                RegistryInteractor.WriteToReg("Root Directory", pwd);
            }
#endregion


            FormMain formMain = new FormMain();
            formMain.Hide();
            Application.Run(formMain);
        }
    }
}
