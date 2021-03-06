﻿namespace SERIOUS_BUSINESS
{
    partial class FormEditOrder
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_itemType = new System.Windows.Forms.ComboBox();
            this.cb_itemDesignation = new System.Windows.Forms.ComboBox();
            this.num_itemCount = new System.Windows.Forms.NumericUpDown();
            this.tb_Name = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_rmItem = new System.Windows.Forms.Button();
            this.btn_addItem = new System.Windows.Forms.Button();
            this.DGV = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tb_email = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_phone = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_accept = new System.Windows.Forms.Button();
            this.lbl_num = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.num_itemCount)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Заказ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Тип товара";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Количество";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Наименование товара";
            // 
            // cb_itemType
            // 
            this.cb_itemType.FormattingEnabled = true;
            this.cb_itemType.Location = new System.Drawing.Point(9, 44);
            this.cb_itemType.Name = "cb_itemType";
            this.cb_itemType.Size = new System.Drawing.Size(272, 21);
            this.cb_itemType.TabIndex = 1;
            // 
            // cb_itemDesignation
            // 
            this.cb_itemDesignation.FormattingEnabled = true;
            this.cb_itemDesignation.Location = new System.Drawing.Point(9, 93);
            this.cb_itemDesignation.Name = "cb_itemDesignation";
            this.cb_itemDesignation.Size = new System.Drawing.Size(233, 21);
            this.cb_itemDesignation.TabIndex = 2;
            // 
            // num_itemCount
            // 
            this.num_itemCount.Enabled = false;
            this.num_itemCount.Location = new System.Drawing.Point(9, 142);
            this.num_itemCount.Name = "num_itemCount";
            this.num_itemCount.Size = new System.Drawing.Size(121, 20);
            this.num_itemCount.TabIndex = 3;
            // 
            // tb_Name
            // 
            this.tb_Name.Location = new System.Drawing.Point(6, 44);
            this.tb_Name.Name = "tb_Name";
            this.tb_Name.Size = new System.Drawing.Size(275, 20);
            this.tb_Name.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.btn_rmItem);
            this.groupBox1.Controls.Add(this.btn_addItem);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cb_itemType);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cb_itemDesignation);
            this.groupBox1.Controls.Add(this.num_itemCount);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(18, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 172);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Позиция";
            // 
            // btn_rmItem
            // 
            this.btn_rmItem.Enabled = false;
            this.btn_rmItem.Location = new System.Drawing.Point(248, 126);
            this.btn_rmItem.Name = "btn_rmItem";
            this.btn_rmItem.Size = new System.Drawing.Size(33, 23);
            this.btn_rmItem.TabIndex = 5;
            this.btn_rmItem.Text = "-";
            this.btn_rmItem.UseVisualStyleBackColor = true;
            // 
            // btn_addItem
            // 
            this.btn_addItem.Enabled = false;
            this.btn_addItem.Location = new System.Drawing.Point(248, 91);
            this.btn_addItem.Name = "btn_addItem";
            this.btn_addItem.Size = new System.Drawing.Size(33, 23);
            this.btn_addItem.TabIndex = 4;
            this.btn_addItem.Text = "+";
            this.btn_addItem.UseVisualStyleBackColor = true;
            // 
            // DGV
            // 
            this.DGV.AllowUserToAddRows = false;
            this.DGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV.Location = new System.Drawing.Point(315, 87);
            this.DGV.Name = "DGV";
            this.DGV.Size = new System.Drawing.Size(520, 289);
            this.DGV.TabIndex = 17;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.tb_email);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.tb_phone);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tb_Name);
            this.groupBox2.Location = new System.Drawing.Point(18, 237);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(287, 172);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Заказчик";
            // 
            // tb_email
            // 
            this.tb_email.Location = new System.Drawing.Point(6, 141);
            this.tb_email.Name = "tb_email";
            this.tb_email.Size = new System.Drawing.Size(275, 20);
            this.tb_email.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Эл. почта";
            // 
            // tb_phone
            // 
            this.tb_phone.Location = new System.Drawing.Point(6, 93);
            this.tb_phone.Name = "tb_phone";
            this.tb_phone.Size = new System.Drawing.Size(275, 20);
            this.tb_phone.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Телефон";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Фамилия И. О.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(18, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "№";
            // 
            // btn_accept
            // 
            this.btn_accept.Enabled = false;
            this.btn_accept.Location = new System.Drawing.Point(679, 385);
            this.btn_accept.Name = "btn_accept";
            this.btn_accept.Size = new System.Drawing.Size(113, 23);
            this.btn_accept.TabIndex = 10;
            this.btn_accept.Text = "Подтвердить";
            this.btn_accept.UseVisualStyleBackColor = true;
            // 
            // lbl_num
            // 
            this.lbl_num.AutoSize = true;
            this.lbl_num.Location = new System.Drawing.Point(43, 3);
            this.lbl_num.Name = "lbl_num";
            this.lbl_num.Size = new System.Drawing.Size(31, 13);
            this.lbl_num.TabIndex = 15;
            this.lbl_num.Text = "1111";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbl_num);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Location = new System.Drawing.Point(90, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(113, 20);
            this.panel1.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(312, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(129, 18);
            this.label9.TabIndex = 18;
            this.label9.Text = "Состав заказа:";
            // 
            // FormEditOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 420);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.DGV);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_accept);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "FormEditOrder";
            this.Text = "Редактирование заказа";
            ((System.ComponentModel.ISupportInitialize)(this.num_itemCount)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_itemType;
        private System.Windows.Forms.ComboBox cb_itemDesignation;
        private System.Windows.Forms.NumericUpDown num_itemCount;
        private System.Windows.Forms.TextBox tb_Name;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tb_email;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_phone;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_accept;
        private System.Windows.Forms.Button btn_addItem;
        private System.Windows.Forms.Label lbl_num;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView DGV;
        private System.Windows.Forms.Button btn_rmItem;
        private System.Windows.Forms.Label label9;
    }
}