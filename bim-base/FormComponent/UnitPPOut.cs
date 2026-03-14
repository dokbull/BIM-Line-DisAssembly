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
    public partial class UnitPPOut: UserControl
    {
        ProcessMain main;

        public UnitPPOut()
        {
            InitializeComponent();
        }

        public void setMain(ProcessMain procMain)
        {
            main = procMain;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UnitPeelTape_Load(object sender, EventArgs e)
        {
            ui_timer.Enabled = true;
        }

        private void ui_timer_Tick(object sender, EventArgs e)
        {
            if (main == null)            
                return;

            //leftGripOutButton._IO_STATE = input(INPUT.OUT_PP_L_GRIP);
            //leftUnGripOutButton._IO_STATE = input(INPUT.OUT_PP_L_UNGRIP);

            //rightGripOutButton._IO_STATE = input(INPUT.OUT_PP_R_GRIP);
            //rightUnGripOutButton._IO_STATE = input(INPUT.OUT_PP_R_UNGRIP);

            //turnButton._IO_STATE = input(INPUT.OUT_PP_TURN);
            //returnButton._IO_STATE = input(INPUT.OUT_PP_RETURN);

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

        private void leftGripOutButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.OUT_PP_GRIP_ON_1, true);

            //setOutput(OUTPUT.OUT_PP_L_GRIP, true);
            //setOutput(OUTPUT.OUT_PP_L_UNGRIP, false);
        }

        private void leftUnGripOutButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.OUT_PP_L_GRIP, false);
            //setOutput(OUTPUT.OUT_PP_L_UNGRIP, true);

        }

        private void rightGripOutButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.OUT_PP_R_GRIP, true);
            //setOutput(OUTPUT.OUT_PP_R_UNGRIP, false);

        }

        private void rightUnGripOutButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.OUT_PP_R_GRIP, false);
            //setOutput(OUTPUT.OUT_PP_R_UNGRIP, true);
        }

        private void turnButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.OUT_PP_RETURN, !output(OUTPUT.OUT_PP_RETURN));

            //setOutput(OUTPUT.OUT_PP_RETURN, false);
            //setOutput(OUTPUT.OUT_PP_TURN, true);
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            //setOutput(OUTPUT.OUT_PP_TURN, !output(OUTPUT.OUT_PP_TURN));

            //setOutput(OUTPUT.OUT_PP_RETURN, true);
            //setOutput(OUTPUT.OUT_PP_TURN, false);
        }
    }
}
