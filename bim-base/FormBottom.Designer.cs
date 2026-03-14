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
            this.exitButton = new System.Windows.Forms.Button();
            this.hideButton = new System.Windows.Forms.Button();
            this.logButton = new System.Windows.Forms.Button();
            this.databutton = new System.Windows.Forms.Button();
            this.teachButton = new System.Windows.Forms.Button();
            this.autoButton = new System.Windows.Forms.Button();
            this.historyList = new System.Windows.Forms.ListBox();
            this.ui_timer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.exitButton, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.hideButton, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.logButton, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.databutton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.teachButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.autoButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.historyList, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1024, 70);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // exitButton
            // 
            this.exitButton.BackgroundImage = global::bim_base.Properties.Resources.btn_Exit;
            this.exitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.exitButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.Location = new System.Drawing.Point(926, 0);
            this.exitButton.Margin = new System.Windows.Forms.Padding(0);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(98, 70);
            this.exitButton.TabIndex = 7;
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // hideButton
            // 
            this.hideButton.BackgroundImage = global::bim_base.Properties.Resources.btn_hide;
            this.hideButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.hideButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hideButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hideButton.Location = new System.Drawing.Point(828, 0);
            this.hideButton.Margin = new System.Windows.Forms.Padding(0);
            this.hideButton.Name = "hideButton";
            this.hideButton.Size = new System.Drawing.Size(98, 70);
            this.hideButton.TabIndex = 5;
            this.hideButton.UseVisualStyleBackColor = true;
            this.hideButton.Click += new System.EventHandler(this.hideButton_Click);
            // 
            // logButton
            // 
            this.logButton.BackgroundImage = global::bim_base.Properties.Resources.btn_log_enable;
            this.logButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logButton.Location = new System.Drawing.Point(294, 0);
            this.logButton.Margin = new System.Windows.Forms.Padding(0);
            this.logButton.Name = "logButton";
            this.logButton.Size = new System.Drawing.Size(98, 70);
            this.logButton.TabIndex = 3;
            this.logButton.UseVisualStyleBackColor = true;
            // 
            // databutton
            // 
            this.databutton.BackgroundImage = global::bim_base.Properties.Resources.btn_data_enable;
            this.databutton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.databutton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.databutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.databutton.Location = new System.Drawing.Point(196, 0);
            this.databutton.Margin = new System.Windows.Forms.Padding(0);
            this.databutton.Name = "databutton";
            this.databutton.Size = new System.Drawing.Size(98, 70);
            this.databutton.TabIndex = 2;
            this.databutton.UseVisualStyleBackColor = true;
            // 
            // teachButton
            // 
            this.teachButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("teachButton.BackgroundImage")));
            this.teachButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.teachButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.teachButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.teachButton.Location = new System.Drawing.Point(98, 0);
            this.teachButton.Margin = new System.Windows.Forms.Padding(0);
            this.teachButton.Name = "teachButton";
            this.teachButton.Size = new System.Drawing.Size(98, 70);
            this.teachButton.TabIndex = 1;
            this.teachButton.UseVisualStyleBackColor = true;
            // 
            // autoButton
            // 
            this.autoButton.BackColor = System.Drawing.Color.Transparent;
            this.autoButton.BackgroundImage = global::bim_base.Properties.Resources.btn_auto_enable;
            this.autoButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.autoButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.autoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.autoButton.Location = new System.Drawing.Point(0, 0);
            this.autoButton.Margin = new System.Windows.Forms.Padding(0);
            this.autoButton.Name = "autoButton";
            this.autoButton.Size = new System.Drawing.Size(98, 70);
            this.autoButton.TabIndex = 0;
            this.autoButton.UseVisualStyleBackColor = false;
            // 
            // historyList
            // 
            this.historyList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.historyList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.historyList.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.historyList.FormattingEnabled = true;
            this.historyList.ItemHeight = 21;
            this.historyList.Location = new System.Drawing.Point(392, 0);
            this.historyList.Margin = new System.Windows.Forms.Padding(0);
            this.historyList.Name = "historyList";
            this.historyList.Size = new System.Drawing.Size(436, 70);
            this.historyList.TabIndex = 8;
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
            this.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormBottom";
            this.Text = "FormBottom";
            this.Load += new System.EventHandler(this.FormBottom_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button autoButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button hideButton;
        private System.Windows.Forms.Button logButton;
        private System.Windows.Forms.Button databutton;
        private System.Windows.Forms.Button teachButton;
        private System.Windows.Forms.ListBox historyList;
        private System.Windows.Forms.Timer ui_timer;
    }
}