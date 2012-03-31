namespace SERIOUS_BUSINESS
{
    partial class FormEditEmpl
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
            this.tb_oldPass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_newPass1 = new System.Windows.Forms.TextBox();
            this.lbl_username = new System.Windows.Forms.Label();
            this.tb_newPass2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_change = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Пользователь:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Старый пароль";
            // 
            // tb_oldPass
            // 
            this.tb_oldPass.Location = new System.Drawing.Point(12, 53);
            this.tb_oldPass.Name = "tb_oldPass";
            this.tb_oldPass.Size = new System.Drawing.Size(134, 20);
            this.tb_oldPass.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Новый пароль";
            // 
            // tb_newPass1
            // 
            this.tb_newPass1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tb_newPass1.Location = new System.Drawing.Point(12, 106);
            this.tb_newPass1.Name = "tb_newPass1";
            this.tb_newPass1.Size = new System.Drawing.Size(134, 20);
            this.tb_newPass1.TabIndex = 4;
            // 
            // lbl_username
            // 
            this.lbl_username.AutoSize = true;
            this.lbl_username.Location = new System.Drawing.Point(143, 18);
            this.lbl_username.Name = "lbl_username";
            this.lbl_username.Size = new System.Drawing.Size(53, 13);
            this.lbl_username.TabIndex = 5;
            this.lbl_username.Text = "username";
            // 
            // tb_newPass2
            // 
            this.tb_newPass2.Location = new System.Drawing.Point(12, 145);
            this.tb_newPass2.Name = "tb_newPass2";
            this.tb_newPass2.Size = new System.Drawing.Size(134, 20);
            this.tb_newPass2.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Повторите новый пароль";
            // 
            // btn_change
            // 
            this.btn_change.Location = new System.Drawing.Point(176, 113);
            this.btn_change.Name = "btn_change";
            this.btn_change.Size = new System.Drawing.Size(75, 23);
            this.btn_change.TabIndex = 8;
            this.btn_change.Text = "Сменить";
            this.btn_change.UseVisualStyleBackColor = true;
            this.btn_change.Click += new System.EventHandler(this.btn_change_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(176, 142);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 9;
            this.btn_cancel.Text = "Отмена";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // FormEditEmpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 171);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_change);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_newPass2);
            this.Controls.Add(this.lbl_username);
            this.Controls.Add(this.tb_newPass1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_oldPass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FormEditEmpl";
            this.Text = "Авторизац. данные";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_oldPass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_newPass1;
        private System.Windows.Forms.Label lbl_username;
        private System.Windows.Forms.TextBox tb_newPass2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_change;
        private System.Windows.Forms.Button btn_cancel;
    }
}