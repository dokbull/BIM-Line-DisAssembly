using SourceGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormIOMonitor : Form
    {
        int m_inputStartIdx = 0;
        int m_outputStartIdx = 0;

        int m_pageViewCnt = 16;

        ProcessMain main = null;

        public FormIOMonitor(ProcessMain procMain)
        {
            InitializeComponent();

            main = procMain;
            init();

            setInputGrid();
            setOutputGrid();
        }

        private void init()
        {
            inputGrid.setRowCol(9, 2, true);
            outputGrid.setRowCol(9, 2, true);

            inputGrid.setHeaderColor(Color.PaleTurquoise, Color.Black);
            outputGrid.setHeaderColor(Color.DarkKhaki, Color.Black);

            for (int i = 0; i < inputGrid.ColumnsCount; i++)
            {
                inputGrid.Columns[i].Width = inputGrid.Width/2;
            }
            for (int i = 0; i < outputGrid.ColumnsCount; i++)
            {
                outputGrid.Columns[i].Width = outputGrid.Width/2;
            }

            for (int i = 0; i < inputGrid.ColumnsCount; i++)
            {
                inputGrid.setTextAlignment(0, i, DevAge.Drawing.ContentAlignment.MiddleCenter); //Header는 Text align center
                for (int j = 1; j < inputGrid.RowsCount; j++)
                {
                    inputGrid.setTextAlignment(j, i, DevAge.Drawing.ContentAlignment.MiddleLeft);
                }
            }
            for (int i = 0; i < outputGrid.ColumnsCount; i++)
            {
                outputGrid.setTextAlignment(0, i, DevAge.Drawing.ContentAlignment.MiddleCenter); //Header는 Text align center
                for (int j = 1; j < outputGrid.RowsCount; j++)
                {
                    outputGrid.setTextAlignment(j, i, DevAge.Drawing.ContentAlignment.MiddleLeft);

                    var controller = new CellClickController();
                    controller.CellClicked += OnOutputGridCell_Clicked;
                    outputGrid.cell(j, i).AddController(controller);
                }
            }
        }

        private void setInputGrid()
        {
            string[] inputHeader = new string[]
            {
                inputAddr('X', m_inputStartIdx,     m_inputStartIdx + 7),
                inputAddr('X', m_inputStartIdx + 8, m_inputStartIdx + 15)
            };

            inputGrid.setHeader(inputHeader);

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int index = m_inputStartIdx + (i * 8) + j;

                    inputGrid.setValue(
                        j + 1,
                        i,
                        $"{findInputAddress('X', index)}:\r\n{convertEnum((INPUT)index)}"
                    );
                }
            }
        }

        private void setOutputGrid()
        {
            string[] outputHeader = new string[]
            {
                outputAddr('Y', m_outputStartIdx,     m_outputStartIdx + 7),
                outputAddr('Y', m_outputStartIdx + 8, m_outputStartIdx + 15)
            };
            outputGrid.setHeader(outputHeader);

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int index = m_outputStartIdx + (i * 8) + j;

                    outputGrid.setValue(
                        j + 1,
                        i,
                        $"{findOutputAddress('Y', index)}:\r\n{convertEnum((OUTPUT)index)}"
                    );
                }
            }
        }

        string convertEnum(INPUT input)
        {
            string ret = input.ToString();

            if (ret.Contains("_") == false)
                ret = "";

            return ret;
        }

        string convertEnum(OUTPUT output)
        {
            string ret = output.ToString();

            if (ret.Contains("_") == false)
                ret = "";

            return ret;
        }

        string findInputAddress(char type, int idx)
        {
            int number = getInputNumber(idx);
            return $"{type}{number:X3}";
        }

        string findOutputAddress(char type, int idx)
        {
            int number = getOutputNumber(idx);
            return $"{type}{number:X3}";
        }

        string inputAddr(char type, int startIdx, int endIdx)
        {
            return $"{findInputAddress(type, startIdx)} ~ {findInputAddress(type, endIdx)}";
        }

        string outputAddr(char type, int startIdx, int endIdx)
        {
            return $"{findOutputAddress(type, startIdx)} ~ {findOutputAddress(type, endIdx)}";
        }

        private int getInputNumber(int idx)
        {
            const int GapStart1 = 0x0A0; // 160
            const int GapSize1 = 0x060; // 96 (0x0A0~0x0FF)

            if (idx >= GapStart1)
                return idx + GapSize1;

            return idx;
        }

        private int getOutputNumber(int idx)
        {
            const int GapStart0 = 0x020; // Y020
            const int GapSize0 = 0x010; // 16 (0x020~0x02F)

            const int GapStart1 = 0x0A0; // 160
            const int GapSize1 = 0x060; // 96 (0x0A0~0x0FF)

            if (idx >= GapStart1)
                return idx + GapSize0 + GapSize1;

            if (idx >= GapStart0)
                return idx + GapSize0;

            return idx;
        }

        private void loadInput()
        {
            for (int i=0; i<2; i++)
            {
                for (int j=0; j<8; j++)
                {
                    int index = m_inputStartIdx + (i * 8) + j;
                    inputGrid.setBackColor(j + 1, i, input(index) ? Color.Lime : Color.White);
                }
            }
        }

        private void loadOutput()
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int index = m_outputStartIdx + (i * 8) + j;
                    outputGrid.setBackColor(j + 1, i, output(index) ? Color.Lime : Color.White);
                }
            }
        }

        private void OnOutputGridCell_Clicked(object sender, DataGridViewCellEventArgs e)
        {
            if (main.isAuto() == true)
                return;

            int index = m_outputStartIdx + (e.ColumnIndex * 8) + e.RowIndex - 1;
            bool value = main.output(index);

            setOutput((OUTPUT)index, !value);
        }

        private void inputFirstButton_Click(object sender, EventArgs e)
        {
            m_inputStartIdx = 0;

            setInputGrid();
        }

        private void inputPrevButton_Click(object sender, EventArgs e)
        {
            m_inputStartIdx -= m_pageViewCnt;
            if (m_inputStartIdx < 0)
                m_inputStartIdx = 0;

            setInputGrid();
        }

        private void inputNextButton_Click(object sender, EventArgs e)
        {
            m_inputStartIdx += m_pageViewCnt;
            if (m_inputStartIdx >= 16 * 14)
                m_inputStartIdx = 16 * 13;

            setInputGrid();
        }

        private void inputLastButton_Click(object sender, EventArgs e)
        {
            m_inputStartIdx = 16 * 13;
            setInputGrid();
        }

        private void outputFirstButton_Click(object sender, EventArgs e)
        {
            m_outputStartIdx = 0;

            setOutputGrid();
        }

        private void outputPrevButton_Click(object sender, EventArgs e)
        {
            m_outputStartIdx -= m_pageViewCnt;
            if (m_outputStartIdx < 0)
                m_outputStartIdx = 0;

            setOutputGrid();
        }

        private void outputNextButton_Click(object sender, EventArgs e)
        {
            m_outputStartIdx += m_pageViewCnt;
            if (m_outputStartIdx >= 16 * 13)
                m_outputStartIdx = 16 * 12;
            setOutputGrid();
        }

        private void outputLastButton_Click(object sender, EventArgs e)
        {
            m_outputStartIdx = 16 * 12;
            setOutputGrid();
        }

        void setOutput(OUTPUT output, bool value)
        {
            main.setOutputForce(output, value);
        }

        bool output(int index)
        {
            return output((OUTPUT)index);
        }

        bool output(OUTPUT output)
        {
            return main.output(output);
        }

        bool input(int index)
        {
            return input((INPUT)index);
        }

        bool input(INPUT input)
        {
            return main.input(input);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            loadInput();
            loadOutput();
            Refresh();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            main.setManualTestIO(false);
        }
    }

    // 셀 클릭 컨트롤러 정의
    public class CellClickController : SourceGrid.Cells.Controllers.ControllerBase
    {
        public event EventHandler<DataGridViewCellEventArgs> CellClicked;
        public event EventHandler<DataGridViewCellEventArgs> CellDoubleClicked;

        public override void OnClick(CellContext sender, EventArgs e)
        {
            base.OnClick(sender, e);
            var mouseEvent = e as MouseEventArgs;
            if (mouseEvent != null)
            {
                int row = sender.Position.Row;
                int col = sender.Position.Column;
                CellClicked?.Invoke(this, new DataGridViewCellEventArgs(col, row));
            }
        }

        public override void OnDoubleClick(CellContext sender, EventArgs e)
        {
            base.OnDoubleClick(sender, e);
            var mouseEvent = e as MouseEventArgs;
            if (mouseEvent != null)
            {
                int row = sender.Position.Row;
                int col = sender.Position.Column;
                CellDoubleClicked?.Invoke(this, new DataGridViewCellEventArgs(col, row));
            }
        }
    }
}
