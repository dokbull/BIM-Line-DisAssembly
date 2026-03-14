namespace bim_base
{
    partial class FormTop
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
            this.components = new System.ComponentModel.Container();
            this.ui_timer = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.versionLabel = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.modelNameLabel = new System.Windows.Forms.Label();
            this.mxLabel = new System.Windows.Forms.Label();
            this.projectNameLabel = new System.Windows.Forms.Label();
            this.cTableLayoutPanel1 = new CTableLayoutPanel();
            this.panel1.SuspendLayout();
            this.cTableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ui_timer
            // 
            this.ui_timer.Enabled = true;
            this.ui_timer.Interval = 500;
            this.ui_timer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cTableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 70);
            this.panel1.TabIndex = 1;
            // 
            // versionLabel
            // 
            this.versionLabel.BackColor = System.Drawing.Color.Transparent;
            this.versionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.versionLabel.Font = new System.Drawing.Font("SamsungOne 800C", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionLabel.Location = new System.Drawing.Point(787, 46);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(234, 24);
            this.versionLabel.TabIndex = 7;
            this.versionLabel.Text = "[ Ver 0.00 ]";
            this.versionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.versionLabel.Click += new System.EventHandler(this.infoLabel_Click);
            // 
            // timeLabel
            // 
            this.timeLabel.BackColor = System.Drawing.Color.Transparent;
            this.timeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeLabel.Font = new System.Drawing.Font("SamsungOne 800C", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.Location = new System.Drawing.Point(787, 23);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(234, 23);
            this.timeLabel.TabIndex = 6;
            this.timeLabel.Text = "0000-00-00  00:00:00";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.timeLabel.Click += new System.EventHandler(this.infoLabel_Click);
            // 
            // modelNameLabel
            // 
            this.modelNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.modelNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelNameLabel.Font = new System.Drawing.Font("SamsungOne 800C", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modelNameLabel.Location = new System.Drawing.Point(787, 0);
            this.modelNameLabel.Name = "modelNameLabel";
            this.modelNameLabel.Size = new System.Drawing.Size(234, 23);
            this.modelNameLabel.TabIndex = 4;
            this.modelNameLabel.Text = "Model Name";
            this.modelNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.modelNameLabel.Click += new System.EventHandler(this.infoLabel_Click);
            // 
            // mxLabel
            // 
            this.mxLabel.AutoSize = true;
            this.mxLabel.BackColor = System.Drawing.Color.Transparent;
            this.mxLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mxLabel.Font = new System.Drawing.Font("SamsungOne 800C", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mxLabel.Location = new System.Drawing.Point(3, 46);
            this.mxLabel.Name = "mxLabel";
            this.mxLabel.Size = new System.Drawing.Size(206, 24);
            this.mxLabel.TabIndex = 2;
            this.mxLabel.Text = "Automation Engineering Group(MX)";
            this.mxLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mxLabel.Click += new System.EventHandler(this.mxLabel_Click);
            // 
            // projectNameLabel
            // 
            this.projectNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectNameLabel.Font = new System.Drawing.Font("SamsungOne 800C", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectNameLabel.Location = new System.Drawing.Point(215, 0);
            this.projectNameLabel.Name = "projectNameLabel";
            this.cTableLayoutPanel1.SetRowSpan(this.projectNameLabel, 3);
            this.projectNameLabel.Size = new System.Drawing.Size(566, 70);
            this.projectNameLabel.TabIndex = 3;
            this.projectNameLabel.Text = "Program Name";
            this.projectNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.projectNameLabel.DoubleClick += new System.EventHandler(this.projectNameLabel_DoubleClick);
            // 
            // cTableLayoutPanel1
            // 
            this.cTableLayoutPanel1.ColumnCount = 3;
            this.cTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.70313F));
            this.cTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.85938F));
            this.cTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.4375F));
            this.cTableLayoutPanel1.Controls.Add(this.versionLabel, 2, 2);
            this.cTableLayoutPanel1.Controls.Add(this.mxLabel, 0, 2);
            this.cTableLayoutPanel1.Controls.Add(this.modelNameLabel, 2, 0);
            this.cTableLayoutPanel1.Controls.Add(this.timeLabel, 2, 1);
            this.cTableLayoutPanel1.Controls.Add(this.projectNameLabel, 1, 0);
            this.cTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.cTableLayoutPanel1.Name = "cTableLayoutPanel1";
            this.cTableLayoutPanel1.RowCount = 3;
            this.cTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.cTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.cTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.cTableLayoutPanel1.Size = new System.Drawing.Size(1024, 70);
            this.cTableLayoutPanel1.TabIndex = 8;
            // 
            // FormTop
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(138)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1024, 70);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormTop";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.FormTop_Load);
            this.panel1.ResumeLayout(false);
            this.cTableLayoutPanel1.ResumeLayout(false);
            this.cTableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer ui_timer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label modelNameLabel;
        private System.Windows.Forms.Label mxLabel;
        private System.Windows.Forms.Label projectNameLabel;
        private CTableLayoutPanel cTableLayoutPanel1;
    }
}