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
    public partial class UnitPPTray : UserControl
    {
        ProcessMain main;
        public UnitPPTray()
        {
            InitializeComponent();
        }

        private void UnitDisplayControl_Load(object sender, EventArgs e)
        {
            ui_timer.Enabled = true;
        }

        public void setMain(ProcessMain procMain)
        { 
            main = procMain; 
        }

        private void ui_timer_Tick(object sender, EventArgs e)
        {
            if (main == null)
                return;

            //xFwdButton._IO_STATE = input(INPUT.TRAY_IN_X_FWD_1);
            //xBwdButton._IO_STATE = input(INPUT.TRAY_IN_X_BWD_1);

            //yFwdButton._IO_STATE = input(INPUT.TRAY_IN_Y_FWD_1);
            //yBwdButton._IO_STATE = input(INPUT.TRAY_IN_Y_BWD_1);

            //upButton._IO_STATE = input(INPUT.TRAY_PP_UP);
            //downButton._IO_STATE = input(INPUT.TRAY_PP_DOWN);  

            //vacButton._IO_STATE = input(INPUT.TRAY_PP_VAC);
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

        private void trayXAlignButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.TRAY_IN_X_ALIGN_1, true);
        }

        private void trayYAlignButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.TRAY_IN_Y_ALIGN_1, true);
        }


        private void solStartStop_Click(object sender, EventArgs e)
        {
           // bool status = output(OUTPUT.TRAY_PP_VAC);
            //setOutput(OUTPUT.TRAY_PP_VAC, !status);
        }

        private void cioButton1_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.TRAY_PP_DOWN, false);
        }

        private void cioButton2_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.TRAY_PP_VAC, !output(OUTPUT.TRAY_PP_VAC));
        }

        private void xBwdButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.TRAY_IN_X_ALIGN_1, false);
        }

        private void yBwdButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.TRAY_IN_Y_ALIGN_1, false);
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.TRAY_PP_DOWN, true);
        }
    }
}
