using System.Drawing;
using System.Windows.Forms;

namespace bim_base
{
    partial class FormServoVelocity
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnBatch;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;

        private void InitializeComponent()
        {
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnBatch = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.MotorVelGrid = new CSourceGrid();
            this.colorButton1 = new SUserControls.ColorButton();
            this.SuspendLayout();
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(839, 79);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(150, 66);
            this.btnNext.TabIndex = 0;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(839, 151);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(150, 66);
            this.btnPrev.TabIndex = 1;
            this.btnPrev.Text = "Previous";
            this.btnPrev.UseVisualStyleBackColor = true;
            // 
            // btnBatch
            // 
            this.btnBatch.Location = new System.Drawing.Point(839, 230);
            this.btnBatch.Name = "btnBatch";
            this.btnBatch.Size = new System.Drawing.Size(150, 66);
            this.btnBatch.TabIndex = 2;
            this.btnBatch.Text = "Batch Input";
            this.btnBatch.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(839, 436);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 66);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(839, 514);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(150, 66);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnServoVelocityExit_Click);
            // 
            // MotorVelGrid
            // 
            this.MotorVelGrid.EnableSort = true;
            this.MotorVelGrid.Location = new System.Drawing.Point(12, 79);
            this.MotorVelGrid.Name = "MotorVelGrid";
            this.MotorVelGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.MotorVelGrid.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.MotorVelGrid.Size = new System.Drawing.Size(808, 500);
            this.MotorVelGrid.TabIndex = 5;
            this.MotorVelGrid.TabStop = true;
            this.MotorVelGrid.ToolTipText = "";
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
            this.colorButton1.Location = new System.Drawing.Point(13, 13);
            this.colorButton1.Margin = new System.Windows.Forms.Padding(4);
            this.colorButton1.Name = "colorButton1";
            this.colorButton1.Size = new System.Drawing.Size(976, 59);
            this.colorButton1.TabIndex = 9;
            this.colorButton1.Text = "Motor Servo Velocity";
            this.colorButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.colorButton1.UseVisualStyleBackColor = true;
            // 
            // FormServoVelocity
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1020, 624);
            this.Controls.Add(this.colorButton1);
            this.Controls.Add(this.MotorVelGrid);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnBatch);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Font = new System.Drawing.Font("굴림", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormServoVelocity";
            this.Text = "ServoVelocity";
            this.Load += new System.EventHandler(this.FormServoVelocity_Load);
            this.ResumeLayout(false);

        }

        private CSourceGrid MotorVelGrid;
        private SUserControls.ColorButton colorButton1;
    }
}