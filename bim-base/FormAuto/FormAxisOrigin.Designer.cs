namespace bim_base
{
    partial class FormAxisOrigin
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.inPP_Y = new AjinMiniStatus();
            this.inPP_Z = new AjinMiniStatus();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.allSelectButton = new System.Windows.Forms.Button();
            this.cancelAllButton = new System.Windows.Forms.Button();
            this.servoOnButton = new System.Windows.Forms.Button();
            this.servoOffButton = new System.Windows.Forms.Button();
            this.execOriginButton = new System.Windows.Forms.Button();
            this.resetAlarmButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.uiTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.moldPP_X = new AjinMiniStatus();
            this.moldPP_Z1 = new AjinMiniStatus();
            this.moldPP_Z2 = new AjinMiniStatus();
            this.outPP_Z = new AjinMiniStatus();
            this.outPP_Y = new AjinMiniStatus();
            this.shuttle_X = new AjinMiniStatus();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 12);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(845, 417);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(839, 325);
            this.panel1.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel5.ColumnCount = 4;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.inPP_Y, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.inPP_Z, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.moldPP_X, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.moldPP_Z1, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.moldPP_Z2, 2, 3);
            this.tableLayoutPanel5.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.outPP_Y, 2, 1);
            this.tableLayoutPanel5.Controls.Add(this.outPP_Z, 3, 1);
            this.tableLayoutPanel5.Controls.Add(this.label4, 3, 2);
            this.tableLayoutPanel5.Controls.Add(this.shuttle_X, 3, 3);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(839, 325);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // inPP_Y
            // 
            this.inPP_Y._SELECT = false;
            this.inPP_Y.BackColor = System.Drawing.Color.Transparent;
            this.inPP_Y.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inPP_Y.Location = new System.Drawing.Point(5, 46);
            this.inPP_Y.Margin = new System.Windows.Forms.Padding(4);
            this.inPP_Y.Name = "inPP_Y";
            this.inPP_Y.Size = new System.Drawing.Size(200, 111);
            this.inPP_Y.TabIndex = 8;
            // 
            // inPP_Z
            // 
            this.inPP_Z._SELECT = false;
            this.inPP_Z.BackColor = System.Drawing.Color.Transparent;
            this.inPP_Z.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inPP_Z.Location = new System.Drawing.Point(214, 46);
            this.inPP_Z.Margin = new System.Windows.Forms.Padding(4);
            this.inPP_Z.Name = "inPP_Z";
            this.inPP_Z.Size = new System.Drawing.Size(200, 111);
            this.inPP_Z.TabIndex = 8;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 9;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.68064F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.68064F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.68064F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.68064F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.117763F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.68064F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.68064F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.117763F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.68064F));
            this.tableLayoutPanel2.Controls.Add(this.allSelectButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cancelAllButton, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.servoOnButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.servoOffButton, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.execOriginButton, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.resetAlarmButton, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.exitButton, 8, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 333);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(845, 84);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // allSelectButton
            // 
            this.allSelectButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.allSelectButton.Location = new System.Drawing.Point(3, 4);
            this.allSelectButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.allSelectButton.Name = "allSelectButton";
            this.allSelectButton.Size = new System.Drawing.Size(109, 76);
            this.allSelectButton.TabIndex = 0;
            this.allSelectButton.Text = "Select\r\nAll";
            this.allSelectButton.UseVisualStyleBackColor = true;
            this.allSelectButton.Click += new System.EventHandler(this.allSelectButton_Click);
            // 
            // cancelAllButton
            // 
            this.cancelAllButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cancelAllButton.Location = new System.Drawing.Point(118, 4);
            this.cancelAllButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelAllButton.Name = "cancelAllButton";
            this.cancelAllButton.Size = new System.Drawing.Size(109, 76);
            this.cancelAllButton.TabIndex = 1;
            this.cancelAllButton.Text = "Cancel\r\nAll";
            this.cancelAllButton.UseVisualStyleBackColor = true;
            this.cancelAllButton.Click += new System.EventHandler(this.cancleAllButton_Click);
            // 
            // servoOnButton
            // 
            this.servoOnButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.servoOnButton.Location = new System.Drawing.Point(233, 4);
            this.servoOnButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.servoOnButton.Name = "servoOnButton";
            this.servoOnButton.Size = new System.Drawing.Size(109, 76);
            this.servoOnButton.TabIndex = 2;
            this.servoOnButton.Text = "Servo\r\nOn";
            this.servoOnButton.UseVisualStyleBackColor = true;
            this.servoOnButton.Click += new System.EventHandler(this.servoOnButton_Click);
            // 
            // servoOffButton
            // 
            this.servoOffButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.servoOffButton.Location = new System.Drawing.Point(348, 4);
            this.servoOffButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.servoOffButton.Name = "servoOffButton";
            this.servoOffButton.Size = new System.Drawing.Size(109, 76);
            this.servoOffButton.TabIndex = 3;
            this.servoOffButton.Text = "Servo\r\nOff";
            this.servoOffButton.UseVisualStyleBackColor = true;
            this.servoOffButton.Click += new System.EventHandler(this.servoOffButton_Click);
            // 
            // execOriginButton
            // 
            this.execOriginButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.execOriginButton.Location = new System.Drawing.Point(480, 4);
            this.execOriginButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.execOriginButton.Name = "execOriginButton";
            this.execOriginButton.Size = new System.Drawing.Size(109, 76);
            this.execOriginButton.TabIndex = 4;
            this.execOriginButton.Text = "Execute\r\nOrigin";
            this.execOriginButton.UseVisualStyleBackColor = true;
            this.execOriginButton.Click += new System.EventHandler(this.execOriginButton_Click);
            // 
            // resetAlarmButton
            // 
            this.resetAlarmButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resetAlarmButton.Location = new System.Drawing.Point(595, 4);
            this.resetAlarmButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.resetAlarmButton.Name = "resetAlarmButton";
            this.resetAlarmButton.Size = new System.Drawing.Size(109, 76);
            this.resetAlarmButton.TabIndex = 5;
            this.resetAlarmButton.Text = "Reset\r\nAlarm";
            this.resetAlarmButton.UseVisualStyleBackColor = true;
            this.resetAlarmButton.Click += new System.EventHandler(this.resetAlarmButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exitButton.Location = new System.Drawing.Point(727, 4);
            this.exitButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(115, 76);
            this.exitButton.TabIndex = 6;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // uiTimer
            // 
            this.uiTimer.Interval = 20;
            this.uiTimer.Tick += new System.EventHandler(this.uiTimer_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SamsungOne 800", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "IN PP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // moldPP_X
            // 
            this.moldPP_X._SELECT = false;
            this.moldPP_X.BackColor = System.Drawing.Color.Transparent;
            this.moldPP_X.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moldPP_X.Location = new System.Drawing.Point(5, 208);
            this.moldPP_X.Margin = new System.Windows.Forms.Padding(4);
            this.moldPP_X.Name = "moldPP_X";
            this.moldPP_X.Size = new System.Drawing.Size(200, 112);
            this.moldPP_X.TabIndex = 10;
            // 
            // moldPP_Z1
            // 
            this.moldPP_Z1._SELECT = false;
            this.moldPP_Z1.BackColor = System.Drawing.Color.Transparent;
            this.moldPP_Z1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moldPP_Z1.Location = new System.Drawing.Point(214, 208);
            this.moldPP_Z1.Margin = new System.Windows.Forms.Padding(4);
            this.moldPP_Z1.Name = "moldPP_Z1";
            this.moldPP_Z1.Size = new System.Drawing.Size(200, 112);
            this.moldPP_Z1.TabIndex = 10;
            // 
            // moldPP_Z2
            // 
            this.moldPP_Z2._SELECT = false;
            this.moldPP_Z2.BackColor = System.Drawing.Color.Transparent;
            this.moldPP_Z2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moldPP_Z2.Location = new System.Drawing.Point(423, 208);
            this.moldPP_Z2.Margin = new System.Windows.Forms.Padding(4);
            this.moldPP_Z2.Name = "moldPP_Z2";
            this.moldPP_Z2.Size = new System.Drawing.Size(200, 112);
            this.moldPP_Z2.TabIndex = 10;
            // 
            // outPP_Z
            // 
            this.outPP_Z._SELECT = false;
            this.outPP_Z.BackColor = System.Drawing.Color.Transparent;
            this.outPP_Z.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outPP_Z.Location = new System.Drawing.Point(632, 46);
            this.outPP_Z.Margin = new System.Windows.Forms.Padding(4);
            this.outPP_Z.Name = "outPP_Z";
            this.outPP_Z.Size = new System.Drawing.Size(202, 112);
            this.outPP_Z.TabIndex = 10;
            // 
            // outPP_Y
            // 
            this.outPP_Y._SELECT = false;
            this.outPP_Y.BackColor = System.Drawing.Color.Transparent;
            this.outPP_Y.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outPP_Y.Location = new System.Drawing.Point(423, 46);
            this.outPP_Y.Margin = new System.Windows.Forms.Padding(4);
            this.outPP_Y.Name = "outPP_Y";
            this.outPP_Y.Size = new System.Drawing.Size(200, 112);
            this.outPP_Y.TabIndex = 10;
            // 
            // shuttle_X
            // 
            this.shuttle_X._SELECT = false;
            this.shuttle_X.BackColor = System.Drawing.Color.Transparent;
            this.shuttle_X.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shuttle_X.Location = new System.Drawing.Point(632, 208);
            this.shuttle_X.Margin = new System.Windows.Forms.Padding(4);
            this.shuttle_X.Name = "shuttle_X";
            this.shuttle_X.Size = new System.Drawing.Size(202, 112);
            this.shuttle_X.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SamsungOne 800", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1, 163);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "MOLD PP";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SamsungOne 800", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(419, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "OUT PP";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SamsungOne 800", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(628, 163);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 25);
            this.label4.TabIndex = 9;
            this.label4.Text = "MOLD SHUTTLE";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormAxisOrigin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(865, 441);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("SamsungOne 800", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAxisOrigin";
            this.Padding = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Origin";
            this.Load += new System.EventHandler(this.FormSubAxisOrigin_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button allSelectButton;
        private System.Windows.Forms.Button cancelAllButton;
        private System.Windows.Forms.Button servoOnButton;
        private System.Windows.Forms.Button servoOffButton;
        private System.Windows.Forms.Button execOriginButton;
        private System.Windows.Forms.Button resetAlarmButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer uiTimer;
        private AjinMiniStatus inPP_Y;
        private AjinMiniStatus inPP_Z;
        private System.Windows.Forms.Label label1;
        private AjinMiniStatus moldPP_X;
        private AjinMiniStatus moldPP_Z1;
        private AjinMiniStatus moldPP_Z2;
        private AjinMiniStatus outPP_Z;
        private AjinMiniStatus outPP_Y;
        private AjinMiniStatus shuttle_X;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}