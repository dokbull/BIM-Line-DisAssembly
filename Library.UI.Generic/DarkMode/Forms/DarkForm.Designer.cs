namespace Lib.UI.Generic.DarkMode.Forms
{
    partial class DarkForm
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
            this.MainPanel = new Lib.UI.Generic.DarkMode.Controls.DarkPanel();
            this.Title = new Lib.UI.Generic.DarkMode.Controls.DarkFormTitle();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.MainPanel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MainPanel.DarkLevel = 20;
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.MainPanel.Location = new System.Drawing.Point(5, 51);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(790, 394);
            this.MainPanel.TabIndex = 1;
            this.MainPanel.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.DarkMainPanel_ControlAdded);
            // 
            // Title
            // 
            this.Title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Title.ButtonText1 = "Button1";
            this.Title.ButtonText2 = "Button2";
            this.Title.ButtonVisible1 = false;
            this.Title.ButtonVisible2 = false;
            this.Title.CloseBox = true;
            this.Title.ControlBoxVisible = true;
            this.Title.DarkLevel = 30;
            this.Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Title.IconImage = null;
            this.Title.LabelText = "Dark Form";
            this.Title.LabelVisible = false;
            this.Title.Location = new System.Drawing.Point(5, 5);
            this.Title.MaximumBox = true;
            this.Title.MinimumBox = true;
            this.Title.Name = "Title";
            this.Title.OptionBox = true;
            this.Title.OptionBoxAlignment = Lib.UI.Generic.DarkMode.Controls.EnumTitleOptionBoxAlignment.Left;
            this.Title.ShowIcon = false;
            this.Title.Size = new System.Drawing.Size(790, 46);
            this.Title.TabIndex = 0;
            this.Title.TitleText = "Dark Form";
            // 
            // DarkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.Title);
            this.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DarkForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "DarkForm";
            this.Load += new System.EventHandler(this.DarkForm_Load);
            this.Move += new System.EventHandler(this.DarkForm_Move);
            this.ResumeLayout(false);

        }

        #endregion
        protected Controls.DarkPanel MainPanel;
        private Controls.DarkFormTitle Title;
    }
}