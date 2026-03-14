namespace bim_base
{
    partial class FormAxisLimit
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
            this.exitButton = new CImageButton();
            this.xAxisLimit = new bim_base.AxisLimitControl();
            this.yAxisLimit = new bim_base.AxisLimitControl();
            this.zAxisLimit = new bim_base.AxisLimitControl();
            this.SuspendLayout();
            // 
            // exitButton
            // 
            this.exitButton._BACK_COLOR = System.Drawing.SystemColors.Control;
            this.exitButton._CHECKED = false;
            this.exitButton._CHECKED_BACK_COLOR = System.Drawing.Color.Transparent;
            this.exitButton._IMAGE = null;
            this.exitButton._IMAGE_SIZE = new System.Drawing.Size(40, 40);
            this.exitButton._IMG_POS = CBUTTON_POS.Left;
            this.exitButton._MARGIN = new System.Windows.Forms.Padding(15, 2, 7, 0);
            this.exitButton._TEXT = "";
            this.exitButton.BackColor = System.Drawing.Color.Transparent;
            this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitButton.ForeColor = System.Drawing.Color.Black;
            this.exitButton.Location = new System.Drawing.Point(375, 373);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(290, 53);
            this.exitButton.TabIndex = 329;
            this.exitButton.Text = "  Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            // 
            // xAxisLimit
            // 
            this.xAxisLimit.BackColor = System.Drawing.Color.White;
            this.xAxisLimit.Font = new System.Drawing.Font("SamsungOne 800", 12F, System.Drawing.FontStyle.Bold);
            this.xAxisLimit.Location = new System.Drawing.Point(13, 14);
            this.xAxisLimit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xAxisLimit.Name = "xAxisLimit";
            this.xAxisLimit.Size = new System.Drawing.Size(212, 351);
            this.xAxisLimit.TabIndex = 336;
            // 
            // yAxisLimit
            // 
            this.yAxisLimit.BackColor = System.Drawing.Color.White;
            this.yAxisLimit.Font = new System.Drawing.Font("SamsungOne 800", 12F, System.Drawing.FontStyle.Bold);
            this.yAxisLimit.Location = new System.Drawing.Point(233, 14);
            this.yAxisLimit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.yAxisLimit.Name = "yAxisLimit";
            this.yAxisLimit.Size = new System.Drawing.Size(212, 351);
            this.yAxisLimit.TabIndex = 336;
            // 
            // zAxisLimit
            // 
            this.zAxisLimit.BackColor = System.Drawing.Color.White;
            this.zAxisLimit.Font = new System.Drawing.Font("SamsungOne 800", 12F, System.Drawing.FontStyle.Bold);
            this.zAxisLimit.Location = new System.Drawing.Point(453, 14);
            this.zAxisLimit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.zAxisLimit.Name = "zAxisLimit";
            this.zAxisLimit.Size = new System.Drawing.Size(212, 351);
            this.zAxisLimit.TabIndex = 336;
            // 
            // FormAxisLimit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(683, 439);
            this.ControlBox = false;
            this.Controls.Add(this.zAxisLimit);
            this.Controls.Add(this.yAxisLimit);
            this.Controls.Add(this.xAxisLimit);
            this.Controls.Add(this.exitButton);
            this.Font = new System.Drawing.Font("SamsungOne 800", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAxisLimit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormSubAxisLimit";
            this.ResumeLayout(false);

        }

        #endregion
        public CImageButton exitButton;
        private AxisLimitControl xAxisLimit;
        private AxisLimitControl yAxisLimit;
        private AxisLimitControl zAxisLimit;
    }
}