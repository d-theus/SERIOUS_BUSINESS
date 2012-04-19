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
        #endregion

        public bool isOk = false;
        public FormEditOrder(SqlConnection _dbConnection)
        {
            InitializeComponent();
            #region Filling components using DB
                dbConnection = _dbConnection;
                sqlcmd_GetOrderNumber.Connection = dbConnection;
                sqlcmd_GetStockCats.Connection = dbConnection;
                sqlcmd_GetCatItems.Connection = dbConnection;
            retry:
            try
            {
                //Order number
                this.lbl_num.Text = sqlcmd_GetOrderNumber.ExecuteScalar().ToString();
                SqlDataReader drd =  sqlcmd_GetStockCats.ExecuteReader();
                //Check box with item categories
                while (drd.Read())
                {
                    this.cb_itemType.Items.Add(drd["name"]);
                }
                drd.Dispose();
                isOk = true;
            }
            catch (Exception ex)
            {
                DialogResult dlgres = MessageBox.Show(ex.Message, "Ошибка базы данных", MessageBoxButtons.RetryCancel);
                switch (dlgres)
                {
                    case DialogResult.Cancel:
                        break;
                    case DialogResult.Retry:
                        goto retry;
                }
            }
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cb_itemDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cb_itemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_itemDesignation.Items.Clear();
            sqlcmd_GetCatItems.CommandText = @"SELECT [valueTxt] AS [designation]FROM 
ItemParameterSet
INNER JOIN 
ItemSet
ON ItemParameterSet.itemID = ItemSet.id AND [valueTxt] != '' AND ItemSet.catID = 
	(SELECT [id] FROM ItemCategorySet WHERE [name] = '" + cb_itemType.Text.ToString() +"')";
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
    }
}
