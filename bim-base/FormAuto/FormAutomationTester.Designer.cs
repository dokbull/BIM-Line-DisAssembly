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
            this.lblRunScan = new System.Windows.Forms.Label();
            this.lblRunProcessing = new System.Windows.Forms.Label();
            this.lblRunProcessingList = new System.Windows.Forms.Label();
            this.btnSetDateTime = new System.Windows.Forms.Button();
            this.DateTimeTextBox = new System.Windows.Forms.TextBox();
            this.btnTpmLossStart = new System.Windows.Forms.Button();
            this.btnTpmLossReplyOn = new System.Windows.Forms.Button();
            this.btnTpmLossReplyOff = new System.Windows.Forms.Button();
            this.lblTpmLossMonitor = new System.Windows.Forms.Label();
            this.btnTerminalRecv = new System.Windows.Forms.Button();
            this.lblTerminalRecvMonitor = new System.Windows.Forms.Label();
            this.lblTerminalSendMonitor = new System.Windows.Forms.Label();
            this.btnTerminalSend = new System.Windows.Forms.Button();
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
            this.btnHandShake.Location = new System.Drawing.Point(16, 77);
            this.btnHandShake.Name = "btnHandShake";
            this.btnHandShake.Size = new System.Drawing.Size(200, 50);
            this.btnHandShake.TabIndex = 2;
            this.btnHandShake.Text = "H/S";
            this.btnHandShake.UseVisualStyleBackColor = true;
            this.btnHandShake.Click += new System.EventHandler(this.btnHandShake_Click);
            // 
            // lblInitialized
            // 
            this.lblInitialized.Location = new System.Drawing.Point(13, 27);
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
            this.lblRunScan.Location = new System.Drawing.Point(222, 27);
            this.lblRunScan.Name = "lblRunScan";
            this.lblRunScan.Size = new System.Drawing.Size(203, 47);
            this.lblRunScan.TabIndex = 4;
            this.lblRunScan.Text = "Scan CIM Request";
            this.lblRunScan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRunProcessing
            // 
            this.lblRunProcessing.Location = new System.Drawing.Point(412, 27);
            this.lblRunProcessing.Name = "lblRunProcessing";
            this.lblRunProcessing.Size = new System.Drawing.Size(203, 47);
            this.lblRunProcessing.TabIndex = 5;
            this.lblRunProcessing.Text = "Processing";
            this.lblRunProcessing.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRunProcessingList
            // 
            this.lblRunProcessingList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRunProcessingList.Location = new System.Drawing.Point(621, 27);
            this.lblRunProcessingList.Name = "lblRunProcessingList";
            this.lblRunProcessingList.Size = new System.Drawing.Size(286, 47);
            this.lblRunProcessingList.TabIndex = 6;
            this.lblRunProcessingList.Text = "Run Processing List";
            this.lblRunProcessingList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSetDateTime
            // 
            this.btnSetDateTime.Location = new System.Drawing.Point(830, 80);
            this.btnSetDateTime.Name = "btnSetDateTime";
            this.btnSetDateTime.Size = new System.Drawing.Size(77, 47);
            this.btnSetDateTime.TabIndex = 8;
            this.btnSetDateTime.Text = "SetDateTime";
            this.btnSetDateTime.UseVisualStyleBackColor = true;
            this.btnSetDateTime.Click += new System.EventHandler(this.btnSetDateTime_Click);
            // 
            // DateTimeTextBox
            // 
            this.DateTimeTextBox.Location = new System.Drawing.Point(621, 90);
            this.DateTimeTextBox.Name = "DateTimeTextBox";
            this.DateTimeTextBox.Size = new System.Drawing.Size(203, 28);
            this.DateTimeTextBox.TabIndex = 9;
            this.DateTimeTextBox.TextChanged += new System.EventHandler(this.DataTimeTextBox_TextChanged);
            // 
            // btnTpmLossStart
            // 
            this.btnTpmLossStart.Location = new System.Drawing.Point(16, 133);
            this.btnTpmLossStart.Name = "btnTpmLossStart";
            this.btnTpmLossStart.Size = new System.Drawing.Size(149, 46);
            this.btnTpmLossStart.TabIndex = 10;
            this.btnTpmLossStart.Text = "TPMLossStart";
            this.btnTpmLossStart.UseVisualStyleBackColor = true;
            this.btnTpmLossStart.Click += new System.EventHandler(this.btnTpmLossStart_Click);
            // 
            // btnTpmLossReplyOn
            // 
            this.btnTpmLossReplyOn.Location = new System.Drawing.Point(171, 133);
            this.btnTpmLossReplyOn.Name = "btnTpmLossReplyOn";
            this.btnTpmLossReplyOn.Size = new System.Drawing.Size(162, 46);
            this.btnTpmLossReplyOn.TabIndex = 11;
            this.btnTpmLossReplyOn.Text = "TPMLossReplyOn";
            this.btnTpmLossReplyOn.UseVisualStyleBackColor = true;
            this.btnTpmLossReplyOn.Click += new System.EventHandler(this.btnTpmLossReplyOn_Click);
            // 
            // btnTpmLossReplyOff
            // 
            this.btnTpmLossReplyOff.Location = new System.Drawing.Point(339, 133);
            this.btnTpmLossReplyOff.Name = "btnTpmLossReplyOff";
            this.btnTpmLossReplyOff.Size = new System.Drawing.Size(162, 46);
            this.btnTpmLossReplyOff.TabIndex = 12;
            this.btnTpmLossReplyOff.Text = "TPMLossReplyOff";
            this.btnTpmLossReplyOff.UseVisualStyleBackColor = true;
            this.btnTpmLossReplyOff.Click += new System.EventHandler(this.btnTpmLossReplyOff_Click);
            // 
            // lblTpmLossMonitor
            // 
            this.lblTpmLossMonitor.Location = new System.Drawing.Point(507, 133);
            this.lblTpmLossMonitor.Name = "lblTpmLossMonitor";
            this.lblTpmLossMonitor.Size = new System.Drawing.Size(203, 46);
            this.lblTpmLossMonitor.TabIndex = 13;
            this.lblTpmLossMonitor.Text = "TPM Monitor";
            this.lblTpmLossMonitor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTerminalRecv
            // 
            this.btnTerminalRecv.Location = new System.Drawing.Point(16, 185);
            this.btnTerminalRecv.Name = "btnTerminalRecv";
            this.btnTerminalRecv.Size = new System.Drawing.Size(149, 46);
            this.btnTerminalRecv.TabIndex = 14;
            this.btnTerminalRecv.Text = "TerminalRecv";
            this.btnTerminalRecv.UseVisualStyleBackColor = true;
            this.btnTerminalRecv.Click += new System.EventHandler(this.btnTerminalRecv_Click);
            // 
            // lblTerminalRecvMonitor
            // 
            this.lblTerminalRecvMonitor.Location = new System.Drawing.Point(171, 185);
            this.lblTerminalRecvMonitor.Name = "lblTerminalRecvMonitor";
            this.lblTerminalRecvMonitor.Size = new System.Drawing.Size(293, 46);
            this.lblTerminalRecvMonitor.TabIndex = 16;
            this.lblTerminalRecvMonitor.Text = "TerminalRecvMonitor";
            this.lblTerminalRecvMonitor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTerminalSendMonitor
            // 
            this.lblTerminalSendMonitor.Location = new System.Drawing.Point(625, 185);
            this.lblTerminalSendMonitor.Name = "lblTerminalSendMonitor";
            this.lblTerminalSendMonitor.Size = new System.Drawing.Size(293, 46);
            this.lblTerminalSendMonitor.TabIndex = 17;
            this.lblTerminalSendMonitor.Text = "TerminalSendMonitor";
            this.lblTerminalSendMonitor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTerminalSend
            // 
            this.btnTerminalSend.Location = new System.Drawing.Point(470, 185);
            this.btnTerminalSend.Name = "btnTerminalSend";
            this.btnTerminalSend.Size = new System.Drawing.Size(149, 46);
            this.btnTerminalSend.TabIndex = 18;
            this.btnTerminalSend.Text = "TerminalSend";
            this.btnTerminalSend.UseVisualStyleBackColor = true;
            this.btnTerminalSend.Click += new System.EventHandler(this.btnTerminalSend_Click);
            // 
            // FormAutomationTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 835);
            this.Controls.Add(this.btnTerminalSend);
            this.Controls.Add(this.lblTerminalSendMonitor);
            this.Controls.Add(this.lblTerminalRecvMonitor);
            this.Controls.Add(this.btnTerminalRecv);
            this.Controls.Add(this.lblTpmLossMonitor);
            this.Controls.Add(this.btnTpmLossReplyOff);
            this.Controls.Add(this.btnTpmLossReplyOn);
            this.Controls.Add(this.btnTpmLossStart);
            this.Controls.Add(this.DateTimeTextBox);
            this.Controls.Add(this.btnSetDateTime);
            this.Controls.Add(this.lblRunProcessingList);
            this.Controls.Add(this.lblRunProcessing);
            this.Controls.Add(this.lblRunScan);
            this.Controls.Add(this.lblInitialized);
            this.Controls.Add(this.btnHandShake);
            this.Controls.Add(this.label1);
            this.Name = "FormAutomationTester";
            this.Text = "CIM Tester";
            this.Load += new System.EventHandler(this.FormAutomationTester_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnHandShake;
        private System.Windows.Forms.Label lblInitialized;
        private System.Windows.Forms.Timer tmrRedraw;
        private System.Windows.Forms.Label lblRunScan;
        private System.Windows.Forms.Label lblRunProcessing;
        private System.Windows.Forms.Label lblRunProcessingList;
        private System.Windows.Forms.Button btnSetDateTime;
        private System.Windows.Forms.TextBox DateTimeTextBox;
        private System.Windows.Forms.Button btnTpmLossStart;
        private System.Windows.Forms.Button btnTpmLossReplyOn;
        private System.Windows.Forms.Button btnTpmLossReplyOff;
        private System.Windows.Forms.Label lblTpmLossMonitor;
        private System.Windows.Forms.Button btnTerminalRecv;
        private System.Windows.Forms.Label lblTerminalRecvMonitor;
        private System.Windows.Forms.Label lblTerminalSendMonitor;
        private System.Windows.Forms.Button btnTerminalSend;
    }
}