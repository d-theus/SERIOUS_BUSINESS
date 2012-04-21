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
        private string sqlcmd_commstr_ItemCategory_ListNames = "SELECT * FROM ItemCategorySet";
        private string sqlcmd_commstr_ItemAndItemParameter_ListWithDesignations_INCOMPLETE = "SELECT [ItemSet].[id], [ItemSet].[storeResidue], [ItemSet].[demand], [ItemSet].[catID], [ItemParameterSet].[valueTxt] FROM ItemSet INNER JOIN ItemParameterSet ON [ItemParameterSet].[itemID] = ItemSet.id WHERE [catID] = @catID AND [ItemParameterSet].[paramCatID] = (SELECT id FROM ParameterCategorySet WHERE name = 'Наименование')";
        private string sqlcmd_commstr_Item_Insert_INCOMPLETE;
        #endregion
        List<res.ItemCategory> CategoryList;
        List<res.ItemParameter> CatItemDesignationList;
        Dictionary<string, res.Item> ItemDictionary;


        public FormEditCategories(SqlConnection _dbConnection)
        {
            InitializeComponent();
            CategoryList = new List<res.ItemCategory>();
            CatItemDesignationList = new List<res.ItemParameter>();
            ItemDictionary = new Dictionary<string, res.Item>();

            dbConnection = _dbConnection;
            sqlCMD = dbConnection.CreateCommand();

            RefillCategoriesListAndCB();

        }

        private void RefillCategoriesListAndCB()
        {
        getCurrentCategories:
            sqlCMD.CommandText = sqlcmd_commstr_ItemCategory_ListNames;
            SqlDataReader drd = null;
            try
            {
                drd = sqlCMD.ExecuteReader();
                CategoryList.Clear();
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
                if (drd != null)
                    drd.Dispose();
                cb_cat.Items.Clear();
                cb_CatOfItem.Items.Clear();
                foreach (res.ItemCategory entry in CategoryList)
                {
                    cb_cat.Items.Add(entry.name);
                    cb_CatOfItem.Items.Add(entry.name);
                }
                RefillItemListAndCB();
            }
        }

        private void RefillItemListAndCB()
        {
            if (cb_CatOfItem.SelectedItem != null)
            {
            getCurrentItems:
                int catID = CategoryList.Find(x => x.name == cb_CatOfItem.SelectedItem.ToString()).id;
                sqlCMD.CommandText = sqlcmd_commstr_ItemAndItemParameter_ListWithDesignations_INCOMPLETE;
                sqlCMD.Parameters.Add(new SqlParameter("catID", catID));
                SqlDataReader drd = null;
                try
                {
                    drd = sqlCMD.ExecuteReader();
                    ItemDictionary.Clear();
                    while (drd.Read())
                    {
                        ItemDictionary.Add(drd["valueTxt"].ToString(), res.Item.CreateItem(int.Parse(drd["id"].ToString()), int.Parse(drd["storeResidue"].ToString()), int.Parse(drd["demand"].ToString()), int.Parse(drd["catID"].ToString())));
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
                            goto getCurrentItems;
                    }
                }
                finally
                {
                    sqlCMD.Parameters.Clear();
                    if (drd != null)
                        drd.Dispose();
                    Dictionary<string, res.Item>.Enumerator enr = ItemDictionary.GetEnumerator();
                    while (enr.MoveNext())
                    {
                        cb_designationOfItem.Items.Add(enr.Current.Key);
                    }
                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cb_CatOfItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefillItemListAndCB();
        }

    }
}
