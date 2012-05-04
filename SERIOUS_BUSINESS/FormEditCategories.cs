using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Data.EntityModel;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace SERIOUS_BUSINESS
{
    public partial class FormEditCategories : Form
    {
        #region sql connection, commands, transaction and strings
        private SqlConnection dbConnection;
        private SqlCommand sqlCMD;
        private SqlTransaction sqlTRS;

        //!!! Insert/Delete procedures may have an impact on other tables !!!

        private const string sqlcmd_commstr_ItemCategory_IsExist_INCOMPLETE = "SELECT COUNT(*) FROM ItemCategorySet WHERE name = @name";
        private const string sqlcmd_commstr_ItemCategory_Insert_INCOMPLETE = "EXEC sp_ItemCategory_INSERT @name"; 
        //+ICPC - creates designation as default property
        private const string sqlcmd_commstr_ItemCategory_List = "SELECT * FROM ItemCategorySet";
        private const string sqlcmd_commstr_Item_List_INCOMPLETE = "SELECT * FROM ItemSet INNER JOIN ItemParameterSet ON [ItemParameterSet].[itemID] = ItemSet.id WHERE [catID] = @catID";
        private const string sqlcmd_commstr_Item_Insert = "EXEC sp_Item_INSERT @catID, @designation";
        private const string sqlcmd_commstr_Item_CheckForSameDes = "EXEC sp_CheckForSameDesignation @designation";
        private const string sqlcmd_commstr_Item_DesignationList_INCOMPLETE = "SELECT ItemSet.id, valueTxt FROM ItemSet INNER JOIN ItemParameterSet ON [ItemParameterSet].[itemID] = ItemSet.id WHERE [catID] = @catID AND paramCatID = (SELECT id FROM ParameterCategorySet WHERE name = 'Наименование')";
        private const string sqlcmd_commstr_ItemParameterCategory_Insert_INCOMPLETE = @"IF (SELECT COUNT(*) FROM ParameterCategorySet WHERE name = @name) = 0 
BEGIN
	INSERT  INTO ParameterCategorySet	
	VALUES (@name, @type)
END";
        private const string sqlcmd_commstr_ItemParameter_ListForCurItem_INCOMPLETE = "SELECT ItemParameterSet.id, name, type, valueTxt, valueDbl, valueBool FROM ItemParameterSet INNER JOIN ParameterCategorySet ON ParameterCategorySet.id = ItemParameterSet.paramCatID WHERE itemID = @itemID";
        private const string sqlcmd_commstr_ItemParameter_List = "SELECT id, name AS [Название], type FROM ParameterCategorySet";
        private const string sqlcmd_commstr_ItemParameter_UPDATE = "UPDATE ItemParameterSet SET [<field>] = @value WHERE id = @pid";
        private const string sqlcmd_commstr_CatHasParam_INCOMPLETE = "SELECT dbo.sf_ICPC_Is_Associated(@icid, @pcid) AS RES";
        private const string sqlcmd_commstr_Parameters_Update = "UPDATE ParameterCategorySet SET name = @name, type = @type WHERE id = @id";
        private const string sqlcmd_commstr_ICPC_Insert = "EXEC sp_ICPC_INSERT @pcid, @icid"; //also creates all associated parameters
        private const string sqlcmd_commstr_ICPC_Delete = "EXEC sp_ICPC_DELETE @pcid, @icid"; //also deletes all associated parameters
        private const string sqlcmd_commstr_ICPC_Select = "SELECT * FROM pureJoin_IPcatsSet";
        #endregion
        DataSet MainDataSet;
        public struct NamedItem
        {
            public int id
            {
                get;
                set;
            }
            public string name
            {
                get;
                set;
            }
        }

        public FormEditCategories(SqlConnection _dbConnection)
        {
            InitializeComponent();
            #region main data set
            MainDataSet = new DataSet();
            MainDataSet.Tables.Add(new DataTable("Categories"));
            MainDataSet.Tables.Add(new DataTable("Items"));
            MainDataSet.Tables.Add(new DataTable("Designations"));
            MainDataSet.Tables.Add(new DataTable("Parameters"));
            MainDataSet.Tables["Parameters"].Columns.Add("Ассоциация с кат.", Type.GetType("System.Boolean"));
            MainDataSet.Tables.Add(new DataTable("CurrentItemParameters"));
            #endregion
            dbConnection = _dbConnection;
            sqlCMD = dbConnection.CreateCommand();

            DGV_itemParameters.DataSource = MainDataSet.Tables["CurrentItemParameters"];
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
                        MainDataSet.Tables["Parameters"].Clear();
                        sda.Fill(MainDataSet.Tables["Parameters"]);
                    }
                    int row_count = MainDataSet.Tables["Parameters"].Rows.Count;
                    for (int row = 0; row < row_count; row++)
                    {
                        MainDataSet.Tables["Parameters"].Rows[row]["Ассоциация с кат."] = isCatHasParam(int.Parse(MainDataSet.Tables["Parameters"].Rows[row]["id"].ToString()), icid);
                    }
                    MainDataSet.Tables["Parameters"].Columns["Название"].ReadOnly = true;
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
            DGV_SetMask(DGV_catParameters);
        }

        private void RefillItemParameterTable()
        {
            if (cb_existingItem.Text.ToString().Length == 0)
            {
                MainDataSet.Tables["CurrentItemParameters"].Clear();
                return;
            }
            int itemID = -1;
            itemID = MainDataSet.Tables["Items"].AsEnumerable().AsQueryable().
                                            First<DataRow>(row => row["valueTxt"].Equals(cb_existingItem.Text.ToString())).Field<int>("id"); 
            using (DataTable dt = new DataTable())
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(sqlCMD))
                {
                    sqlCMD.CommandText = sqlcmd_commstr_ItemParameter_ListForCurItem_INCOMPLETE;
                    sqlCMD.Parameters.Add(new SqlParameter("@itemID", itemID));
                    sda.Fill(dt);
                }
                MainDataSet.Tables["CurrentItemParameters"].Clear();
                DataTable valuesT = new DataTable();
                valuesT.Columns.AddRange(new DataColumn[]  
                                            { new DataColumn("Параметр", Type.GetType("System.String")), 
                                              new DataColumn("type", Type.GetType("System.Int16")),
                                              new DataColumn("id", Type.GetType("System.Int32")),
                                              new DataColumn("Значение", Type.GetType("System.String"))
                                            });
                var parameters = dt.AsEnumerable().AsQueryable();
                foreach (var row in parameters)
                {
                    DataRow newrow = valuesT.NewRow();
                    short type = row.Field<short>("type");
                    try
                    {
                        switch (type)
                        {
                            case 1:
                                newrow["Значение"] = row.Field<string>("valueTxt");
                                break;
                            case 2:
                                newrow["Значение"] = row.Field<double>("valueDbl");
                                break;
                            case 3:
                                newrow["Значение"] = row.Field<bool>("valueBool");
                                break;
                        }
                    }
                    catch (InvalidCastException ice)
                    {
                        switch (type)
                        {
                            case 1:
                                newrow["Значение"] = "";
                                break;
                            case 2:
                                newrow["Значение"] = 0.0d;
                                break;
                            case 3:
                                newrow["Значение"] = false;
                                break;
                        }
                    }
                    newrow["Параметр"] = row.Field<string>("name");
                    newrow["type"] = row.Field<Int16>("type");
                    newrow["id"] = row.Field<int>("id");
                    valuesT.Rows.Add(newrow);
                }
                MainDataSet.Tables["CurrentItemParameters"].Merge(valuesT);
            }
            sqlCMD.Parameters.Clear();
            DGV_SetMask(DGV_itemParameters);
        }

        private bool isCatHasParam(int pcid, int icid)
        {
            bool count = false;
            sqlCMD.CommandText = sqlcmd_commstr_CatHasParam_INCOMPLETE;
            sqlCMD.Parameters.Add(new SqlParameter("pcid", pcid));
            sqlCMD.Parameters.Add(new SqlParameter("icid", icid));
            try
            {
                count = bool.Parse(sqlCMD.ExecuteScalar().ToString());
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
            return count;
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
                DialogResult apprvmnt = MessageBox.Show(string.Format("Добавить новый {0} параметр {1}?", ptype_name, tb_newParamName.Text), "Продложить?", MessageBoxButtons.YesNo);
                if (apprvmnt == DialogResult.No)
                    return;
            lbl_try_insert_cat:
                sqlCMD.CommandText = sqlcmd_commstr_ItemParameterCategory_Insert_INCOMPLETE;
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
                    RefillItemParameterTable();
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
            DialogResult dlgres = MessageBox.Show("Изменения повлекут добавление или удаление параметров. Продолжить?", "Внимание", MessageBoxButtons.YesNo);
            if (dlgres == DialogResult.No)
            {
                return;
            }  
        lbl_try_update_rows:
            DataTable changesT = MainDataSet.Tables["Parameters"].GetChanges();
            var selectRes = MainDataSet.Tables["Categories"].AsEnumerable()
                            .Where(cat => cat.Field<string>("name").Equals(cb_cat.Text.ToString()));//Select(string.Format("name = '{0}'", cb_cat.Text));
            int icid = selectRes.ElementAt(0).Field<int>("id");
            foreach (DataRow ent in changesT.Rows)
            {
                if (ent.Field<string>("name").Equals("Наименование") && ent.Field<bool>("Ассоциация с кат.").Equals(false))
                {
                    MessageBox.Show("Попытка удалить наименование, действие не будет выполнено", "Внимание");
                    MainDataSet.Tables["Parameters"].AsEnumerable().AsQueryable().First<DataRow>(row => row.Field<string>("name").Equals("Наименование"))["Ассоциация с кат."] = true;
                    return;
                }
                sqlTRS = dbConnection.BeginTransaction("Updating ParameterCategorySet");
                sqlCMD.Transaction = sqlTRS;
                int pcid = ent.Field<int>("id");
                sqlCMD.Parameters.AddRange(new SqlParameter[] { new SqlParameter("icid", icid), new SqlParameter("pcid", pcid) });
                if (bool.Parse(ent["Ассоциация с кат."].ToString()))
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
                    sqlTRS.Commit();
                }
                catch (Exception exc)
                {
                    sqlTRS.Rollback();
                    dlgres = MessageBox.Show(exc.Message, "Ошибка", MessageBoxButtons.RetryCancel);
                    switch (dlgres)
                    {
                        case DialogResult.Cancel:
                            sqlCMD.Parameters.Clear();
                            return;
                        case DialogResult.Retry:
                            goto lbl_try_update_rows;
                    }
                }
                finally
                {
                    sqlCMD.Parameters.Clear();
                }
            }
        }

        private void tb_newItemDesignation_TextChanged_1(object sender, EventArgs e)
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

        private void btn_addItem_Click(object sender, EventArgs e)
        {

            if (cb_cat.SelectedIndex >= 0)
            {
            DataRow[] selectRes = MainDataSet.Tables["Categories"].Select(string.Format("name = '{0}'", cb_cat.Text));
            int icid = int.Parse(selectRes[0]["id"].ToString());
            DialogResult appmnt = MessageBox.Show(String.Format("Вы точно хотите добавить предмет {0} в категорию {1}", tb_newItemDesignation.Text, cb_cat.Text), "Добавить предмет к категории?", MessageBoxButtons.YesNo);
            if (appmnt == DialogResult.No)
                return;
            sqlCMD.CommandText = sqlcmd_commstr_Item_CheckForSameDes;
            sqlCMD.Parameters.AddRange(new SqlParameter[] { new SqlParameter("catID", icid), new SqlParameter("designation", tb_newItemDesignation.Text.ToString()) });
            lbl_try_insert_item:
                try
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter(sqlCMD);
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        string messageContents = "";
                        foreach (DataRow ent in dt.Rows)
                        {
                            messageContents += "\n" + ent[0];
                        }
                        DialogResult dlgres = MessageBox.Show("Товары с таким наименованием уже есть в следующих категориях: " + messageContents + "\n Продолжить?", "Возможно дублирование", MessageBoxButtons.YesNo);
                        if (dlgres == DialogResult.No)
                        {
                            sqlCMD.Parameters.Clear();
                            dt.Dispose();
                            sda.Dispose();
                            return;
                        }       
                    }
                    sqlCMD.Parameters.Clear();
                    sqlCMD.Parameters.AddRange(new SqlParameter[] { new SqlParameter("catID", icid), new SqlParameter("designation", tb_newItemDesignation.Text.ToString()) });
                    sqlCMD.CommandText = sqlcmd_commstr_Item_Insert;
                    sqlTRS = dbConnection.BeginTransaction();
                    sqlCMD.Transaction = sqlTRS;
                    sqlCMD.ExecuteNonQuery();
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
                            goto lbl_try_insert_item;
                    }
                }
                finally
                {
                    sqlCMD.Parameters.Clear();
                    RefillItemTable();
                }
            }
            else
            {
                MessageBox.Show("Выберите категорию");
                return;
            }
        }

        private void cb_existingItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefillItemParameterTable();
        }

        private void btn_accItem_Click(object sender, EventArgs e)
        {
            DialogResult appmnt = MessageBox.Show("Применить изменения параметров?", "Внимание", MessageBoxButtons.YesNo);
            if (appmnt == DialogResult.No)
            {
                return;
            }
            if (isDesignationEmpty(MainDataSet.Tables["CurrentItemParameters"]))
            {
                MessageBox.Show("Невозможно присвоить товару пустое наименование", "Ошибка");
                return;
            }
        lbl_try_update_params:
            DataTable changesT = MainDataSet.Tables["CurrentItemParameters"].GetChanges();
            foreach (DataRow ent in changesT.Rows)
            {
                string field = "";
                Int16 type = ent.Field<Int16>("type");
                switch (type)
                {
                    case 1:
                        field = "valueTxt";
                        sqlCMD.Parameters.Add(new SqlParameter("value", ent.Field<string>("Значение")));
                        break;
                    case 2:
                        field = "valueDbl";
                        sqlCMD.Parameters.Add(new SqlParameter("value",Double.Parse(ent.Field<string>("Значение"))));
                        break;
                    case 3:
                        field = "valueBool";
                        sqlCMD.Parameters.Add(new SqlParameter("value", Boolean.Parse(ent.Field<string>("Значение"))));
                        break;
                }
                int pid = ent.Field<int>("id");
                sqlCMD.Parameters.Add(new SqlParameter("pid", pid));
                sqlCMD.CommandText = sqlcmd_commstr_ItemParameter_UPDATE.Replace("<field>", field);
                try
                {
                    sqlTRS = dbConnection.BeginTransaction();
                    sqlCMD.Transaction = sqlTRS;
                    sqlCMD.ExecuteNonQuery();
                    sqlTRS.Commit();
                    sqlCMD.Parameters.Clear();
                }
                catch (Exception exc)
                {
                    sqlTRS.Rollback();
                    sqlCMD.Parameters.Clear();
                    DialogResult dlgres = MessageBox.Show(exc.Message, "Ошибка базы данных", MessageBoxButtons.RetryCancel);
                    switch (dlgres)
                    {
                        case DialogResult.Cancel:
                            return;
                        case DialogResult.Retry:
                            goto lbl_try_update_params;
                    }
                }
            }
        }

        private bool isDesignationEmpty(DataTable tbl)
        {
            DataRow designation_row= tbl.AsEnumerable().AsQueryable().First<DataRow>(row => row.Field<string>("Параметр") == "Наименование");
            return designation_row.Field<string>("Значение").Length == 0;
        }

        private void DGV_SetMask(DataGridView DGV)
        {
            System.Text.RegularExpressions.Regex regexp = new System.Text.RegularExpressions.Regex("^[A-Z, a-z]+$");
            int cnt = DGV.Columns.Count, i = 0;
            while (i < cnt)
            {
                if (regexp.IsMatch(DGV.Columns[i].Name)) DGV.Columns[i].Visible = false;
                i++;
            }
        }
    }
}
