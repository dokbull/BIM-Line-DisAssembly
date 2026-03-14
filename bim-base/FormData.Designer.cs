namespace bim_base
{
    partial class FormData
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
            this.swLimitLabel = new System.Windows.Forms.Label();
            this.modelButton = new System.Windows.Forms.Button();
            this.systemManagerButton = new System.Windows.Forms.Button();
            this.motorVelButton = new System.Windows.Forms.Button();
            this.jogVelButton = new System.Windows.Forms.Button();
            this.portSettingButton = new System.Windows.Forms.Button();
            this.ui_timer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.Controls.Add(this.swLimitLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.modelButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.systemManagerButton, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.motorVelButton, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.jogVelButton, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.portSettingButton, 3, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.15287F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 31.84713F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 31.84713F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.15287F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(984, 588);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // swLimitLabel
            // 
            this.swLimitLabel.AutoSize = true;
            this.swLimitLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.swLimitLabel.Location = new System.Drawing.Point(3, 0);
            this.swLimitLabel.Name = "swLimitLabel";
            this.swLimitLabel.Size = new System.Drawing.Size(117, 106);
            this.swLimitLabel.TabIndex = 8;
            this.swLimitLabel.Click += new System.EventHandler(this.swLimitLabel_Click);
            // 
            // modelButton
            // 
            this.modelButton.BackColor = System.Drawing.Color.White;
            this.modelButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.modelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.modelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modelButton.Location = new System.Drawing.Point(130, 113);
            this.modelButton.Margin = new System.Windows.Forms.Padding(7);
            this.modelButton.Name = "modelButton";
            this.modelButton.Size = new System.Drawing.Size(232, 173);
            this.modelButton.TabIndex = 0;
            this.modelButton.Text = "Model";
            this.modelButton.UseVisualStyleBackColor = false;
            this.modelButton.Click += new System.EventHandler(this.modelButton_Click);
            // 
            // systemManagerButton
            // 
            this.systemManagerButton.BackColor = System.Drawing.Color.White;
            this.systemManagerButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.systemManagerButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.systemManagerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.systemManagerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.systemManagerButton.Location = new System.Drawing.Point(376, 113);
            this.systemManagerButton.Margin = new System.Windows.Forms.Padding(7);
            this.systemManagerButton.Name = "systemManagerButton";
            this.systemManagerButton.Size = new System.Drawing.Size(232, 173);
            this.systemManagerButton.TabIndex = 1;
            this.systemManagerButton.Text = "System\r\nManager";
            this.systemManagerButton.UseVisualStyleBackColor = false;
            this.systemManagerButton.Click += new System.EventHandler(this.systemManagerButton_Click);
            // 
            // motorVelButton
            // 
            this.motorVelButton.BackColor = System.Drawing.Color.White;
            this.motorVelButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.motorVelButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.motorVelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.motorVelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.motorVelButton.Location = new System.Drawing.Point(130, 300);
            this.motorVelButton.Margin = new System.Windows.Forms.Padding(7);
            this.motorVelButton.Name = "motorVelButton";
            this.motorVelButton.Size = new System.Drawing.Size(232, 173);
            this.motorVelButton.TabIndex = 2;
            this.motorVelButton.Text = "Motor\r\nVelocity";
            this.motorVelButton.UseVisualStyleBackColor = false;
            this.motorVelButton.Click += new System.EventHandler(this.motorVelButton_Click);
            // 
            // jogVelButton
            // 
            this.jogVelButton.BackColor = System.Drawing.Color.White;
            this.jogVelButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jogVelButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.jogVelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.jogVelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jogVelButton.Location = new System.Drawing.Point(376, 300);
            this.jogVelButton.Margin = new System.Windows.Forms.Padding(7);
            this.jogVelButton.Name = "jogVelButton";
            this.jogVelButton.Size = new System.Drawing.Size(232, 173);
            this.jogVelButton.TabIndex = 3;
            this.jogVelButton.Text = "Jog\r\nVelocity";
            this.jogVelButton.UseVisualStyleBackColor = false;
            this.jogVelButton.Visible = false;
            this.jogVelButton.Click += new System.EventHandler(this.jogVelButton_Click);
            // 
            // portSettingButton
            // 
            this.portSettingButton.BackColor = System.Drawing.Color.White;
            this.portSettingButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.portSettingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.portSettingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.portSettingButton.Location = new System.Drawing.Point(622, 300);
            this.portSettingButton.Margin = new System.Windows.Forms.Padding(7);
            this.portSettingButton.Name = "portSettingButton";
            this.portSettingButton.Size = new System.Drawing.Size(232, 173);
            this.portSettingButton.TabIndex = 6;
            this.portSettingButton.Text = "Port\r\nSetting";
            this.portSettingButton.UseVisualStyleBackColor = false;
            this.portSettingButton.Click += new System.EventHandler(this.portSettingButton_Click);
            // 
            // ui_timer
            // 
            this.ui_timer.Enabled = true;
            this.ui_timer.Tick += new System.EventHandler(this.ui_timer_Tick);
            // 
            // FormData
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 628);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormData";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Text = "FormCenter2";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button modelButton;
        private System.Windows.Forms.Button systemManagerButton;
        private System.Windows.Forms.Button motorVelButton;
        private System.Windows.Forms.Button jogVelButton;
        private System.Windows.Forms.Button portSettingButton;
        private System.Windows.Forms.Label swLimitLabel;
        private System.Windows.Forms.Timer ui_timer;
    }
}