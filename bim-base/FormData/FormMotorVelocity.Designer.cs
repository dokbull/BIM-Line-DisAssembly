namespace bim_base
{
    partial class FormMotorVelocity
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
            this.saveButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.InTransferZVelLabel = new System.Windows.Forms.Label();
            this.OutTransferZVelLabel = new System.Windows.Forms.Label();
            this.UnloadYVelLabel = new System.Windows.Forms.Label();
            this.InTransferZAccLabel = new System.Windows.Forms.Label();
            this.OutTransferZAccLabel = new System.Windows.Forms.Label();
            this.UnloadYAccLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.loadButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(305, 416);
            this.saveButton.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(149, 73);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.Location = new System.Drawing.Point(468, 416);
            this.exitButton.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(149, 73);
            this.exitButton.TabIndex = 2;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click_1);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.InTransferZVelLabel, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.OutTransferZVelLabel, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.UnloadYVelLabel, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.InTransferZAccLabel, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.OutTransferZAccLabel, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.UnloadYAccLabel, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(7, 7);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(616, 386);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 92);
            this.label1.TabIndex = 0;
            this.label1.Text = "AXIS NAME";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(207, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 92);
            this.label2.TabIndex = 1;
            this.label2.Text = "Velocity\r\n(mm/sec)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Gainsboro;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(408, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(202, 92);
            this.label3.TabIndex = 2;
            this.label3.Text = "Acc/Dec\r\n(ms)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Gainsboro;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(6, 99);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 92);
            this.label5.TabIndex = 4;
            this.label5.Text = "IN TRANSFER Z";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Gainsboro;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(6, 192);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(200, 92);
            this.label6.TabIndex = 5;
            this.label6.Text = "OUT TRANSFER Z";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InTransferZVelLabel
            // 
            this.InTransferZVelLabel.AutoSize = true;
            this.InTransferZVelLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InTransferZVelLabel.Location = new System.Drawing.Point(207, 99);
            this.InTransferZVelLabel.Margin = new System.Windows.Forms.Padding(0);
            this.InTransferZVelLabel.Name = "InTransferZVelLabel";
            this.InTransferZVelLabel.Size = new System.Drawing.Size(200, 92);
            this.InTransferZVelLabel.TabIndex = 8;
            this.InTransferZVelLabel.Tag = "VEL";
            this.InTransferZVelLabel.Text = "0.00";
            this.InTransferZVelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.InTransferZVelLabel.Click += new System.EventHandler(this.loaderLabel_Click);
            // 
            // OutTransferZVelLabel
            // 
            this.OutTransferZVelLabel.AutoSize = true;
            this.OutTransferZVelLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutTransferZVelLabel.Location = new System.Drawing.Point(207, 192);
            this.OutTransferZVelLabel.Margin = new System.Windows.Forms.Padding(0);
            this.OutTransferZVelLabel.Name = "OutTransferZVelLabel";
            this.OutTransferZVelLabel.Size = new System.Drawing.Size(200, 92);
            this.OutTransferZVelLabel.TabIndex = 9;
            this.OutTransferZVelLabel.Tag = "VEL";
            this.OutTransferZVelLabel.Text = "0.00";
            this.OutTransferZVelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.OutTransferZVelLabel.Click += new System.EventHandler(this.loaderLabel_Click);
            // 
            // UnloadYVelLabel
            // 
            this.UnloadYVelLabel.AutoSize = true;
            this.UnloadYVelLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UnloadYVelLabel.Location = new System.Drawing.Point(207, 285);
            this.UnloadYVelLabel.Margin = new System.Windows.Forms.Padding(0);
            this.UnloadYVelLabel.Name = "UnloadYVelLabel";
            this.UnloadYVelLabel.Size = new System.Drawing.Size(200, 95);
            this.UnloadYVelLabel.TabIndex = 10;
            this.UnloadYVelLabel.Tag = "VEL";
            this.UnloadYVelLabel.Text = "0.00";
            this.UnloadYVelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.UnloadYVelLabel.Click += new System.EventHandler(this.loaderLabel_Click);
            // 
            // InTransferZAccLabel
            // 
            this.InTransferZAccLabel.AutoSize = true;
            this.InTransferZAccLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InTransferZAccLabel.Location = new System.Drawing.Point(408, 99);
            this.InTransferZAccLabel.Margin = new System.Windows.Forms.Padding(0);
            this.InTransferZAccLabel.Name = "InTransferZAccLabel";
            this.InTransferZAccLabel.Size = new System.Drawing.Size(202, 92);
            this.InTransferZAccLabel.TabIndex = 12;
            this.InTransferZAccLabel.Tag = "ACC";
            this.InTransferZAccLabel.Text = "0.00";
            this.InTransferZAccLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.InTransferZAccLabel.Click += new System.EventHandler(this.loaderLabel_Click);
            // 
            // OutTransferZAccLabel
            // 
            this.OutTransferZAccLabel.AutoSize = true;
            this.OutTransferZAccLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutTransferZAccLabel.Location = new System.Drawing.Point(408, 192);
            this.OutTransferZAccLabel.Margin = new System.Windows.Forms.Padding(0);
            this.OutTransferZAccLabel.Name = "OutTransferZAccLabel";
            this.OutTransferZAccLabel.Size = new System.Drawing.Size(202, 92);
            this.OutTransferZAccLabel.TabIndex = 13;
            this.OutTransferZAccLabel.Tag = "ACC";
            this.OutTransferZAccLabel.Text = "0.00";
            this.OutTransferZAccLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.OutTransferZAccLabel.Click += new System.EventHandler(this.loaderLabel_Click);
            // 
            // UnloadYAccLabel
            // 
            this.UnloadYAccLabel.AutoSize = true;
            this.UnloadYAccLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UnloadYAccLabel.Location = new System.Drawing.Point(408, 285);
            this.UnloadYAccLabel.Margin = new System.Windows.Forms.Padding(0);
            this.UnloadYAccLabel.Name = "UnloadYAccLabel";
            this.UnloadYAccLabel.Size = new System.Drawing.Size(202, 95);
            this.UnloadYAccLabel.TabIndex = 14;
            this.UnloadYAccLabel.Tag = "ACC";
            this.UnloadYAccLabel.Text = "0.00";
            this.UnloadYAccLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.UnloadYAccLabel.Click += new System.EventHandler(this.loaderLabel_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Gainsboro;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(6, 285);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(200, 95);
            this.label7.TabIndex = 6;
            this.label7.Text = "OUT CONVEYOR Y";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // loadButton
            // 
            this.loadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadButton.Location = new System.Drawing.Point(142, 416);
            this.loadButton.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(149, 73);
            this.loadButton.TabIndex = 1;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // FormMotorVelocity
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(628, 512);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.loadButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMotorVelocity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Motor Velocity";
            this.Load += new System.EventHandler(this.FormSubMotorVelocity_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label InTransferZVelLabel;
        private System.Windows.Forms.Label OutTransferZVelLabel;
        private System.Windows.Forms.Label UnloadYVelLabel;
        private System.Windows.Forms.Label InTransferZAccLabel;
        private System.Windows.Forms.Label OutTransferZAccLabel;
        private System.Windows.Forms.Label UnloadYAccLabel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button loadButton;
    }
}