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

        private const string sqlcmd_commstr_ItemCategory_IsExist_INCOMPLETE = "SELECT COUNT(*) FROM ItemCategorySet WHERE name = @name";
        private const string sqlcmd_commstr_ItemCategory_Insert_INCOMPLETE = "INSERT INTO ItemCategorySet (name) VALUES (@name)";
        private const string sqlcmd_commstr_ItemCategory_ListNames = "SELECT * FROM ItemCategorySet";
        private const string sqlcmd_commstr_ItemAndItemParameter_ListWithDesignations_INCOMPLETE = "SELECT [ItemSet].[id], [ItemSet].[storeResidue], [ItemSet].[demand], [ItemSet].[catID], [ItemParameterSet].[valueTxt] FROM ItemSet INNER JOIN ItemParameterSet ON [ItemParameterSet].[itemID] = ItemSet.id WHERE [catID] = @catID AND [ItemParameterSet].[paramCatID] = (SELECT id FROM ParameterCategorySet WHERE name = 'Наименование')";
        private const string sqlcmd_commstr_ItemParameter_IsExist_INCOMPLETE = "SELECT COUNT(*) FROM ";
        private const string sqlcmd_commstr_ItemParameter_Insert_INCOMPLETE = "";
        private const string sqlcmd_commstr_Item_Insert_INCOMPLETE = "";
        private const string sqlcmd_commstr_ItemParameter_List = "SELECT id, name FROM ParameterCategorySet INNER JOIN pureJoin_IPcatsSet ON  ParameterCategorySet.id = pureJoin_IPcatsSet.PCID WHERE pureJoin_IPcatsSet.ICID = @categoryID";
        #endregion
        List<res.ItemCategory> CategoryList;
        List<res.ParameterCategory> ParamCatList;
        List<res.ItemParameter> CatItemDesignationList;
        Dictionary<string, res.Item> ItemDictionary;

        public FormEditCategories(SqlConnection _dbConnection)
        {
            InitializeComponent();
            CategoryList = new List<res.ItemCategory>();
            CatItemDesignationList = new List<res.ItemParameter>();
            ItemDictionary = new Dictionary<string, res.Item>();
            ParamCatList = new List<res.ParameterCategory>();

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
                        return;
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
                            return;
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

        private void RefillParameterCategoryList()
        {
        get_param_list:
            sqlCMD.CommandText = sqlcmd_commstr_ItemParameter_List;
        sqlCMD.Parameters.Add(new SqlParameter("categoryID", CategoryList.Any<res.ParameterCategory>(x => x.name == )));
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

        private void btn_addCat_Click(object sender, EventArgs e)
        {
            DialogResult reslt =  MessageBox.Show("Добавить категорию товаров " + tb_catName.Text.ToString() + " ?", "Добавление категории товаров", MessageBoxButtons.YesNo);
            if (reslt == DialogResult.No) return;
            try_add_cat:
            try
            {
                sqlTRS = dbConnection.BeginTransaction("Adding new item category");
                sqlCMD.CommandText = sqlcmd_commstr_ItemCategory_IsExist_INCOMPLETE;
                sqlCMD.Parameters.Add(new SqlParameter("name", tb_catName.Text.ToString()));
                sqlCMD.Transaction = sqlTRS;
                if (int.Parse(sqlCMD.ExecuteScalar().ToString()) > 0)
                {
                    throw new Exception("Такая категория уже есть");
                }
                sqlCMD.CommandText = sqlcmd_commstr_ItemCategory_Insert_INCOMPLETE; //parameter named @name is same as last used
                sqlCMD.ExecuteNonQuery();
                sqlTRS.Commit();
                RefillCategoriesListAndCB();
            }
            catch (SqlException exc)
            {
                sqlTRS.Rollback();
                DialogResult dlgres = MessageBox.Show(exc.Message, "Ошибка", MessageBoxButtons.RetryCancel);
                switch (dlgres)
                {
                    case DialogResult.Cancel:
                        return;
                    case DialogResult.Retry:
                        goto try_add_cat;
                }
            }
            catch (Exception exc)
            {
                sqlTRS.Rollback();
                DialogResult dlgres = MessageBox.Show(exc.Message, "Ошибка");
                return;
            }
            finally
            {
                sqlCMD.Parameters.Clear();
            }
        }

        private void tb_catName_TextChanged(object sender, EventArgs e)
        {
            if (tb_catName.Text.ToString().Length == 0)
            {
                btn_addCat.Enabled = false;
            }
            else
            {
                btn_addCat.Enabled = true;
            }
        }

        private void tb_name_TextChanged(object sender, EventArgs e)
        {
            if (tb_name.Text.ToString().Length == 0)
            {
                btn_addPar.Enabled = false;
            }
            else
            {
                btn_addPar.Enabled = true;
            }
        }


    }
}
