using SourceGrid;
using SourceGrid.Cells.Models;
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
    public partial class FormTeachInPP : Form
    {
        ProcessMain main = null;
        ModelInfo m_model = null;

        public FormTeachInPP(ProcessMain procMain)
        {
            InitializeComponent();
            main = procMain;
            m_model = Common.MODEL_INFO(Conf.CURR_MODEL_IDX);

            List<ExtAxis> axisList = new List<ExtAxis>();
            axisList.Add(main.axis(AXIS.IN_PP_Y));
            axisList.Add(main.axis(AXIS.IN_PP_Z));

            jogControl1.setMain(main);
            jogControl1.setAxis(axisList);
        }

        private void FormTeachPP_Load(object sender, EventArgs e)
        {
            CSourceGrid g = motorPosGrid;

            g.Selection.EnableMultiSelection = false;

            g.setRowCol(5, 3, true, true);
            g.setTextAlignment(DevAge.Drawing.ContentAlignment.MiddleCenter);
            g.setHeader(new string[] { "", "PP Y [mm]", "PP Z [mm]" });
            g.setHeaderColor(Color.Black, Color.White);

            List<string> rowList = new List<string>();

            rowList.Add("Current");
            rowList.Add("WAIT");
            rowList.Add("PICK");
            rowList.Add("PLACE");

            int idx = 0;
            foreach (string text in rowList)
            {
                g.setValue(idx + 1, 0, text);
                g.setColors(idx + 1, 0, Color.Black, Color.White);
                idx++;
            }

            ui_timer.Enabled = true;
        }

        private List<POS> getPosData()
        {
            List<POS> posList = new List<POS>();
            POS WAIT = m_model.teachData(TEACH_POS.PICK_PP_WAIT);
            POS PICK = m_model.teachData(TEACH_POS.PICK_PP_PICK);
            POS PLACE = m_model.teachData(TEACH_POS.PICK_PP_PLACE);

            posList.Add(WAIT);
            posList.Add(PICK);
            posList.Add(PLACE);

            return posList;
        }

        void updateGird()
        {
            CSourceGrid g = motorPosGrid;

            double posY = main.axis(AXIS.IN_PP_Y).pos();
            double posZ = main.axis(AXIS.IN_PP_Z).pos();

            g.setValue(1, 1, posY.ToString("0.00"));
            g.setValue(1, 2, posZ.ToString("0.00"));
            g.setBackColor(1, 1, main.axis(AXIS.IN_PP_Y).isMoving() ? Color.Lime : Color.White);
            g.setBackColor(1, 2, main.axis(AXIS.IN_PP_Z).isMoving() ? Color.Lime : Color.White);

            List<POS> posList = getPosData();

            for (int i = 0; i < posList.Count; i++)
            {
                g.setValue(2 + i, 1, posList[i].y.ToString("0.00"));
                g.setValue(2 + i, 2, posList[i].z.ToString("0.00"));
            }
        }

        private void movePosition(TEACH_POS target, ACT gripAction)
        {
            POS pos = m_model.teachPos(target);

            double tarX = pos.x;
            double tarY = pos.y;
            double tarZ = pos.z;

            CMessageBox msgBox = new CMessageBox(Common.TITLE, "MOVE ?" +
                "\r\n" + "X:" + tarX.ToString("0.00") + " Y:" + tarY.ToString("0.00") + " Z:" + tarZ.ToString("0.00"), MessageBoxButtons.OKCancel);
            bool ret = msgBox.showDialog();

            if (ret == false)
                return;

            //main.procManualPP().start(target, gripAction);
        }

        private int getGridSelectPos()
        {
            int row = 0;
            PositionCollection positions = motorPosGrid.Selection.GetSelectionRegion().GetCellsPositions();
            if (positions.Count <= 0)
                return row;
             
            Position firstSelectedCell = positions[0];
            row = firstSelectedCell.Row;

            return row;
        }

        private bool checkIncludeEnum(TEACH_POS teachPos)
        {
            bool retn = false;
            Type enumType = typeof(TEACH_POS);
            MemberInfo[] memberInfos = enumType.GetMember(teachPos.ToString());
            if (memberInfos.Length > 0)
                retn = true;

            return retn;
        }

        private void xySave(int idx)
        {
            TEACH_POS teachPos = (TEACH_POS)(idx - 2);  //FIXME@JW header, current 라인 제외
            if (checkIncludeEnum(teachPos) == false)
                return;

            POS pos = m_model.teachData(teachPos);

            pos.x = 0.0d;
            pos.y = Util.toDouble(motorPosGrid.cell(1, 1).Value.ToString());

            m_model.saveTeachPos(pos);
        }

        private void zSave(int idx)
        {
            TEACH_POS teachPos = (TEACH_POS)(idx - 2);  //FIXME@JW header, current 라인 제외
            if (checkIncludeEnum(teachPos) == false)
                return;

            POS pos = m_model.teachData(teachPos);

            pos.z = Util.toDouble(motorPosGrid.cell(1, 2).Value.ToString());

            m_model.saveTeachPos(pos);
        }

        private void xySaveButton_Click(object sender, EventArgs e)
        {
            int row = getGridSelectPos();

            if (row <= 0)
                return;

            CMessageBox msgBox = new CMessageBox(Common.TITLE, "X/Y Save ?", MessageBoxButtons.OKCancel);
            if (msgBox.showDialog() == false)
                return;

            xySave(row);
        }

        private void zSaveButton_Click(object sender, EventArgs e)
        {
            int row = getGridSelectPos();

            if (row <= 0)
                return;

            CMessageBox msgBox = new CMessageBox(Common.TITLE, "Z Save ?", MessageBoxButtons.OKCancel);
            if (msgBox.showDialog() == false)
                return;

            zSave(row);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            int row = getGridSelectPos();

            if (row <= 0)
                return;
            CMessageBox msgBox = new CMessageBox(Common.TITLE, "Save ?", MessageBoxButtons.OKCancel);
            if (msgBox.showDialog() == false)
                return;

            xySave(row);
            zSave(row);
        }

        private void moveButton_Click(object sender, EventArgs e)
        {
            int row = getGridSelectPos();

            if (row < 2)
                return;

            TEACH_POS teachPos = (TEACH_POS)(row - 2);  //FIXME@JW header, current 라인 제외
            if (checkIncludeEnum(teachPos) == false)
                return;

            movePosition(teachPos, ACT.WAIT);
        }

        private void zUpButton_Click(object sender, EventArgs e)
        {

        }

        private void zDownButton_Click(object sender, EventArgs e)
        {
            int row = getGridSelectPos();

            if (row < 2)
                return;

            TEACH_POS teachPos = (TEACH_POS)(row - 2);  //FIXME@JW header, current 라인 제외
            if (checkIncludeEnum(teachPos) == false)
                return;

            POS pos = m_model.teachPos(teachPos);
        }

        private void ui_timer_Tick(object sender, EventArgs e)
        {
            if (main == null)
                return;

            updateGird();
        }

        private void gripOnButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to GRIP ON ?");

            if (ret == false)
                return;

            //main.procManualPP().setGripOn();
        }

        private void gripOffButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to GRIP OFF ?");

            if (ret == false)
                return;

            //main.procManualPP().setGripOff();
        }
    }
}
