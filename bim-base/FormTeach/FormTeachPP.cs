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
    public partial class FormTeachPP : Form
    {
        ProcessMain main = null;
        ModelInfo m_model = null;
        List<ExtAxis> axisList = null;
        POS TEACH_CURRENT = new POS();
        POS TEACH_TARGET = new POS();

        int m_selectedIndex = -1;
        List<MyButton.ButtonPress> m_buttonList = new List<MyButton.ButtonPress>() { };
        List<POS> posList = new List<POS>();
        MyButton.ButtonPress buttonSelected = null;
        Color buttonOldColor = Color.Yellow;
        public FormTeachPP(ProcessMain procMain)
        {
            InitializeComponent();
            main = procMain;
            m_model = Common.MODEL_INFO;

            //axisList = new List<ExtAxis>() { main.axis(AXIS.TRANSFER_X) };
            //if (main.isSimulation() == false) {
            //    jogControl31.setMain(main, true);
            //    jogControl31.setAxis(axisList);
            //}
        }

        private void FormTeachPP_Load(object sender, EventArgs e)
        {
            List<string> rowList = new List<string>();

            m_buttonList.Add(btnPosLoader);
            m_buttonList.Add(btnPosBarcode);
            m_buttonList.Add(btnPosAlignRR);
            m_buttonList.Add(btnPosAlignFR);
            buttonOldColor = btnPosAlignFR.GradientTop;
            posList = getPosData();
            foreach (MyButton.ButtonPress btn in m_buttonList) {
                //btn.
                btn.Click += onBTN_Click;
                btn.MouseDoubleClick += onBTN_DoubleClick;
            }
            showPanel(1);
            Invalidate();
            ui_timer.Enabled = true;
        }
        private void onBTN_DoubleClick(object sender, MouseEventArgs e)
        {
            if (buttonSelected == null) return;
            TEACH_POS teachPos = (TEACH_POS)(m_buttonList.IndexOf(buttonSelected));  //FIXME@JW header, current 라인 제외
            if (checkIncludeEnum(teachPos) == false)
                return;

            movePosition(teachPos);
        }
        private void xySave()
        {
            double _x = 0.0d;
            double _y = 0.0d;
            TEACH_POS teachPos = (TEACH_POS)(m_buttonList.IndexOf(buttonSelected));  //FIXME@JW header, current 라인 제외
            if (checkIncludeEnum(teachPos) == false)
                return;

            if (main.isSimulation() == false) {
                _x = Util.toDouble(TEACH_TARGET_X.Text);
                //_y = Util.toDouble(motorPosGrid.cell(1, 2).Value.ToString());
            }
            POS pos = m_model.teachData(teachPos);

            pos.x = _x;
            pos.y = _y;

            m_model.saveTeachPos(pos);
        }
        private void onBTN_Click(object sender, EventArgs e)
        {
            buttonSelected = (MyButton.ButtonPress)sender;
        }
        public void OnGridCell_Clicked(object sender, DataGridViewCellEventArgs e)
        {
            if (main.isAuto() == true)
                return;

            //m_rowSelected = e.RowIndex;

            int row = e.RowIndex;
            int col = e.ColumnIndex;
            if (col <= 0)
                return;


            if (m_selectedIndex != row) {
                m_selectedIndex = row;
                return;
            }

            if (m_selectedIndex < 2)
                return;

            int idx = m_selectedIndex - 2;


            List<POS> posList = getPosData();
            POS pos = posList[idx];

            if (pos == null)
                return;

            double position = pos.z;

            FormNumpad dlg = new FormNumpad(position.ToString("0.00"), true);
            if (dlg.ShowDialog() == DialogResult.OK) {
                if (col == 1)
                    pos.x = Util.toDouble(dlg.getNewValue());
                else
                    return;

                m_model.saveTeachPos(pos);
            }
        }
        private List<POS> getPosData()
        {
            List<POS> posList = new List<POS>();
            //posList.Clear();
            POS LOADER = m_model.teachData(TEACH_POS.PICK_PP_WAIT);

            posList.Add(LOADER);
            return posList;
        }


        private void movePosition(TEACH_POS target)
        {
            POS pos = m_model.teachPos(target);

            double tarX = pos.x;

            CMessageBox msgBox = new CMessageBox(Common.TITLE, "MOVE ?" +
                "\r\n" + "X:" + tarX.ToString("0.00"), MessageBoxButtons.OKCancel);
            bool ret = msgBox.showDialog();

            if (ret == false)
                return;
            //main.axis(AXIS.TRANSFER_X).absMove(tarX);
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

        private void xySaveButton_Click(object sender, EventArgs e)
        {
            //int row = getGridSelectPos();

            //if (row <= 0)
            //    return;

            CMessageBox msgBox = new CMessageBox(Common.TITLE, "X/Y Save ?", MessageBoxButtons.OKCancel);
            if (msgBox.showDialog() == false)
                return;

            xySave();
        }

        private void moveButton_Click(object sender, EventArgs e)
        {
            if (buttonSelected == null) {
                CMessageBox msgBox = new CMessageBox(Common.TITLE, "Please select one position to move", MessageBoxButtons.OK);
                msgBox.showDialog();
                return;
            }

            TEACH_POS teachPos = (TEACH_POS)(m_buttonList.IndexOf(buttonSelected));  //FIXME@JW header, current 라인 제외
            if (checkIncludeEnum(teachPos) == false)
                return;

            movePosition(teachPos);
        }

        private void ui_timer_Tick(object sender, EventArgs e)
        {
            if (main == null)
                return;
            if (buttonSelected == null) return;
            UpdateTeachData();
            //updateGird();
        }
        public void UpdateTeachData()
        {
            if (main.isSimulation() == false) {
                //TEACH_CURRENT.x = main.axis(AXIS.TRANSFER_X).pos();
                TEACH_CURENT_X.Text = TEACH_CURRENT.x.ToString("0.00");
            }
            foreach(MyButton.ButtonPress btn in m_buttonList) {
                if (btn == buttonSelected) {

                    buttonSelected.GradientBottom = Color.Yellow;
                    buttonSelected.GradientTop = Color.Yellow;
                }
                else {

                    btn.GradientBottom = buttonOldColor;
                    btn.GradientTop = buttonOldColor;
                }
            }
            posList = getPosData();
            TEACH_TARGET.x = posList[m_buttonList.IndexOf(buttonSelected)].x;
            TEACH_TARGET_X.Text = TEACH_TARGET.x.ToString("0.00");
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

        private void slot4PickButton_Click(object sender, EventArgs e)
        {

        }

        private void jogControlX1_Load(object sender, EventArgs e)
        {

        }

        private void buttonSDV015_Click(object sender, EventArgs e)
        {

        }

        private void buttonSDV017_Click(object sender, EventArgs e)
        {

        }

        private void motorPosGrid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ucUnitAlign1_Load(object sender, EventArgs e)
        {

        }

        private void colorButton4_Click(object sender, EventArgs e)
        {

        }

        private void TEACH_CURENT_Y_Click(object sender, EventArgs e)
        {

        }

        private void btnPosAlignFR_Click(object sender, EventArgs e)
        {

            showPanel(1);
        }

        private void btnPosAlignRR_Click(object sender, EventArgs e)
        {

            showPanel(1);
        }

        private void btnPosBarcode_Click(object sender, EventArgs e)
        {

            showPanel(0);
        }

        private void btnPosLoader_Click(object sender, EventArgs e)
        {
            showPanel(0);
        }

        private void colorButton4_Click_1(object sender, EventArgs e)
        {

        }
        int idx_old = 0;
        public void showPanel(int idx)
        {
            if (idx_old == idx) return;
            idx_old = idx;
            if (idx == 0) {
                pnUnit1.Visible = false;
                pnUnit2.Visible = false;
                pnUnit3.Visible = false;
                pnUnit4.Visible = false;
                pnUnit1.Visible = true;
                pnUnit2.Visible = true;
            }
            if (idx == 1) {
                pnUnit1.Visible = false;
                pnUnit2.Visible = false;
                pnUnit3.Visible = false;
                pnUnit4.Visible = false;
                pnUnit3.Visible = true;
                pnUnit4.Visible = true;
            }
        }

        private void TEACH_TARGET_X_Click(object sender, EventArgs e)
        {
            if (buttonSelected == null) return;
            List<POS> posList = getPosData();
            POS pos = posList[m_buttonList.IndexOf(buttonSelected)];
            if (pos == null)
                return;

            double position = pos.z;

            FormNumpad dlg = new FormNumpad(position.ToString("0.00"), true);
            if (dlg.ShowDialog() == DialogResult.OK) {
                pos.x = Util.toDouble(dlg.getNewValue());
                m_model.saveTeachPos(pos);
            }
        }
    }
}
