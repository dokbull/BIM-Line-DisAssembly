namespace bim_base
{
    partial class FormOperatorCallHistory
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
            this.gridHistory = new CSourceGrid();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.gridHistory);
            this.MainPanel.Location = new System.Drawing.Point(7, 77);
            this.MainPanel.Size = new System.Drawing.Size(1129, 590);
            // 
            // gridHistory
            // 
            this.gridHistory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridHistory.EnableSort = true;
            this.gridHistory.Location = new System.Drawing.Point(0, 0);
            this.gridHistory.Name = "gridHistory";
            this.gridHistory.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.gridHistory.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.gridHistory.Size = new System.Drawing.Size(1129, 590);
            this.gridHistory.TabIndex = 0;
            this.gridHistory.TabStop = true;
            this.gridHistory.ToolTipText = "";
            // 
            // FormOperatorCallHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.ClientSize = new System.Drawing.Size(1143, 675);
            this.FormSizable = false;
            this.Name = "FormOperatorCallHistory";
            this.OptionBoxAlignment = Lib.UI.Generic.DarkMode.Controls.EnumTitleOptionBoxAlignment.Right;
            this.Text = "Operator Call History";
            this.TitleButtonText1 = "Close";
            this.TitleButtonVisible1 = true;
            this.TitleControlBox = false;
            this.TitleMaximumBox = false;
            this.TitleMinimumBox = false;
            this.TitleText = "Operator Call History";
            this.TitleVisible = true;
            this.InputLanguageChanging += new System.Windows.Forms.InputLanguageChangingEventHandler(this.FormOperatorCallHistory_InputLanguageChanging);
            this.MainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CSourceGrid gridHistory;
    }
}
