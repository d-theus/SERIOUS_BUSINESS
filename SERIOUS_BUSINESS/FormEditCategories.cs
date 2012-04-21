using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SERIOUS_BUSINESS
{
    public partial class FormEditCategories : Form
    {
#region sql connection, commands, transaction and strings
        private SqlConnection dbConnection;
        private SqlCommand sqlCMD;
        private SqlTransaction sqlTRS;

        private string sqlcmd_commstr_ItemCategory_IsExist_INCOMPLETE;
        private string sqlcmd_commstr_ItemCategory_Insert_INCOMPLETE;
        private string sqlcmd_commstr_ItemCategory_ListNames = "SELECT [name] FROM ItemCategorySet";
        private string sqlcmd_commstr_Item_IsExistInCat_INCOMPLETE;
        private string sqlcmd_commstr_Item_Insert_INCOMPLETE;
#endregion
        List<res.ItemCategory> CategoryList;
        List<res.ItemParameter> CatItemDesignationList;


        public FormEditCategories(SqlConnection _dbConnection)
        {
            InitializeComponent();
            CategoryList = new List<res.ItemCategory>();
            CatItemDesignationList = new List<res.ItemParameter>();

            dbConnection = _dbConnection;
            sqlCMD = dbConnection.CreateCommand();

            RefillCategoriesListAndCB();

        }

        private void RefillCategoriesListAndCB()
        {
        getCurrentCategories:
            try
            {
                sqlCMD.CommandText = sqlcmd_commstr_ItemCategory_ListNames;
                SqlDataReader drd = sqlCMD.ExecuteReader();
                while (drd.Read())
                {
                    CategoryList.Add(res.ItemCategory.CreateItemCategory(drd["name"].ToString(), int.Parse(drd["id"].ToString())));
                }
            }
            catch (Exception exc)
            {
                DialogResult dlgres = MessageBox.Show(exc.Message, "Ошибка", MessageBoxButtons.RetryCancel);
                switch (dlgres)
                {
                    case DialogResult.Cancel:
                        break;
                    case DialogResult.Retry:
                        goto getCurrentCategories;
                }
            }
            finally
            {
                cb_cat.Items.AddRange(CategoryList.ToArray());
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
