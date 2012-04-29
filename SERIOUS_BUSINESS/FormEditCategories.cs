using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Data.EntityModel;
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
        private const string sqlcmd_commstr_ItemCategory_List = "SELECT * FROM ItemCategorySet";
        private const string sqlcmd_commstr_Item_List_INCOMPLETE = "SELECT * FROM ItemSet INNER JOIN ItemParameterSet ON [ItemParameterSet].[itemID] = ItemSet.id WHERE [catID] = @catID";
        private const string sqlcmd_commstr_Item_DesignationList_INCOMPLETE = "SELECT ItemSet.id, valueTxt FROM ItemSet INNER JOIN ItemParameterSet ON [ItemParameterSet].[itemID] = ItemSet.id WHERE [catID] = @catID AND paramCatID = (SELECT id FROM ParameterCategorySet WHERE name = 'Наименование')";
        private const string sqlcmd_commstr_ItemParameter_Insert_INCOMPLETE = @"IF (SELECT COUNT(*) FROM ParameterCategorySet WHERE name = @name) = 0 
BEGIN
	INSERT  INTO ParameterCategorySet	
	VALUES (@name)
END";
        private const string sqlcmd_commstr_ItemParameter_ListForCurItem_INCOMPLETE = "SELECT name, valueTxt, valueDbl, valueBool FROM ItemParameterSetINNER JOIN ParameterCategorySet ON ParameterCategorySet.id = ItemParameterSet.paramCatID";
        private const string sqlcmd_commstr_ItemParameter_List = "SELECT * FROM ParameterCategorySet";
        private const string sqlcmd_commstr_CatHasParam_INCOMPLETE = @"SELECT COUNT(*) FROM 
