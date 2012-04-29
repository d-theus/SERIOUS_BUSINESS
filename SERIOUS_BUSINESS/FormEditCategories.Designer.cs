namespace SERIOUS_BUSINESS
{
    partial class FormEditCategories
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
            this.label1 = new System.Windows.Forms.Label();
            this.tb_newParamName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btn_accParams = new System.Windows.Forms.Button();
            this.DGV_catParameters = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rb_binary = new System.Windows.Forms.RadioButton();
            this.rb_text = new System.Windows.Forms.RadioButton();
            this.rb_numeric = new System.Windows.Forms.RadioButton();
            this.btn_addPar = new System.Windows.Forms.Button();
            this.cb_cat = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_newItemDesignation = new System.Windows.Forms.TextBox();
            this.btn_addItem = new System.Windows.Forms.Button();
            this.btn_accItem = new System.Windows.Forms.Button();
            this.cb_existingItem = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DGV_itemParameters = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_addCat = new System.Windows.Forms.Button();
            this.tb_catName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_catParameters)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_itemParameters)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название";
            // 
            // tb_newParamName
            // 
            this.tb_newParamName.Location = new System.Drawing.Point(7, 32);
            this.tb_newParamName.Name = "tb_newParamName";
            this.tb_newParamName.Size = new System.Drawing.Size(307, 20);
            this.tb_newParamName.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.cb_cat);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 112);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 561);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Изменить категорию товаров";
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox5.Controls.Add(this.btn_accParams);
            this.groupBox5.Controls.Add(this.DGV_catParameters);
            this.groupBox5.Location = new System.Drawing.Point(1, 144);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(374, 149);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Параметры категории";
            // 
            // btn_accParams
            // 
            this.btn_accParams.Location = new System.Drawing.Point(263, 120);
            this.btn_accParams.Name = "btn_accParams";
            this.btn_accParams.Size = new System.Drawing.Size(93, 23);
            this.btn_accParams.TabIndex = 1;
            this.btn_accParams.Text = "Применить";
            this.btn_accParams.UseVisualStyleBackColor = true;
            // 
            // DGV_catParameters
            // 
            this.DGV_catParameters.AllowUserToAddRows = false;
            this.DGV_catParameters.AllowUserToDeleteRows = false;
            this.DGV_catParameters.AllowUserToResizeColumns = false;
            this.DGV_catParameters.AllowUserToResizeRows = false;
            this.DGV_catParameters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV_catParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_catParameters.Location = new System.Drawing.Point(10, 20);
            this.DGV_catParameters.Name = "DGV_catParameters";
            this.DGV_catParameters.Size = new System.Drawing.Size(351, 94);
            this.DGV_catParameters.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rb_binary);
            this.groupBox4.Controls.Add(this.tb_newParamName);
            this.groupBox4.Controls.Add(this.rb_text);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.rb_numeric);
            this.groupBox4.Controls.Add(this.btn_addPar);
            this.groupBox4.Location = new System.Drawing.Point(0, 59);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(375, 79);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Новый параметр категории";
            // 
            // rb_binary
            // 
            this.rb_binary.AutoSize = true;
            this.rb_binary.Location = new System.Drawing.Point(174, 59);
            this.rb_binary.Name = "rb_binary";
            this.rb_binary.Size = new System.Drawing.Size(77, 17);
            this.rb_binary.TabIndex = 7;
            this.rb_binary.TabStop = true;
            this.rb_binary.Text = "Двоичный";
            this.rb_binary.UseVisualStyleBackColor = true;
            // 
            // rb_text
            // 
            this.rb_text.AutoSize = true;
            this.rb_text.Location = new System.Drawing.Point(87, 59);
            this.rb_text.Name = "rb_text";
            this.rb_text.Size = new System.Drawing.Size(81, 17);
            this.rb_text.TabIndex = 6;
            this.rb_text.TabStop = true;
            this.rb_text.Text = "Текстовый";
            this.rb_text.UseVisualStyleBackColor = true;
            // 
            // rb_numeric
            // 
            this.rb_numeric.AutoSize = true;
            this.rb_numeric.Location = new System.Drawing.Point(6, 59);
            this.rb_numeric.Name = "rb_numeric";
            this.rb_numeric.Size = new System.Drawing.Size(75, 17);
            this.rb_numeric.TabIndex = 5;
            this.rb_numeric.TabStop = true;
            this.rb_numeric.Text = "Числовой";
            this.rb_numeric.UseVisualStyleBackColor = true;
            // 
            // btn_addPar
            // 
            this.btn_addPar.Enabled = false;
            this.btn_addPar.Location = new System.Drawing.Point(334, 30);
            this.btn_addPar.Name = "btn_addPar";
            this.btn_addPar.Size = new System.Drawing.Size(35, 23);
            this.btn_addPar.TabIndex = 4;
            this.btn_addPar.Text = "+";
            this.btn_addPar.UseVisualStyleBackColor = true;
            this.btn_addPar.Click += new System.EventHandler(this.btn_addPar_Click);
            // 
            // cb_cat
            // 
            this.cb_cat.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cb_cat.FormattingEnabled = true;
            this.cb_cat.Location = new System.Drawing.Point(13, 32);
            this.cb_cat.Name = "cb_cat";
            this.cb_cat.Size = new System.Drawing.Size(321, 21);
            this.cb_cat.Sorted = true;
            this.cb_cat.TabIndex = 2;
            this.cb_cat.SelectedIndexChanged += new System.EventHandler(this.cb_cat_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.tb_newItemDesignation);
            this.groupBox3.Controls.Add(this.btn_addItem);
            this.groupBox3.Controls.Add(this.btn_accItem);
            this.groupBox3.Controls.Add(this.cb_existingItem);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.DGV_itemParameters);
            this.groupBox3.Location = new System.Drawing.Point(0, 299);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(375, 256);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Товары категории";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Добавить:";
            // 
            // tb_newItemDesignation
            // 
            this.tb_newItemDesignation.Location = new System.Drawing.Point(13, 36);
            this.tb_newItemDesignation.Name = "tb_newItemDesignation";
            this.tb_newItemDesignation.Size = new System.Drawing.Size(301, 20);
            this.tb_newItemDesignation.TabIndex = 9;
            // 
            // btn_addItem
            // 
            this.btn_addItem.Enabled = false;
            this.btn_addItem.Location = new System.Drawing.Point(334, 33);
            this.btn_addItem.Name = "btn_addItem";
            this.btn_addItem.Size = new System.Drawing.Size(35, 23);
            this.btn_addItem.TabIndex = 6;
            this.btn_addItem.Text = "+";
            this.btn_addItem.UseVisualStyleBackColor = true;
            // 
            // btn_accItem
            // 
            this.btn_accItem.Location = new System.Drawing.Point(264, 83);
            this.btn_accItem.Name = "btn_accItem";
            this.btn_accItem.Size = new System.Drawing.Size(93, 23);
            this.btn_accItem.TabIndex = 5;
            this.btn_accItem.Text = "Применить";
            this.btn_accItem.UseVisualStyleBackColor = true;
            // 
            // cb_existingItem
            // 
            this.cb_existingItem.FormattingEnabled = true;
            this.cb_existingItem.Location = new System.Drawing.Point(10, 86);
            this.cb_existingItem.Name = "cb_existingItem";
            this.cb_existingItem.Size = new System.Drawing.Size(247, 21);
            this.cb_existingItem.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Изменить существующий";
            // 
            // DGV_itemParameters
            // 
            this.DGV_itemParameters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV_itemParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_itemParameters.Location = new System.Drawing.Point(10, 112);
            this.DGV_itemParameters.Name = "DGV_itemParameters";
            this.DGV_itemParameters.Size = new System.Drawing.Size(351, 138);
            this.DGV_itemParameters.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Категория";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_addCat);
            this.groupBox2.Controls.Add(this.tb_catName);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(381, 80);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Добавить категорию товаров";
            // 
            // btn_addCat
            // 
            this.btn_addCat.Enabled = false;
            this.btn_addCat.Location = new System.Drawing.Point(334, 41);
            this.btn_addCat.Name = "btn_addCat";
            this.btn_addCat.Size = new System.Drawing.Size(35, 23);
            this.btn_addCat.TabIndex = 2;
            this.btn_addCat.Text = "+";
            this.btn_addCat.UseVisualStyleBackColor = true;
            this.btn_addCat.Click += new System.EventHandler(this.btn_addCat_Click);
            // 
            // tb_catName
            // 
            this.tb_catName.Location = new System.Drawing.Point(7, 43);
            this.tb_catName.Name = "tb_catName";
            this.tb_catName.Size = new System.Drawing.Size(307, 20);
            this.tb_catName.TabIndex = 1;
            this.tb_catName.TextChanged += new System.EventHandler(this.tb_catName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Название";
            // 
            // FormEditCategories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 677);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEditCategories";
            this.Text = "Товары и параметры";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_catParameters)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_itemParameters)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_newParamName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_addPar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_cat;
        private System.Windows.Forms.TextBox tb_catName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rb_binary;
        private System.Windows.Forms.RadioButton rb_text;
        private System.Windows.Forms.RadioButton rb_numeric;
        private System.Windows.Forms.Button btn_addCat;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_addItem;
        private System.Windows.Forms.Button btn_accItem;
        private System.Windows.Forms.ComboBox cb_existingItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView DGV_itemParameters;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_newItemDesignation;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btn_accParams;
        private System.Windows.Forms.DataGridView DGV_catParameters;
    }
}