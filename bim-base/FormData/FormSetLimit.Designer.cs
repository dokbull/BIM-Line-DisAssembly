namespace bim_base
{
    partial class FormSetLimit
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
            this.uiTimer = new System.Windows.Forms.Timer(this.components);
            this.stepGrid = new CSourceGrid();
            this.jogControl2 = new bim_base.JogControl();
            this.btSaveLimitY = new System.Windows.Forms.Button();
            this.btSaveLimitX = new System.Windows.Forms.Button();
            this.lbLimitYPlus = new System.Windows.Forms.Button();
            this.lbLimitYMinus = new System.Windows.Forms.Button();
            this.lbLimitXPlus = new System.Windows.Forms.Button();
            this.lbLimitXMinus = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.lbTargetY = new System.Windows.Forms.Label();
            this.lbTargetX = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbcurrentY = new System.Windows.Forms.Label();
            this.lbcurrentX = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.stepGrid.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiTimer
            // 
            this.uiTimer.Enabled = true;
            this.uiTimer.Tick += new System.EventHandler(this.uiTimer_Tick);
            // 
            // stepGrid
            // 
            this.stepGrid.Controls.Add(this.jogControl2);
            this.stepGrid.Controls.Add(this.btSaveLimitY);
            this.stepGrid.Controls.Add(this.btSaveLimitX);
            this.stepGrid.Controls.Add(this.lbLimitYPlus);
            this.stepGrid.Controls.Add(this.lbLimitYMinus);
            this.stepGrid.Controls.Add(this.lbLimitXPlus);
            this.stepGrid.Controls.Add(this.lbLimitXMinus);
            this.stepGrid.Controls.Add(this.tableLayoutPanel1);
            this.stepGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepGrid.EnableSort = true;
            this.stepGrid.Location = new System.Drawing.Point(0, 0);
            this.stepGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.stepGrid.Name = "stepGrid";
            this.stepGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.stepGrid.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.stepGrid.Size = new System.Drawing.Size(796, 359);
            this.stepGrid.TabIndex = 29;
            this.stepGrid.TabStop = true;
            this.stepGrid.ToolTipText = "";
            // 
            // jogControl2
            // 
            this.jogControl2.BackColor = System.Drawing.Color.White;
            this.jogControl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.jogControl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jogControl2.Location = new System.Drawing.Point(509, 12);
            this.jogControl2.Name = "jogControl2";
            this.jogControl2.Size = new System.Drawing.Size(275, 344);
            this.jogControl2.TabIndex = 21;
            // 
            // btSaveLimitY
            // 
            this.btSaveLimitY.BackColor = System.Drawing.Color.Gainsboro;
            this.btSaveLimitY.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btSaveLimitY.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btSaveLimitY.Location = new System.Drawing.Point(267, 267);
            this.btSaveLimitY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btSaveLimitY.Name = "btSaveLimitY";
            this.btSaveLimitY.Size = new System.Drawing.Size(124, 54);
            this.btSaveLimitY.TabIndex = 20;
            this.btSaveLimitY.Tag = "-1";
            this.btSaveLimitY.Text = "Save Limit Y";
            this.btSaveLimitY.UseVisualStyleBackColor = false;
            this.btSaveLimitY.Click += new System.EventHandler(this.btSaveLimitY_Click);
            // 
            // btSaveLimitX
            // 
            this.btSaveLimitX.BackColor = System.Drawing.Color.Gainsboro;
            this.btSaveLimitX.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btSaveLimitX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btSaveLimitX.Location = new System.Drawing.Point(267, 203);
            this.btSaveLimitX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btSaveLimitX.Name = "btSaveLimitX";
            this.btSaveLimitX.Size = new System.Drawing.Size(124, 54);
            this.btSaveLimitX.TabIndex = 19;
            this.btSaveLimitX.Tag = "-1";
            this.btSaveLimitX.Text = "Save Limit X";
            this.btSaveLimitX.UseVisualStyleBackColor = false;
            this.btSaveLimitX.Click += new System.EventHandler(this.btSaveLimitX_Click);
            // 
            // lbLimitYPlus
            // 
            this.lbLimitYPlus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.lbLimitYPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbLimitYPlus.Location = new System.Drawing.Point(135, 267);
            this.lbLimitYPlus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbLimitYPlus.Name = "lbLimitYPlus";
            this.lbLimitYPlus.Size = new System.Drawing.Size(124, 54);
            this.lbLimitYPlus.TabIndex = 18;
            this.lbLimitYPlus.Tag = "-1";
            this.lbLimitYPlus.Text = "Limit Y+";
            this.lbLimitYPlus.UseVisualStyleBackColor = true;
            this.lbLimitYPlus.Click += new System.EventHandler(this.lbLimitYPlus_Click);
            // 
            // lbLimitYMinus
            // 
            this.lbLimitYMinus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.lbLimitYMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbLimitYMinus.Location = new System.Drawing.Point(3, 267);
            this.lbLimitYMinus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbLimitYMinus.Name = "lbLimitYMinus";
            this.lbLimitYMinus.Size = new System.Drawing.Size(124, 54);
            this.lbLimitYMinus.TabIndex = 17;
            this.lbLimitYMinus.Tag = "-1";
            this.lbLimitYMinus.Text = "Limit Y-";
            this.lbLimitYMinus.UseVisualStyleBackColor = true;
            this.lbLimitYMinus.Click += new System.EventHandler(this.lbLimitYMinus_Click);
            // 
            // lbLimitXPlus
            // 
            this.lbLimitXPlus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.lbLimitXPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbLimitXPlus.Location = new System.Drawing.Point(135, 203);
            this.lbLimitXPlus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbLimitXPlus.Name = "lbLimitXPlus";
            this.lbLimitXPlus.Size = new System.Drawing.Size(124, 54);
            this.lbLimitXPlus.TabIndex = 16;
            this.lbLimitXPlus.Tag = "-1";
            this.lbLimitXPlus.Text = "Limit X+";
            this.lbLimitXPlus.UseVisualStyleBackColor = true;
            this.lbLimitXPlus.Click += new System.EventHandler(this.lbLimitXPlus_Click);
            // 
            // lbLimitXMinus
            // 
            this.lbLimitXMinus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.lbLimitXMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbLimitXMinus.Location = new System.Drawing.Point(3, 203);
            this.lbLimitXMinus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbLimitXMinus.Name = "lbLimitXMinus";
            this.lbLimitXMinus.Size = new System.Drawing.Size(124, 54);
            this.lbLimitXMinus.TabIndex = 15;
            this.lbLimitXMinus.Tag = "-1";
            this.lbLimitXMinus.Text = "Limit X-";
            this.lbLimitXMinus.UseVisualStyleBackColor = true;
            this.lbLimitXMinus.Click += new System.EventHandler(this.lbLimitXMinus_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbTargetY, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbTargetX, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbcurrentY, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbcurrentX, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(500, 131);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Gainsboro;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(2, 2);
            this.label9.Margin = new System.Windows.Forms.Padding(1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(123, 40);
            this.label9.TabIndex = 44;
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbTargetY
            // 
            this.lbTargetY.AutoSize = true;
            this.lbTargetY.BackColor = System.Drawing.Color.Honeydew;
            this.lbTargetY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTargetY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTargetY.ForeColor = System.Drawing.Color.Red;
            this.lbTargetY.Location = new System.Drawing.Point(314, 88);
            this.lbTargetY.Margin = new System.Windows.Forms.Padding(1);
            this.lbTargetY.Name = "lbTargetY";
            this.lbTargetY.Size = new System.Drawing.Size(184, 41);
            this.lbTargetY.TabIndex = 43;
            this.lbTargetY.Text = "0.00";
            this.lbTargetY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbTargetY.Click += new System.EventHandler(this.lbTargetY_Click);
            // 
            // lbTargetX
            // 
            this.lbTargetX.AutoSize = true;
            this.lbTargetX.BackColor = System.Drawing.Color.Honeydew;
            this.lbTargetX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTargetX.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTargetX.ForeColor = System.Drawing.Color.Red;
            this.lbTargetX.Location = new System.Drawing.Point(128, 88);
            this.lbTargetX.Margin = new System.Windows.Forms.Padding(1);
            this.lbTargetX.Name = "lbTargetX";
            this.lbTargetX.Size = new System.Drawing.Size(183, 41);
            this.lbTargetX.TabIndex = 42;
            this.lbTargetX.Text = "0.00";
            this.lbTargetX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbTargetX.Click += new System.EventHandler(this.lbTargetX_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Honeydew;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(2, 88);
            this.label6.Margin = new System.Windows.Forms.Padding(1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 41);
            this.label6.TabIndex = 41;
            this.label6.Text = "Target";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbcurrentY
            // 
            this.lbcurrentY.AutoSize = true;
            this.lbcurrentY.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbcurrentY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbcurrentY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbcurrentY.Location = new System.Drawing.Point(314, 45);
            this.lbcurrentY.Margin = new System.Windows.Forms.Padding(1);
            this.lbcurrentY.Name = "lbcurrentY";
            this.lbcurrentY.Size = new System.Drawing.Size(184, 40);
            this.lbcurrentY.TabIndex = 40;
            this.lbcurrentY.Text = "0.00";
            this.lbcurrentY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbcurrentX
            // 
            this.lbcurrentX.AutoSize = true;
            this.lbcurrentX.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbcurrentX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbcurrentX.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbcurrentX.Location = new System.Drawing.Point(128, 45);
            this.lbcurrentX.Margin = new System.Windows.Forms.Padding(1);
            this.lbcurrentX.Name = "lbcurrentX";
            this.lbcurrentX.Size = new System.Drawing.Size(183, 40);
            this.lbcurrentX.TabIndex = 39;
            this.lbcurrentX.Text = "0.00";
            this.lbcurrentX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 45);
            this.label3.Margin = new System.Windows.Forms.Padding(1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 40);
            this.label3.TabIndex = 38;
            this.label3.Text = "Current";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(314, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 40);
            this.label2.TabIndex = 37;
            this.label2.Text = "PP Attach Y";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(128, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 40);
            this.label1.TabIndex = 36;
            this.label1.Text = "PP Attach X";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormSetLimit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(796, 359);
            this.Controls.Add(this.stepGrid);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSetLimit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormSetLimit";
            this.Load += new System.EventHandler(this.FormStep_Load);
            this.stepGrid.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CSourceGrid stepGrid;
        private System.Windows.Forms.Timer uiTimer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
       // private JogControl jogControl1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbTargetY;
        private System.Windows.Forms.Label lbTargetX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbcurrentY;
        private System.Windows.Forms.Label lbcurrentX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btSaveLimitY;
        private System.Windows.Forms.Button btSaveLimitX;
        private System.Windows.Forms.Button lbLimitYPlus;
        private System.Windows.Forms.Button lbLimitYMinus;
        private System.Windows.Forms.Button lbLimitXPlus;
        private System.Windows.Forms.Button lbLimitXMinus;
        //private JogControl jogControl1;
        private JogControl jogControl2;
    }
}