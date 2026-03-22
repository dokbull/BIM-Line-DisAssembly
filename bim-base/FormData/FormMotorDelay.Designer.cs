namespace bim_base
{
    partial class FormMotorDelay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMotorDelay));
            this.saveButton = new SUserControls.ColorButton();
            this.exitButton = new SUserControls.ColorButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.colorButton3 = new SUserControls.ColorButton();
            this.colorButton1 = new SUserControls.ColorButton();
            this.colorButton2 = new SUserControls.ColorButton();
            this.MotorListGrid = new CSourceGrid();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.BorderLineColor = System.Drawing.Color.Black;
            this.saveButton.BorderThickness = 0F;
            this.saveButton.Checked = false;
            this.saveButton.CheckedButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(53)))), ((int)(((byte)(98)))), ((int)(((byte)(141)))));
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.saveButton.GradientBottom = System.Drawing.Color.White;
            this.saveButton.GradientTop = System.Drawing.Color.White;
            this.saveButton.Location = new System.Drawing.Point(508, 481);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(210, 79);
            this.saveButton.TabIndex = 1156;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.Transparent;
            this.exitButton.BorderLineColor = System.Drawing.Color.Black;
            this.exitButton.BorderThickness = 1F;
            this.exitButton.Checked = false;
            this.exitButton.CheckedButtonColor = System.Drawing.Color.Black;
            this.exitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.exitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange;
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.ForeColor = System.Drawing.Color.Black;
            this.exitButton.GradientBottom = System.Drawing.Color.White;
            this.exitButton.GradientTop = System.Drawing.Color.White;
            this.exitButton.Image = ((System.Drawing.Image)(resources.GetObject("exitButton.Image")));
            this.exitButton.Location = new System.Drawing.Point(744, 481);
            this.exitButton.Margin = new System.Windows.Forms.Padding(4);
            this.exitButton.Name = "exitButton";
            this.exitButton.RectCornerRadius = 2;
            this.exitButton.Size = new System.Drawing.Size(228, 79);
            this.exitButton.TabIndex = 1162;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 279F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(45, 97);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(428, 366);
            this.tableLayoutPanel1.TabIndex = 1161;
            // 
            // colorButton3
            // 
            this.colorButton3.BorderLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(53)))), ((int)(((byte)(98)))), ((int)(((byte)(141)))));
            this.colorButton3.BorderThickness = 0F;
            this.colorButton3.Checked = false;
            this.colorButton3.CheckedButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(53)))), ((int)(((byte)(98)))), ((int)(((byte)(141)))));
            this.colorButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorButton3.GradientBottom = System.Drawing.Color.Silver;
            this.colorButton3.GradientTop = System.Drawing.Color.Silver;
            this.colorButton3.Location = new System.Drawing.Point(31, 87);
            this.colorButton3.Margin = new System.Windows.Forms.Padding(4);
            this.colorButton3.Name = "colorButton3";
            this.colorButton3.Size = new System.Drawing.Size(458, 386);
            this.colorButton3.TabIndex = 1160;
            this.colorButton3.UseVisualStyleBackColor = true;
            // 
            // colorButton1
            // 
            this.colorButton1.BorderLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(53)))), ((int)(((byte)(98)))), ((int)(((byte)(141)))));
            this.colorButton1.BorderThickness = 0F;
            this.colorButton1.Checked = false;
            this.colorButton1.CheckedButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(53)))), ((int)(((byte)(98)))), ((int)(((byte)(141)))));
            this.colorButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorButton1.GradientBottom = System.Drawing.Color.Silver;
            this.colorButton1.GradientTop = System.Drawing.Color.Silver;
            this.colorButton1.Location = new System.Drawing.Point(31, 12);
            this.colorButton1.Margin = new System.Windows.Forms.Padding(4);
            this.colorButton1.Name = "colorButton1";
            this.colorButton1.Size = new System.Drawing.Size(941, 59);
            this.colorButton1.TabIndex = 1158;
            this.colorButton1.Text = "Motor Delay Setting";
            this.colorButton1.UseVisualStyleBackColor = true;
            // 
            // colorButton2
            // 
            this.colorButton2.BorderLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(53)))), ((int)(((byte)(98)))), ((int)(((byte)(141)))));
            this.colorButton2.BorderThickness = 0F;
            this.colorButton2.Checked = false;
            this.colorButton2.CheckedButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(53)))), ((int)(((byte)(98)))), ((int)(((byte)(141)))));
            this.colorButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorButton2.GradientBottom = System.Drawing.Color.Silver;
            this.colorButton2.GradientTop = System.Drawing.Color.Silver;
            this.colorButton2.Location = new System.Drawing.Point(508, 87);
            this.colorButton2.Margin = new System.Windows.Forms.Padding(4);
            this.colorButton2.Name = "colorButton2";
            this.colorButton2.Size = new System.Drawing.Size(464, 386);
            this.colorButton2.TabIndex = 1159;
            this.colorButton2.UseVisualStyleBackColor = true;
            // 
            // MotorListGrid
            // 
            this.MotorListGrid.EnableSort = true;
            this.MotorListGrid.Location = new System.Drawing.Point(518, 97);
            this.MotorListGrid.Name = "MotorListGrid";
            this.MotorListGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.MotorListGrid.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.MotorListGrid.Size = new System.Drawing.Size(442, 365);
            this.MotorListGrid.TabIndex = 1163;
            this.MotorListGrid.TabStop = true;
            this.MotorListGrid.ToolTipText = "";
            // 
            // FormMotorDelay
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1003, 567);
            this.ControlBox = false;
            this.Controls.Add(this.MotorListGrid);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.colorButton3);
            this.Controls.Add(this.colorButton1);
            this.Controls.Add(this.colorButton2);
            this.Name = "FormMotorDelay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.motorDelay_Load);
            this.Text = "FormMotorDelay";
            this.ResumeLayout(false);

        }

        #endregion

        private SUserControls.ColorButton saveButton;
        public SUserControls.ColorButton exitButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private SUserControls.ColorButton colorButton3;
        private SUserControls.ColorButton colorButton1;
        private SUserControls.ColorButton colorButton2;
        private CSourceGrid MotorListGrid;
    }
}