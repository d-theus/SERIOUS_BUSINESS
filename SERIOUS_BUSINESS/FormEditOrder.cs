using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SERIOUS_BUSINESS
{
    public partial class FormEditOrder : Form
    {
        #region sql conn & commands & transactions

        private SqlConnection dbConnection = null;
        private res.Employee curUsr = null;
        private res.Consumer curCons = null;
        private res.Order curOrder = null;
        
        
        const string sqlcmd_comstr_ItemParameters_GetDesignations_INCOMPLETE = "SELECT [valueTxt] AS [designation] FROM ItemParameterSet INNER JOIN ItemSet ON ItemParameterSet.itemID = ItemSet.id AND [valueTxt] != '' AND ItemSet.catID = (SELECT [id] FROM ItemCategorySet WHERE [name] = '<ItemCategoryName>'";
        const string sqlcmd_comstr_ItemCategories_GetNames = "SELECT [name] FROM ItemCategorySet";
        const string sqlcmd_comstr_Positions_List = "SELECT [dbo].[PositionSet].[id] AS [Номер],[dbo].[PositionSet].[itemID] AS [Артикул] , [valueTxt] AS [Наименование], [count] AS [Количество] FROM PositionSet INNER JOIN ItemParameterSet ON ItemParameterSet.itemID = PositionSet.itemID AND ItemParameterSet.paramCatID = (SELECT [id] FROM ParameterCategorySet WHERE name = 'Наименование')";
        const string sqlcmd_comstr_Consumers_GetCount = "SELECT COUNT(*) FROM ConsumerSet";
        const string sqlcmd_comstr_Orders_Insert_INCOMPLETE = "INSERT INTO [OrderSet] ([date], [status], [consID], [emplID]) VALUES (<date>, 'Обрабатывается', <consumerID>, <emplID>)";
        const string sqlcmd_comstr_Consumers_Insert_INCOMPLETE = "INSERT INTO ConsumerSet (id, name, phone, email) VALUES ('<id>','<name>' , '<phone>', '<email>')";
        const string sqlcmd_comstr_Orders_GetCurrId_INCOMPLETE = "SELECT [id] FROM OrderSet WHERE date ='<date>'";
        const string sqlcmd_comstr_Positions_Insert_INCOMPLETE = "INSERT INTO PositionSet (id,orderID,itemID,[count]) VALUES (<orderID>,<itemID>,<count>)";


        SqlCommand sqlCMD = null;
        SqlTransaction sqlTRS;
        
        #endregion

        public bool isOk = false;
        public DialogResult dlgResult;

        private DataSet tables = new DataSet();
        public FormEditOrder(SqlConnection _dbConnection, res.Employee _curUsr)
        {
            InitializeComponent();
            curUsr = _curUsr;
retry:
            try
            {
                #region Init connection and commands
                dbConnection = _dbConnection;
                sqlCMD = dbConnection.CreateCommand();
                #endregion

                #region Filling check boxes using DB
                //Order number
                this.lbl_num.Text = "Новый";
                //Check box with item categories
                sqlCMD.CommandText = sqlcmd_comstr_ItemCategories_GetNames;
                SqlDataReader drd = sqlCMD.ExecuteReader();
                while (drd.Read())
                {
                    this.cb_itemType.Items.Add(drd["name"]);
                }
                drd.Dispose();
                #endregion

                #region Positions table initializing
                sqlCMD.CommandText = sqlcmd_comstr_Positions_List + "WHERE [dbo].[PositionSet].[id] = -1"; //empty table as columns source
                SqlDataAdapter dad = new SqlDataAdapter(sqlCMD);
                dad.Fill(tables);
                this.DGV.DataSource = tables.Tables[0];
                DGV.Refresh();

                #endregion
            }
            catch (Exception ex)
            {
                DialogResult dlgres = MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.RetryCancel);
                switch (dlgres)
                {
                    case DialogResult.Cancel:
                        break;
                    case DialogResult.Retry:
                        goto retry;
                }
            }
            this.isOk = true;
        }


        private void cb_itemDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cb_itemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_itemDesignation.Items.Clear();
            cb_itemDesignation.Text = "";
            sqlCMD.CommandText = sqlcmd_comstr_Positions_List.Replace("<ItemCategoryName>", cb_itemType.Text.ToString());
            try
            {
                SqlDataReader drd = sqlCMD.ExecuteReader();
                while (drd.Read())
                {
                    cb_itemDesignation.Items.Add(drd["designation"]);
                }
                drd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка базы данных");
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {

            if (tables.Tables[0].Rows.Count > 0 && !tb_Name.Text.ToString().Equals("") && !tb_phone.Text.ToString().Equals(""))
            {
#region consumer
        try_create_consumer:
                sqlTRS = dbConnection.BeginTransaction("Transaction : New consumer");
                sqlCMD.Transaction = sqlTRS;
                try
                {
                sqlCMD.CommandText = sqlcmd_comstr_Consumers_GetCount;
                curCons = res.Consumer.CreateConsumer(tb_Name.Text.ToString(), tb_phone.Text.ToString(), tb_email.Text.ToString(), int.Parse(sqlCMD.ExecuteScalar().ToString()));
                sqlCMD.CommandText = sqlcmd_comstr_Consumers_Insert_INCOMPLETE.Replace("<id>", curCons.id.ToString()).Replace("<name>", curCons.name.ToString()).Replace("<phone>", curCons.phone.ToString()).Replace("<email>", curCons.email.ToString());
                    sqlCMD.ExecuteNonQuery();
                    sqlTRS.Commit();
                }
                catch(Exception exc)
                {
                    sqlTRS.Rollback();
                    DialogResult result = MessageBox.Show(exc.Message,"Ошибка добавления клиента в базу данных", MessageBoxButtons.RetryCancel);
                    switch (result)
                    {
                        case DialogResult.Cancel:
                            return;
                        case DialogResult.Retry:
                            goto try_create_consumer;
                    }
                }
#endregion
#region order
                curOrder = res.Order.CreateOrder(-1, DateTime.Now, "Набор", curCons.id, curUsr.id); 
        try_create_order:
                sqlCMD.CommandText = sqlcmd_comstr_Orders_Insert_INCOMPLETE.Replace("<consumerID>", curCons.id.ToString()).Replace("<emplId>", curUsr.id.ToString());
                sqlTRS = dbConnection.BeginTransaction("Transaction : New order");
                sqlCMD.Transaction = sqlTRS;
                try
                {
                    sqlCMD.ExecuteNonQuery();
                    sqlCMD.CommandText = sqlcmd_comstr_Orders_GetCurrId_INCOMPLETE.Replace("<date>", curOrder.date.ToString());
                    curOrder.id = int.Parse(sqlCMD.ExecuteScalar().ToString());
                    sqlTRS.Commit();
                }
                catch(Exception exc)
                {
                    sqlTRS.Rollback();
                    DialogResult result = MessageBox.Show(exc.Message, "Ошибка добавления заказа в базу данных", MessageBoxButtons.RetryCancel);
                    switch (result)
                    {
                        case DialogResult.Cancel:
                            return;
                        case DialogResult.Retry:
                            goto try_create_order;
                    }
                }
#endregion
#region positions
            try_create_positions_set:
                sqlTRS = dbConnection.BeginTransaction("Transaction : New order");
                sqlCMD.Transaction = sqlTRS;
                try
                {
                    foreach (DataRow entry in tables.Tables[0].Rows)
                    {
                        sqlCMD.CommandText = sqlcmd_comstr_Positions_Insert_INCOMPLETE.Replace("<orderID>",curOrder.id.ToString()).Replace("<count>",entry["Количество"].ToString()).Replace("<itemID>", entry["Артикул"].ToString());
                        sqlCMD.ExecuteNonQuery();
                    }
                    sqlTRS.Commit();
                }
                catch (Exception exc)
                {
                    sqlTRS.Rollback();
                    DialogResult result = MessageBox.Show(exc.Message, "Ошибка добавления заказа в базу данных", MessageBoxButtons.RetryCancel);
                    switch (result)
                    {
                        case DialogResult.Cancel:
                            return;
                        case DialogResult.Retry:
                            goto try_create_positions_set;
                    }
                }
#endregion
            }

            this.dlgResult = DialogResult.OK;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.dlgResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_addItem_Click(object sender, EventArgs e)
        {
            if (cb_itemDesignation.Text.ToString().Length != 0 && num_itemCount.Value > 0)
            {
                tables.Tables[0].BeginLoadData();
                DataRow drow = tables.Tables[0].NewRow();
                drow[0] = tables.Tables[0].Rows.Count +1;
                drow[1] = cb_itemDesignation.SelectedItem.ToString();
                drow[2] = num_itemCount.Value;
                tables.Tables[0].Rows.Add(drow);
                tables.Tables[0].EndLoadData();

                DGV_DataSourceChanged(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Не выбран товар или количество выбранных товаров равно нулю");
            }
        }

        private void DGV_DataSourceChanged(object sender, EventArgs e)
        {
            if (tables.Tables[0].Rows.Count > 0)
                this.btn_rmItem.Enabled = true;
            else
                this.btn_rmItem.Enabled = false;
            this.Refresh();
        }

        private void btn_rmItem_Click(object sender, EventArgs e)
        {
            tables.Tables[0].Rows.RemoveAt(tables.Tables[0].Rows.Count -1);
            DGV_DataSourceChanged(this, EventArgs.Empty);
        }

        private void FormEditOrder_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

    }
}
