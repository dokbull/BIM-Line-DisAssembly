namespace bim_base
{
    partial class FormTeachOutPP
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
            this.xSaveButton = new System.Windows.Forms.Button();
            this.moveButton = new System.Windows.Forms.Button();
            this.zSaveButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.motorPosGrid = new CSourceGrid();
            this.ui_timer = new System.Windows.Forms.Timer(this.components);
            this.jogControl1 = new bim_base.JogControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.ppVacOnButton = new System.Windows.Forms.Button();
            this.ppVacOffButton = new System.Windows.Forms.Button();
            this.fwdButton = new System.Windows.Forms.Button();
            this.bwdButton = new System.Windows.Forms.Button();
            this.reverseUpButton1 = new System.Windows.Forms.Button();
            this.reverseDownButton1 = new System.Windows.Forms.Button();
            this.reverseVacOnButton1 = new System.Windows.Forms.Button();
            this.reverseVacOffButton1 = new System.Windows.Forms.Button();
            this.reverseTurnButton1 = new System.Windows.Forms.Button();
            this.reverseReturnButton1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.reverseVacOnButton2 = new System.Windows.Forms.Button();
            this.reverseVacOffButton2 = new System.Windows.Forms.Button();
            this.reverseTurnButton2 = new System.Windows.Forms.Button();
            this.reverseReturnButton2 = new System.Windows.Forms.Button();
            this.reverseUpButton2 = new System.Windows.Forms.Button();
            this.reverseDownButton2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // xSaveButton
            // 
            this.xSaveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xSaveButton.Location = new System.Drawing.Point(3, 312);
            this.xSaveButton.Name = "xSaveButton";
            this.xSaveButton.Size = new System.Drawing.Size(139, 35);
            this.xSaveButton.TabIndex = 19;
            this.xSaveButton.Text = "Y Save";
            this.xSaveButton.UseVisualStyleBackColor = true;
            this.xSaveButton.Click += new System.EventHandler(this.xySaveButton_Click);
            // 
            // moveButton
            // 
            this.moveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.moveButton.Location = new System.Drawing.Point(583, 312);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(141, 35);
            this.moveButton.TabIndex = 22;
            this.moveButton.Text = "Move";
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.Click += new System.EventHandler(this.moveButton_Click);
            // 
            // zSaveButton
            // 
            this.zSaveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zSaveButton.Location = new System.Drawing.Point(148, 312);
            this.zSaveButton.Name = "zSaveButton";
            this.zSaveButton.Size = new System.Drawing.Size(139, 35);
            this.zSaveButton.TabIndex = 18;
            this.zSaveButton.Text = "Z Save";
            this.zSaveButton.UseVisualStyleBackColor = true;
            this.zSaveButton.Click += new System.EventHandler(this.zSaveButton_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Controls.Add(this.xSaveButton, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.moveButton, 4, 3);
            this.tableLayoutPanel3.Controls.Add(this.zSaveButton, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.motorPosGrid, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.67033F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.67033F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.67033F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.98901F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(727, 350);
            this.tableLayoutPanel3.TabIndex = 54;
            // 
            // motorPosGrid
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.motorPosGrid, 5);
            this.motorPosGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.motorPosGrid.EnableSort = true;
            this.motorPosGrid.Location = new System.Drawing.Point(3, 3);
            this.motorPosGrid.Name = "motorPosGrid";
            this.motorPosGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.tableLayoutPanel3.SetRowSpan(this.motorPosGrid, 3);
            this.motorPosGrid.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.motorPosGrid.Size = new System.Drawing.Size(721, 303);
            this.motorPosGrid.TabIndex = 12;
            this.motorPosGrid.TabStop = true;
            this.motorPosGrid.ToolTipText = "";
            // 
            // ui_timer
            // 
            this.ui_timer.Tick += new System.EventHandler(this.ui_timer_Tick);
            // 
            // jogControl1
            // 
            this.jogControl1.BackColor = System.Drawing.Color.White;
            this.jogControl1.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jogControl1.Location = new System.Drawing.Point(745, 12);
            this.jogControl1.Name = "jogControl1";
            this.jogControl1.Size = new System.Drawing.Size(246, 350);
            this.jogControl1.TabIndex = 58;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.ppVacOnButton, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.ppVacOffButton, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.fwdButton, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.bwdButton, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.reverseVacOnButton1, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.reverseVacOffButton1, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.reverseUpButton1, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.reverseDownButton1, 4, 2);
            this.tableLayoutPanel2.Controls.Add(this.reverseReturnButton1, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.reverseTurnButton1, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.reverseVacOnButton2, 5, 1);
            this.tableLayoutPanel2.Controls.Add(this.reverseVacOffButton2, 5, 2);
            this.tableLayoutPanel2.Controls.Add(this.reverseTurnButton2, 6, 1);
            this.tableLayoutPanel2.Controls.Add(this.reverseReturnButton2, 6, 2);
            this.tableLayoutPanel2.Controls.Add(this.reverseUpButton2, 7, 1);
            this.tableLayoutPanel2.Controls.Add(this.reverseDownButton2, 7, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 368);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(979, 181);
            this.tableLayoutPanel2.TabIndex = 59;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanel2.SetColumnSpan(this.label3, 2);
            this.label3.Location = new System.Drawing.Point(1, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(243, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "PP";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ppVacOnButton
            // 
            this.ppVacOnButton.Location = new System.Drawing.Point(4, 25);
            this.ppVacOnButton.Name = "ppVacOnButton";
            this.ppVacOnButton.Size = new System.Drawing.Size(115, 72);
            this.ppVacOnButton.TabIndex = 2;
            this.ppVacOnButton.Text = "VAC ON";
            this.ppVacOnButton.UseVisualStyleBackColor = true;
            this.ppVacOnButton.Click += new System.EventHandler(this.ppVacOnButton_Click);
            // 
            // ppVacOffButton
            // 
            this.ppVacOffButton.Location = new System.Drawing.Point(4, 104);
            this.ppVacOffButton.Name = "ppVacOffButton";
            this.ppVacOffButton.Size = new System.Drawing.Size(115, 73);
            this.ppVacOffButton.TabIndex = 3;
            this.ppVacOffButton.Text = "VAC OFF";
            this.ppVacOffButton.UseVisualStyleBackColor = true;
            this.ppVacOffButton.Click += new System.EventHandler(this.ppVacOffButton_Click);
            // 
            // fwdButton
            // 
            this.fwdButton.Location = new System.Drawing.Point(126, 25);
            this.fwdButton.Name = "fwdButton";
            this.fwdButton.Size = new System.Drawing.Size(115, 72);
            this.fwdButton.TabIndex = 2;
            this.fwdButton.Text = "FWD";
            this.fwdButton.UseVisualStyleBackColor = true;
            this.fwdButton.Click += new System.EventHandler(this.fwdButton_Click);
            // 
            // bwdButton
            // 
            this.bwdButton.Location = new System.Drawing.Point(126, 104);
            this.bwdButton.Name = "bwdButton";
            this.bwdButton.Size = new System.Drawing.Size(115, 72);
            this.bwdButton.TabIndex = 2;
            this.bwdButton.Text = "BWD";
            this.bwdButton.UseVisualStyleBackColor = true;
            this.bwdButton.Click += new System.EventHandler(this.bwdButton_Click);
            // 
            // reverseUpButton1
            // 
            this.reverseUpButton1.Location = new System.Drawing.Point(492, 25);
            this.reverseUpButton1.Name = "reverseUpButton1";
            this.reverseUpButton1.Size = new System.Drawing.Size(115, 72);
            this.reverseUpButton1.TabIndex = 2;
            this.reverseUpButton1.Text = "UP";
            this.reverseUpButton1.UseVisualStyleBackColor = true;
            this.reverseUpButton1.Click += new System.EventHandler(this.reverseUpButton1_Click);
            // 
            // reverseDownButton1
            // 
            this.reverseDownButton1.Location = new System.Drawing.Point(492, 104);
            this.reverseDownButton1.Name = "reverseDownButton1";
            this.reverseDownButton1.Size = new System.Drawing.Size(115, 72);
            this.reverseDownButton1.TabIndex = 2;
            this.reverseDownButton1.Text = "DOWN";
            this.reverseDownButton1.UseVisualStyleBackColor = true;
            this.reverseDownButton1.Click += new System.EventHandler(this.reverseDownButton1_Click);
            // 
            // reverseVacOnButton1
            // 
            this.reverseVacOnButton1.Location = new System.Drawing.Point(248, 25);
            this.reverseVacOnButton1.Name = "reverseVacOnButton1";
            this.reverseVacOnButton1.Size = new System.Drawing.Size(115, 72);
            this.reverseVacOnButton1.TabIndex = 2;
            this.reverseVacOnButton1.Text = "VAC ON";
            this.reverseVacOnButton1.UseVisualStyleBackColor = true;
            this.reverseVacOnButton1.Click += new System.EventHandler(this.reverseVacOnButton1_Click);
            // 
            // reverseVacOffButton1
            // 
            this.reverseVacOffButton1.Location = new System.Drawing.Point(248, 104);
            this.reverseVacOffButton1.Name = "reverseVacOffButton1";
            this.reverseVacOffButton1.Size = new System.Drawing.Size(115, 72);
            this.reverseVacOffButton1.TabIndex = 2;
            this.reverseVacOffButton1.Text = "VAC OFF";
            this.reverseVacOffButton1.UseVisualStyleBackColor = true;
            this.reverseVacOffButton1.Click += new System.EventHandler(this.reverseVacOffButton1_Click);
            // 
            // reverseTurnButton1
            // 
            this.reverseTurnButton1.Location = new System.Drawing.Point(370, 25);
            this.reverseTurnButton1.Name = "reverseTurnButton1";
            this.reverseTurnButton1.Size = new System.Drawing.Size(115, 72);
            this.reverseTurnButton1.TabIndex = 2;
            this.reverseTurnButton1.Text = "TURN";
            this.reverseTurnButton1.UseVisualStyleBackColor = true;
            this.reverseTurnButton1.Click += new System.EventHandler(this.reverseTurnButton1_Click);
            // 
            // reverseReturnButton1
            // 
            this.reverseReturnButton1.Location = new System.Drawing.Point(370, 104);
            this.reverseReturnButton1.Name = "reverseReturnButton1";
            this.reverseReturnButton1.Size = new System.Drawing.Size(115, 72);
            this.reverseReturnButton1.TabIndex = 2;
            this.reverseReturnButton1.Text = "RETURN";
            this.reverseReturnButton1.UseVisualStyleBackColor = true;
            this.reverseReturnButton1.Click += new System.EventHandler(this.reverseReturnButton1_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanel2.SetColumnSpan(this.label2, 3);
            this.label2.Location = new System.Drawing.Point(245, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(365, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "FRONT REVERSE";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanel2.SetColumnSpan(this.label1, 3);
            this.label1.Location = new System.Drawing.Point(611, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(367, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "FRONT REVERSE";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // reverseVacOnButton2
            // 
            this.reverseVacOnButton2.Location = new System.Drawing.Point(614, 25);
            this.reverseVacOnButton2.Name = "reverseVacOnButton2";
            this.reverseVacOnButton2.Size = new System.Drawing.Size(115, 72);
            this.reverseVacOnButton2.TabIndex = 2;
            this.reverseVacOnButton2.Text = "VAC ON";
            this.reverseVacOnButton2.UseVisualStyleBackColor = true;
            this.reverseVacOnButton2.Click += new System.EventHandler(this.reverseVacOnButton2_Click);
            // 
            // reverseVacOffButton2
            // 
            this.reverseVacOffButton2.Location = new System.Drawing.Point(614, 104);
            this.reverseVacOffButton2.Name = "reverseVacOffButton2";
            this.reverseVacOffButton2.Size = new System.Drawing.Size(115, 72);
            this.reverseVacOffButton2.TabIndex = 2;
            this.reverseVacOffButton2.Text = "VAC OFF";
            this.reverseVacOffButton2.UseVisualStyleBackColor = true;
            this.reverseVacOffButton2.Click += new System.EventHandler(this.reverseVacOffButton2_Click);
            // 
            // reverseTurnButton2
            // 
            this.reverseTurnButton2.Location = new System.Drawing.Point(736, 25);
            this.reverseTurnButton2.Name = "reverseTurnButton2";
            this.reverseTurnButton2.Size = new System.Drawing.Size(115, 72);
            this.reverseTurnButton2.TabIndex = 2;
            this.reverseTurnButton2.Text = "TURN";
            this.reverseTurnButton2.UseVisualStyleBackColor = true;
            this.reverseTurnButton2.Click += new System.EventHandler(this.reverseTurnButton2_Click);
            // 
            // reverseReturnButton2
            // 
            this.reverseReturnButton2.Location = new System.Drawing.Point(736, 104);
            this.reverseReturnButton2.Name = "reverseReturnButton2";
            this.reverseReturnButton2.Size = new System.Drawing.Size(115, 72);
            this.reverseReturnButton2.TabIndex = 2;
            this.reverseReturnButton2.Text = "RETURN";
            this.reverseReturnButton2.UseVisualStyleBackColor = true;
            this.reverseReturnButton2.Click += new System.EventHandler(this.reverseReturnButton2_Click);
            // 
            // reverseUpButton2
            // 
            this.reverseUpButton2.Location = new System.Drawing.Point(858, 25);
            this.reverseUpButton2.Name = "reverseUpButton2";
            this.reverseUpButton2.Size = new System.Drawing.Size(115, 72);
            this.reverseUpButton2.TabIndex = 2;
            this.reverseUpButton2.Text = "UP";
            this.reverseUpButton2.UseVisualStyleBackColor = true;
            this.reverseUpButton2.Click += new System.EventHandler(this.reverseUpButton2_Click);
            // 
            // reverseDownButton2
            // 
            this.reverseDownButton2.Location = new System.Drawing.Point(858, 104);
            this.reverseDownButton2.Name = "reverseDownButton2";
            this.reverseDownButton2.Size = new System.Drawing.Size(115, 72);
            this.reverseDownButton2.TabIndex = 2;
            this.reverseDownButton2.Text = "DOWN";
            this.reverseDownButton2.UseVisualStyleBackColor = true;
            this.reverseDownButton2.Click += new System.EventHandler(this.reverseDownButton2_Click);
            // 
            // FormTeachOutPP
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1003, 561);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.jogControl1);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Font = new System.Drawing.Font("SamsungOne 800", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormTeachOutPP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormTeachPP";
            this.Load += new System.EventHandler(this.FormTeachPP_Load);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button xSaveButton;
        private System.Windows.Forms.Button moveButton;
        private System.Windows.Forms.Button zSaveButton;
        private CSourceGrid motorPosGrid;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Timer ui_timer;
        private JogControl jogControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ppVacOnButton;
        private System.Windows.Forms.Button ppVacOffButton;
        private System.Windows.Forms.Button fwdButton;
        private System.Windows.Forms.Button bwdButton;
        private System.Windows.Forms.Button reverseUpButton1;
        private System.Windows.Forms.Button reverseDownButton1;
        private System.Windows.Forms.Button reverseVacOnButton1;
        private System.Windows.Forms.Button reverseVacOffButton1;
        private System.Windows.Forms.Button reverseTurnButton1;
        private System.Windows.Forms.Button reverseReturnButton1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button reverseVacOnButton2;
        private System.Windows.Forms.Button reverseVacOffButton2;
        private System.Windows.Forms.Button reverseTurnButton2;
        private System.Windows.Forms.Button reverseReturnButton2;
        private System.Windows.Forms.Button reverseUpButton2;
        private System.Windows.Forms.Button reverseDownButton2;
    }
}