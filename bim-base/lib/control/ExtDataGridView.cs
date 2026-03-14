using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

class ExtDataGridView : DataGridView
{
    public string m_autoSaveName = "";
    public string m_path = "";

    public bool AutoResizeColumnHeader
    {
        get;
        set;
    }

    public bool UseHorizontalScrollBar
    {
        get;
        set;
    }

    public bool isSort
    {
        get;
        set;
    }

    public bool _COLUMN_NUMPAD
    {
        get;
        set;
    }

    public bool _AUTO_SAVE
    {
        get;
        set;
    }

    public string _AUTO_SAVE_NAME
    {
        get { return m_autoSaveName; }
        set { m_autoSaveName = value; }
    }

    CFileManager m_fileManager = null;

    public ExtDataGridView()
    {
        DoubleBuffered = true;

        AllowUserToAddRows = false;
        AllowUserToDeleteRows = false;
        AllowUserToResizeColumns = false;
        AllowUserToResizeRows = false;

        SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        MultiSelect = false;
        ReadOnly = true;

        RowHeadersVisible = false;

        this.Resize += new EventHandler(ExtDataGridView_Resize);
    }

    private void ExtDataGridView_Resize(Object sender, EventArgs e)
    {
        int scrollBarGap = 0;

        if (UseHorizontalScrollBar)
            scrollBarGap = 25;

        if (AutoResizeColumnHeader)
        {
            foreach (DataGridViewColumn col in this.Columns)
                col.Width = (Width - scrollBarGap) / this.ColumnCount-1;
        }

        setColumnHeaderStyle(DataGridViewContentAlignment.MiddleCenter);
    }

    public void setColumnHeaderStyle(DataGridViewContentAlignment alignment)
    {
        foreach (DataGridViewColumn col in this.Columns)
        {
            if (isSort == false)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            else
                col.SortMode = DataGridViewColumnSortMode.Automatic;

            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
    }

    private void InitializeComponent()
    {
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        this.SuspendLayout();
        // 
        // ExtDataGridView
        // 
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
        dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
        dataGridViewCellStyle2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
        dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        this.DefaultCellStyle = dataGridViewCellStyle2;
        this.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
        dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
        dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
        this.RowTemplate.Height = 23;
        this.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.ExtDataGridView_CellBeginEdit);
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        this.ResumeLayout(false);
    }

    public void MoveUp()
    {
        if (RowCount <= 0)
            return;

        if (SelectedRows.Count <= 0)
            return;

        var index = SelectedCells[0].OwningRow.Index;

        if (index == 0)
            return;

        var rows = Rows;
        var prevRow = rows[index - 1];
        rows.Remove(prevRow);
        prevRow.Frozen = false;
        rows.Insert(index, prevRow);
        ClearSelection();
        Rows[index - 1].Selected = true;
    }

    public void MoveDown()
    {
        if (RowCount <= 0)
            return;

        if (SelectedRows.Count <= 0)
            return;

        var rowCount = Rows.Count;
        var index = SelectedCells[0].OwningRow.Index;

        if (index == rowCount - 1) // Here used 1 instead of 2
            return;

        var rows = Rows;
        var nextRow = rows[index + 1];
        rows.Remove(nextRow);
        nextRow.Frozen = false;
        rows.Insert(index, nextRow);
        ClearSelection();
        Rows[index + 1].Selected = true;
    }

    public void setEnable(int row, int col, bool enabled)
    {
        if (row > Rows.Count - 1)
            return;

        if (col > ColumnCount - 1)
            return;

        DataGridViewCell cell = Rows[row].Cells[col];

        if (cell == null)
            return;

        cell.ReadOnly = !enabled;

        if (enabled)
        {
            cell.Style.BackColor = cell.OwningColumn.DefaultCellStyle.BackColor;
            cell.Style.ForeColor = cell.OwningColumn.DefaultCellStyle.ForeColor;
        }
        else
        {
            cell.Style.BackColor = Color.LightGray;
            cell.Style.ForeColor = Color.WhiteSmoke;
        }
    }

    private void ExtDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
    {
        if (_COLUMN_NUMPAD == false)
            return;

        int r = e.RowIndex;
        int c = e.ColumnIndex;

        if (c == 0)
            return;

        ExtDataGridView gridView = (ExtDataGridView)sender;

        string value = gridView[c, r].Value.ToString();

        CNumberKeyPad keypad = new CNumberKeyPad(
            CNumberKeyPad.Style.Style_Unknwon, "Input");

        keypad.setText(value);
        keypad.StartPosition = FormStartPosition.CenterParent;

        DialogResult result = keypad.ShowDialog();

        if (result != DialogResult.OK)
        {
            e.Cancel = true;
            return;
        }

        string value2 = keypad.text();
        gridView[c, r].Style.BackColor = Color.LightSkyBlue;

        if (value == value2)
        {
            gridView[c, r].Style.BackColor = Color.White;
            return;
        }

        double val = Util.toDouble(value2, 0.0d);
        gridView[c, r].Value = val.ToString("0.000");
        e.Cancel = true;
    }

    protected override void OnCreateControl()
    {
        this.DoubleBuffered = true;
        base.OnCreateControl();

        if (m_autoSaveName == "")
            return;

        m_path = Common.PATH + "\\AutoSave\\ExtDataGridView\\" + m_autoSaveName + ".log";
        m_fileManager = new CFileManager(m_path);

        List<string[]> list = m_fileManager.readAll(',');

        if (list == null)
            return;

        this.SuspendLayout();

        foreach (string[] texts in list)
        {
            this.Rows.Add(texts);
        }

        this.ResumeLayout();
    }

    protected override void Dispose(bool disposing)
    {
        if (m_fileManager != null)
        {
            List<string> list = new List<string>();

            foreach (DataGridViewRow row in this.Rows)
            {
                string text = "";

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null)
                    {
                        text += cell.Value.ToString();
                    }

                    if (cell.ColumnIndex < row.Cells.Count - 1)
                    {
                        text += ",";
                    }
                }

                list.Add(text);
            }

            m_fileManager.writeAll(list, false);
        }

        base.Dispose(disposing);
    }
}
