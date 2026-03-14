namespace bim_base.FormComponent
{
    partial class UnitPPOut
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
            this.components = new System.ComponentModel.Container();
            this.ui_timer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.leftUnGripOutButton = new CIOButton();
            this.leftGripOutButton = new CIOButton();
            this.rightUnGripOutButton = new CIOButton();
            this.rightGripOutButton = new CIOButton();
            this.turnButton = new CIOButton();
            this.returnButton = new CIOButton();
            this.label7 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ui_timer
            // 
            this.ui_timer.Tick += new System.EventHandler(this.ui_timer_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.leftUnGripOutButton, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.leftGripOutButton, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.rightUnGripOutButton, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.rightGripOutButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.turnButton, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.returnButton, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("SamsungOne 800C", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(961, 157);
            this.tableLayoutPanel1.TabIndex = 1;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(321, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(318, 25);
            this.label2.TabIndex = 53;
            this.label2.Text = "RIGHT OUT";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(319, 25);
            this.label1.TabIndex = 52;
            this.label1.Text = "LEFT OUT";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // leftUnGripOutButton
            // 
            this.leftUnGripOutButton._IO_FALSE_COLOR = System.Drawing.Color.Red;
            this.leftUnGripOutButton._IO_POS = CBUTTON_POS.Left;
            this.leftUnGripOutButton._IO_SIZE = new System.Drawing.Size(10, 10);
            this.leftUnGripOutButton._IO_STATE = false;
            this.leftUnGripOutButton._IO_STATE_EXTRA = false;
            this.leftUnGripOutButton._IO_TRUE_COLOR = System.Drawing.Color.Green;
            this.leftUnGripOutButton._MARGIN = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.leftUnGripOutButton._OUT_STATE = false;
            this.leftUnGripOutButton._TEXT = "Grip Off";
            this.leftUnGripOutButton.BackColor = System.Drawing.Color.Transparent;
            this.leftUnGripOutButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftUnGripOutButton.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftUnGripOutButton.Location = new System.Drawing.Point(4, 95);
            this.leftUnGripOutButton.Name = "leftUnGripOutButton";
            this.leftUnGripOutButton.Size = new System.Drawing.Size(313, 58);
            this.leftUnGripOutButton.TabIndex = 51;
            this.leftUnGripOutButton.UseVisualStyleBackColor = false;
            this.leftUnGripOutButton.Click += new System.EventHandler(this.leftUnGripOutButton_Click);
            // 
            // leftGripOutButton
            // 
            this.leftGripOutButton._IO_FALSE_COLOR = System.Drawing.Color.Red;
            this.leftGripOutButton._IO_POS = CBUTTON_POS.Left;
            this.leftGripOutButton._IO_SIZE = new System.Drawing.Size(10, 10);
            this.leftGripOutButton._IO_STATE = false;
            this.leftGripOutButton._IO_STATE_EXTRA = false;
            this.leftGripOutButton._IO_TRUE_COLOR = System.Drawing.Color.Green;
            this.leftGripOutButton._MARGIN = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.leftGripOutButton._OUT_STATE = false;
            this.leftGripOutButton._TEXT = "Grip On";
            this.leftGripOutButton.BackColor = System.Drawing.Color.Transparent;
            this.leftGripOutButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftGripOutButton.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftGripOutButton.Location = new System.Drawing.Point(4, 30);
            this.leftGripOutButton.Name = "leftGripOutButton";
            this.leftGripOutButton.Size = new System.Drawing.Size(313, 58);
            this.leftGripOutButton.TabIndex = 50;
            this.leftGripOutButton.UseVisualStyleBackColor = false;
            this.leftGripOutButton.Click += new System.EventHandler(this.leftGripOutButton_Click);
            // 
            // rightUnGripOutButton
            // 
            this.rightUnGripOutButton._IO_FALSE_COLOR = System.Drawing.Color.Red;
            this.rightUnGripOutButton._IO_POS = CBUTTON_POS.Left;
            this.rightUnGripOutButton._IO_SIZE = new System.Drawing.Size(10, 10);
            this.rightUnGripOutButton._IO_STATE = false;
            this.rightUnGripOutButton._IO_STATE_EXTRA = false;
            this.rightUnGripOutButton._IO_TRUE_COLOR = System.Drawing.Color.Green;
            this.rightUnGripOutButton._MARGIN = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.rightUnGripOutButton._OUT_STATE = false;
            this.rightUnGripOutButton._TEXT = "Grip Off";
            this.rightUnGripOutButton.BackColor = System.Drawing.Color.Transparent;
            this.rightUnGripOutButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightUnGripOutButton.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightUnGripOutButton.Location = new System.Drawing.Point(324, 95);
            this.rightUnGripOutButton.Name = "rightUnGripOutButton";
            this.rightUnGripOutButton.Size = new System.Drawing.Size(312, 58);
            this.rightUnGripOutButton.TabIndex = 47;
            this.rightUnGripOutButton.UseVisualStyleBackColor = false;
            this.rightUnGripOutButton.Click += new System.EventHandler(this.rightUnGripOutButton_Click);
            // 
            // rightGripOutButton
            // 
            this.rightGripOutButton._IO_FALSE_COLOR = System.Drawing.Color.Red;
            this.rightGripOutButton._IO_POS = CBUTTON_POS.Left;
            this.rightGripOutButton._IO_SIZE = new System.Drawing.Size(10, 10);
            this.rightGripOutButton._IO_STATE = false;
            this.rightGripOutButton._IO_STATE_EXTRA = false;
            this.rightGripOutButton._IO_TRUE_COLOR = System.Drawing.Color.Green;
            this.rightGripOutButton._MARGIN = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.rightGripOutButton._OUT_STATE = false;
            this.rightGripOutButton._TEXT = "Grip On";
            this.rightGripOutButton.BackColor = System.Drawing.Color.Transparent;
            this.rightGripOutButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightGripOutButton.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightGripOutButton.Location = new System.Drawing.Point(324, 30);
            this.rightGripOutButton.Name = "rightGripOutButton";
            this.rightGripOutButton.Size = new System.Drawing.Size(312, 58);
            this.rightGripOutButton.TabIndex = 46;
            this.rightGripOutButton.UseVisualStyleBackColor = false;
            this.rightGripOutButton.Click += new System.EventHandler(this.rightGripOutButton_Click);
            // 
            // turnButton
            // 
            this.turnButton._IO_FALSE_COLOR = System.Drawing.Color.Red;
            this.turnButton._IO_POS = CBUTTON_POS.Left;
            this.turnButton._IO_SIZE = new System.Drawing.Size(10, 10);
            this.turnButton._IO_STATE = false;
            this.turnButton._IO_STATE_EXTRA = false;
            this.turnButton._IO_TRUE_COLOR = System.Drawing.Color.Green;
            this.turnButton._MARGIN = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.turnButton._OUT_STATE = false;
            this.turnButton._TEXT = "TURN";
            this.turnButton.BackColor = System.Drawing.Color.Transparent;
            this.turnButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.turnButton.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.turnButton.Location = new System.Drawing.Point(643, 30);
            this.turnButton.Name = "turnButton";
            this.turnButton.Size = new System.Drawing.Size(314, 58);
            this.turnButton.TabIndex = 40;
            this.turnButton.UseVisualStyleBackColor = false;
            this.turnButton.Click += new System.EventHandler(this.turnButton_Click);
            // 
            // returnButton
            // 
            this.returnButton._IO_FALSE_COLOR = System.Drawing.Color.Red;
            this.returnButton._IO_POS = CBUTTON_POS.Left;
            this.returnButton._IO_SIZE = new System.Drawing.Size(10, 10);
            this.returnButton._IO_STATE = false;
            this.returnButton._IO_STATE_EXTRA = false;
            this.returnButton._IO_TRUE_COLOR = System.Drawing.Color.Green;
            this.returnButton._MARGIN = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.returnButton._OUT_STATE = false;
            this.returnButton._TEXT = "RETURN";
            this.returnButton.BackColor = System.Drawing.Color.Transparent;
            this.returnButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.returnButton.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.returnButton.Location = new System.Drawing.Point(643, 95);
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(314, 58);
            this.returnButton.TabIndex = 39;
            this.returnButton.UseVisualStyleBackColor = false;
            this.returnButton.Click += new System.EventHandler(this.returnButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(640, 1);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(320, 25);
            this.label7.TabIndex = 21;
            this.label7.Text = "PICKER";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UnitPPOut
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UnitPPOut";
            this.Size = new System.Drawing.Size(961, 157);
            this.Load += new System.EventHandler(this.UnitPeelTape_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer ui_timer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CIOButton turnButton;
        private CIOButton returnButton;
        private System.Windows.Forms.Label label7;
        private CIOButton rightUnGripOutButton;
        private CIOButton rightGripOutButton;
        private CIOButton leftGripOutButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private CIOButton leftUnGripOutButton;
    }
}
