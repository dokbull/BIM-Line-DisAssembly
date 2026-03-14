
namespace bim_base
{
    partial class FormOutputTactime
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

        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.DataGridView dgv2;
        private System.Windows.Forms.DataGridView dgv3;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.Label lblAve;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox groupStats;
        private System.Windows.Forms.Panel panelGrids;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.dgv2 = new System.Windows.Forms.DataGridView();
            this.dgv3 = new System.Windows.Forms.DataGridView();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.lblMax = new System.Windows.Forms.Label();
            this.lblMin = new System.Windows.Forms.Label();
            this.lblAve = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.groupStats = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelGrids = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.BT_EXIT = new SUserControls.ColorButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv3)).BeginInit();
            this.groupStats.SuspendLayout();
            this.panelGrids.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv1
            // 
            this.dgv1.Location = new System.Drawing.Point(0, 0);
            this.dgv1.Name = "dgv1";
            this.dgv1.Size = new System.Drawing.Size(240, 615);
            this.dgv1.TabIndex = 0;
            // 
            // dgv2
            // 
            this.dgv2.Location = new System.Drawing.Point(250, 0);
            this.dgv2.Name = "dgv2";
            this.dgv2.Size = new System.Drawing.Size(240, 615);
            this.dgv2.TabIndex = 1;
            // 
            // dgv3
            // 
            this.dgv3.Location = new System.Drawing.Point(500, 0);
            this.dgv3.Name = "dgv3";
            this.dgv3.Size = new System.Drawing.Size(240, 615);
            this.dgv3.TabIndex = 2;
            // 
            // lblCurrent
            // 
            this.lblCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrent.Location = new System.Drawing.Point(205, 42);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(115, 39);
            this.lblCurrent.TabIndex = 0;
            this.lblCurrent.Text = "Current Time: -";
            // 
            // lblMax
            // 
            this.lblMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMax.Location = new System.Drawing.Point(206, 93);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(115, 39);
            this.lblMax.TabIndex = 1;
            this.lblMax.Text = "Time Max: -";
            // 
            // lblMin
            // 
            this.lblMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMin.Location = new System.Drawing.Point(205, 145);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(115, 39);
            this.lblMin.TabIndex = 2;
            this.lblMin.Text = "Time Min: -";
            // 
            // lblAve
            // 
            this.lblAve.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAve.Location = new System.Drawing.Point(205, 197);
            this.lblAve.Name = "lblAve";
            this.lblAve.Size = new System.Drawing.Size(115, 39);
            this.lblAve.TabIndex = 3;
            this.lblAve.Text = "Time Ave: -";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(894, 269);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(109, 46);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // groupStats
            // 
            this.groupStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupStats.Controls.Add(this.label4);
            this.groupStats.Controls.Add(this.label3);
            this.groupStats.Controls.Add(this.label2);
            this.groupStats.Controls.Add(this.label1);
            this.groupStats.Controls.Add(this.lblCurrent);
            this.groupStats.Controls.Add(this.lblMax);
            this.groupStats.Controls.Add(this.lblMin);
            this.groupStats.Controls.Add(this.lblAve);
            this.groupStats.Location = new System.Drawing.Point(768, 12);
            this.groupStats.Name = "groupStats";
            this.groupStats.Size = new System.Drawing.Size(337, 242);
            this.groupStats.TabIndex = 3;
            this.groupStats.TabStop = false;
            this.groupStats.Text = "Statistics";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Silver;
            this.label4.Enabled = false;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(181, 39);
            this.label4.TabIndex = 246;
            this.label4.Text = "Time Ave";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Silver;
            this.label3.Enabled = false;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 39);
            this.label3.TabIndex = 245;
            this.label3.Text = "Time Min";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Silver;
            this.label2.Enabled = false;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 39);
            this.label2.TabIndex = 244;
            this.label2.Text = "Time Max";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Silver;
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 39);
            this.label1.TabIndex = 243;
            this.label1.Text = "Current Time";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelGrids
            // 
            this.panelGrids.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGrids.Controls.Add(this.dgv1);
            this.panelGrids.Controls.Add(this.dgv2);
            this.panelGrids.Controls.Add(this.dgv3);
            this.panelGrids.Location = new System.Drawing.Point(12, 99);
            this.panelGrids.Name = "panelGrids";
            this.panelGrids.Size = new System.Drawing.Size(750, 622);
            this.panelGrids.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label5.Enabled = false;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(750, 48);
            this.label5.TabIndex = 868;
            this.label5.Text = "TACT TIME";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BT_EXIT
            // 
            this.BT_EXIT.BackColor = System.Drawing.Color.Transparent;
            this.BT_EXIT.BorderLineColor = System.Drawing.Color.Black;
            this.BT_EXIT.Checked = false;
            this.BT_EXIT.CheckedButtonColor = System.Drawing.Color.Black;
            this.BT_EXIT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.BT_EXIT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrange;
            this.BT_EXIT.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_EXIT.ForeColor = System.Drawing.Color.Red;
            this.BT_EXIT.GradientBottom = System.Drawing.Color.White;
            this.BT_EXIT.GradientTop = System.Drawing.Color.MistyRose;
            this.BT_EXIT.Location = new System.Drawing.Point(894, 635);
            this.BT_EXIT.Name = "BT_EXIT";
            this.BT_EXIT.RectCornerRadius = 2;
            this.BT_EXIT.Size = new System.Drawing.Size(158, 79);
            this.BT_EXIT.TabIndex = 1154;
            this.BT_EXIT.Text = "Exit";
            this.BT_EXIT.UseVisualStyleBackColor = false;
            this.BT_EXIT.Click += new System.EventHandler(this.BT_EXIT_Click);
            // 
            // FormOutputTactime
            // 
            this.ClientSize = new System.Drawing.Size(1119, 744);
            this.Controls.Add(this.BT_EXIT);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panelGrids);
            this.Controls.Add(this.groupStats);
            this.Controls.Add(this.btnReset);
            this.MinimumSize = new System.Drawing.Size(1030, 399);
            this.Name = "FormOutputTactime";
            this.Text = "Output Tact Time ";
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv3)).EndInit();
            this.groupStats.ResumeLayout(false);
            this.panelGrids.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        public SUserControls.ColorButton BT_EXIT;
    }
}
