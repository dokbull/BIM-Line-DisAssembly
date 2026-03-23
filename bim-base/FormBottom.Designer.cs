namespace bim_base
{
    partial class FormBottom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBottom));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.BT_EXIT = new SUserControls.ColorButton();
            this.BT_HIDE = new SUserControls.ColorButton();
            this.BT_ALARM = new SUserControls.ColorButton();
            this.BT_MONITOR = new SUserControls.ColorButton();
            this.BT_DATA = new SUserControls.ColorButton();
            this.BT_TEACH = new SUserControls.ColorButton();
            this.BT_MANUAL = new SUserControls.ColorButton();
            this.BT_AUTO = new SUserControls.ColorButton();
            this.historyList = new System.Windows.Forms.ListBox();
            this.ui_timer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.Controls.Add(this.BT_EXIT, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.BT_HIDE, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.BT_ALARM, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.BT_MONITOR, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.BT_DATA, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.BT_TEACH, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.BT_MANUAL, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.BT_AUTO, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.historyList, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1024, 70);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // BT_EXIT
            // 
            this.BT_EXIT.BackColor = System.Drawing.Color.Transparent;
            this.BT_EXIT.BorderLineColor = System.Drawing.Color.Black;
            this.BT_EXIT.BorderThickness = 0F;
            this.BT_EXIT.Checked = false;
            this.BT_EXIT.CheckedButtonColor = System.Drawing.Color.Black;
            this.BT_EXIT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BT_EXIT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.BT_EXIT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange;
            this.BT_EXIT.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_EXIT.ForeColor = System.Drawing.Color.Black;
            this.BT_EXIT.GradientBottom = System.Drawing.Color.White;
            this.BT_EXIT.GradientTop = System.Drawing.Color.White;
            this.BT_EXIT.Image = ((System.Drawing.Image)(resources.GetObject("BT_EXIT.Image")));
            this.BT_EXIT.Location = new System.Drawing.Point(929, 3);
            this.BT_EXIT.Name = "BT_EXIT";
            this.BT_EXIT.RectCornerRadius = 2;
            this.BT_EXIT.Size = new System.Drawing.Size(92, 64);
            this.BT_EXIT.TabIndex = 1153;
            this.BT_EXIT.Text = "Exit";
            this.BT_EXIT.UseVisualStyleBackColor = false;
            this.BT_EXIT.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // BT_HIDE
            // 
            this.BT_HIDE.BackColor = System.Drawing.Color.Transparent;
            this.BT_HIDE.BorderLineColor = System.Drawing.Color.Black;
            this.BT_HIDE.BorderThickness = 0F;
            this.BT_HIDE.Checked = false;
            this.BT_HIDE.CheckedButtonColor = System.Drawing.Color.Black;
            this.BT_HIDE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BT_HIDE.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.BT_HIDE.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange;
            this.BT_HIDE.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_HIDE.ForeColor = System.Drawing.Color.Black;
            this.BT_HIDE.GradientBottom = System.Drawing.Color.White;
            this.BT_HIDE.GradientTop = System.Drawing.Color.White;
            this.BT_HIDE.Location = new System.Drawing.Point(831, 3);
            this.BT_HIDE.Name = "BT_HIDE";
            this.BT_HIDE.RectCornerRadius = 2;
            this.BT_HIDE.Size = new System.Drawing.Size(92, 64);
            this.BT_HIDE.TabIndex = 1152;
            this.BT_HIDE.Text = "Hide";
            this.BT_HIDE.UseVisualStyleBackColor = false;
            this.BT_HIDE.Click += new System.EventHandler(this.hideButton_Click);
            // 
            // BT_ALARM
            // 
            this.BT_ALARM.BackColor = System.Drawing.Color.Transparent;
            this.BT_ALARM.BorderLineColor = System.Drawing.Color.Black;
            this.BT_ALARM.BorderThickness = 0F;
            this.BT_ALARM.Checked = false;
            this.BT_ALARM.CheckedButtonColor = System.Drawing.Color.Black;
            this.BT_ALARM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BT_ALARM.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.BT_ALARM.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange;
            this.BT_ALARM.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_ALARM.ForeColor = System.Drawing.Color.Black;
            this.BT_ALARM.GradientBottom = System.Drawing.Color.White;
            this.BT_ALARM.GradientTop = System.Drawing.Color.White;
            this.BT_ALARM.Image = ((System.Drawing.Image)(resources.GetObject("BT_ALARM.Image")));
            this.BT_ALARM.Location = new System.Drawing.Point(733, 3);
            this.BT_ALARM.Name = "BT_ALARM";
            this.BT_ALARM.RectCornerRadius = 2;
            this.BT_ALARM.Size = new System.Drawing.Size(92, 64);
            this.BT_ALARM.TabIndex = 1151;
            this.BT_ALARM.Text = "Alarm";
            this.BT_ALARM.UseVisualStyleBackColor = false;
            this.BT_ALARM.Click += new System.EventHandler(this.BT_ALARM_Click);
            // 
            // BT_MONITOR
            // 
            this.BT_MONITOR.BackColor = System.Drawing.Color.Transparent;
            this.BT_MONITOR.BorderLineColor = System.Drawing.Color.Black;
            this.BT_MONITOR.BorderThickness = 0F;
            this.BT_MONITOR.Checked = false;
            this.BT_MONITOR.CheckedButtonColor = System.Drawing.Color.Black;
            this.BT_MONITOR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BT_MONITOR.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.BT_MONITOR.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange;
            this.BT_MONITOR.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_MONITOR.ForeColor = System.Drawing.Color.Black;
            this.BT_MONITOR.GradientBottom = System.Drawing.Color.White;
            this.BT_MONITOR.GradientTop = System.Drawing.Color.White;
            this.BT_MONITOR.Image = ((System.Drawing.Image)(resources.GetObject("BT_MONITOR.Image")));
            this.BT_MONITOR.Location = new System.Drawing.Point(395, 3);
            this.BT_MONITOR.Name = "BT_MONITOR";
            this.BT_MONITOR.RectCornerRadius = 2;
            this.BT_MONITOR.Size = new System.Drawing.Size(92, 64);
            this.BT_MONITOR.TabIndex = 1150;
            this.BT_MONITOR.Text = "Monitor";
            this.BT_MONITOR.UseVisualStyleBackColor = false;
            // 
            // BT_DATA
            // 
            this.BT_DATA.BackColor = System.Drawing.Color.Transparent;
            this.BT_DATA.BorderLineColor = System.Drawing.Color.Black;
            this.BT_DATA.BorderThickness = 0F;
            this.BT_DATA.Checked = false;
            this.BT_DATA.CheckedButtonColor = System.Drawing.Color.Black;
            this.BT_DATA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BT_DATA.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.BT_DATA.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange;
            this.BT_DATA.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_DATA.ForeColor = System.Drawing.Color.Black;
            this.BT_DATA.GradientBottom = System.Drawing.Color.White;
            this.BT_DATA.GradientTop = System.Drawing.Color.White;
            this.BT_DATA.Image = ((System.Drawing.Image)(resources.GetObject("BT_DATA.Image")));
            this.BT_DATA.Location = new System.Drawing.Point(297, 3);
            this.BT_DATA.Name = "BT_DATA";
            this.BT_DATA.RectCornerRadius = 2;
            this.BT_DATA.Size = new System.Drawing.Size(92, 64);
            this.BT_DATA.TabIndex = 1149;
            this.BT_DATA.Text = "Data";
            this.BT_DATA.UseVisualStyleBackColor = false;
            this.BT_DATA.Click += new System.EventHandler(this.BT_DATA_Click);
            // 
            // BT_TEACH
            // 
            this.BT_TEACH.BackColor = System.Drawing.Color.Transparent;
            this.BT_TEACH.BorderLineColor = System.Drawing.Color.Black;
            this.BT_TEACH.BorderThickness = 0F;
            this.BT_TEACH.Checked = false;
            this.BT_TEACH.CheckedButtonColor = System.Drawing.Color.Black;
            this.BT_TEACH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BT_TEACH.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.BT_TEACH.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange;
            this.BT_TEACH.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_TEACH.ForeColor = System.Drawing.Color.Black;
            this.BT_TEACH.GradientBottom = System.Drawing.Color.White;
            this.BT_TEACH.GradientTop = System.Drawing.Color.White;
            this.BT_TEACH.Image = ((System.Drawing.Image)(resources.GetObject("BT_TEACH.Image")));
            this.BT_TEACH.Location = new System.Drawing.Point(199, 3);
            this.BT_TEACH.Name = "BT_TEACH";
            this.BT_TEACH.RectCornerRadius = 2;
            this.BT_TEACH.Size = new System.Drawing.Size(92, 64);
            this.BT_TEACH.TabIndex = 1148;
            this.BT_TEACH.Text = "Teach";
            this.BT_TEACH.UseVisualStyleBackColor = false;
            // 
            // BT_MANUAL
            // 
            this.BT_MANUAL.BackColor = System.Drawing.Color.Transparent;
            this.BT_MANUAL.BorderLineColor = System.Drawing.Color.Black;
            this.BT_MANUAL.BorderThickness = 0F;
            this.BT_MANUAL.Checked = false;
            this.BT_MANUAL.CheckedButtonColor = System.Drawing.Color.Black;
            this.BT_MANUAL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BT_MANUAL.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.BT_MANUAL.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange;
            this.BT_MANUAL.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_MANUAL.ForeColor = System.Drawing.Color.Black;
            this.BT_MANUAL.GradientBottom = System.Drawing.Color.White;
            this.BT_MANUAL.GradientTop = System.Drawing.Color.White;
            this.BT_MANUAL.Image = ((System.Drawing.Image)(resources.GetObject("BT_MANUAL.Image")));
            this.BT_MANUAL.Location = new System.Drawing.Point(101, 3);
            this.BT_MANUAL.Name = "BT_MANUAL";
            this.BT_MANUAL.RectCornerRadius = 2;
            this.BT_MANUAL.Size = new System.Drawing.Size(92, 64);
            this.BT_MANUAL.TabIndex = 1147;
            this.BT_MANUAL.Text = "Manual";
            this.BT_MANUAL.UseVisualStyleBackColor = false;
            // 
            // BT_AUTO
            // 
            this.BT_AUTO.BackColor = System.Drawing.Color.Transparent;
            this.BT_AUTO.BorderLineColor = System.Drawing.Color.Black;
            this.BT_AUTO.BorderThickness = 0F;
            this.BT_AUTO.Checked = false;
            this.BT_AUTO.CheckedButtonColor = System.Drawing.Color.Black;
            this.BT_AUTO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BT_AUTO.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BT_AUTO.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BT_AUTO.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_AUTO.ForeColor = System.Drawing.Color.Black;
            this.BT_AUTO.GradientBottom = System.Drawing.Color.White;
            this.BT_AUTO.GradientTop = System.Drawing.Color.White;
            this.BT_AUTO.Image = ((System.Drawing.Image)(resources.GetObject("BT_AUTO.Image")));
            this.BT_AUTO.Location = new System.Drawing.Point(3, 3);
            this.BT_AUTO.Name = "BT_AUTO";
            this.BT_AUTO.RectCornerRadius = 2;
            this.BT_AUTO.Size = new System.Drawing.Size(92, 64);
            this.BT_AUTO.TabIndex = 1146;
            this.BT_AUTO.Text = "Auto";
            this.BT_AUTO.UseVisualStyleBackColor = false;
            // 
            // historyList
            // 
            this.historyList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.historyList.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.historyList.FormattingEnabled = true;
            this.historyList.ItemHeight = 12;
            this.historyList.Location = new System.Drawing.Point(490, 3);
            this.historyList.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.historyList.Name = "historyList";
            this.historyList.Size = new System.Drawing.Size(240, 62);
            this.historyList.TabIndex = 13;
            // 
            // ui_timer
            // 
            this.ui_timer.Tick += new System.EventHandler(this.ui_timer_Tick);
            // 
            // FormBottom
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1024, 70);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormBottom";
            this.Text = "FormBottom";
            this.Load += new System.EventHandler(this.FormBottom_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Timer ui_timer;
        public SUserControls.ColorButton BT_EXIT;
        public SUserControls.ColorButton BT_HIDE;
        public SUserControls.ColorButton BT_ALARM;
        public SUserControls.ColorButton BT_MONITOR;
        public SUserControls.ColorButton BT_DATA;
        public SUserControls.ColorButton BT_TEACH;
        public SUserControls.ColorButton BT_MANUAL;
        public SUserControls.ColorButton BT_AUTO;
        private System.Windows.Forms.ListBox historyList;
    }
}