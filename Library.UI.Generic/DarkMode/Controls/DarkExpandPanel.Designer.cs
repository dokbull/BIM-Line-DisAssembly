namespace Lib.UI.Generic.DarkMode.Controls
{
    partial class DarkExpandPanel
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
            this.ExpandButton = new Lib.UI.Generic.DarkMode.Controls.DarkButton();
            this.SuspendLayout();
            // 
            // ExpandButton
            // 
            this.ExpandButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ExpandButton.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ExpandButton.DarkLevel = 30;
            this.ExpandButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ExpandButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ExpandButton.Location = new System.Drawing.Point(0, 0);
            this.ExpandButton.Name = "ExpandButton";
            this.ExpandButton.Size = new System.Drawing.Size(75, 23);
            this.ExpandButton.TabIndex = 0;
            this.ExpandButton.UseVisualStyleBackColor = false;
            this.ExpandButton.Click += new System.EventHandler(this.ExpandButton_Click);
            // 
            // DarkExpandPanel
            // 
            this.Size = new System.Drawing.Size(220, 204);
            this.Resize += new System.EventHandler(this.ExpandPanel_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private DarkButton ExpandButton;
    }
}
