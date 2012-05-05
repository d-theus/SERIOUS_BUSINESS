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
        private res.Model1Container database;
        private IQueryable<res.ItemCategory> Categories;
        private IQueryable<res.Item> Items;
        private List<res.Position> curPositions;
        private res.Position newPos = null;
        private res.Item selItem = null;
        private res.Employee curEmployee;
        private res.Consumer curConsumer;
        private res.Order curOrder;

        private DataTable DGV_contentsT;
        public FormEditOrder(ref res.Employee curEmpl)
        {
            InitializeComponent();
            curEmployee = curEmpl;
            DGV_contentsT_Init();
            #region database context & entities filling
            database = new res.Model1Container();
            curPositions = new List<res.Position>();
            curOrder = res.Order.CreateOrder(-1, DateTime.Now, "Обработка", -1, -1);
            Items = from it in database.ItemSet select it;
            Categories = from cat in database.ItemCategorySet select cat;
            #endregion
            #region data sources
            cb_itemType.DataSource = Categories.ToArray();
            cb_itemType.DisplayMember = "name";
            cb_itemType.ValueMember = "id";

            DGV.DataSource = DGV_contentsT;
            #endregion
            #region event_bindings
            cb_itemType.SelectedValueChanged += new EventHandler(btn_addPos_check);
            cb_itemDesignation.SelectedValueChanged += new EventHandler(btn_addPos_check);
            cb_itemDesignation.SelectedValueChanged += new EventHandler(num_itemCount_anull);
            num_itemCount.ValueChanged += new EventHandler(btn_addPos_check);
            num_itemCount.ValueChanged += new EventHandler(num_itemCount_ValueChanged);
            btn_addItem.Click += new EventHandler(btn_rmItem_check);
            btn_rmItem.Click += new EventHandler(btn_rmItem_check);
            btn_addItem.Click += new EventHandler(DGV_contentsT_Refill);
            btn_rmItem.Click += new EventHandler(DGV_contentsT_Refill);
            DGV.SelectionChanged += new EventHandler(btn_rmItem_check);
            #endregion
        }

        void num_itemCount_check(object sender, EventArgs e)
        {
            num_itemCount.Enabled = newPos != null;
        }

        void num_itemCount_ValueChanged(object sender, EventArgs e)
        {
            if (selItem != null && newPos != null)
            {
                if (num_itemCount.Value > selItem.storeResidue) num_itemCount.Value = selItem.storeResidue;
                newPos.count = (int)num_itemCount.Value;
            }
        }

        void btn_addPos_check(object sender, EventArgs e)
        {
            btn_addItem.Enabled = cb_itemDesignation.Text != "" && cb_itemType.Text != "" && (int)num_itemCount.Value > 0;
        }

        void cb_itemDesignation_Refill()
        {
            if (cb_itemType.Text != "")
            {
                try
                {
                    cb_itemDesignation.DataSource = (from item in database.ItemSet
                                                     join designation in database.ItemParameterSet
                                                     on item.id equals designation.itemID
                                                     where designation.ParameterCategory.name == "Наименование" && item.catID == (int)cb_itemType.SelectedValue
                                                     select designation).ToArray();
                    cb_itemDesignation.ValueMember = "itemID";
                    cb_itemDesignation.DisplayMember = "valueTxt";
                }
                catch (System.InvalidCastException)
                {
                    return;
                }
            }
        }

        private void cb_itemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_itemDesignation_Refill();
        }

        private void cb_itemDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                selItem = Items.Single(it => it.id == (int)cb_itemDesignation.SelectedValue);
                if (selItem != null)
                    newPos = res.Position.CreatePosition(-1, -1, 0, selItem.id);
            }
            catch (System.InvalidCastException)
            {
                selItem = null;
                newPos = null;
            }
            finally
            {
                num_itemCount_check(null, null);
            }
        }

        private void btn_addItem_Click(object sender, EventArgs e)
        {
            #region check for same
            if (curPositions.Any(pos => pos.itemID == newPos.itemID))
            {
                MessageBox.Show("Позиция с таким товаром уже есть в списке заказа", "Ошибка добавления позиции");
                return;
            }
            #endregion
            else
            {
                curPositions.Add(res.Position.CreatePosition(-1, -1, newPos.count, newPos.itemID));
            }
        }

        private void num_itemCount_anull(object sender, EventArgs e)
        {
            num_itemCount.Value = 0;
        }

        private void btn_rmItem_check(object sender, EventArgs e)
        {
            btn_rmItem.Enabled = curPositions.Count > 0 && DGV.SelectedRows.Count > 0;
        }

        private void DGV_contentsT_Init()
        {
            DGV_contentsT = new DataTable();
            DGV_contentsT.Columns.Add(new DataColumn("Наименование", "".GetType()));
            DGV_contentsT.Columns["Наименование"].ReadOnly = true;
            DGV_contentsT.Columns.Add(new DataColumn("Количество", 1.GetType()));
        }

        private void DGV_contentsT_Refill(object sender, EventArgs e)
        {
            DGV_contentsT.Clear();
            foreach (var ent in curPositions)
            {
                DataRow newrow = DGV_contentsT.NewRow();
                newrow["Наименование"] = Items.Single(item => item.id == newPos.itemID).ItemParameter.Single(param => param.ParameterCategory.name == "Наименование").valueTxt;
                newrow["Количество"] = ent.count;
                DGV_contentsT.Rows.Add(newrow);
            }
        }

        private void btn_rmItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in DGV.SelectedRows)
            {
                curPositions.RemoveAt(row.Index);
            }
        }

        private void btn_accept_check(object sender, EventArgs e)
        {
            btn_accept.Enabled = tb_Name.Text != "" && tb_phone.Text != "" && curPositions.Count > 0;
        }

        private void btn_accept_Click(object sender, EventArgs e)
        {
            #region check if items still available
            foreach (res.Position pos in curPositions)
            {
                if (pos.count > (from item in database.ItemSet where item.id == pos.itemID select item.storeResidue).Single())
                {
                    Items = from it in database.ItemSet select it;
                    string message = string.Format("Во время составления заказа на складе стало нехватать предметов {0}. Данные об изменении занесены, перезаполните поля.", pos.Item.ItemParameter.Single(par => par.ParameterCategory.name == "Наименование"));
                    MessageBox.Show(message, "Внимание");
                    return;
                }
            }
            #endregion

            #region confirmation
            DialogResult cnf = MessageBox.Show("Подтвердить заказ?", "Подтверждение", MessageBoxButtons.YesNo);
            if (cnf == DialogResult.No)
            {
                return;
            }
            
            #endregion

            #region add order and positions, decrease stock residues
            newPos = null;
            database.AddToOrderSet(curOrder);
            curConsumer = res.Consumer.CreateConsumer(tb_Name.Text, tb_phone.Text, tb_email.Text, -1);
            curOrder.emplID = curEmployee.id;
            curOrder.Consumer = curConsumer;
            foreach (var ent in curPositions)
            {
                Items.Single(item => item.id == ent.itemID).storeResidue -= ent.count;
                curOrder.Position.Add(ent);
            }
            database.SaveChanges();
            this.Close();
            #endregion
        }
    }
}
