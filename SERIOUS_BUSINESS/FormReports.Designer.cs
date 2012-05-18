namespace SERIOUS_BUSINESS
{
    partial class FormReports
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
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.cb_type = new System.Windows.Forms.ComboBox();
            this.mc_initialDate = new System.Windows.Forms.MonthCalendar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_generate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(12, 5);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(384, 491);
            this.webBrowser.TabIndex = 0;
            this.webBrowser.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // cb_type
            // 
            this.cb_type.FormattingEnabled = true;
            this.cb_type.Location = new System.Drawing.Point(403, 38);
            this.cb_type.Name = "cb_type";
            this.cb_type.Size = new System.Drawing.Size(221, 21);
            this.cb_type.TabIndex = 1;
            // 
            // mc_initialDate
            // 
            this.mc_initialDate.Location = new System.Drawing.Point(403, 96);
            this.mc_initialDate.MaxSelectionCount = 365;
            this.mc_initialDate.Name = "mc_initialDate";
            this.mc_initialDate.TabIndex = 2;
            this.mc_initialDate.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.mc_initialDate_DateChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(400, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Тип отчета";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(400, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Начало наблюдения";
            // 
            // btn_generate
            // 
            this.btn_generate.Location = new System.Drawing.Point(517, 271);
            this.btn_generate.Name = "btn_generate";
            this.btn_generate.Size = new System.Drawing.Size(107, 23);
            this.btn_generate.TabIndex = 2;
            this.btn_generate.Text = "Сгенерировать";
            this.btn_generate.UseVisualStyleBackColor = true;
            this.btn_generate.Click += new System.EventHandler(this.btn_generate_Click);
            // 
            // FormReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 508);
            this.Controls.Add(this.btn_generate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mc_initialDate);
            this.Controls.Add(this.cb_type);
            this.Controls.Add(this.webBrowser);
            this.MaximizeBox = false;
            this.Name = "FormReports";
            this.Text = "FormReports";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.ComboBox cb_type;
        private System.Windows.Forms.MonthCalendar mc_initialDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_generate;
    }
}