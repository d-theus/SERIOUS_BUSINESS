using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SERIOUS_BUSINESS.res;

namespace SERIOUS_BUSINESS
{
    enum accessModifiers { acc_none, acc_stock, acc_ord, acc_adm };
    public partial class FormMain : Form
    {

        private res.Model1Container database;
        private res.Employee curEmpl;
        private List<TableWithAccessAndCMS> availableTables;
        private DataTable DGV_contentsT;
        private Dictionary<RadioButton, Func<object, object, bool>> searchPredicate;

        public FormMain()
        {
            InitializeComponent();
            this.Hide();
            Login();
            #region event bindings

            cb_table.SelectedIndexChanged += new EventHandler(this.check_cb_tableOptions);
            cb_table.SelectedIndexChanged += new EventHandler(this.cb_table_SelectedIndexChanged);
            cb_table.SelectedIndexChanged += new EventHandler(this.check_panel_Search);

            cb_tableOptions.SelectedIndexChanged += new EventHandler(cb_tableOptions_SelectedIndexChanged);
            cb_tableOptions.SelectedIndexChanged += new EventHandler(this.check_panel_Search);

            cb_parameterName.SelectedIndexChanged += new EventHandler(cb_parameterName_SelectedIndexChanged);
            cb_parameterName.SelectedIndexChanged += new EventHandler(this.check_criteria);

            panel_Search.EnabledChanged += new EventHandler(this.check_criteria);

            tb_search.TextChanged += new EventHandler(tb_search_TextChanged);

            DGV.SelectionChanged += new EventHandler(this.check_panel_Search);
            DGV.SelectionChanged += new EventHandler(DGV_SelectionChanged);
            DGV.DataSourceChanged += new EventHandler(this.cb_cb_parameterName_Refill);
            DGV.DataSourceChanged += new EventHandler(this.check_btn_Search);
            DGV.DataSourceChanged += new EventHandler(this.check_panel_Search);

            btn_find.Click += new EventHandler(btn_find_Click);
            btn_find.Click += new EventHandler(this.check_panel_Search);

            btn_ClearFilter.Click += new EventHandler(btn_ClearFilter_Click);
            #endregion

            //UIE.CmB_SetToFirst(ref cb_tableOptions);
            //UIE.CmB_SetToFirst(ref cb_table);
            //UIE.CmB_SetToFirst(ref cb_parameterName);
        }

        //################# INITIALIZATION ############################

        private void cb_table_Init_And_Fill()
        {
            availableTables = new List<TableWithAccessAndCMS>();

            availableTables.AddRange(new TableWithAccessAndCMS[] { 
            new TableWithAccessAndCMS("Склад", (int)accessModifiers.acc_stock, this.CMS_STORE),
            new TableWithAccessAndCMS("Характеристики товаров", (int)accessModifiers.acc_ord, this.CMS_MGR_S),
            new TableWithAccessAndCMS("Заказы сотрудника", (int)accessModifiers.acc_ord, this.CMS_MGR_O),
            new TableWithAccessAndCMS("Все заказы", (int)accessModifiers.acc_adm, this.CMS_ADM_O),
            new TableWithAccessAndCMS("Сотрудники", (int)accessModifiers.acc_adm, this.CMS_ADM_EMP)});

            cb_table.DataSource = availableTables.Where(tbl => tbl.accessMod == curEmpl.Appointment.accessModifier || curEmpl.Appointment.accessModifier == (int)accessModifiers.acc_adm).ToList();
            cb_table.ValueMember = "accessMod";
            cb_table.DisplayMember = "name";

            cb_table.SelectAll();
            this.cb_table_SelectedIndexChanged(this, null);
        }

        private void search_Panel_Initialization()
        {
            searchPredicate = new Dictionary<RadioButton, Func<object, object, bool>>();
            Func<object, object, bool> ME = delegate(object x, object y) { return Double.Parse(x.ToString()) <= Double.Parse(y.ToString()); };
            Func<object, object, bool> LE = delegate(object x, object y) { return Double.Parse(x.ToString()) >= Double.Parse(y.ToString()); };
            Func<object, object, bool> E = delegate(object x, object y) { return x.Equals(y); };
            searchPredicate.Add(rb_ME, ME);
            searchPredicate.Add(rb_E, E);
            searchPredicate.Add(rb_LE, LE);
        }

