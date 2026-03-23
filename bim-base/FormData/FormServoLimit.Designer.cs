namespace bim_base
{
    partial class FormServoLimit
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
            this.MotorServoLimitGrid = new CSourceGrid();
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
            this.colorButton1.Location = new System.Drawing.Point(15, 9);
            this.colorButton1.Margin = new System.Windows.Forms.Padding(4);
            this.colorButton1.Name = "colorButton1";
            this.colorButton1.Size = new System.Drawing.Size(976, 59);
            this.colorButton1.TabIndex = 23;
            this.colorButton1.Text = "Motor Servo Limit";
            this.colorButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.colorButton1.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(841, 75);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(150, 66);
            this.btnNext.TabIndex = 17;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(841, 147);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(150, 66);
            this.btnPrev.TabIndex = 18;
            this.btnPrev.Text = "Previous";
            this.btnPrev.UseVisualStyleBackColor = true;
            // 
            // btnBatch
            // 
            this.btnBatch.Location = new System.Drawing.Point(841, 226);
            this.btnBatch.Name = "btnBatch";
            this.btnBatch.Size = new System.Drawing.Size(150, 66);
            this.btnBatch.TabIndex = 19;
            this.btnBatch.Text = "Batch Input";
            this.btnBatch.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(841, 432);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 66);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(841, 510);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(150, 66);
            this.btnExit.TabIndex = 21;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // MotorServoLimitGrid
            // 
            this.MotorServoLimitGrid.EnableSort = true;
            this.MotorServoLimitGrid.Location = new System.Drawing.Point(14, 75);
            this.MotorServoLimitGrid.Name = "MotorServoLimitGrid";
            this.MotorServoLimitGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.MotorServoLimitGrid.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.MotorServoLimitGrid.Size = new System.Drawing.Size(808, 500);
            this.MotorServoLimitGrid.TabIndex = 22;
            this.MotorServoLimitGrid.TabStop = true;
            this.MotorServoLimitGrid.ToolTipText = "";
            // 
            // FormServoLimit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1004, 585);
            this.Controls.Add(this.colorButton1);
            this.Controls.Add(this.MotorServoLimitGrid);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnBatch);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Font = new System.Drawing.Font("굴림", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormServoLimit";
            this.Text = "FormServoLimit";
            this.Load += new System.EventHandler(this.FormServoLimit_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private SUserControls.ColorButton colorButton1;
        private CSourceGrid MotorServoLimitGrid;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnBatch;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
    }
}