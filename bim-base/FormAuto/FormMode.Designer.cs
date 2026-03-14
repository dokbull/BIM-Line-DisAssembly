namespace bim_base
{
    partial class FormMode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMode));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.autoButton = new CImageButton();
            this.dryButton = new CImageButton();
            this.byPassButton = new CImageButton();
            this.changeButton = new CImageButton();
            this.exitButton = new CImageButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.autoButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dryButton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.byPassButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.changeButton, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.exitButton, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(561, 136);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // autoButton
            // 
            this.autoButton._BACK_COLOR = System.Drawing.SystemColors.Control;
            this.autoButton._CHECKED = false;
            this.autoButton._CHECKED_BACK_COLOR = System.Drawing.Color.SkyBlue;
            this.autoButton._IMAGE = ((System.Drawing.Image)(resources.GetObject("autoButton._IMAGE")));
            this.autoButton._IMAGE_SIZE = new System.Drawing.Size(20, 20);
            this.autoButton._IMG_POS = CBUTTON_POS.Left;
            this.autoButton._MARGIN = new System.Windows.Forms.Padding(10, 0, 15, 0);
            this.autoButton._TEXT = "AUTO";
            this.autoButton._TEXT_ALIGN = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoButton.BackColor = System.Drawing.SystemColors.Control;
            this.autoButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.autoButton.Location = new System.Drawing.Point(5, 5);
            this.autoButton.Margin = new System.Windows.Forms.Padding(5);
            this.autoButton.Name = "autoButton";
            this.autoButton.Size = new System.Drawing.Size(177, 58);
            this.autoButton.TabIndex = 0;
            this.autoButton.UseVisualStyleBackColor = false;
            this.autoButton.Click += new System.EventHandler(this.autoButton_Click);
            // 
            // dryButton
            // 
            this.dryButton._BACK_COLOR = System.Drawing.SystemColors.Control;
            this.dryButton._CHECKED = false;
            this.dryButton._CHECKED_BACK_COLOR = System.Drawing.Color.SkyBlue;
            this.dryButton._IMAGE = ((System.Drawing.Image)(resources.GetObject("dryButton._IMAGE")));
            this.dryButton._IMAGE_SIZE = new System.Drawing.Size(20, 20);
            this.dryButton._IMG_POS = CBUTTON_POS.Left;
            this.dryButton._MARGIN = new System.Windows.Forms.Padding(10, 0, 15, 0);
            this.dryButton._TEXT = "DRY";
            this.dryButton._TEXT_ALIGN = System.Drawing.ContentAlignment.MiddleLeft;
            this.dryButton.BackColor = System.Drawing.SystemColors.Control;
            this.dryButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dryButton.Location = new System.Drawing.Point(379, 5);
            this.dryButton.Margin = new System.Windows.Forms.Padding(5);
            this.dryButton.Name = "dryButton";
            this.dryButton.Size = new System.Drawing.Size(177, 58);
            this.dryButton.TabIndex = 1;
            this.dryButton.UseVisualStyleBackColor = false;
            this.dryButton.Click += new System.EventHandler(this.dryButton_Click);
            // 
            // byPassButton
            // 
            this.byPassButton._BACK_COLOR = System.Drawing.SystemColors.Control;
            this.byPassButton._CHECKED = false;
            this.byPassButton._CHECKED_BACK_COLOR = System.Drawing.Color.Transparent;
            this.byPassButton._IMAGE = ((System.Drawing.Image)(resources.GetObject("byPassButton._IMAGE")));
            this.byPassButton._IMAGE_SIZE = new System.Drawing.Size(20, 20);
            this.byPassButton._IMG_POS = CBUTTON_POS.Left;
            this.byPassButton._MARGIN = new System.Windows.Forms.Padding(10, 0, 15, 0);
            this.byPassButton._TEXT = "BYPASS";
            this.byPassButton._TEXT_ALIGN = System.Drawing.ContentAlignment.MiddleLeft;
            this.byPassButton.BackColor = System.Drawing.SystemColors.Control;
            this.byPassButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.byPassButton.Location = new System.Drawing.Point(192, 5);
            this.byPassButton.Margin = new System.Windows.Forms.Padding(5);
            this.byPassButton.Name = "byPassButton";
            this.byPassButton.Size = new System.Drawing.Size(177, 58);
            this.byPassButton.TabIndex = 2;
            this.byPassButton.UseVisualStyleBackColor = false;
            this.byPassButton.Click += new System.EventHandler(this.byPassButton_Click);
            // 
            // changeButton
            // 
            this.changeButton._BACK_COLOR = System.Drawing.SystemColors.Control;
            this.changeButton._CHECKED = false;
            this.changeButton._CHECKED_BACK_COLOR = System.Drawing.Color.Transparent;
            this.changeButton._IMAGE = global::bim_base.Properties.Resources.Change;
            this.changeButton._IMAGE_SIZE = new System.Drawing.Size(20, 20);
            this.changeButton._IMG_POS = CBUTTON_POS.Left;
            this.changeButton._MARGIN = new System.Windows.Forms.Padding(10, 0, 15, 0);
            this.changeButton._TEXT = "CHANGE";
            this.changeButton._TEXT_ALIGN = System.Drawing.ContentAlignment.MiddleLeft;
            this.changeButton.BackColor = System.Drawing.SystemColors.Control;
            this.changeButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changeButton.Location = new System.Drawing.Point(5, 73);
            this.changeButton.Margin = new System.Windows.Forms.Padding(5);
            this.changeButton.Name = "changeButton";
            this.changeButton.Size = new System.Drawing.Size(177, 58);
            this.changeButton.TabIndex = 3;
            this.changeButton.UseVisualStyleBackColor = false;
            this.changeButton.Click += new System.EventHandler(this.changeButton_Click);
            // 
            // exitButton
            // 
            this.exitButton._BACK_COLOR = System.Drawing.SystemColors.Control;
            this.exitButton._CHECKED = false;
            this.exitButton._CHECKED_BACK_COLOR = System.Drawing.Color.Transparent;
            this.exitButton._IMAGE = global::bim_base.Properties.Resources.exit;
            this.exitButton._IMAGE_SIZE = new System.Drawing.Size(20, 20);
            this.exitButton._IMG_POS = CBUTTON_POS.Left;
            this.exitButton._MARGIN = new System.Windows.Forms.Padding(10, 0, 15, 0);
            this.exitButton._TEXT = "EXIT";
            this.exitButton._TEXT_ALIGN = System.Drawing.ContentAlignment.MiddleLeft;
            this.exitButton.BackColor = System.Drawing.SystemColors.Control;
            this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exitButton.Location = new System.Drawing.Point(379, 73);
            this.exitButton.Margin = new System.Windows.Forms.Padding(5);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(177, 58);
            this.exitButton.TabIndex = 5;
            this.exitButton.UseVisualStyleBackColor = false;
            // 
            // FormMode
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(581, 156);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("SamsungOne 800C", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMode";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Model Run";
            this.Load += new System.EventHandler(this.FormSubAuto_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CImageButton autoButton;
        private CImageButton dryButton;
        private CImageButton byPassButton;
        private CImageButton changeButton;
        private CImageButton exitButton;
    }
}