﻿namespace SERIOUS_BUSINESS
{
    partial class FormNewEmpl
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
            this.tb_name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_login = new System.Windows.Forms.TextBox();
            this.btn_ok = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.rb_store = new System.Windows.Forms.RadioButton();
            this.rb_ord = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_accFull = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_name
            // 
            this.tb_name.Location = new System.Drawing.Point(12, 40);
            this.tb_name.Name = "tb_name";
            this.tb_name.Size = new System.Drawing.Size(284, 20);
            this.tb_name.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Фамилия И.О.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Логин";
            // 
            // tb_login
            // 
            this.tb_login.Location = new System.Drawing.Point(12, 83);
            this.tb_login.Name = "tb_login";
            this.tb_login.Size = new System.Drawing.Size(284, 20);
            this.tb_login.TabIndex = 2;
            // 
            // btn_ok
            // 
            this.btn_ok.Enabled = false;
            this.btn_ok.Location = new System.Drawing.Point(191, 158);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(92, 23);
            this.btn_ok.TabIndex = 5;
            this.btn_ok.Text = "Добавить";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(274, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Справка: пароль по умолчанию такой же, как логин.";
            // 
            // rb_store
            // 
            this.rb_store.AutoSize = true;
            this.rb_store.Location = new System.Drawing.Point(7, 16);
            this.rb_store.Name = "rb_store";
            this.rb_store.Size = new System.Drawing.Size(56, 17);
            this.rb_store.TabIndex = 7;
            this.rb_store.TabStop = true;
            this.rb_store.Text = "Склад";
            this.rb_store.UseVisualStyleBackColor = true;
            // 
            // rb_ord
            // 
            this.rb_ord.AutoSize = true;
            this.rb_ord.Location = new System.Drawing.Point(7, 35);
            this.rb_ord.Name = "rb_ord";
            this.rb_ord.Size = new System.Drawing.Size(64, 17);
            this.rb_ord.TabIndex = 8;
            this.rb_ord.TabStop = true;
            this.rb_ord.Text = "Заказы";
            this.rb_ord.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb_accFull);
            this.groupBox1.Controls.Add(this.rb_store);
            this.groupBox1.Controls.Add(this.rb_ord);
            this.groupBox1.Location = new System.Drawing.Point(12, 123);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(108, 78);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Доступ";
            // 
            // rb_accFull
            // 
            this.rb_accFull.AutoSize = true;
            this.rb_accFull.Location = new System.Drawing.Point(7, 58);
            this.rb_accFull.Name = "rb_accFull";
            this.rb_accFull.Size = new System.Drawing.Size(65, 17);
            this.rb_accFull.TabIndex = 9;
            this.rb_accFull.TabStop = true;
            this.rb_accFull.Text = "Полный";
            this.rb_accFull.UseVisualStyleBackColor = true;
            // 
            // FormNewEmpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 206);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.tb_login);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormNewEmpl";
            this.Text = "Новый сотрудник";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_login;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rb_store;
        private System.Windows.Forms.RadioButton rb_ord;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rb_accFull;
    }
}