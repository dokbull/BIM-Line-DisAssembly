namespace Lib.UI.Generic.DarkMode.Forms
{
    partial class DarkLoginForm
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.darkTableLayoutPanel1 = new Lib.UI.Generic.DarkMode.Controls.DarkTableLayoutPanel();
            this.txtPassword = new Lib.UI.Generic.DarkMode.Controls.DarkTextBox();
            this.lblPassword = new Lib.UI.Generic.DarkMode.Controls.DarkLabel();
            this.lblID = new Lib.UI.Generic.DarkMode.Controls.DarkLabel();
            this.txtID = new Lib.UI.Generic.DarkMode.Controls.DarkTextBox();
            this.picLockIcon = new Lib.UI.Generic.DarkMode.Controls.DarkPictureBox();
            this.MainPanel.SuspendLayout();
            this.darkTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLockIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // DarkMainPanel
            // 
            this.MainPanel.Controls.Add(this.darkTableLayoutPanel1);
            this.MainPanel.Padding = new System.Windows.Forms.Padding(25);
            this.MainPanel.Size = new System.Drawing.Size(423, 124);
            // 
            // darkTableLayoutPanel1
            // 
            this.darkTableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.darkTableLayoutPanel1.ColumnCount = 3;
            this.darkTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.darkTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.darkTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.darkTableLayoutPanel1.Controls.Add(this.txtPassword, 2, 1);
            this.darkTableLayoutPanel1.Controls.Add(this.lblPassword, 1, 1);
            this.darkTableLayoutPanel1.Controls.Add(this.lblID, 1, 0);
            this.darkTableLayoutPanel1.Controls.Add(this.txtID, 2, 0);
            this.darkTableLayoutPanel1.Controls.Add(this.picLockIcon, 0, 0);
            this.darkTableLayoutPanel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.darkTableLayoutPanel1.DarkLevel = 20;
            this.darkTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.darkTableLayoutPanel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.darkTableLayoutPanel1.Location = new System.Drawing.Point(25, 25);
            this.darkTableLayoutPanel1.Name = "darkTableLayoutPanel1";
            this.darkTableLayoutPanel1.RowCount = 2;
            this.darkTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.darkTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.darkTableLayoutPanel1.Size = new System.Drawing.Size(373, 74);
            this.darkTableLayoutPanel1.TabIndex = 0;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPassword.DarkLevel = 50;
            this.txtPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtPassword.Location = new System.Drawing.Point(203, 47);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(10);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(160, 21);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInput_KeyPress);
            // 
            // lblPassword
            // 
            this.lblPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.lblPassword.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblPassword.DarkLevel = 20;
            this.lblPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblPassword.Location = new System.Drawing.Point(84, 47);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(10);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(99, 17);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblID
            // 
            this.lblID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.lblID.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblID.DarkLevel = 20;
            this.lblID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblID.Location = new System.Drawing.Point(84, 10);
            this.lblID.Margin = new System.Windows.Forms.Padding(10);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(99, 17);
            this.lblID.TabIndex = 0;
            this.lblID.Text = "ID";
            this.lblID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtID
            // 
            this.txtID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.txtID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtID.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtID.DarkLevel = 50;
            this.txtID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtID.Location = new System.Drawing.Point(203, 10);
            this.txtID.Margin = new System.Windows.Forms.Padding(10);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(160, 21);
            this.txtID.TabIndex = 1;
            this.txtID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInput_KeyPress);
            // 
            // picLockIcon
            // 
            this.picLockIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.picLockIcon.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.picLockIcon.DarkLevel = 20;
            this.picLockIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picLockIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.picLockIcon.Location = new System.Drawing.Point(12, 12);
            this.picLockIcon.Margin = new System.Windows.Forms.Padding(12);
            this.picLockIcon.Name = "picLockIcon";
            this.darkTableLayoutPanel1.SetRowSpan(this.picLockIcon, 2);
            this.picLockIcon.Size = new System.Drawing.Size(50, 50);
            this.picLockIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picLockIcon.TabIndex = 4;
            this.picLockIcon.TabStop = false;
            // 
            // DarkLoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.ClientSize = new System.Drawing.Size(433, 180);
            this.FormSizable = false;
            this.Name = "DarkLoginForm";
            this.OptionBox = true;
            this.OptionBoxAlignment = Lib.UI.Generic.DarkMode.Controls.EnumTitleOptionBoxAlignment.Right;
            this.ShowIcon = true;
            this.Text = "Dark Form";
            this.TitleButtonText1 = "취소";
            this.TitleButtonText2 = "확인";
            this.TitleButtonVisible1 = true;
            this.TitleButtonVisible2 = true;
            this.TitleControlBox = false;
            this.TitleText = "Login";
            this.TitleVisible = true;
            this.Load += new System.EventHandler(this.FormLogin_Load);
            this.MainPanel.ResumeLayout(false);
            this.darkTableLayoutPanel1.ResumeLayout(false);
            this.darkTableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLockIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.DarkTableLayoutPanel darkTableLayoutPanel1;
        private Controls.DarkTextBox txtPassword;
        private Controls.DarkLabel lblPassword;
        private Controls.DarkLabel lblID;
        private Controls.DarkTextBox txtID;
        private Controls.DarkPictureBox picLockIcon;
    }
}
