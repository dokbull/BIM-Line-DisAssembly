#if USE_CSOURCEGRID
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SourceGrid;
using System.Drawing;
using System.Windows.Forms;

public class CCell : SourceGrid.Cells.Cell
{
    public void setValue(string text)
    {
        this.Value = text;
    }

    public void setValue(bool value)
    {
        this.Value = value ? "True" : "False";
    }
}

public class CViewCell : SourceGrid.Cells.Views.Cell
{ 
}


public class CSourceGrid : SourceGrid.Grid
{
    public void setRowCol(int row, int col, 
        bool rowAutoSize = false, bool colAutoSize = false, 
        bool rowScrollBar = false)
    {
        this.Redim(row, col);

        for (int r = 0; r < row; r++)
        {
            for (int c = 0; c < col; c++)
            {
                CCell cell = new CCell();
                CViewCell viewCell = new CViewCell();

                this[r, c] = cell;
                this[r, c].View = viewCell;
            }
        }

        int width = this.Width;
        int height = this.Height;

        if (rowScrollBar)
            width -= 10;

        if (rowAutoSize == true)
        {
            for (int r = 0; r < row; r++)
                this.Rows[r].Height = height / row;
        }

        if (colAutoSize == true)
        {
            for (int c = 0; c < col; c++)
                this.Columns[c].Width = width / col;
        }
    }

    public void addRow(int addCount)
    {
        int rowCount = this.Rows.Count;
        int colCount = this.Columns.Count;
        this.Rows.SetCount(rowCount + addCount);

        for (int r = 0; r < addCount; r++)
        {
            for (int c = 0; c < colCount; c++)
            {
                CCell cell = new CCell();
                CViewCell viewCell = new CViewCell();

                this[rowCount + r, c] = cell;
                this[rowCount + r, c].View = viewCell;
            }
        }
    }

    public void setRowSize(int size)
    {
        for (int r = 0; r < this.RowsCount; r++)
            this.Rows[r].Height = size;
    }

    public void setColSize(int col, int size)
    {
        this.Columns[col].Width = size;
    }

    public CCell cell(int r, int c)
    {
        return (CCell)this[r, c];
    }

    public CViewCell viewCell(int r, int c)
    {
        return (CViewCell)this[r, c].View;
    }

    public void setValueOKNG(int r, int c, bool value)
    {
        setValue(r, c, value == true ? "OK" : "NG");
    }

    public void setValue(int r, int c, string text)
    {
        if (cell(r, c) == null)
            return;

        if (cell(r, c).Value != null)
        {
            string agoValue = cell(r, c).Value.ToString();

            if (text != agoValue)
                cell(r, c).setValue(text);

            return;
        }

        cell(r, c).setValue(text);
    }

    public void setColors(int r, int c, Color backColor, Color foreColor)
    {
        setBackColor(r, c, backColor);
        setForeColor(r, c, foreColor);
    }

    public void setBackColor(int r, int c, Color color)
    {
        Color agoColor = viewCell(r, c).BackColor;

        if (agoColor != color)
            viewCell(r, c).BackColor = color;
    }

    public void setForeColor(int r, int c, Color color)
    {
        Color agoColor = viewCell(r, c).ForeColor;

        if (agoColor != color)
            viewCell(r, c).ForeColor = color;
    }

    public void setValueWithColor(int r, int c, bool value)
    {
        setValue(r, c, value == true ? "1" : "0");

        if (value)
            setBackColor(r, c, Color.Lime);
        else
            setBackColor(r, c, Color.White);
    }

    public void setValue(int r, int c, string text, Color backColor, Color foreColor)
    {
        setValue(r, c, text);
        setBackColor(r, c, backColor);
        setForeColor(r, c, foreColor);
    }

    public void setTextAlignment(int r, int c, DevAge.Drawing.ContentAlignment alignment)
    {
        viewCell(r, c).TextAlignment = alignment;
    }

    public void setTextAlignment(DevAge.Drawing.ContentAlignment alignment)
    {
        for (int r = 0; r < this.RowsCount; r++)
        {
            for (int c = 0; c < this.ColumnsCount; c++)
            {
                setTextAlignment(r, c, alignment);
            }
        }
    }

    public void setWordWrap(int r, int c, bool value)
    {
        viewCell(r, c).WordWrap = value;
    }

    public void setWordWrap(bool value)
    {
        for (int r = 0; r < this.RowsCount; r++)
        {
            for (int c = 0; c < this.ColumnsCount; c++)
            {
                setWordWrap(r, c, value);
            }
        }
    }

