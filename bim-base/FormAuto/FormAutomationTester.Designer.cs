namespace bim_base
{
    partial class FormAutomationTester
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnHandShake = new System.Windows.Forms.Button();
            this.lblInitialized = new System.Windows.Forms.Label();
            this.tmrRedraw = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblRunScan = new System.Windows.Forms.Label();
            this.lblRunProcessing = new System.Windows.Forms.Label();
            this.lblRunProcessingList = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 18);
            this.label1.TabIndex = 1;
            // 
            // btnHandShake
            // 
            this.btnHandShake.Location = new System.Drawing.Point(29, 77);
            this.btnHandShake.Name = "btnHandShake";
            this.btnHandShake.Size = new System.Drawing.Size(200, 50);
            this.btnHandShake.TabIndex = 2;
            this.btnHandShake.Text = "H/S";
            this.btnHandShake.UseVisualStyleBackColor = true;
            this.btnHandShake.Click += new System.EventHandler(this.btnHandShake_Click);
            // 
            // lblInitialized
            // 
            this.lblInitialized.Location = new System.Drawing.Point(26, 27);
            this.lblInitialized.Name = "lblInitialized";
            this.lblInitialized.Size = new System.Drawing.Size(203, 47);
            this.lblInitialized.TabIndex = 3;
            this.lblInitialized.Text = "Initialized";
            this.lblInitialized.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmrRedraw
            // 
            this.tmrRedraw.Tick += new System.EventHandler(this.tmrRedraw_Tick);
            // 
            // lblRunScan
            // 
            this.lblRunScan.Location = new System.Drawing.Point(235, 27);
            this.lblRunScan.Name = "lblRunScan";
            this.lblRunScan.Size = new System.Drawing.Size(203, 47);
            this.lblRunScan.TabIndex = 4;
            this.lblRunScan.Text = "Scan CIM Request";
            this.lblRunScan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRunProcessing
            // 
            this.lblRunProcessing.Location = new System.Drawing.Point(425, 27);
            this.lblRunProcessing.Name = "lblRunProcessing";
            this.lblRunProcessing.Size = new System.Drawing.Size(203, 47);
            this.lblRunProcessing.TabIndex = 5;
            this.lblRunProcessing.Text = "Processing";
            this.lblRunProcessing.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRunProcessingList
            // 
            this.lblRunProcessingList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRunProcessingList.Location = new System.Drawing.Point(634, 27);
            this.lblRunProcessingList.Name = "lblRunProcessingList";
            this.lblRunProcessingList.Size = new System.Drawing.Size(286, 47);
            this.lblRunProcessingList.TabIndex = 6;
            this.lblRunProcessingList.Text = "Run Processing List";
            this.lblRunProcessingList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormAutomationTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 835);
            this.Controls.Add(this.lblRunProcessingList);
            this.Controls.Add(this.lblRunProcessing);
            this.Controls.Add(this.lblRunScan);
            this.Controls.Add(this.lblInitialized);
            this.Controls.Add(this.btnHandShake);
            this.Controls.Add(this.label1);
            this.Name = "FormAutomationTester";
            this.Text = "CIM Tester";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnHandShake;
        private System.Windows.Forms.Label lblInitialized;
        private System.Windows.Forms.Timer tmrRedraw;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblRunScan;
        private System.Windows.Forms.Label lblRunProcessing;
        private System.Windows.Forms.Label lblRunProcessingList;
    }
}