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
            this.cb_app = new System.Windows.Forms.ComboBox();
            this.gb_info = new System.Windows.Forms.GroupBox();
            this.lbl_id = new System.Windows.Forms.Label();
            this.lbl_name = new System.Windows.Forms.Label();
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
            this.btn_search.Enabled = false;
            this.btn_search.Location = new System.Drawing.Point(151, 62);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 23);
            this.btn_search.TabIndex = 1;
            this.btn_search.Text = "Найти";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
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
            this.tb_search.TextChanged += new System.EventHandler(this.tb_search_TextChanged);
            // 
            // gb_access
            // 
            this.gb_access.Controls.Add(this.cb_app);
            this.gb_access.Location = new System.Drawing.Point(6, 191);
            this.gb_access.Name = "gb_access";
            this.gb_access.Size = new System.Drawing.Size(232, 49);
            this.gb_access.TabIndex = 1;
            this.gb_access.TabStop = false;
            this.gb_access.Text = "Доступ";
            // 
            // cb_app
            // 
            this.cb_app.FormattingEnabled = true;
            this.cb_app.Location = new System.Drawing.Point(6, 19);
            this.cb_app.Name = "cb_app";
            this.cb_app.Size = new System.Drawing.Size(220, 21);
            this.cb_app.TabIndex = 4;
            // 
            // gb_info
            // 
            this.gb_info.Controls.Add(this.lbl_id);
            this.gb_info.Controls.Add(this.lbl_name);
            this.gb_info.Controls.Add(this.label3);
            this.gb_info.Controls.Add(this.label2);
            this.gb_info.Location = new System.Drawing.Point(6, 119);
            this.gb_info.Name = "gb_info";
            this.gb_info.Size = new System.Drawing.Size(232, 66);
            this.gb_info.TabIndex = 2;
            this.gb_info.TabStop = false;
            this.gb_info.Text = "Информация";
            // 
            // lbl_id
            // 
            this.lbl_id.AutoSize = true;
            this.lbl_id.Location = new System.Drawing.Point(104, 43);
            this.lbl_id.Name = "lbl_id";
            this.lbl_id.Size = new System.Drawing.Size(0, 13);
            this.lbl_id.TabIndex = 3;
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Location = new System.Drawing.Point(104, 16);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(0, 13);
            this.lbl_name.TabIndex = 2;
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
            this.btc_accept.Location = new System.Drawing.Point(40, 246);
            this.btc_accept.Name = "btc_accept";
            this.btc_accept.Size = new System.Drawing.Size(166, 23);
            this.btc_accept.TabIndex = 5;
            this.btc_accept.Text = "Применить";
            this.btc_accept.UseVisualStyleBackColor = true;
            this.btc_accept.Click += new System.EventHandler(this.btc_accept_Click);
            // 
            // FormEditEmplSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 272);
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
        private System.Windows.Forms.GroupBox gb_info;
        private System.Windows.Forms.Label lbl_id;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btc_accept;
        private System.Windows.Forms.ComboBox cb_app;
    }
}