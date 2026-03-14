partial class AxisControlPaix
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.minusButton = new System.Windows.Forms.Button();
            this.plusButton = new System.Windows.Forms.Button();
            this.jogSpeed1Button = new System.Windows.Forms.Button();
            this.jogSpeed2Button = new System.Windows.Forms.Button();
            this.jogSpeed3Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nowPositionLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.absMovePosTextBox = new CTextBox();
            this.absMoveButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.readyLabel = new System.Windows.Forms.Label();
            this.busyLabel = new System.Windows.Forms.Label();
            this.inposLabel = new System.Windows.Forms.Label();
            this.minusLimitLabel = new System.Windows.Forms.Label();
            this.plusLimitLabel = new System.Windows.Forms.Label();
            this.orgLabel = new System.Windows.Forms.Label();
            this.alarmLabel = new System.Windows.Forms.Label();
            this.resetAlarmButton = new System.Windows.Forms.Button();
            this.orgButton = new System.Windows.Forms.Button();
            this.servoOnButton = new System.Windows.Forms.Button();
            this.emgStopButton = new System.Windows.Forms.Button();
            this.servoOffButton = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.loadRatioLabel = new System.Windows.Forms.Label();
            this.speedTextBox = new CTextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label12 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // minusButton
            // 
            this.minusButton.Location = new System.Drawing.Point(617, 64);
            this.minusButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.minusButton.Name = "minusButton";
            this.minusButton.Size = new System.Drawing.Size(163, 59);
            this.minusButton.TabIndex = 0;
            this.minusButton.Text = "JOG MINUS (-)";
            this.minusButton.UseVisualStyleBackColor = true;
            this.minusButton.Click += new System.EventHandler(this.minusButton_Click);
            this.minusButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.minusButton_MouseDown);
            this.minusButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.minusButton_MouseUp);
            // 
            // plusButton
            // 
            this.plusButton.Location = new System.Drawing.Point(786, 64);
            this.plusButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.plusButton.Name = "plusButton";
            this.plusButton.Size = new System.Drawing.Size(163, 59);
            this.plusButton.TabIndex = 0;
            this.plusButton.Text = "JOG PLUS (+)";
            this.plusButton.UseVisualStyleBackColor = true;
            this.plusButton.Click += new System.EventHandler(this.plusButton_Click);
            this.plusButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.plusButton_MouseDown);
            this.plusButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.plusButton_MouseUp);
            // 
            // jogSpeed1Button
            // 
            this.jogSpeed1Button.Location = new System.Drawing.Point(712, 1);
            this.jogSpeed1Button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.jogSpeed1Button.Name = "jogSpeed1Button";
            this.jogSpeed1Button.Size = new System.Drawing.Size(75, 60);
            this.jogSpeed1Button.TabIndex = 1;
            this.jogSpeed1Button.Text = "저속 ( 1 )";
            this.jogSpeed1Button.UseVisualStyleBackColor = true;
            this.jogSpeed1Button.Click += new System.EventHandler(this.jogSpeed1Button_Click);
            // 
            // jogSpeed2Button
            // 
            this.jogSpeed2Button.Location = new System.Drawing.Point(793, 1);
            this.jogSpeed2Button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.jogSpeed2Button.Name = "jogSpeed2Button";
            this.jogSpeed2Button.Size = new System.Drawing.Size(75, 60);
            this.jogSpeed2Button.TabIndex = 1;
            this.jogSpeed2Button.Text = "중속 ( 5 )";
            this.jogSpeed2Button.UseVisualStyleBackColor = true;
            this.jogSpeed2Button.Click += new System.EventHandler(this.jogSpeed2Button_Click);
            // 
            // jogSpeed3Button
            // 
            this.jogSpeed3Button.Location = new System.Drawing.Point(874, 2);
            this.jogSpeed3Button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.jogSpeed3Button.Name = "jogSpeed3Button";
            this.jogSpeed3Button.Size = new System.Drawing.Size(75, 60);
            this.jogSpeed3Button.TabIndex = 1;
            this.jogSpeed3Button.Text = "고속 ( 10 )";
            this.jogSpeed3Button.UseVisualStyleBackColor = true;
            this.jogSpeed3Button.Click += new System.EventHandler(this.jogSpeed3Button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(331, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Move Position [mm]";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(141, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Current Position [mm]";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nowPositionLabel
            // 
            this.nowPositionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nowPositionLabel.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.nowPositionLabel.Location = new System.Drawing.Point(141, 19);
            this.nowPositionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.nowPositionLabel.Name = "nowPositionLabel";
            this.nowPositionLabel.Size = new System.Drawing.Size(181, 27);
            this.nowPositionLabel.TabIndex = 4;
            this.nowPositionLabel.Text = "00000";
            this.nowPositionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.absMovePosTextBox, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.nowPositionLabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.absMoveButton, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.nameLabel, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 2);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.42857F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.11765F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.70588F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(518, 85);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // absMovePosTextBox
            // 
            this.absMovePosTextBox._MinusEnable = true;
            this.absMovePosTextBox._NumberPad = true;
            this.absMovePosTextBox._NumberPadText = null;
            this.absMovePosTextBox._NumberStyle = CNumberKeyPad.Style.Style_Unknwon;
            this.absMovePosTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.absMovePosTextBox.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.absMovePosTextBox.Location = new System.Drawing.Point(331, 22);
            this.absMovePosTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.absMovePosTextBox.Name = "absMovePosTextBox";
            this.absMovePosTextBox.ReadOnly = true;
            this.absMovePosTextBox.Size = new System.Drawing.Size(182, 22);
            this.absMovePosTextBox.TabIndex = 2;
            this.absMovePosTextBox.Text = "0";
            this.absMovePosTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // absMoveButton
            // 
            this.absMoveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.absMoveButton.Location = new System.Drawing.Point(331, 50);
            this.absMoveButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.absMoveButton.Name = "absMoveButton";
            this.absMoveButton.Size = new System.Drawing.Size(182, 31);
            this.absMoveButton.TabIndex = 0;
            this.absMoveButton.Text = "ABS MOVE";
            this.absMoveButton.UseVisualStyleBackColor = true;
            this.absMoveButton.Click += new System.EventHandler(this.absMoveButton_Click);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Location = new System.Drawing.Point(141, 50);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(181, 31);
            this.button1.TabIndex = 0;
            this.button1.Text = "HOME";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.absMoveButton_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(5, 1);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(127, 17);
            this.label10.TabIndex = 4;
            this.label10.Text = "Name";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameLabel.Location = new System.Drawing.Point(1, 19);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(0);
            this.nameLabel.Name = "nameLabel";
            this.tableLayoutPanel1.SetRowSpan(this.nameLabel, 2);
            this.nameLabel.Size = new System.Drawing.Size(135, 65);
            this.nameLabel.TabIndex = 4;
            this.nameLabel.Text = "--";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.45238F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.54762F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.readyLabel, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.busyLabel, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.inposLabel, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.minusLimitLabel, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.plusLimitLabel, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.orgLabel, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.alarmLabel, 1, 6);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(953, 3);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 7;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(158, 121);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(5, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "READY";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(5, 18);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "BUSY";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(5, 35);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "INPOS";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(5, 52);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "- LIMIT";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(5, 69);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "+ LIMIT";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(5, 86);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "ORG";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(5, 103);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 17);
            this.label9.TabIndex = 0;
            this.label9.Text = "ALARM";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // readyLabel
            // 
            this.readyLabel.AutoSize = true;
            this.readyLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.readyLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.readyLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.readyLabel.Location = new System.Drawing.Point(112, 4);
            this.readyLabel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.readyLabel.Name = "readyLabel";
            this.readyLabel.Size = new System.Drawing.Size(41, 10);
            this.readyLabel.TabIndex = 1;
            // 
            // busyLabel
            // 
            this.busyLabel.AutoSize = true;
            this.busyLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.busyLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.busyLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.busyLabel.Location = new System.Drawing.Point(112, 21);
            this.busyLabel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.busyLabel.Name = "busyLabel";
            this.busyLabel.Size = new System.Drawing.Size(41, 10);
            this.busyLabel.TabIndex = 1;
            // 
            // inposLabel
            // 
            this.inposLabel.AutoSize = true;
            this.inposLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inposLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inposLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.inposLabel.Location = new System.Drawing.Point(112, 38);
            this.inposLabel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.inposLabel.Name = "inposLabel";
            this.inposLabel.Size = new System.Drawing.Size(41, 10);
            this.inposLabel.TabIndex = 1;
            // 
            // minusLimitLabel
            // 
            this.minusLimitLabel.AutoSize = true;
            this.minusLimitLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.minusLimitLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.minusLimitLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minusLimitLabel.Location = new System.Drawing.Point(112, 55);
            this.minusLimitLabel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.minusLimitLabel.Name = "minusLimitLabel";
            this.minusLimitLabel.Size = new System.Drawing.Size(41, 10);
            this.minusLimitLabel.TabIndex = 1;
            // 
            // plusLimitLabel
            // 
            this.plusLimitLabel.AutoSize = true;
            this.plusLimitLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plusLimitLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plusLimitLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plusLimitLabel.Location = new System.Drawing.Point(112, 72);
            this.plusLimitLabel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.plusLimitLabel.Name = "plusLimitLabel";
            this.plusLimitLabel.Size = new System.Drawing.Size(41, 10);
            this.plusLimitLabel.TabIndex = 1;
            // 
            // orgLabel
            // 
            this.orgLabel.AutoSize = true;
            this.orgLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.orgLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orgLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.orgLabel.Location = new System.Drawing.Point(112, 89);
            this.orgLabel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.orgLabel.Name = "orgLabel";
            this.orgLabel.Size = new System.Drawing.Size(41, 10);
            this.orgLabel.TabIndex = 1;
            // 
            // alarmLabel
            // 
            this.alarmLabel.AutoSize = true;
            this.alarmLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.alarmLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alarmLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.alarmLabel.Location = new System.Drawing.Point(112, 106);
            this.alarmLabel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.alarmLabel.Name = "alarmLabel";
            this.alarmLabel.Size = new System.Drawing.Size(41, 11);
            this.alarmLabel.TabIndex = 1;
            // 
            // resetAlarmButton
            // 
            this.resetAlarmButton.Location = new System.Drawing.Point(288, 93);
            this.resetAlarmButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.resetAlarmButton.Name = "resetAlarmButton";
            this.resetAlarmButton.Size = new System.Drawing.Size(90, 33);
            this.resetAlarmButton.TabIndex = 0;
            this.resetAlarmButton.Text = "Reset Alarm";
            this.resetAlarmButton.UseVisualStyleBackColor = true;
            this.resetAlarmButton.Click += new System.EventHandler(this.resetAlarmButton_Click);
            // 
            // orgButton
            // 
            this.orgButton.Location = new System.Drawing.Point(192, 93);
            this.orgButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.orgButton.Name = "orgButton";
            this.orgButton.Size = new System.Drawing.Size(90, 33);
            this.orgButton.TabIndex = 0;
            this.orgButton.Text = "원점 수행";
            this.orgButton.UseVisualStyleBackColor = true;
            this.orgButton.Click += new System.EventHandler(this.orgButton_Click);
            // 
            // servoOnButton
            // 
            this.servoOnButton.Location = new System.Drawing.Point(0, 93);
            this.servoOnButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.servoOnButton.Name = "servoOnButton";
            this.servoOnButton.Size = new System.Drawing.Size(90, 33);
            this.servoOnButton.TabIndex = 0;
            this.servoOnButton.Text = "SERVO ON";
            this.servoOnButton.UseVisualStyleBackColor = true;
            this.servoOnButton.Click += new System.EventHandler(this.servoOnButton_Click);
            // 
            // emgStopButton
            // 
            this.emgStopButton.Location = new System.Drawing.Point(1116, 1);
            this.emgStopButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.emgStopButton.Name = "emgStopButton";
            this.emgStopButton.Size = new System.Drawing.Size(156, 125);
            this.emgStopButton.TabIndex = 0;
            this.emgStopButton.Text = "긴급 정지";
            this.emgStopButton.UseVisualStyleBackColor = true;
            this.emgStopButton.Click += new System.EventHandler(this.emgStopButton_Click);
            // 
            // servoOffButton
            // 
            this.servoOffButton.Location = new System.Drawing.Point(96, 93);
            this.servoOffButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.servoOffButton.Name = "servoOffButton";
            this.servoOffButton.Size = new System.Drawing.Size(90, 33);
            this.servoOffButton.TabIndex = 0;
            this.servoOffButton.Text = "SERVO OFF";
            this.servoOffButton.UseVisualStyleBackColor = true;
            this.servoOffButton.Click += new System.EventHandler(this.servoOffButton_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(5, 1);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 26);
            this.label11.TabIndex = 8;
            this.label11.Text = "부하율 (%)";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // loadRatioLabel
            // 
            this.loadRatioLabel.AutoSize = true;
            this.loadRatioLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadRatioLabel.Location = new System.Drawing.Point(74, 1);
            this.loadRatioLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.loadRatioLabel.Name = "loadRatioLabel";
            this.loadRatioLabel.Size = new System.Drawing.Size(107, 26);
            this.loadRatioLabel.TabIndex = 8;
            this.loadRatioLabel.Text = "부하율";
            this.loadRatioLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // speedTextBox
            // 
            this.speedTextBox._MinusEnable = true;
            this.speedTextBox._NumberPad = true;
            this.speedTextBox._NumberPadText = null;
            this.speedTextBox._NumberStyle = CNumberKeyPad.Style.Style_Unknwon;
            this.speedTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.speedTextBox.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.speedTextBox.Location = new System.Drawing.Point(74, 31);
            this.speedTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.speedTextBox.Name = "speedTextBox";
            this.speedTextBox.ReadOnly = true;
            this.speedTextBox.Size = new System.Drawing.Size(107, 22);
            this.speedTextBox.TabIndex = 7;
            this.speedTextBox.Text = "1";
            this.speedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.speedTextBox.TextChanged += new System.EventHandler(this.speedTextBox_TextChanged);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.68844F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.31156F));
            this.tableLayoutPanel3.Controls.Add(this.label11, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.speedTextBox, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.loadRatioLabel, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label12, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(523, 2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(186, 56);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(5, 28);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 27);
            this.label12.TabIndex = 8;
            this.label12.Text = "설정 속도";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AxisControlPaix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.jogSpeed3Button);
            this.Controls.Add(this.jogSpeed2Button);
            this.Controls.Add(this.jogSpeed1Button);
            this.Controls.Add(this.plusButton);
            this.Controls.Add(this.servoOffButton);
            this.Controls.Add(this.servoOnButton);
            this.Controls.Add(this.emgStopButton);
            this.Controls.Add(this.orgButton);
            this.Controls.Add(this.resetAlarmButton);
            this.Controls.Add(this.minusButton);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "AxisControlPaix";
            this.Size = new System.Drawing.Size(1280, 130);
            this.Load += new System.EventHandler(this.AxisControl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button minusButton;
    private System.Windows.Forms.Button plusButton;
    private System.Windows.Forms.Button jogSpeed1Button;
    private System.Windows.Forms.Button jogSpeed2Button;
    private System.Windows.Forms.Button jogSpeed3Button;
    private CTextBox absMovePosTextBox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label nowPositionLabel;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Button absMoveButton;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label readyLabel;
    private System.Windows.Forms.Label busyLabel;
    private System.Windows.Forms.Label inposLabel;
    private System.Windows.Forms.Label minusLimitLabel;
    private System.Windows.Forms.Label plusLimitLabel;
    private System.Windows.Forms.Label orgLabel;
    private System.Windows.Forms.Label alarmLabel;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button resetAlarmButton;
    private System.Windows.Forms.Button orgButton;
    private System.Windows.Forms.Button servoOnButton;
    private System.Windows.Forms.Button emgStopButton;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label nameLabel;
    private CTextBox speedTextBox;
    private System.Windows.Forms.Button servoOffButton;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label loadRatioLabel;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    private System.Windows.Forms.Label label12;
}
