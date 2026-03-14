namespace bim_base.FormComponent
{
    partial class UnitPPTray
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.downButton = new CIOButton();
            this.yBwdButton = new CIOButton();
            this.xBwdButton = new CIOButton();
            this.vacButton = new CIOButton();
            this.upButton = new CIOButton();
            this.label2 = new System.Windows.Forms.Label();
            this.xFwdButton = new CIOButton();
            this.yFwdButton = new CIOButton();
            this.ui_timer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tray X Align";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.downButton, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.yBwdButton, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.xBwdButton, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.vacButton, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.upButton, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.xFwdButton, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.yFwdButton, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("SamsungOne 800C", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(489, 157);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(123, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Tray Y Align";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // downButton
            // 
            this.downButton._IO_FALSE_COLOR = System.Drawing.Color.Red;
            this.downButton._IO_POS = CBUTTON_POS.Left;
            this.downButton._IO_SIZE = new System.Drawing.Size(10, 10);
            this.downButton._IO_STATE = false;
            this.downButton._IO_STATE_EXTRA = false;
            this.downButton._IO_TRUE_COLOR = System.Drawing.Color.Green;
            this.downButton._MARGIN = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.downButton._OUT_STATE = false;
            this.downButton._TEXT = "DOWN";
            this.downButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.downButton.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downButton.Location = new System.Drawing.Point(247, 94);
            this.downButton.Margin = new System.Windows.Forms.Padding(2);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(117, 60);
            this.downButton.TabIndex = 8;
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // yBwdButton
            // 
            this.yBwdButton._IO_FALSE_COLOR = System.Drawing.Color.Red;
            this.yBwdButton._IO_POS = CBUTTON_POS.Left;
            this.yBwdButton._IO_SIZE = new System.Drawing.Size(10, 10);
            this.yBwdButton._IO_STATE = false;
            this.yBwdButton._IO_STATE_EXTRA = false;
            this.yBwdButton._IO_TRUE_COLOR = System.Drawing.Color.Green;
            this.yBwdButton._MARGIN = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.yBwdButton._OUT_STATE = false;
            this.yBwdButton._TEXT = "BWD";
            this.yBwdButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.yBwdButton.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yBwdButton.Location = new System.Drawing.Point(125, 94);
            this.yBwdButton.Margin = new System.Windows.Forms.Padding(2);
            this.yBwdButton.Name = "yBwdButton";
            this.yBwdButton.Size = new System.Drawing.Size(117, 60);
            this.yBwdButton.TabIndex = 7;
            this.yBwdButton.UseVisualStyleBackColor = true;
            this.yBwdButton.Click += new System.EventHandler(this.yBwdButton_Click);
            // 
            // xBwdButton
            // 
            this.xBwdButton._IO_FALSE_COLOR = System.Drawing.Color.Red;
            this.xBwdButton._IO_POS = CBUTTON_POS.Left;
            this.xBwdButton._IO_SIZE = new System.Drawing.Size(10, 10);
            this.xBwdButton._IO_STATE = false;
            this.xBwdButton._IO_STATE_EXTRA = false;
            this.xBwdButton._IO_TRUE_COLOR = System.Drawing.Color.Green;
            this.xBwdButton._MARGIN = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.xBwdButton._OUT_STATE = false;
            this.xBwdButton._TEXT = "BWD";
            this.xBwdButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xBwdButton.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xBwdButton.Location = new System.Drawing.Point(3, 94);
            this.xBwdButton.Margin = new System.Windows.Forms.Padding(2);
            this.xBwdButton.Name = "xBwdButton";
            this.xBwdButton.Size = new System.Drawing.Size(117, 60);
            this.xBwdButton.TabIndex = 6;
            this.xBwdButton.UseVisualStyleBackColor = true;
            this.xBwdButton.Click += new System.EventHandler(this.xBwdButton_Click);
            // 
            // vacButton
            // 
            this.vacButton._IO_FALSE_COLOR = System.Drawing.Color.Red;
            this.vacButton._IO_POS = CBUTTON_POS.Left;
            this.vacButton._IO_SIZE = new System.Drawing.Size(10, 10);
            this.vacButton._IO_STATE = false;
            this.vacButton._IO_STATE_EXTRA = false;
            this.vacButton._IO_TRUE_COLOR = System.Drawing.Color.Green;
            this.vacButton._MARGIN = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.vacButton._OUT_STATE = false;
            this.vacButton._TEXT = "VAC";
            this.vacButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vacButton.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vacButton.Location = new System.Drawing.Point(369, 29);
            this.vacButton.Margin = new System.Windows.Forms.Padding(2);
            this.vacButton.Name = "vacButton";
            this.vacButton.Size = new System.Drawing.Size(117, 60);
            this.vacButton.TabIndex = 5;
            this.vacButton.UseVisualStyleBackColor = true;
            this.vacButton.Click += new System.EventHandler(this.cioButton2_Click);
            // 
            // upButton
            // 
            this.upButton._IO_FALSE_COLOR = System.Drawing.Color.Red;
            this.upButton._IO_POS = CBUTTON_POS.Left;
            this.upButton._IO_SIZE = new System.Drawing.Size(10, 10);
            this.upButton._IO_STATE = false;
            this.upButton._IO_STATE_EXTRA = false;
            this.upButton._IO_TRUE_COLOR = System.Drawing.Color.Green;
            this.upButton._MARGIN = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.upButton._OUT_STATE = false;
            this.upButton._TEXT = "UP";
            this.upButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.upButton.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.upButton.Location = new System.Drawing.Point(247, 29);
            this.upButton.Margin = new System.Windows.Forms.Padding(2);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(117, 60);
            this.upButton.TabIndex = 4;
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.cioButton1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tableLayoutPanel1.SetColumnSpan(this.label2, 2);
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(245, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(243, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Control";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // xFwdButton
            // 
            this.xFwdButton._IO_FALSE_COLOR = System.Drawing.Color.Red;
            this.xFwdButton._IO_POS = CBUTTON_POS.Left;
            this.xFwdButton._IO_SIZE = new System.Drawing.Size(10, 10);
            this.xFwdButton._IO_STATE = false;
            this.xFwdButton._IO_STATE_EXTRA = false;
            this.xFwdButton._IO_TRUE_COLOR = System.Drawing.Color.Green;
            this.xFwdButton._MARGIN = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.xFwdButton._OUT_STATE = false;
            this.xFwdButton._TEXT = "FWD";
            this.xFwdButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xFwdButton.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xFwdButton.Location = new System.Drawing.Point(3, 29);
            this.xFwdButton.Margin = new System.Windows.Forms.Padding(2);
            this.xFwdButton.Name = "xFwdButton";
            this.xFwdButton.Size = new System.Drawing.Size(117, 60);
            this.xFwdButton.TabIndex = 0;
            this.xFwdButton.UseVisualStyleBackColor = true;
            this.xFwdButton.Click += new System.EventHandler(this.trayXAlignButton_Click);
            // 
            // yFwdButton
            // 
            this.yFwdButton._IO_FALSE_COLOR = System.Drawing.Color.Red;
            this.yFwdButton._IO_POS = CBUTTON_POS.Left;
            this.yFwdButton._IO_SIZE = new System.Drawing.Size(10, 10);
            this.yFwdButton._IO_STATE = false;
            this.yFwdButton._IO_STATE_EXTRA = false;
            this.yFwdButton._IO_TRUE_COLOR = System.Drawing.Color.Green;
            this.yFwdButton._MARGIN = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.yFwdButton._OUT_STATE = false;
            this.yFwdButton._TEXT = "FWD";
            this.yFwdButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.yFwdButton.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yFwdButton.Location = new System.Drawing.Point(125, 29);
            this.yFwdButton.Margin = new System.Windows.Forms.Padding(2);
            this.yFwdButton.Name = "yFwdButton";
            this.yFwdButton.Size = new System.Drawing.Size(117, 60);
            this.yFwdButton.TabIndex = 1;
            this.yFwdButton.UseVisualStyleBackColor = true;
            this.yFwdButton.Click += new System.EventHandler(this.trayYAlignButton_Click);
            // 
            // ui_timer
            // 
            this.ui_timer.Tick += new System.EventHandler(this.ui_timer_Tick);
            // 
            // UnitPPTray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "UnitPPTray";
            this.Size = new System.Drawing.Size(489, 157);
            this.Load += new System.EventHandler(this.UnitDisplayControl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion
    private CIOButton yFwdButton;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private CIOButton xFwdButton;
    private System.Windows.Forms.Timer ui_timer;
        private CIOButton vacButton;
        private CIOButton upButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private CIOButton downButton;
        private CIOButton yBwdButton;
        private CIOButton xBwdButton;
    }
}
