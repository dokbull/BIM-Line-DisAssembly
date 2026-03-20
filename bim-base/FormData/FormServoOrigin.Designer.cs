namespace bim_base
{
    partial class FormServoOrigin
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
            this.colorButton1 = new SUserControls.ColorButton();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnBatch = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.MotorOriginGrid = new CSourceGrid();
            this.SuspendLayout();
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
            this.colorButton1.Location = new System.Drawing.Point(23, 29);
            this.colorButton1.Margin = new System.Windows.Forms.Padding(4);
            this.colorButton1.Name = "colorButton1";
            this.colorButton1.Size = new System.Drawing.Size(976, 59);
            this.colorButton1.TabIndex = 16;
            this.colorButton1.Text = "Motor Servo Origin";
            this.colorButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.colorButton1.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(849, 95);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(150, 66);
            this.btnNext.TabIndex = 10;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(849, 167);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(150, 66);
            this.btnPrev.TabIndex = 11;
            this.btnPrev.Text = "Previous";
            this.btnPrev.UseVisualStyleBackColor = true;
            // 
            // btnBatch
            // 
            this.btnBatch.Location = new System.Drawing.Point(849, 246);
            this.btnBatch.Name = "btnBatch";
            this.btnBatch.Size = new System.Drawing.Size(150, 66);
            this.btnBatch.TabIndex = 12;
            this.btnBatch.Text = "Batch Input";
            this.btnBatch.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(849, 452);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 66);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(849, 530);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(150, 66);
            this.btnExit.TabIndex = 14;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // MotorOriginGrid
            // 
            this.MotorOriginGrid.EnableSort = true;
            this.MotorOriginGrid.Location = new System.Drawing.Point(22, 95);
            this.MotorOriginGrid.Name = "MotorOriginGrid";
            this.MotorOriginGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.MotorOriginGrid.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.MotorOriginGrid.Size = new System.Drawing.Size(808, 500);
            this.MotorOriginGrid.TabIndex = 15;
            this.MotorOriginGrid.TabStop = true;
            this.MotorOriginGrid.ToolTipText = "";
            // 
            // FormServoOrigin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1020, 624);
            this.Controls.Add(this.colorButton1);
            this.Controls.Add(this.MotorOriginGrid);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnBatch);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Font = new System.Drawing.Font("굴림", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormServoOrigin";
            this.Text = "FormServoOrigin";
            this.Load += new System.EventHandler(this.FormServoOrigin_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private SUserControls.ColorButton colorButton1;
        private CSourceGrid MotorOriginGrid;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnBatch;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
    }
}