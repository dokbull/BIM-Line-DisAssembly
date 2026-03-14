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
            this.inPPButton = new System.Windows.Forms.Button();
            this.cylButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.trayPPButton = new System.Windows.Forms.Button();
            this.outPPButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inPPButton
            // 
            this.inPPButton.BackColor = System.Drawing.Color.White;
            this.inPPButton.Location = new System.Drawing.Point(121, 153);
            this.inPPButton.Name = "inPPButton";
            this.inPPButton.Size = new System.Drawing.Size(247, 147);
            this.inPPButton.TabIndex = 0;
            this.inPPButton.Text = "IN PP";
            this.inPPButton.UseVisualStyleBackColor = false;
            this.inPPButton.Click += new System.EventHandler(this.inPPButton_Click);
            // 
            // cylButton
            // 
            this.cylButton.BackColor = System.Drawing.Color.White;
            this.cylButton.Location = new System.Drawing.Point(262, 321);
            this.cylButton.Name = "cylButton";
            this.cylButton.Size = new System.Drawing.Size(247, 147);
            this.cylButton.TabIndex = 0;
            this.cylButton.Text = "CYLINDER";
            this.cylButton.UseVisualStyleBackColor = false;
            this.cylButton.Visible = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(527, 321);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(247, 147);
            this.button1.TabIndex = 0;
            this.button1.Text = "MOTION";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            // 
            // trayPPButton
            // 
            this.trayPPButton.BackColor = System.Drawing.Color.White;
            this.trayPPButton.Location = new System.Drawing.Point(389, 153);
            this.trayPPButton.Name = "trayPPButton";
            this.trayPPButton.Size = new System.Drawing.Size(247, 147);
            this.trayPPButton.TabIndex = 0;
            this.trayPPButton.Text = "TRAY PP";
            this.trayPPButton.UseVisualStyleBackColor = false;
            this.trayPPButton.Click += new System.EventHandler(this.trayPPButton_Click);
            // 
            // outPPButton
            // 
            this.outPPButton.BackColor = System.Drawing.Color.White;
            this.outPPButton.Location = new System.Drawing.Point(656, 153);
            this.outPPButton.Name = "outPPButton";
            this.outPPButton.Size = new System.Drawing.Size(247, 147);
            this.outPPButton.TabIndex = 0;
            this.outPPButton.Text = "OUT PP";
            this.outPPButton.UseVisualStyleBackColor = false;
            this.outPPButton.Click += new System.EventHandler(this.outPPButton_Click);
            // 
            // FormTeach
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 628);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cylButton);
            this.Controls.Add(this.outPPButton);
            this.Controls.Add(this.trayPPButton);
            this.Controls.Add(this.inPPButton);
            this.Font = new System.Drawing.Font("SamsungOne 800C", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormTeach";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Text = "FormCenter1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button inPPButton;
        private System.Windows.Forms.Button cylButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button trayPPButton;
        private System.Windows.Forms.Button outPPButton;
    }
}