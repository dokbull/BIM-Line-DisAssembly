namespace bim_base
{
    partial class FormPassword
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.buttonPress1 = new System.Windows.Forms.Button();
            this.pwTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cancelButton.ForeColor = System.Drawing.Color.Black;
            this.cancelButton.Location = new System.Drawing.Point(312, 66);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(145, 54);
            this.cancelButton.TabIndex = 898;
            this.cancelButton.Text = "CANCEL";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.okButton.ForeColor = System.Drawing.Color.Black;
            this.okButton.Location = new System.Drawing.Point(161, 66);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(145, 54);
            this.okButton.TabIndex = 898;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // buttonPress1
            // 
            this.buttonPress1.BackColor = System.Drawing.Color.LightBlue;
            this.buttonPress1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPress1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPress1.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.buttonPress1.ForeColor = System.Drawing.Color.Blue;
            this.buttonPress1.Location = new System.Drawing.Point(3, 3);
            this.buttonPress1.Name = "buttonPress1";
            this.buttonPress1.Size = new System.Drawing.Size(130, 42);
            this.buttonPress1.TabIndex = 297;
            this.buttonPress1.Text = "PASSWORD";
            this.buttonPress1.UseVisualStyleBackColor = false;
            // 
            // pwTextBox
            // 
            this.pwTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pwTextBox.Font = new System.Drawing.Font("맑은 고딕", 20F);
            this.pwTextBox.Location = new System.Drawing.Point(139, 3);
            this.pwTextBox.Name = "pwTextBox";
            this.pwTextBox.PasswordChar = '*';
            this.pwTextBox.ReadOnly = true;
            this.pwTextBox.Size = new System.Drawing.Size(303, 43);
            this.pwTextBox.TabIndex = 298;
            this.pwTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.pwTextBox.Click += new System.EventHandler(this.pwTextBox_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.5618F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.4382F));
            this.tableLayoutPanel1.Controls.Add(this.buttonPress1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pwTextBox, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(445, 48);
            this.tableLayoutPanel1.TabIndex = 899;
            // 
            // FormPassword
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(471, 131);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ADMIN LOGIN";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Button buttonPress1;
        public System.Windows.Forms.Button okButton;
        public System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox pwTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}