pureJoin_IPcatsSet
WHERE ICID = @icid AND PCID = @pcid";
        #endregion
        DataSet MainDataSet;

        public FormEditCategories(SqlConnection _dbConnection)
        {
            InitializeComponent();
            #region main data set
            MainDataSet = new DataSet();
            MainDataSet.Tables.Add(new DataTable("Categories"));

            MainDataSet.Tables.Add(new DataTable("Items"));
            MainDataSet.Tables.Add(new DataTable("Designations"));
            MainDataSet.Tables.Add(new DataTable("Parameters"));
            MainDataSet.Tables["Parameters"].Columns.Add("AssocWithCat", Type.GetType("System.Boolean"));
            #endregion
            dbConnection = _dbConnection;
            sqlCMD = dbConnection.CreateCommand();

            DGV_itemParameters.DataSource = MainDataSet.Tables["Parameters"];
            cb_cat.DataSource = MainDataSet.Tables["Categories"];
            cb_existingItem.DataSource = MainDataSet.Tables["Designations"];
            clb_params.DataSource = MainDataSet.Tables["Parameters"];

            RefillCategoriesTable();
            cb_cat.DisplayMember = "name";
            clb_params.DisplayMember = "name";
        }

        private void RefillCategoriesTable()
        {

            sqlCMD.CommandText = sqlcmd_commstr_ItemCategory_List;
            using (SqlDataAdapter sda = new SqlDataAdapter(sqlCMD))
            {
            getCurrentCategories:
                try
                {
                    MainDataSet.Tables["Categories"].Clear();
                    sda.Fill(MainDataSet.Tables["Categories"]);
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
                    MainDataSet.Tables["Categories"].PrimaryKey = new DataColumn[] {MainDataSet.Tables["Categories"].Columns["name"]};
                    cb_cat.Update();
                }
            }
        }

        private void RefillItemTable()
        {
            if (cb_cat.Text.Length > 0)
            {
                int icid = -1;
            getcurrentitems:
                DataRow[] selectRes = MainDataSet.Tables["Categories"].Select(string.Format("name = '{0}'", cb_cat.Text));
                icid = int.Parse(selectRes[0]["id"].ToString());

                sqlCMD.CommandText = sqlcmd_commstr_Item_List_INCOMPLETE;
                sqlCMD.Parameters.Add(new SqlParameter("catid", icid));
                try
                {
                    MainDataSet.Tables["Items"].Clear();
                    MainDataSet.Tables["Designations"].Clear();

                    using (SqlDataAdapter sda = new SqlDataAdapter(sqlCMD))
                        sda.Fill(MainDataSet.Tables["Items"]);

                    sqlCMD.CommandText = sqlcmd_commstr_Item_DesignationList_INCOMPLETE;
                    using (SqlDataAdapter sda = new SqlDataAdapter(sqlCMD))
                        sda.Fill(MainDataSet.Tables["Designations"]);
                }
                catch (Exception exc)
                {
                    DialogResult dlgres = MessageBox.Show(exc.Message, "ошибка", MessageBoxButtons.RetryCancel);
                    switch (dlgres)
                    {
                        case DialogResult.Cancel:
                            return;
                        case DialogResult.Retry:
                            goto getcurrentitems;
                    }
                }
                finally
                {
                    sqlCMD.Parameters.Clear();
                    cb_existingItem.DisplayMember = "valueTxt";
                    cb_existingItem.ValueMember = "id";
                    cb_existingItem.Update();
                }
            }
            else
            {
                MessageBox.Show("Выберите категорию");
            }
        }

        private void RefillParameterTable()
        {
            if (cb_cat.SelectedIndex >= 0)
            {
                int icid = -1;
            lbl_try_fill_params:
                DataRow[] selectRes = MainDataSet.Tables["Categories"].Select(string.Format("name = '{0}'", cb_cat.Text));
                icid = int.Parse(selectRes[0]["id"].ToString());
                sqlCMD.CommandText = sqlcmd_commstr_ItemParameter_List;
                try
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(sqlCMD))
                    {
                        MainDataSet.Tables["Parameters"].Clear();
                        sda.Fill(MainDataSet.Tables["Parameters"]);
                    }
                    int row_count = MainDataSet.Tables["Parameters"].Rows.Count;
                    for (int row = 0; row < row_count; row++)
                    {
                        MainDataSet.Tables["Parameters"].Rows[row]["AssocWithCat"] = isCatHasParam(int.Parse(MainDataSet.Tables["Parameters"].Rows[row]["id"].ToString()) ,icid);
                    }
                }
                catch (Exception exc)
                {
                    DialogResult dlgres = MessageBox.Show(exc.Message, "ошибка", MessageBoxButtons.RetryCancel);
                    switch (dlgres)
                    {
                        case DialogResult.Cancel:
                            return;
                        case DialogResult.Retry:
                            goto lbl_try_fill_params;
                    }
                }
                finally
                {
                    clb_params.Update();
                }
            }
            else
            {
                MessageBox.Show("Выберите категорию");
            }
        }

        private void associateParameterToCategory()
        {
        }
        private bool isCatHasParam(int pcid, int icid)
        {
            int count = 0;
            sqlCMD.CommandText = sqlcmd_commstr_CatHasParam_INCOMPLETE;
            sqlCMD.Parameters.Add(new SqlParameter("pcid", pcid));
            sqlCMD.Parameters.Add(new SqlParameter("icid", icid));
            try
            {

                count = int.Parse(sqlCMD.ExecuteScalar().ToString());
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return false;
            }
            finally
            {
                sqlCMD.Parameters.Clear();
            }
            return count == 1;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_addCat_Click(object sender, EventArgs e)
        {
            DialogResult reslt = MessageBox.Show("Добавить категорию товаров " + tb_catName.Text.ToString() + " ?", "Добавление категории товаров", MessageBoxButtons.YesNo);
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
                RefillCategoriesTable();
                tb_catName.Clear();
            }
            catch (SqlException exc)
            {
                sqlTRS.Rollback();
                DialogResult dlgres = MessageBox.Show(exc.Message, "Ошибка базы данных", MessageBoxButtons.RetryCancel);
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

        private void cb_cat_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefillItemTable();
            RefillParameterTable();
        }

        private void tb_newItemDesignation_TextChanged(object sender, EventArgs e)
        {
            if (tb_newItemDesignation.Text.ToString().Length == 0)
            {
                btn_addItem.Enabled = false;
            }
            else
            {
                btn_addItem.Enabled = true;
            }
        }


        private void cb_designationOfItem_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btn_addPar_Click(object sender, EventArgs e)
        {
            if (cb_cat.SelectedIndex >= 0)
            {
            lbl_try_insert_cat:
                string format = string.Format("name = '{0}'", cb_cat.Text);
                int catid = int.Parse(MainDataSet.Tables["Categories"].Select(string.Format("name = '{0}'", cb_cat.SelectedText))[0]["id"].ToString());
                sqlCMD.CommandText = sqlcmd_commstr_ItemParameter_Insert_INCOMPLETE;
                sqlCMD.Parameters.Add(new SqlParameter("name", tb_newParamName.Text.ToString()));
                try
                {
                    sqlTRS = dbConnection.BeginTransaction("Inserting parameter for category");
                    sqlCMD.Transaction = sqlTRS;
                    if (sqlCMD.ExecuteNonQuery() < 1)
                    {
                        MessageBox.Show("Такой параметр уже есть");
                    }
                    sqlTRS.Commit();
                }
                catch (Exception exc)
                {
                    sqlTRS.Rollback();
                    DialogResult dlgres = MessageBox.Show(exc.Message, "Ошибка базы данных", MessageBoxButtons.RetryCancel);
                    switch (dlgres)
                    {
                        case DialogResult.Cancel:
                            return;
                        case DialogResult.Retry:
                            goto lbl_try_insert_cat;
                    }
                }
                finally
                {
                    sqlCMD.Parameters.Clear();
                }
            }

            else
            {
                MessageBox.Show("Выберите категорию");
            }
        }


        private void tb_newParamName_TextChanged(object sender, EventArgs e)
        {
            if (tb_newParamName.Text.ToString().Length == 0)
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
