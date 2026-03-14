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
            this.label3 = new System.Windows.Forms.Label();
            this.gripOnButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.gripOffButton = new System.Windows.Forms.Button();
            this.xSaveButton = new System.Windows.Forms.Button();
            this.moveButton = new System.Windows.Forms.Button();
            this.zUpButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.slot1PickButton = new System.Windows.Forms.Button();
            this.zDownButton = new System.Windows.Forms.Button();
            this.zSaveButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.inCvPickButton = new System.Windows.Forms.Button();
            this.slot2PickButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.outCVDropButton = new System.Windows.Forms.Button();
            this.nextCVDropButton = new System.Windows.Forms.Button();
            this.slot1DropButton = new System.Windows.Forms.Button();
            this.slot2DropButton = new System.Windows.Forms.Button();
            this.slot3DropButton = new System.Windows.Forms.Button();
            this.slot3PickButotn = new System.Windows.Forms.Button();
            this.slot4DropButton = new System.Windows.Forms.Button();
            this.slot4PickButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.ui_timer = new System.Windows.Forms.Timer(this.components);
            this.jogControl1 = new bim_base.JogControl();
            this.motorPosGrid = new CSourceGrid();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanel2.SetColumnSpan(this.label3, 2);
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(1, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(360, 30);
            this.label3.TabIndex = 0;
            this.label3.Text = "Manual";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gripOnButton
            // 
            this.gripOnButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gripOnButton.Location = new System.Drawing.Point(4, 35);
            this.gripOnButton.Name = "gripOnButton";
            this.gripOnButton.Size = new System.Drawing.Size(173, 42);
            this.gripOnButton.TabIndex = 2;
            this.gripOnButton.Text = "GRIP ON";
            this.gripOnButton.UseVisualStyleBackColor = true;
            this.gripOnButton.Click += new System.EventHandler(this.gripOnButton_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.gripOnButton, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.gripOffButton, 1, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 573);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(362, 81);
            this.tableLayoutPanel2.TabIndex = 57;
            this.tableLayoutPanel2.Visible = false;
            // 
            // gripOffButton
            // 
            this.gripOffButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gripOffButton.Location = new System.Drawing.Point(184, 35);
            this.gripOffButton.Name = "gripOffButton";
            this.gripOffButton.Size = new System.Drawing.Size(174, 42);
            this.gripOffButton.TabIndex = 2;
            this.gripOffButton.Text = "GRIP OFF";
            this.gripOffButton.UseVisualStyleBackColor = true;
            this.gripOffButton.Click += new System.EventHandler(this.gripOffButton_Click);
            // 
            // xSaveButton
            // 
            this.xSaveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xSaveButton.Location = new System.Drawing.Point(3, 300);
            this.xSaveButton.Name = "xSaveButton";
            this.xSaveButton.Size = new System.Drawing.Size(130, 67);
            this.xSaveButton.TabIndex = 19;
            this.xSaveButton.Text = "X/Y Save";
            this.xSaveButton.UseVisualStyleBackColor = true;
            this.xSaveButton.Click += new System.EventHandler(this.xySaveButton_Click);
            // 
            // moveButton
            // 
            this.moveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.moveButton.Location = new System.Drawing.Point(411, 3);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(93, 93);
            this.moveButton.TabIndex = 22;
            this.moveButton.Text = "Move";
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.Click += new System.EventHandler(this.moveButton_Click);
            // 
            // zUpButton
            // 
            this.zUpButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zUpButton.Location = new System.Drawing.Point(411, 102);
            this.zUpButton.Name = "zUpButton";
            this.zUpButton.Size = new System.Drawing.Size(93, 93);
            this.zUpButton.TabIndex = 21;
            this.zUpButton.Text = "Z Up";
            this.zUpButton.UseVisualStyleBackColor = true;
            this.zUpButton.Click += new System.EventHandler(this.zUpButton_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanel1.SetColumnSpan(this.label2, 6);
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(1, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(505, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "Action";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // slot1PickButton
            // 
            this.slot1PickButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slot1PickButton.Location = new System.Drawing.Point(88, 108);
            this.slot1PickButton.Name = "slot1PickButton";
            this.slot1PickButton.Size = new System.Drawing.Size(77, 67);
            this.slot1PickButton.TabIndex = 1;
            this.slot1PickButton.Text = "#1 Pick";
            this.slot1PickButton.UseVisualStyleBackColor = true;
            // 
            // zDownButton
            // 
            this.zDownButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zDownButton.Location = new System.Drawing.Point(411, 201);
            this.zDownButton.Name = "zDownButton";
            this.zDownButton.Size = new System.Drawing.Size(93, 93);
            this.zDownButton.TabIndex = 20;
            this.zDownButton.Text = "Z Down";
            this.zDownButton.UseVisualStyleBackColor = true;
            this.zDownButton.Click += new System.EventHandler(this.zDownButton_Click);
            // 
            // zSaveButton
            // 
            this.zSaveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zSaveButton.Location = new System.Drawing.Point(139, 300);
            this.zSaveButton.Name = "zSaveButton";
            this.zSaveButton.Size = new System.Drawing.Size(130, 67);
            this.zSaveButton.TabIndex = 18;
            this.zSaveButton.Text = "Z Save";
            this.zSaveButton.UseVisualStyleBackColor = true;
            this.zSaveButton.Click += new System.EventHandler(this.zSaveButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveButton.Location = new System.Drawing.Point(275, 300);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(130, 67);
            this.saveButton.TabIndex = 17;
            this.saveButton.Text = "X/Y/Z Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // inCvPickButton
            // 
            this.inCvPickButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inCvPickButton.Location = new System.Drawing.Point(4, 35);
            this.inCvPickButton.Name = "inCvPickButton";
            this.inCvPickButton.Size = new System.Drawing.Size(77, 66);
            this.inCvPickButton.TabIndex = 1;
            this.inCvPickButton.Text = "In C/V Pick";
            this.inCvPickButton.UseVisualStyleBackColor = true;
            // 
            // slot2PickButton
            // 
            this.slot2PickButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slot2PickButton.Location = new System.Drawing.Point(172, 108);
            this.slot2PickButton.Name = "slot2PickButton";
            this.slot2PickButton.Size = new System.Drawing.Size(77, 67);
            this.slot2PickButton.TabIndex = 2;
            this.slot2PickButton.Text = "#2 Pick";
            this.slot2PickButton.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Controls.Add(this.inCvPickButton, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.outCVDropButton, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.nextCVDropButton, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.slot1DropButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.slot1PickButton, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.slot2DropButton, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.slot2PickButton, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.slot3DropButton, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.slot3PickButotn, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.slot4DropButton, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.slot4PickButton, 4, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 388);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(507, 179);
            this.tableLayoutPanel1.TabIndex = 55;
            this.tableLayoutPanel1.Visible = false;
            // 
            // outCVDropButton
            // 
            this.outCVDropButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outCVDropButton.Location = new System.Drawing.Point(424, 35);
            this.outCVDropButton.Name = "outCVDropButton";
            this.outCVDropButton.Size = new System.Drawing.Size(79, 66);
            this.outCVDropButton.TabIndex = 2;
            this.outCVDropButton.Text = "Out C/V Drop";
            this.outCVDropButton.UseVisualStyleBackColor = true;
            // 
            // nextCVDropButton
            // 
            this.nextCVDropButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nextCVDropButton.Location = new System.Drawing.Point(424, 108);
            this.nextCVDropButton.Name = "nextCVDropButton";
            this.nextCVDropButton.Size = new System.Drawing.Size(79, 67);
            this.nextCVDropButton.TabIndex = 2;
            this.nextCVDropButton.Text = "Next C/V Drop";
            this.nextCVDropButton.UseVisualStyleBackColor = true;
            // 
            // slot1DropButton
            // 
            this.slot1DropButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slot1DropButton.Location = new System.Drawing.Point(88, 35);
            this.slot1DropButton.Name = "slot1DropButton";
            this.slot1DropButton.Size = new System.Drawing.Size(77, 66);
            this.slot1DropButton.TabIndex = 1;
            this.slot1DropButton.Text = "#1 Drop";
            this.slot1DropButton.UseVisualStyleBackColor = true;
            // 
            // slot2DropButton
            // 
            this.slot2DropButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slot2DropButton.Location = new System.Drawing.Point(172, 35);
            this.slot2DropButton.Name = "slot2DropButton";
            this.slot2DropButton.Size = new System.Drawing.Size(77, 66);
            this.slot2DropButton.TabIndex = 2;
            this.slot2DropButton.Text = "#2 Drop";
            this.slot2DropButton.UseVisualStyleBackColor = true;
            // 
            // slot3DropButton
            // 
            this.slot3DropButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slot3DropButton.Location = new System.Drawing.Point(256, 35);
            this.slot3DropButton.Name = "slot3DropButton";
            this.slot3DropButton.Size = new System.Drawing.Size(77, 66);
            this.slot3DropButton.TabIndex = 2;
            this.slot3DropButton.Text = "#3 Drop";
            this.slot3DropButton.UseVisualStyleBackColor = true;
            // 
            // slot3PickButotn
            // 
            this.slot3PickButotn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slot3PickButotn.Location = new System.Drawing.Point(256, 108);
            this.slot3PickButotn.Name = "slot3PickButotn";
            this.slot3PickButotn.Size = new System.Drawing.Size(77, 67);
            this.slot3PickButotn.TabIndex = 2;
            this.slot3PickButotn.Text = "#3 Pick";
            this.slot3PickButotn.UseVisualStyleBackColor = true;
            // 
            // slot4DropButton
            // 
            this.slot4DropButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slot4DropButton.Location = new System.Drawing.Point(340, 35);
            this.slot4DropButton.Name = "slot4DropButton";
            this.slot4DropButton.Size = new System.Drawing.Size(77, 66);
            this.slot4DropButton.TabIndex = 2;
            this.slot4DropButton.Text = "#4 Drop";
            this.slot4DropButton.UseVisualStyleBackColor = true;
            // 
            // slot4PickButton
            // 
            this.slot4PickButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slot4PickButton.Location = new System.Drawing.Point(340, 108);
            this.slot4PickButton.Name = "slot4PickButton";
            this.slot4PickButton.Size = new System.Drawing.Size(77, 67);
            this.slot4PickButton.TabIndex = 2;
            this.slot4PickButton.Text = "#4 Pick";
            this.slot4PickButton.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.tableLayoutPanel3.Controls.Add(this.xSaveButton, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.moveButton, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.zUpButton, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.zDownButton, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.zSaveButton, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.saveButton, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.motorPosGrid, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(507, 370);
            this.tableLayoutPanel3.TabIndex = 54;
            // 
            // ui_timer
            // 
            this.ui_timer.Tick += new System.EventHandler(this.ui_timer_Tick);
            // 
            // jogControl1
            // 
            this.jogControl1.BackColor = System.Drawing.Color.White;
            this.jogControl1.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jogControl1.Location = new System.Drawing.Point(568, 12);
            this.jogControl1.Name = "jogControl1";
            this.jogControl1.Size = new System.Drawing.Size(246, 370);
            this.jogControl1.TabIndex = 58;
            // 
            // motorPosGrid
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.motorPosGrid, 3);
            this.motorPosGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.motorPosGrid.EnableSort = true;
            this.motorPosGrid.Location = new System.Drawing.Point(3, 3);
            this.motorPosGrid.Name = "motorPosGrid";
            this.motorPosGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.tableLayoutPanel3.SetRowSpan(this.motorPosGrid, 3);
            this.motorPosGrid.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.motorPosGrid.Size = new System.Drawing.Size(402, 291);
            this.motorPosGrid.TabIndex = 12;
            this.motorPosGrid.TabStop = true;
            this.motorPosGrid.ToolTipText = "";
            // 
            // FormTeachOutPP
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(885, 659);
            this.Controls.Add(this.jogControl1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Font = new System.Drawing.Font("SamsungOne 800", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormTeachOutPP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormTeachPP";
            this.Load += new System.EventHandler(this.FormTeachPP_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button gripOnButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button gripOffButton;
        private System.Windows.Forms.Button xSaveButton;
        private System.Windows.Forms.Button moveButton;
        private System.Windows.Forms.Button zUpButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button inCvPickButton;
        private System.Windows.Forms.Button slot2PickButton;
        private System.Windows.Forms.Button slot1PickButton;
        private System.Windows.Forms.Button slot1DropButton;
        private System.Windows.Forms.Button zDownButton;
        private System.Windows.Forms.Button zSaveButton;
        private System.Windows.Forms.Button saveButton;
        private CSourceGrid motorPosGrid;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button slot2DropButton;
        private System.Windows.Forms.Button slot3PickButotn;
        private System.Windows.Forms.Button slot3DropButton;
        private System.Windows.Forms.Button slot4PickButton;
        private System.Windows.Forms.Button slot4DropButton;
        private System.Windows.Forms.Button outCVDropButton;
        private System.Windows.Forms.Button nextCVDropButton;
        private System.Windows.Forms.Timer ui_timer;
        private JogControl jogControl1;
    }
}