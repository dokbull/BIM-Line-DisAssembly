namespace bim_base
{
    partial class FormTeachTrayPP
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
            this.xbSaveButton = new System.Windows.Forms.Button();
            this.z1SaveButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.z2SaveButton = new System.Windows.Forms.Button();
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(756, 405);
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
            this.xSaveButton.Location = new System.Drawing.Point(3, 435);
            this.xSaveButton.Name = "xSaveButton";
            this.xSaveButton.Size = new System.Drawing.Size(139, 99);
            this.xSaveButton.TabIndex = 19;
            this.xSaveButton.Text = "MOLD X Save";
            this.xSaveButton.UseVisualStyleBackColor = true;
            this.xSaveButton.Click += new System.EventHandler(this.xySaveButton_Click);
            // 
            // moveButton
            // 
            this.moveButton.Location = new System.Drawing.Point(583, 435);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(141, 99);
            this.moveButton.TabIndex = 22;
            this.moveButton.Text = "Move";
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.Click += new System.EventHandler(this.moveButton_Click);
            // 
            // xbSaveButton
            // 
            this.xbSaveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xbSaveButton.Location = new System.Drawing.Point(148, 435);
            this.xbSaveButton.Name = "xbSaveButton";
            this.xbSaveButton.Size = new System.Drawing.Size(139, 99);
            this.xbSaveButton.TabIndex = 18;
            this.xbSaveButton.Text = "BASE X Save";
            this.xbSaveButton.UseVisualStyleBackColor = true;
            this.xbSaveButton.Click += new System.EventHandler(this.xbSaveButton_Click);
            // 
            // z1SaveButton
            // 
            this.z1SaveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.z1SaveButton.Location = new System.Drawing.Point(293, 435);
            this.z1SaveButton.Name = "z1SaveButton";
            this.z1SaveButton.Size = new System.Drawing.Size(139, 99);
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
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(727, 537);
            this.tableLayoutPanel3.TabIndex = 54;
            // 
            // z2SaveButton
            // 
            this.z2SaveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.z2SaveButton.Location = new System.Drawing.Point(438, 435);
            this.z2SaveButton.Name = "z2SaveButton";
            this.z2SaveButton.Size = new System.Drawing.Size(139, 99);
            this.z2SaveButton.TabIndex = 23;
            this.z2SaveButton.Text = "Z2 Save";
            this.z2SaveButton.UseVisualStyleBackColor = true;
            this.z2SaveButton.Click += new System.EventHandler(this.z2SaveButton_Click);
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
            // FormTeachTrayPP
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1003, 561);
            this.Controls.Add(this.jogControl1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormTeachTrayPP";
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
        private System.Windows.Forms.Button xSaveButton;
        private System.Windows.Forms.Button moveButton;
        private System.Windows.Forms.Button xbSaveButton;
        private System.Windows.Forms.Button z1SaveButton;
        private CSourceGrid motorPosGrid;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Timer ui_timer;
        private JogControl jogControl1;
        private System.Windows.Forms.Button z2SaveButton;
    }
}