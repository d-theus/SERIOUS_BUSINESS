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

#region resources interaction
            string appRootPath = System.Text.RegularExpressions.Regex.Split(Application.StartupPath, "bin")[0];
            ResXResourceSet resRs = new ResXResourceSet(appRootPath + @"res\Dynamic.resx");
            var enr = resRs.GetEnumerator();
            List<ResXDataNode> nodes = new List<ResXDataNode>();
            while (enr.MoveNext())
            {
                if (enr.Entry.Key.ToString().Equals("path_app_root") && (enr.Entry.Value.ToString().Length == 0))
                    nodes.Add(new ResXDataNode(enr.Entry.Key.ToString(), appRootPath));
                else if (enr.Entry.Key.ToString().Equals("path_app_res") && (enr.Entry.Value.ToString().Length == 0))
                    nodes.Add(new ResXDataNode(enr.Entry.Key.ToString(), appRootPath + @"res\"));
                else nodes.Add(new ResXDataNode(enr.Entry.Key.ToString(), enr.Entry.Value.ToString()));
            }

            ResXResourceWriter resWr = new ResXResourceWriter(appRootPath + @"res\Dynamic.resx");
            foreach (var entry in nodes)
                resWr.AddResource(entry);
            resWr.Close();
#endregion
            FormMain formMain = new FormMain();
            formMain.Hide();
            Application.Run(formMain);
        }
    }
}
