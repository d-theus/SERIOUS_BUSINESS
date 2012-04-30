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
	VALUES (@name, @type)
END";
        private const string sqlcmd_commstr_ItemParameter_ListForCurItem_INCOMPLETE = "SELECT name, valueTxt, valueDbl, valueBool FROM ItemParameterSetINNER JOIN ParameterCategorySet ON ParameterCategorySet.id = ItemParameterSet.paramCatID";
        private const string sqlcmd_commstr_ItemParameter_List = "SELECT * FROM ParameterCategorySet";
        private const string sqlcmd_commstr_CatHasParam_INCOMPLETE = @"SELECT COUNT(*) FROM 
pureJoin_IPcatsSet
WHERE ICID = @icid AND PCID = @pcid";
        private const string sqlcmd_commstr_Parameters_Update = "UPDATE ParameterCategorySet SET name = @name, type = @type WHERE id = @id";
        private const string sqlcmd_commstr_ICPC_Insert = "IF (SELECT COUNT(*) FROM pureJoin_IPcatsSet WHERE ICID = @icid AND PCID = @pcid) = 0 BEGIN INSERT INTO pureJoin_IPcatsSet (ICID, PCID) VALUES (@icid, @pcid) END";
        private const string sqlcmd_commstr_ICPC_Delete = "DELETE FROM pureJoin_IPcatsSet WHERE ICID = @icid AND PCID = @pcid";
        private const string sqlcmd_commstr_ICPC_Select = "SELECT * FROM pureJoin_IPcatsSet";
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
            MainDataSet.Tables.Add(new DataTable("ICPC"));
            #endregion
            dbConnection = _dbConnection;
            sqlCMD = dbConnection.CreateCommand();

            DGV_itemParameters.DataSource = MainDataSet.Tables["Parameters"];
            cb_cat.DataSource = MainDataSet.Tables["Categories"];
            cb_existingItem.DataSource = MainDataSet.Tables["Designations"];
            DGV_catParameters.DataSource = MainDataSet.Tables["Parameters"];

            RefillCategoriesTable();
            cb_cat.DisplayMember = "name";
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
                    MainDataSet.Tables["Categories"].PrimaryKey = new DataColumn[] { MainDataSet.Tables["Categories"].Columns["name"] };
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
                        //MainDataSet.Tables["Parameters"].Clear();
                        if (MainDataSet.Tables["Parameters"].Columns.Count < 2)
                            sda.Fill(MainDataSet.Tables["Parameters"]);
                        else
                            sda.Update(MainDataSet.Tables["Parameters"]);
                    }
                    int row_count = MainDataSet.Tables["Parameters"].Rows.Count;
                    for (int row = 0; row < row_count; row++)
                    {
                        MainDataSet.Tables["Parameters"].Rows[row]["AssocWithCat"] = isCatHasParam(int.Parse(MainDataSet.Tables["Parameters"].Rows[row]["id"].ToString()), icid);
                    }
                    MainDataSet.Tables["Parameters"].Columns["name"].ReadOnly = true;
                    MainDataSet.Tables["Parameters"].Columns["id"].ReadOnly = true;
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
            }
            else
            {
                MessageBox.Show("Выберите категорию");
            }
        }

        private void RefillICPC()
        {
        lbl_try_fill_ICPC:
            sqlCMD.CommandText = sqlcmd_commstr_ICPC_Select;
            try
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(sqlCMD))
                {
                    MainDataSet.Tables["ICPC"].Clear();
                    sda.Fill(MainDataSet.Tables["ICPC"]);
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
                        goto lbl_try_fill_ICPC;
                }
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
            short ptype = 0;
            string ptype_name = "";
            if (rb_text.Checked) { ptype = 1; ptype_name = "Текстовый"; }
            else if (rb_numeric.Checked) { ptype = 2; ptype_name = "Числовой"; }
            else if (rb_binary.Checked) { ptype = 3; ptype_name = "Двоичный"; }
            else
            {
                MessageBox.Show("Выберите тип нового параметра");
                return;
            }
            if (cb_cat.SelectedIndex >= 0)
            {
                DialogResult apprvmnt = MessageBox.Show(string.Format("Добавить новый {0} параметр {1}?", ptype_name, tb_newParamName.Text), "Продложить?", MessageBoxButtons.YesNo);
                if (apprvmnt == DialogResult.No)
                    return;
            lbl_try_insert_cat:
                sqlCMD.CommandText = sqlcmd_commstr_ItemParameter_Insert_INCOMPLETE;
                sqlCMD.Parameters.Add(new SqlParameter("name", tb_newParamName.Text.ToString()));
                sqlCMD.Parameters.Add(new SqlParameter("type", ptype));
                try
                {
                    sqlTRS = dbConnection.BeginTransaction("Inserting parameter");
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

        private void btn_accParams_Click(object sender, EventArgs e)
        {
        lbl_try_update_rows:
            DataTable changesT = MainDataSet.Tables["Parameters"].GetChanges();
            DataRow[] selectRes = MainDataSet.Tables["Categories"].Select(string.Format("name = '{0}'", cb_cat.Text));
            int icid = int.Parse(selectRes[0]["id"].ToString());
            sqlTRS = dbConnection.BeginTransaction("Updating ParameterCategorySet");
            sqlCMD.Transaction = sqlTRS;
            foreach (DataRow ent in changesT.Rows)
            {
                int pcid = int.Parse(ent["id"].ToString());
                sqlCMD.Parameters.AddRange(new SqlParameter[] { new SqlParameter("icid", icid), new SqlParameter("pcid", pcid) });
                if (bool.Parse(ent["AssocWithCat"].ToString()))
                {
                    sqlCMD.CommandText = sqlcmd_commstr_ICPC_Insert;
                }
                else
                {
                    sqlCMD.CommandText = sqlcmd_commstr_ICPC_Delete;
                }
                try
                {
                    int cnt = sqlCMD.ExecuteNonQuery();
                 //   if (cnt < 1)
                 //       throw new Exception("No rows was changed");
                    sqlCMD.Parameters.Clear();
                }
                catch (Exception exc)
                {
                    sqlTRS.Rollback();
                    sqlCMD.Parameters.Clear();
                    DialogResult dlgres = MessageBox.Show(exc.Message, "Ошибка", MessageBoxButtons.RetryCancel);
                    switch (dlgres)
                    {
                        case DialogResult.Cancel:
                            return;
                        case DialogResult.Retry:
                            goto lbl_try_update_rows;
                    }
                }
            }
            sqlTRS.Commit();
        }
    }
}
