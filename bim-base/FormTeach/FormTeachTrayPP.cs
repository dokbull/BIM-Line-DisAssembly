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
    public partial class FormTeachTrayPP : Form
    {
        ProcessMain main = null;
        ModelInfo m_model = null;

        public FormTeachTrayPP(ProcessMain procMain)
        {
            InitializeComponent();
            main = procMain;
            m_model = Common.MODEL_INFO;

            List<ExtAxis> axisList = new List<ExtAxis>();
            axisList.Add(main.axis(AXIS.MOLD_PP_X));
            axisList.Add(main.axis(AXIS.BASE_X));
            axisList.Add(main.axis(AXIS.MOLD_PP_ZL));
            axisList.Add(main.axis(AXIS.MOLD_PP_ZR));

            jogControl1.setMain(main);
            jogControl1.setAxis(axisList);
        }

        private void FormTeachPP_Load(object sender, EventArgs e)
        {
            CSourceGrid g = motorPosGrid;

            g.Selection.EnableMultiSelection = false;

            g.setRowCol(5, 5, true, true);
            g.setTextAlignment(DevAge.Drawing.ContentAlignment.MiddleCenter);
            g.setHeader(new string[] { "", "PP X [mm]", "BASE X [mm]", "PP Y [mm]", "PP Z [mm]" });
            g.setHeaderColor(Color.Black, Color.White);

            List<string> rowList = new List<string>();

            rowList.Add("Current");
            rowList.Add("WAIT");
            rowList.Add("LEFT");
            rowList.Add("RIGHT");

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
            POS WAIT = m_model.teachData(TEACH_POS.MOLD_PP_WAIT);
            POS LEFT = m_model.teachData(TEACH_POS.MOLD_PP_LEFT);
            POS RIGHT = m_model.teachData(TEACH_POS.MOLD_PP_RIGHT);

            posList.Add(WAIT);
            posList.Add(LEFT);
            posList.Add(RIGHT);

            return posList;
        }

        void updateGird()
        {
            CSourceGrid g = motorPosGrid;

            double posX1 = main.axis(AXIS.MOLD_PP_X).pos();
            double posX2 = main.axis(AXIS.BASE_X).pos();
            double posZ1 = main.axis(AXIS.MOLD_PP_ZL).pos();
            double posZ2 = main.axis(AXIS.MOLD_PP_ZR).pos();

            g.setValue(1, 1, posX1.ToString("0.00"));
            g.setValue(1, 2, posX2.ToString("0.00"));
            g.setValue(1, 3, posZ1.ToString("0.00"));
            g.setValue(1, 4, posZ2.ToString("0.00"));
            g.setBackColor(1, 1, main.axis(AXIS.MOLD_PP_X).isMoving() ? Color.Lime : Color.White);
            g.setBackColor(1, 2, main.axis(AXIS.BASE_X).isMoving() ? Color.Lime : Color.White);
            g.setBackColor(1, 3, main.axis(AXIS.MOLD_PP_ZL).isMoving() ? Color.Lime : Color.White);
            g.setBackColor(1, 4, main.axis(AXIS.MOLD_PP_ZR).isMoving() ? Color.Lime : Color.White);

            List<POS> posList = getPosData();

            for (int i = 0; i < posList.Count; i++)
            {
                g.setValue(2 + i, 1, posList[i].x.ToString("0.00"));
                g.setValue(2 + i, 2, posList[i].xB.ToString("0.00"));
                g.setValue(2 + i, 3, posList[i].zL.ToString("0.00"));
                g.setValue(2 + i, 4, posList[i].zR.ToString("0.00"));
            }
        }

        private void movePosition(TEACH_POS target, ACT gripAction)
        {
            POS pos = m_model.teachPos(target);

            double tarX = pos.x;
            double tarY = pos.y;
            double tarZ = pos.z;
#if false
            if (tarX < 0) tarX = 0.0d;
            if (tarY < 0) tarY = 0.0d;
            if (tarZ < 0) tarZ = 0.0d;
#endif
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
        private void xSave(int idx)
        {
            TEACH_POS teachPos = (TEACH_POS.MOLD_PP_WAIT + idx - 2);  //FIXME@JW header, current 라인 제외
            if (checkIncludeEnum(teachPos) == false)
                return;

            POS pos = m_model.teachData(teachPos);

            pos.x = Util.toDouble(motorPosGrid.cell(1, 1).Value.ToString());
            
            m_model.saveTeachPos(pos);
        }

        private void xbSave(int idx)
        {
            TEACH_POS teachPos = (TEACH_POS.MOLD_PP_WAIT + idx - 2);  //FIXME@JW header, current 라인 제외
            if (checkIncludeEnum(teachPos) == false)
                return;

            POS pos = m_model.teachData(teachPos);

            pos.xB = Util.toDouble(motorPosGrid.cell(1, 2).Value.ToString());

            m_model.saveTeachPos(pos);
        }

        private void z1Save(int idx)
        {
            TEACH_POS teachPos = (TEACH_POS.MOLD_PP_WAIT + idx - 2);  //FIXME@JW header, current 라인 제외
            if (checkIncludeEnum(teachPos) == false)
                return;

            POS pos = m_model.teachData(teachPos);

            pos.zL = Util.toDouble(motorPosGrid.cell(1, 3).Value.ToString());
            
            m_model.saveTeachPos(pos);
        }

        private void z2Save(int idx)
        {
            TEACH_POS teachPos = (TEACH_POS.MOLD_PP_WAIT + idx - 2);  //FIXME@JW header, current 라인 제외
            if (checkIncludeEnum(teachPos) == false)
                return;

            POS pos = m_model.teachData(teachPos);

            pos.zR = Util.toDouble(motorPosGrid.cell(1, 4).Value.ToString());

            m_model.saveTeachPos(pos);
        }

        private void xySaveButton_Click(object sender, EventArgs e)
        {
            int row = getGridSelectPos();

            if (row <= 0)
                return;

            CMessageBox msgBox = new CMessageBox(Common.TITLE, "X Save ?", MessageBoxButtons.OKCancel);
            if (msgBox.showDialog() == false)
                return;

            xSave(row);
        }

        private void xbSaveButton_Click(object sender, EventArgs e)
        {
            int row = getGridSelectPos();

            if (row <= 0)
                return;

            CMessageBox msgBox = new CMessageBox(Common.TITLE, "BASE X Save ?", MessageBoxButtons.OKCancel);
            if (msgBox.showDialog() == false)
                return;

            xbSave(row);
        }

        private void z1SaveButton_Click(object sender, EventArgs e)
        {
            int row = getGridSelectPos();

            if (row <= 0)
                return;
            CMessageBox msgBox = new CMessageBox(Common.TITLE, "Z1 Save ?", MessageBoxButtons.OKCancel);
            if (msgBox.showDialog() == false)
                return;

            z1Save(row);
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

        private void z2SaveButton_Click(object sender, EventArgs e)
        {
            int row = getGridSelectPos();

            if (row <= 0)
                return;
            CMessageBox msgBox = new CMessageBox(Common.TITLE, "Z2 Save ?", MessageBoxButtons.OKCancel);
            if (msgBox.showDialog() == false)
                return;

            z2Save(row);
        }
    }
}
