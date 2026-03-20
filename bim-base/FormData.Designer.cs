namespace bim_base
{
    partial class FormData
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
            this.pnlFormData = new System.Windows.Forms.Panel();
            this.vacuumDelayButton = new System.Windows.Forms.Button();
            this.cylinderDelayButton = new System.Windows.Forms.Button();
            this.motorDelayButton = new System.Windows.Forms.Button();
            this.motorVelButton = new System.Windows.Forms.Button();
            this.btnServoOrigin = new System.Windows.Forms.Button();
            this.btnServoLimit = new System.Windows.Forms.Button();
            this.btnServoVelocity = new System.Windows.Forms.Button();
            this.jogVelButton = new System.Windows.Forms.Button();
            this.colorButton7 = new SUserControls.ColorButton();
            this.systemManagerButton = new System.Windows.Forms.Button();
            this.portSettingButton = new System.Windows.Forms.Button();
            this.timerSettingButton = new System.Windows.Forms.Button();
            this.colorButton6 = new SUserControls.ColorButton();
            this.modelButton = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.colorButton5 = new SUserControls.ColorButton();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.colorButton4 = new SUserControls.ColorButton();
            this.colorButton3 = new SUserControls.ColorButton();
            this.colorButton2 = new SUserControls.ColorButton();
            this.tapIO = new SUserControls.ColorButton();
            this.colorButton1 = new SUserControls.ColorButton();
            this.label1 = new System.Windows.Forms.Label();
            this.softLimitLabel = new System.Windows.Forms.Label();
            this.ui_timer = new System.Windows.Forms.Timer(this.components);
            this.pnlFormData.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFormData
            // 
            this.pnlFormData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFormData.Controls.Add(this.vacuumDelayButton);
            this.pnlFormData.Controls.Add(this.cylinderDelayButton);
            this.pnlFormData.Controls.Add(this.motorDelayButton);
            this.pnlFormData.Controls.Add(this.motorVelButton);
            this.pnlFormData.Controls.Add(this.btnServoOrigin);
            this.pnlFormData.Controls.Add(this.btnServoLimit);
            this.pnlFormData.Controls.Add(this.btnServoVelocity);
            this.pnlFormData.Controls.Add(this.jogVelButton);
            this.pnlFormData.Controls.Add(this.colorButton7);
            this.pnlFormData.Controls.Add(this.systemManagerButton);
            this.pnlFormData.Controls.Add(this.portSettingButton);
            this.pnlFormData.Controls.Add(this.timerSettingButton);
            this.pnlFormData.Controls.Add(this.colorButton6);
            this.pnlFormData.Controls.Add(this.modelButton);
            this.pnlFormData.Controls.Add(this.button5);
            this.pnlFormData.Controls.Add(this.colorButton5);
            this.pnlFormData.Controls.Add(this.button4);
            this.pnlFormData.Controls.Add(this.button2);
            this.pnlFormData.Controls.Add(this.button1);
            this.pnlFormData.Controls.Add(this.button3);
            this.pnlFormData.Controls.Add(this.tableLayoutPanel1);
            this.pnlFormData.Controls.Add(this.colorButton1);
            this.pnlFormData.Controls.Add(this.label1);
            this.pnlFormData.Controls.Add(this.softLimitLabel);
            this.pnlFormData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFormData.Location = new System.Drawing.Point(20, 20);
            this.pnlFormData.Margin = new System.Windows.Forms.Padding(1);
            this.pnlFormData.Name = "pnlFormData";
            this.pnlFormData.Size = new System.Drawing.Size(984, 588);
            this.pnlFormData.TabIndex = 0;
            // 
            // vacuumDelayButton
            // 
            this.vacuumDelayButton.BackColor = System.Drawing.Color.White;
            this.vacuumDelayButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.vacuumDelayButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.vacuumDelayButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.vacuumDelayButton.Location = new System.Drawing.Point(517, 327);
            this.vacuumDelayButton.Margin = new System.Windows.Forms.Padding(7);
            this.vacuumDelayButton.Name = "vacuumDelayButton";
            this.vacuumDelayButton.Size = new System.Drawing.Size(200, 50);
            this.vacuumDelayButton.TabIndex = 1211;
            this.vacuumDelayButton.Text = "Vacuum Delay";
            this.vacuumDelayButton.UseVisualStyleBackColor = false;
            this.vacuumDelayButton.Click += new System.EventHandler(this.vacuumDelayButton_Click);
            // 
            // cylinderDelayButton
            // 
            this.cylinderDelayButton.BackColor = System.Drawing.Color.White;
            this.cylinderDelayButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.cylinderDelayButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cylinderDelayButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.cylinderDelayButton.Location = new System.Drawing.Point(517, 248);
            this.cylinderDelayButton.Margin = new System.Windows.Forms.Padding(7);
            this.cylinderDelayButton.Name = "cylinderDelayButton";
            this.cylinderDelayButton.Size = new System.Drawing.Size(200, 50);
            this.cylinderDelayButton.TabIndex = 1210;
            this.cylinderDelayButton.Text = "Cylinder Delay";
            this.cylinderDelayButton.UseVisualStyleBackColor = false;
            this.cylinderDelayButton.Click += new System.EventHandler(this.cylinderDelayButton_Click);
            // 
            // motorDelayButton
            // 
            this.motorDelayButton.BackColor = System.Drawing.Color.White;
            this.motorDelayButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.motorDelayButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.motorDelayButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.motorDelayButton.Location = new System.Drawing.Point(517, 169);
            this.motorDelayButton.Margin = new System.Windows.Forms.Padding(7);
            this.motorDelayButton.Name = "motorDelayButton";
            this.motorDelayButton.Size = new System.Drawing.Size(200, 50);
            this.motorDelayButton.TabIndex = 1209;
            this.motorDelayButton.Text = "Motor Delay";
            this.motorDelayButton.UseVisualStyleBackColor = false;
            this.motorDelayButton.Click += new System.EventHandler(this.motorDelayButton_Click);
            // 
            // motorVelButton
            // 
            this.motorVelButton.BackColor = System.Drawing.Color.White;
            this.motorVelButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.motorVelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.motorVelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.motorVelButton.Location = new System.Drawing.Point(774, 530);
            this.motorVelButton.Margin = new System.Windows.Forms.Padding(7);
            this.motorVelButton.Name = "motorVelButton";
            this.motorVelButton.Size = new System.Drawing.Size(200, 50);
            this.motorVelButton.TabIndex = 1208;
            this.motorVelButton.Text = "Motor Velocity";
            this.motorVelButton.UseVisualStyleBackColor = false;
            this.motorVelButton.Click += new System.EventHandler(this.motorVelButton_Click);
            // 
            // btnServoOrigin
            // 
            this.btnServoOrigin.BackColor = System.Drawing.Color.White;
            this.btnServoOrigin.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnServoOrigin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnServoOrigin.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnServoOrigin.Location = new System.Drawing.Point(774, 248);
            this.btnServoOrigin.Margin = new System.Windows.Forms.Padding(7);
            this.btnServoOrigin.Name = "btnServoOrigin";
            this.btnServoOrigin.Size = new System.Drawing.Size(200, 50);
            this.btnServoOrigin.TabIndex = 1207;
            this.btnServoOrigin.Text = "Servo Origin";
            this.btnServoOrigin.UseVisualStyleBackColor = false;
            this.btnServoOrigin.Click += new System.EventHandler(this.btnServoOrigin_Click);
            // 
            // btnServoLimit
            // 
            this.btnServoLimit.BackColor = System.Drawing.Color.White;
            this.btnServoLimit.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnServoLimit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnServoLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnServoLimit.Location = new System.Drawing.Point(774, 169);
            this.btnServoLimit.Margin = new System.Windows.Forms.Padding(7);
            this.btnServoLimit.Name = "btnServoLimit";
            this.btnServoLimit.Size = new System.Drawing.Size(200, 50);
            this.btnServoLimit.TabIndex = 1206;
            this.btnServoLimit.Text = "Servo Limit";
            this.btnServoLimit.UseVisualStyleBackColor = false;
            this.btnServoLimit.Click += new System.EventHandler(this.btnServoLimit_Click);
            // 
            // btnServoVelocity
            // 
            this.btnServoVelocity.BackColor = System.Drawing.Color.White;
            this.btnServoVelocity.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnServoVelocity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnServoVelocity.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnServoVelocity.Location = new System.Drawing.Point(774, 90);
            this.btnServoVelocity.Margin = new System.Windows.Forms.Padding(7);
            this.btnServoVelocity.Name = "btnServoVelocity";
            this.btnServoVelocity.Size = new System.Drawing.Size(200, 50);
            this.btnServoVelocity.TabIndex = 1205;
            this.btnServoVelocity.Text = "Servo Velocity";
            this.btnServoVelocity.UseVisualStyleBackColor = false;
            this.btnServoVelocity.Click += new System.EventHandler(this.btnServoVelocity_Click);
            // 
            // jogVelButton
            // 
            this.jogVelButton.BackColor = System.Drawing.Color.White;
            this.jogVelButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.jogVelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.jogVelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jogVelButton.Location = new System.Drawing.Point(774, 454);
            this.jogVelButton.Margin = new System.Windows.Forms.Padding(7);
            this.jogVelButton.Name = "jogVelButton";
            this.jogVelButton.Size = new System.Drawing.Size(200, 50);
            this.jogVelButton.TabIndex = 1204;
            this.jogVelButton.Text = "Jog Velocity";
            this.jogVelButton.UseVisualStyleBackColor = false;
            this.jogVelButton.Click += new System.EventHandler(this.jogVelButton_Click);
            // 
            // colorButton7
            // 
            this.colorButton7.BackColor = System.Drawing.Color.Transparent;
            this.colorButton7.BorderLineColor = System.Drawing.Color.Black;
            this.colorButton7.BorderThickness = 0F;
            this.colorButton7.Checked = true;
            this.colorButton7.CheckedButtonColor = System.Drawing.Color.Black;
            this.colorButton7.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.colorButton7.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.colorButton7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorButton7.ForeColor = System.Drawing.Color.Black;
            this.colorButton7.GradientBottom = System.Drawing.Color.White;
            this.colorButton7.GradientTop = System.Drawing.Color.White;
            this.colorButton7.Location = new System.Drawing.Point(748, 47);
            this.colorButton7.Name = "colorButton7";
            this.colorButton7.RectCornerRadius = 2;
            this.colorButton7.Size = new System.Drawing.Size(250, 550);
            this.colorButton7.TabIndex = 1203;
            this.colorButton7.UseVisualStyleBackColor = false;
            // 
            // systemManagerButton
            // 
            this.systemManagerButton.BackColor = System.Drawing.Color.White;
            this.systemManagerButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.systemManagerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.systemManagerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.systemManagerButton.Location = new System.Drawing.Point(260, 327);
            this.systemManagerButton.Margin = new System.Windows.Forms.Padding(7);
            this.systemManagerButton.Name = "systemManagerButton";
            this.systemManagerButton.Size = new System.Drawing.Size(200, 50);
            this.systemManagerButton.TabIndex = 1202;
            this.systemManagerButton.Text = "System Manger";
            this.systemManagerButton.UseVisualStyleBackColor = false;
            this.systemManagerButton.Click += new System.EventHandler(this.systemManagerButton_Click);
            // 
            // portSettingButton
            // 
            this.portSettingButton.BackColor = System.Drawing.Color.White;
            this.portSettingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.portSettingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.portSettingButton.Location = new System.Drawing.Point(260, 248);
            this.portSettingButton.Margin = new System.Windows.Forms.Padding(7);
            this.portSettingButton.Name = "portSettingButton";
            this.portSettingButton.Size = new System.Drawing.Size(200, 50);
            this.portSettingButton.TabIndex = 1201;
            this.portSettingButton.Text = "Port Setting";
            this.portSettingButton.UseVisualStyleBackColor = false;
            this.portSettingButton.Click += new System.EventHandler(this.portSettingButton_Click);
            // 
            // timerSettingButton
            // 
            this.timerSettingButton.BackColor = System.Drawing.Color.White;
            this.timerSettingButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.timerSettingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.timerSettingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timerSettingButton.Location = new System.Drawing.Point(517, 90);
            this.timerSettingButton.Margin = new System.Windows.Forms.Padding(7);
            this.timerSettingButton.Name = "timerSettingButton";
            this.timerSettingButton.Size = new System.Drawing.Size(200, 50);
            this.timerSettingButton.TabIndex = 1200;
            this.timerSettingButton.Text = "Timer";
            this.timerSettingButton.UseVisualStyleBackColor = false;
            this.timerSettingButton.Click += new System.EventHandler(this.timerSettingButton_Click);
            // 
            // colorButton6
            // 
            this.colorButton6.BackColor = System.Drawing.Color.Transparent;
            this.colorButton6.BorderLineColor = System.Drawing.Color.Black;
            this.colorButton6.BorderThickness = 0F;
            this.colorButton6.Checked = true;
            this.colorButton6.CheckedButtonColor = System.Drawing.Color.Black;
            this.colorButton6.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.colorButton6.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.colorButton6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorButton6.ForeColor = System.Drawing.Color.Black;
            this.colorButton6.GradientBottom = System.Drawing.Color.White;
            this.colorButton6.GradientTop = System.Drawing.Color.White;
            this.colorButton6.Location = new System.Drawing.Point(492, 47);
            this.colorButton6.Name = "colorButton6";
            this.colorButton6.RectCornerRadius = 2;
            this.colorButton6.Size = new System.Drawing.Size(250, 550);
            this.colorButton6.TabIndex = 1199;
            this.colorButton6.UseVisualStyleBackColor = false;
            // 
            // modelButton
            // 
            this.modelButton.BackColor = System.Drawing.Color.White;
            this.modelButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.modelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.modelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modelButton.Location = new System.Drawing.Point(260, 169);
            this.modelButton.Margin = new System.Windows.Forms.Padding(7);
            this.modelButton.Name = "modelButton";
            this.modelButton.Size = new System.Drawing.Size(200, 50);
            this.modelButton.TabIndex = 1198;
            this.modelButton.Text = "Model";
            this.modelButton.UseVisualStyleBackColor = false;
            this.modelButton.Click += new System.EventHandler(this.modelButton_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.White;
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(260, 90);
            this.button5.Margin = new System.Windows.Forms.Padding(7);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(200, 50);
            this.button5.TabIndex = 1197;
            this.button5.Text = "Interlock";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // colorButton5
            // 
            this.colorButton5.BackColor = System.Drawing.Color.Transparent;
            this.colorButton5.BorderLineColor = System.Drawing.Color.Black;
            this.colorButton5.BorderThickness = 0F;
            this.colorButton5.Checked = true;
            this.colorButton5.CheckedButtonColor = System.Drawing.Color.Black;
            this.colorButton5.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.colorButton5.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.colorButton5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorButton5.ForeColor = System.Drawing.Color.Black;
            this.colorButton5.GradientBottom = System.Drawing.Color.White;
            this.colorButton5.GradientTop = System.Drawing.Color.White;
            this.colorButton5.Location = new System.Drawing.Point(236, 47);
            this.colorButton5.Name = "colorButton5";
            this.colorButton5.RectCornerRadius = 2;
            this.colorButton5.Size = new System.Drawing.Size(250, 550);
            this.colorButton5.TabIndex = 1196;
            this.colorButton5.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(4, 327);
            this.button4.Margin = new System.Windows.Forms.Padding(7);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(200, 50);
            this.button4.TabIndex = 1195;
            this.button4.Text = "Teperature";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(4, 169);
            this.button2.Margin = new System.Windows.Forms.Padding(7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(200, 50);
            this.button2.TabIndex = 1193;
            this.button2.Text = "Recipe List";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(4, 90);
            this.button1.Margin = new System.Windows.Forms.Padding(7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 50);
            this.button1.TabIndex = 1192;
            this.button1.Text = "Recipe Data";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(4, 248);
            this.button3.Margin = new System.Windows.Forms.Padding(7);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(200, 50);
            this.button3.TabIndex = 1194;
            this.button3.Text = "Pump Data";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.colorButton4, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.colorButton3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.colorButton2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tapIO, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(-23, -6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1024, 47);
            this.tableLayoutPanel1.TabIndex = 1191;
            // 
            // colorButton4
            // 
            this.colorButton4.BackColor = System.Drawing.Color.Transparent;
            this.colorButton4.BorderLineColor = System.Drawing.Color.Black;
            this.colorButton4.BorderThickness = 0F;
            this.colorButton4.Checked = false;
            this.colorButton4.CheckedButtonColor = System.Drawing.Color.Black;
            this.colorButton4.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.colorButton4.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.colorButton4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorButton4.ForeColor = System.Drawing.Color.Black;
            this.colorButton4.GradientBottom = System.Drawing.Color.Silver;
            this.colorButton4.GradientTop = System.Drawing.Color.Silver;
            this.colorButton4.Location = new System.Drawing.Point(771, 3);
            this.colorButton4.Name = "colorButton4";
            this.colorButton4.RectCornerRadius = 2;
            this.colorButton4.Size = new System.Drawing.Size(250, 39);
            this.colorButton4.TabIndex = 1151;
            this.colorButton4.Text = "Servo Setting";
            this.colorButton4.UseVisualStyleBackColor = false;
            // 
            // colorButton3
            // 
            this.colorButton3.BackColor = System.Drawing.Color.Transparent;
            this.colorButton3.BorderLineColor = System.Drawing.Color.Black;
            this.colorButton3.BorderThickness = 0F;
            this.colorButton3.Checked = false;
            this.colorButton3.CheckedButtonColor = System.Drawing.Color.Black;
            this.colorButton3.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.colorButton3.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.colorButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorButton3.ForeColor = System.Drawing.Color.Black;
            this.colorButton3.GradientBottom = System.Drawing.Color.Silver;
            this.colorButton3.GradientTop = System.Drawing.Color.Silver;
            this.colorButton3.Location = new System.Drawing.Point(515, 3);
            this.colorButton3.Name = "colorButton3";
            this.colorButton3.RectCornerRadius = 2;
            this.colorButton3.Size = new System.Drawing.Size(250, 39);
            this.colorButton3.TabIndex = 1150;
            this.colorButton3.Text = "Delay";
            this.colorButton3.UseVisualStyleBackColor = false;
            // 
            // colorButton2
            // 
            this.colorButton2.BackColor = System.Drawing.Color.Transparent;
            this.colorButton2.BorderLineColor = System.Drawing.Color.Black;
            this.colorButton2.BorderThickness = 0F;
            this.colorButton2.Checked = false;
            this.colorButton2.CheckedButtonColor = System.Drawing.Color.Black;
            this.colorButton2.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.colorButton2.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.colorButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorButton2.ForeColor = System.Drawing.Color.Black;
            this.colorButton2.GradientBottom = System.Drawing.Color.Silver;
            this.colorButton2.GradientTop = System.Drawing.Color.Silver;
            this.colorButton2.Location = new System.Drawing.Point(259, 3);
            this.colorButton2.Name = "colorButton2";
            this.colorButton2.RectCornerRadius = 2;
            this.colorButton2.Size = new System.Drawing.Size(250, 39);
            this.colorButton2.TabIndex = 1149;
            this.colorButton2.Text = "Additional Settings";
            this.colorButton2.UseVisualStyleBackColor = false;
            // 
            // tapIO
            // 
            this.tapIO.BackColor = System.Drawing.Color.Transparent;
            this.tapIO.BorderLineColor = System.Drawing.Color.Black;
            this.tapIO.BorderThickness = 0F;
            this.tapIO.Checked = false;
            this.tapIO.CheckedButtonColor = System.Drawing.Color.Black;
            this.tapIO.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tapIO.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tapIO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tapIO.ForeColor = System.Drawing.Color.Black;
            this.tapIO.GradientBottom = System.Drawing.Color.Silver;
            this.tapIO.GradientTop = System.Drawing.Color.Silver;
            this.tapIO.Location = new System.Drawing.Point(3, 3);
            this.tapIO.Name = "tapIO";
            this.tapIO.RectCornerRadius = 2;
            this.tapIO.Size = new System.Drawing.Size(250, 39);
            this.tapIO.TabIndex = 1148;
            this.tapIO.Text = "Recipe Setting";
            this.tapIO.UseVisualStyleBackColor = false;
            // 
            // colorButton1
            // 
            this.colorButton1.BackColor = System.Drawing.Color.Transparent;
            this.colorButton1.BorderLineColor = System.Drawing.Color.Black;
            this.colorButton1.BorderThickness = 0F;
            this.colorButton1.Checked = true;
            this.colorButton1.CheckedButtonColor = System.Drawing.Color.Black;
            this.colorButton1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.colorButton1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.colorButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorButton1.ForeColor = System.Drawing.Color.Black;
            this.colorButton1.GradientBottom = System.Drawing.Color.White;
            this.colorButton1.GradientTop = System.Drawing.Color.White;
            this.colorButton1.Location = new System.Drawing.Point(-20, 47);
            this.colorButton1.Name = "colorButton1";
            this.colorButton1.RectCornerRadius = 2;
            this.colorButton1.Size = new System.Drawing.Size(250, 550);
            this.colorButton1.TabIndex = 1190;
            this.colorButton1.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(903, -6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 42);
            this.label1.TabIndex = 1189;
            // 
            // softLimitLabel
            // 
            this.softLimitLabel.Location = new System.Drawing.Point(-19, -4);
            this.softLimitLabel.Name = "softLimitLabel";
            this.softLimitLabel.Size = new System.Drawing.Size(153, 118);
            this.softLimitLabel.TabIndex = 1188;
            // 
            // ui_timer
            // 
            this.ui_timer.Enabled = true;
            // 
            // FormData
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 628);
            this.Controls.Add(this.pnlFormData);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormData";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Text = "FormCenter2";
            this.Load += new System.EventHandler(this.FormData_Load);
            this.pnlFormData.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFormData;
        private System.Windows.Forms.Button motorVelButton;
        private System.Windows.Forms.Button btnServoOrigin;
        private System.Windows.Forms.Button btnServoLimit;
        private System.Windows.Forms.Button btnServoVelocity;
        private System.Windows.Forms.Button jogVelButton;
        public SUserControls.ColorButton colorButton7;
        private System.Windows.Forms.Button systemManagerButton;
        private System.Windows.Forms.Button portSettingButton;
        private System.Windows.Forms.Button timerSettingButton;
        public SUserControls.ColorButton colorButton6;
        private System.Windows.Forms.Button modelButton;
        private System.Windows.Forms.Button button5;
        public SUserControls.ColorButton colorButton5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public SUserControls.ColorButton colorButton4;
        public SUserControls.ColorButton colorButton3;
        public SUserControls.ColorButton colorButton2;
        public SUserControls.ColorButton tapIO;
        public SUserControls.ColorButton colorButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label softLimitLabel;
        private System.Windows.Forms.Timer ui_timer;
        private System.Windows.Forms.Button vacuumDelayButton;
        private System.Windows.Forms.Button cylinderDelayButton;
        private System.Windows.Forms.Button motorDelayButton;
    }
}