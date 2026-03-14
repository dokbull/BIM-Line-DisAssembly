namespace bim_base
{
    partial class FormProductInfo
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.hideButton = new System.Windows.Forms.Button();
            this.showButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.resetButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.output1Label = new System.Windows.Forms.Label();
            this.output2Label = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ng1Label = new System.Windows.Forms.Label();
            this.ng2Label = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.dayButton = new System.Windows.Forms.Button();
            this.nightButton = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.grid = new CSourceGrid();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label13 = new System.Windows.Forms.Label();
            this.selectedDateLabel = new System.Windows.Forms.Label();
            this.calendar = new System.Windows.Forms.MonthCalendar();
            this.ui_Timer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(965, 599);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.exitButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 522);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(959, 74);
            this.panel2.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.hideButton);
            this.groupBox1.Controls.Add(this.showButton);
            this.groupBox1.Location = new System.Drawing.Point(3, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 71);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Product Info Preview";
            // 
            // hideButton
            // 
            this.hideButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hideButton.Location = new System.Drawing.Point(125, 23);
            this.hideButton.Name = "hideButton";
            this.hideButton.Size = new System.Drawing.Size(108, 48);
            this.hideButton.TabIndex = 1;
            this.hideButton.Text = "HIDDEN";
            this.hideButton.UseVisualStyleBackColor = true;
            this.hideButton.Click += new System.EventHandler(this.hideButton_Click);
            // 
            // showButton
            // 
            this.showButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showButton.Location = new System.Drawing.Point(6, 23);
            this.showButton.Name = "showButton";
            this.showButton.Size = new System.Drawing.Size(108, 48);
            this.showButton.TabIndex = 0;
            this.showButton.Text = "SHOW";
            this.showButton.UseVisualStyleBackColor = true;
            this.showButton.Click += new System.EventHandler(this.showButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitButton.Font = new System.Drawing.Font("SamsungOne 800", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.Location = new System.Drawing.Point(765, 10);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(191, 62);
            this.exitButton.TabIndex = 0;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.tableLayoutPanel4);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.grid);
            this.panel1.Controls.Add(this.tableLayoutPanel5);
            this.panel1.Controls.Add(this.calendar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(959, 506);
            this.panel1.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Location = new System.Drawing.Point(7, 169);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(373, 235);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Today";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.resetButton, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.output1Label, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.output2Label, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.ng1Label, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.ng2Label, 2, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(7, 28);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(360, 200);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 50);
            this.label3.TabIndex = 2;
            // 
            // resetButton
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.resetButton, 2);
            this.resetButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resetButton.Font = new System.Drawing.Font("SamsungOne 800", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetButton.Location = new System.Drawing.Point(96, 153);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(261, 44);
            this.resetButton.TabIndex = 2;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(93, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 50);
            this.label2.TabIndex = 0;
            this.label2.Text = "#1";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label.Location = new System.Drawing.Point(226, 0);
            this.label.Margin = new System.Windows.Forms.Padding(0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(134, 50);
            this.label.TabIndex = 2;
            this.label.Text = "#2";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // output1Label
            // 
            this.output1Label.AutoSize = true;
            this.output1Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.output1Label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.output1Label.Location = new System.Drawing.Point(93, 50);
            this.output1Label.Margin = new System.Windows.Forms.Padding(0);
            this.output1Label.Name = "output1Label";
            this.output1Label.Size = new System.Drawing.Size(133, 50);
            this.output1Label.TabIndex = 1;
            this.output1Label.Text = "~";
            this.output1Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // output2Label
            // 
            this.output2Label.AutoSize = true;
            this.output2Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.output2Label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.output2Label.Location = new System.Drawing.Point(226, 50);
            this.output2Label.Margin = new System.Windows.Forms.Padding(0);
            this.output2Label.Name = "output2Label";
            this.output2Label.Size = new System.Drawing.Size(134, 50);
            this.output2Label.TabIndex = 5;
            this.output2Label.Text = "~";
            this.output2Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(0, 50);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 50);
            this.label6.TabIndex = 4;
            this.label6.Text = "Output";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(0, 100);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 50);
            this.label5.TabIndex = 3;
            this.label5.Text = "NG";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ng1Label
            // 
            this.ng1Label.AutoSize = true;
            this.ng1Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ng1Label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ng1Label.Location = new System.Drawing.Point(93, 100);
            this.ng1Label.Margin = new System.Windows.Forms.Padding(0);
            this.ng1Label.Name = "ng1Label";
            this.ng1Label.Size = new System.Drawing.Size(133, 50);
            this.ng1Label.TabIndex = 8;
            this.ng1Label.Text = "~";
            this.ng1Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ng2Label
            // 
            this.ng2Label.AutoSize = true;
            this.ng2Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ng2Label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ng2Label.Location = new System.Drawing.Point(226, 100);
            this.ng2Label.Margin = new System.Windows.Forms.Padding(0);
            this.ng2Label.Name = "ng2Label";
            this.ng2Label.Size = new System.Drawing.Size(134, 50);
            this.ng2Label.TabIndex = 7;
            this.ng2Label.Text = "~";
            this.ng2Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.dayButton, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.nightButton, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(386, 199);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(565, 33);
            this.tableLayoutPanel4.TabIndex = 20;
            // 
            // dayButton
            // 
            this.dayButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dayButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dayButton.Location = new System.Drawing.Point(0, 0);
            this.dayButton.Margin = new System.Windows.Forms.Padding(0);
            this.dayButton.Name = "dayButton";
            this.dayButton.Size = new System.Drawing.Size(282, 33);
            this.dayButton.TabIndex = 0;
            this.dayButton.Text = "08:00 ~ 20:00";
            this.dayButton.UseVisualStyleBackColor = true;
            this.dayButton.Click += new System.EventHandler(this.dayButton_Click);
            // 
            // nightButton
            // 
            this.nightButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nightButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nightButton.Location = new System.Drawing.Point(282, 0);
            this.nightButton.Margin = new System.Windows.Forms.Padding(0);
            this.nightButton.Name = "nightButton";
            this.nightButton.Size = new System.Drawing.Size(283, 33);
            this.nightButton.TabIndex = 0;
            this.nightButton.Text = "20:00 ~ 08:00";
            this.nightButton.UseVisualStyleBackColor = true;
            this.nightButton.Click += new System.EventHandler(this.nightButton_Click);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.LightGray;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Location = new System.Drawing.Point(386, 169);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(565, 30);
            this.label12.TabIndex = 19;
            this.label12.Text = "Product Information";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grid
            // 
            this.grid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.grid.EnableSort = true;
            this.grid.Font = new System.Drawing.Font("SamsungOne 800", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grid.Location = new System.Drawing.Point(386, 230);
            this.grid.Margin = new System.Windows.Forms.Padding(4);
            this.grid.Name = "grid";
            this.grid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.grid.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.grid.Size = new System.Drawing.Size(565, 269);
            this.grid.TabIndex = 18;
            this.grid.TabStop = true;
            this.grid.ToolTipText = "";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.selectedDateLabel, 1, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(386, 129);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(220, 40);
            this.tableLayoutPanel5.TabIndex = 17;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Margin = new System.Windows.Forms.Padding(0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(110, 40);
            this.label13.TabIndex = 0;
            this.label13.Text = "Selected";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // selectedDateLabel
            // 
            this.selectedDateLabel.AutoSize = true;
            this.selectedDateLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.selectedDateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectedDateLabel.Location = new System.Drawing.Point(110, 0);
            this.selectedDateLabel.Margin = new System.Windows.Forms.Padding(0);
            this.selectedDateLabel.Name = "selectedDateLabel";
            this.selectedDateLabel.Size = new System.Drawing.Size(110, 40);
            this.selectedDateLabel.TabIndex = 1;
            this.selectedDateLabel.Text = "2024-01-01";
            this.selectedDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // calendar
            // 
            this.calendar.Location = new System.Drawing.Point(731, 7);
            this.calendar.Margin = new System.Windows.Forms.Padding(0);
            this.calendar.MaxSelectionCount = 1;
            this.calendar.Name = "calendar";
            this.calendar.ShowTodayCircle = false;
            this.calendar.TabIndex = 16;
            this.calendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.calendar_DateChanged);
            // 
            // ui_Timer
            // 
            this.ui_Timer.Tick += new System.EventHandler(this.uiTimer_Tick);
            // 
            // FormProductInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 599);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("SamsungOne 800", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormProductInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Product Info";
            this.Load += new System.EventHandler(this.FormSubProductInfo_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button hideButton;
        private System.Windows.Forms.Button showButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label output1Label;
        private System.Windows.Forms.Label ng1Label;
        private System.Windows.Forms.Label ng2Label;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label output2Label;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer ui_Timer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button dayButton;
        private System.Windows.Forms.Button nightButton;
        private System.Windows.Forms.Label label12;
        private CSourceGrid grid;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label selectedDateLabel;
        private System.Windows.Forms.MonthCalendar calendar;
    }
}