namespace bim_base
{
    partial class FormCIMHistory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridHistory = new ExtDataGridView();
            this.clmDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridHistory)).BeginInit();
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
            this.gridHistory._AUTO_SAVE = false;
            this.gridHistory._AUTO_SAVE_NAME = "";
            this.gridHistory._COLUMN_NUMPAD = false;
            this.gridHistory.AllowUserToAddRows = false;
            this.gridHistory.AllowUserToDeleteRows = false;
            this.gridHistory.AllowUserToResizeColumns = false;
            this.gridHistory.AllowUserToResizeRows = false;
            this.gridHistory.AutoResizeColumnHeader = false;
            this.gridHistory.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmDateTime,
            this.clmNo,
            this.clmMessage});
            this.gridHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridHistory.isSort = false;
            this.gridHistory.Location = new System.Drawing.Point(0, 0);
            this.gridHistory.MultiSelect = false;
            this.gridHistory.Name = "gridHistory";
            this.gridHistory.ReadOnly = true;
            this.gridHistory.RowHeadersVisible = false;
            this.gridHistory.RowHeadersWidth = 62;
            this.gridHistory.RowTemplate.Height = 23;
            this.gridHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridHistory.Size = new System.Drawing.Size(1129, 590);
            this.gridHistory.TabIndex = 0;
            this.gridHistory.UseHorizontalScrollBar = false;
            // 
            // clmDateTime
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmDateTime.DefaultCellStyle = dataGridViewCellStyle2;
            this.clmDateTime.HeaderText = "날자/시간";
            this.clmDateTime.MinimumWidth = 8;
            this.clmDateTime.Name = "clmDateTime";
            this.clmDateTime.ReadOnly = true;
            this.clmDateTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmDateTime.Width = 250;
            // 
            // clmNo
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmNo.DefaultCellStyle = dataGridViewCellStyle3;
            this.clmNo.HeaderText = "No.";
            this.clmNo.MinimumWidth = 8;
            this.clmNo.Name = "clmNo";
            this.clmNo.ReadOnly = true;
            this.clmNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmMessage
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.clmMessage.DefaultCellStyle = dataGridViewCellStyle4;
            this.clmMessage.HeaderText = "Message";
            this.clmMessage.MinimumWidth = 8;
            this.clmMessage.Name = "clmMessage";
            this.clmMessage.ReadOnly = true;
            this.clmMessage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmMessage.Width = 750;
            // 
            // FormCIMHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.ClientSize = new System.Drawing.Size(1143, 675);
            this.FormSizable = false;
            this.Name = "FormCIMHistory";
            this.OptionBoxAlignment = Lib.UI.Generic.DarkMode.Controls.EnumTitleOptionBoxAlignment.Right;
            this.Text = "Operator Call History";
            this.TitleButtonText1 = "Close";
            this.TitleButtonVisible1 = true;
            this.TitleControlBox = false;
            this.TitleLabelText = "                                History                                ";
            this.TitleLabelVisible = true;
            this.TitleMaximumBox = false;
            this.TitleMinimumBox = false;
            this.TitleText = "Operator Call";
            this.TitleVisible = true;
            this.Load += new System.EventHandler(this.FormCIMHistory_Load);
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ExtDataGridView gridHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmMessage;
    }
}
