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
        enum accessModifiers {acc_none, acc_stock, acc_ord, acc_adm};
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

            cb_table.DataSource = availableTables;
            cb_table.ValueMember = "accessMod";
            cb_table.DisplayMember = "name";
        }

        private void btn_find_Click(object sender, EventArgs e)
        {

        }

        private void cb_table_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((int)cb_table.SelectedValue == curEmpl.Appointment.accessModifier || curEmpl.Appointment.accessModifier == (int)accessModifiers.acc_adm)
                {
                    DGV_contentsT = new DataTable();
                    IQueryable<res.Stock_for_Stock> view = (from vrow in database.Stock_for_Stock select vrow);
                    DGV.DataSource = view.ToArray();
                }
            }
            catch (InvalidCastException)
            {
                return;
            }
        }
    }
    class TableWithAccess
    {
        public string name
        {
            get;
            set;
        }
        public int accessMod
        {
            get;
            set;
        }
        public TableWithAccess(string _name, int _AM)
        {
            this.name = _name;
            this.accessMod = _AM;
        }
    }
}
