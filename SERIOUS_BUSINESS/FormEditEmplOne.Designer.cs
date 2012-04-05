namespace SERIOUS_BUSINESS
{
    partial class FormEditEmplOne
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
            this.tb_newPass2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_passwdChange = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_loginChange = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_login = new System.Windows.Forms.TextBox();
            this.lbl_username = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.label2.Location = new System.Drawing.Point(7, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Старый пароль";
            // 
            // tb_oldPass
            // 
            this.tb_oldPass.Location = new System.Drawing.Point(7, 38);
            this.tb_oldPass.Name = "tb_oldPass";
            this.tb_oldPass.PasswordChar = '*';
            this.tb_oldPass.Size = new System.Drawing.Size(173, 20);
            this.tb_oldPass.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Новый пароль";
            // 
            // tb_newPass1
            // 
            this.tb_newPass1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tb_newPass1.Location = new System.Drawing.Point(7, 91);
            this.tb_newPass1.Name = "tb_newPass1";
            this.tb_newPass1.PasswordChar = '*';
            this.tb_newPass1.Size = new System.Drawing.Size(173, 20);
            this.tb_newPass1.TabIndex = 4;
            // 
            // tb_newPass2
            // 
            this.tb_newPass2.Location = new System.Drawing.Point(7, 130);
            this.tb_newPass2.Name = "tb_newPass2";
            this.tb_newPass2.PasswordChar = '*';
            this.tb_newPass2.Size = new System.Drawing.Size(173, 20);
            this.tb_newPass2.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Повторите новый пароль";
            // 
            // btn_passwdChange
            // 
            this.btn_passwdChange.Location = new System.Drawing.Point(105, 151);
            this.btn_passwdChange.Name = "btn_passwdChange";
            this.btn_passwdChange.Size = new System.Drawing.Size(75, 23);
            this.btn_passwdChange.TabIndex = 6;
            this.btn_passwdChange.Text = "Сменить";
            this.btn_passwdChange.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_passwdChange);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tb_oldPass);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tb_newPass2);
            this.groupBox1.Controls.Add(this.tb_newPass1);
            this.groupBox1.Location = new System.Drawing.Point(16, 130);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(191, 179);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Пароль";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_loginChange);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tb_login);
            this.groupBox2.Location = new System.Drawing.Point(16, 35);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(191, 89);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Логин";
            // 
            // btn_loginChange
            // 
            this.btn_loginChange.Location = new System.Drawing.Point(105, 62);
            this.btn_loginChange.Name = "btn_loginChange";
            this.btn_loginChange.Size = new System.Drawing.Size(75, 23);
            this.btn_loginChange.TabIndex = 1;
            this.btn_loginChange.Text = "Сменить";
            this.btn_loginChange.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Новый логин";
            // 
            // tb_login
            // 
            this.tb_login.Location = new System.Drawing.Point(7, 36);
            this.tb_login.Name = "tb_login";
            this.tb_login.Size = new System.Drawing.Size(173, 20);
            this.tb_login.TabIndex = 0;
            // 
            // lbl_username
            // 
            this.lbl_username.AutoSize = true;
            this.lbl_username.Location = new System.Drawing.Point(123, 17);
            this.lbl_username.Name = "lbl_username";
            this.lbl_username.Size = new System.Drawing.Size(35, 13);
            this.lbl_username.TabIndex = 12;
            this.lbl_username.Text = "label6";
            // 
            // FormEditEmplOne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(210, 312);
            this.Controls.Add(this.lbl_username);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FormEditEmplOne";
            this.Text = "Мои данные";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_oldPass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_newPass1;
        private System.Windows.Forms.TextBox tb_newPass2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_passwdChange;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_loginChange;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_login;
        private System.Windows.Forms.Label lbl_username;
    }
}