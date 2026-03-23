namespace bim_base
{
    partial class FormAlarmList
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lightAlarmButton = new System.Windows.Forms.Button();
            this.heavyAlarmButton = new System.Windows.Forms.Button();
            this.currentAlarmButton = new System.Windows.Forms.Button();
            this.alarmResetButton = new System.Windows.Forms.Button();
            this.alarmListGrid = new CSourceGrid();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lightAlarmButton);
            this.panel1.Controls.Add(this.heavyAlarmButton);
            this.panel1.Controls.Add(this.currentAlarmButton);
            this.panel1.Controls.Add(this.alarmResetButton);
            this.panel1.Location = new System.Drawing.Point(787, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(214, 602);
            this.panel1.TabIndex = 5;
            // 
            // lightAlarmButton
            // 
            this.lightAlarmButton.Location = new System.Drawing.Point(10, 185);
            this.lightAlarmButton.Margin = new System.Windows.Forms.Padding(5, 10, 10, 5);
            this.lightAlarmButton.Name = "lightAlarmButton";
            this.lightAlarmButton.Size = new System.Drawing.Size(194, 64);
            this.lightAlarmButton.TabIndex = 6;
            this.lightAlarmButton.Text = "LightAlarm";
            this.lightAlarmButton.UseVisualStyleBackColor = true;
            this.lightAlarmButton.Click += new System.EventHandler(this.lightAlarmButton_Click);
            // 
            // heavyAlarmButton
            // 
            this.heavyAlarmButton.Location = new System.Drawing.Point(10, 106);
            this.heavyAlarmButton.Margin = new System.Windows.Forms.Padding(5, 10, 10, 5);
            this.heavyAlarmButton.Name = "heavyAlarmButton";
            this.heavyAlarmButton.Size = new System.Drawing.Size(194, 64);
            this.heavyAlarmButton.TabIndex = 5;
            this.heavyAlarmButton.Text = "HeavyAlarm";
            this.heavyAlarmButton.UseVisualStyleBackColor = true;
            this.heavyAlarmButton.Click += new System.EventHandler(this.heavyAlarmButton_Click);
            // 
            // currentAlarmButton
            // 
            this.currentAlarmButton.Location = new System.Drawing.Point(10, 27);
            this.currentAlarmButton.Margin = new System.Windows.Forms.Padding(5, 10, 10, 5);
            this.currentAlarmButton.Name = "currentAlarmButton";
            this.currentAlarmButton.Size = new System.Drawing.Size(194, 64);
            this.currentAlarmButton.TabIndex = 4;
            this.currentAlarmButton.Text = "CurrentAlarm";
            this.currentAlarmButton.UseVisualStyleBackColor = true;
            this.currentAlarmButton.Click += new System.EventHandler(this.currentAlarmButton_Click);
            // 
            // alarmResetButton
            // 
            this.alarmResetButton.Location = new System.Drawing.Point(10, 502);
            this.alarmResetButton.Margin = new System.Windows.Forms.Padding(5, 10, 10, 5);
            this.alarmResetButton.Name = "alarmResetButton";
            this.alarmResetButton.Size = new System.Drawing.Size(194, 64);
            this.alarmResetButton.TabIndex = 3;
            this.alarmResetButton.Text = "AlarmReset";
            this.alarmResetButton.UseVisualStyleBackColor = true;
            this.alarmResetButton.Click += new System.EventHandler(this.alarmResetButton_Click);
            // 
            // alarmListGrid
            // 
            this.alarmListGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.alarmListGrid.EnableSort = true;
            this.alarmListGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alarmListGrid.Location = new System.Drawing.Point(4, 4);
            this.alarmListGrid.Margin = new System.Windows.Forms.Padding(4);
            this.alarmListGrid.Name = "alarmListGrid";
            this.alarmListGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.alarmListGrid.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.alarmListGrid.Size = new System.Drawing.Size(776, 600);
            this.alarmListGrid.TabIndex = 4;
            this.alarmListGrid.TabStop = true;
            this.alarmListGrid.ToolTipText = "";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel1.Controls.Add(this.alarmListGrid, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1004, 608);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // FormAlarmList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 628);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormAlarmList";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "FormAlarmList";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button lightAlarmButton;
        private System.Windows.Forms.Button heavyAlarmButton;
        private System.Windows.Forms.Button currentAlarmButton;
        private System.Windows.Forms.Button alarmResetButton;
        private CSourceGrid alarmListGrid;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}