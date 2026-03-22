namespace bim_base
{
    partial class FormVacuumDelay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVacuumDelay));
            this.saveButton = new SUserControls.ColorButton();
            this.exitButton = new SUserControls.ColorButton();
            this.colorButton1 = new SUserControls.ColorButton();
            this.colorButton2 = new SUserControls.ColorButton();
            this.VacuumListGrid = new CSourceGrid();
            this.NextButton = new SUserControls.ColorButton();
            this.PreviouslyButton = new SUserControls.ColorButton();
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
            this.saveButton.Location = new System.Drawing.Point(44, 623);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(210, 79);
            this.saveButton.TabIndex = 1170;
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
            this.exitButton.Location = new System.Drawing.Point(280, 623);
            this.exitButton.Margin = new System.Windows.Forms.Padding(4);
            this.exitButton.Name = "exitButton";
            this.exitButton.RectCornerRadius = 2;
            this.exitButton.Size = new System.Drawing.Size(228, 79);
            this.exitButton.TabIndex = 1176;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
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
            this.colorButton1.Location = new System.Drawing.Point(44, 24);
            this.colorButton1.Margin = new System.Windows.Forms.Padding(4);
            this.colorButton1.Name = "colorButton1";
            this.colorButton1.Size = new System.Drawing.Size(464, 59);
            this.colorButton1.TabIndex = 1172;
            this.colorButton1.Text = "Vacuum Delay Setting";
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
            this.colorButton2.Location = new System.Drawing.Point(44, 99);
            this.colorButton2.Margin = new System.Windows.Forms.Padding(4);
            this.colorButton2.Name = "colorButton2";
            this.colorButton2.Size = new System.Drawing.Size(464, 386);
            this.colorButton2.TabIndex = 1173;
            this.colorButton2.UseVisualStyleBackColor = true;
            // 
            // VacuumListGrid
            // 
            this.VacuumListGrid.EnableSort = true;
            this.VacuumListGrid.Location = new System.Drawing.Point(56, 113);
            this.VacuumListGrid.Name = "VacuumListGrid";
            this.VacuumListGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.VacuumListGrid.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.VacuumListGrid.Size = new System.Drawing.Size(439, 361);
            this.VacuumListGrid.TabIndex = 1177;
            this.VacuumListGrid.TabStop = true;
            this.VacuumListGrid.ToolTipText = "";
            // 
            // NextButton
            // 
            this.NextButton.BorderLineColor = System.Drawing.Color.Black;
            this.NextButton.BorderThickness = 0F;
            this.NextButton.Checked = false;
            this.NextButton.CheckedButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(53)))), ((int)(((byte)(98)))), ((int)(((byte)(141)))));
            this.NextButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.NextButton.GradientBottom = System.Drawing.Color.White;
            this.NextButton.GradientTop = System.Drawing.Color.White;
            this.NextButton.Location = new System.Drawing.Point(375, 503);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(133, 48);
            this.NextButton.TabIndex = 1178;
            this.NextButton.Text = "Next";
            this.NextButton.UseVisualStyleBackColor = true;
            // 
            // PreviouslyButton
            // 
            this.PreviouslyButton.BorderLineColor = System.Drawing.Color.Black;
            this.PreviouslyButton.BorderThickness = 0F;
            this.PreviouslyButton.Checked = false;
            this.PreviouslyButton.CheckedButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(53)))), ((int)(((byte)(98)))), ((int)(((byte)(141)))));
            this.PreviouslyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.PreviouslyButton.GradientBottom = System.Drawing.Color.White;
            this.PreviouslyButton.GradientTop = System.Drawing.Color.White;
            this.PreviouslyButton.Location = new System.Drawing.Point(44, 503);
            this.PreviouslyButton.Name = "PreviouslyButton";
            this.PreviouslyButton.Size = new System.Drawing.Size(133, 48);
            this.PreviouslyButton.TabIndex = 1179;
            this.PreviouslyButton.Text = "Prev";
            this.PreviouslyButton.UseVisualStyleBackColor = true;
            // 
            // FormVacuumDelay
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(551, 726);
            this.ControlBox = false;
            this.Controls.Add(this.PreviouslyButton);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.VacuumListGrid);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.colorButton1);
            this.Controls.Add(this.colorButton2);
            this.Name = "FormVacuumDelay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormVacuumDelay";
            this.ResumeLayout(false);

        }

        #endregion

        private SUserControls.ColorButton saveButton;
        public SUserControls.ColorButton exitButton;
        private SUserControls.ColorButton colorButton1;
        private SUserControls.ColorButton colorButton2;
        private CSourceGrid VacuumListGrid;
        private SUserControls.ColorButton NextButton;
        private SUserControls.ColorButton PreviouslyButton;
    }
}