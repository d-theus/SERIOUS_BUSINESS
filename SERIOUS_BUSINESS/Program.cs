using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Resources;

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

            string appRootPath = System.Text.RegularExpressions.Regex.Split(Application.StartupPath, "bin")[0];

            ResXResourceSet resRs = new ResXResourceSet(appRootPath + @"res\Dynamic.resx");
            bool firstLaunch = false;
            var enr = resRs.GetEnumerator();
            while (enr.MoveNext())
            {
                if (enr.Entry.Value.ToString().Length == 0)
                {
                    firstLaunch = true;
                }
            }
            if (firstLaunch)
            {
                ResXResourceWriter resWr = new ResXResourceWriter(appRootPath + @"res\Dynamic.resx");
                resWr.AddResource("path_app_root", appRootPath);
                resWr.AddResource("path_app_res", appRootPath + @"res\");
                resWr.Close();
            }
            FormMain formMain = new FormMain();
            formMain.Hide();
            Application.Run(formMain);
        }
    }
}