        //################# IDENTIFICATION ############################

        private void Login()
        {
            FormLogin flogin = new FormLogin();
            DialogResult res = flogin.ShowDialog();
            switch (res)
            {
                case DialogResult.OK:
                    this.Enabled = true;
                    this.curEmpl = flogin.usr;
                    curEmpl.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.user_prop_changed);
                    this.Text = Settings.AppTitle + " - " + curEmpl.login;

                    database = new res.Model1Container(RegistryInteractor.GetFromReg("Connection String"));
                    search_Panel_Initialization();
                    cb_table_Init_And_Fill();

                    this.Enabled = true;
                    this.Show();

                    break;
                default:
                    MessageBox.Show("Окно авторизации было закрыто", "Ошибка авторизации");
                    this.Show();
                    break;
            }
        }

        private void Logout()
        {
            this.curEmpl = null;
            this.Enabled = false;
            this.Hide();
        }

        //################# COMBO BOXES ###############################

        private void cb_table_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cb_table.Text)
            {
                case "Склад":
                    #region stock for stock
                    try
                    {
                        IQueryable<StockForStock> view =
                            from item in database.ItemSet
                            select new StockForStock
                            {
                                id = item.id,
                                Категория = item.ItemCategory.name,
                                Наименование = item.ItemParameter.FirstOrDefault(par => par.ParameterCategory.name == "Наименование").valueTxt,
                                Остаток = item.storeResidue
                            };
                        List<StockForStock> Aview = new List<StockForStock>(); 
                        DateTime since = DateTime.Now.AddMonths(-1);
                        foreach (var row in view)
                        {
                            var position = from pos in database.PositionSet 
                                                 where pos.itemID == row.id && pos.Order.date >= since 
                                                 select pos;
                            if (position.Any())
                            {
                                row.Спрос = position.Sum(pos => pos.count);
                                Aview.Add(row);
                            }
                            else
                            {
                                row.Спрос = 0;
                                Aview.Add(row);
                            }
                        }
                        TableOperator.SetNewContentCommon(Aview.ToArray(), ref DGV_contentsT);
                        TableOperator.FormatHeaders(ref DGV_contentsT);
                        DGV.DataSource = DGV_contentsT;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Ошибка" + exc.Message);
                        return;
                    }
                    #endregion
                    break;
                case "Заказы сотрудника":
                    #region Manager orders
                    try
                    {
                        var Orders = from ord in database.OrderSet
                                     where ord.emplID == curEmpl.id
                                     select new OrdersForManager
                                     {
                                         Дата = ord.date,
                                         Номер = ord.id,
                                         Заказчик = ord.Consumer.name,
                                         Статус = ord.status
                                     };
                        TableOperator.SetNewContentCommon(Orders.ToArray(), ref DGV_contentsT);
                        DGV.DataSource = DGV_contentsT;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Ошибка" + exc.Message);
                        return;
                    }
                    #endregion
                    break;
                case "Все заказы":
                    #region All orders
                    try
                    {
                        var Orders = from ord in database.OrderSet
                                     select
                                         new AllOrders
                                         {
                                             Дата = ord.date,
                                             Номер = ord.id,
                                             Заказчик = ord.Consumer.name,
                                             Статус = ord.status,
                                             Сотрудник = ord.Employee.name
                                         };
                        TableOperator.SetNewContentCommon(Orders.ToArray(), ref DGV_contentsT);
                        DGV.DataSource = DGV_contentsT;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Ошибка: \n" + exc.Message);
                        return;
                    }
                    #endregion
                    break;
                case "Сотрудники":
                    #region Employees
                    try
                    {
                        IEnumerable<Employees> EView = from emp in database.EmployeeSet
                                                       select
                                                           new Employees
                                                           {
                                                               Номер = emp.id,
                                                               Имя = emp.name,
                                                               Логин = emp.login,
                                                               Доступ = emp.Appointment.name
                                                           };
                        TableOperator.SetNewContentCommon(EView.ToArray(), ref DGV_contentsT);
                        DGV.DataSource = DGV_contentsT;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Произошла ошибка :\n" + exc.Message);
                    }
                    #endregion
                    break;
            }

            if (availableTables.Where(t => t.name == cb_table.Text.ToString()).Any())
                DGV.ContextMenuStrip = availableTables.Single(t => t.name == cb_table.Text.ToString()).mstrip;
        }

        private void cb_tableOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cb_table.Text)
            {
                case "Характеристики товаров":
                    int catID = 0;
                    #region try parse selection
                    try
                    {
                        catID = (int)cb_tableOptions.SelectedValue;
                    }
                    catch (InvalidCastException)
                    {
                        return;
                    }
                    #endregion
                    #region stock for manager
                    try
                    {
                        IQueryable<StockForManager> StockDB = from item in database.ItemSet
                                                              where item.catID == catID
                                                              select
                                                                  new StockForManager
                                                                  {
                                                                      id = item.id,
                                                                      stockResidue = item.storeResidue
                                                                  };
                        List<StockForManager> Stock = new List<StockForManager>();
                        foreach (var item in StockDB)
                        {
                            NamedParameter[] cItemParams = NamedParameter.CastToNamed((from items in database.ItemSet where items.id == item.id select items).Single().ItemParameter.AsQueryable()).ToArray();
                            Stock.Add(new StockForManager { id = item.id, stockResidue = item.stockResidue, Parameters = cItemParams.ToList() });
                        }
                        TableOperator.SetNewContentUnique(Stock.AsEnumerable(), ref DGV_contentsT);
                        DGV.DataSource = DGV_contentsT;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Ошибка: \n" + exc.Message);
                    }

                    #endregion
                    break;
                default:
                    break;
            }
        }

        private void cb_cb_parameterName_Refill(object sender, EventArgs e)
        {
            cb_parameterName.Items.Clear();
            if (DGV_contentsT != null)
            {
                foreach (DataColumn col in DGV_contentsT.Columns)
                {
                    cb_parameterName.Items.Add(col.Caption);
                }
            }
            if (cb_parameterName.Items.Count > 0)
                cb_parameterName.SelectedIndex = 0;
        }

        void cb_parameterName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Type type = DGV_contentsT.Columns[(int)cb_parameterName.SelectedIndex].DataType;
            }
            catch (InvalidCastException)
            {
                return;
            }
        }

        //################# ENABLE CHECKS #############################

        private void check_panel_Search(object sender, EventArgs e)
        {
            panel_Search.Enabled = DGV.RowCount > 1 || DGV.DataSource != DGV_contentsT;
        }

        private void check_cb_tableOptions(object sender, EventArgs e)
        {
            switch (cb_table.Text.ToString())
            {
                case "Склад":
                    cb_tableOptions.Enabled = false;
                    break;
                case "Характеристики товаров":
                    cb_tableOptions.DataSource = (from cat in database.ItemCategorySet select cat).ToList();
                    cb_tableOptions.DisplayMember = "name";
                    cb_tableOptions.ValueMember = "id";
                    cb_tableOptions.Text = "Выберите категорию";
                    cb_tableOptions.Enabled = true;
                    break;
                default:
                    cb_tableOptions.Text = "Опции недоступны";
                    cb_tableOptions.Enabled = false;
                    break;
            }
        }

        void tb_search_TextChanged(object sender, EventArgs e)
        {
            btn_find.Enabled = tb_search.Text != "";
        }

        private void check_btn_Search(object sender, EventArgs e)
        {
            btn_find.Enabled = DGV.RowCount > 0;
        }

        private void check_criteria(object sender, EventArgs e)
        {
            if (panel_Search.Enabled)
            {
                bool enbl_ML = !(DGV_contentsT.Columns[cb_parameterName.Text].DataType == "".GetType() || (DGV_contentsT.Columns[cb_parameterName.Text].DataType == true.GetType()));
                rb_LE.Enabled = enbl_ML;
                rb_ME.Enabled = enbl_ML;
                bool enbl_Lk = DGV_contentsT.Columns[cb_parameterName.Text].DataType == "".GetType();
                rb_like.Enabled = enbl_Lk;
            }
        }

        //################# CLICK HANDLERS ############################

        private void btn_find_Click(object sender, EventArgs e)
        {
            int selectedIndex = DGV_contentsT.Columns[cb_parameterName.Text].Ordinal;
            if (!rb_like.Checked)
            {
                DGV.DataSource = TableOperator.Where(DGV_contentsT, row => searchPredicate.FirstOrDefault(  //select predicate
            ent => ent.Key.Checked).Value(row[selectedIndex].ToString(),                                //first pred arg
            tb_search.Text));
            }                                                                      //second pred arg
            else
            {
                DGV.DataSource = TableOperator.Like(DGV_contentsT, cb_parameterName.Text, tb_search.Text);
            }
            if (!DGV.DataSource.Equals(DGV_contentsT))
                btn_ClearFilter.Enabled = true;
            else
                btn_ClearFilter.Enabled = false;
        }

        void btn_ClearFilter_Click(object sender, EventArgs e)
        {
            DGV.DataSource = DGV_contentsT;
            tb_search.Text = "";
            btn_ClearFilter.Enabled = false;
        }

        //################# MENU CLICKS ###############################

        private void редактированиеПароляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEditEmplOne emplDlg = new FormEditEmplOne(ref this.curEmpl);
            emplDlg.ShowDialog();
        }

        private void оформитьНовыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (curEmpl.Appointment.accessModifier == (int)accessModifiers.acc_ord || curEmpl.Appointment.accessModifier == (int)accessModifiers.acc_adm)
            {
                FormEditOrder formOrder = new FormEditOrder(ref curEmpl);
                bindCloseEvent(formOrder);
                formOrder.Show();
            }
            else
            {
                MessageBox.Show("Ваш уровень доступа не позволяет совершить это действие", "Ошибка аутентификации", MessageBoxButtons.OK);
            }
        }

        private void оформитьПоступлениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (curEmpl.Appointment.accessModifier == (int)accessModifiers.acc_stock || curEmpl.Appointment.accessModifier == (int)accessModifiers.acc_adm)
            {
                FormIntake formIntake = new FormIntake();
                bindCloseEvent(formIntake);
                formIntake.ShowDialog();
            }
            else
            {
                MessageBox.Show("Ваш уровень доступа не позволяет совершить это действие", "Ошибка аутентификации", MessageBoxButtons.OK);
            }
        }

        private void категорииИХарактеристикиТоваровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (curEmpl.Appointment.accessModifier == (int)accessModifiers.acc_stock || curEmpl.Appointment.accessModifier == (int)accessModifiers.acc_adm)
            {
                FormEditCategories formCat = new FormEditCategories();
                bindCloseEvent(formCat);
                formCat.ShowDialog();
            }
            else
            {
                MessageBox.Show("Ваш уровень доступа не позволяет совершить это действие", "Ошибка аутентификации", MessageBoxButtons.OK);
            }
        }

        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (curEmpl.Appointment.accessModifier == (int)accessModifiers.acc_adm)
            {
                FormNewEmpl newEmpl = new FormNewEmpl();
                newEmpl.ShowDialog();
            }
            else
            {
                MessageBox.Show("Ваш уровень доступа не позволяет совершить это действие", "Ошибка аутентификации", MessageBoxButtons.OK);
            }
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (curEmpl.Appointment.accessModifier == (int)accessModifiers.acc_adm)
            {
                FormEditEmplSet formEmplSet = new FormEditEmplSet(curEmpl);
                bindCloseEvent(formEmplSet);
                formEmplSet.ShowDialog();
            }
            else
            {
                MessageBox.Show("Ваш уровень доступа не позволяет совершить это действие", "Ошибка аутентификации", MessageBoxButtons.OK);
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Logout();
            Login();
        }

        private void генераторОтчетовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormReports reports = new FormReports(curEmpl);
            reports.ShowDialog();
        }

        //################# EXTERNAL EVENTS HANDLERS####################

        private void user_prop_changed(Object sender, EventArgs e)
        {
            this.Text = Settings.AppTitle + " - " + curEmpl.login;
        }

        private void dialogClosed(Object sender, EventArgs e)
        {
            if (cb_table.Text != "Категории товаров")
            {
                cb_table_SelectedIndexChanged(sender, e);
            }
            else
            {
                cb_tableOptions_SelectedIndexChanged(sender, e);
            }
        }

        //################# MENU STRIP HANDLERS ########################

        //##### CMS_STORE

        private void занестиДанныеОПоступленииToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (DGV.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in DGV.SelectedRows)
                {
                    int selID = (int)row.Cells["id"].Value;
                    res.Item selected = (from items in database.ItemSet where items.id == selID select items).FirstOrDefault();
                    FormIntake formIntake = new FormIntake(selected);
                    formIntake.ShowDialog();
                }
            }
            this.cb_table_SelectedIndexChanged(this, null);
        }

        private void редактироватьОписаниеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DGV.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in DGV.SelectedRows)
                {
                    int selID = (int)row.Cells["id"].Value;
                    res.Item selected = (from items in database.ItemSet where items.id == selID select items).FirstOrDefault();
                    FormEditCategories formcats = new FormEditCategories(selected);
                    bindCloseEvent(formcats);
                    formcats.ShowDialog();
                }
                this.cb_table_SelectedIndexChanged(this, null);
            }
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEditCategories formcats = new FormEditCategories();
            bindCloseEvent(formcats);
            formcats.ShowDialog();
            this.cb_table_SelectedIndexChanged(this, null);
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DGV.SelectedRows.Count > 0)
            {
                List<res.Item> markedForRemoval = new List<Item>();
                bool all = true;
                foreach (DataGridViewRow row in DGV.SelectedRows)
                {
                    int selID = (int)row.Cells["id"].Value;
                    res.Item selected = (from items in database.ItemSet where items.id == selID select items).FirstOrDefault();
                    if (selected.Position.Count == 0)
                    {
                        markedForRemoval.Add(selected);
                    }
                    else
                    {
                        all = false;
                    }
                }
                if (!all)
                {
                    MessageBox.Show("Некоторые предметы нельзя удалить, так как они присутствуют в составе активных заказов");
                }
                if (markedForRemoval.Count > 0)
                {
                    #region confirmation
                    var cnf = MessageBox.Show("Вы действительно хотите удалить эти предметы?", "Внимание", MessageBoxButtons.YesNo);
                    if (cnf == DialogResult.No)
                        return;
                    #endregion
                    foreach (var item in markedForRemoval)
                        database.DeleteObject(item);
                    database.SaveChanges();
                }
                this.cb_table_SelectedIndexChanged(this, null);
            }
        }

        //#### CMS_MGR_S

        private void добавитьВЗаказToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DGV.SelectedRows.Count > 0)
            {
                List<Item> itemsToOrder = new List<Item>();
                foreach (DataGridViewRow row in DGV.SelectedRows)
                {
                    int itemID = (int)row.Cells["id"].Value;
                    Item currItem = (from items in database.ItemSet where items.id == itemID select items).Single();
                    itemsToOrder.Add(currItem);
                }
                FormEditOrder order = new FormEditOrder(ref curEmpl, itemsToOrder.ToArray());
                order.ShowDialog();
            }
            cb_tableOptions_SelectedIndexChanged(this, null); //refresh
        }

        //#### CMS_MGR_O

        private void новыйToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormEditOrder order = new FormEditOrder(ref curEmpl);
            order.ShowDialog();
            this.cb_table_SelectedIndexChanged(this, null); //refresh
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DGV.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in DGV.SelectedRows)
                {
                    int selID = (int)row.Cells["Номер"].Value;
                    Order selOrder = (from ord in database.OrderSet where ord.id == selID select ord).Single();
                    FormEditOrder order = new FormEditOrder(ref curEmpl, ref selOrder);
                    order.ShowDialog();
                    this.cb_table_SelectedIndexChanged(this, null);
                }
            }
            this.cb_table_SelectedIndexChanged(this, null);
        }

        private void удалитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (DGV.SelectedRows.Count > 0)
            {
                #region confirmation
                if (MessageBox.Show("Вы действительно хотите удалить выбранные заказы?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
                #endregion
                foreach (DataGridViewRow row in DGV.SelectedRows)
                {
                    int selID = (int)row.Cells["Номер"].Value;
                    Order selOrder = (from ord in database.OrderSet where ord.id == selID select ord).Single();
                    database.DeleteObject(selOrder);
                    this.cb_table_SelectedIndexChanged(this, null);
                }
                database.SaveChanges();
            }
            this.cb_table_SelectedIndexChanged(this, null);
        }

        //##### CMS_ADM_O
        private void новыйЗаказToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEditOrder order = new FormEditOrder(ref curEmpl);
            order.ShowDialog();
            this.cb_table_SelectedIndexChanged(this, null); //refresh
        }

        private void изменитьЗаказToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DGV.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in DGV.SelectedRows)
                {
                    int selID = (int)row.Cells["Номер"].Value;
                    Order selOrder = (from ord in database.OrderSet where ord.id == selID select ord).Single();
                    FormEditOrder order = new FormEditOrder(ref curEmpl, ref selOrder);
                    order.ShowDialog();
                    this.cb_table_SelectedIndexChanged(this, null);
                }
            }
        }

        private void удалитьЗаказToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DGV.SelectedRows.Count > 0)
            {
                #region confirmation
                if (MessageBox.Show("Вы действительно хотите удалить выбранные заказы?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
                #endregion
                foreach (DataGridViewRow row in DGV.SelectedRows)
                {
                    int selID = (int)row.Cells["Номер"].Value;
                    Order selOrder = (from ord in database.OrderSet where ord.id == selID select ord).Single();
                    database.DeleteObject(selOrder);
                }
                database.SaveChanges();
                this.cb_table_SelectedIndexChanged(this, null);
            }
        }

        private void сотрудникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DGV.SelectedRows.Count == 1)
            {
                int selID = (int)DGV.SelectedRows[0].Cells["Номер"].Value;
                Order selOrder = (from ord in database.OrderSet where ord.id == selID select ord).Single();
                FormEditEmplSet formempl = new FormEditEmplSet(selOrder.Employee);
                bindCloseEvent(formempl);
                formempl.ShowDialog();
            }
            this.cb_table_SelectedIndexChanged(this, null);
        }

        //##### CMS_EMPL

        private void новыйToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FormNewEmpl formNewEmpl = new FormNewEmpl();
            bindCloseEvent(formNewEmpl);
            formNewEmpl.ShowDialog();
            this.cb_table_SelectedIndexChanged(this, null);
        }

        private void изменитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (DGV.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in DGV.SelectedRows)
                {
                    int id = (int)row.Cells["Номер"].Value;
                    Employee selEmpl = (from emp in database.EmployeeSet where emp.id == id select emp).Single();
                    FormEditEmplSet formEmpl = new FormEditEmplSet(selEmpl, curEmpl);
                    bindCloseEvent(formEmpl);
                    formEmpl.ShowDialog();
                }
            }
            this.cb_table_SelectedIndexChanged(this, null);
        }

        private void удалитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (DGV.SelectedRows.Count > 0)
            {
                #region confirmation
                if (MessageBox.Show("Вы действительно хотите удалить выбранных сотрудников?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
                #endregion
                foreach (DataGridViewRow row in DGV.SelectedRows)
                {
                    int selID = (int)row.Cells["Номер"].Value;
                    Employee selEmpl = (from emp in database.EmployeeSet where emp.id == selID select emp).Single();
                    if (selEmpl.id == curEmpl.id || selEmpl.login == "admin")
                    {
                        MessageBox.Show("Сотрудник не может удалить себя или администратора");
                    }
                    else
                    {
                        database.DeleteObject(selEmpl);
                    }
                }
                database.SaveChanges();
                this.cb_table_SelectedIndexChanged(this, null);
            }
        }

        //############# DGV HANDLERS ###############
        void DGV_SelectionChanged(object sender, EventArgs e)
        {
            if (DGV.SelectedCells.Count >= 1)
            {
                foreach (DataGridViewCell cell in DGV.SelectedCells)
                {
                    DGV.Rows[cell.RowIndex].Selected = true;
                }
            }

            if (DGV.SelectedColumns.Count >= 1)
            {
                cb_parameterName.SelectedText = DGV.SelectedColumns[0].Name;
            }
        }

        private void bindCloseEvent(Form form)
        {
            form.FormClosed += new FormClosedEventHandler(this.dialogClosed);
        }

    }
}
