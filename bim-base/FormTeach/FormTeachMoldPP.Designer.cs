namespace bim_base
{
    partial class FormTeachMoldPP
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
            this.xbSaveButton = new System.Windows.Forms.Button();
            this.z1SaveButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.z2SaveButton = new System.Windows.Forms.Button();
            this.motorPosGrid = new CSourceGrid();
            this.ui_timer = new System.Windows.Forms.Timer(this.components);
            this.jogControl1 = new bim_base.JogControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.leftGripOnButton = new System.Windows.Forms.Button();
            this.leftGripOffButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.holdOnButton = new System.Windows.Forms.Button();
            this.holdOffButton = new System.Windows.Forms.Button();
            this.openerFwdButton = new System.Windows.Forms.Button();
            this.openerBwdButton = new System.Windows.Forms.Button();
            this.coverLockButton = new System.Windows.Forms.Button();
            this.coverUnlockButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rightGripOnButton = new System.Windows.Forms.Button();
            this.rightGripOffButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
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
            this.xSaveButton.Text = "MOLD X Save";
            this.xSaveButton.UseVisualStyleBackColor = true;
            this.xSaveButton.Click += new System.EventHandler(this.xySaveButton_Click);
            // 
            // moveButton
            // 
            this.moveButton.Location = new System.Drawing.Point(583, 312);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(141, 35);
            this.moveButton.TabIndex = 22;
            this.moveButton.Text = "Move";
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.Click += new System.EventHandler(this.moveButton_Click);
            // 
            // xbSaveButton
            // 
            this.xbSaveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xbSaveButton.Location = new System.Drawing.Point(148, 312);
            this.xbSaveButton.Name = "xbSaveButton";
            this.xbSaveButton.Size = new System.Drawing.Size(139, 35);
            this.xbSaveButton.TabIndex = 18;
            this.xbSaveButton.Text = "BASE X Save";
            this.xbSaveButton.UseVisualStyleBackColor = true;
            this.xbSaveButton.Click += new System.EventHandler(this.xbSaveButton_Click);
            // 
            // z1SaveButton
            // 
            this.z1SaveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.z1SaveButton.Location = new System.Drawing.Point(293, 312);
            this.z1SaveButton.Name = "z1SaveButton";
            this.z1SaveButton.Size = new System.Drawing.Size(139, 35);
            this.z1SaveButton.TabIndex = 17;
            this.z1SaveButton.Text = "Z1 Save";
            this.z1SaveButton.UseVisualStyleBackColor = true;
            this.z1SaveButton.Click += new System.EventHandler(this.z1SaveButton_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Controls.Add(this.z2SaveButton, 3, 3);
            this.tableLayoutPanel3.Controls.Add(this.xSaveButton, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.xbSaveButton, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.z1SaveButton, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.motorPosGrid, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.moveButton, 4, 3);
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
            // z2SaveButton
            // 
            this.z2SaveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.z2SaveButton.Location = new System.Drawing.Point(438, 312);
            this.z2SaveButton.Name = "z2SaveButton";
            this.z2SaveButton.Size = new System.Drawing.Size(139, 35);
            this.z2SaveButton.TabIndex = 23;
            this.z2SaveButton.Text = "Z2 Save";
            this.z2SaveButton.UseVisualStyleBackColor = true;
            this.z2SaveButton.Click += new System.EventHandler(this.z2SaveButton_Click);
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
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.leftGripOnButton, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.leftGripOffButton, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.upButton, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.downButton, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.holdOnButton, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.holdOffButton, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.openerFwdButton, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.openerBwdButton, 4, 2);
            this.tableLayoutPanel2.Controls.Add(this.coverLockButton, 5, 1);
            this.tableLayoutPanel2.Controls.Add(this.coverUnlockButton, 5, 2);
            this.tableLayoutPanel2.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.rightGripOnButton, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.rightGripOffButton, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 3, 0);
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
            this.tableLayoutPanel2.TabIndex = 59;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(1, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "LEFT PP";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // leftGripOnButton
            // 
            this.leftGripOnButton.Location = new System.Drawing.Point(4, 25);
            this.leftGripOnButton.Name = "leftGripOnButton";
            this.leftGripOnButton.Size = new System.Drawing.Size(155, 72);
            this.leftGripOnButton.TabIndex = 2;
            this.leftGripOnButton.Text = "GRIP ON";
            this.leftGripOnButton.UseVisualStyleBackColor = true;
            this.leftGripOnButton.Click += new System.EventHandler(this.leftGripOnButton_Click);
            // 
            // leftGripOffButton
            // 
            this.leftGripOffButton.Location = new System.Drawing.Point(4, 104);
            this.leftGripOffButton.Name = "leftGripOffButton";
            this.leftGripOffButton.Size = new System.Drawing.Size(155, 73);
            this.leftGripOffButton.TabIndex = 3;
            this.leftGripOffButton.Text = "GRIP OFF";
            this.leftGripOffButton.UseVisualStyleBackColor = true;
            this.leftGripOffButton.Click += new System.EventHandler(this.leftGripOffButton_Click);
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
            // holdOnButton
            // 
            this.holdOnButton.Location = new System.Drawing.Point(490, 25);
            this.holdOnButton.Name = "holdOnButton";
            this.holdOnButton.Size = new System.Drawing.Size(155, 72);
            this.holdOnButton.TabIndex = 2;
            this.holdOnButton.Text = "HOLD ON";
            this.holdOnButton.UseVisualStyleBackColor = true;
            this.holdOnButton.Click += new System.EventHandler(this.holdOnButton_Click);
            // 
            // holdOffButton
            // 
            this.holdOffButton.Location = new System.Drawing.Point(490, 104);
            this.holdOffButton.Name = "holdOffButton";
            this.holdOffButton.Size = new System.Drawing.Size(155, 72);
            this.holdOffButton.TabIndex = 2;
            this.holdOffButton.Text = "HOLD OFF";
            this.holdOffButton.UseVisualStyleBackColor = true;
            this.holdOffButton.Click += new System.EventHandler(this.holdOffButton_Click);
            // 
            // openerFwdButton
            // 
            this.openerFwdButton.Location = new System.Drawing.Point(652, 25);
            this.openerFwdButton.Name = "openerFwdButton";
            this.openerFwdButton.Size = new System.Drawing.Size(155, 72);
            this.openerFwdButton.TabIndex = 2;
            this.openerFwdButton.Text = "OPENER FWD";
            this.openerFwdButton.UseVisualStyleBackColor = true;
            this.openerFwdButton.Click += new System.EventHandler(this.openerFwdButton_Click);
            // 
            // openerBwdButton
            // 
            this.openerBwdButton.Location = new System.Drawing.Point(652, 104);
            this.openerBwdButton.Name = "openerBwdButton";
            this.openerBwdButton.Size = new System.Drawing.Size(155, 72);
            this.openerBwdButton.TabIndex = 2;
            this.openerBwdButton.Text = "OPENER BWD";
            this.openerBwdButton.UseVisualStyleBackColor = true;
            this.openerBwdButton.Click += new System.EventHandler(this.openerBwdButton_Click);
            // 
            // coverLockButton
            // 
            this.coverLockButton.Location = new System.Drawing.Point(814, 25);
            this.coverLockButton.Name = "coverLockButton";
            this.coverLockButton.Size = new System.Drawing.Size(161, 72);
            this.coverLockButton.TabIndex = 2;
            this.coverLockButton.Text = "COVER LOCK";
            this.coverLockButton.UseVisualStyleBackColor = true;
            this.coverLockButton.Click += new System.EventHandler(this.coverLockButton_Click);
            // 
            // coverUnlockButton
            // 
            this.coverUnlockButton.Location = new System.Drawing.Point(814, 104);
            this.coverUnlockButton.Name = "coverUnlockButton";
            this.coverUnlockButton.Size = new System.Drawing.Size(161, 73);
            this.coverUnlockButton.TabIndex = 2;
            this.coverUnlockButton.Text = "COVER UNLOCK";
            this.coverUnlockButton.UseVisualStyleBackColor = true;
            this.coverUnlockButton.Click += new System.EventHandler(this.coverUnlockButton_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(163, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "RIGHT PP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rightGripOnButton
            // 
            this.rightGripOnButton.Location = new System.Drawing.Point(166, 25);
            this.rightGripOnButton.Name = "rightGripOnButton";
            this.rightGripOnButton.Size = new System.Drawing.Size(155, 72);
            this.rightGripOnButton.TabIndex = 2;
            this.rightGripOnButton.Text = "GRIP ON";
            this.rightGripOnButton.UseVisualStyleBackColor = true;
            this.rightGripOnButton.Click += new System.EventHandler(this.rightGripOnButton_Click);
            // 
            // rightGripOffButton
            // 
            this.rightGripOffButton.Location = new System.Drawing.Point(166, 104);
            this.rightGripOffButton.Name = "rightGripOffButton";
            this.rightGripOffButton.Size = new System.Drawing.Size(155, 73);
            this.rightGripOffButton.TabIndex = 3;
            this.rightGripOffButton.Text = "GRIP OFF";
            this.rightGripOffButton.UseVisualStyleBackColor = true;
            this.rightGripOffButton.Click += new System.EventHandler(this.rightGripOffButton_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(325, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "SHUTTLE";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanel2.SetColumnSpan(this.label4, 3);
            this.label4.Location = new System.Drawing.Point(487, 1);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(491, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "STAGE";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormTeachMoldPP
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1003, 561);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.jogControl1);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormTeachMoldPP";
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
        private System.Windows.Forms.Button xbSaveButton;
        private System.Windows.Forms.Button z1SaveButton;
        private CSourceGrid motorPosGrid;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Timer ui_timer;
        private JogControl jogControl1;
        private System.Windows.Forms.Button z2SaveButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button leftGripOnButton;
        private System.Windows.Forms.Button leftGripOffButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button holdOnButton;
        private System.Windows.Forms.Button holdOffButton;
        private System.Windows.Forms.Button openerFwdButton;
        private System.Windows.Forms.Button openerBwdButton;
        private System.Windows.Forms.Button coverLockButton;
        private System.Windows.Forms.Button coverUnlockButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button rightGripOnButton;
        private System.Windows.Forms.Button rightGripOffButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
    }
}