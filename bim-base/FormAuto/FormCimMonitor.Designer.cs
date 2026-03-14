namespace bim_base
{
    partial class FormCimMonitor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ui_timer = new System.Windows.Forms.Timer(this.components);
            this.readDropDown = new System.Windows.Forms.ComboBox();
            this.writeDropDown = new System.Windows.Forms.ComboBox();
            this.setTextBox = new System.Windows.Forms.TextBox();
            this.setButton = new System.Windows.Forms.Button();
            this.readLabel = new System.Windows.Forms.Label();
            this.writeLabel = new System.Windows.Forms.Label();
            this.gridView = new ExtDataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exitButton = new CImageButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ui_timer
            // 
            this.ui_timer.Enabled = true;
            this.ui_timer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // readDropDown
            // 
            this.readDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.readDropDown.FormattingEnabled = true;
            this.readDropDown.Location = new System.Drawing.Point(13, 501);
            this.readDropDown.Name = "readDropDown";
            this.readDropDown.Size = new System.Drawing.Size(422, 28);
            this.readDropDown.TabIndex = 4;
            // 
            // writeDropDown
            // 
            this.writeDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.writeDropDown.FormattingEnabled = true;
            this.writeDropDown.Location = new System.Drawing.Point(13, 536);
            this.writeDropDown.Name = "writeDropDown";
            this.writeDropDown.Size = new System.Drawing.Size(422, 28);
            this.writeDropDown.TabIndex = 4;
            // 
            // setTextBox
            // 
            this.setTextBox.Location = new System.Drawing.Point(13, 571);
            this.setTextBox.Name = "setTextBox";
            this.setTextBox.Size = new System.Drawing.Size(422, 26);
            this.setTextBox.TabIndex = 5;
            // 
            // setButton
            // 
            this.setButton.Location = new System.Drawing.Point(441, 573);
            this.setButton.Name = "setButton";
            this.setButton.Size = new System.Drawing.Size(75, 27);
            this.setButton.TabIndex = 6;
            this.setButton.Text = "SET";
            this.setButton.UseVisualStyleBackColor = true;
            this.setButton.Click += new System.EventHandler(this.setButton_Click);
            // 
            // readLabel
            // 
            this.readLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.readLabel.Location = new System.Drawing.Point(441, 501);
            this.readLabel.Name = "readLabel";
            this.readLabel.Size = new System.Drawing.Size(502, 29);
            this.readLabel.TabIndex = 7;
            this.readLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // writeLabel
            // 
            this.writeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.writeLabel.Location = new System.Drawing.Point(441, 536);
            this.writeLabel.Name = "writeLabel";
            this.writeLabel.Size = new System.Drawing.Size(502, 29);
            this.writeLabel.TabIndex = 7;
            this.writeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gridView
            // 
            this.gridView._AUTO_SAVE = false;
            this.gridView._AUTO_SAVE_NAME = "";
            this.gridView._COLUMN_NUMPAD = false;
            this.gridView.AllowUserToAddRows = false;
            this.gridView.AllowUserToDeleteRows = false;
            this.gridView.AllowUserToResizeColumns = false;
            this.gridView.AllowUserToResizeRows = false;
            this.gridView.AutoResizeColumnHeader = false;
            this.gridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.gridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridView.isSort = false;
            this.gridView.Location = new System.Drawing.Point(0, 0);
            this.gridView.MultiSelect = false;
            this.gridView.Name = "gridView";
            this.gridView.ReadOnly = true;
            this.gridView.RowHeadersVisible = false;
            this.gridView.RowTemplate.Height = 23;
            this.gridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridView.Size = new System.Drawing.Size(943, 495);
            this.gridView.TabIndex = 3;
            this.gridView.UseHorizontalScrollBar = false;
            this.gridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridView_CellContentClick);
            this.gridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridView_CellContentDoubleClick);
            // 
            // Column2
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column2.HeaderText = "입력 값";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column3.HeaderText = "설명";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 350;
            // 
            // Column4
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column4.HeaderText = "출력 값";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column5
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column5.HeaderText = "설명";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Width = 350;
            // 
            // exitButton
            // 
            this.exitButton._BACK_COLOR = System.Drawing.SystemColors.Control;
            this.exitButton._CHECKED = false;
            this.exitButton._CHECKED_BACK_COLOR = System.Drawing.Color.Transparent;
            this.exitButton._IMAGE = null;
            this.exitButton._IMAGE_SIZE = new System.Drawing.Size(20, 20);
            this.exitButton._IMG_POS = CBUTTON_POS.Left;
            this.exitButton._MARGIN = new System.Windows.Forms.Padding(7, 0, -15, 0);
            this.exitButton._TEXT = "EXIT";
            this.exitButton.BackColor = System.Drawing.SystemColors.Control;
            this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitButton.Location = new System.Drawing.Point(791, 571);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(152, 38);
            this.exitButton.TabIndex = 0;
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // FormSimMonitor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(944, 608);
            this.ControlBox = false;
            this.Controls.Add(this.writeLabel);
            this.Controls.Add(this.readLabel);
            this.Controls.Add(this.setButton);
            this.Controls.Add(this.setTextBox);
            this.Controls.Add(this.writeDropDown);
            this.Controls.Add(this.readDropDown);
            this.Controls.Add(this.gridView);
            this.Controls.Add(this.exitButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSimMonitor";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IO Monitor";
            this.Load += new System.EventHandler(this.FormSimMonitor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer ui_timer;
        private CImageButton exitButton;
        private ExtDataGridView gridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.ComboBox readDropDown;
        private System.Windows.Forms.ComboBox writeDropDown;
        private System.Windows.Forms.TextBox setTextBox;
        private System.Windows.Forms.Button setButton;
        private System.Windows.Forms.Label readLabel;
        private System.Windows.Forms.Label writeLabel;
    }
}