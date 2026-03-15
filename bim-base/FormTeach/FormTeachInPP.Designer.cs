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
            this.gripOnButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.gripOffButton = new System.Windows.Forms.Button();
            this.ySaveButton = new System.Windows.Forms.Button();
            this.moveButton = new System.Windows.Forms.Button();
            this.zSaveButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.ui_timer = new System.Windows.Forms.Timer(this.components);
            this.jogControl1 = new bim_base.JogControl();
            this.motorPosGrid = new CSourceGrid();
            this.tableLayoutPanel2.SuspendLayout();
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
            this.label3.Size = new System.Drawing.Size(244, 30);
            this.label3.TabIndex = 0;
            this.label3.Text = "Manual";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gripOnButton
            // 
            this.gripOnButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gripOnButton.Location = new System.Drawing.Point(4, 35);
            this.gripOnButton.Name = "gripOnButton";
            this.gripOnButton.Size = new System.Drawing.Size(115, 42);
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(748, 404);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(246, 81);
            this.tableLayoutPanel2.TabIndex = 57;
            // 
            // gripOffButton
            // 
            this.gripOffButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gripOffButton.Location = new System.Drawing.Point(126, 35);
            this.gripOffButton.Name = "gripOffButton";
            this.gripOffButton.Size = new System.Drawing.Size(116, 42);
            this.gripOffButton.TabIndex = 2;
            this.gripOffButton.Text = "GRIP OFF";
            this.gripOffButton.UseVisualStyleBackColor = true;
            this.gripOffButton.Click += new System.EventHandler(this.gripOffButton_Click);
            // 
            // ySaveButton
            // 
            this.ySaveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ySaveButton.Location = new System.Drawing.Point(3, 435);
            this.ySaveButton.Name = "ySaveButton";
            this.ySaveButton.Size = new System.Drawing.Size(139, 99);
            this.ySaveButton.TabIndex = 19;
            this.ySaveButton.Text = "Y Save";
            this.ySaveButton.UseVisualStyleBackColor = true;
            this.ySaveButton.Click += new System.EventHandler(this.xySaveButton_Click);
            // 
            // moveButton
            // 
            this.moveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.moveButton.Location = new System.Drawing.Point(583, 435);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(141, 99);
            this.moveButton.TabIndex = 22;
            this.moveButton.Text = "Move";
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.Click += new System.EventHandler(this.moveButton_Click);
            // 
            // zSaveButton
            // 
            this.zSaveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zSaveButton.Location = new System.Drawing.Point(148, 435);
            this.zSaveButton.Name = "zSaveButton";
            this.zSaveButton.Size = new System.Drawing.Size(139, 99);
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
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(727, 537);
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
            this.jogControl1.Location = new System.Drawing.Point(745, 12);
            this.jogControl1.Name = "jogControl1";
            this.jogControl1.Size = new System.Drawing.Size(246, 370);
            this.jogControl1.TabIndex = 58;
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
            this.motorPosGrid.Size = new System.Drawing.Size(721, 426);
            this.motorPosGrid.TabIndex = 12;
            this.motorPosGrid.TabStop = true;
            this.motorPosGrid.ToolTipText = "";
            // 
            // FormTeachInPP
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1003, 561);
            this.Controls.Add(this.jogControl1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Font = new System.Drawing.Font("SamsungOne 800", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormTeachInPP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormTeachPP";
            this.Load += new System.EventHandler(this.FormTeachPP_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button gripOnButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button gripOffButton;
        private System.Windows.Forms.Button ySaveButton;
        private System.Windows.Forms.Button moveButton;
        private System.Windows.Forms.Button zSaveButton;
        private CSourceGrid motorPosGrid;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Timer ui_timer;
        private JogControl jogControl1;
    }
}