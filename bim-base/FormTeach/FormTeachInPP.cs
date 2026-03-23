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
        ModelInfo m_mc = null;
        ModelInfo m_model = null;

        public FormTeachInPP(ProcessMain procMain)
        {
            InitializeComponent();
            main = procMain;
            m_mc = Common.MC_MODEL();
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

            g.setRowCol(6, 5, true, true);
            g.setTextAlignment(DevAge.Drawing.ContentAlignment.MiddleCenter);
            g.setHeader(new string[] { "", "MC Y", "TEACH", "MC Z", "TEACH"});
            g.setHeaderColor(Color.Black, Color.White);

            List<string> rowList = new List<string>();

            rowList.Add("Current");
            rowList.Add("WAIT");
            rowList.Add("PICK");
            rowList.Add("PLACE");
            rowList.Add("NG OUT");

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

            POS WAIT = m_mc.teachData(TEACH_POS.PICK_PP_WAIT);
            POS PICK = m_mc.teachData(TEACH_POS.PICK_PP_PICK);
            POS PLACE = m_mc.teachData(TEACH_POS.PICK_PP_PLACE);
            POS NG = m_mc.teachData(TEACH_POS.PICK_PP_NG_OUT);

            posList.Add(WAIT);
            posList.Add(PICK);
            posList.Add(PLACE);
            posList.Add(NG);

            return posList;
        }

        List<POS> getModelPosData()
        {
            List<POS> posList = new List<POS>();

            POS WAIT = m_model.teachData(TEACH_POS.PICK_PP_WAIT);
            POS PICK = m_model.teachData(TEACH_POS.PICK_PP_PICK);
            POS PLACE = m_model.teachData(TEACH_POS.PICK_PP_PLACE);
            POS NG = m_model.teachData(TEACH_POS.PICK_PP_NG_OUT);

            posList.Add(WAIT);
            posList.Add(PICK);
            posList.Add(PLACE);
            posList.Add(NG);

            return posList;
        }

        void updateGird()
        {
            CSourceGrid g = motorPosGrid;

            double posY = main.axis(AXIS.IN_PP_Y).pos();
            double posZ = main.axis(AXIS.IN_PP_Z).pos();

            g.setValue(1, 1, posY.ToString("0.00"));
            g.setValue(1, 2, "-");
            g.setValue(1, 3, posZ.ToString("0.00"));
            g.setValue(1, 4, "-");
            g.setBackColor(1, 1, main.axis(AXIS.IN_PP_Y).isMoving() ? Color.Lime : Color.White);
            g.setBackColor(1, 3, main.axis(AXIS.IN_PP_Z).isMoving() ? Color.Lime : Color.White);

            List<POS> mcList = getMcPosData();
            List<POS> posList = getModelPosData();

            for (int i = 0; i < mcList.Count; i++)
            {
                g.setValue(2 + i, 1, mcList[i].y.ToString("0.00"));
                g.setValue(2 + i, 3, mcList[i].z.ToString("0.00"));

                g.setValue(2 + i, 2, posList[i].y.ToString("0.00"));
                g.setValue(2 + i, 4, posList[i].z.ToString("0.00"));
            }
        }

        private void movePosition(TEACH_POS target)
        {
            POS mc = m_mc.teachPos(target);
            POS pos = m_model.teachPos(target);

            double tarY = mc.y + pos.y;
            double tarZ = mc.z + pos.z;

            CMessageBox msgBox = new CMessageBox(Common.TITLE, "MOVE ?" +
                "\r\n" + "Y:" + tarY.ToString("0.00") + " Z:" + tarZ.ToString("0.00"), MessageBoxButtons.OKCancel);
            bool ret = msgBox.showDialog();

            if (ret == false)
                return;

            ExtAxis Y = main.axis(AXIS.IN_PP_Y);

            main.procManualInPP().start(target, Y.checkPos(tarY));
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
            TEACH_POS teachPos = (TEACH_POS)(idx - 2); 

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
            TEACH_POS teachPos = (TEACH_POS)(idx - 2); 

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

            CMessageBox msgBox = new CMessageBox(Common.TITLE, "X Save ?", MessageBoxButtons.OKCancel);
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

            TEACH_POS teachPos = (TEACH_POS)(row - 2);  

            if (checkIncludeEnum(teachPos) == false)
                return;

            movePosition(teachPos);
        }

        private void ui_timer_Tick(object sender, EventArgs e)
        {
            if (main == null)
                return;

            updateGird();

            setColor(ppGripOnButton, INPUT.MOLD_IN_PP_GRIP, OUTPUT.MOLD_IN_PP_GRIP);
            setColor(ppGripOffButton, INPUT.MOLD_IN_PP_UNGRIP, OUTPUT.MOLD_IN_PP_UNGRIP);
            setColor(fwdButton, INPUT.ALIGN_CV_ALIGN_FWD, OUTPUT.ALIGN_CV_ALIGN_FWD);
            setColor(bwdButton, INPUT.ALIGN_CV_ALIGN_BWD, OUTPUT.ALIGN_CV_ALIGN_BWD);
            setColor(upButton, INPUT.ALIGN_CV_MOLD_UP, OUTPUT.ALIGN_CV_MOLD_UP);
            setColor(downButton, INPUT.ALIGN_CV_MOLD_DOWN, OUTPUT.ALIGN_CV_MOLD_DOWN);
            setColor(reverseGripOnButton, INPUT.MOLD_IN_REVERSE_GRIP, OUTPUT.MOLD_IN_REVERSE_GRIP);
            setColor(reverseGripOffButton, INPUT.MOLD_IN_REVERSE_UNGRIP, OUTPUT.MOLD_IN_REVERSE_UNGRIP);
            setColor(reverseTurnButton, INPUT.MOLD_IN_REVERSE_TURN, OUTPUT.MOLD_IN_REVERSE_TURN);
            setColor(reverseReturnButton, INPUT.MOLD_IN_REVERSE_RETURN, OUTPUT.MOLD_IN_REVERSE_RETURN);
            setColor(reverseUpButton, INPUT.MOLD_SHUTTLE_UP, OUTPUT.SHUTTLE_MOLD_UP);
            setColor(reverseDownButton, INPUT.MOLD_SHUTTLE_DOWN, OUTPUT.SHUTTLE_MOLD_DOWN);
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

        private void ppGripOnButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to GRIP ON ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.MOLD_IN_PP_GRIP, true);
            main.setOutput(OUTPUT.MOLD_IN_PP_UNGRIP, false);
        }

        private void ppGripOffButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to GRIP OFF ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.MOLD_IN_PP_GRIP, false);
            main.setOutput(OUTPUT.MOLD_IN_PP_UNGRIP, true);
        }

        private void fwdButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to FWD ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.ALIGN_CV_ALIGN_FWD, true);
            main.setOutput(OUTPUT.ALIGN_CV_ALIGN_BWD, false);
        }

        private void bwdButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to BWD ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.ALIGN_CV_ALIGN_FWD, false);
            main.setOutput(OUTPUT.ALIGN_CV_ALIGN_BWD, true);
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to UP ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.ALIGN_CV_MOLD_UP, true);
            main.setOutput(OUTPUT.ALIGN_CV_MOLD_DOWN, false);
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to DOWN ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.ALIGN_CV_MOLD_UP, false);
            main.setOutput(OUTPUT.ALIGN_CV_MOLD_DOWN, true);
        }

        private void reverseGripOnButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to GRIP ON ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.MOLD_IN_REVERSE_GRIP, true);
            main.setOutput(OUTPUT.MOLD_IN_REVERSE_UNGRIP, false);
        }

        private void reverseGripOffButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to GRIP OFF ?");

            if (ret == false)
                return;

            if (input(INPUT.MOLD_IN_REVERSE_RETURN) == false)
            {
                if (input(INPUT.MOLD_SHUTTLE_UP) == false)
                {
                    CMessageBox error = new CMessageBox(Common.TITLE, "can not ungrip without up", MessageBoxButtons.OK);
                    error.showDialog();
                    return;
                }
            }

            main.setOutput(OUTPUT.MOLD_IN_REVERSE_GRIP, false);
            main.setOutput(OUTPUT.MOLD_IN_REVERSE_UNGRIP, true);
        }

        private void reverseTurnButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to TURN ?");

            if (ret == false)
                return;

            if (main.input(INPUT.MOLD_IN_REVERSE_UNGRIP) == true)
            {
                CMessageBox error = new CMessageBox(Common.TITLE, "can not turn without grip", MessageBoxButtons.OK);
                error.showDialog();
                return;
            }

            if (main.input(INPUT.MOLD_SHUTTLE_DOWN) == false)
            {
                CMessageBox error = new CMessageBox(Common.TITLE, "can not turn without down", MessageBoxButtons.OK);
                error.showDialog();
                return;
            }

            main.setOutput(OUTPUT.MOLD_IN_REVERSE_TURN, true);
            main.setOutput(OUTPUT.MOLD_IN_REVERSE_RETURN, false);
        }

        private void reverseReturnButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to RETURN ?");

            if (ret == false)
                return;

            if (main.input(INPUT.MOLD_SHUTTLE_DOWN) == false)
            {
                CMessageBox error = new CMessageBox(Common.TITLE, "can not return without down", MessageBoxButtons.OK);
                error.showDialog();
                return;
            }

            main.setOutput(OUTPUT.MOLD_IN_REVERSE_TURN, false);
            main.setOutput(OUTPUT.MOLD_IN_REVERSE_RETURN, true);
        }

        private void reverseUpButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to UP ?");

            if (ret == false)
                return;

            if (main.input(INPUT.MOLD_IN_REVERSE_TURN) == false)
            {
                CMessageBox error = new CMessageBox(Common.TITLE, "can not up without turn", MessageBoxButtons.OK);
                error.showDialog();
                return;
            }

            main.setOutput(OUTPUT.SHUTTLE_MOLD_UP, true);
            main.setOutput(OUTPUT.SHUTTLE_MOLD_DOWN, false);
        }

        private void reverseDownButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to DOWN ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.SHUTTLE_MOLD_UP, false);
            main.setOutput(OUTPUT.SHUTTLE_MOLD_DOWN, true);
        }
    }
}
