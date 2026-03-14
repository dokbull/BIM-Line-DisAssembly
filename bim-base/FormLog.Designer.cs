namespace bim_base
{
    partial class FormLog
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
            this.calendar = new System.Windows.Forms.MonthCalendar();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.selectedDateLabel = new System.Windows.Forms.Label();
            this.todayButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.readAlarmButton = new System.Windows.Forms.Button();
            this.clearAlarmButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.endDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.startDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.ui_timer = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.countGrid = new CSourceGrid();
            this.grid = new CSourceGrid();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // calendar
            // 
            this.calendar.Location = new System.Drawing.Point(20, 5);
            this.calendar.Margin = new System.Windows.Forms.Padding(0);
            this.calendar.MaxSelectionCount = 1;
            this.calendar.Name = "calendar";
            this.calendar.ShowTodayCircle = false;
            this.calendar.TabIndex = 0;
            this.calendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.calendar_DateChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.selectedDateLabel, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(257, 120);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(336, 38);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Selected Date";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // selectedDateLabel
            // 
            this.selectedDateLabel.AutoSize = true;
            this.selectedDateLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.selectedDateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectedDateLabel.Location = new System.Drawing.Point(168, 0);
            this.selectedDateLabel.Margin = new System.Windows.Forms.Padding(0);
            this.selectedDateLabel.Name = "selectedDateLabel";
            this.selectedDateLabel.Size = new System.Drawing.Size(168, 38);
            this.selectedDateLabel.TabIndex = 1;
            this.selectedDateLabel.Text = "2024-01-01";
            this.selectedDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // todayButton
            // 
            this.todayButton.BackColor = System.Drawing.Color.Beige;
            this.todayButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.todayButton.Location = new System.Drawing.Point(257, 55);
            this.todayButton.Margin = new System.Windows.Forms.Padding(10);
            this.todayButton.Name = "todayButton";
            this.todayButton.Size = new System.Drawing.Size(168, 59);
            this.todayButton.TabIndex = 5;
            this.todayButton.Text = "Today";
            this.todayButton.UseVisualStyleBackColor = false;
            this.todayButton.Click += new System.EventHandler(this.todayButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.readAlarmButton);
            this.panel1.Controls.Add(this.clearAlarmButton);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.endDateTimePicker);
            this.panel1.Controls.Add(this.startDateTimePicker);
            this.panel1.Location = new System.Drawing.Point(726, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 150);
            this.panel1.TabIndex = 6;
            // 
            // readAlarmButton
            // 
            this.readAlarmButton.Location = new System.Drawing.Point(145, 75);
            this.readAlarmButton.Name = "readAlarmButton";
            this.readAlarmButton.Size = new System.Drawing.Size(131, 67);
            this.readAlarmButton.TabIndex = 5;
            this.readAlarmButton.Text = "Read\r\nAlarm";
            this.readAlarmButton.UseVisualStyleBackColor = true;
            this.readAlarmButton.Click += new System.EventHandler(this.readAlarmButton_Click);
            // 
            // clearAlarmButton
            // 
            this.clearAlarmButton.Location = new System.Drawing.Point(8, 75);
            this.clearAlarmButton.Name = "clearAlarmButton";
            this.clearAlarmButton.Size = new System.Drawing.Size(131, 67);
            this.clearAlarmButton.TabIndex = 4;
            this.clearAlarmButton.Text = "Clear\r\nAlarm";
            this.clearAlarmButton.UseVisualStyleBackColor = true;
            this.clearAlarmButton.Click += new System.EventHandler(this.clearAlarmButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "End Date :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Start Date :";
            // 
            // endDateTimePicker
            // 
            this.endDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDateTimePicker.Location = new System.Drawing.Point(135, 41);
            this.endDateTimePicker.Name = "endDateTimePicker";
            this.endDateTimePicker.Size = new System.Drawing.Size(141, 29);
            this.endDateTimePicker.TabIndex = 1;
            // 
            // startDateTimePicker
            // 
            this.startDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDateTimePicker.Location = new System.Drawing.Point(135, 6);
            this.startDateTimePicker.Name = "startDateTimePicker";
            this.startDateTimePicker.Size = new System.Drawing.Size(141, 29);
            this.startDateTimePicker.TabIndex = 0;
            // 
            // ui_timer
            // 
            this.ui_timer.Enabled = true;
            this.ui_timer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.LightGray;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(20, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(495, 30);
            this.label4.TabIndex = 11;
            this.label4.Text = "Error Time List";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.LightGray;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(515, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(495, 30);
            this.label5.TabIndex = 12;
            this.label5.Text = "Error Count";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // countGrid
            // 
            this.countGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.countGrid.EnableSort = true;
            this.countGrid.Font = new System.Drawing.Font("SamsungOne 800C", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countGrid.Location = new System.Drawing.Point(515, 196);
            this.countGrid.Margin = new System.Windows.Forms.Padding(0);
            this.countGrid.Name = "countGrid";
            this.countGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.countGrid.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.countGrid.Size = new System.Drawing.Size(495, 422);
            this.countGrid.TabIndex = 10;
            this.countGrid.TabStop = true;
            this.countGrid.ToolTipText = "";
            // 
            // grid
            // 
            this.grid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.grid.EnableSort = true;
            this.grid.Font = new System.Drawing.Font("SamsungOne 800C", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grid.Location = new System.Drawing.Point(20, 196);
            this.grid.Margin = new System.Windows.Forms.Padding(4);
            this.grid.Name = "grid";
            this.grid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.grid.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.grid.Size = new System.Drawing.Size(495, 422);
            this.grid.TabIndex = 3;
            this.grid.TabStop = true;
            this.grid.ToolTipText = "";
            // 
            // FormLog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 628);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.countGrid);
            this.Controls.Add(this.todayButton);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.calendar);
            this.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormLog";
            this.Padding = new System.Windows.Forms.Padding(20, 5, 20, 25);
            this.Text = "FromLog";
            this.VisibleChanged += new System.EventHandler(this.FormLog_VisibleChanged);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.MonthCalendar calendar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer ui_timer;
        private System.Windows.Forms.Button todayButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button readAlarmButton;
        private System.Windows.Forms.Button clearAlarmButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker endDateTimePicker;
        private System.Windows.Forms.DateTimePicker startDateTimePicker;
        private CSourceGrid countGrid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label selectedDateLabel;
        private CSourceGrid grid;
    }
}