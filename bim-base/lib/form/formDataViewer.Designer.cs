partial class formDataViewer
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
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.extDataGridView1 = new ExtDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.extDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(617, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(134, 60);
            this.button2.TabIndex = 0;
            this.button2.Text = "Excel 변환 (&E)";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(477, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(134, 60);
            this.button3.TabIndex = 0;
            this.button3.Text = "검색 (&S)";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(757, 13);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(134, 60);
            this.button4.TabIndex = 0;
            this.button4.Text = "설정 (&O)";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 13);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(177, 21);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(275, 13);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(177, 21);
            this.dateTimePicker2.TabIndex = 1;
            // 
            // extDataGridView1
            // 
            this.extDataGridView1.AllowUserToAddRows = false;
            this.extDataGridView1.AllowUserToDeleteRows = false;
            this.extDataGridView1.AllowUserToResizeColumns = false;
            this.extDataGridView1.AllowUserToResizeRows = false;
            this.extDataGridView1.AutoResizeColumnHeader = false;
            this.extDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.extDataGridView1.isSort = false;
            this.extDataGridView1.Location = new System.Drawing.Point(12, 78);
            this.extDataGridView1.MultiSelect = false;
            this.extDataGridView1.Name = "extDataGridView1";
            this.extDataGridView1.ReadOnly = true;
            this.extDataGridView1.RowHeadersVisible = false;
            this.extDataGridView1.RowTemplate.Height = 23;
            this.extDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.extDataGridView1.Size = new System.Drawing.Size(879, 291);
            this.extDataGridView1.TabIndex = 2;
            this.extDataGridView1.UseHorizontalScrollBar = false;
            // 
            // formDataViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 382);
            this.Controls.Add(this.extDataGridView1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Name = "formDataViewer";
            this.Text = "formDataViewer";
            this.Load += new System.EventHandler(this.formDataViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.extDataGridView1)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.DateTimePicker dateTimePicker1;
    private ExtDataGridView extDataGridView1;
    private System.Windows.Forms.DateTimePicker dateTimePicker2;
}
