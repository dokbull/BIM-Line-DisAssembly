partial class miniAxisControlPaixRtex
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.cTableLayoutPanel1 = new CTableLayoutPanel();
            this.cTableLayoutPanel2 = new CTableLayoutPanel();
            this.inpos = new System.Windows.Forms.Label();
            this.minusLimit = new System.Windows.Forms.Label();
            this.plusLimit = new System.Windows.Forms.Label();
            this.servoOn = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.cTableLayoutPanel1.SuspendLayout();
            this.cTableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cTableLayoutPanel1
            // 
            this.cTableLayoutPanel1.ColumnCount = 1;
            this.cTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.cTableLayoutPanel1.Controls.Add(this.cTableLayoutPanel2, 0, 1);
            this.cTableLayoutPanel1.Controls.Add(this.nameLabel, 0, 0);
            this.cTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.cTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.cTableLayoutPanel1.Name = "cTableLayoutPanel1";
            this.cTableLayoutPanel1.RowCount = 2;
            this.cTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 71.42857F));
            this.cTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.cTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.cTableLayoutPanel1.Size = new System.Drawing.Size(207, 91);
            this.cTableLayoutPanel1.TabIndex = 2;
            // 
            // cTableLayoutPanel2
            // 
            this.cTableLayoutPanel2.ColumnCount = 4;
            this.cTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.cTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.cTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.cTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.cTableLayoutPanel2.Controls.Add(this.inpos, 0, 0);
            this.cTableLayoutPanel2.Controls.Add(this.minusLimit, 1, 0);
            this.cTableLayoutPanel2.Controls.Add(this.plusLimit, 2, 0);
            this.cTableLayoutPanel2.Controls.Add(this.servoOn, 3, 0);
            this.cTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cTableLayoutPanel2.Location = new System.Drawing.Point(0, 65);
            this.cTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.cTableLayoutPanel2.Name = "cTableLayoutPanel2";
            this.cTableLayoutPanel2.RowCount = 1;
            this.cTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.cTableLayoutPanel2.Size = new System.Drawing.Size(207, 26);
            this.cTableLayoutPanel2.TabIndex = 2;
            // 
            // inpos
            // 
            this.inpos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inpos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inpos.Font = new System.Drawing.Font("SamsungOne 800C", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inpos.Location = new System.Drawing.Point(4, 1);
            this.inpos.Margin = new System.Windows.Forms.Padding(4, 1, 1, 4);
            this.inpos.Name = "inpos";
            this.inpos.Size = new System.Drawing.Size(46, 21);
            this.inpos.TabIndex = 0;
            this.inpos.Text = "INPOS";
            this.inpos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // minusLimit
            // 
            this.minusLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.minusLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.minusLimit.Font = new System.Drawing.Font("SamsungOne 800C", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minusLimit.Location = new System.Drawing.Point(52, 1);
            this.minusLimit.Margin = new System.Windows.Forms.Padding(1, 1, 1, 4);
            this.minusLimit.Name = "minusLimit";
            this.minusLimit.Size = new System.Drawing.Size(49, 21);
            this.minusLimit.TabIndex = 0;
            this.minusLimit.Text = "- LIMIT";
            this.minusLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // plusLimit
            // 
            this.plusLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plusLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plusLimit.Font = new System.Drawing.Font("SamsungOne 800C", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plusLimit.Location = new System.Drawing.Point(103, 1);
            this.plusLimit.Margin = new System.Windows.Forms.Padding(1, 1, 1, 4);
            this.plusLimit.Name = "plusLimit";
            this.plusLimit.Size = new System.Drawing.Size(49, 21);
            this.plusLimit.TabIndex = 0;
            this.plusLimit.Text = "+ LIMIT";
            this.plusLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // servoOn
            // 
            this.servoOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.servoOn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.servoOn.Font = new System.Drawing.Font("SamsungOne 800C", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.servoOn.Location = new System.Drawing.Point(154, 1);
            this.servoOn.Margin = new System.Windows.Forms.Padding(1, 1, 4, 4);
            this.servoOn.Name = "servoOn";
            this.servoOn.Size = new System.Drawing.Size(49, 21);
            this.servoOn.TabIndex = 0;
            this.servoOn.Text = "SERVO";
            this.servoOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nameLabel
            // 
            this.nameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameLabel.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(4, 4);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(4);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(199, 57);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Axis Name\r\n(123.45)";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.nameLabel.Click += new System.EventHandler(this.nameLabel_Click);
            // 
            // miniAxisControlPaixRtex
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.cTableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "miniAxisControlPaixRtex";
            this.Size = new System.Drawing.Size(207, 91);
            this.Load += new System.EventHandler(this.AxisControl_Load);
            this.cTableLayoutPanel1.ResumeLayout(false);
            this.cTableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion

    private CTableLayoutPanel cTableLayoutPanel1;
    private CTableLayoutPanel cTableLayoutPanel2;
    private System.Windows.Forms.Label inpos;
    private System.Windows.Forms.Label minusLimit;
    private System.Windows.Forms.Label plusLimit;
    private System.Windows.Forms.Label servoOn;
    private System.Windows.Forms.Label nameLabel;
}
