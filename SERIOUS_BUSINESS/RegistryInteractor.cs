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
            try
            {
                RegistryKey openKey = Registry.LocalMachine.OpenSubKey(res.Settings.reg_Subkey, true);
                openKey.SetValue(field, value);
                openKey.Close();
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка записи в реестр" + exc.Message, "Внимание");
            }

        }
        public static string GetFromReg(string field)
        {
            try
            {
                RegistryKey readKey = Registry.LocalMachine.OpenSubKey(res.Settings.reg_Subkey);
                string result = (string)readKey.GetValue(field);
                readKey.Close();
                return result;
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка чтения в реестр" + exc.Message, "Внимание");
                return "";
            }
        }
        public static void CreateSubkey()
        {
            try
            {
                RegistryKey regCreateKey = Registry.LocalMachine.CreateSubKey(res.Settings.reg_Subkey);
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка создания ключа в реестре" + exc.Message, "Внимание");
            }
        }

        public static bool SubkeyExists()
        {
            RegistryKey readKey = null;
            try
            {
                readKey = Registry.LocalMachine.OpenSubKey(res.Settings.reg_Subkey);
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка выполнения вспомогательной операции в реестре " + exc.Message, "Внимание");
            }
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
