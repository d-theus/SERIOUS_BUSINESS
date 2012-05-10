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
    public partial class FormIntake : Form
    {
        private res.Model1Container database;
        private IQueryable<res.ItemCategory> Categories;
        private IQueryable<res.Item> CurrentCatItems;
        private IQueryable<res.ItemParameter> CurrentCatDesignations;

        public FormIntake()
        {
            InitializeComponent();
            database = new res.Model1Container(RegistryInteractor.GetFromReg("Connection String"));
            database.Connection.Open();
            cb_type.IsAccessible = false;
            cb_designation.IsAccessible = false;
            btn_accept.Enabled = false;

            FillCategories();
        }

        public FormIntake(res.Item _preselectedItem)
        {
            InitializeComponent();
            database = new res.Model1Container(RegistryInteractor.GetFromReg("Connection String"));
            database.Connection.Open();
            cb_type.IsAccessible = false;
            cb_designation.IsAccessible = false;
            btn_accept.Enabled = false;

            FillCategories();

            cb_type.SelectedValue = _preselectedItem.catID;
            RefillItems();
            cb_designation.SelectedValue = _preselectedItem.id;
        }

        private void FillCategories()
        {
            Categories = from cats in database.ItemCategorySet select cats;
            cb_type.DataSource = Categories.ToArray<res.ItemCategory>();
            cb_type.DisplayMember = "name";
            cb_type.ValueMember = "id";
            cb_type.IsAccessible = true;
        }
        private void RefillItems()
        {
            if (!cb_type.Text.Equals("") && cb_type.IsAccessible)
            {
                Int32 selected_cat_id = int.Parse(cb_type.SelectedValue.ToString());
                CurrentCatItems = from items in database.ItemSet where items.catID == selected_cat_id select items;

                int designation_parameter_id = (from PC in database.ParameterCategorySet where PC.name.Equals("Наименование") select PC.id).Sum();
                CurrentCatDesignations = from param in database.ItemParameterSet
                                             join item in database.ItemSet on param.itemID equals item.id
                                             where item.catID == selected_cat_id && param.ParameterCategory.name.Equals("Наименование")
                                             select param;
                cb_designation.DataSource = CurrentCatDesignations;
                cb_designation.DisplayMember = "valueTxt";
                cb_designation.ValueMember = "itemID";
                cb_designation.IsAccessible = true;
            }
        }

        private void cb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefillItems();
        }

        private void num_count_ValueChanged(object sender, EventArgs e)
        {
            if (num_count.Value == 0)
                btn_accept.Enabled = false;
            else
                btn_accept.Enabled = true;
        }

        private void btn_accept_Click(object sender, EventArgs e)
        {
            if (((string)cb_designation.Text).Length == 0)
            {
                MessageBox.Show("Выберите товар из списка", "Ошибка");
                return;
            }
            DialogResult appmnt = MessageBox.Show(string.Format("Действительно занести данные о поступлении {0} единиц товара '{1}'", (Int32)num_count.Value, (string)cb_designation.Text), "Внимание", MessageBoxButtons.YesNo);
            if (appmnt == DialogResult.No) return;
            int selected_id = -1;
            selected_id = int.Parse(cb_designation.SelectedValue.ToString());
            CurrentCatItems.Single<res.Item>(i => i.id == selected_id).storeResidue += (Int32)num_count.Value;
            try_save_changes:
            try
            {
                database.SaveChanges();
            }
            catch (Exception exc)
            {
                DialogResult reslt = MessageBox.Show(exc.Message, "Ошибка", MessageBoxButtons.RetryCancel);
                switch (reslt)
                {
                    case DialogResult.Cancel:
                        return;
                    case DialogResult.Retry:
                        goto try_save_changes;
                }
            }
        }

        private void FormIntake_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
