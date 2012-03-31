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
            this.tb_name = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_binary = new System.Windows.Forms.RadioButton();
            this.rb_text = new System.Windows.Forms.RadioButton();
            this.rb_numeric = new System.Windows.Forms.RadioButton();
            this.btn_addPar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_cat = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_addCat = new System.Windows.Forms.Button();
            this.tb_catName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название";
            // 
            // tb_name
            // 
            this.tb_name.Location = new System.Drawing.Point(7, 83);
            this.tb_name.Name = "tb_name";
            this.tb_name.Size = new System.Drawing.Size(242, 20);
            this.tb_name.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb_binary);
            this.groupBox1.Controls.Add(this.rb_text);
            this.groupBox1.Controls.Add(this.rb_numeric);
            this.groupBox1.Controls.Add(this.btn_addPar);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmb_cat);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tb_name);
            this.groupBox1.Location = new System.Drawing.Point(12, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 135);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Добавить новый параметр";
            // 
            // rb_binary
            // 
            this.rb_binary.AutoSize = true;
            this.rb_binary.Location = new System.Drawing.Point(174, 110);
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
            this.rb_text.Location = new System.Drawing.Point(87, 110);
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
            this.rb_numeric.Location = new System.Drawing.Point(6, 110);
            this.rb_numeric.Name = "rb_numeric";
            this.rb_numeric.Size = new System.Drawing.Size(75, 17);
            this.rb_numeric.TabIndex = 5;
            this.rb_numeric.TabStop = true;
            this.rb_numeric.Text = "Числовой";
            this.rb_numeric.UseVisualStyleBackColor = true;
            // 
            // btn_addPar
            // 
            this.btn_addPar.Location = new System.Drawing.Point(255, 83);
            this.btn_addPar.Name = "btn_addPar";
            this.btn_addPar.Size = new System.Drawing.Size(41, 23);
            this.btn_addPar.TabIndex = 4;
            this.btn_addPar.Text = "+";
            this.btn_addPar.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Категория";
            // 
            // cmb_cat
            // 
            this.cmb_cat.FormattingEnabled = true;
            this.cmb_cat.Location = new System.Drawing.Point(6, 43);
            this.cmb_cat.Name = "cmb_cat";
            this.cmb_cat.Size = new System.Drawing.Size(188, 21);
            this.cmb_cat.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_addCat);
            this.groupBox2.Controls.Add(this.tb_catName);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(302, 80);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Добавить новый тип товаров";
            // 
            // btn_addCat
            // 
            this.btn_addCat.Location = new System.Drawing.Point(255, 43);
            this.btn_addCat.Name = "btn_addCat";
            this.btn_addCat.Size = new System.Drawing.Size(41, 23);
            this.btn_addCat.TabIndex = 2;
            this.btn_addCat.Text = "+";
            this.btn_addCat.UseVisualStyleBackColor = true;
            // 
            // tb_catName
            // 
            this.tb_catName.Location = new System.Drawing.Point(7, 43);
            this.tb_catName.Name = "tb_catName";
            this.tb_catName.Size = new System.Drawing.Size(242, 20);
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
            // FormEditCategories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 262);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormEditCategories";
            this.Text = "Услуги";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_name;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_addPar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_cat;
        private System.Windows.Forms.TextBox tb_catName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rb_binary;
        private System.Windows.Forms.RadioButton rb_text;
        private System.Windows.Forms.RadioButton rb_numeric;
        private System.Windows.Forms.Button btn_addCat;
    }
}