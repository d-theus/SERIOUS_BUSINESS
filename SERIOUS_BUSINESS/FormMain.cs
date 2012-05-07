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
        private Dictionary<RadioButton, int> searchPredicate;

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
                    cb_tableOptions.SelectedIndexChanged += new EventHandler(cb_tableOptions_SelectedIndexChanged);
                    #endregion

                    #region DB
                    database = new res.Model1Container();
                    #endregion

                    #region Search organization
                    searchPredicate = new Dictionary<RadioButton, int>();
                    searchPredicate.Add(rb_ME, 1);
                    searchPredicate.Add(rb_E, 2);
                    searchPredicate.Add(rb_LE, 3);
                    #endregion
                    #region Available tables list
                    cb_table_Init_And_Fill();
                    #endregion

                    break;
                default:
                    MessageBox.Show("Окно авторизации было закрыто", "Ошибка авторизации");
                    break;
            }

        }

        void cb_tableOptions_SelectedIndexChanged(object sender, EventArgs e)
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
                        IQueryable<StockForManager> StockDB = from item in database.ItemSet where item.catID == catID
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
                        if (DGV_contentsT != null)
                            DGV_contentsT.Dispose();
                        DGV_contentsT = new DataTable();
                        if (Stock.Any())
                        {
                            Stock.First().Parameters.OrderBy(par => par.id).ToList().ForEach(par =>
                            {
                                DGV_contentsT.Columns.Add(new DataColumn(par.name, "".GetType()));
                            });
                            DGV_contentsT.Columns.Add(new DataColumn("Остаток", "".GetType()));
                            foreach (var item in Stock)
                            {
                                DataRow nrow = DGV_contentsT.NewRow();
                                int i = 0;
                                item.Parameters.ForEach(par =>
                                {
                                    nrow[i] = par.GetValue();
                                    i++;
                                });
                                nrow[i] = item.stockResidue;
                                DGV_contentsT.Rows.Add(nrow);
                            }
                            DGV.DataSource = DGV_contentsT;
                        }
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Ошибка: \n" + exc.Message);
                        throw;
                    }

                    #endregion
                    break;
                default:
                    break;
            }
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //MENU
        #region Account settings
        private void редактированиеПароляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEditEmplOne emplDlg = new FormEditEmplOne(ref this.curEmpl);
            emplDlg.ShowDialog();
        }
        #endregion
        #region Orders
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
        #endregion
        #region Store
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
                //FormEditCategories formCat = new FormEditCategories(this.dbConnection);
                //formCat.ShowDialog();
            }
            else
            {
                MessageBox.Show("Ваш уровень доступа не позволяет совершить это действие", "Ошибка аутентификации", MessageBoxButtons.OK);
            }
        }
        #endregion
        #region Employees
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
        #endregion
        private void user_prop_changed(Object sender, EventArgs e)
        {
            this.Text = Settings.AppTitle + " - " + curEmpl.login;
        }

        private void DGV_SelectionChanged(object sender, EventArgs e)
        {
            bool any_rows_selected = DGV.SelectedRows.Count > 0;
            panel_edit.Enabled = any_rows_selected;
        }

        private void cb_table_Init_And_Fill()
        {
            availableTables = new List<TableWithAccess>();
            availableTables.AddRange(new TableWithAccess[] { 
            new TableWithAccess("Склад", (int)accessModifiers.acc_stock),
            new TableWithAccess("Характеристики товаров", (int)accessModifiers.acc_ord),
            new TableWithAccess("Заказы сотрудника", (int)accessModifiers.acc_ord),
            new TableWithAccess("Все заказы", (int)accessModifiers.acc_adm)});

            cb_table.DataSource = availableTables.Where(tbl => tbl.accessMod == curEmpl.Appointment.accessModifier || curEmpl.Appointment.accessModifier == (int)accessModifiers.acc_adm).ToList();
            cb_table.ValueMember = "accessMod";
            cb_table.DisplayMember = "name";
        }

        private void btn_find_Click(object sender, EventArgs e)
        {

        }

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
                                Категория = item.ItemCategory.name,
                                Наименование = item.ItemParameter.FirstOrDefault(par => par.ParameterCategory.name == "Наименование").valueTxt,
                                Спрос = item.demand,
                                Остаток = item.storeResidue
                            };
                        DGV.DataSource = view.ToArray();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Ошибка" + exc.Message);
                        return;
                    }
                    #endregion
                    break;
                case "Характеристики товаров":

                    break;
                case "Заказы сотрудника":
                    #region Manager orders
                    try
                    {
                        var Orders = from ord in database.OrderSet
                                     where ord.emplID == curEmpl.id
                                     select new OrdersForManager
                                     {
                                         Дата =ord.date,
                                         Номер = ord.id,
                                         Заказчик = ord.Consumer.name,
                                         Статус = ord.status
                                     };
                        DGV.DataSource = Orders.AsQueryable().ToList();
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
                        DGV.DataSource = Orders.AsQueryable().ToList();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Ошибка: \n"+exc.Message);
                        return;
                    }
                    #endregion
                    break;
            }
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
    }
}
