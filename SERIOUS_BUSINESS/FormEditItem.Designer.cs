namespace SERIOUS_BUSINESS
{
    partial class FormEditItem
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
            this.btn_ok = new System.Windows.Forms.Button();
            this.tn_cancel = new System.Windows.Forms.Button();
            this.cb_type = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_designation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pb_purchPrice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_salePrice = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Тип";
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(49, 184);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 1;
            this.btn_ok.Text = "ОК";
            this.btn_ok.UseVisualStyleBackColor = true;
            // 
            // tn_cancel
            // 
            this.tn_cancel.Location = new System.Drawing.Point(130, 184);
            this.tn_cancel.Name = "tn_cancel";
            this.tn_cancel.Size = new System.Drawing.Size(75, 23);
            this.tn_cancel.TabIndex = 2;
            this.tn_cancel.Text = "Отмена";
            this.tn_cancel.UseVisualStyleBackColor = true;
            this.tn_cancel.Click += new System.EventHandler(this.tn_cancel_Click);
            // 
            // cb_type
            // 
            this.cb_type.FormattingEnabled = true;
            this.cb_type.Location = new System.Drawing.Point(12, 25);
            this.cb_type.Name = "cb_type";
            this.cb_type.Size = new System.Drawing.Size(156, 21);
            this.cb_type.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Наименование";
            // 
            // tb_designation
            // 
            this.tb_designation.Location = new System.Drawing.Point(12, 65);
            this.tb_designation.Name = "tb_designation";
            this.tb_designation.Size = new System.Drawing.Size(193, 20);
            this.tb_designation.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Стоимость закупки";
            // 
            // pb_purchPrice
            // 
            this.pb_purchPrice.Location = new System.Drawing.Point(12, 119);
            this.pb_purchPrice.Name = "pb_purchPrice";
            this.pb_purchPrice.Size = new System.Drawing.Size(83, 20);
            this.pb_purchPrice.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Стоимость продажи";
            // 
            // tb_salePrice
            // 
            this.tb_salePrice.Location = new System.Drawing.Point(12, 158);
            this.tb_salePrice.Name = "tb_salePrice";
            this.tb_salePrice.Size = new System.Drawing.Size(83, 20);
            this.tb_salePrice.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(101, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "руб.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(101, 161);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "руб.";
            // 
            // FormEditItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 218);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tb_salePrice);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pb_purchPrice);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_designation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cb_type);
            this.Controls.Add(this.tn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FormEditItem";
            this.Text = "Услуга\\товар";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button tn_cancel;
        private System.Windows.Forms.ComboBox cb_type;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_designation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox pb_purchPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_salePrice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}