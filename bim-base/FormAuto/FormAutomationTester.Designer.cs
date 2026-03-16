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
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 1;
            // 
            // btnHandShake
            // 
            this.btnHandShake.Location = new System.Drawing.Point(10, 56);
            this.btnHandShake.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnHandShake.Name = "btnHandShake";
            this.btnHandShake.Size = new System.Drawing.Size(120, 36);
            this.btnHandShake.TabIndex = 2;
            this.btnHandShake.Text = "H/S";
            this.btnHandShake.UseVisualStyleBackColor = true;
            this.btnHandShake.Click += new System.EventHandler(this.btnHandShake_Click);
            // 
            // lblInitialized
            // 
            this.lblInitialized.Location = new System.Drawing.Point(8, 20);
            this.lblInitialized.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInitialized.Name = "lblInitialized";
            this.lblInitialized.Size = new System.Drawing.Size(122, 34);
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
            this.lblRunScan.Location = new System.Drawing.Point(133, 20);
            this.lblRunScan.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRunScan.Name = "lblRunScan";
            this.lblRunScan.Size = new System.Drawing.Size(122, 34);
            this.lblRunScan.TabIndex = 4;
            this.lblRunScan.Text = "Scan CIM Request";
            this.lblRunScan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRunProcessing
            // 
            this.lblRunProcessing.Location = new System.Drawing.Point(247, 20);
            this.lblRunProcessing.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRunProcessing.Name = "lblRunProcessing";
            this.lblRunProcessing.Size = new System.Drawing.Size(122, 34);
            this.lblRunProcessing.TabIndex = 5;
            this.lblRunProcessing.Text = "Processing";
            this.lblRunProcessing.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRunProcessingList
            // 
            this.lblRunProcessingList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRunProcessingList.Location = new System.Drawing.Point(373, 20);
            this.lblRunProcessingList.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRunProcessingList.Name = "lblRunProcessingList";
            this.lblRunProcessingList.Size = new System.Drawing.Size(172, 34);
            this.lblRunProcessingList.TabIndex = 6;
            this.lblRunProcessingList.Text = "Run Processing List";
            this.lblRunProcessingList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSetDateTime
            // 
            this.btnSetDateTime.Location = new System.Drawing.Point(498, 58);
            this.btnSetDateTime.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSetDateTime.Name = "btnSetDateTime";
            this.btnSetDateTime.Size = new System.Drawing.Size(46, 34);
            this.btnSetDateTime.TabIndex = 8;
            this.btnSetDateTime.Text = "SetDateTime";
            this.btnSetDateTime.UseVisualStyleBackColor = true;
            this.btnSetDateTime.Click += new System.EventHandler(this.btnSetDateTime_Click);
            // 
            // DateTimeTextBox
            // 
            this.DateTimeTextBox.Location = new System.Drawing.Point(373, 65);
            this.DateTimeTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DateTimeTextBox.Name = "DateTimeTextBox";
            this.DateTimeTextBox.Size = new System.Drawing.Size(123, 20);
            this.DateTimeTextBox.TabIndex = 9;
            this.DateTimeTextBox.TextChanged += new System.EventHandler(this.DataTimeTextBox_TextChanged);
            // 
            // btnTpmLossStart
            // 
            this.btnTpmLossStart.Location = new System.Drawing.Point(10, 96);
            this.btnTpmLossStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTpmLossStart.Name = "btnTpmLossStart";
            this.btnTpmLossStart.Size = new System.Drawing.Size(89, 33);
            this.btnTpmLossStart.TabIndex = 10;
            this.btnTpmLossStart.Text = "TPMLossStart";
            this.btnTpmLossStart.UseVisualStyleBackColor = true;
            this.btnTpmLossStart.Click += new System.EventHandler(this.btnTpmLossStart_Click);
            // 
            // btnTpmLossReplyOn
            // 
            this.btnTpmLossReplyOn.Location = new System.Drawing.Point(103, 96);
            this.btnTpmLossReplyOn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTpmLossReplyOn.Name = "btnTpmLossReplyOn";
            this.btnTpmLossReplyOn.Size = new System.Drawing.Size(97, 33);
            this.btnTpmLossReplyOn.TabIndex = 11;
            this.btnTpmLossReplyOn.Text = "TPMLossReplyOn";
            this.btnTpmLossReplyOn.UseVisualStyleBackColor = true;
            this.btnTpmLossReplyOn.Click += new System.EventHandler(this.btnTpmLossReplyOn_Click);
            // 
            // btnTpmLossReplyOff
            // 
            this.btnTpmLossReplyOff.Location = new System.Drawing.Point(203, 96);
            this.btnTpmLossReplyOff.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTpmLossReplyOff.Name = "btnTpmLossReplyOff";
            this.btnTpmLossReplyOff.Size = new System.Drawing.Size(97, 33);
            this.btnTpmLossReplyOff.TabIndex = 12;
            this.btnTpmLossReplyOff.Text = "TPMLossReplyOff";
            this.btnTpmLossReplyOff.UseVisualStyleBackColor = true;
            this.btnTpmLossReplyOff.Click += new System.EventHandler(this.btnTpmLossReplyOff_Click);
            // 
            // lblTpmLossMonitor
            // 
            this.lblTpmLossMonitor.Location = new System.Drawing.Point(304, 96);
            this.lblTpmLossMonitor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTpmLossMonitor.Name = "lblTpmLossMonitor";
            this.lblTpmLossMonitor.Size = new System.Drawing.Size(122, 33);
            this.lblTpmLossMonitor.TabIndex = 13;
            this.lblTpmLossMonitor.Text = "TPM Monitor";
            this.lblTpmLossMonitor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTerminalRecv
            // 
            this.btnTerminalRecv.Location = new System.Drawing.Point(10, 134);
            this.btnTerminalRecv.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTerminalRecv.Name = "btnTerminalRecv";
            this.btnTerminalRecv.Size = new System.Drawing.Size(89, 33);
            this.btnTerminalRecv.TabIndex = 14;
            this.btnTerminalRecv.Text = "TerminalRecv";
            this.btnTerminalRecv.UseVisualStyleBackColor = true;
            this.btnTerminalRecv.Click += new System.EventHandler(this.btnTerminalRecv_Click);
            // 
            // lblTerminalRecvMonitor
            // 
            this.lblTerminalRecvMonitor.Location = new System.Drawing.Point(103, 134);
            this.lblTerminalRecvMonitor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTerminalRecvMonitor.Name = "lblTerminalRecvMonitor";
            this.lblTerminalRecvMonitor.Size = new System.Drawing.Size(176, 33);
            this.lblTerminalRecvMonitor.TabIndex = 16;
            this.lblTerminalRecvMonitor.Text = "TerminalRecvMonitor";
            this.lblTerminalRecvMonitor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTerminalSendMonitor
            // 
            this.lblTerminalSendMonitor.Location = new System.Drawing.Point(375, 134);
            this.lblTerminalSendMonitor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTerminalSendMonitor.Name = "lblTerminalSendMonitor";
            this.lblTerminalSendMonitor.Size = new System.Drawing.Size(176, 33);
            this.lblTerminalSendMonitor.TabIndex = 17;
            this.lblTerminalSendMonitor.Text = "TerminalSendMonitor";
            this.lblTerminalSendMonitor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTerminalSend
            // 
            this.btnTerminalSend.Location = new System.Drawing.Point(282, 134);
            this.btnTerminalSend.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTerminalSend.Name = "btnTerminalSend";
            this.btnTerminalSend.Size = new System.Drawing.Size(89, 33);
            this.btnTerminalSend.TabIndex = 18;
            this.btnTerminalSend.Text = "TerminalSend";
            this.btnTerminalSend.UseVisualStyleBackColor = true;
            this.btnTerminalSend.Click += new System.EventHandler(this.btnTerminalSend_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(280, 204);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 33);
            this.button1.TabIndex = 18;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormAutomationTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 603);
            this.Controls.Add(this.button1);
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
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
        private System.Windows.Forms.Button button1;
    }
}