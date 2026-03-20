namespace bim_base
{
    partial class FormTeachInPP
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
            this.label3 = new System.Windows.Forms.Label();
            this.ppGripOnButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ppGripOffButton = new System.Windows.Forms.Button();
            this.fwdButton = new System.Windows.Forms.Button();
            this.bwdButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.reverseGripOnButton = new System.Windows.Forms.Button();
            this.reverseGripOffButton = new System.Windows.Forms.Button();
            this.reverseTurnButton = new System.Windows.Forms.Button();
            this.reverseReturnButton = new System.Windows.Forms.Button();
            this.reverseUpButton = new System.Windows.Forms.Button();
            this.reverseDownButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ySaveButton = new System.Windows.Forms.Button();
            this.moveButton = new System.Windows.Forms.Button();
            this.zSaveButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.motorPosGrid = new CSourceGrid();
            this.ui_timer = new System.Windows.Forms.Timer(this.components);
            this.jogControl1 = new bim_base.JogControl();
            this.mySqlDataAdapter1 = new MySqlConnector.MySqlDataAdapter();
            this.cSourceGrid1 = new CSourceGrid();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.motorPosGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(1, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "PP";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ppGripOnButton
            // 
            this.ppGripOnButton.Location = new System.Drawing.Point(4, 25);
            this.ppGripOnButton.Name = "ppGripOnButton";
            this.ppGripOnButton.Size = new System.Drawing.Size(155, 72);
            this.ppGripOnButton.TabIndex = 2;
            this.ppGripOnButton.Text = "GRIP ON";
            this.ppGripOnButton.UseVisualStyleBackColor = true;
            this.ppGripOnButton.Click += new System.EventHandler(this.ppGripOnButton_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.ppGripOnButton, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.ppGripOffButton, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.fwdButton, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.bwdButton, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.upButton, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.downButton, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.reverseGripOnButton, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.reverseGripOffButton, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.reverseTurnButton, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.reverseReturnButton, 4, 2);
            this.tableLayoutPanel2.Controls.Add(this.reverseUpButton, 5, 1);
            this.tableLayoutPanel2.Controls.Add(this.reverseDownButton, 5, 2);
            this.tableLayoutPanel2.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 3, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 368);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(979, 181);
            this.tableLayoutPanel2.TabIndex = 57;
            // 
            // ppGripOffButton
            // 
            this.ppGripOffButton.Location = new System.Drawing.Point(4, 104);
            this.ppGripOffButton.Name = "ppGripOffButton";
            this.ppGripOffButton.Size = new System.Drawing.Size(155, 73);
            this.ppGripOffButton.TabIndex = 3;
            this.ppGripOffButton.Text = "GRIP OFF";
            this.ppGripOffButton.UseVisualStyleBackColor = true;
            this.ppGripOffButton.Click += new System.EventHandler(this.ppGripOffButton_Click);
            // 
            // fwdButton
            // 
            this.fwdButton.Location = new System.Drawing.Point(166, 25);
            this.fwdButton.Name = "fwdButton";
            this.fwdButton.Size = new System.Drawing.Size(155, 72);
            this.fwdButton.TabIndex = 2;
            this.fwdButton.Text = "FWD";
            this.fwdButton.UseVisualStyleBackColor = true;
            this.fwdButton.Click += new System.EventHandler(this.fwdButton_Click);
            // 
            // bwdButton
            // 
            this.bwdButton.Location = new System.Drawing.Point(166, 104);
            this.bwdButton.Name = "bwdButton";
            this.bwdButton.Size = new System.Drawing.Size(155, 72);
            this.bwdButton.TabIndex = 2;
            this.bwdButton.Text = "BWD";
            this.bwdButton.UseVisualStyleBackColor = true;
            this.bwdButton.Click += new System.EventHandler(this.bwdButton_Click);
            // 
            // upButton
            // 
            this.upButton.Location = new System.Drawing.Point(328, 25);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(155, 72);
            this.upButton.TabIndex = 2;
            this.upButton.Text = "UP";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // downButton
            // 
            this.downButton.Location = new System.Drawing.Point(328, 104);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(155, 72);
            this.downButton.TabIndex = 2;
            this.downButton.Text = "DOWN";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // reverseGripOnButton
            // 
            this.reverseGripOnButton.Location = new System.Drawing.Point(490, 25);
            this.reverseGripOnButton.Name = "reverseGripOnButton";
            this.reverseGripOnButton.Size = new System.Drawing.Size(155, 72);
            this.reverseGripOnButton.TabIndex = 2;
            this.reverseGripOnButton.Text = "GRIP ON";
            this.reverseGripOnButton.UseVisualStyleBackColor = true;
            this.reverseGripOnButton.Click += new System.EventHandler(this.reverseGripOnButton_Click);
            // 
            // reverseGripOffButton
            // 
            this.reverseGripOffButton.Location = new System.Drawing.Point(490, 104);
            this.reverseGripOffButton.Name = "reverseGripOffButton";
            this.reverseGripOffButton.Size = new System.Drawing.Size(155, 73);
            this.reverseGripOffButton.TabIndex = 2;
            this.reverseGripOffButton.Text = "GRIP OFF";
            this.reverseGripOffButton.UseVisualStyleBackColor = true;
            this.reverseGripOffButton.Click += new System.EventHandler(this.reverseGripOffButton_Click);
            // 
            // reverseTurnButton
            // 
            this.reverseTurnButton.Location = new System.Drawing.Point(652, 25);
            this.reverseTurnButton.Name = "reverseTurnButton";
            this.reverseTurnButton.Size = new System.Drawing.Size(155, 72);
            this.reverseTurnButton.TabIndex = 2;
            this.reverseTurnButton.Text = "TURN";
            this.reverseTurnButton.UseVisualStyleBackColor = true;
            this.reverseTurnButton.Click += new System.EventHandler(this.reverseTurnButton_Click);
            // 
            // reverseReturnButton
            // 
            this.reverseReturnButton.Location = new System.Drawing.Point(652, 104);
            this.reverseReturnButton.Name = "reverseReturnButton";
            this.reverseReturnButton.Size = new System.Drawing.Size(155, 72);
            this.reverseReturnButton.TabIndex = 2;
            this.reverseReturnButton.Text = "RETURN";
            this.reverseReturnButton.UseVisualStyleBackColor = true;
            this.reverseReturnButton.Click += new System.EventHandler(this.reverseReturnButton_Click);
            // 
            // reverseUpButton
            // 
            this.reverseUpButton.Location = new System.Drawing.Point(814, 25);
            this.reverseUpButton.Name = "reverseUpButton";
            this.reverseUpButton.Size = new System.Drawing.Size(161, 72);
            this.reverseUpButton.TabIndex = 2;
            this.reverseUpButton.Text = "UP";
            this.reverseUpButton.UseVisualStyleBackColor = true;
            this.reverseUpButton.Click += new System.EventHandler(this.reverseUpButton_Click);
            // 
            // reverseDownButton
            // 
            this.reverseDownButton.Location = new System.Drawing.Point(814, 104);
            this.reverseDownButton.Name = "reverseDownButton";
            this.reverseDownButton.Size = new System.Drawing.Size(161, 72);
            this.reverseDownButton.TabIndex = 2;
            this.reverseDownButton.Text = "DOWN";
            this.reverseDownButton.UseVisualStyleBackColor = true;
            this.reverseDownButton.Click += new System.EventHandler(this.reverseDownButton_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanel2.SetColumnSpan(this.label1, 2);
            this.label1.Location = new System.Drawing.Point(163, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(323, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "ALIGN";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanel2.SetColumnSpan(this.label2, 3);
            this.label2.Location = new System.Drawing.Point(487, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(491, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "REVERSE";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ySaveButton
            // 
            this.ySaveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ySaveButton.Location = new System.Drawing.Point(3, 312);
            this.ySaveButton.Name = "ySaveButton";
            this.ySaveButton.Size = new System.Drawing.Size(139, 35);
            this.ySaveButton.TabIndex = 19;
            this.ySaveButton.Text = "Y Save";
            this.ySaveButton.UseVisualStyleBackColor = true;
            this.ySaveButton.Click += new System.EventHandler(this.xySaveButton_Click);
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
            this.tableLayoutPanel3.Controls.Add(this.ySaveButton, 0, 3);
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
            this.motorPosGrid.Controls.Add(this.cSourceGrid1);
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
            this.jogControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jogControl1.Location = new System.Drawing.Point(745, 12);
            this.jogControl1.Name = "jogControl1";
            this.jogControl1.Size = new System.Drawing.Size(246, 350);
            this.jogControl1.TabIndex = 58;
            // 
            // mySqlDataAdapter1
            // 
            this.mySqlDataAdapter1.DeleteCommand = null;
            this.mySqlDataAdapter1.InsertCommand = null;
            this.mySqlDataAdapter1.SelectCommand = null;
            this.mySqlDataAdapter1.UpdateBatchSize = 0;
            this.mySqlDataAdapter1.UpdateCommand = null;
            // 
            // cSourceGrid1
            // 
            this.cSourceGrid1.EnableSort = true;
            this.cSourceGrid1.Location = new System.Drawing.Point(504, 195);
            this.cSourceGrid1.Name = "cSourceGrid1";
            this.cSourceGrid1.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.cSourceGrid1.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.cSourceGrid1.Size = new System.Drawing.Size(200, 100);
            this.cSourceGrid1.TabIndex = 4;
            this.cSourceGrid1.TabStop = true;
            this.cSourceGrid1.ToolTipText = "";
            // 
            // FormTeachInPP
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1003, 561);
            this.Controls.Add(this.jogControl1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormTeachInPP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormTeachPP";
            this.Load += new System.EventHandler(this.FormTeachPP_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.motorPosGrid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ppGripOnButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button ySaveButton;
        private System.Windows.Forms.Button moveButton;
        private System.Windows.Forms.Button zSaveButton;
        private CSourceGrid motorPosGrid;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Timer ui_timer;
        private JogControl jogControl1;
        private System.Windows.Forms.Button ppGripOffButton;
        private System.Windows.Forms.Button fwdButton;
        private System.Windows.Forms.Button bwdButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button reverseGripOnButton;
        private System.Windows.Forms.Button reverseGripOffButton;
        private System.Windows.Forms.Button reverseTurnButton;
        private System.Windows.Forms.Button reverseReturnButton;
        private System.Windows.Forms.Button reverseUpButton;
        private System.Windows.Forms.Button reverseDownButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private MySqlConnector.MySqlDataAdapter mySqlDataAdapter1;
        private CSourceGrid cSourceGrid1;
    }
}