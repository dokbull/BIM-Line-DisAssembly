using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bim_base.FormComponent
{
    public partial class UBPPControl : UserControl
    {
        ProcessMain main;
        bool useAlignControl = true;
        public UBPPControl()
        {
            InitializeComponent();
        }

        private void UnitDisplayControl_Load(object sender, EventArgs e)
        {
            ui_timer.Enabled = true;
        }

        public void setMain(ProcessMain procMain, bool useAlignXY = true)
        { 
            main = procMain;
            useAlignControl = useAlignXY;
        }

        private void ui_timer_Tick(object sender, EventArgs e)
        {
            if (main == null)
                return;


            //ub1UpButton._IO_STATE = input(INPUT.DISP_PP_L_VAC_UP);
            //ub1DonwButton._IO_STATE = input(INPUT.DISP_PP_L_VAC_DOWN);

            //ub2UpButton._IO_STATE = input(INPUT.DISP_PP_R_VAC_UP);
            //ub2DownButton._IO_STATE = input(INPUT.DISP_PP_R_VAC_DOWN);

            //turnButton._IO_STATE = input(INPUT.DISP_PP_TURN);
            //returnButton._IO_STATE = input(INPUT.DISP_PP_RETURN);

            //ub1GripButton._IO_STATE = input(INPUT.DISP_PP_L_GRIP);
            //ub1UngripButton._IO_STATE = input(INPUT.DISP_PP_L_UNGRIP);

            //ub2GripButton._IO_STATE = input(INPUT.DISP_PP_R_GRIP);
            //ub2UngripButton._IO_STATE = input(INPUT.DISP_PP_R_UNGRIP);

            //leftVacButton._IO_STATE = output(OUTPUT.DISP_PP_VAC_L);
            //rightVacButton._IO_STATE = output(OUTPUT.DISP_PP_VAC_R);

        }

        public void setOutput(OUTPUT output, bool value)
        {
            if (main == null)
                return;
            main.setOutput(output, value);
        }

        public bool output(OUTPUT output)
        {
            if (main == null)
                return false;

            if (main.output(output) == true)
                return true;
            else
                return false;
        }

        public bool input(INPUT input)
        {
            if (main == null)
                return false;

            if (main.input(input) == true)
                return true;
            else
                return false;
        }

        private void trayX2FwdButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.TRAY_IN_X_ALIGN_2, true);
        }

        private void trayY2FwdButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.TRAY_IN_Y_ALIGN_2, true);
        }
        private void trayX1FwdButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.TRAY_IN_X_ALIGN_1, true);
        }

        private void trayY1FwdButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.TRAY_IN_Y_ALIGN_1, true);
        }

        private void ub1UpDownButton_Click(object sender, EventArgs e)
        {
            ///setOutput(OUTPUT.DISP_PP_L_DOWN, false);
        }
        private void ub1GripOnOffButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.DISP_PP_L_GRIP, true);
            //setOutput(OUTPUT.DISP_PP_L_UNGRIP, false);
        }

        private void ub2UpDownButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.DISP_PP_R_DOWN, false);
        }

        private void ub2GripOnOffButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.DISP_PP_R_GRIP, true);
            //setOutput(OUTPUT.DISP_PP_R_UNGRIP, false);
        }

        private void solTurnReturn_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.DISP_PP_RETURN, false);
            //setOutput(OUTPUT.DISP_PP_TURN, true);
        }

        private void trayX1BwdButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.TRAY_IN_X_ALIGN_1, false);
        }

        private void trayY1BwdButton_Click(object sender, EventArgs e)
        {

            //setOutput(OUTPUT.TRAY_IN_Y_ALIGN_1, false);
        }
        private void trayX2BwdButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.TRAY_IN_X_ALIGN_2, false);
        }

        private void trayY2BwdButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.TRAY_IN_Y_ALIGN_2, false);
        }

        private void ub1DonwButton_Click(object sender, EventArgs e)
        {
            //if (input(INPUT.DISP_PP_L_UNGRIP) == false)
            //{
            //    CMessageBox.showInfo("GRIP IS ON, CAN NOT DOWN CYLINDER");
            //    return;
            //}

            //setOutput(OUTPUT.DISP_PP_L_DOWN, true);
        }

        private void ub1UngripButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.DISP_PP_L_GRIP, false);
            //setOutput(OUTPUT.DISP_PP_L_UNGRIP, true);
        }

        private void ub2DownButton_Click(object sender, EventArgs e)
        {
            //if (input(INPUT.DISP_PP_R_UNGRIP) == false)
            //{
            //    CMessageBox.showInfo("GRIP IS ON, CAN NOT DOWN CYLINDER");
            //    return;
            //}

            //setOutput(OUTPUT.DISP_PP_R_DOWN, true);
        }

        private void ub2UngripButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.DISP_PP_R_GRIP, false);
            //setOutput(OUTPUT.DISP_PP_R_UNGRIP, true);
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.DISP_PP_RETURN, true);
            //setOutput(OUTPUT.DISP_PP_TURN, false);
        }

        private void leftVacButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.DISP_PP_VAC_L, !output(OUTPUT.DISP_PP_VAC_L));
        }

        private void rightVacButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.DISP_PP_VAC_R, !output(OUTPUT.DISP_PP_VAC_R));
        }
    }
}
