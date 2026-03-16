namespace Lib.UI.Generic.DarkMode.Forms
{
    partial class DarkMessageBox
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
            this.picIcon = new Lib.UI.Generic.DarkMode.Controls.DarkPictureBox();
            this.lblMessage = new Lib.UI.Generic.DarkMode.Controls.DarkLabel();
            this.cmbSelect = new Lib.UI.Generic.DarkMode.Controls.DarkComboBox();
            this.MainPanel.SuspendLayout();
            this.darkTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.darkTableLayoutPanel1);
            this.MainPanel.Location = new System.Drawing.Point(6, 59);
            this.MainPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MainPanel.Size = new System.Drawing.Size(524, 189);
            // 
            // darkTableLayoutPanel1
            // 
            this.darkTableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.darkTableLayoutPanel1.ColumnCount = 4;
            this.darkTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.darkTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.darkTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.darkTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.darkTableLayoutPanel1.Controls.Add(this.picIcon, 1, 1);
            this.darkTableLayoutPanel1.Controls.Add(this.lblMessage, 2, 1);
            this.darkTableLayoutPanel1.Controls.Add(this.cmbSelect, 2, 0);
            this.darkTableLayoutPanel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.darkTableLayoutPanel1.DarkLevel = 20;
            this.darkTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.darkTableLayoutPanel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.darkTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.darkTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.darkTableLayoutPanel1.Name = "darkTableLayoutPanel1";
            this.darkTableLayoutPanel1.RowCount = 4;
            this.darkTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.darkTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.darkTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.darkTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.darkTableLayoutPanel1.Size = new System.Drawing.Size(524, 189);
            this.darkTableLayoutPanel1.TabIndex = 0;
            // 
            // picIcon
            // 
            this.picIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.picIcon.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.picIcon.DarkLevel = 20;
            this.picIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.picIcon.Location = new System.Drawing.Point(36, 46);
            this.picIcon.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(39, 48);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picIcon.TabIndex = 0;
            this.picIcon.TabStop = false;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.lblMessage.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblMessage.DarkLevel = 20;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblMessage.Location = new System.Drawing.Point(85, 54);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(8, 11, 8, 11);
            this.lblMessage.Name = "lblMessage";
            this.darkTableLayoutPanel1.SetRowSpan(this.lblMessage, 2);
            this.lblMessage.Size = new System.Drawing.Size(397, 81);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "message";
            // 
            // cmbSelect
            // 
            this.cmbSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.cmbSelect.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cmbSelect.DarkLevel = 30;
            this.cmbSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelect.Font = new System.Drawing.Font("Gulim", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmbSelect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbSelect.FormattingEnabled = true;
            this.cmbSelect.Location = new System.Drawing.Point(79, 2);
            this.cmbSelect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbSelect.Name = "cmbSelect";
            this.cmbSelect.Size = new System.Drawing.Size(409, 35);
            this.cmbSelect.TabIndex = 2;
            this.cmbSelect.Visible = false;
            this.cmbSelect.SelectedIndexChanged += new System.EventHandler(this.cmbSelect_SelectedIndexChanged);
            // 
            // DarkMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(536, 257);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DarkMessageBox";
            this.OptionBoxAlignment = Lib.UI.Generic.DarkMode.Controls.EnumTitleOptionBoxAlignment.Right;
            this.Padding = new System.Windows.Forms.Padding(6, 9, 6, 9);
            this.Text = "Dark Form";
            this.TitleButtonText1 = "Cancel";
            this.TitleButtonText2 = "OK";
            this.TitleButtonVisible1 = true;
            this.TitleButtonVisible2 = true;
            this.TitleControlBox = false;
            this.TitleText = "Title";
            this.TitleVisible = true;
            this.MainPanel.ResumeLayout(false);
            this.darkTableLayoutPanel1.ResumeLayout(false);
            this.darkTableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.DarkTableLayoutPanel darkTableLayoutPanel1;
        private Controls.DarkPictureBox picIcon;
        private Controls.DarkLabel lblMessage;
        private Controls.DarkComboBox cmbSelect;
    }
}
