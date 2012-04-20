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
    public partial class FormEditOrder : Form
    {
        #region sql conn & commands

        static private SqlConnection dbConnection = null;
        SqlCommand sqlcmd_GetOrderNumber = new SqlCommand("SELECT MAX([id]) FROM " + res.Settings.dbConn_DbName + ".[dbo].[OrderSet]");
        SqlCommand sqlcmd_GetStockCats = new SqlCommand("SELECT [name] FROM " + res.Settings.dbConn_DbName + ".[dbo].[ItemCategorySet]");
        SqlCommand sqlcmd_GetCatItems = new SqlCommand();
        SqlCommand sqlcmd_GetPositions = new SqlCommand(@"SELECT [dbo].[PositionSet].[id] AS [Номер], [valueTxt] AS [Наименование], [count] AS [Количество] FROM
	PositionSet
INNER JOIN
	ItemParameterSet
ON		ItemParameterSet.itemID = PositionSet.itemID 
	AND 
		ItemParameterSet.paramCatID = (SELECT [id] FROM ParameterCategorySet WHERE name = 'Наименование')");
        #endregion

        public bool isOk = false;
        public DialogResult dlgResult;

        private DataSet tables = new DataSet();
        public FormEditOrder(SqlConnection _dbConnection)
        {
            InitializeComponent();
retry:
            try
            {
                #region Init connection and commands
                dbConnection = _dbConnection;
                sqlcmd_GetOrderNumber.Connection = dbConnection;
                sqlcmd_GetStockCats.Connection = dbConnection;
                sqlcmd_GetCatItems.Connection = dbConnection;
                sqlcmd_GetPositions.Connection = dbConnection;
                #endregion

                #region Filling check boxes using DB
                //Order number
                this.lbl_num.Text = sqlcmd_GetOrderNumber.ExecuteScalar().ToString();
                SqlDataReader drd = sqlcmd_GetStockCats.ExecuteReader();
                //Check box with item categories
                while (drd.Read())
                {
                    this.cb_itemType.Items.Add(drd["name"]);
                }
                drd.Dispose();
                #endregion

                #region Positions table initializing

                SqlCommand sqlcmd_GetEmptyPosTable = sqlcmd_GetPositions.Clone();
                sqlcmd_GetEmptyPosTable.CommandText += "WHERE [dbo].[PositionSet].[id] = -1";
                SqlDataAdapter dad = new SqlDataAdapter(sqlcmd_GetEmptyPosTable);
                dad.Fill(tables);
                this.DGV.DataSource = tables.Tables[0];
                DGV.Refresh();
                sqlcmd_GetEmptyPosTable.Dispose();

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
            sqlcmd_GetCatItems.CommandText = @"SELECT [valueTxt] AS [designation]FROM 
ItemParameterSet
INNER JOIN 
ItemSet
ON ItemParameterSet.itemID = ItemSet.id AND [valueTxt] != '' AND ItemSet.catID = 
	(SELECT [id] FROM ItemCategorySet WHERE [name] = '" + cb_itemType.Text.ToString() + "')";
            try
            {
                SqlDataReader drd = sqlcmd_GetCatItems.ExecuteReader();
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
            this.dlgResult = DialogResult.OK;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.dlgResult = DialogResult.Cancel;
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
            MessageBox.Show(tables.Tables[0].Rows.Count.ToString());
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
    }
}
