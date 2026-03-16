namespace bim_base
{
    partial class FormTeach
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
            this.inPPButton = new System.Windows.Forms.Button();
            this.peelingTapeButton = new System.Windows.Forms.Button();
            this.PPbutton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.RPPButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.outPPTab = new MyButton.ButtonPress();
            this.moldPPTab = new MyButton.ButtonPress();
            this.inPPTab = new MyButton.ButtonPress();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.uiTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // inPPButton
            // 
            this.inPPButton.BackColor = System.Drawing.Color.White;
            this.inPPButton.Location = new System.Drawing.Point(12, 12);
            this.inPPButton.Name = "inPPButton";
            this.inPPButton.Size = new System.Drawing.Size(14, 10);
            this.inPPButton.TabIndex = 0;
            this.inPPButton.Text = "DISPLAY\r\nTRANSFER";
            this.inPPButton.UseVisualStyleBackColor = false;
            // 
            // peelingTapeButton
            // 
            this.peelingTapeButton.BackColor = System.Drawing.Color.White;
            this.peelingTapeButton.Location = new System.Drawing.Point(12, 12);
            this.peelingTapeButton.Name = "peelingTapeButton";
            this.peelingTapeButton.Size = new System.Drawing.Size(14, 10);
            this.peelingTapeButton.TabIndex = 0;
            this.peelingTapeButton.Text = "NG BUFFER";
            this.peelingTapeButton.UseVisualStyleBackColor = false;
            // 
            // PPbutton
            // 
            this.PPbutton.BackColor = System.Drawing.Color.White;
            this.PPbutton.Location = new System.Drawing.Point(12, 12);
            this.PPbutton.Name = "PPbutton";
            this.PPbutton.Size = new System.Drawing.Size(14, 10);
            this.PPbutton.TabIndex = 1;
            this.PPbutton.Text = "IN LIFT\r\nOUT LIFT";
            this.PPbutton.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(14, 10);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 61;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // RPPButton
            // 
            this.RPPButton.BackColor = System.Drawing.Color.White;
            this.RPPButton.Location = new System.Drawing.Point(12, 12);
            this.RPPButton.Name = "RPPButton";
            this.RPPButton.Size = new System.Drawing.Size(14, 10);
            this.RPPButton.TabIndex = 62;
            this.RPPButton.Text = "PP ATTACH\r\nCV ATTACH\r\nREMOVE VINYL";
            this.RPPButton.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1022, 624);
            this.tableLayoutPanel1.TabIndex = 63;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.outPPTab, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.moldPPTab, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.inPPTab, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1022, 40);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // outPPTab
            // 
            this.outPPTab.BorderLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(53)))), ((int)(((byte)(98)))), ((int)(((byte)(141)))));
            this.outPPTab.Checked = false;
            this.outPPTab.CheckedButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(53)))), ((int)(((byte)(98)))), ((int)(((byte)(141)))));
            this.outPPTab.ColorPress = System.Drawing.Color.Blue;
            this.outPPTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outPPTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outPPTab.ForeColor = System.Drawing.Color.White;
            this.outPPTab.GradientBottom = System.Drawing.Color.Gray;
            this.outPPTab.GradientTop = System.Drawing.Color.Gray;
            this.outPPTab.ImagePress = null;
            this.outPPTab.ImageRelease = null;
            this.outPPTab.Location = new System.Drawing.Point(682, 2);
            this.outPPTab.Margin = new System.Windows.Forms.Padding(1);
            this.outPPTab.Name = "outPPTab";
            this.outPPTab.Offset_Image_X = 5;
            this.outPPTab.SetPress = false;
            this.outPPTab.Size = new System.Drawing.Size(338, 36);
            this.outPPTab.TabIndex = 2;
            this.outPPTab.Text = "OUT PP";
            this.outPPTab.UseVisualStyleBackColor = true;
            this.outPPTab.Click += new System.EventHandler(this.outPPTab_Click);
            // 
            // moldPPTab
            // 
            this.moldPPTab.BorderLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(53)))), ((int)(((byte)(98)))), ((int)(((byte)(141)))));
            this.moldPPTab.Checked = false;
            this.moldPPTab.CheckedButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(53)))), ((int)(((byte)(98)))), ((int)(((byte)(141)))));
            this.moldPPTab.ColorPress = System.Drawing.Color.Blue;
            this.moldPPTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.moldPPTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moldPPTab.ForeColor = System.Drawing.Color.White;
            this.moldPPTab.GradientBottom = System.Drawing.Color.Gray;
            this.moldPPTab.GradientTop = System.Drawing.Color.Gray;
            this.moldPPTab.ImagePress = null;
            this.moldPPTab.ImageRelease = null;
            this.moldPPTab.Location = new System.Drawing.Point(342, 2);
            this.moldPPTab.Margin = new System.Windows.Forms.Padding(1);
            this.moldPPTab.Name = "moldPPTab";
            this.moldPPTab.Offset_Image_X = 5;
            this.moldPPTab.SetPress = false;
            this.moldPPTab.Size = new System.Drawing.Size(337, 36);
            this.moldPPTab.TabIndex = 1;
            this.moldPPTab.Text = "MOLD PP";
            this.moldPPTab.UseVisualStyleBackColor = true;
            this.moldPPTab.Click += new System.EventHandler(this.moldPPTab_Click);
            // 
            // inPPTab
            // 
            this.inPPTab.BorderLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(53)))), ((int)(((byte)(98)))), ((int)(((byte)(141)))));
            this.inPPTab.Checked = false;
            this.inPPTab.CheckedButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(53)))), ((int)(((byte)(98)))), ((int)(((byte)(141)))));
            this.inPPTab.ColorPress = System.Drawing.Color.Blue;
            this.inPPTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inPPTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inPPTab.ForeColor = System.Drawing.Color.White;
            this.inPPTab.GradientBottom = System.Drawing.Color.Gray;
            this.inPPTab.GradientTop = System.Drawing.Color.Gray;
            this.inPPTab.ImagePress = null;
            this.inPPTab.ImageRelease = null;
            this.inPPTab.Location = new System.Drawing.Point(2, 2);
            this.inPPTab.Margin = new System.Windows.Forms.Padding(1);
            this.inPPTab.Name = "inPPTab";
            this.inPPTab.Offset_Image_X = 5;
            this.inPPTab.SetPress = false;
            this.inPPTab.Size = new System.Drawing.Size(337, 36);
            this.inPPTab.TabIndex = 0;
            this.inPPTab.Text = "IN PP";
            this.inPPTab.UseVisualStyleBackColor = true;
            this.inPPTab.Click += new System.EventHandler(this.inPPTab_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(3, 43);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1016, 578);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1008, 549);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1008, 549);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1008, 549);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // uiTimer
            // 
            this.uiTimer.Enabled = true;
            this.uiTimer.Tick += new System.EventHandler(this.uiTimer_Tick);
            // 
            // FormTeach
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 624);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.RPPButton);
            this.Controls.Add(this.PPbutton);
            this.Controls.Add(this.peelingTapeButton);
            this.Controls.Add(this.inPPButton);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormTeach";
            this.Text = "FormCenter1";
            this.Load += new System.EventHandler(this.FormTeach_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button inPPButton;
        private System.Windows.Forms.Button peelingTapeButton;
        private System.Windows.Forms.Button PPbutton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button RPPButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private MyButton.ButtonPress inPPTab;
        private MyButton.ButtonPress outPPTab;
        private MyButton.ButtonPress moldPPTab;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Timer uiTimer;
    }
}