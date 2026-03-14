partial class formDataViewerSetting
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.yearUpDown = new System.Windows.Forms.NumericUpDown();
            this.monthUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.deleteDateLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dbAddressTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dbAccountTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dbPasswordTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dbNameTextBox = new System.Windows.Forms.TextBox();
            this.connectTestButton = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yearUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.monthUpDown)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 320);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data storage period";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 390);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(533, 27);
            this.label2.TabIndex = 0;
            this.label2.Text = "Overdue data is exported in csv format and compressed and stored monthly.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.yearUpDown, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.monthUpDown, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(133, 317);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(258, 21);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(67, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "Year";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(195, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 21);
            this.label4.TabIndex = 2;
            this.label4.Text = "Month";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // yearUpDown
            // 
            this.yearUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.yearUpDown.Location = new System.Drawing.Point(3, 3);
            this.yearUpDown.Name = "yearUpDown";
            this.yearUpDown.Size = new System.Drawing.Size(58, 21);
            this.yearUpDown.TabIndex = 3;
            this.yearUpDown.ValueChanged += new System.EventHandler(this.yearUpDown_ValueChanged);
            // 
            // monthUpDown
            // 
            this.monthUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monthUpDown.Location = new System.Drawing.Point(131, 3);
            this.monthUpDown.Name = "monthUpDown";
            this.monthUpDown.Size = new System.Drawing.Size(58, 21);
            this.monthUpDown.TabIndex = 3;
            this.monthUpDown.ValueChanged += new System.EventHandler(this.monthUpDown_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(12, 359);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "Delete date";
            // 
            // deleteDateLabel
            // 
            this.deleteDateLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.deleteDateLabel.Location = new System.Drawing.Point(134, 359);
            this.deleteDateLabel.Name = "deleteDateLabel";
            this.deleteDateLabel.Size = new System.Drawing.Size(257, 12);
            this.deleteDateLabel.TabIndex = 0;
            this.deleteDateLabel.Text = "-----";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.42377F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.57623F));
            this.tableLayoutPanel2.Controls.Add(this.dbAddressTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.dbAccountTextBox, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.dbPasswordTextBox, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label10, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.dbNameTextBox, 1, 3);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(14, 12);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.43903F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.2439F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.85366F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.85366F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(387, 164);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // dbAddressTextBox
            // 
            this.dbAddressTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbAddressTextBox.Location = new System.Drawing.Point(112, 3);
            this.dbAddressTextBox.Multiline = true;
            this.dbAddressTextBox.Name = "dbAddressTextBox";
            this.dbAddressTextBox.Size = new System.Drawing.Size(272, 80);
            this.dbAddressTextBox.TabIndex = 3;
            this.dbAddressTextBox.TextChanged += new System.EventHandler(this.dbAddressTextBox_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 86);
            this.label7.TabIndex = 3;
            this.label7.Text = "DB Address";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(3, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 25);
            this.label8.TabIndex = 3;
            this.label8.Text = "account";
            // 
            // dbAccountTextBox
            // 
            this.dbAccountTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbAccountTextBox.Location = new System.Drawing.Point(112, 89);
            this.dbAccountTextBox.Name = "dbAccountTextBox";
            this.dbAccountTextBox.Size = new System.Drawing.Size(272, 21);
            this.dbAccountTextBox.TabIndex = 3;
            this.dbAccountTextBox.TextChanged += new System.EventHandler(this.dbAccountTextBox_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(3, 111);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 26);
            this.label9.TabIndex = 3;
            this.label9.Text = "password";
            // 
            // dbPasswordTextBox
            // 
            this.dbPasswordTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbPasswordTextBox.Location = new System.Drawing.Point(112, 114);
            this.dbPasswordTextBox.Name = "dbPasswordTextBox";
            this.dbPasswordTextBox.Size = new System.Drawing.Size(272, 21);
            this.dbPasswordTextBox.TabIndex = 3;
            this.dbPasswordTextBox.TextChanged += new System.EventHandler(this.dbPasswordTextBox_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(3, 137);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(103, 27);
            this.label10.TabIndex = 3;
            this.label10.Text = "database name";
            // 
            // dbNameTextBox
            // 
            this.dbNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbNameTextBox.Location = new System.Drawing.Point(112, 140);
            this.dbNameTextBox.Name = "dbNameTextBox";
            this.dbNameTextBox.Size = new System.Drawing.Size(272, 21);
            this.dbNameTextBox.TabIndex = 3;
            this.dbNameTextBox.TextChanged += new System.EventHandler(this.dbNameTextBox_TextChanged);
            // 
            // connectTestButton
            // 
            this.connectTestButton.Location = new System.Drawing.Point(409, 15);
            this.connectTestButton.Name = "connectTestButton";
            this.connectTestButton.Size = new System.Drawing.Size(159, 39);
            this.connectTestButton.TabIndex = 3;
            this.connectTestButton.Text = "Connect Test";
            this.connectTestButton.UseVisualStyleBackColor = true;
            this.connectTestButton.Click += new System.EventHandler(this.connectTestButton_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 199);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(1124, 88);
            this.listBox1.TabIndex = 4;
            // 
            // formDataViewerSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 427);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.connectTestButton);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.deleteDateLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Name = "formDataViewerSetting";
            this.Text = "formDataViewerSetting";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yearUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.monthUpDown)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.NumericUpDown yearUpDown;
    private System.Windows.Forms.NumericUpDown monthUpDown;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label deleteDateLabel;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox dbAddressTextBox;
    private System.Windows.Forms.TextBox dbAccountTextBox;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox dbPasswordTextBox;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox dbNameTextBox;
    private System.Windows.Forms.Button connectTestButton;
    private System.Windows.Forms.ListBox listBox1;
}
