namespace bim_base
{
    partial class FormDataSystem
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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.inCVTimeout = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.inCVDelayStopLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cvInLabel = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.gripOffLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.gripOnLabel = new System.Windows.Forms.Label();
            this.mesGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.mesModeLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mesSuffixTypeLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.mesUseLabel = new System.Windows.Forms.Label();
            this.groupBox5.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.mesGroupBox.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(488, 428);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(147, 39);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tableLayoutPanel8);
            this.groupBox5.Font = new System.Drawing.Font("SamsungOne 800C", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(324, 19);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.groupBox5.Size = new System.Drawing.Size(306, 189);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "In Convayor Time";
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.inCVTimeout, 1, 2);
            this.tableLayoutPanel8.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this.inCVDelayStopLabel, 1, 1);
            this.tableLayoutPanel8.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.cvInLabel, 1, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 31);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 3;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(300, 148);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // inCVTimeout
            // 
            this.inCVTimeout.AutoSize = true;
            this.inCVTimeout.BackColor = System.Drawing.Color.White;
            this.inCVTimeout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inCVTimeout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inCVTimeout.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inCVTimeout.Location = new System.Drawing.Point(151, 101);
            this.inCVTimeout.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.inCVTimeout.Name = "inCVTimeout";
            this.inCVTimeout.Size = new System.Drawing.Size(146, 44);
            this.inCVTimeout.TabIndex = 5;
            this.inCVTimeout.Text = "0.0 Sec";
            this.inCVTimeout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 101);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 44);
            this.label2.TabIndex = 4;
            this.label2.Text = "Timeout";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // inCVDelayStopLabel
            // 
            this.inCVDelayStopLabel.AutoSize = true;
            this.inCVDelayStopLabel.BackColor = System.Drawing.Color.White;
            this.inCVDelayStopLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inCVDelayStopLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inCVDelayStopLabel.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inCVDelayStopLabel.Location = new System.Drawing.Point(151, 52);
            this.inCVDelayStopLabel.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.inCVDelayStopLabel.Name = "inCVDelayStopLabel";
            this.inCVDelayStopLabel.Size = new System.Drawing.Size(146, 43);
            this.inCVDelayStopLabel.TabIndex = 3;
            this.inCVDelayStopLabel.Text = "0.0 Sec";
            this.inCVDelayStopLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 52);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 43);
            this.label1.TabIndex = 2;
            this.label1.Text = "I/F Off Delay";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(3, 3);
            this.label13.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(146, 43);
            this.label13.TabIndex = 0;
            this.label13.Text = "More Run Time";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cvInLabel
            // 
            this.cvInLabel.AutoSize = true;
            this.cvInLabel.BackColor = System.Drawing.Color.White;
            this.cvInLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cvInLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cvInLabel.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cvInLabel.Location = new System.Drawing.Point(151, 3);
            this.cvInLabel.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.cvInLabel.Name = "cvInLabel";
            this.cvInLabel.Size = new System.Drawing.Size(146, 43);
            this.cvInLabel.TabIndex = 1;
            this.cvInLabel.Text = "0.0 Sec";
            this.cvInLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.tableLayoutPanel6);
            this.groupBox7.Font = new System.Drawing.Font("SamsungOne 800C", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(14, 19);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(5, 10, 5, 50);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(302, 133);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Grip Time";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.gripOffLabel, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.gripOnLabel, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 31);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(296, 99);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // gripOffLabel
            // 
            this.gripOffLabel.AutoSize = true;
            this.gripOffLabel.BackColor = System.Drawing.Color.White;
            this.gripOffLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gripOffLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gripOffLabel.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gripOffLabel.Location = new System.Drawing.Point(149, 52);
            this.gripOffLabel.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.gripOffLabel.Name = "gripOffLabel";
            this.gripOffLabel.Size = new System.Drawing.Size(144, 44);
            this.gripOffLabel.TabIndex = 3;
            this.gripOffLabel.Text = "0.0 Sec";
            this.gripOffLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.gripOffLabel.Click += new System.EventHandler(this.gripOffDelay_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 52);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 44);
            this.label4.TabIndex = 2;
            this.label4.Text = "Off Delay";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Click += new System.EventHandler(this.gripOffDelay_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 43);
            this.label6.TabIndex = 0;
            this.label6.Text = "On Delay";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label6.Click += new System.EventHandler(this.gripOnDelay_Click);
            // 
            // gripOnLabel
            // 
            this.gripOnLabel.AutoSize = true;
            this.gripOnLabel.BackColor = System.Drawing.Color.White;
            this.gripOnLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gripOnLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gripOnLabel.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gripOnLabel.Location = new System.Drawing.Point(149, 3);
            this.gripOnLabel.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.gripOnLabel.Name = "gripOnLabel";
            this.gripOnLabel.Size = new System.Drawing.Size(144, 43);
            this.gripOnLabel.TabIndex = 1;
            this.gripOnLabel.Text = "0.0 Sec";
            this.gripOnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.gripOnLabel.Click += new System.EventHandler(this.gripOnDelay_Click);
            // 
            // mesGroupBox
            // 
            this.mesGroupBox.Controls.Add(this.tableLayoutPanel12);
            this.mesGroupBox.Font = new System.Drawing.Font("SamsungOne 800", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mesGroupBox.Location = new System.Drawing.Point(13, 162);
            this.mesGroupBox.Margin = new System.Windows.Forms.Padding(5, 10, 5, 50);
            this.mesGroupBox.Name = "mesGroupBox";
            this.mesGroupBox.Padding = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.mesGroupBox.Size = new System.Drawing.Size(303, 170);
            this.mesGroupBox.TabIndex = 10;
            this.mesGroupBox.TabStop = false;
            this.mesGroupBox.Text = "MES";
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 2;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.Controls.Add(this.mesModeLabel, 1, 1);
            this.tableLayoutPanel12.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel12.Controls.Add(this.mesSuffixTypeLabel, 1, 2);
            this.tableLayoutPanel12.Controls.Add(this.label8, 0, 2);
            this.tableLayoutPanel12.Controls.Add(this.label32, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.mesUseLabel, 1, 0);
            this.tableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel12.Font = new System.Drawing.Font("SamsungOne 800", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel12.Location = new System.Drawing.Point(3, 31);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 3;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(297, 129);
            this.tableLayoutPanel12.TabIndex = 0;
            // 
            // mesModeLabel
            // 
            this.mesModeLabel.AutoSize = true;
            this.mesModeLabel.BackColor = System.Drawing.Color.White;
            this.mesModeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mesModeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mesModeLabel.Location = new System.Drawing.Point(149, 46);
            this.mesModeLabel.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.mesModeLabel.Name = "mesModeLabel";
            this.mesModeLabel.Size = new System.Drawing.Size(145, 37);
            this.mesModeLabel.TabIndex = 7;
            this.mesModeLabel.Text = "NETWORK";
            this.mesModeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mesModeLabel.Click += new System.EventHandler(this.mesModeLabel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 46);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(144, 37);
            this.label5.TabIndex = 6;
            this.label5.Text = "MODE";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.Click += new System.EventHandler(this.mesModeLabel_Click);
            // 
            // mesSuffixTypeLabel
            // 
            this.mesSuffixTypeLabel.AutoSize = true;
            this.mesSuffixTypeLabel.BackColor = System.Drawing.Color.White;
            this.mesSuffixTypeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mesSuffixTypeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mesSuffixTypeLabel.Location = new System.Drawing.Point(149, 89);
            this.mesSuffixTypeLabel.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.mesSuffixTypeLabel.Name = "mesSuffixTypeLabel";
            this.mesSuffixTypeLabel.Size = new System.Drawing.Size(145, 37);
            this.mesSuffixTypeLabel.TabIndex = 5;
            this.mesSuffixTypeLabel.Text = "CRLF";
            this.mesSuffixTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mesSuffixTypeLabel.Click += new System.EventHandler(this.mesSuffixTypeLabel_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 89);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(144, 37);
            this.label8.TabIndex = 4;
            this.label8.Text = "suffix type";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label8.Click += new System.EventHandler(this.mesSuffixTypeLabel_Click);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label32.Location = new System.Drawing.Point(3, 3);
            this.label32.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(144, 37);
            this.label32.TabIndex = 0;
            this.label32.Text = "USE";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label32.Click += new System.EventHandler(this.mesUseLabel_Click);
            // 
            // mesUseLabel
            // 
            this.mesUseLabel.AutoSize = true;
            this.mesUseLabel.BackColor = System.Drawing.Color.White;
            this.mesUseLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mesUseLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mesUseLabel.Location = new System.Drawing.Point(149, 3);
            this.mesUseLabel.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.mesUseLabel.Name = "mesUseLabel";
            this.mesUseLabel.Size = new System.Drawing.Size(145, 37);
            this.mesUseLabel.TabIndex = 1;
            this.mesUseLabel.Text = "Not Use";
            this.mesUseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mesUseLabel.Click += new System.EventHandler(this.mesUseLabel_Click);
            // 
            // FormDataSystem
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(647, 479);
            this.ControlBox = false;
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.mesGroupBox);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox7);
            this.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDataSystem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormSubSystemManager";
            this.Load += new System.EventHandler(this.FormSubSystemManager_Load);
            this.groupBox5.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.mesGroupBox.ResumeLayout(false);
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label cvInLabel;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label gripOffLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label gripOnLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label inCVDelayStopLabel;
        private System.Windows.Forms.Label inCVTimeout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox mesGroupBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        private System.Windows.Forms.Label mesModeLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label mesSuffixTypeLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label mesUseLabel;
    }
}