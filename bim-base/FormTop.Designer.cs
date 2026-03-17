namespace bim_base
{
    partial class FormTop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTop));
            this.ui_timer = new System.Windows.Forms.Timer(this.components);
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lbVersion = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.BT_BUZZER = new SUserControls.ColorButton();
            this.lbMessage1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lbScreenNo = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lbPPID = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbProjectName = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lbEQNo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lbCurrentTime = new System.Windows.Forms.Label();
            this.lbCurrentDate = new System.Windows.Forms.Label();
            this.lbMessage2 = new System.Windows.Forms.Label();
            this.lbMessage3 = new System.Windows.Forms.Label();
            this.lbScreenName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ui_timer
            // 
            this.ui_timer.Enabled = true;
            this.ui_timer.Interval = 1000;
            this.ui_timer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picLogo.Image = global::bim_base.Properties.Resources.logosamsung;
            this.picLogo.Location = new System.Drawing.Point(0, 0);
            this.picLogo.Margin = new System.Windows.Forms.Padding(0);
            this.picLogo.Name = "picLogo";
            this.tableLayoutPanel1.SetRowSpan(this.picLogo, 2);
            this.picLogo.Size = new System.Drawing.Size(91, 46);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 9;
            this.picLogo.TabStop = false;
            // 
            // lbVersion
            // 
            this.lbVersion.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbVersion.Location = new System.Drawing.Point(744, 46);
            this.lbVersion.Margin = new System.Windows.Forms.Padding(0);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(157, 24);
            this.lbVersion.TabIndex = 7;
            this.lbVersion.Text = "(Ver 1.1.0.0 beta)";
            this.lbVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbStatus
            // 
            this.lbStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.ForeColor = System.Drawing.Color.Red;
            this.lbStatus.Location = new System.Drawing.Point(0, 46);
            this.lbStatus.Margin = new System.Windows.Forms.Padding(0);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(91, 24);
            this.lbStatus.TabIndex = 2;
            this.lbStatus.Text = "OFF-LINE";
            this.lbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbStatus.Click += new System.EventHandler(this.label1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 91F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 166F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 157F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 123F));
            this.tableLayoutPanel1.Controls.Add(this.BT_BUZZER, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbMessage1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.picLogo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbProjectName, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbVersion, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbStatus, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbMessage2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbMessage3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbScreenName, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1024, 70);
            this.tableLayoutPanel1.TabIndex = 1139;
            // 
            // BT_BUZZER
            // 
            this.BT_BUZZER.BackColor = System.Drawing.Color.Transparent;
            this.BT_BUZZER.BorderLineColor = System.Drawing.Color.Black;
            this.BT_BUZZER.BorderThickness = 0F;
            this.BT_BUZZER.Checked = false;
            this.BT_BUZZER.CheckedButtonColor = System.Drawing.Color.Black;
            this.BT_BUZZER.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BT_BUZZER.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BT_BUZZER.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_BUZZER.ForeColor = System.Drawing.Color.Black;
            this.BT_BUZZER.GradientBottom = System.Drawing.Color.LightSkyBlue;
            this.BT_BUZZER.GradientTop = System.Drawing.Color.LightSkyBlue;
            this.BT_BUZZER.Image = ((System.Drawing.Image)(resources.GetObject("BT_BUZZER.Image")));
            this.BT_BUZZER.Location = new System.Drawing.Point(901, 0);
            this.BT_BUZZER.Margin = new System.Windows.Forms.Padding(0);
            this.BT_BUZZER.Name = "BT_BUZZER";
            this.BT_BUZZER.RectCornerRadius = 2;
            this.tableLayoutPanel1.SetRowSpan(this.BT_BUZZER, 3);
            this.BT_BUZZER.Size = new System.Drawing.Size(123, 70);
            this.BT_BUZZER.TabIndex = 1148;
            this.BT_BUZZER.Text = "BUZZER";
            this.BT_BUZZER.UseVisualStyleBackColor = false;
            // 
            // lbMessage1
            // 
            this.lbMessage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbMessage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMessage1.ForeColor = System.Drawing.Color.Black;
            this.lbMessage1.Location = new System.Drawing.Point(91, 0);
            this.lbMessage1.Margin = new System.Windows.Forms.Padding(0);
            this.lbMessage1.Name = "lbMessage1";
            this.lbMessage1.Size = new System.Drawing.Size(106, 23);
            this.lbMessage1.TabIndex = 1141;
            this.lbMessage1.Text = "삼성디스플레이";
            this.lbMessage1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.06061F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.93939F));
            this.tableLayoutPanel5.Controls.Add(this.lbScreenNo, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(578, 46);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(166, 24);
            this.tableLayoutPanel5.TabIndex = 1141;
            // 
            // lbScreenNo
            // 
            this.lbScreenNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(233)))), ((int)(((byte)(244)))));
            this.lbScreenNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbScreenNo.ForeColor = System.Drawing.Color.Black;
            this.lbScreenNo.Location = new System.Drawing.Point(76, 0);
            this.lbScreenNo.Margin = new System.Windows.Forms.Padding(0);
            this.lbScreenNo.Name = "lbScreenNo";
            this.lbScreenNo.Size = new System.Drawing.Size(54, 24);
            this.lbScreenNo.TabIndex = 1140;
            this.lbScreenNo.Text = "1";
            this.lbScreenNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 24);
            this.label10.TabIndex = 1140;
            this.label10.Text = "Screen No:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.48485F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.51515F));
            this.tableLayoutPanel4.Controls.Add(this.lbPPID, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(578, 23);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(166, 23);
            this.tableLayoutPanel4.TabIndex = 1140;
            // 
            // lbPPID
            // 
            this.lbPPID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(233)))), ((int)(((byte)(244)))));
            this.lbPPID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPPID.ForeColor = System.Drawing.Color.Black;
            this.lbPPID.Location = new System.Drawing.Point(47, 0);
            this.lbPPID.Margin = new System.Windows.Forms.Padding(0);
            this.lbPPID.Name = "lbPPID";
            this.lbPPID.Size = new System.Drawing.Size(107, 23);
            this.lbPPID.TabIndex = 1140;
            this.lbPPID.Text = "ABCDEFGHIJK";
            this.lbPPID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbPPID.Click += new System.EventHandler(this.lbPPID_Click);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 23);
            this.label8.TabIndex = 1140;
            this.label8.Text = "PPID:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbProjectName
            // 
            this.lbProjectName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProjectName.ForeColor = System.Drawing.Color.LimeGreen;
            this.lbProjectName.Location = new System.Drawing.Point(744, 0);
            this.lbProjectName.Margin = new System.Windows.Forms.Padding(0);
            this.lbProjectName.Name = "lbProjectName";
            this.lbProjectName.Size = new System.Drawing.Size(157, 23);
            this.lbProjectName.TabIndex = 7;
            this.lbProjectName.Text = "COOLINGMC-S-V1";
            this.lbProjectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.21656F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.78344F));
            this.tableLayoutPanel2.Controls.Add(this.lbEQNo, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(744, 23);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(157, 23);
            this.tableLayoutPanel2.TabIndex = 1139;
            // 
            // lbEQNo
            // 
            this.lbEQNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(233)))), ((int)(((byte)(244)))));
            this.lbEQNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEQNo.ForeColor = System.Drawing.Color.Black;
            this.lbEQNo.Location = new System.Drawing.Point(60, 0);
            this.lbEQNo.Margin = new System.Windows.Forms.Padding(0);
            this.lbEQNo.Name = "lbEQNo";
            this.lbEQNo.Size = new System.Drawing.Size(63, 23);
            this.lbEQNo.TabIndex = 1140;
            this.lbEQNo.Text = "0";
            this.lbEQNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbEQNo.Click += new System.EventHandler(this.lbEQNo_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 23);
            this.label3.TabIndex = 1140;
            this.label3.Text = "EQ No:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.75F));
            this.tableLayoutPanel3.Controls.Add(this.lbCurrentTime, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbCurrentDate, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(578, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(166, 23);
            this.tableLayoutPanel3.TabIndex = 1140;
            // 
            // lbCurrentTime
            // 
            this.lbCurrentTime.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbCurrentTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCurrentTime.ForeColor = System.Drawing.Color.Black;
            this.lbCurrentTime.Location = new System.Drawing.Point(93, 0);
            this.lbCurrentTime.Margin = new System.Windows.Forms.Padding(0);
            this.lbCurrentTime.Name = "lbCurrentTime";
            this.lbCurrentTime.Size = new System.Drawing.Size(73, 23);
            this.lbCurrentTime.TabIndex = 1141;
            this.lbCurrentTime.Text = "09 : 31 : 55";
            this.lbCurrentTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbCurrentDate
            // 
            this.lbCurrentDate.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbCurrentDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCurrentDate.ForeColor = System.Drawing.Color.Black;
            this.lbCurrentDate.Location = new System.Drawing.Point(0, 0);
            this.lbCurrentDate.Margin = new System.Windows.Forms.Padding(0);
            this.lbCurrentDate.Name = "lbCurrentDate";
            this.lbCurrentDate.Size = new System.Drawing.Size(93, 23);
            this.lbCurrentDate.TabIndex = 1140;
            this.lbCurrentDate.Text = "2026 . 02 . 07";
            this.lbCurrentDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbMessage2
            // 
            this.lbMessage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbMessage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMessage2.ForeColor = System.Drawing.Color.Black;
            this.lbMessage2.Location = new System.Drawing.Point(91, 23);
            this.lbMessage2.Margin = new System.Windows.Forms.Padding(0);
            this.lbMessage2.Name = "lbMessage2";
            this.lbMessage2.Size = new System.Drawing.Size(106, 23);
            this.lbMessage2.TabIndex = 1141;
            this.lbMessage2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbMessage3
            // 
            this.lbMessage3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbMessage3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMessage3.ForeColor = System.Drawing.Color.Black;
            this.lbMessage3.Location = new System.Drawing.Point(91, 46);
            this.lbMessage3.Margin = new System.Windows.Forms.Padding(0);
            this.lbMessage3.Name = "lbMessage3";
            this.lbMessage3.Size = new System.Drawing.Size(106, 23);
            this.lbMessage3.TabIndex = 1141;
            this.lbMessage3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbMessage3.Visible = false;
            // 
            // lbScreenName
            // 
            this.lbScreenName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbScreenName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbScreenName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbScreenName.ForeColor = System.Drawing.Color.Black;
            this.lbScreenName.Location = new System.Drawing.Point(197, 0);
            this.lbScreenName.Margin = new System.Windows.Forms.Padding(0);
            this.lbScreenName.Name = "lbScreenName";
            this.tableLayoutPanel1.SetRowSpan(this.lbScreenName, 3);
            this.lbScreenName.Size = new System.Drawing.Size(381, 70);
            this.lbScreenName.TabIndex = 1140;
            this.lbScreenName.Text = "Auto Screen";
            this.lbScreenName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbScreenName.Click += new System.EventHandler(this.infoLabel_Click);
            // 
            // FormTop
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1024, 70);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormTop";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.FormTop_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer ui_timer;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbProjectName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lbEQNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lbCurrentTime;
        private System.Windows.Forms.Label lbCurrentDate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lbPPID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label lbScreenNo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbScreenName;
        private System.Windows.Forms.Label lbMessage1;
        private System.Windows.Forms.Label lbMessage2;
        private System.Windows.Forms.Label lbMessage3;
        public SUserControls.ColorButton BT_BUZZER;
    }
}