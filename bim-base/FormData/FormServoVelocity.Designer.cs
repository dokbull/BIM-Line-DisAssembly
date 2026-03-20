namespace bim_base
{
    partial class FormServoVelocity
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
            this.label5 = new System.Windows.Forms.Label();
            this.btnServoVelocityExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(369, 219);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 12);
            this.label5.TabIndex = 31;
            this.label5.Text = "GPT 바보!";
            // 
            // btnServoVelocityExit
            // 
            this.btnServoVelocityExit.Location = new System.Drawing.Point(859, 556);
            this.btnServoVelocityExit.Name = "btnServoVelocityExit";
            this.btnServoVelocityExit.Size = new System.Drawing.Size(138, 46);
            this.btnServoVelocityExit.TabIndex = 32;
            this.btnServoVelocityExit.Text = "Exit";
            this.btnServoVelocityExit.UseVisualStyleBackColor = true;
            this.btnServoVelocityExit.Click += new System.EventHandler(this.btnServoVelocityExit_Click);
            // 
            // FormServoVelocity
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1020, 624);
            this.Controls.Add(this.btnServoVelocityExit);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormServoVelocity";
            this.Text = "ServoVelocity";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnServoVelocityExit;
    }
}