    public int selectedRow()
    {
        var selection = Selection;

        if (selection.IsEmpty())
            return -1;
         
        var position = selection.GetSelectionRegion().GetCellsPositions().FirstOrDefault();

        if (position == null)
            return -1;
            
        return position.Row;
    }

    public void selectPos(ref Point pt)
    {   
        PositionCollection positions = Selection.GetSelectionRegion().GetCellsPositions();
        if (positions.Count <= 0)
        {
            pt.X = -1;
            pt.Y = -1;
            return;
        }

        Position firstSelectedCell = positions[0];

        pt.X = firstSelectedCell.Column;
        pt.Y = firstSelectedCell.Row;
    }

}

public static class SourceGridExt
{
    /// <summary>
    /// header 지정
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="text">string array</param>
    public static void setHeader(this SourceGrid.Grid grid, string[] text)
    {
        for (int i=0; i<text.Length; i++)
        {
            if (i >= grid.ColumnsCount)
                break;

            grid[0, i].Value = text[i];
        }
    }

    public static void setHeader(this SourceGrid.Grid grid, List<string> list)
    {
        int idx = 0;

        foreach (string text in list)
        {
            if (idx >= grid.ColumnsCount)
                break;

            grid[0, idx].Value = text;
            idx++;
        }
    }

    public static void setHeaderColor(this SourceGrid.Grid grid, Color backColor, Color foreColor)
    {
        for (int i = 0; i < grid.ColumnsCount; i++)
        {
            grid[0, i].View.BackColor = backColor;
            grid[0, i].View.ForeColor = foreColor;
        }
    }

    public static void holdHeader(this SourceGrid.Grid grid, int count)
    {
        grid.FixedRows = count;
    }

    public static void setValue(this SourceGrid.Cells.ICell cell, string text)
    {
        cell.Value = text;
    }

    public static void setColor(this SourceGrid.Cells.Views.Cell cell, Color backColor, Color foreColot)
    {
    }


    public static bool setRowSpan(this SourceGrid.Grid grid, int row, int col, int count)
    {
        try
        {
            if (grid.Rows.Count < row + count)
            {
                Debug.debug("CSourceGrid::setRowSpan count error. set count:" + (row + count) + " row count:" + grid.Rows.Count);
                return false;
            }

            for (int r = 0; r < count; r++)
            {
                if (r == 0)
                    continue;

                grid[row + r, col] = null;
            }

            grid[row, col].RowSpan = count;

            return true;
        }
        catch (Exception e)
        {
            Debug.warning("CSourceGrid::setRowSpan exception message:" + e.Message);
            return false;
        }
    }

    public static bool setColSpan(this SourceGrid.Grid grid, int row, int col, int count)
    {
        try
        {
            if (grid.Columns.Count < col + count)
            {
                Debug.debug("CSourceGrid::setColSpan count error. set count:" + (col + count) + " col count:" + grid.Columns.Count);
                return false;
            }

            for (int c = 0; c < count; c++)
            {
                if (c == 0)
                    continue;

                grid[row, col + c] = null;
            }

            grid[row, col].ColumnSpan = count;

            return true;
        }
        catch (Exception e)
        {
            Debug.warning("CSourceGrid::setColSpan exception message:" + e.Message);
            return false;
        }
    }

    public static bool setRowColSpan(this SourceGrid.Grid grid, int row, int col, int rowCount, int colCount)
    {
        try
        {
            if (grid.Rows.Count < row + rowCount)
            {
                Debug.debug("CSourceGrid::setRowSpan count error. set count:" + (row + rowCount) + " row count:" + grid.Rows.Count);
                return false;
            }

            if (grid.Columns.Count < col + colCount)
            {
                Debug.debug("CSourceGrid::setColSpan count error. set count:" + (col + colCount) + " col count:" + grid.Columns.Count);
                return false;
            }

            for (int r = 0; r < rowCount; r++)
            {
                for (int c = 0; c < colCount; c++)
                {
                    if (r == 0 && c == 0)
                        continue;

                    grid[row + r, col + c] = null;
                }
            }

            grid[row, col].RowSpan = rowCount;
            grid[row, col].ColumnSpan = colCount;

            return true;
        }
        catch (Exception e)
        {
            Debug.warning("CSourceGrid::setColSpan exception message:" + e.Message);
            return false;
        }
    }
}
#endif
