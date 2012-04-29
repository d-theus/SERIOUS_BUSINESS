﻿namespace SERIOUS_BUSINESS
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
            this.cb_cat = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rb_binary = new System.Windows.Forms.RadioButton();
            this.rb_text = new System.Windows.Forms.RadioButton();
            this.rb_numeric = new System.Windows.Forms.RadioButton();
            this.btn_addPar = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_newItemDesignation = new System.Windows.Forms.TextBox();
            this.btn_addItem = new System.Windows.Forms.Button();
            this.btn_editItem = new System.Windows.Forms.Button();
            this.cb_existingItem = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DGV_itemParameters = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_addCat = new System.Windows.Forms.Button();
            this.tb_catName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.clb_params = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_itemParameters)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
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
            this.tb_newParamName.Size = new System.Drawing.Size(327, 20);
            this.tb_newParamName.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.cb_cat);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 179);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 439);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Изменить категорию товаров";
            // 
            // cb_cat
            // 
            this.cb_cat.FormattingEnabled = true;
            this.cb_cat.Location = new System.Drawing.Point(13, 32);
            this.cb_cat.Name = "cb_cat";
            this.cb_cat.Size = new System.Drawing.Size(321, 21);
            this.cb_cat.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rb_binary);
            this.groupBox4.Controls.Add(this.tb_newParamName);
            this.groupBox4.Controls.Add(this.rb_text);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.rb_numeric);
            this.groupBox4.Controls.Add(this.btn_addPar);
            this.groupBox4.Location = new System.Drawing.Point(12, 97);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(381, 79);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Новый параметр";
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
            this.btn_addPar.Location = new System.Drawing.Point(340, 30);
            this.btn_addPar.Name = "btn_addPar";
            this.btn_addPar.Size = new System.Drawing.Size(35, 23);
            this.btn_addPar.TabIndex = 4;
            this.btn_addPar.Text = "+";
            this.btn_addPar.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.tb_newItemDesignation);
            this.groupBox3.Controls.Add(this.btn_addItem);
            this.groupBox3.Controls.Add(this.btn_editItem);
            this.groupBox3.Controls.Add(this.cb_existingItem);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.DGV_itemParameters);
            this.groupBox3.Location = new System.Drawing.Point(7, 188);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(368, 241);
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
            this.tb_newItemDesignation.Location = new System.Drawing.Point(7, 36);
            this.tb_newItemDesignation.Name = "tb_newItemDesignation";
            this.tb_newItemDesignation.Size = new System.Drawing.Size(308, 20);
            this.tb_newItemDesignation.TabIndex = 7;
            // 
            // btn_addItem
            // 
            this.btn_addItem.Location = new System.Drawing.Point(327, 34);
            this.btn_addItem.Name = "btn_addItem";
            this.btn_addItem.Size = new System.Drawing.Size(35, 23);
            this.btn_addItem.TabIndex = 6;
            this.btn_addItem.Text = "+";
            this.btn_addItem.UseVisualStyleBackColor = true;
            // 
            // btn_editItem
            // 
            this.btn_editItem.Location = new System.Drawing.Point(264, 83);
            this.btn_editItem.Name = "btn_editItem";
            this.btn_editItem.Size = new System.Drawing.Size(93, 23);
            this.btn_editItem.TabIndex = 5;
            this.btn_editItem.Text = "Редактировать";
            this.btn_editItem.UseVisualStyleBackColor = true;
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
            this.DGV_itemParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_itemParameters.Location = new System.Drawing.Point(10, 112);
            this.DGV_itemParameters.Name = "DGV_itemParameters";
            this.DGV_itemParameters.Size = new System.Drawing.Size(351, 122);
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
            this.btn_addCat.Location = new System.Drawing.Point(340, 41);
            this.btn_addCat.Name = "btn_addCat";
            this.btn_addCat.Size = new System.Drawing.Size(35, 23);
            this.btn_addCat.TabIndex = 2;
            this.btn_addCat.Text = "+";
            this.btn_addCat.UseVisualStyleBackColor = true;
            // 
            // tb_catName
            // 
            this.tb_catName.Location = new System.Drawing.Point(7, 43);
            this.tb_catName.Name = "tb_catName";
            this.tb_catName.Size = new System.Drawing.Size(327, 20);
            this.tb_catName.TabIndex = 1;
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
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox5.Controls.Add(this.clb_params);
            this.groupBox5.Location = new System.Drawing.Point(7, 60);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(368, 122);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Параметры категории";
            // 
            // clb_params
            // 
            this.clb_params.FormattingEnabled = true;
            this.clb_params.Location = new System.Drawing.Point(7, 20);
            this.clb_params.Name = "clb_params";
            this.clb_params.Size = new System.Drawing.Size(355, 94);
            this.clb_params.TabIndex = 0;
            // 
            // FormEditCategories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 620);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEditCategories";
            this.Text = "Товары и параметры";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_itemParameters)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
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
        private System.Windows.Forms.Button btn_editItem;
        private System.Windows.Forms.ComboBox cb_existingItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView DGV_itemParameters;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_newItemDesignation;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckedListBox clb_params;
    }
}