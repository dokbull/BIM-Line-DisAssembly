namespace bim_base
{
    partial class FormUnitInit
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
            this.ppButton = new System.Windows.Forms.Button();
            this.uiTimer = new System.Windows.Forms.Timer(this.components);
            this.selectAllButton = new System.Windows.Forms.Button();
            this.DeselectAllButton = new System.Windows.Forms.Button();
            this.initialButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ppButton
            // 
            this.ppButton.Location = new System.Drawing.Point(206, 26);
            this.ppButton.Name = "ppButton";
            this.ppButton.Size = new System.Drawing.Size(161, 100);
            this.ppButton.TabIndex = 1;
            this.ppButton.Text = "PP";
            this.ppButton.UseVisualStyleBackColor = true;
            this.ppButton.Click += new System.EventHandler(this.initPPButton_Click);
            // 
            // uiTimer
            // 
            this.uiTimer.Enabled = true;
            this.uiTimer.Tick += new System.EventHandler(this.uiTimer_Tick);
            // 
            // selectAllButton
            // 
            this.selectAllButton.Location = new System.Drawing.Point(12, 254);
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(125, 65);
            this.selectAllButton.TabIndex = 1;
            this.selectAllButton.Text = "Select All";
            this.selectAllButton.UseVisualStyleBackColor = true;
            this.selectAllButton.Click += new System.EventHandler(this.selectAllButton_Click);
            // 
            // DeselectAllButton
            // 
            this.DeselectAllButton.Location = new System.Drawing.Point(143, 254);
            this.DeselectAllButton.Name = "DeselectAllButton";
            this.DeselectAllButton.Size = new System.Drawing.Size(125, 65);
            this.DeselectAllButton.TabIndex = 1;
            this.DeselectAllButton.Text = "Deselect All";
            this.DeselectAllButton.UseVisualStyleBackColor = true;
            this.DeselectAllButton.Click += new System.EventHandler(this.DeselectAllButton_Click);
            // 
            // initialButton
            // 
            this.initialButton.Location = new System.Drawing.Point(445, 254);
            this.initialButton.Name = "initialButton";
            this.initialButton.Size = new System.Drawing.Size(125, 65);
            this.initialButton.TabIndex = 1;
            this.initialButton.Text = "Initial";
            this.initialButton.UseVisualStyleBackColor = true;
            this.initialButton.Click += new System.EventHandler(this.initialButton_Click);
            // 
            // FormUnitInit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(582, 331);
            this.Controls.Add(this.initialButton);
            this.Controls.Add(this.DeselectAllButton);
            this.Controls.Add(this.selectAllButton);
            this.Controls.Add(this.ppButton);
            this.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormUnitInit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormUnitInit";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ppButton;
        private System.Windows.Forms.Timer uiTimer;
        private System.Windows.Forms.Button selectAllButton;
        private System.Windows.Forms.Button DeselectAllButton;
        private System.Windows.Forms.Button initialButton;
    }
}