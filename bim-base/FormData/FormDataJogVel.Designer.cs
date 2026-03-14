namespace bim_base
{
    partial class FormDataJogVelocity
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.saveButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.xLowLabel = new System.Windows.Forms.Label();
            this.yLowLabel = new System.Windows.Forms.Label();
            this.zLowLabel = new System.Windows.Forms.Label();
            this.xMidLabel = new System.Windows.Forms.Label();
            this.yMidLabel = new System.Windows.Forms.Label();
            this.zMidLabel = new System.Windows.Forms.Label();
            this.xHighLabel = new System.Windows.Forms.Label();
            this.yHighLabel = new System.Windows.Forms.Label();
            this.zHighLabel = new System.Windows.Forms.Label();
            this.loadButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.saveButton, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.loadButton, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(109, 100);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(634, 316);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.Color.White;
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveButton.Font = new System.Drawing.Font("SamsungOne 800C", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(479, 271);
            this.saveButton.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(152, 42);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 4);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.xLowLabel, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.yLowLabel, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.zLowLabel, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.xMidLabel, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.yMidLabel, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.zMidLabel, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.xHighLabel, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.yHighLabel, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.zHighLabel, 3, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(624, 258);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 51);
            this.label1.TabIndex = 0;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(156, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 51);
            this.label2.TabIndex = 1;
            this.label2.Text = "LOW\r\n(mm/sec)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Gainsboro;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(312, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 51);
            this.label3.TabIndex = 2;
            this.label3.Text = "MIDDLE\r\n(mm/sec)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Gainsboro;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(468, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 51);
            this.label4.TabIndex = 3;
            this.label4.Text = "HIGH\r\n(mm/sec)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Gainsboro;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(0, 51);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 51);
            this.label5.TabIndex = 4;
            this.label5.Text = "PP X";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Gainsboro;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(0, 102);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(156, 51);
            this.label6.TabIndex = 5;
            this.label6.Text = "PP Y";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Gainsboro;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(0, 153);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(156, 51);
            this.label7.TabIndex = 6;
            this.label7.Text = "PP Z";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // xLowLabel
            // 
            this.xLowLabel.AutoSize = true;
            this.xLowLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.xLowLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xLowLabel.Location = new System.Drawing.Point(156, 51);
            this.xLowLabel.Margin = new System.Windows.Forms.Padding(0);
            this.xLowLabel.Name = "xLowLabel";
            this.xLowLabel.Size = new System.Drawing.Size(156, 51);
            this.xLowLabel.TabIndex = 8;
            this.xLowLabel.Tag = "LOW_X";
            this.xLowLabel.Text = "0.00";
            this.xLowLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.xLowLabel.Click += new System.EventHandler(this.loaderLabel_Click);
            // 
            // yLowLabel
            // 
            this.yLowLabel.AutoSize = true;
            this.yLowLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.yLowLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.yLowLabel.Location = new System.Drawing.Point(156, 102);
            this.yLowLabel.Margin = new System.Windows.Forms.Padding(0);
            this.yLowLabel.Name = "yLowLabel";
            this.yLowLabel.Size = new System.Drawing.Size(156, 51);
            this.yLowLabel.TabIndex = 9;
            this.yLowLabel.Tag = "LOW_Y";
            this.yLowLabel.Text = "0.00";
            this.yLowLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.yLowLabel.Click += new System.EventHandler(this.loaderLabel_Click);
            // 
            // zLowLabel
            // 
            this.zLowLabel.AutoSize = true;
            this.zLowLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.zLowLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zLowLabel.Location = new System.Drawing.Point(156, 153);
            this.zLowLabel.Margin = new System.Windows.Forms.Padding(0);
            this.zLowLabel.Name = "zLowLabel";
            this.zLowLabel.Size = new System.Drawing.Size(156, 51);
            this.zLowLabel.TabIndex = 10;
            this.zLowLabel.Tag = "LOW_Z";
            this.zLowLabel.Text = "0.00";
            this.zLowLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.zLowLabel.Click += new System.EventHandler(this.loaderLabel_Click);
            // 
            // xMidLabel
            // 
            this.xMidLabel.AutoSize = true;
            this.xMidLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.xMidLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xMidLabel.Location = new System.Drawing.Point(312, 51);
            this.xMidLabel.Margin = new System.Windows.Forms.Padding(0);
            this.xMidLabel.Name = "xMidLabel";
            this.xMidLabel.Size = new System.Drawing.Size(156, 51);
            this.xMidLabel.TabIndex = 12;
            this.xMidLabel.Tag = "MID_X";
            this.xMidLabel.Text = "0.00";
            this.xMidLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.xMidLabel.Click += new System.EventHandler(this.loaderLabel_Click);
            // 
            // yMidLabel
            // 
            this.yMidLabel.AutoSize = true;
            this.yMidLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.yMidLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.yMidLabel.Location = new System.Drawing.Point(312, 102);
            this.yMidLabel.Margin = new System.Windows.Forms.Padding(0);
            this.yMidLabel.Name = "yMidLabel";
            this.yMidLabel.Size = new System.Drawing.Size(156, 51);
            this.yMidLabel.TabIndex = 13;
            this.yMidLabel.Tag = "MID_Y";
            this.yMidLabel.Text = "0.00";
            this.yMidLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.yMidLabel.Click += new System.EventHandler(this.loaderLabel_Click);
            // 
            // zMidLabel
            // 
            this.zMidLabel.AutoSize = true;
            this.zMidLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.zMidLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zMidLabel.Location = new System.Drawing.Point(312, 153);
            this.zMidLabel.Margin = new System.Windows.Forms.Padding(0);
            this.zMidLabel.Name = "zMidLabel";
            this.zMidLabel.Size = new System.Drawing.Size(156, 51);
            this.zMidLabel.TabIndex = 14;
            this.zMidLabel.Tag = "MID_Z";
            this.zMidLabel.Text = "0.00";
            this.zMidLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.zMidLabel.Click += new System.EventHandler(this.loaderLabel_Click);
            // 
            // xHighLabel
            // 
            this.xHighLabel.AutoSize = true;
            this.xHighLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.xHighLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xHighLabel.Location = new System.Drawing.Point(468, 51);
            this.xHighLabel.Margin = new System.Windows.Forms.Padding(0);
            this.xHighLabel.Name = "xHighLabel";
            this.xHighLabel.Size = new System.Drawing.Size(156, 51);
            this.xHighLabel.TabIndex = 16;
            this.xHighLabel.Tag = "HIGH_X";
            this.xHighLabel.Text = "0.00";
            this.xHighLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.xHighLabel.Click += new System.EventHandler(this.loaderLabel_Click);
            // 
            // yHighLabel
            // 
            this.yHighLabel.AutoSize = true;
            this.yHighLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.yHighLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.yHighLabel.Location = new System.Drawing.Point(468, 102);
            this.yHighLabel.Margin = new System.Windows.Forms.Padding(0);
            this.yHighLabel.Name = "yHighLabel";
            this.yHighLabel.Size = new System.Drawing.Size(156, 51);
            this.yHighLabel.TabIndex = 17;
            this.yHighLabel.Tag = "HIGH_Y";
            this.yHighLabel.Text = "0.00";
            this.yHighLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.yHighLabel.Click += new System.EventHandler(this.loaderLabel_Click);
            // 
            // zHighLabel
            // 
            this.zHighLabel.AutoSize = true;
            this.zHighLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.zHighLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zHighLabel.Location = new System.Drawing.Point(468, 153);
            this.zHighLabel.Margin = new System.Windows.Forms.Padding(0);
            this.zHighLabel.Name = "zHighLabel";
            this.zHighLabel.Size = new System.Drawing.Size(156, 51);
            this.zHighLabel.TabIndex = 18;
            this.zHighLabel.Tag = "HIGH_Z";
            this.zHighLabel.Text = "0.00";
            this.zHighLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.zHighLabel.Click += new System.EventHandler(this.loaderLabel_Click);
            // 
            // loadButton
            // 
            this.loadButton.BackColor = System.Drawing.Color.White;
            this.loadButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadButton.Font = new System.Drawing.Font("SamsungOne 800C", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadButton.Location = new System.Drawing.Point(321, 271);
            this.loadButton.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(150, 42);
            this.loadButton.TabIndex = 1;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = false;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // FormDataJogVelocity
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(840, 524);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDataJogVelocity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormSubJogVelocity";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label xLowLabel;
        private System.Windows.Forms.Label yLowLabel;
        private System.Windows.Forms.Label xMidLabel;
        private System.Windows.Forms.Label yMidLabel;
        private System.Windows.Forms.Label xHighLabel;
        private System.Windows.Forms.Label yHighLabel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label zLowLabel;
        private System.Windows.Forms.Label zMidLabel;
        private System.Windows.Forms.Label zHighLabel;
        private System.Windows.Forms.Button loadButton;
    }
}