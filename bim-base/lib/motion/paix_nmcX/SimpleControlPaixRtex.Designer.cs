partial class SimpleControlRtex
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
    /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
    /// </summary>
    private void InitializeComponent()
    {
            this.nowPosLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.loadPerLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.negName = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.limitNegLabel = new System.Windows.Forms.Label();
            this.alarmLabel = new System.Windows.Forms.Label();
            this.posName = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.limitPosLabel = new System.Windows.Forms.Label();
            this.orgLabel = new System.Windows.Forms.Label();
            this.orgName = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.movePosTextBox = new CTextBox();
            this.jogNegButton = new System.Windows.Forms.Button();
            this.jogPosButton = new System.Windows.Forms.Button();
            this.moveButton = new System.Windows.Forms.Button();
            this.speedTextBox = new CTextBox();
            this.speedLowButton = new System.Windows.Forms.Button();
            this.speedMidButton = new System.Windows.Forms.Button();
            this.speedHighButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nowPosLabel
            // 
            this.nowPosLabel.AutoSize = true;
            this.nowPosLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nowPosLabel.Location = new System.Drawing.Point(70, 0);
            this.nowPosLabel.Name = "nowPosLabel";
            this.nowPosLabel.Size = new System.Drawing.Size(95, 30);
            this.nowPosLabel.TabIndex = 4;
            this.nowPosLabel.Text = "-";
            this.nowPosLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameLabel.Location = new System.Drawing.Point(3, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(61, 30);
            this.nameLabel.TabIndex = 4;
            this.nameLabel.Text = "-";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // loadPerLabel
            // 
            this.loadPerLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.loadPerLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadPerLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadPerLabel.Location = new System.Drawing.Point(136, 32);
            this.loadPerLabel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loadPerLabel.Name = "loadPerLabel";
            this.loadPerLabel.Size = new System.Drawing.Size(28, 15);
            this.loadPerLabel.TabIndex = 4;
            this.loadPerLabel.Text = "10%";
            this.loadPerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel2.Controls.Add(this.loadPerLabel, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.negName, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label10, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.limitNegLabel, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.alarmLabel, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.posName, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label9, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.limitPosLabel, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.orgLabel, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.orgName, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 30);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(168, 50);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // negName
            // 
            this.negName.AutoSize = true;
            this.negName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.negName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.negName.Location = new System.Drawing.Point(4, 1);
            this.negName.Name = "negName";
            this.negName.Size = new System.Drawing.Size(26, 28);
            this.negName.TabIndex = 4;
            this.negName.Text = "RLS";
            this.negName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.Location = new System.Drawing.Point(136, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 28);
            this.label10.TabIndex = 4;
            this.label10.Text = "부하";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // limitNegLabel
            // 
            this.limitNegLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.limitNegLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.limitNegLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.limitNegLabel.Location = new System.Drawing.Point(4, 32);
            this.limitNegLabel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.limitNegLabel.Name = "limitNegLabel";
            this.limitNegLabel.Size = new System.Drawing.Size(26, 15);
            this.limitNegLabel.TabIndex = 4;
            this.limitNegLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // alarmLabel
            // 
            this.alarmLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.alarmLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alarmLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.alarmLabel.Location = new System.Drawing.Point(103, 32);
            this.alarmLabel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.alarmLabel.Name = "alarmLabel";
            this.alarmLabel.Size = new System.Drawing.Size(26, 15);
            this.alarmLabel.TabIndex = 4;
            this.alarmLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // posName
            // 
            this.posName.AutoSize = true;
            this.posName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.posName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.posName.Location = new System.Drawing.Point(37, 1);
            this.posName.Name = "posName";
            this.posName.Size = new System.Drawing.Size(26, 28);
            this.posName.TabIndex = 4;
            this.posName.Text = "FLS";
            this.posName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.Location = new System.Drawing.Point(103, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 28);
            this.label9.TabIndex = 4;
            this.label9.Text = "알람";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // limitPosLabel
            // 
            this.limitPosLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.limitPosLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.limitPosLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.limitPosLabel.Location = new System.Drawing.Point(37, 32);
            this.limitPosLabel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.limitPosLabel.Name = "limitPosLabel";
            this.limitPosLabel.Size = new System.Drawing.Size(26, 15);
            this.limitPosLabel.TabIndex = 4;
            this.limitPosLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // orgLabel
            // 
            this.orgLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.orgLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orgLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.orgLabel.Location = new System.Drawing.Point(70, 32);
            this.orgLabel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.orgLabel.Name = "orgLabel";
            this.orgLabel.Size = new System.Drawing.Size(26, 15);
            this.orgLabel.TabIndex = 4;
            this.orgLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // orgName
            // 
            this.orgName.AutoSize = true;
            this.orgName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orgName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.orgName.Location = new System.Drawing.Point(70, 1);
            this.orgName.Name = "orgName";
            this.orgName.Size = new System.Drawing.Size(26, 28);
            this.orgName.TabIndex = 4;
            this.orgName.Text = "DOG";
            this.orgName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.Controls.Add(this.movePosTextBox, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.nowPosLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.jogNegButton, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.jogPosButton, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.moveButton, 7, 1);
            this.tableLayoutPanel1.Controls.Add(this.speedTextBox, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.speedLowButton, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.speedMidButton, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.speedHighButton, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.stopButton, 8, 1);
            this.tableLayoutPanel1.Controls.Add(this.nameLabel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.9863F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.28767F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.00297F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(580, 80);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // movePosTextBox
            // 
            this.movePosTextBox._MinusEnable = true;
            this.movePosTextBox._NumberPad = true;
            this.movePosTextBox._NumberPadText = null;
            this.movePosTextBox._NumberStyle = CNumberKeyPad.Style.Style_Unknwon;
            this.tableLayoutPanel1.SetColumnSpan(this.movePosTextBox, 2);
            this.movePosTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.movePosTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.movePosTextBox.Location = new System.Drawing.Point(435, 0);
            this.movePosTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.movePosTextBox.Name = "movePosTextBox";
            this.movePosTextBox.ReadOnly = true;
            this.movePosTextBox.Size = new System.Drawing.Size(149, 26);
            this.movePosTextBox.TabIndex = 2;
            this.movePosTextBox.Text = "0";
            this.movePosTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // jogNegButton
            // 
            this.jogNegButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jogNegButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.jogNegButton.Location = new System.Drawing.Point(297, 0);
            this.jogNegButton.Margin = new System.Windows.Forms.Padding(0);
            this.jogNegButton.Name = "jogNegButton";
            this.tableLayoutPanel1.SetRowSpan(this.jogNegButton, 3);
            this.jogNegButton.Size = new System.Drawing.Size(69, 80);
            this.jogNegButton.TabIndex = 1;
            this.jogNegButton.Text = "조그 -";
            this.jogNegButton.UseVisualStyleBackColor = true;
            this.jogNegButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.jogNegButton_MouseDown);
            this.jogNegButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.jogNegButton_MouseUp);
            // 
            // jogPosButton
            // 
            this.jogPosButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jogPosButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.jogPosButton.Location = new System.Drawing.Point(366, 0);
            this.jogPosButton.Margin = new System.Windows.Forms.Padding(0);
            this.jogPosButton.Name = "jogPosButton";
            this.tableLayoutPanel1.SetRowSpan(this.jogPosButton, 3);
            this.jogPosButton.Size = new System.Drawing.Size(69, 80);
            this.jogPosButton.TabIndex = 1;
            this.jogPosButton.Text = "조그 +";
            this.jogPosButton.UseVisualStyleBackColor = true;
            this.jogPosButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.jogPosButton_MouseDown);
            this.jogPosButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.jogPosButton_MouseUp);
            // 
            // moveButton
            // 
            this.moveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.moveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.moveButton.Location = new System.Drawing.Point(435, 30);
            this.moveButton.Margin = new System.Windows.Forms.Padding(0);
            this.moveButton.Name = "moveButton";
            this.tableLayoutPanel1.SetRowSpan(this.moveButton, 2);
            this.moveButton.Size = new System.Drawing.Size(69, 50);
            this.moveButton.TabIndex = 1;
            this.moveButton.Text = "이동";
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.Click += new System.EventHandler(this.moveButton_Click);
            // 
            // speedTextBox
            // 
            this.speedTextBox._MinusEnable = true;
            this.speedTextBox._NumberPad = true;
            this.speedTextBox._NumberPadText = null;
            this.speedTextBox._NumberStyle = CNumberKeyPad.Style.Style_Unknwon;
            this.tableLayoutPanel1.SetColumnSpan(this.speedTextBox, 3);
            this.speedTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.speedTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.speedTextBox.Location = new System.Drawing.Point(168, 0);
            this.speedTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.speedTextBox.Name = "speedTextBox";
            this.speedTextBox.ReadOnly = true;
            this.speedTextBox.Size = new System.Drawing.Size(129, 26);
            this.speedTextBox.TabIndex = 2;
            this.speedTextBox.Text = "99999";
            this.speedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.speedTextBox.TextChanged += new System.EventHandler(this.speedTextBox_TextChanged);
            // 
            // speedLowButton
            // 
            this.speedLowButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.speedLowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.speedLowButton.Location = new System.Drawing.Point(168, 30);
            this.speedLowButton.Margin = new System.Windows.Forms.Padding(0);
            this.speedLowButton.Name = "speedLowButton";
            this.tableLayoutPanel1.SetRowSpan(this.speedLowButton, 2);
            this.speedLowButton.Size = new System.Drawing.Size(43, 50);
            this.speedLowButton.TabIndex = 1;
            this.speedLowButton.Text = "저";
            this.speedLowButton.UseVisualStyleBackColor = true;
            this.speedLowButton.Click += new System.EventHandler(this.speedLowButton_Click);
            // 
            // speedMidButton
            // 
            this.speedMidButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.speedMidButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.speedMidButton.Location = new System.Drawing.Point(211, 30);
            this.speedMidButton.Margin = new System.Windows.Forms.Padding(0);
            this.speedMidButton.Name = "speedMidButton";
            this.tableLayoutPanel1.SetRowSpan(this.speedMidButton, 2);
            this.speedMidButton.Size = new System.Drawing.Size(43, 50);
            this.speedMidButton.TabIndex = 1;
            this.speedMidButton.Text = "중";
            this.speedMidButton.UseVisualStyleBackColor = true;
            this.speedMidButton.Click += new System.EventHandler(this.speedMidButton_Click);
            // 
            // speedHighButton
            // 
            this.speedHighButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.speedHighButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.speedHighButton.Location = new System.Drawing.Point(254, 30);
            this.speedHighButton.Margin = new System.Windows.Forms.Padding(0);
            this.speedHighButton.Name = "speedHighButton";
            this.tableLayoutPanel1.SetRowSpan(this.speedHighButton, 2);
            this.speedHighButton.Size = new System.Drawing.Size(43, 50);
            this.speedHighButton.TabIndex = 1;
            this.speedHighButton.Text = "고";
            this.speedHighButton.UseVisualStyleBackColor = true;
            this.speedHighButton.Click += new System.EventHandler(this.speedHighButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stopButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopButton.Location = new System.Drawing.Point(504, 30);
            this.stopButton.Margin = new System.Windows.Forms.Padding(0);
            this.stopButton.Name = "stopButton";
            this.tableLayoutPanel1.SetRowSpan(this.stopButton, 2);
            this.stopButton.Size = new System.Drawing.Size(80, 50);
            this.stopButton.TabIndex = 1;
            this.stopButton.Text = "정지";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // SimpleControlRtex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SimpleControlRtex";
            this.Size = new System.Drawing.Size(580, 80);
            this.Load += new System.EventHandler(this.SimpleControl_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label nowPosLabel;
    private CTextBox speedTextBox;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private CTextBox movePosTextBox;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.Label loadPerLabel;
    private System.Windows.Forms.Label negName;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label limitNegLabel;
    private System.Windows.Forms.Label alarmLabel;
    private System.Windows.Forms.Label posName;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label limitPosLabel;
    private System.Windows.Forms.Label orgLabel;
    private System.Windows.Forms.Label orgName;
    private System.Windows.Forms.Button jogNegButton;
    private System.Windows.Forms.Button jogPosButton;
    private System.Windows.Forms.Button moveButton;
    private System.Windows.Forms.Button speedLowButton;
    private System.Windows.Forms.Button speedMidButton;
    private System.Windows.Forms.Button speedHighButton;
    private System.Windows.Forms.Button stopButton;
    private System.Windows.Forms.Label nameLabel;
}
