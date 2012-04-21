namespace SERIOUS_BUSINESS
{
    partial class FormEditEmplSet
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
            this.gb_search = new System.Windows.Forms.GroupBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_search = new System.Windows.Forms.TextBox();
            this.gb_access = new System.Windows.Forms.GroupBox();
            this.rb_adm = new System.Windows.Forms.RadioButton();
            this.rb_mgr = new System.Windows.Forms.RadioButton();
            this.rb_store = new System.Windows.Forms.RadioButton();
            this.rb_none = new System.Windows.Forms.RadioButton();
            this.gb_info = new System.Windows.Forms.GroupBox();
            this.lbl_apptment = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btc_accept = new System.Windows.Forms.Button();
            this.gb_search.SuspendLayout();
            this.gb_access.SuspendLayout();
            this.gb_info.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_search
            // 
            this.gb_search.Controls.Add(this.btn_search);
            this.gb_search.Controls.Add(this.label1);
            this.gb_search.Controls.Add(this.tb_search);
            this.gb_search.Location = new System.Drawing.Point(6, 12);
            this.gb_search.Name = "gb_search";
            this.gb_search.Size = new System.Drawing.Size(232, 100);
            this.gb_search.TabIndex = 0;
            this.gb_search.TabStop = false;
            this.gb_search.Text = "Поиск";
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(151, 62);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 23);
            this.btn_search.TabIndex = 2;
            this.btn_search.Text = "Найти";
            this.btn_search.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Логин";
            // 
            // tb_search
            // 
            this.tb_search.Location = new System.Drawing.Point(6, 36);
            this.tb_search.Name = "tb_search";
            this.tb_search.Size = new System.Drawing.Size(220, 20);
            this.tb_search.TabIndex = 0;
            // 
            // gb_access
            // 
            this.gb_access.Controls.Add(this.rb_adm);
            this.gb_access.Controls.Add(this.rb_mgr);
            this.gb_access.Controls.Add(this.rb_store);
            this.gb_access.Controls.Add(this.rb_none);
            this.gb_access.Location = new System.Drawing.Point(6, 216);
            this.gb_access.Name = "gb_access";
            this.gb_access.Size = new System.Drawing.Size(232, 115);
            this.gb_access.TabIndex = 1;
            this.gb_access.TabStop = false;
            this.gb_access.Text = "Доступ";
            // 
            // rb_adm
            // 
            this.rb_adm.AutoSize = true;
            this.rb_adm.Location = new System.Drawing.Point(7, 92);
            this.rb_adm.Name = "rb_adm";
            this.rb_adm.Size = new System.Drawing.Size(65, 17);
            this.rb_adm.TabIndex = 3;
            this.rb_adm.TabStop = true;
            this.rb_adm.Text = "Полный";
            this.rb_adm.UseVisualStyleBackColor = true;
            // 
            // rb_mgr
            // 
            this.rb_mgr.AutoSize = true;
            this.rb_mgr.Location = new System.Drawing.Point(7, 68);
            this.rb_mgr.Name = "rb_mgr";
            this.rb_mgr.Size = new System.Drawing.Size(129, 17);
            this.rb_mgr.TabIndex = 2;
            this.rb_mgr.TabStop = true;
            this.rb_mgr.Text = "Частичный (Заказы)";
            this.rb_mgr.UseVisualStyleBackColor = true;
            // 
            // rb_store
            // 
            this.rb_store.AutoSize = true;
            this.rb_store.Location = new System.Drawing.Point(7, 44);
            this.rb_store.Name = "rb_store";
            this.rb_store.Size = new System.Drawing.Size(121, 17);
            this.rb_store.TabIndex = 1;
            this.rb_store.TabStop = true;
            this.rb_store.Text = "Частичный (Склад)";
            this.rb_store.UseVisualStyleBackColor = true;
            // 
            // rb_none
            // 
            this.rb_none.AutoSize = true;
            this.rb_none.Location = new System.Drawing.Point(7, 20);
            this.rb_none.Name = "rb_none";
            this.rb_none.Size = new System.Drawing.Size(87, 17);
            this.rb_none.TabIndex = 0;
            this.rb_none.TabStop = true;
            this.rb_none.Text = "Нет (убрать)";
            this.rb_none.UseVisualStyleBackColor = true;
            // 
            // gb_info
            // 
            this.gb_info.Controls.Add(this.lbl_apptment);
            this.gb_info.Controls.Add(this.label6);
            this.gb_info.Controls.Add(this.label5);
            this.gb_info.Controls.Add(this.label4);
            this.gb_info.Controls.Add(this.label3);
            this.gb_info.Controls.Add(this.label2);
            this.gb_info.Location = new System.Drawing.Point(6, 119);
            this.gb_info.Name = "gb_info";
            this.gb_info.Size = new System.Drawing.Size(232, 91);
            this.gb_info.TabIndex = 2;
            this.gb_info.TabStop = false;
            this.gb_info.Text = "Информация";
            // 
            // lbl_apptment
            // 
            this.lbl_apptment.AutoSize = true;
            this.lbl_apptment.Location = new System.Drawing.Point(102, 68);
            this.lbl_apptment.Name = "lbl_apptment";
            this.lbl_apptment.Size = new System.Drawing.Size(86, 13);
            this.lbl_apptment.TabIndex = 5;
            this.lbl_apptment.Text = "Администратор";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(10, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "Должность:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(104, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "11111";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(104, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "фио";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(10, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(10, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "ФИО:";
            // 
            // btc_accept
            // 
            this.btc_accept.Location = new System.Drawing.Point(41, 332);
            this.btc_accept.Name = "btc_accept";
            this.btc_accept.Size = new System.Drawing.Size(166, 23);
            this.btc_accept.TabIndex = 3;
            this.btc_accept.Text = "Применить";
            this.btc_accept.UseVisualStyleBackColor = true;
            // 
            // FormEditEmplSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 357);
            this.Controls.Add(this.btc_accept);
            this.Controls.Add(this.gb_info);
            this.Controls.Add(this.gb_access);
            this.Controls.Add(this.gb_search);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormEditEmplSet";
            this.Text = "Сотрудники";
            this.gb_search.ResumeLayout(false);
            this.gb_search.PerformLayout();
            this.gb_access.ResumeLayout(false);
            this.gb_access.PerformLayout();
            this.gb_info.ResumeLayout(false);
            this.gb_info.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_search;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_search;
        private System.Windows.Forms.GroupBox gb_access;
        private System.Windows.Forms.RadioButton rb_adm;
        private System.Windows.Forms.RadioButton rb_mgr;
        private System.Windows.Forms.RadioButton rb_store;
        private System.Windows.Forms.RadioButton rb_none;
        private System.Windows.Forms.GroupBox gb_info;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_apptment;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btc_accept;
    }
}