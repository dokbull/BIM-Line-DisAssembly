namespace bim_base
{
    partial class FormAlarm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cPanel1 = new CPanel();
            this.statusLabelPrinter = new System.Windows.Forms.Label();
            this.statusBagSupply = new System.Windows.Forms.Label();
            this.statusSealing = new System.Windows.Forms.Label();
            this.statusLabelOutputHandler = new System.Windows.Forms.Label();
            this.statusBagHandler = new System.Windows.Forms.Label();
            this.statusProductHandler = new System.Windows.Forms.Label();
            this.statusInputCV = new System.Windows.Forms.Label();
            this.statusEMO = new System.Windows.Forms.Label();
            this.statusDoor = new System.Windows.Forms.Label();
            this.statusNGCv = new System.Windows.Forms.Label();
            this.statusOkCV = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.alarmResetButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.errMsgLabel = new System.Windows.Forms.Label();
            this.errActLabel = new System.Windows.Forms.Label();
            this.uiTimer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.cPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.cPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(820, 663);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // cPanel1
            // 
            this.cPanel1.Controls.Add(this.statusLabelPrinter);
            this.cPanel1.Controls.Add(this.statusBagSupply);
            this.cPanel1.Controls.Add(this.statusSealing);
            this.cPanel1.Controls.Add(this.statusLabelOutputHandler);
            this.cPanel1.Controls.Add(this.statusBagHandler);
            this.cPanel1.Controls.Add(this.statusProductHandler);
            this.cPanel1.Controls.Add(this.statusInputCV);
            this.cPanel1.Controls.Add(this.statusEMO);
            this.cPanel1.Controls.Add(this.statusDoor);
            this.cPanel1.Controls.Add(this.statusNGCv);
            this.cPanel1.Controls.Add(this.statusOkCV);
            this.cPanel1.Controls.Add(this.pictureBox1);
            this.cPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cPanel1.Location = new System.Drawing.Point(0, 0);
            this.cPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.cPanel1.Name = "cPanel1";
            this.cPanel1.Size = new System.Drawing.Size(820, 397);
            this.cPanel1.TabIndex = 0;
            // 
            // statusLabelPrinter
            // 
            this.statusLabelPrinter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusLabelPrinter.Location = new System.Drawing.Point(500, 63);
            this.statusLabelPrinter.Name = "statusLabelPrinter";
            this.statusLabelPrinter.Size = new System.Drawing.Size(108, 40);
            this.statusLabelPrinter.TabIndex = 9;
            this.statusLabelPrinter.Tag = "3";
            this.statusLabelPrinter.Text = "Label Printer";
            this.statusLabelPrinter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusBagSupply
            // 
            this.statusBagSupply.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusBagSupply.Location = new System.Drawing.Point(303, 22);
            this.statusBagSupply.Name = "statusBagSupply";
            this.statusBagSupply.Size = new System.Drawing.Size(108, 40);
            this.statusBagSupply.TabIndex = 9;
            this.statusBagSupply.Tag = "3";
            this.statusBagSupply.Text = "Bag Supply";
            this.statusBagSupply.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusSealing
            // 
            this.statusSealing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusSealing.Location = new System.Drawing.Point(371, 235);
            this.statusSealing.Name = "statusSealing";
            this.statusSealing.Size = new System.Drawing.Size(96, 32);
            this.statusSealing.TabIndex = 9;
            this.statusSealing.Tag = "3";
            this.statusSealing.Text = "Sealing";
            this.statusSealing.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusLabelOutputHandler
            // 
            this.statusLabelOutputHandler.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusLabelOutputHandler.Location = new System.Drawing.Point(471, 159);
            this.statusLabelOutputHandler.Name = "statusLabelOutputHandler";
            this.statusLabelOutputHandler.Size = new System.Drawing.Size(126, 52);
            this.statusLabelOutputHandler.TabIndex = 9;
            this.statusLabelOutputHandler.Tag = "3";
            this.statusLabelOutputHandler.Text = "Label/Output\r\nHandler";
            this.statusLabelOutputHandler.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusBagHandler
            // 
            this.statusBagHandler.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusBagHandler.Location = new System.Drawing.Point(253, 168);
            this.statusBagHandler.Name = "statusBagHandler";
            this.statusBagHandler.Size = new System.Drawing.Size(87, 53);
            this.statusBagHandler.TabIndex = 9;
            this.statusBagHandler.Tag = "3";
            this.statusBagHandler.Text = "Bag Handler";
            this.statusBagHandler.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusProductHandler
            // 
            this.statusProductHandler.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusProductHandler.Location = new System.Drawing.Point(253, 258);
            this.statusProductHandler.Name = "statusProductHandler";
            this.statusProductHandler.Size = new System.Drawing.Size(112, 52);
            this.statusProductHandler.TabIndex = 9;
            this.statusProductHandler.Tag = "3";
            this.statusProductHandler.Text = "Product Handler";
            this.statusProductHandler.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusInputCV
            // 
            this.statusInputCV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusInputCV.Location = new System.Drawing.Point(69, 258);
            this.statusInputCV.Name = "statusInputCV";
            this.statusInputCV.Size = new System.Drawing.Size(126, 50);
            this.statusInputCV.TabIndex = 6;
            this.statusInputCV.Tag = "4";
            this.statusInputCV.Text = "Product Input\r\nConveyor";
            this.statusInputCV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusEMO
            // 
            this.statusEMO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusEMO.Location = new System.Drawing.Point(224, 348);
            this.statusEMO.Name = "statusEMO";
            this.statusEMO.Size = new System.Drawing.Size(78, 32);
            this.statusEMO.TabIndex = 5;
            this.statusEMO.Tag = "1";
            this.statusEMO.Text = "EMO";
            this.statusEMO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusDoor
            // 
            this.statusDoor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusDoor.Location = new System.Drawing.Point(371, 348);
            this.statusDoor.Name = "statusDoor";
            this.statusDoor.Size = new System.Drawing.Size(96, 32);
            this.statusDoor.TabIndex = 5;
            this.statusDoor.Tag = "1";
            this.statusDoor.Text = "Door";
            this.statusDoor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusNGCv
            // 
            this.statusNGCv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusNGCv.Location = new System.Drawing.Point(510, 339);
            this.statusNGCv.Name = "statusNGCv";
            this.statusNGCv.Size = new System.Drawing.Size(112, 50);
            this.statusNGCv.TabIndex = 5;
            this.statusNGCv.Tag = "5";
            this.statusNGCv.Text = "NG\r\nConveyor";
            this.statusNGCv.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusOkCV
            // 
            this.statusOkCV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusOkCV.Location = new System.Drawing.Point(605, 258);
            this.statusOkCV.Name = "statusOkCV";
            this.statusOkCV.Size = new System.Drawing.Size(112, 50);
            this.statusOkCV.TabIndex = 5;
            this.statusOkCV.Tag = "5";
            this.statusOkCV.Text = "OK\r\nConveyor";
            this.statusOkCV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = global::bim_base.Properties.Resources.BAG_PACKING;
            this.pictureBox1.Location = new System.Drawing.Point(176, -18);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(456, 425);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 397);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(820, 266);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(820, 33);
            this.label2.TabIndex = 0;
            this.label2.Text = "Error List";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Controls.Add(this.alarmResetButton, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.exitButton, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 220);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(820, 46);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // alarmResetButton
            // 
            this.alarmResetButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alarmResetButton.Location = new System.Drawing.Point(659, 3);
            this.alarmResetButton.Name = "alarmResetButton";
            this.alarmResetButton.Size = new System.Drawing.Size(158, 40);
            this.alarmResetButton.TabIndex = 0;
            this.alarmResetButton.Text = "Alarm Reset";
            this.alarmResetButton.UseVisualStyleBackColor = true;
            this.alarmResetButton.Click += new System.EventHandler(this.alarmResetButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exitButton.Location = new System.Drawing.Point(3, 3);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(158, 40);
            this.exitButton.TabIndex = 0;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.errMsgLabel, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.errActLabel, 1, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 36);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(814, 181);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 89);
            this.label1.TabIndex = 0;
            this.label1.Text = "Message";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(4, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 89);
            this.label3.TabIndex = 1;
            this.label3.Text = "Solution";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // errMsgLabel
            // 
            this.errMsgLabel.AutoSize = true;
            this.errMsgLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errMsgLabel.Location = new System.Drawing.Point(169, 1);
            this.errMsgLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.errMsgLabel.Name = "errMsgLabel";
            this.errMsgLabel.Size = new System.Drawing.Size(639, 89);
            this.errMsgLabel.TabIndex = 2;
            this.errMsgLabel.Text = "Test Message";
            this.errMsgLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // errActLabel
            // 
            this.errActLabel.AutoSize = true;
            this.errActLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errActLabel.Location = new System.Drawing.Point(169, 91);
            this.errActLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.errActLabel.Name = "errActLabel";
            this.errActLabel.Size = new System.Drawing.Size(639, 89);
            this.errActLabel.TabIndex = 3;
            this.errActLabel.Text = "Test solution";
            this.errActLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiTimer
            // 
            this.uiTimer.Tick += new System.EventHandler(this.uiTimer_Tick);
            // 
            // FormAlarm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(820, 663);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAlarm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormSubErrorAlarm";
            this.Load += new System.EventHandler(this.FormSubErrorAlarm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.cPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CPanel cPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label statusInputCV;
        private System.Windows.Forms.Label statusOkCV;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button alarmResetButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label statusDoor;
        private System.Windows.Forms.Label statusProductHandler;
        private System.Windows.Forms.Timer uiTimer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label errMsgLabel;
        private System.Windows.Forms.Label errActLabel;
        private System.Windows.Forms.Label statusEMO;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label statusNGCv;
        private System.Windows.Forms.Label statusSealing;
        private System.Windows.Forms.Label statusLabelPrinter;
        private System.Windows.Forms.Label statusBagSupply;
        private System.Windows.Forms.Label statusLabelOutputHandler;
        private System.Windows.Forms.Label statusBagHandler;
    }
}