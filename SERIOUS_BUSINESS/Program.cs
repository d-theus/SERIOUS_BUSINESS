using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Resources;
using Microsoft.Win32;

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

#region Writing current pwd to registry
            RegistryKey regRead = Registry.LocalMachine.OpenSubKey(res.Settings.reg_Subkey);
            if (regRead == null)
            {
                RegistryKey regCreateKey = Registry.LocalMachine.CreateSubKey(res.Settings.reg_Subkey);
                regCreateKey.SetValue("Last User", "");
                Regex sep = new Regex("bin");
                pwd = sep.Split(Application.ExecutablePath)[0];
                regCreateKey.SetValue("Root Directory", pwd); 
            }
#endregion

            FormMain formMain = new FormMain();
            formMain.Hide();
            Application.Run(formMain);
        }
    }
}
