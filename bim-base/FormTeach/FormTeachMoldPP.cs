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
    public partial class FormTeachMoldPP : Form
    {
        ProcessMain main = null;
        ModelInfo m_mc = null;
        ModelInfo m_model = null;

        public FormTeachMoldPP(ProcessMain procMain)
        {
            InitializeComponent();
            main = procMain;
            m_mc = Common.MC_MODEL();
            m_model = Common.MODEL_INFO(Conf.CURR_MODEL_IDX);

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

            g.setRowCol(5, 9, true, true);
            g.setTextAlignment(DevAge.Drawing.ContentAlignment.MiddleCenter);
            g.setHeader(new string[] { "", "MC X", "TEACH", "MC XB", "TEACH", "MC ZL", "TEACH",  "MC ZR", "TEACH" }); g.setHeaderColor(Color.Black, Color.White);

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
        List<POS> getMcPosData()
        {
            List<POS> posList = new List<POS>();

            POS WAIT = m_mc.teachData(TEACH_POS.MOLD_PP_WAIT);
            POS LEFT = m_mc.teachData(TEACH_POS.MOLD_PP_LEFT);
            POS RIGHT = m_mc.teachData(TEACH_POS.MOLD_PP_RIGHT);

            posList.Add(WAIT);
            posList.Add(LEFT);
            posList.Add(RIGHT);

            return posList;
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
            g.setValue(1, 2, "-");
            g.setValue(1, 3, posX2.ToString("0.00"));
            g.setValue(1, 4, "-");
            g.setValue(1, 5, posZ1.ToString("0.00"));
            g.setValue(1, 6, "-");
            g.setValue(1, 7, posZ2.ToString("0.00"));
            g.setValue(1, 8, "-");

            g.setBackColor(1, 1, main.axis(AXIS.MOLD_PP_X).isMoving() ? Color.Lime : Color.White);
            g.setBackColor(1, 3, main.axis(AXIS.BASE_X).isMoving() ? Color.Lime : Color.White);
            g.setBackColor(1, 5, main.axis(AXIS.MOLD_PP_ZL).isMoving() ? Color.Lime : Color.White);
            g.setBackColor(1, 7, main.axis(AXIS.MOLD_PP_ZR).isMoving() ? Color.Lime : Color.White);

            List<POS> mcList = getMcPosData();
            List<POS> posList = getPosData();

            for (int i = 0; i < mcList.Count; i++)
            {
                g.setValue(2 + i, 1, mcList[i].x.ToString("0.00"));
                g.setValue(2 + i, 3, mcList[i].xB.ToString("0.00"));
                g.setValue(2 + i, 5, mcList[i].zL.ToString("0.00"));
                g.setValue(2 + i, 7, mcList[i].zR.ToString("0.00"));

                g.setValue(2 + i, 2, posList[i].x.ToString("0.00"));
                g.setValue(2 + i, 4, posList[i].xB.ToString("0.00"));
                g.setValue(2 + i, 6, posList[i].zL.ToString("0.00"));
                g.setValue(2 + i, 8, posList[i].zR.ToString("0.00"));
            }
        }

        private void movePosition(TEACH_POS target)
        {
            POS pos = m_model.teachPos(target);

            double tarX = pos.x;
            double tarZ1 = pos.zL;
            double tarZ2 = pos.zR;

            CMessageBox msgBox = new CMessageBox(Common.TITLE, "MOVE ?" +
                "\r\n" + "X:" + tarX.ToString("0.00") + " ZL:" + tarZ1.ToString("0.00") + " ZR:" + tarZ2.ToString("0.00"), MessageBoxButtons.OKCancel);
            bool ret = msgBox.showDialog();

            if (ret == false)
                return;

            ExtAxis X = main.axis(AXIS.MOLD_PP_X);

            main.procManualMoldPP().start(target, X.checkPos(tarX));
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
            TEACH_POS teachPos = (TEACH_POS.MOLD_PP_WAIT + idx - 2);  

            if (checkIncludeEnum(teachPos) == false)
                return;

            POS mc = m_mc.teachPos(teachPos);
            POS pos = m_model.teachData(teachPos);

            pos.x = Util.toDouble(motorPosGrid.cell(1, 1).Value.ToString()) - mc.x;
            
            m_model.saveTeachPos(pos);
        }

        private void xbSave(int idx)
        {
            TEACH_POS teachPos = (TEACH_POS.MOLD_PP_WAIT + idx - 2);  
            if (checkIncludeEnum(teachPos) == false)
                return;

            POS mc = m_mc.teachPos(teachPos);
            POS pos = m_model.teachData(teachPos);

            pos.xB = Util.toDouble(motorPosGrid.cell(1, 3).Value.ToString()) - mc.xB;

            m_model.saveTeachPos(pos);
        }

        private void z1Save(int idx)
        {
            TEACH_POS teachPos = (TEACH_POS.MOLD_PP_WAIT + idx - 2);  
            if (checkIncludeEnum(teachPos) == false)
                return;

            POS mc = m_mc.teachPos(teachPos);
            POS pos = m_model.teachData(teachPos);

            pos.zL = Util.toDouble(motorPosGrid.cell(1, 5).Value.ToString()) - mc.zL;
            
            m_model.saveTeachPos(pos);
        }

        private void z2Save(int idx)
        {
            TEACH_POS teachPos = (TEACH_POS.MOLD_PP_WAIT + idx - 2);  
            if (checkIncludeEnum(teachPos) == false)
                return;

            POS mc = m_mc.teachPos(teachPos);
            POS pos = m_model.teachData(teachPos);

            pos.zR = Util.toDouble(motorPosGrid.cell(1, 7).Value.ToString()) - mc.zR;

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

            movePosition(teachPos);
        }

        private void ui_timer_Tick(object sender, EventArgs e)
        {
            if (main == null)
                return;

            updateGird();

            setColor(leftGripOnButton, INPUT.MOLD_OUT_PP_GRIP_1, OUTPUT.MOLD_OUT_PP_GRIP_1);
            setColor(leftGripOffButton, INPUT.MOLD_OUT_PP_UNGRIP_1, OUTPUT.MOLD_OUT_PP_UNGRIP_1);
            setColor(rightGripOnButton, INPUT.MOLD_OUT_PP_GRIP_2, OUTPUT.MOLD_OUT_PP_GRIP_2);
            setColor(rightGripOffButton, INPUT.MOLD_OUT_PP_UNGRIP_2, OUTPUT.MOLD_OUT_PP_UNGRIP_2);
            setColor(upButton, INPUT.MOLD_SHUTTLE_SERVO_UP_1, INPUT.MOLD_SHUTTLE_SERVO_UP_2, OUTPUT.SHUTTLE_SERVO_MOLD_UP);
            setColor(downButton, INPUT.MOLD_SHUTTLE_SERVO_DOWN_1, INPUT.MOLD_SHUTTLE_SERVO_DOWN_2, OUTPUT.SHUTTLE_SERVO_MOLD_DOWN);
            setColor(holdOnButton, INPUT.MOLD_SHUTTLE_PUSHER_FWD_1, INPUT.MOLD_SHUTTLE_PUSHER_FWD_2, OUTPUT.SHUTTLE_MOLD_PUSHER_FWD);
            setColor(holdOffButton, INPUT.MOLD_SHUTTLE_PUSHER_BWD_1, INPUT.MOLD_SHUTTLE_PUSHER_BWD_2, OUTPUT.SHUTTLE_MOLD_PUSHER_BWD);
            setColor(openerFwdButton, INPUT.MOLD_SHUTTLE_UNLOCK_FWD_1, INPUT.MOLD_SHUTTLE_UNLOCK_FWD_2, OUTPUT.SHUTTLE_MOLD_UNLOCK_FWD);
            setColor(openerBwdButton, INPUT.MOLD_SHUTTLE_UNLOCK_BWD_1, INPUT.MOLD_SHUTTLE_UNLOCK_BWD_2, OUTPUT.SHUTTLE_MOLD_UNLOCK_BWD);
            // setColor(coverLockButton, INPUT.MOLD_SHUTTLE_UP, OUTPUT.SHUTTLE_MOLD_UP);
            // setColor(coverUnlockButton, INPUT.MOLD_SHUTTLE_DOWN, OUTPUT.SHUTTLE_MOLD_DOWN);
        }

        bool input(INPUT input)
        {
            return main.input(input);
        }

        bool output(OUTPUT output)
        {
            return main.output(output);
        }

        void setColor(Button btn, INPUT inputEnum, OUTPUT outputEnum)
        {
            if (input(inputEnum) == true)
                btn.BackColor = Color.SkyBlue;
            else if (output(outputEnum) == true)
                btn.BackColor = Color.Yellow;
            else
                btn.BackColor = Color.White;
        }

        void setColor(Button btn, INPUT inputEnum1, INPUT inputEnum2, OUTPUT outputEnum)
        {
            if (input(inputEnum1) == true && input(inputEnum2) == true)
                btn.BackColor = Color.SkyBlue;
            else if (output(outputEnum) == true)
                btn.BackColor = Color.Yellow;
            else
                btn.BackColor = Color.White;
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

        private void bwdButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to WD ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.ALIGN_CV_ALIGN_FWD, true);
            main.setOutput(OUTPUT.ALIGN_CV_ALIGN_BWD, false);
        }

        private void leftGripOnButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to GRIP ON ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.MOLD_OUT_PP_GRIP_1, true);
            main.setOutput(OUTPUT.MOLD_OUT_PP_UNGRIP_1, false);
        }

        private void leftGripOffButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to GRIP OFF ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.MOLD_OUT_PP_UNGRIP_1, true);
            main.setOutput(OUTPUT.MOLD_OUT_PP_GRIP_1, false);
        }

        private void rightGripOnButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to GRIP ON ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.MOLD_OUT_PP_GRIP_2, true);
            main.setOutput(OUTPUT.MOLD_OUT_PP_UNGRIP_2, false);
        }

        private void rightGripOffButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to GRIP OFF ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.MOLD_OUT_PP_UNGRIP_2, true);
            main.setOutput(OUTPUT.MOLD_OUT_PP_GRIP_2, false);
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to UP ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.SHUTTLE_SERVO_MOLD_UP, true);
            main.setOutput(OUTPUT.SHUTTLE_SERVO_MOLD_DOWN, false);
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to DOWN ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.SHUTTLE_SERVO_MOLD_DOWN, true);
            main.setOutput(OUTPUT.SHUTTLE_SERVO_MOLD_UP, false);
        }

        private void holdOnButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to HOLD ON ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.SHUTTLE_MOLD_PUSHER_FWD, true);
            main.setOutput(OUTPUT.SHUTTLE_MOLD_PUSHER_BWD, false);
        }

        private void holdOffButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to HOLD OFF ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.SHUTTLE_MOLD_PUSHER_BWD, true);
            main.setOutput(OUTPUT.SHUTTLE_MOLD_PUSHER_FWD, false);
        }

        private void openerFwdButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to OPENER FWD ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.SHUTTLE_MOLD_UNLOCK_FWD, true);
            main.setOutput(OUTPUT.SHUTTLE_MOLD_UNLOCK_BWD, false);
        }

        private void openerBwdButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to OPENER BWD ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.SHUTTLE_MOLD_UNLOCK_BWD, true);
            main.setOutput(OUTPUT.SHUTTLE_MOLD_UNLOCK_FWD, false);
        }

        private void coverLockButton_Click(object sender, EventArgs e)
        {

        }

        private void coverUnlockButton_Click(object sender, EventArgs e)
        {

        }
    }
}
