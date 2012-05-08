using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Data.EntityModel;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace SERIOUS_BUSINESS
{
    enum PType { pt_txt = 1, pt_dbl = 2, pt_bool = 3 };
    public partial class FormEditCategories : Form
    {
        #region context & entities
        res.Model1Container database;
        IQueryable<res.ItemCategory> Categories;
        IQueryable<AssociatedPC> Parameters;
        IQueryable<NamedItem> Items;
        IQueryable<NamedParameter> CurItemParameters;
        #endregion
        DataTable TCatParameters, TItemParameters;
        private Dictionary<RadioButton, short> PtypeSelector;

        public FormEditCategories()
        {
            InitializeComponent();
            #region Event Bindings
            this.tb_catName.TextChanged += new System.EventHandler(this.btn_checks);
            this.tb_newParamName.TextChanged += new System.EventHandler(this.btn_checks);
            this.tb_newItemDesignation.TextChanged += new System.EventHandler(this.btn_checks);

            this.cb_cat.SelectedIndexChanged += new System.EventHandler(this.RefillAssociations);
            this.cb_cat.SelectedIndexChanged += new System.EventHandler(this.RefillItems);
            this.cb_cat.SelectedIndexChanged += new System.EventHandler(this.RefillItemParameters);

            this.cb_existingItem.SelectedIndexChanged += new System.EventHandler(this.RefillItemParameters);

            this.btn_accParams.Click += new EventHandler(btn_accParams_Click);
            this.btn_accParams.Click += new System.EventHandler(this.RefillItemParameters);

            this.btn_addPar.Click += new System.EventHandler(this.RefillAssociations);

            this.btn_addCat.Click += new System.EventHandler(this.btn_addCat_Click);
            this.btn_addCat.Click += new System.EventHandler(this.RefillCategories);

            this.btn_addItem.Click += new System.EventHandler(this.btn_addItem_Click);
            this.btn_addItem.Click += new System.EventHandler(this.RefillItems);

            this.btn_accItem.Click += new System.EventHandler(this.btn_accItem_Click);

            this.DGV_catParameters.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.btn_checks);
            this.DGV_itemParameters.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.btn_checks);

            #endregion

            #region context & entities
            database = new res.Model1Container();
            #endregion
            InitTables();
            FillPTypeSelector();
            RefillCategories(null, null);
        }

        public FormEditCategories(res.Item _preselItem) : this()
        {
            cb_cat.SelectedValue = _preselItem.catID;
            RefillItems(null, null);
            cb_existingItem.SelectedValue = _preselItem.id;
        }

        private void InitTables()
        {
            TCatParameters = new DataTable();
            TItemParameters = new DataTable();

            TCatParameters.Columns.AddRange(new DataColumn[] { new DataColumn("Название", "".GetType()), new DataColumn("Ассоциация", true.GetType()), new DataColumn("id", 1.GetType()) });
            TItemParameters.Columns.AddRange(new DataColumn[] { new DataColumn("Параметр", "".GetType()), new DataColumn("Значение", "".GetType()), new DataColumn("id", 1.GetType()), new DataColumn("type", System.Type.GetType("System.Int16")) });

            TItemParameters.PrimaryKey = new DataColumn[] { TItemParameters.Columns["Параметр"] };

            DGV_catParameters.DataSource = TCatParameters;
            DGV_itemParameters.DataSource = TItemParameters;
        }

        private void RefillCategories(object sender, EventArgs e)
        {
            Categories = from cat in database.ItemCategorySet select cat;
            cb_cat.DataSource = Categories.OrderBy(x => x.name).ToArray();
            cb_cat.DisplayMember = "name";
            cb_cat.ValueMember = "id";
        }

        private void RefillAssociations(object sender, EventArgs e)
        {
            int catID = 0;
            try
            {
                catID = (int)cb_cat.SelectedValue;
            }
            catch (InvalidCastException)
            {
                return;
            }
            TCatParameters.Clear();
            Parameters =
    from PC in database.ParameterCategorySet
    select new AssociatedPC
    {
        id = PC.id,
        name = PC.name,
        type = PC.type,
        associated = (from ICPC in database.pureJoin_IPcatsSet where ICPC.PCID == PC.id && ICPC.ICID == catID select ICPC).Any()
    };
            foreach (var par in Parameters.AsEnumerable())
            {
                DataRow nrow = TCatParameters.NewRow();
                nrow[0] = par.name;
                nrow[1] = par.associated;
                nrow[2] = par.id;
                TCatParameters.Rows.Add(nrow);
            }

            DGV_SetConstraints(ref DGV_catParameters, new DataGridViewRow[] { DGV_catParameters.Rows[0] }, new DataGridViewColumn[] { DGV_catParameters.Columns["Название"] });
            DGV_SetMask(ref DGV_catParameters);
        }

        private void FillPTypeSelector()
        {
            PtypeSelector = new Dictionary<RadioButton, short>();
            PtypeSelector.Add(rb_text, (short)PType.pt_txt);
            PtypeSelector.Add(rb_numeric, (short)PType.pt_dbl);
            PtypeSelector.Add(rb_binary, (short)PType.pt_bool);
        }

        private void RefillItems(object sender, EventArgs e)
        {
            cb_existingItem.Text = "";
            int catID = -1;
            try
            {
                catID = (int)cb_cat.SelectedValue;
            }
            catch (InvalidCastException)
            {
                return;
            }
            Items = from item in database.ItemSet
                    join param in database.ItemParameterSet
                    on item.id equals param.itemID
                    where param.ParameterCategory.name == "Наименование" && item.catID == catID
                    select new NamedItem
                    {
                        name = param.valueTxt,
                        id = item.id
                    };
            cb_existingItem.DataSource = Items.OrderBy(x => x.name).ToArray();
            cb_existingItem.DisplayMember = "name";
            cb_existingItem.ValueMember = "id";
        }

        private void RefillItemParameters(object sender, EventArgs e)
        {
            int itemID = 0;
            try
            {
                itemID = (int)cb_existingItem.SelectedValue;
            }
            catch (InvalidCastException)
            {
                TItemParameters.Clear();
                return;
            }
            catch (NullReferenceException)
            {
                return;
            }
            TItemParameters.Clear();
            CurItemParameters = from par in database.ItemParameterSet
                                join PC in database.ParameterCategorySet
                                    on par.paramCatID equals PC.id
                                where par.itemID == itemID
                                select new NamedParameter
                                {
                                    id = par.id,
                                    name = PC.name,
                                    type = PC.type,
                                    valueBool = par.valueBool,
                                    valueDbl = par.valueDbl,
                                    valueTxt = par.valueTxt
                                };
            CurItemParameters.ToList().ForEach(par =>
                {
                    DataRow nrow = TItemParameters.NewRow();
                    nrow["Параметр"] = par.name;
                    nrow["Значение"] = par.GetValue(par.type);
                    nrow["id"] = par.id;
                    nrow["type"] = par.type;
                    TItemParameters.Rows.Add(nrow);
                });

            DGV_SetConstraints(ref DGV_itemParameters, new DataGridViewRow[] { }, new DataGridViewColumn[] { DGV_itemParameters.Columns[0] });
            DGV_SetMask(ref DGV_itemParameters);
        }

        private void btn_checks(object sender, EventArgs e)
        {
            btn_addCat.Enabled = tb_catName.Text != "";
            btn_addPar.Enabled = tb_newParamName.Text != "" && (rb_binary.Checked || rb_numeric.Checked || rb_text.Checked);
            btn_addItem.Enabled = tb_newItemDesignation.Text != "";
            btn_accParams.Enabled = DGV_catParameters.RowCount > 0;
            btn_accItem.Enabled = DGV_itemParameters.RowCount > 0;
        }

        private bool isDesignationEmpty(DataTable tbl)
        {
            DataRow designation_row = tbl.AsEnumerable().AsQueryable().First<DataRow>(row => row.Field<string>("Параметр") == "Наименование");
            return designation_row.Field<string>("Значение").Length == 0;
        }

        private void DGV_SetMask(ref DataGridView DGV)
        {
            System.Text.RegularExpressions.Regex regexp = new System.Text.RegularExpressions.Regex("^[A-Z, a-z]+$");
            int cnt = DGV.Columns.Count, i = 0;
            while (i < cnt)
            {
                if (regexp.IsMatch(DGV.Columns[i].Name)) DGV.Columns[i].Visible = false;
                i++;
            }
        }

        private void DGV_SetConstraints(ref DataGridView DGV, DataGridViewRow[] rows, DataGridViewColumn[] cols)
        {
            foreach (DataGridViewRow row in rows)
            {
                DGV.Rows[row.Index].ReadOnly = true;
            }
            foreach (DataGridViewColumn col in cols)
            {
                DGV.Columns[col.Index].ReadOnly = true;
            }
        }

        private void btn_addCat_Click(object sender, EventArgs e)
        {
            #region check for same name
            if ((from cat in database.ItemCategorySet where cat.name == tb_catName.Text select cat).Any())
            {
                MessageBox.Show("Такая категория уже присутствует");
                return;
            }
            #endregion
            #region confirmation
            if (MessageBox.Show(string.Format("Вы уверены, что хотите добавить категорию {0}", tb_catName.Text)) == DialogResult.No) return;
            #endregion
            #region addition
            try
            {
                res.ItemCategory newCat = res.ItemCategory.CreateItemCategory(tb_catName.Text, -1);
                res.pureJoin_IPcats newAssoc = res.pureJoin_IPcats.CreatepureJoin_IPcats(0, 0, 0);
                newAssoc.ParameterCategory = (from param in database.ParameterCategorySet where param.name == "Наименование" select param).Single();
                newAssoc.ItemCategory = newCat;
                database.AddTopureJoin_IPcatsSet(newAssoc);
                database.SaveChanges();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ошибка при создании новой категории: \n" + exc.Message);
            }
            #endregion
        }

        private void btn_addPar_Click(object sender, EventArgs e)
        {
            #region check for same name
            if ((from param in database.ParameterCategorySet where param.name == tb_newParamName.Text select param).Any())
            {
                MessageBox.Show("Такой параметр уже существует");
                return;
            }
            #endregion
            #region confirmation
            if (MessageBox.Show(string.Format("Вы уверены, что хотите добавить параметр {0}?", tb_newParamName.Text)) == DialogResult.No) return;
            #endregion
            #region param addition
            try
            {
                res.ParameterCategory newPC = res.ParameterCategory.CreateParameterCategory(0, tb_newParamName.Text, PtypeSelector.Single(ent => ent.Key.Checked).Value);
                database.AddToParameterCategorySet(newPC);
                database.SaveChanges();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ошибка при добавлении нового параметра:" + exc.Message);
                return;
            }
            #endregion
        }

        private void btn_accParams_Click(object sender, EventArgs e)
        {
            #region confirmation
            if (MessageBox.Show(string.Format("Вы уверены, что хотите применить параметры к категории?")) == DialogResult.No) return;
            #endregion

            #region accepting
            try
            {
                foreach (DataRow row in TCatParameters.GetChanges().Rows)
                {
                    string name = (string)row["Название"];
                    bool new_val = (bool)row["Ассоциация"];
                    int IC = (int)cb_cat.SelectedValue;
                    int PC = (int)row[2];
                    if (Parameters.Single<AssociatedPC>(ap => ap.name == name).associated != new_val)
                    {

                        if (new_val)
                        {
                            res.pureJoin_IPcats assoc = res.pureJoin_IPcats.CreatepureJoin_IPcats(IC, PC, -1);
                            List<res.ItemParameter> newParams = new List<res.ItemParameter>();
                            foreach (var item in Categories.Single(cat => cat.id == IC).Item)
                            {
                                res.ItemParameter newParam = res.ItemParameter.CreateItemParameter(0, 0, 0);
                                newParam.paramCatID = PC;
                                item.ItemParameter.Add(newParam);
                                newParams.Add(newParam);
                            }

                            database.AddTopureJoin_IPcatsSet(assoc);
                            database.SaveChanges();
                        }
                        else
                        {
                            foreach (var item in Categories.Single(cat => cat.id == IC).Item)
                            {
                                database.DeleteObject(item.ItemParameter.Single(param => param.paramCatID == PC));
                            }

                            database.DeleteObject((from ICPC in database.pureJoin_IPcatsSet where ICPC.ICID == IC && ICPC.PCID == PC select ICPC).Single());
                            database.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Произошла ошибка во время ассоциирования параметров к категории\n" + exc.Message);
                return;
            }
            RefillAssociations(null, null);
            #endregion
        }

        private void btn_addItem_Click(object sender, EventArgs e)
        {
            #region check for same name
            var same_des = from param in database.ItemParameterSet where param.ParameterCategory.name == "Наименование" && param.valueTxt == tb_newItemDesignation.Text select param;
            if ((same_des).Any())
            {
                string repetitions = "";
                same_des.ToList().ForEach(des => repetitions += des.Item.ItemCategory.name + "\n");
                DialogResult dlgres = MessageBox.Show("Такие предметы уже есть в категориях \n" + repetitions + "\nВсе равно переименовать?", "Внимание", MessageBoxButtons.YesNo);
                if (dlgres == DialogResult.No)
                {
                    return;
                }
            }
            #endregion
            #region confirmation
            if (MessageBox.Show(string.Format("Вы уверены, что хотите добавить предмет {0} в категорию {1}", tb_newItemDesignation.Text, tb_catName.Text)) == DialogResult.No) return;
            #endregion
            #region addition
            int catID = (int)cb_cat.SelectedValue;
            res.Item newItem = res.Item.CreateItem(0, 0, 0, catID);
            List<res.ItemParameter> newItemParameters = new List<res.ItemParameter>();
            foreach (var assoc in Categories.Single(cat => cat.id == catID).pureJoin_IPcats)
            {
                res.ItemParameter newParam = res.ItemParameter.CreateItemParameter(0, 0, 0);
                newParam.ParameterCategory = assoc.ParameterCategory;
                if (assoc.ParameterCategory.name == "Наименование") newParam.valueTxt = tb_newItemDesignation.Text;
                newParam.Item = newItem;
                newItemParameters.Add(newParam);
            }
            database.AddToItemSet(newItem);
            database.SaveChanges();
            #endregion
        }

        private void btn_accItem_Click(object sender, EventArgs e)
        {
            #region check for filled designation
            if (String.IsNullOrEmpty(TItemParameters.Select("Параметр = 'Наименование'", "").Single().Field<string>("Значение")))
            {
                MessageBox.Show("Попытка записать пустое наименование, отмена.");
                return;
            }
            #endregion
            #region check for same designation
            var same_des = from param in database.ItemParameterSet where param.ParameterCategory.name == "Наименование" && param.valueTxt == tb_newItemDesignation.Text select param;
            if ((same_des).Any())
            {
                string repetitions = "";
                same_des.ToList().ForEach(des => repetitions += des.Item.ItemCategory.name + "\n");
                DialogResult dlgres = MessageBox.Show("Такие предметы уже есть в категориях /n" + repetitions + "\nВсе равно переименовать?", "Внимание", MessageBoxButtons.YesNo);
                if (dlgres == DialogResult.No)
                {
                    return;
                }
            }
            #endregion
            #region confirmation
            if (MessageBox.Show(string.Format("Вы уверены, что хотите изменить характеристики предмета {0}?", cb_existingItem.Text)) == DialogResult.No) return;
            #endregion
            #region accepting
            int itemID = 0;
            try
            {
                itemID = (int)cb_existingItem.SelectedValue;
            }
            catch (InvalidCastException)
            {
                return;
            }

            res.Item curItem = (from itm in database.ItemSet where itm.id == itemID select itm).Single();
            foreach (DataRow row in TItemParameters.Rows)
            {
                int pid = (int)row["id"];
                short type = (short)row["type"];
                var curParam = curItem.ItemParameter.Single(par => par.id == pid);
                try
                {
                    curParam.valueBool = NamedParameter.GetTypedBValue(row["Значение"].ToString(), type);
                    curParam.valueDbl = NamedParameter.GetTypedDValue(row["Значение"].ToString(), type);
                    curParam.valueTxt = NamedParameter.GetTypedSValue(row["Значение"].ToString(), type);
                }
                catch (FormatException exc)
                {
                    MessageBox.Show(exc.Message);
                    return;
                }
            }
            database.SaveChanges();
            this.RefillItems(null, null);
            #endregion
        }


    }
    private class NamedItem
    {
        public int id
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }
        public NamedItem() { }
    }
    private class AssociatedPC
    {
        public int id { get; set; }
        public string name { get; set; }
        public short type { get; set; }
        public bool associated { get; set; }
        public AssociatedPC() { }
    }
    private class NamedParameter
    {
        public int id { get; set; }
        public short type { get; set; }
        public string name { get; set; }
        public string valueTxt { get; set; }
        public double? valueDbl { get; set; }
        public bool? valueBool { get; set; }

        public NamedParameter() { }

        public string GetValue(short _type)
        {
            switch (_type)
            {
                case ((short)PType.pt_txt):
                    return valueTxt;
                case ((short)PType.pt_dbl):
                    return valueDbl.ToString();
                case ((short)PType.pt_bool):
                    return valueBool.ToString();
                default:
                    return null;
            }
        }

        static public bool? GetTypedBValue(string _value, short _type)
        {
            if (_type == (short)PType.pt_bool)

                try
                {
                    return bool.Parse(_value);
                }
                catch (FormatException)
                {
                    throw new FormatException("Не удалось распознать значение " + _value + "\nПроверьте правильность ввода:\n Входная строка должна иметь формат {True, False}");
                }

            else return null;
        }

        static public double? GetTypedDValue(string _value, short _type)
        {
            if (_type == (short)PType.pt_dbl)

                try
                {
                    return double.Parse(_value);
                }
                catch (FormatException)
                {
                    throw new FormatException("Не удалось распознать значение " + _value + "\nПроверьте правильность ввода:\n Входная строка должна иметь формат '1234,1234'");
                }

            else return null;

        }

        static public string GetTypedSValue(string _value, short _type)
        {
            if (_type == (short)PType.pt_txt)

                return _value;
            else return null;

        }

    }
}
