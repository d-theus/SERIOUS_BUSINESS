using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace SERIOUS_BUSINESS
{
    class RegistryInteractor
    {
        public RegistryInteractor() {}
        public static void WriteToReg(string field, string value)
        {
            RegistryKey openKey = Registry.LocalMachine.OpenSubKey(res.Settings.reg_Subkey, true);
            openKey.SetValue(field, value);
            openKey.Close();

        }
        public static string GetFromReg(string field)
        {
            RegistryKey readKey = Registry.LocalMachine.OpenSubKey(res.Settings.reg_Subkey);
            string result = (string)readKey.GetValue(field);
            readKey.Close();
            return result;
        }
        public static void CreateSubkey()
        {
            RegistryKey regCreateKey = Registry.LocalMachine.CreateSubKey(res.Settings.reg_Subkey, RegistryKeyPermissionCheck.ReadWriteSubTree);
        }

        public static bool SubkeyExists()
        {
            RegistryKey readKey = Registry.LocalMachine.OpenSubKey(res.Settings.reg_Subkey);
            if (readKey == null)
            {
                return false;
            }
            else
            {
                readKey.Close();
                return true;
            } 
        }
    }
}
