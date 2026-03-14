namespace bim_base
{
    partial class JogControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JogControl));
            this.ui_timer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.jogMidButton = new System.Windows.Forms.Button();
            this.jogHighButton = new System.Windows.Forms.Button();
            this.jogLowButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.jogYUpButton = new System.Windows.Forms.Button();
            this.jogXLeftButton = new System.Windows.Forms.Button();
            this.jogXRightButton = new System.Windows.Forms.Button();
            this.jogYDownButton = new System.Windows.Forms.Button();
            this.jogYUpLabel = new System.Windows.Forms.Label();
            this.jogXRightLabel = new System.Windows.Forms.Label();
            this.jogYDownLabel = new System.Windows.Forms.Label();
            this.jogZUpButton = new System.Windows.Forms.Button();
            this.jogZDownButton = new System.Windows.Forms.Button();
            this.jogZUpLabel = new System.Windows.Forms.Label();
            this.jogZDownLabel = new System.Windows.Forms.Label();
            this.jogX2LeftLabel = new System.Windows.Forms.Label();
            this.jogX2LeftButton = new System.Windows.Forms.Button();
            this.jogX2RightLabel = new System.Windows.Forms.Label();
            this.jogX2RightButton = new System.Windows.Forms.Button();
            this.jogXLeftLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // ui_timer
            // 
            this.ui_timer.Enabled = true;
            this.ui_timer.Tick += new System.EventHandler(this.ui_timer_Tick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel7, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(253, 386);
            this.tableLayoutPanel2.TabIndex = 16;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.jogMidButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.jogHighButton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.jogLowButton, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(253, 45);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // jogMidButton
            // 
            this.jogMidButton.BackColor = System.Drawing.Color.White;
            this.jogMidButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jogMidButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.jogMidButton.Location = new System.Drawing.Point(88, 5);
            this.jogMidButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.jogMidButton.Name = "jogMidButton";
            this.jogMidButton.Size = new System.Drawing.Size(76, 35);
            this.jogMidButton.TabIndex = 3;
            this.jogMidButton.Tag = "1";
            this.jogMidButton.Text = "Middle";
            this.jogMidButton.UseVisualStyleBackColor = false;
            this.jogMidButton.Click += new System.EventHandler(this.jogButton_Click);
            // 
            // jogHighButton
            // 
            this.jogHighButton.BackColor = System.Drawing.Color.White;
            this.jogHighButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jogHighButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.jogHighButton.Location = new System.Drawing.Point(172, 5);
            this.jogHighButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.jogHighButton.Name = "jogHighButton";
            this.jogHighButton.Size = new System.Drawing.Size(77, 35);
            this.jogHighButton.TabIndex = 3;
            this.jogHighButton.Tag = "2";
            this.jogHighButton.Text = "High";
            this.jogHighButton.UseVisualStyleBackColor = false;
            this.jogHighButton.Click += new System.EventHandler(this.jogButton_Click);
            // 
            // jogLowButton
            // 
            this.jogLowButton.BackColor = System.Drawing.Color.White;
            this.jogLowButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jogLowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.jogLowButton.Location = new System.Drawing.Point(4, 5);
            this.jogLowButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.jogLowButton.Name = "jogLowButton";
            this.jogLowButton.Size = new System.Drawing.Size(76, 35);
            this.jogLowButton.TabIndex = 4;
            this.jogLowButton.Tag = "0";
            this.jogLowButton.Text = "Low";
            this.jogLowButton.UseVisualStyleBackColor = false;
            this.jogLowButton.Click += new System.EventHandler(this.jogButton_Click);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 4;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.Controls.Add(this.jogYUpButton, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.jogXLeftButton, 0, 3);
            this.tableLayoutPanel7.Controls.Add(this.jogXRightButton, 2, 3);
            this.tableLayoutPanel7.Controls.Add(this.jogYDownButton, 1, 5);
            this.tableLayoutPanel7.Controls.Add(this.jogYUpLabel, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.jogXRightLabel, 2, 2);
            this.tableLayoutPanel7.Controls.Add(this.jogYDownLabel, 1, 7);
            this.tableLayoutPanel7.Controls.Add(this.jogZUpButton, 3, 1);
            this.tableLayoutPanel7.Controls.Add(this.jogZDownButton, 3, 5);
            this.tableLayoutPanel7.Controls.Add(this.jogZUpLabel, 3, 0);
            this.tableLayoutPanel7.Controls.Add(this.jogZDownLabel, 3, 7);
            this.tableLayoutPanel7.Controls.Add(this.jogX2LeftLabel, 0, 8);
            this.tableLayoutPanel7.Controls.Add(this.jogX2LeftButton, 0, 9);
            this.tableLayoutPanel7.Controls.Add(this.jogX2RightLabel, 2, 8);
            this.tableLayoutPanel7.Controls.Add(this.jogX2RightButton, 2, 9);
            this.tableLayoutPanel7.Controls.Add(this.jogXLeftLabel, 0, 2);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(4, 50);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 11;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(245, 331);
            this.tableLayoutPanel7.TabIndex = 13;
            // 
            // jogYUpButton
            // 
            this.jogYUpButton.BackgroundImage = global::bim_base.Properties.Resources.up_disable;
            this.jogYUpButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.jogYUpButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jogYUpButton.Font = new System.Drawing.Font("맑은 고딕", 18F);
            this.jogYUpButton.Location = new System.Drawing.Point(65, 35);
            this.jogYUpButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.jogYUpButton.Name = "jogYUpButton";
            this.tableLayoutPanel7.SetRowSpan(this.jogYUpButton, 2);
            this.jogYUpButton.Size = new System.Drawing.Size(53, 50);
            this.jogYUpButton.TabIndex = 0;
            this.jogYUpButton.Tag = "1";
            this.jogYUpButton.UseVisualStyleBackColor = true;
            this.jogYUpButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.jogYButton_MouseDown);
            this.jogYUpButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.jogButton_MouseUp);
            // 
            // jogXLeftButton
            // 
            this.jogXLeftButton.BackgroundImage = global::bim_base.Properties.Resources.left_disable;
            this.jogXLeftButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.jogXLeftButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jogXLeftButton.Font = new System.Drawing.Font("맑은 고딕", 18F);
            this.jogXLeftButton.Location = new System.Drawing.Point(4, 95);
            this.jogXLeftButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.jogXLeftButton.Name = "jogXLeftButton";
            this.tableLayoutPanel7.SetRowSpan(this.jogXLeftButton, 2);
            this.jogXLeftButton.Size = new System.Drawing.Size(53, 50);
            this.jogXLeftButton.TabIndex = 2;
            this.jogXLeftButton.Tag = "-1";
            this.jogXLeftButton.UseVisualStyleBackColor = true;
            this.jogXLeftButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.jogXButton_MouseDown);
            this.jogXLeftButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.jogButton_MouseUp);
            // 
            // jogXRightButton
            // 
            this.jogXRightButton.BackgroundImage = global::bim_base.Properties.Resources.right_disable;
            this.jogXRightButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.jogXRightButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jogXRightButton.Font = new System.Drawing.Font("맑은 고딕", 18F);
            this.jogXRightButton.Location = new System.Drawing.Point(126, 95);
            this.jogXRightButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.jogXRightButton.Name = "jogXRightButton";
            this.tableLayoutPanel7.SetRowSpan(this.jogXRightButton, 2);
            this.jogXRightButton.Size = new System.Drawing.Size(53, 50);
            this.jogXRightButton.TabIndex = 3;
            this.jogXRightButton.Tag = "1";
            this.jogXRightButton.UseVisualStyleBackColor = true;
            this.jogXRightButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.jogXButton_MouseDown);
            this.jogXRightButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.jogButton_MouseUp);
            // 
            // jogYDownButton
            // 
            this.jogYDownButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("jogYDownButton.BackgroundImage")));
            this.jogYDownButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.jogYDownButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jogYDownButton.Font = new System.Drawing.Font("맑은 고딕", 18F);
            this.jogYDownButton.Location = new System.Drawing.Point(65, 155);
            this.jogYDownButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.jogYDownButton.Name = "jogYDownButton";
            this.tableLayoutPanel7.SetRowSpan(this.jogYDownButton, 2);
            this.jogYDownButton.Size = new System.Drawing.Size(53, 50);
            this.jogYDownButton.TabIndex = 4;
            this.jogYDownButton.Tag = "-1";
            this.jogYDownButton.UseVisualStyleBackColor = true;
            this.jogYDownButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.jogYButton_MouseDown);
            this.jogYDownButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.jogButton_MouseUp);
            // 
            // jogYUpLabel
            // 
            this.jogYUpLabel.AutoSize = true;
            this.jogYUpLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jogYUpLabel.Font = new System.Drawing.Font("SamsungOne 800C", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jogYUpLabel.Location = new System.Drawing.Point(65, 0);
            this.jogYUpLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.jogYUpLabel.Name = "jogYUpLabel";
            this.jogYUpLabel.Size = new System.Drawing.Size(53, 30);
            this.jogYUpLabel.TabIndex = 6;
            this.jogYUpLabel.Text = "Y FW";
            this.jogYUpLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // jogXRightLabel
            // 
            this.jogXRightLabel.AutoSize = true;
            this.jogXRightLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jogXRightLabel.Font = new System.Drawing.Font("SamsungOne 800C", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jogXRightLabel.Location = new System.Drawing.Point(126, 60);
            this.jogXRightLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.jogXRightLabel.Name = "jogXRightLabel";
            this.jogXRightLabel.Size = new System.Drawing.Size(53, 30);
            this.jogXRightLabel.TabIndex = 7;
            this.jogXRightLabel.Text = "X R";
            this.jogXRightLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // jogYDownLabel
            // 
            this.jogYDownLabel.AutoSize = true;
            this.jogYDownLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jogYDownLabel.Font = new System.Drawing.Font("SamsungOne 800C", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jogYDownLabel.Location = new System.Drawing.Point(65, 210);
            this.jogYDownLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.jogYDownLabel.Name = "jogYDownLabel";
            this.jogYDownLabel.Size = new System.Drawing.Size(53, 30);
            this.jogYDownLabel.TabIndex = 8;
            this.jogYDownLabel.Text = "Y BW";
            this.jogYDownLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // jogZUpButton
            // 
            this.jogZUpButton.BackgroundImage = global::bim_base.Properties.Resources.up_disable;
            this.jogZUpButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.jogZUpButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jogZUpButton.Font = new System.Drawing.Font("맑은 고딕", 18F);
            this.jogZUpButton.Location = new System.Drawing.Point(187, 35);
            this.jogZUpButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.jogZUpButton.Name = "jogZUpButton";
            this.tableLayoutPanel7.SetRowSpan(this.jogZUpButton, 2);
            this.jogZUpButton.Size = new System.Drawing.Size(54, 50);
            this.jogZUpButton.TabIndex = 0;
            this.jogZUpButton.Tag = "1";
            this.jogZUpButton.UseVisualStyleBackColor = true;
            this.jogZUpButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.jogZButton_MouseDown);
            this.jogZUpButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.jogButton_MouseUp);
            // 
            // jogZDownButton
            // 
            this.jogZDownButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("jogZDownButton.BackgroundImage")));
            this.jogZDownButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.jogZDownButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jogZDownButton.Font = new System.Drawing.Font("맑은 고딕", 18F);
            this.jogZDownButton.Location = new System.Drawing.Point(187, 155);
            this.jogZDownButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.jogZDownButton.Name = "jogZDownButton";
            this.tableLayoutPanel7.SetRowSpan(this.jogZDownButton, 2);
            this.jogZDownButton.Size = new System.Drawing.Size(54, 50);
            this.jogZDownButton.TabIndex = 4;
            this.jogZDownButton.Tag = "-1";
            this.jogZDownButton.UseVisualStyleBackColor = true;
            this.jogZDownButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.jogZButton_MouseDown);
            this.jogZDownButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.jogButton_MouseUp);
            // 
            // jogZUpLabel
            // 
            this.jogZUpLabel.AutoSize = true;
            this.jogZUpLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jogZUpLabel.Font = new System.Drawing.Font("SamsungOne 800C", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jogZUpLabel.Location = new System.Drawing.Point(187, 0);
            this.jogZUpLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.jogZUpLabel.Name = "jogZUpLabel";
            this.jogZUpLabel.Size = new System.Drawing.Size(54, 30);
            this.jogZUpLabel.TabIndex = 9;
            this.jogZUpLabel.Text = "Z UP";
            this.jogZUpLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // jogZDownLabel
            // 
            this.jogZDownLabel.AutoSize = true;
            this.jogZDownLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jogZDownLabel.Font = new System.Drawing.Font("SamsungOne 800C", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jogZDownLabel.Location = new System.Drawing.Point(187, 210);
            this.jogZDownLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.jogZDownLabel.Name = "jogZDownLabel";
            this.jogZDownLabel.Size = new System.Drawing.Size(54, 30);
            this.jogZDownLabel.TabIndex = 10;
            this.jogZDownLabel.Text = "Z DN";
            this.jogZDownLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // jogX2LeftLabel
            // 
            this.jogX2LeftLabel.AutoSize = true;
            this.jogX2LeftLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jogX2LeftLabel.Font = new System.Drawing.Font("SamsungOne 800C", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jogX2LeftLabel.Location = new System.Drawing.Point(4, 240);
            this.jogX2LeftLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.jogX2LeftLabel.Name = "jogX2LeftLabel";
            this.jogX2LeftLabel.Size = new System.Drawing.Size(53, 30);
            this.jogX2LeftLabel.TabIndex = 11;
            this.jogX2LeftLabel.Text = "X L";
            this.jogX2LeftLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // jogX2LeftButton
            // 
            this.jogX2LeftButton.BackgroundImage = global::bim_base.Properties.Resources.left_disable;
            this.jogX2LeftButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.jogX2LeftButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jogX2LeftButton.Font = new System.Drawing.Font("맑은 고딕", 18F);
            this.jogX2LeftButton.Location = new System.Drawing.Point(4, 275);
            this.jogX2LeftButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.jogX2LeftButton.Name = "jogX2LeftButton";
            this.tableLayoutPanel7.SetRowSpan(this.jogX2LeftButton, 2);
            this.jogX2LeftButton.Size = new System.Drawing.Size(53, 51);
            this.jogX2LeftButton.TabIndex = 12;
            this.jogX2LeftButton.Tag = "-1";
            this.jogX2LeftButton.UseVisualStyleBackColor = true;
            this.jogX2LeftButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.jogX2Button_MouseDown);
            this.jogX2LeftButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.jogButton_MouseUp);
            // 
            // jogX2RightLabel
            // 
            this.jogX2RightLabel.AutoSize = true;
            this.jogX2RightLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jogX2RightLabel.Font = new System.Drawing.Font("SamsungOne 800C", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jogX2RightLabel.Location = new System.Drawing.Point(126, 240);
            this.jogX2RightLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.jogX2RightLabel.Name = "jogX2RightLabel";
            this.jogX2RightLabel.Size = new System.Drawing.Size(53, 30);
            this.jogX2RightLabel.TabIndex = 11;
            this.jogX2RightLabel.Text = "X R";
            this.jogX2RightLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // jogX2RightButton
            // 
            this.jogX2RightButton.BackgroundImage = global::bim_base.Properties.Resources.right_disable;
            this.jogX2RightButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.jogX2RightButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jogX2RightButton.Font = new System.Drawing.Font("맑은 고딕", 18F);
            this.jogX2RightButton.Location = new System.Drawing.Point(126, 275);
            this.jogX2RightButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.jogX2RightButton.Name = "jogX2RightButton";
            this.tableLayoutPanel7.SetRowSpan(this.jogX2RightButton, 2);
            this.jogX2RightButton.Size = new System.Drawing.Size(53, 51);
            this.jogX2RightButton.TabIndex = 13;
            this.jogX2RightButton.Tag = "1";
            this.jogX2RightButton.UseVisualStyleBackColor = true;
            this.jogX2RightButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.jogX2Button_MouseDown);
            this.jogX2RightButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.jogButton_MouseUp);
            // 
            // jogXLeftLabel
            // 
            this.jogXLeftLabel.AutoSize = true;
            this.jogXLeftLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jogXLeftLabel.Font = new System.Drawing.Font("SamsungOne 800C", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jogXLeftLabel.Location = new System.Drawing.Point(4, 60);
            this.jogXLeftLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.jogXLeftLabel.Name = "jogXLeftLabel";
            this.jogXLeftLabel.Size = new System.Drawing.Size(53, 30);
            this.jogXLeftLabel.TabIndex = 11;
            this.jogXLeftLabel.Text = "X L";
            this.jogXLeftLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // JogControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "JogControl";
            this.Size = new System.Drawing.Size(253, 386);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer ui_timer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Button jogYUpButton;
        private System.Windows.Forms.Button jogXLeftButton;
        private System.Windows.Forms.Button jogXRightButton;
        private System.Windows.Forms.Button jogYDownButton;
        private System.Windows.Forms.Label jogYUpLabel;
        private System.Windows.Forms.Label jogXRightLabel;
        private System.Windows.Forms.Label jogYDownLabel;
        private System.Windows.Forms.Button jogZUpButton;
        private System.Windows.Forms.Button jogZDownButton;
        private System.Windows.Forms.Label jogZUpLabel;
        private System.Windows.Forms.Label jogZDownLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button jogMidButton;
        private System.Windows.Forms.Button jogHighButton;
        private System.Windows.Forms.Button jogLowButton;
        private System.Windows.Forms.Label jogX2LeftLabel;
        private System.Windows.Forms.Button jogX2LeftButton;
        private System.Windows.Forms.Label jogX2RightLabel;
        private System.Windows.Forms.Button jogX2RightButton;
        private System.Windows.Forms.Label jogXLeftLabel;
    }
}
