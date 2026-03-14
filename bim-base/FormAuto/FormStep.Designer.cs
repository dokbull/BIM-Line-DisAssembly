namespace bim_base
{
    partial class FormStep
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
            this.stepGrid = new CSourceGrid();
            this.uiTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // stepGrid
            // 
            this.stepGrid.EnableSort = true;
            this.stepGrid.Location = new System.Drawing.Point(0, 0);
            this.stepGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.stepGrid.Name = "stepGrid";
            this.stepGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.stepGrid.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.stepGrid.Size = new System.Drawing.Size(750, 531);
            this.stepGrid.TabIndex = 29;
            this.stepGrid.TabStop = true;
            this.stepGrid.ToolTipText = "";
            // 
            // uiTimer
            // 
            this.uiTimer.Enabled = true;
            this.uiTimer.Tick += new System.EventHandler(this.uiTimer_Tick);
            // 
            // FormStep
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(744, 541);
            this.Controls.Add(this.stepGrid);
            this.Font = new System.Drawing.Font("SamsungOne 800", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormStep";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormStep";
            this.Load += new System.EventHandler(this.FormStep_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CSourceGrid stepGrid;
        private System.Windows.Forms.Timer uiTimer;
    }
}