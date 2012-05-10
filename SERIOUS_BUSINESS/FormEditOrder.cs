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
        enum OrderMode { mode_new, mode_edit };
        short mode;
        private res.Model1Container database;
        private IQueryable<res.ItemCategory> Categories;
        private IQueryable<res.Item> Items;
        private List<PositionForOrder> curPositions;
        private res.Employee curEmployee;
        private res.Consumer curConsumer;
        private res.Order curOrder;
        int curItemStockRes = 0;

        private DataTable DGV_contentsT;

        public FormEditOrder(ref res.Employee curEmpl)
        {
            InitializeComponent();
            mode = (short)OrderMode.mode_new;
            curEmployee = curEmpl;
            #region database context & entities filling
            database = new res.Model1Container(RegistryInteractor.GetFromReg("Connection String"));
            curPositions = new List<PositionForOrder>();
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
            cb_itemType.SelectedValueChanged += new EventHandler(this.cb_itemType_SelectedIndexChanged);

            cb_itemDesignation.SelectedIndexChanged += new EventHandler(this.cb_itemDesignation_SelectedIndexChanged);
            cb_itemDesignation.SelectedValueChanged += new EventHandler(this.num_itemCount_check);
            cb_itemDesignation.SelectedValueChanged += new EventHandler(btn_addPos_check);
            cb_itemDesignation.SelectedValueChanged += new EventHandler(num_itemCount_anull);

            num_itemCount.ValueChanged += new EventHandler(btn_addPos_check);
            num_itemCount.ValueChanged += new EventHandler(num_itemCount_ValueChanged);

            btn_addItem.Click += new EventHandler(btn_addItem_Click);
            btn_addItem.Click += new EventHandler(btn_rmItem_check);
            btn_addItem.Click += new EventHandler(DGV_contentsT_Refill);
            btn_addItem.Click += new EventHandler(this.btn_accept_check);

            btn_rmItem.Click += new EventHandler(btn_rmItem_Click);
            btn_rmItem.Click += new EventHandler(btn_rmItem_check);
            btn_rmItem.Click += new EventHandler(DGV_contentsT_Refill);
            btn_rmItem.Click += new EventHandler(this.btn_accept_check);

            btn_accept.Click += new EventHandler(btn_accept_Click);

            tb_Name.TextChanged += new EventHandler(this.btn_accept_check);
            tb_phone.TextChanged += new EventHandler(this.btn_accept_check);

            DGV.SelectionChanged += new EventHandler(btn_rmItem_check);
            DGV.CellValueChanged += new DataGridViewCellEventHandler(DGV_CellValueChanged);
            DGV.CellValueChanged +=new DataGridViewCellEventHandler(DGV_contentsT_Refill);
            DGV.DataError += new DataGridViewDataErrorEventHandler(DGV_DataError);
            #endregion
        }

        public FormEditOrder(ref res.Employee curEmpl, res.Item[] _preselItems)
            : this(ref curEmpl)
        {
            bool all = true;
            foreach (var item in _preselItems)
            {
                if (item.storeResidue >= 1)
                {
                    curPositions.Add(new PositionForOrder { id = item.id, Наименование = item.ItemParameter.Single(par => par.ParameterCategory.name == "Наименование").valueTxt, Количество = 1 });
                }
                else
                {
                    all = false;
                }
            }
            DGV_contentsT_Refill(null, null);
            if (!all) MessageBox.Show("Некоторых позиций в данный момент нет на складе, они не были добавлены в список");
        }

        public FormEditOrder(ref res.Employee curEmpl, ref res.Order _presetOrder)
            : this(ref curEmpl)
        {
            this.curOrder = _presetOrder;
            this.mode = (short)OrderMode.mode_edit;
            this.lbl_num.Text = _presetOrder.id.ToString();
            foreach (var pos in _presetOrder.Position)
                this.curPositions.Add(new PositionForOrder { id = pos.itemID, Количество = pos.count, Наименование = pos.Item.ItemParameter.Single(par => par.ParameterCategory.name == "Наименование").valueTxt });
            this.tb_Name.Text = _presetOrder.Consumer.name;
            this.tb_phone.Text = _presetOrder.Consumer.phone;
            this.tb_email.Text = _presetOrder.Consumer.email;

            DGV_contentsT_Refill(this, null);
        }

        void DGV_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Не удалось распознать введенное значение как количество");
            DGV_contentsT.Rows[e.RowIndex][e.ColumnIndex] = 1;
            return;
        }

        void DGV_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int newVal = (int)DGV_contentsT.Rows[e.RowIndex][e.ColumnIndex];
            int itemID = (int)DGV_contentsT.Rows[e.RowIndex][0];
            if (newVal <= 0)
            {
                var cnf = MessageBox.Show(string.Format("Вы точно хотите убрать товар {0} из списка позиций?", DGV_contentsT.Rows[e.RowIndex]["Наименование"]), "Внимание", MessageBoxButtons.YesNo);
                switch (cnf)
                {
                    case DialogResult.Yes:
                        curPositions.RemoveAll(pos => pos.id == itemID);
                        break;
                    case DialogResult.No:
                        curPositions.Single(pos => pos.id == itemID).Количество = 1;
                        break;
                }
            }
            else
            {
                int residue = (from items in database.ItemSet where items.id == itemID select items.storeResidue).FirstOrDefault();
                if (mode == (short)OrderMode.mode_edit)
                {
                    int delta = PositionDelta.Calculate((from pos in database.PositionSet where pos.orderID == curOrder.id select pos).FirstOrDefault(), newVal);

                    if (delta <= residue)
                    {
                        curPositions.Single(pos => pos.id == itemID).Количество = newVal;
                    }
                    else
                    {
                        curPositions.Single(pos => pos.id == itemID).Количество += residue;
                        MessageBox.Show("На складе не осталось такого количества товаров этого типа, в таблицу занесено максимальное количество");
                        return;
                    }
                }
                else
                {
                    if (newVal <= residue)
                    {
                        curPositions.ElementAt(e.RowIndex).Количество = newVal;
                    }
                    else
                    {
                        curPositions.ElementAt(e.RowIndex).Количество = residue;
                        MessageBox.Show("На складе не осталось такого количества товаров этого типа, в таблицу занесено максимальное количество");
                        return;
                    }
                }

            }
        }

        void num_itemCount_check(object sender, EventArgs e)
        {
            try
            {
                num_itemCount.Enabled = (int)cb_itemDesignation.SelectedValue > 0;
            }
            catch (InvalidCastException)
            {
                return;
            }
        }

        void num_itemCount_ValueChanged(object sender, EventArgs e)
        {
            if ((int)num_itemCount.Value > curItemStockRes)
            {
                num_itemCount.Value = curItemStockRes;
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
                if (Items.Any(itm => itm.id == (int)cb_itemDesignation.SelectedValue))
                {
                    curItemStockRes = Items.Single(item => item.ItemParameter.FirstOrDefault(p => p.ParameterCategory.name == "Наименование").valueTxt == cb_itemDesignation.Text).storeResidue;
                }
            }
            catch (InvalidCastException)
            {
                return;
            }
        }

        private void btn_addItem_Click(object sender, EventArgs e)
        {
            #region check for same
            MessageBox.Show("Selected id: " + cb_itemDesignation.SelectedValue);
            if (curPositions.Any(pos => pos.id == (int)cb_itemDesignation.SelectedValue))
            {
                MessageBox.Show("Позиция с таким товаром уже есть в списке заказа", "Ошибка добавления позиции");
                return;
            }
            #endregion
            else
            {
                try
                {
                    curPositions.Add(new PositionForOrder { id = (int)cb_itemDesignation.SelectedValue, Наименование = cb_itemDesignation.Text, Количество = (int)num_itemCount.Value });
                }
                catch (InvalidCastException)
                {
                    MessageBox.Show("Ошибка добавления");
                    return;
                }
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

        private void DGV_contentsT_Refill(object sender, EventArgs e)
        {
            TableOperator.SetNewContentCommon(curPositions.ToArray(), ref DGV_contentsT);
            DGV.DataSource = DGV_contentsT;
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
            if (mode == (short)OrderMode.mode_new)
            {
                #region check if items still available
                foreach (PositionForOrder pos in curPositions)
                {
                    if (pos.Количество > (from item in database.ItemSet where item.id == pos.id select item.storeResidue).Single())
                    {
                        Items = from it in database.ItemSet select it;
                        string message = string.Format("Во время составления заказа на складе стало нехватать предметов {0}. Данные об изменении занесены, перезаполните поля.", pos.Наименование);
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

                curOrder.Employee = (from emp in database.EmployeeSet where emp.id == curEmployee.id select emp).Single();
                curOrder.Consumer = res.Consumer.CreateConsumer(tb_Name.Text, tb_phone.Text, tb_email.Text, 0);
                foreach (var ent in curPositions)
                {

                    Items.Single(item => item.id == ent.id).storeResidue -= ent.Количество;
                    Items.Single(item => item.id == ent.id).demand += ent.Количество;
                    curOrder.Position.Add(res.Position.CreatePosition(0, 0, ent.Количество, ent.id));
                }
                database.AddToOrderSet(curOrder);
                #endregion
            }
            else
            {
                #region confirmation
                DialogResult cnf = MessageBox.Show("Подтвердить изменение заказа?", "Подтверждение", MessageBoxButtons.YesNo);
                if (cnf == DialogResult.No)
                {
                    return;
                }
                #endregion
                #region get difference
                List<PositionDelta> deltas = new List<PositionDelta>();
                List<res.Position> newPositions = new List<res.Position>();
                foreach (var pos in curPositions)
                {
                    res.Position old = null;
                    old = curOrder.Position.FirstOrDefault(opos => opos.itemID == pos.id);
                    if (old != null)
                        deltas.Add(new PositionDelta(old, pos));
                    else
                        newPositions.Add(res.Position.CreatePosition(curOrder.id, 0, pos.Количество, pos.id));
                }
                #endregion
                #region increase or decrease stock residue, demand for OLD, change OLD
                var DBpositions = from pos in database.PositionSet where pos.orderID == curOrder.id select pos;
                foreach (var pos in DBpositions)
                {
                    res.Position posRef = DBpositions.Single(rpos => rpos.id == pos.id);
                    PositionDelta.ApplyPosAndItem(ref posRef, deltas.Single(del => del.id == pos.id));
                    database.ApplyCurrentValues<res.Position>("PositionSet", posRef);
                    database.ApplyCurrentValues<res.Item>("ItemSet", posRef.Item);
                }
                #endregion
                #region increase or decrease stock residue, demand for NEW, add NEW to order
                foreach (var pos in newPositions)
                {
                    pos.Item.storeResidue -= pos.count;
                    pos.Item.demand += pos.count;
                    database.ApplyCurrentValues<res.Item>("ItemsSet", pos.Item);
                    res.Position refPos = newPositions.Single(rpos => rpos.id == pos.id);
                    curOrder.Position.Add(refPos);
                }
                #endregion
                var consRef = (from cons in database.ConsumerSet where cons.id == curOrder.consID select cons).Single();
                var ordRef = (from ord in database.OrderSet where ord.id == curOrder.id select ord).Single();
                consRef.name = tb_Name.Text;
                consRef.phone = tb_phone.Text;
                consRef.email = tb_email.Text;
                database.ApplyCurrentValues("ConsumerSet", consRef);
                database.ApplyCurrentValues("OrderSet", ordRef);
            }
            database.SaveChanges();
            this.Close();
        }
    }
}
