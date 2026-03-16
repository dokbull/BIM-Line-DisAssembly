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

            ExtAxis Y = main.axis(AXIS.UB_PP_Y);

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

            movePosition(teachPos);
        }

        private void ui_timer_Tick(object sender, EventArgs e)
        {
            if (main == null)
                return;

            updateGird();

            setColor(ppVacOnButton, INPUT.UB_OUT_PP_VAC, OUTPUT.UB_OUT_PP_VAC);
            setColor(ppVacOffButton, !input(INPUT.UB_OUT_PP_VAC), !output(OUTPUT.UB_OUT_PP_VAC));
            setColor(fwdButton, INPUT.UB_OUT_PP_VAC, OUTPUT.UB_OUT_PP_FWD);
            setColor(bwdButton, INPUT.UB_OUT_PP_BWD, OUTPUT.UB_OUT_PP_BWD);

            setColor(reverseTurnButton1, INPUT.UB_OUT_REVERSE_TURN_1, OUTPUT.UB_OUT_REVERSE_TURN_1);
            setColor(reverseReturnButton1, INPUT.UB_OUT_REVERSE_RETURN_1, OUTPUT.UB_OUT_REVERSE_RETURN_1);
            setColor(reverseUpButton1, INPUT.UB_OUT_REVERSE_UP_1, OUTPUT.UB_OUT_REVERSE_UP_1);
            setColor(reverseDownButton1, INPUT.UB_OUT_REVERSE_DOWN_1, OUTPUT.UB_OUT_REVERSE_DOWN_1);
            setColor(reverseVacOnButton1, INPUT.UB_OUT_REVERSE_VAC_1, OUTPUT.UB_OUT_REVERSE_VAC_1);
            setColor(reverseVacOffButton1, !input(INPUT.UB_OUT_REVERSE_VAC_1), !output(OUTPUT.UB_OUT_REVERSE_VAC_1));

            setColor(reverseTurnButton2, INPUT.UB_OUT_REVERSE_TURN_2, OUTPUT.UB_OUT_REVERSE_TURN_2);
            setColor(reverseReturnButton2, INPUT.UB_OUT_REVERSE_RETURN_2, OUTPUT.UB_OUT_REVERSE_RETURN_2);
            setColor(reverseUpButton2, INPUT.UB_OUT_REVERSE_UP_2, OUTPUT.UB_OUT_REVERSE_UP_2);
            setColor(reverseDownButton2, INPUT.UB_OUT_REVERSE_DOWN_2, OUTPUT.UB_OUT_REVERSE_DOWN_2);
            setColor(reverseVacOnButton2, INPUT.UB_OUT_REVERSE_VAC_2, OUTPUT.UB_OUT_REVERSE_VAC_2);
            setColor(reverseVacOffButton2, !input(INPUT.UB_OUT_REVERSE_VAC_2), !output(OUTPUT.UB_OUT_REVERSE_VAC_2));
        }

        bool input(INPUT input)
        {
            return main.input(input);
        }

        bool output(OUTPUT output)
        {
            return main.output(output);
        }

        void setColor(Button btn, bool inputValue, bool outputValue)
        {
            if (inputValue == true)
                btn.BackColor = Color.SkyBlue;
            else if (outputValue == true)
                btn.BackColor = Color.Yellow;
            else
                btn.BackColor = Color.White;
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
        private void ppVacOnButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to VAC ON ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.UB_OUT_PP_VAC, true);
        }

        private void ppVacOffButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to VAC OFF ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.UB_OUT_PP_VAC, false);
        }

        private void fwdButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to FWD ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.UB_OUT_PP_FWD, true);
            main.setOutput(OUTPUT.UB_OUT_PP_BWD, false);
        }

        private void bwdButton_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to BWD ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.UB_OUT_PP_FWD, false);
            main.setOutput(OUTPUT.UB_OUT_PP_BWD, true);
        }

        private void reverseVacOnButton1_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to VAC ON ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.UB_OUT_REVERSE_VAC_1, true);
        }

        private void reverseVacOffButton1_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to VAC OFF ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.UB_OUT_REVERSE_VAC_1, false);
        }

        private void reverseTurnButton1_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to TURN ?");

            if (ret == false)
                return;

            if (main.input(INPUT.UB_OUT_REVERSE_UP_1) == false)
            {
                CMessageBox error = new CMessageBox(Common.TITLE, "can not turn without up", MessageBoxButtons.OK);
                error.showDialog();
                return;
            }

            main.setOutput(OUTPUT.UB_OUT_REVERSE_TURN_1, true);
            main.setOutput(OUTPUT.UB_OUT_REVERSE_RETURN_1, false);
        }

        private void reverseReturnButton1_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to RETURN ?");

            if (ret == false)
                return;

            if (main.input(INPUT.UB_OUT_REVERSE_UP_1) == false)
            {
                CMessageBox error = new CMessageBox(Common.TITLE, "can not return without up", MessageBoxButtons.OK);
                error.showDialog();
                return;
            }

            main.setOutput(OUTPUT.UB_OUT_REVERSE_RETURN_1, true);
            main.setOutput(OUTPUT.UB_OUT_REVERSE_TURN_1, false);
        }

        private void reverseUpButton1_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to UP ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.UB_OUT_REVERSE_UP_1, true);
            main.setOutput(OUTPUT.UB_OUT_REVERSE_DOWN_1, false);
        }

        private void reverseDownButton1_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to DOWN ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.UB_OUT_REVERSE_DOWN_1, true);
            main.setOutput(OUTPUT.UB_OUT_REVERSE_UP_1, false);
        }

        private void reverseVacOnButton2_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to VAC ON ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.UB_OUT_REVERSE_VAC_2, true);
        }

        private void reverseVacOffButton2_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to VAC OFF ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.UB_OUT_REVERSE_VAC_2, false);
        }

        private void reverseTurnButton2_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to TURN ?");

            if (ret == false)
                return;

            if (main.input(INPUT.UB_OUT_REVERSE_UP_2) == false)
            {
                CMessageBox error = new CMessageBox(Common.TITLE, "can not turn without up", MessageBoxButtons.OK);
                error.showDialog();
                return;
            }

            main.setOutput(OUTPUT.UB_OUT_REVERSE_TURN_2, true);
            main.setOutput(OUTPUT.UB_OUT_REVERSE_RETURN_2, false);
        }

        private void reverseReturnButton2_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to RETURN ?");

            if (ret == false)
                return;

            if (main.input(INPUT.UB_OUT_REVERSE_UP_2) == false)
            {
                CMessageBox error = new CMessageBox(Common.TITLE, "can not return without up", MessageBoxButtons.OK);
                error.showDialog();
                return;
            }

            main.setOutput(OUTPUT.UB_OUT_REVERSE_RETURN_2, true);
            main.setOutput(OUTPUT.UB_OUT_REVERSE_TURN_2, false);
        }

        private void reverseUpButton2_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to UP ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.UB_OUT_REVERSE_UP_2, true);
            main.setOutput(OUTPUT.UB_OUT_REVERSE_DOWN_2, false);
        }

        private void reverseDownButton2_Click(object sender, EventArgs e)
        {
            bool ret = CMessageBox.showMessage("Do you want to DOWN ?");

            if (ret == false)
                return;

            main.setOutput(OUTPUT.UB_OUT_REVERSE_DOWN_2, true);
            main.setOutput(OUTPUT.UB_OUT_REVERSE_UP_2, false);
        }
    }
}
