namespace bim_base
{
    partial class AxisLimitControl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.axisGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.axisSpeedButton = new System.Windows.Forms.Button();
            this.axisMinusButton = new System.Windows.Forms.Button();
            this.axisPlusButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.negLimitButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.posLimitButton = new System.Windows.Forms.Button();
            this.axisCurPosLabel = new System.Windows.Forms.Label();
            this.axisNegLimitLabel = new System.Windows.Forms.Label();
            this.axisPosLimitLabel = new System.Windows.Forms.Label();
            this.ui_timer = new System.Windows.Forms.Timer(this.components);
            this.axisGroupBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // axisGroupBox
            // 
            this.axisGroupBox.Controls.Add(this.tableLayoutPanel1);
            this.axisGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axisGroupBox.Location = new System.Drawing.Point(0, 0);
            this.axisGroupBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.axisGroupBox.Name = "axisGroupBox";
            this.axisGroupBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.axisGroupBox.Size = new System.Drawing.Size(251, 336);
            this.axisGroupBox.TabIndex = 335;
            this.axisGroupBox.TabStop = false;
            this.axisGroupBox.Text = "Axis-X";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 10;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.negLimitButton, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.posLimitButton, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.axisCurPosLabel, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.axisNegLimitLabel, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.axisPosLimitLabel, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(243, 304);
            this.tableLayoutPanel1.TabIndex = 332;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 10);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.axisSpeedButton, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.axisMinusButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.axisPlusButton, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 183);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(237, 54);
            this.tableLayoutPanel2.TabIndex = 333;
            // 
            // axisSpeedButton
            // 
            this.axisSpeedButton.BackColor = System.Drawing.Color.Transparent;
            this.axisSpeedButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axisSpeedButton.ForeColor = System.Drawing.Color.Black;
            this.axisSpeedButton.Location = new System.Drawing.Point(83, 0);
            this.axisSpeedButton.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.axisSpeedButton.Name = "axisSpeedButton";
            this.axisSpeedButton.Size = new System.Drawing.Size(71, 54);
            this.axisSpeedButton.TabIndex = 230;
            this.axisSpeedButton.Tag = "0";
            this.axisSpeedButton.Text = "LOW";
            this.axisSpeedButton.UseVisualStyleBackColor = false;
            this.axisSpeedButton.Click += new System.EventHandler(this.axisSpeedButton_Click);
            // 
            // axisMinusButton
            // 
            this.axisMinusButton.BackColor = System.Drawing.Color.Transparent;
            this.axisMinusButton.BackgroundImage = bim_base.Properties.Resources.jog_left;
            this.axisMinusButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.axisMinusButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axisMinusButton.ForeColor = System.Drawing.Color.Black;
            this.axisMinusButton.Location = new System.Drawing.Point(4, 0);
            this.axisMinusButton.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.axisMinusButton.Name = "axisMinusButton";
            this.axisMinusButton.Size = new System.Drawing.Size(71, 54);
            this.axisMinusButton.TabIndex = 224;
            this.axisMinusButton.Tag = "left";
            this.axisMinusButton.UseVisualStyleBackColor = false;
            this.axisMinusButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.axisMinusButton_MouseDown);
            this.axisMinusButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.axisButton_MouseUp);
            // 
            // axisPlusButton
            // 
            this.axisPlusButton.BackColor = System.Drawing.Color.Transparent;
            this.axisPlusButton.BackgroundImage = bim_base.Properties.Resources.jog_right;
            this.axisPlusButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.axisPlusButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axisPlusButton.ForeColor = System.Drawing.Color.Black;
            this.axisPlusButton.Location = new System.Drawing.Point(162, 0);
            this.axisPlusButton.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.axisPlusButton.Name = "axisPlusButton";
            this.axisPlusButton.Size = new System.Drawing.Size(71, 54);
            this.axisPlusButton.TabIndex = 225;
            this.axisPlusButton.Tag = "right";
            this.axisPlusButton.UseVisualStyleBackColor = false;
            this.axisPlusButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.axisPlusButton_MouseDown);
            this.axisPlusButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.axisButton_MouseUp);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 4);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(4, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 56);
            this.label1.TabIndex = 308;
            this.label1.Text = "Limit +";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.label3, 4);
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(4, 122);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 56);
            this.label3.TabIndex = 310;
            this.label3.Text = "Current Position";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // negLimitButton
            // 
            this.negLimitButton.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.SetColumnSpan(this.negLimitButton, 5);
            this.negLimitButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.negLimitButton.ForeColor = System.Drawing.Color.Black;
            this.negLimitButton.Location = new System.Drawing.Point(4, 242);
            this.negLimitButton.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.negLimitButton.Name = "negLimitButton";
            this.negLimitButton.Size = new System.Drawing.Size(112, 60);
            this.negLimitButton.TabIndex = 324;
            this.negLimitButton.Text = "Set Limit -";
            this.negLimitButton.UseVisualStyleBackColor = false;
            this.negLimitButton.Click += new System.EventHandler(this.negLimitButton_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.label2, 4);
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(4, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 56);
            this.label2.TabIndex = 310;
            this.label2.Text = "Limit -";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // posLimitButton
            // 
            this.posLimitButton.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.SetColumnSpan(this.posLimitButton, 5);
            this.posLimitButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.posLimitButton.ForeColor = System.Drawing.Color.Black;
            this.posLimitButton.Location = new System.Drawing.Point(124, 242);
            this.posLimitButton.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.posLimitButton.Name = "posLimitButton";
            this.posLimitButton.Size = new System.Drawing.Size(115, 60);
            this.posLimitButton.TabIndex = 325;
            this.posLimitButton.Text = "Set Limit+";
            this.posLimitButton.UseVisualStyleBackColor = false;
            this.posLimitButton.Click += new System.EventHandler(this.posLimitButton_Click);
            // 
            // axisCurPosLabel
            // 
            this.axisCurPosLabel.BackColor = System.Drawing.Color.Transparent;
            this.axisCurPosLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.axisCurPosLabel, 6);
            this.axisCurPosLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axisCurPosLabel.ForeColor = System.Drawing.Color.Black;
            this.axisCurPosLabel.Location = new System.Drawing.Point(100, 122);
            this.axisCurPosLabel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.axisCurPosLabel.Name = "axisCurPosLabel";
            this.axisCurPosLabel.Size = new System.Drawing.Size(139, 56);
            this.axisCurPosLabel.TabIndex = 331;
            this.axisCurPosLabel.Text = "--";
            this.axisCurPosLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // axisNegLimitLabel
            // 
            this.axisNegLimitLabel.BackColor = System.Drawing.Color.Transparent;
            this.axisNegLimitLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.axisNegLimitLabel, 6);
            this.axisNegLimitLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axisNegLimitLabel.ForeColor = System.Drawing.Color.Black;
            this.axisNegLimitLabel.Location = new System.Drawing.Point(100, 62);
            this.axisNegLimitLabel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.axisNegLimitLabel.Name = "axisNegLimitLabel";
            this.axisNegLimitLabel.Size = new System.Drawing.Size(139, 56);
            this.axisNegLimitLabel.TabIndex = 311;
            this.axisNegLimitLabel.Text = "--";
            this.axisNegLimitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // axisPosLimitLabel
            // 
            this.axisPosLimitLabel.BackColor = System.Drawing.Color.Transparent;
            this.axisPosLimitLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.axisPosLimitLabel, 6);
            this.axisPosLimitLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axisPosLimitLabel.ForeColor = System.Drawing.Color.Black;
            this.axisPosLimitLabel.Location = new System.Drawing.Point(100, 2);
            this.axisPosLimitLabel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.axisPosLimitLabel.Name = "axisPosLimitLabel";
            this.axisPosLimitLabel.Size = new System.Drawing.Size(139, 56);
            this.axisPosLimitLabel.TabIndex = 309;
            this.axisPosLimitLabel.Text = "--";
            this.axisPosLimitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ui_timer
            // 
            this.ui_timer.Enabled = true;
            this.ui_timer.Tick += new System.EventHandler(this.ui_timer_Tick);
            // 
            // AxisLimitControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.axisGroupBox);
            this.Font = new System.Drawing.Font("SamsungOne 800", 12F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AxisLimitControl";
            this.Size = new System.Drawing.Size(251, 336);
            this.Load += new System.EventHandler(this.AxisLimitControl_Load);
            this.axisGroupBox.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox axisGroupBox;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label axisPosLimitLabel;
        public System.Windows.Forms.Label axisNegLimitLabel;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label axisCurPosLabel;
        public System.Windows.Forms.Button axisSpeedButton;
        public System.Windows.Forms.Button axisMinusButton;
        public System.Windows.Forms.Button axisPlusButton;
        public System.Windows.Forms.Button posLimitButton;
        public System.Windows.Forms.Button negLimitButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Timer ui_timer;
    }
}
