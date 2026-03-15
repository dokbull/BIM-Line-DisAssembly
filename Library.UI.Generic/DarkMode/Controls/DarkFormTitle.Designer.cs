namespace Lib.UI.Generic.DarkMode.Controls
{
    partial class DarkFormTitle
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
            this.pnlTitle = new Lib.UI.Generic.DarkMode.Controls.DarkPanel();
            this.pnlOption = new Lib.UI.Generic.DarkMode.Controls.DarkFlowLayoutPanel();
            this.txtTitleTextBox = new Lib.UI.Generic.DarkMode.Controls.DarkTextBox();
            this.btnTitleButton1 = new Lib.UI.Generic.DarkMode.Controls.DarkButton();
            this.btnTitleButton2 = new Lib.UI.Generic.DarkMode.Controls.DarkButton();
            this.lblTitleLabel = new Lib.UI.Generic.DarkMode.Controls.DarkLabel();
            this.lblTitleText = new Lib.UI.Generic.DarkMode.Controls.DarkLabel();
            this.picIcon = new Lib.UI.Generic.DarkMode.Controls.DarkPictureBox();
            this.pnlControlBox = new Lib.UI.Generic.DarkMode.Controls.DarkFlowLayoutPanel();
            this.btnClose = new Lib.UI.Generic.DarkMode.Controls.DarkButton();
            this.btnMaxBox = new Lib.UI.Generic.DarkMode.Controls.DarkButton();
            this.btnMinBox = new Lib.UI.Generic.DarkMode.Controls.DarkButton();
            this.pnlTitle.SuspendLayout();
            this.pnlOption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.pnlControlBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlTitle.Controls.Add(this.pnlOption);
            this.pnlTitle.Controls.Add(this.lblTitleText);
            this.pnlTitle.Controls.Add(this.picIcon);
            this.pnlTitle.Controls.Add(this.pnlControlBox);
            this.pnlTitle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlTitle.DarkLevel = 30;
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Padding = new System.Windows.Forms.Padding(7);
            this.pnlTitle.Size = new System.Drawing.Size(866, 48);
            this.pnlTitle.TabIndex = 1;
            // 
            // pnlOption
            // 
            this.pnlOption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlOption.Controls.Add(this.txtTitleTextBox);
            this.pnlOption.Controls.Add(this.btnTitleButton1);
            this.pnlOption.Controls.Add(this.btnTitleButton2);
            this.pnlOption.Controls.Add(this.lblTitleLabel);
            this.pnlOption.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlOption.DarkLevel = 30;
            this.pnlOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pnlOption.Location = new System.Drawing.Point(124, 7);
            this.pnlOption.Name = "pnlOption";
            this.pnlOption.Size = new System.Drawing.Size(624, 34);
            this.pnlOption.TabIndex = 2;
            this.pnlOption.DoubleClick += new System.EventHandler(this.Title_DoubleClickEvent);
            this.pnlOption.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Title_MouseDown);
            this.pnlOption.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Title_MouseMove);
            this.pnlOption.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Title_MouseUp);
            // 
            // txtTitleTextBox
            // 
            this.txtTitleTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.txtTitleTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTitleTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTitleTextBox.DarkLevel = 70;
            this.txtTitleTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtTitleTextBox.Location = new System.Drawing.Point(7, 7);
            this.txtTitleTextBox.Margin = new System.Windows.Forms.Padding(7);
            this.txtTitleTextBox.Name = "txtTitleTextBox";
            this.txtTitleTextBox.Size = new System.Drawing.Size(100, 21);
            this.txtTitleTextBox.TabIndex = 5;
            this.txtTitleTextBox.Visible = false;
            // 
            // btnTitleButton1
            // 
            this.btnTitleButton1.AutoSize = true;
            this.btnTitleButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnTitleButton1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnTitleButton1.DarkLevel = 30;
            this.btnTitleButton1.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnTitleButton1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnTitleButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnTitleButton1.Location = new System.Drawing.Point(114, 0);
            this.btnTitleButton1.Margin = new System.Windows.Forms.Padding(0);
            this.btnTitleButton1.Name = "btnTitleButton1";
            this.btnTitleButton1.Size = new System.Drawing.Size(75, 35);
            this.btnTitleButton1.TabIndex = 2;
            this.btnTitleButton1.Text = "Button1";
            this.btnTitleButton1.UseVisualStyleBackColor = false;
            this.btnTitleButton1.Visible = false;
            this.btnTitleButton1.Click += new System.EventHandler(this.btnTitleButton2_Click);
            // 
            // btnTitleButton2
            // 
            this.btnTitleButton2.AutoSize = true;
            this.btnTitleButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnTitleButton2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnTitleButton2.DarkLevel = 30;
            this.btnTitleButton2.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnTitleButton2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnTitleButton2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnTitleButton2.Location = new System.Drawing.Point(189, 0);
            this.btnTitleButton2.Margin = new System.Windows.Forms.Padding(0);
            this.btnTitleButton2.Name = "btnTitleButton2";
            this.btnTitleButton2.Size = new System.Drawing.Size(75, 35);
            this.btnTitleButton2.TabIndex = 4;
            this.btnTitleButton2.Text = "Button2";
            this.btnTitleButton2.UseVisualStyleBackColor = false;
            this.btnTitleButton2.Visible = false;
            this.btnTitleButton2.Click += new System.EventHandler(this.btnTitleButton2_Click);
            // 
            // lblTitleLabel
            // 
            this.lblTitleLabel.AutoSize = true;
            this.lblTitleLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.lblTitleLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTitleLabel.DarkLevel = 70;
            this.lblTitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblTitleLabel.Location = new System.Drawing.Point(264, 0);
            this.lblTitleLabel.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitleLabel.Name = "lblTitleLabel";
            this.lblTitleLabel.Padding = new System.Windows.Forms.Padding(10);
            this.lblTitleLabel.Size = new System.Drawing.Size(56, 32);
            this.lblTitleLabel.TabIndex = 3;
            this.lblTitleLabel.Text = "Label";
            this.lblTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitleLabel.Visible = false;
            this.lblTitleLabel.DoubleClick += new System.EventHandler(this.Title_DoubleClickEvent);
            this.lblTitleLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Title_MouseDown);
            this.lblTitleLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Title_MouseMove);
            this.lblTitleLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Title_MouseUp);
            // 
            // lblTitleText
            // 
            this.lblTitleText.AutoSize = true;
            this.lblTitleText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblTitleText.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTitleText.DarkLevel = 30;
            this.lblTitleText.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTitleText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblTitleText.Location = new System.Drawing.Point(41, 7);
            this.lblTitleText.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitleText.Name = "lblTitleText";
            this.lblTitleText.Padding = new System.Windows.Forms.Padding(10);
            this.lblTitleText.Size = new System.Drawing.Size(83, 32);
            this.lblTitleText.TabIndex = 0;
            this.lblTitleText.Text = "Dark Form";
            this.lblTitleText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitleText.DoubleClick += new System.EventHandler(this.Title_DoubleClickEvent);
            this.lblTitleText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Title_MouseDown);
            this.lblTitleText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Title_MouseMove);
            this.lblTitleText.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Title_MouseUp);
            // 
            // picIcon
            // 
            this.picIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.picIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picIcon.DarkLevel = 30;
            this.picIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.picIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.picIcon.Location = new System.Drawing.Point(7, 7);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(34, 34);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picIcon.TabIndex = 1;
            this.picIcon.TabStop = false;
            this.picIcon.DoubleClick += new System.EventHandler(this.Title_DoubleClickEvent);
            this.picIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Title_MouseDown);
            this.picIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Title_MouseMove);
            this.picIcon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Title_MouseUp);
            // 
            // pnlControlBox
            // 
            this.pnlControlBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlControlBox.Controls.Add(this.btnClose);
            this.pnlControlBox.Controls.Add(this.btnMaxBox);
            this.pnlControlBox.Controls.Add(this.btnMinBox);
            this.pnlControlBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pnlControlBox.DarkLevel = 30;
            this.pnlControlBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControlBox.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlControlBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pnlControlBox.Location = new System.Drawing.Point(748, 7);
            this.pnlControlBox.Margin = new System.Windows.Forms.Padding(0);
            this.pnlControlBox.Name = "pnlControlBox";
            this.pnlControlBox.Size = new System.Drawing.Size(111, 34);
            this.pnlControlBox.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnClose.DarkLevel = 30;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnClose.Location = new System.Drawing.Point(83, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(25, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMaxBox
            // 
            this.btnMaxBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnMaxBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnMaxBox.DarkLevel = 30;
            this.btnMaxBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMaxBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMaxBox.Location = new System.Drawing.Point(52, 3);
            this.btnMaxBox.Name = "btnMaxBox";
            this.btnMaxBox.Size = new System.Drawing.Size(25, 25);
            this.btnMaxBox.TabIndex = 1;
            this.btnMaxBox.Text = "□";
            this.btnMaxBox.UseVisualStyleBackColor = false;
            this.btnMaxBox.Click += new System.EventHandler(this.btnMaxBox_Click);
            // 
            // btnMinBox
            // 
            this.btnMinBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnMinBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnMinBox.DarkLevel = 30;
            this.btnMinBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMinBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMinBox.Location = new System.Drawing.Point(21, 3);
            this.btnMinBox.Name = "btnMinBox";
            this.btnMinBox.Size = new System.Drawing.Size(25, 25);
            this.btnMinBox.TabIndex = 2;
            this.btnMinBox.Text = "_";
            this.btnMinBox.UseVisualStyleBackColor = false;
            this.btnMinBox.Click += new System.EventHandler(this.btnMinBox_Click);
            // 
            // DarkFormTitle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlTitle);
            this.Name = "DarkFormTitle";
            this.Size = new System.Drawing.Size(866, 48);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.pnlOption.ResumeLayout(false);
            this.pnlOption.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.pnlControlBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DarkPanel pnlTitle;
        private DarkFlowLayoutPanel pnlOption;
        private DarkLabel lblTitleText;
        private DarkButton btnTitleButton1;
        private DarkLabel lblTitleLabel;
        private DarkPictureBox picIcon;
        private DarkFlowLayoutPanel pnlControlBox;
        private DarkButton btnClose;
        private DarkButton btnMaxBox;
        private DarkButton btnMinBox;
        private DarkButton btnTitleButton2;
        private DarkTextBox txtTitleTextBox;
    }
}
