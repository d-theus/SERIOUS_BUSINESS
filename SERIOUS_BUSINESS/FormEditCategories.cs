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
	INSERT INTO pureJoin_IPcatsSet VALUES
	(@catID, IDENT_CURRENT('ParameterCategorySet')) 
END";
        private const string sqlcmd_commstr_Item_Insert_INCOMPLETE = "";
        private const string sqlcmd_commstr_ItemParameter_ListForCurItem_INCOMPLETE = "SELECT name, valueTxt, valueDbl, valueBool FROM ItemParameterSetINNER JOIN ParameterCategorySet ON ParameterCategorySet.id = ItemParameterSet.paramCatID";
        #endregion
        DataSet MainDataSet;

        public FormEditCategories(SqlConnection _dbConnection)
        {
            InitializeComponent();
            MainDataSet = new DataSet();
            MainDataSet.Tables.Add(new DataTable("Categories"));
            MainDataSet.Tables.Add(new DataTable("Items"));
            MainDataSet.Tables.Add(new DataTable("Designations"));
            MainDataSet.Tables.Add(new DataTable("Parameters"));
            dbConnection = _dbConnection;
            sqlCMD = dbConnection.CreateCommand();

            DGV_itemParameters.DataSource = MainDataSet.Tables["Parameters"];
            cb_cat.DataSource = MainDataSet.Tables["Categories"];
            cb_designationOfItem.DataSource = MainDataSet.Tables["Designations"];

            RefillCategoriesTable();
            cb_cat.DisplayMember = "name";
            cb_cat.ValueMember = "id";
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
                    cb_cat.Update();
                }
            }
        }

        private void RefillItemTable()
        {
            if (cb_cat.SelectedIndex >= 0)
            {
            getcurrentitems:
                string format = string.Format("name = '{0}'", cb_cat.Text);
                int catid = int.Parse(MainDataSet.Tables["Categories"].Select(string.Format("name = '{0}'", cb_cat.SelectedText))[0]["id"].ToString());
                sqlCMD.CommandText = sqlcmd_commstr_Item_List_INCOMPLETE;
                sqlCMD.Parameters.Add(new SqlParameter("catid", catid));
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
                    cb_designationOfItem.DisplayMember = "valueTxt";
                    cb_designationOfItem.ValueMember = "id";
                    cb_designationOfItem.Update();
                }
            }
            else
            {
                MessageBox.Show("Выберите категорию");
            }
        }

        private void RefillParameterCategoryList()
        {
            //try_get_param_list:
            //    SqlDataReader rdr = null;
            //    try
            //    {
            //        sqlCMD.CommandText = sqlcmd_commstr_ItemParameter_ListForCurItem;
            //        sqlCMD.Parameters.Add(new SqlParameter("categoryID", CategoryList.Find(x => x.name.ToString() == cb_cat.SelectedItem.ToString()).id.ToString()));

            //    }
            //    catch (Exception exc)
            //    {
            //        DialogResult dlgres = MessageBox.Show(exc.Message, "Ошибка", MessageBoxButtons.RetryCancel);
            //        switch (dlgres)
            //        {
            //            case DialogResult.Cancel:
            //                return;
            //            case DialogResult.Retry:
            //                goto try_get_param_list;
            //        }
            //    }
            //    finally
            //    {
            //        if (rdr != null)
            //            rdr.Dispose();
            //        sqlCMD.Parameters.Clear();
            //    }
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

        private void cb_cat_SelectedIndexChanged(object sender, EventArgs e)
        { 
            RefillItemTable();
        }

        private void tb_itemDesignation_TextChanged(object sender, EventArgs e)
        {
            if (tb_itemDesignation.Text.ToString().Length == 0)
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
                sqlCMD.Parameters.Add(new SqlParameter("name", tb_name.Text.ToString()));
                sqlCMD.Parameters.Add(new SqlParameter("catID", catid));
                    try
                    {
                        sqlTRS = dbConnection.BeginTransaction("Inserting parameter for category");
                        sqlCMD.Transaction = sqlTRS;
                        if (sqlCMD.ExecuteNonQuery() < 1)
                        {
                            MessageBox.Show("Такой параметр");
                    }
                catch
                    {
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

    }
}
