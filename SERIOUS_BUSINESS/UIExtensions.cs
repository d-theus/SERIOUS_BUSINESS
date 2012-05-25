using System;
using System.Windows.Forms;

namespace SERIOUS_BUSINESS
{
    class UIE //xtensions
    {

        public static void CmB_SetToFirst(ref ComboBox cb)
        {
            if (cb.Items.Count > 0)
            {
                cb.SelectedIndex = 0;
            }
        }
    }
}