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
    public partial class FormMain : Form
    {
        enum accessModifiers { acc_none, acc_stock, acc_ord, acc_adm };

        private res.Model1Container database;
        private res.Employee curEmpl;
        private List<TableWithAccess> availableTables;
        private DataTable DGV_contentsT;
        private Dictionary<RadioButton, Func<string, string, bool>> searchPredicate;

        public FormMain()
        {
            InitializeComponent();
            this.Hide();
            FormLogin formLogin = new FormLogin();
            DialogResult res = formLogin.ShowDialog();
            switch (res)
            {
                case DialogResult.OK:
                    this.Enabled = true;
                    this.curEmpl = formLogin.usr;
                    this.Text = Settings.AppTitle + " - " + curEmpl.login;
                    #region event bindings
                    curEmpl.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.user_prop_changed);

                    cb_table.SelectedIndexChanged += new EventHandler(this.check_cb_tableOptions);
                    cb_table.SelectedIndexChanged += new EventHandler(this.cb_table_SelectedIndexChanged);

                    cb_tableOptions.SelectedIndexChanged += new EventHandler(cb_tableOptions_SelectedIndexChanged);

                    cb_parameterName.SelectedIndexChanged += new EventHandler(cb_parameterName_SelectedIndexChanged);

                    tb_search.TextChanged += new EventHandler(tb_search_TextChanged);

                    DGV.SelectionChanged += new EventHandler(this.check_panel_Search);
                    DGV.DataSourceChanged += new EventHandler(this.cb_cb_parameterName_Refill);
                    DGV.DataSourceChanged += new EventHandler(this.check_btn_Search);

                    btn_find.Click += new EventHandler(btn_find_Click);

                    btn_ClearFilter.Click += new EventHandler(btn_ClearFilter_Click);

                    #endregion

                    database = new res.Model1Container();
                    search_Panel_Initialization();
                    cb_table_Init_And_Fill();

                    break;
                default:
                    MessageBox.Show("Окно авторизации было закрыто", "Ошибка авторизации");
                    break;
            }

        }


        //################# INITIALIZATION ############################

        private void cb_table_Init_And_Fill()
        {
            availableTables = new List<TableWithAccess>();

            availableTables.AddRange(new TableWithAccess[] { 
            new TableWithAccess("Склад", (int)accessModifiers.acc_stock, this.CMS_STORE),
            new TableWithAccess("Характеристики товаров", (int)accessModifiers.acc_ord, this.CMS_MGR_S),
            new TableWithAccess("Заказы сотрудника", (int)accessModifiers.acc_ord, this.CMS_MGR_O),
            new TableWithAccess("Все заказы", (int)accessModifiers.acc_adm, this.CMS_ADM_O),
            new TableWithAccess("Сотрудники", (int)accessModifiers.acc_adm, this.CMS_ADM_EMP)});

            cb_table.DataSource = availableTables.Where(tbl => tbl.accessMod == curEmpl.Appointment.accessModifier || curEmpl.Appointment.accessModifier == (int)accessModifiers.acc_adm).ToList();
            cb_table.ValueMember = "accessMod";
            cb_table.DisplayMember = "name";
        }

        private void search_Panel_Initialization()
        {
            searchPredicate = new Dictionary<RadioButton, Func<string, string, bool>>();
            Func<string, string, bool> ME = delegate(string s1, string s2) { return s1.CompareTo(s2) <= 0; };
            Func<string, string, bool> E = delegate(string s1, string s2) { return s1.CompareTo(s2) == 0; };
            Func<string, string, bool> LE = delegate(string s1, string s2) { return s1.CompareTo(s2) >= 0; };
            searchPredicate.Add(rb_ME, ME);
            searchPredicate.Add(rb_E, E);
            searchPredicate.Add(rb_LE, LE);
        }

        //################# COMBO BOXES ###############################

        private void cb_table_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cb_table.Text.ToString())
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
                                Спрос = item.demand,
                                Остаток = item.storeResidue
                            };
                        TableOperator.SetNewContentCommon(view.ToArray(), ref DGV_contentsT);
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
            panel_Search.Enabled = DGV.RowCount > 1;
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

        //################# CLICK HANDLERS ############################

        private void btn_find_Click(object sender, EventArgs e)
        {
            int selectedIndex = DGV_contentsT.Columns[cb_parameterName.Text].Ordinal;
            DGV.DataSource = TableOperator.Where(ref DGV_contentsT, row => searchPredicate.FirstOrDefault(  //select predicate
                ent => ent.Key.Checked).Value(row[selectedIndex].ToString(),                                //first pred arg
                tb_search.Text));                                                                           //second pred arg
            btn_ClearFilter.Enabled = true;
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
                //FormEditCategories formCat = new FormEditCategories();
                //formCat.ShowDialog();
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
            FormEditEmplSet formEmplSet = new FormEditEmplSet();
            formEmplSet.ShowDialog();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //################# EXTERNAL EVENTS HANDLERS####################

        private void user_prop_changed(Object sender, EventArgs e)
        {
            this.Text = Settings.AppTitle + " - " + curEmpl.login;
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
                    formcats.ShowDialog();
                }
            }
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEditCategories formcats = new FormEditCategories();
            formcats.ShowDialog();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DGV.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in DGV.SelectedRows)
                {
                    int selID = (int)row.Cells["id"].Value;
                    res.Item selected = (from items in database.ItemSet where items.id == selID select items).FirstOrDefault();
                    FormEditCategories formcats = new FormEditCategories(selected);
                    formcats.ShowDialog();
                }
            }
        }

        //#### CMS_MGR_S

        private void добавитьВЗаказToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //#### CMS_MGR_O

        private void новыйToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void удалитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        //##### CMS_ADM_O
        private void новыйЗаказToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void изменитьЗаказToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void удалитьЗаказToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void сотрудникToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //##### CMS_EMPL

        private void новыйToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void изменитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void удалитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

    }
}
