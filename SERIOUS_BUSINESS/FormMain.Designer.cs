namespace SERIOUS_BUSINESS
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.инструментыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.генераторОтчетовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оформитьНовыйЗаказToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оформитьНовыйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.показатьТаблицуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.занестиДанныеОПоступленииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оформитьПоступлениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.категорииИХарактеристикиТоваровToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.показатьТаблицуToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.сотрудникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новыйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редактироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.показатьТаблицуToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.пользовательToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редактированиеПароляToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.DGV = new System.Windows.Forms.DataGridView();
            this.ms_dgvMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.редактироватьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.дОбавитьЗаписьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьЗапистьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_search = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_LE = new System.Windows.Forms.RadioButton();
            this.rb_E = new System.Windows.Forms.RadioButton();
            this.rb_ME = new System.Windows.Forms.RadioButton();
            this.cb_parameterName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_search = new System.Windows.Forms.TextBox();
            this.btn_find = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cb_table = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel_edit = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_edit = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.ms_dgvMS.SuspendLayout();
            this.panel_search.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel_edit.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.инструментыToolStripMenuItem,
            this.toolStripMenuItem1,
            this.пользовательToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(792, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // инструментыToolStripMenuItem
            // 
            this.инструментыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.генераторОтчетовToolStripMenuItem,
            this.оформитьНовыйЗаказToolStripMenuItem,
            this.занестиДанныеОПоступленииToolStripMenuItem,
            this.сотрудникиToolStripMenuItem});
            this.инструментыToolStripMenuItem.Name = "инструментыToolStripMenuItem";
            this.инструментыToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.инструментыToolStripMenuItem.Text = "Инструменты";
            // 
            // генераторОтчетовToolStripMenuItem
            // 
            this.генераторОтчетовToolStripMenuItem.Name = "генераторОтчетовToolStripMenuItem";
            this.генераторОтчетовToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.генераторОтчетовToolStripMenuItem.Text = "Генератор отчетов";
            // 
            // оформитьНовыйЗаказToolStripMenuItem
            // 
            this.оформитьНовыйЗаказToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оформитьНовыйToolStripMenuItem,
            this.показатьТаблицуToolStripMenuItem});
            this.оформитьНовыйЗаказToolStripMenuItem.Name = "оформитьНовыйЗаказToolStripMenuItem";
            this.оформитьНовыйЗаказToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.оформитьНовыйЗаказToolStripMenuItem.Text = "Заказы";
            // 
            // оформитьНовыйToolStripMenuItem
            // 
            this.оформитьНовыйToolStripMenuItem.Name = "оформитьНовыйToolStripMenuItem";
            this.оформитьНовыйToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.оформитьНовыйToolStripMenuItem.Text = "Оформить новый";
            this.оформитьНовыйToolStripMenuItem.Click += new System.EventHandler(this.оформитьНовыйToolStripMenuItem_Click);
            // 
            // показатьТаблицуToolStripMenuItem
            // 
            this.показатьТаблицуToolStripMenuItem.Name = "показатьТаблицуToolStripMenuItem";
            this.показатьТаблицуToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.показатьТаблицуToolStripMenuItem.Text = "Показать таблицу";
            // 
            // занестиДанныеОПоступленииToolStripMenuItem
            // 
            this.занестиДанныеОПоступленииToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оформитьПоступлениеToolStripMenuItem,
            this.категорииИХарактеристикиТоваровToolStripMenuItem,
            this.показатьТаблицуToolStripMenuItem1});
            this.занестиДанныеОПоступленииToolStripMenuItem.Name = "занестиДанныеОПоступленииToolStripMenuItem";
            this.занестиДанныеОПоступленииToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.занестиДанныеОПоступленииToolStripMenuItem.Text = "Склад";
            // 
            // оформитьПоступлениеToolStripMenuItem
            // 
            this.оформитьПоступлениеToolStripMenuItem.Name = "оформитьПоступлениеToolStripMenuItem";
            this.оформитьПоступлениеToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.оформитьПоступлениеToolStripMenuItem.Text = "Занести данные о поступлении товаров";
            this.оформитьПоступлениеToolStripMenuItem.Click += new System.EventHandler(this.оформитьПоступлениеToolStripMenuItem_Click);
            // 
            // категорииИХарактеристикиТоваровToolStripMenuItem
            // 
            this.категорииИХарактеристикиТоваровToolStripMenuItem.Name = "категорииИХарактеристикиТоваровToolStripMenuItem";
            this.категорииИХарактеристикиТоваровToolStripMenuItem.Size = new System.Drawing.Size(312, 22);
            this.категорииИХарактеристикиТоваровToolStripMenuItem.Text = "Категории и характеристики товаров\\услуг";
            this.категорииИХарактеристикиТоваровToolStripMenuItem.Click += new System.EventHandler(this.категорииИХарактеристикиТоваровToolStripMenuItem_Click);
            // 
            // показатьТаблицуToolStripMenuItem1
            // 
            this.показатьТаблицуToolStripMenuItem1.Name = "показатьТаблицуToolStripMenuItem1";
            this.показатьТаблицуToolStripMenuItem1.Size = new System.Drawing.Size(312, 22);
            this.показатьТаблицуToolStripMenuItem1.Text = "Показать таблицу";
            // 
            // сотрудникиToolStripMenuItem
            // 
            this.сотрудникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новыйToolStripMenuItem,
            this.редактироватьToolStripMenuItem,
            this.показатьТаблицуToolStripMenuItem2});
            this.сотрудникиToolStripMenuItem.Name = "сотрудникиToolStripMenuItem";
            this.сотрудникиToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.сотрудникиToolStripMenuItem.Text = "Сотрудники";
            // 
            // новыйToolStripMenuItem
            // 
            this.новыйToolStripMenuItem.Name = "новыйToolStripMenuItem";
            this.новыйToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.новыйToolStripMenuItem.Text = "Новый";
            this.новыйToolStripMenuItem.Click += new System.EventHandler(this.новыйToolStripMenuItem_Click);
            // 
            // редактироватьToolStripMenuItem
            // 
            this.редактироватьToolStripMenuItem.Name = "редактироватьToolStripMenuItem";
            this.редактироватьToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.редактироватьToolStripMenuItem.Text = "Редактировать";
            this.редактироватьToolStripMenuItem.Click += new System.EventHandler(this.редактироватьToolStripMenuItem_Click);
            // 
            // показатьТаблицуToolStripMenuItem2
            // 
            this.показатьТаблицуToolStripMenuItem2.Name = "показатьТаблицуToolStripMenuItem2";
            this.показатьТаблицуToolStripMenuItem2.Size = new System.Drawing.Size(172, 22);
            this.показатьТаблицуToolStripMenuItem2.Text = "Показать таблицу";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 20);
            // 
            // пользовательToolStripMenuItem
            // 
            this.пользовательToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.редактированиеПароляToolStripMenuItem,
            this.выходToolStripMenuItem1});
            this.пользовательToolStripMenuItem.Name = "пользовательToolStripMenuItem";
            this.пользовательToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.пользовательToolStripMenuItem.Text = "Пользователь";
            // 
            // редактированиеПароляToolStripMenuItem
            // 
            this.редактированиеПароляToolStripMenuItem.Name = "редактированиеПароляToolStripMenuItem";
            this.редактированиеПароляToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.редактированиеПароляToolStripMenuItem.Text = "Редактирование данных";
            this.редактированиеПароляToolStripMenuItem.Click += new System.EventHandler(this.редактированиеПароляToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem1
            // 
            this.выходToolStripMenuItem1.Name = "выходToolStripMenuItem1";
            this.выходToolStripMenuItem1.Size = new System.Drawing.Size(206, 22);
            this.выходToolStripMenuItem1.Text = "Выход";
            // 
            // DGV
            // 
            this.DGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV.ContextMenuStrip = this.ms_dgvMS;
            this.DGV.Location = new System.Drawing.Point(10, 10);
            this.DGV.Margin = new System.Windows.Forms.Padding(10);
            this.DGV.Name = "DGV";
            this.DGV.Size = new System.Drawing.Size(518, 383);
            this.DGV.TabIndex = 1;
            this.DGV.SelectionChanged += new System.EventHandler(this.DGV_SelectionChanged);
            // 
            // ms_dgvMS
            // 
            this.ms_dgvMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.редактироватьToolStripMenuItem1,
            this.дОбавитьЗаписьToolStripMenuItem,
            this.удалитьЗапистьToolStripMenuItem});
            this.ms_dgvMS.Name = "ms_dgvMS";
            this.ms_dgvMS.Size = new System.Drawing.Size(167, 70);
            // 
            // редактироватьToolStripMenuItem1
            // 
            this.редактироватьToolStripMenuItem1.Name = "редактироватьToolStripMenuItem1";
            this.редактироватьToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.редактироватьToolStripMenuItem1.Size = new System.Drawing.Size(166, 22);
            this.редактироватьToolStripMenuItem1.Text = "Редактировать";
            // 
            // дОбавитьЗаписьToolStripMenuItem
            // 
            this.дОбавитьЗаписьToolStripMenuItem.Name = "дОбавитьЗаписьToolStripMenuItem";
            this.дОбавитьЗаписьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)));
            this.дОбавитьЗаписьToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.дОбавитьЗаписьToolStripMenuItem.Text = "Добавить запись";
            // 
            // удалитьЗапистьToolStripMenuItem
            // 
            this.удалитьЗапистьToolStripMenuItem.Name = "удалитьЗапистьToolStripMenuItem";
            this.удалитьЗапистьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
            this.удалитьЗапистьToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.удалитьЗапистьToolStripMenuItem.Text = "Удалить запись";
            // 
            // panel_search
            // 
            this.panel_search.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel_search.BackColor = System.Drawing.SystemColors.Control;
            this.panel_search.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_search.Controls.Add(this.groupBox1);
            this.panel_search.Controls.Add(this.cb_parameterName);
            this.panel_search.Controls.Add(this.label1);
            this.panel_search.Controls.Add(this.tb_search);
            this.panel_search.Controls.Add(this.btn_find);
            this.panel_search.Enabled = false;
            this.panel_search.Location = new System.Drawing.Point(3, 73);
            this.panel_search.Name = "panel_search";
            this.panel_search.Size = new System.Drawing.Size(238, 118);
            this.panel_search.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb_LE);
            this.groupBox1.Controls.Add(this.rb_E);
            this.groupBox1.Controls.Add(this.rb_ME);
            this.groupBox1.Location = new System.Drawing.Point(3, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(45, 87);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // rb_LE
            // 
            this.rb_LE.AutoSize = true;
            this.rb_LE.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb_LE.Location = new System.Drawing.Point(6, 57);
            this.rb_LE.Name = "rb_LE";
            this.rb_LE.Size = new System.Drawing.Size(33, 20);
            this.rb_LE.TabIndex = 2;
            this.rb_LE.Text = "≥";
            this.rb_LE.UseVisualStyleBackColor = true;
            // 
            // rb_E
            // 
            this.rb_E.AutoSize = true;
            this.rb_E.Checked = true;
            this.rb_E.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb_E.Location = new System.Drawing.Point(6, 34);
            this.rb_E.Name = "rb_E";
            this.rb_E.Size = new System.Drawing.Size(33, 20);
            this.rb_E.TabIndex = 1;
            this.rb_E.TabStop = true;
            this.rb_E.Text = "=";
            this.rb_E.UseVisualStyleBackColor = true;
            // 
            // rb_ME
            // 
            this.rb_ME.AutoSize = true;
            this.rb_ME.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb_ME.Location = new System.Drawing.Point(6, 10);
            this.rb_ME.Name = "rb_ME";
            this.rb_ME.Size = new System.Drawing.Size(33, 20);
            this.rb_ME.TabIndex = 0;
            this.rb_ME.Text = "≤";
            this.rb_ME.UseVisualStyleBackColor = true;
            // 
            // cb_parameterName
            // 
            this.cb_parameterName.FormattingEnabled = true;
            this.cb_parameterName.Location = new System.Drawing.Point(49, 4);
            this.cb_parameterName.Name = "cb_parameterName";
            this.cb_parameterName.Size = new System.Drawing.Size(182, 21);
            this.cb_parameterName.TabIndex = 3;
            this.cb_parameterName.Text = "Имя параметра";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Поиск";
            // 
            // tb_search
            // 
            this.tb_search.Location = new System.Drawing.Point(51, 50);
            this.tb_search.Name = "tb_search";
            this.tb_search.Size = new System.Drawing.Size(182, 20);
            this.tb_search.TabIndex = 1;
            // 
            // btn_find
            // 
            this.btn_find.Enabled = false;
            this.btn_find.Location = new System.Drawing.Point(156, 80);
            this.btn_find.Name = "btn_find";
            this.btn_find.Size = new System.Drawing.Size(75, 23);
            this.btn_find.TabIndex = 0;
            this.btn_find.Text = "Найти";
            this.btn_find.UseVisualStyleBackColor = true;
            this.btn_find.Click += new System.EventHandler(this.btn_find_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cb_table);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(238, 64);
            this.panel2.TabIndex = 3;
            // 
            // cb_table
            // 
            this.cb_table.FormattingEnabled = true;
            this.cb_table.Location = new System.Drawing.Point(6, 28);
            this.cb_table.Name = "cb_table";
            this.cb_table.Size = new System.Drawing.Size(225, 21);
            this.cb_table.TabIndex = 1;
            this.cb_table.SelectedIndexChanged += new System.EventHandler(this.cb_table_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Таблица";
            // 
            // panel_edit
            // 
            this.panel_edit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_edit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_edit.Controls.Add(this.label3);
            this.panel_edit.Controls.Add(this.btn_delete);
            this.panel_edit.Controls.Add(this.btn_add);
            this.panel_edit.Controls.Add(this.btn_edit);
            this.panel_edit.Enabled = false;
            this.panel_edit.Location = new System.Drawing.Point(3, 198);
            this.panel_edit.Name = "panel_edit";
            this.panel_edit.Size = new System.Drawing.Size(238, 104);
            this.panel_edit.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Изменение";
            // 
            // btn_delete
            // 
            this.btn_delete.Location = new System.Drawing.Point(34, 74);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(154, 23);
            this.btn_delete.TabIndex = 2;
            this.btn_delete.Text = "Удалить";
            this.btn_delete.UseVisualStyleBackColor = true;
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(34, 45);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(154, 23);
            this.btn_add.TabIndex = 1;
            this.btn_add.Text = "Добавить";
            this.btn_add.UseVisualStyleBackColor = true;
            // 
            // btn_edit
            // 
            this.btn_edit.Location = new System.Drawing.Point(34, 16);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(154, 23);
            this.btn_edit.TabIndex = 0;
            this.btn_edit.Text = "Изменить";
            this.btn_edit.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel_search, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel_edit, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 124F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(244, 305);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // splitContainer1
            // 
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DGV);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(792, 403);
            this.splitContainer1.SplitterDistance = 538;
            this.splitContainer1.TabIndex = 6;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 427);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Enabled = false;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.ms_dgvMS.ResumeLayout(false);
            this.panel_search.ResumeLayout(false);
            this.panel_search.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel_edit.ResumeLayout(false);
            this.panel_edit.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem инструментыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem генераторОтчетовToolStripMenuItem;
        private System.Windows.Forms.DataGridView DGV;
        private System.Windows.Forms.Panel panel_search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_search;
        private System.Windows.Forms.Button btn_find;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_table;
        private System.Windows.Forms.Panel panel_edit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem пользовательToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редактированиеПароляToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem оформитьНовыйЗаказToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem занестиДанныеОПоступленииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оформитьНовыйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem показатьТаблицуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оформитьПоступлениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem категорииИХарактеристикиТоваровToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem показатьТаблицуToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem сотрудникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новыйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem показатьТаблицуToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem редактироватьToolStripMenuItem;
        private System.Windows.Forms.ComboBox cb_parameterName;
        private System.Windows.Forms.ContextMenuStrip ms_dgvMS;
        private System.Windows.Forms.ToolStripMenuItem редактироватьToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem дОбавитьЗаписьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьЗапистьToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rb_LE;
        private System.Windows.Forms.RadioButton rb_E;
        private System.Windows.Forms.RadioButton rb_ME;

    }
}