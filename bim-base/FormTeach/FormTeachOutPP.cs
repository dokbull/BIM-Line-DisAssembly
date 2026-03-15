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
    public partial class FormTeachOutPP : Form
    {
        ProcessMain main = null;
        ModelInfo m_mc = null;
        ModelInfo m_model = null;

        public FormTeachOutPP(ProcessMain procMain)
        {
            InitializeComponent();
            main = procMain;
            m_mc = Common.MC_MODEL();
            m_model = Common.MODEL_INFO(Conf.CURR_MODEL_IDX);

            List<ExtAxis> axisList = new List<ExtAxis>();
            axisList.Add(main.axis(AXIS.UB_PP_Y));
            axisList.Add(main.axis(AXIS.UB_PP_Z));

            jogControl1.setMain(main);
            jogControl1.setAxis(axisList);
        }

        private void FormTeachPP_Load(object sender, EventArgs e)
        {
            CSourceGrid g = motorPosGrid;

            g.Selection.EnableMultiSelection = false;

            g.setRowCol(6, 5, true, true);
            g.setTextAlignment(DevAge.Drawing.ContentAlignment.MiddleCenter);
            g.setHeader(new string[] { "", "MC Y", "TEACH", "MC Z", "TEACH" });
            g.setHeaderColor(Color.Black, Color.White);

            List<string> rowList = new List<string>();

            rowList.Add("Current");
            rowList.Add("WAIT");
            rowList.Add("PICK");
            rowList.Add("PLACE R");
            rowList.Add("PLACE F");

            int idx = 0;
            foreach (string text in rowList)
            {
                g.setValue(idx + 1, 0, text);
                g.setColors(idx + 1, 0, Color.Black, Color.White);
                idx++;
            }

            ui_timer.Enabled = true;
        }
        List<POS> getMcPosData()
        {
            List<POS> posList = new List<POS>();

            POS WAIT = m_mc.teachData(TEACH_POS.UB_PP_WAIT);
            POS PICK = m_mc.teachData(TEACH_POS.UB_PP_PICK);
            POS PLACE_R = m_mc.teachData(TEACH_POS.UB_PP_PLACE_REAR);
            POS PLACE_F = m_mc.teachData(TEACH_POS.UB_PP_PLACE_FRONT);

            posList.Add(WAIT);
            posList.Add(PICK);
            posList.Add(PLACE_R);
            posList.Add(PLACE_F);

            return posList;
        }

        private List<POS> getPosData()
        {
            List<POS> posList = new List<POS>();

            POS WAIT = m_model.teachData(TEACH_POS.UB_PP_WAIT);
            POS PICK = m_model.teachData(TEACH_POS.UB_PP_PICK);
            POS PLACE_R = m_model.teachData(TEACH_POS.UB_PP_PLACE_REAR);
            POS PLACE_F = m_model.teachData(TEACH_POS.UB_PP_PLACE_FRONT);

            posList.Add(WAIT);
            posList.Add(PICK);
            posList.Add(PLACE_R);
            posList.Add(PLACE_F);

            return posList;
        }

        void updateGird()
        {
            CSourceGrid g = motorPosGrid;

            double posY = main.axis(AXIS.UB_PP_Y).pos();
            double posZ = main.axis(AXIS.UB_PP_Z).pos();

            g.setValue(1, 1, posY.ToString("0.00"));
            g.setValue(1, 2, "-");
            g.setValue(1, 3, posZ.ToString("0.00"));
            g.setValue(1, 4, "-");
            g.setBackColor(1, 1, main.axis(AXIS.UB_PP_Y).isMoving() ? Color.Lime : Color.White);
            g.setBackColor(1, 3, main.axis(AXIS.UB_PP_Z).isMoving() ? Color.Lime : Color.White);

            List<POS> mcList = getMcPosData();
            List<POS> posList = getPosData();

            for (int i = 0; i < mcList.Count; i++)
            {
                g.setValue(2 + i, 1, mcList[i].y.ToString("0.00"));
                g.setValue(2 + i, 3, mcList[i].z.ToString("0.00"));

                g.setValue(2 + i, 2, posList[i].y.ToString("0.00"));
                g.setValue(2 + i, 4, posList[i].z.ToString("0.00"));
            }
        }

        private void movePosition(TEACH_POS target, ACT gripAction)
        {
            POS mc = m_mc.teachPos(target);
            POS pos = m_model.teachPos(target);

            double tarX = mc.x + pos.x;
            double tarY = mc.y + pos.y;
            double tarZ = mc.z + pos.z;

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
            TEACH_POS teachPos = (TEACH_POS.UB_PP_WAIT + idx - 2);
           
            if (checkIncludeEnum(teachPos) == false)
                return;

            POS mc = m_mc.teachPos(teachPos);
            POS pos = m_model.teachData(teachPos);

            pos.x = 0.0d;
            pos.y = Util.toDouble(motorPosGrid.cell(1, 1).Value.ToString()) - mc.y;

            m_model.saveTeachPos(pos);
        }

        private void zSave(int idx)
        {
            TEACH_POS teachPos = (TEACH_POS.UB_PP_WAIT + idx - 2);

            if (checkIncludeEnum(teachPos) == false)
                return;

            POS mc = m_mc.teachPos(teachPos);
            POS pos = m_model.teachData(teachPos);

            pos.z = Util.toDouble(motorPosGrid.cell(1, 3).Value.ToString()) - mc.z;

            m_model.saveTeachPos(pos);
        }

        private void xySaveButton_Click(object sender, EventArgs e)
        {
            int row = getGridSelectPos();

            if (row <= 0)
                return;

            CMessageBox msgBox = new CMessageBox(Common.TITLE, "Y Save ?", MessageBoxButtons.OKCancel);
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
