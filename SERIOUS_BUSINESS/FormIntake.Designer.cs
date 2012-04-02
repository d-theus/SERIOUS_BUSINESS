namespace SERIOUS_BUSINESS
{
    partial class FormIntake
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
            this.num_count = new System.Windows.Forms.NumericUpDown();
            this.cmb_designation = new System.Windows.Forms.ComboBox();
            this.cmb_type = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_accept = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.num_count)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Тип";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Наименование";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Количество";
            // 
            // num_count
            // 
            this.num_count.Location = new System.Drawing.Point(12, 134);
            this.num_count.Name = "num_count";
            this.num_count.Size = new System.Drawing.Size(120, 20);
            this.num_count.TabIndex = 3;
            // 
            // cmb_designation
            // 
            this.cmb_designation.FormattingEnabled = true;
            this.cmb_designation.Location = new System.Drawing.Point(12, 94);
            this.cmb_designation.Name = "cmb_designation";
            this.cmb_designation.Size = new System.Drawing.Size(365, 21);
            this.cmb_designation.TabIndex = 4;
            // 
            // cmb_type
            // 
            this.cmb_type.FormattingEnabled = true;
            this.cmb_type.Location = new System.Drawing.Point(12, 54);
            this.cmb_type.Name = "cmb_type";
            this.cmb_type.Size = new System.Drawing.Size(365, 21);
            this.cmb_type.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(12, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Поступления";
            // 
            // btn_accept
            // 
            this.btn_accept.Location = new System.Drawing.Point(258, 134);
            this.btn_accept.Name = "btn_accept";
            this.btn_accept.Size = new System.Drawing.Size(119, 23);
            this.btn_accept.TabIndex = 7;
            this.btn_accept.Text = "Занести данные";
            this.btn_accept.UseVisualStyleBackColor = true;
            // 
            // FormIntake
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 161);
            this.Controls.Add(this.btn_accept);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmb_type);
            this.Controls.Add(this.cmb_designation);
            this.Controls.Add(this.num_count);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FormIntake";
            this.Text = "Новые поступления";
            ((System.ComponentModel.ISupportInitialize)(this.num_count)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown num_count;
        private System.Windows.Forms.ComboBox cmb_designation;
        private System.Windows.Forms.ComboBox cmb_type;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_accept;

    }
}