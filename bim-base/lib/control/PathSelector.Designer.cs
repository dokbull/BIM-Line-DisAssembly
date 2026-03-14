partial class PathSelector
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
        this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
        this.pathChangeButton = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.tableLayoutPanel1.SuspendLayout();
        this.SuspendLayout();
        // 
        // tableLayoutPanel1
        // 
        this.tableLayoutPanel1.ColumnCount = 2;
        this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.16708F));
        this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.83292F));
        this.tableLayoutPanel1.Controls.Add(this.pathChangeButton, 1, 0);
        this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
        this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
        this.tableLayoutPanel1.Name = "tableLayoutPanel1";
        this.tableLayoutPanel1.RowCount = 1;
        this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
        this.tableLayoutPanel1.Size = new System.Drawing.Size(407, 37);
        this.tableLayoutPanel1.TabIndex = 1;
        // 
        // pathChangeButton
        // 
        this.pathChangeButton.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pathChangeButton.Location = new System.Drawing.Point(312, 3);
        this.pathChangeButton.Name = "pathChangeButton";
        this.pathChangeButton.Size = new System.Drawing.Size(92, 31);
        this.pathChangeButton.TabIndex = 1;
        this.pathChangeButton.Text = "Change";
        this.pathChangeButton.UseVisualStyleBackColor = true;
        this.pathChangeButton.Click += new System.EventHandler(this.pathChangeButton_Click);
        // 
        // label1
        // 
        this.label1.BackColor = System.Drawing.Color.White;
        this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.label1.Location = new System.Drawing.Point(3, 3);
        this.label1.Margin = new System.Windows.Forms.Padding(3);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(303, 31);
        this.label1.TabIndex = 2;
        this.label1.Text = "label1";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // PathSelector
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.tableLayoutPanel1);
        this.Name = "PathSelector";
        this.Size = new System.Drawing.Size(407, 37);
        this.Load += new System.EventHandler(this.PathSelector_Load);
        this.tableLayoutPanel1.ResumeLayout(false);
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Button pathChangeButton;
    private System.Windows.Forms.Label label1;
}
