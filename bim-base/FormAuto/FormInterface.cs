using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormInterface: Form
    {
        ProcessMain main;
        public FormInterface(ProcessMain _main)
        {
            InitializeComponent();
            FormMain m_Main = FormMain.getForm();
            this.Location = new Point(m_Main.Left + (m_Main.Width - this.Width) / 2, m_Main.Top + (m_Main.Height - this.Height) / 2);
            main = _main;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ui_timer_Tick(object sender, EventArgs e)
        {
            //prevInputIF1._IO_STATE = main.input(INPUT.IF_PREV_MC_READY_1) ? true : false;
            //prevInputIF2._IO_STATE = main.input(INPUT.IF_PREV_MC_READY_2) ? true : false;

            //prevOutputIF1._IO_STATE = main.output(OUTPUT.IF_PREV_MC_READY_1) ? true : false;
            //prevOutputIF2._IO_STATE = main.output(OUTPUT.IF_PREV_MC_READY_2) ? true : false;

            //nextInputIF1._IO_STATE = main.input(INPUT.IF_NEXT_MC_READY_1) ? true : false;
            //nextInputIF2._IO_STATE = main.input(INPUT.IF_NEXT_MC_READY_2) ? true : false;

            //nextOutputIF1._IO_STATE = main.output(OUTPUT.IF_NEXT_MC_READY_1) ? true : false;
            //nextOutputIF2._IO_STATE = main.output(OUTPUT.IF_NEXT_MC_READY_2) ? true : false;
        }

        private void FormInterface_Load(object sender, EventArgs e)
        {
            ui_timer.Enabled = true;

            main.setForceInterface(true);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frontIFSet1Button_Click(object sender, EventArgs e)
        {

        }

        private void frontIFSet2Button_Click(object sender, EventArgs e)
        {

        }

        private void FormInterface_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.setForceInterface(false);
        }

        private void input3Button_Click(object sender, EventArgs e)
        {

        }

        private void prevInput_Click(object sender, EventArgs e)
        {

        }

        private void prevOutput_Click(object sender, EventArgs e)
        {
            if (main.isAuto() == true)
                return;

            
        }

        private void nextInput_Click(object sender, EventArgs e)
        {

        }

        private void nextOutput_Click(object sender, EventArgs e)
        {
            if (main.isAuto() == true)
                return;

            
        }

        private void prevInputIF2_Click(object sender, EventArgs e)
        {

        }

        private void prevOutputIF2_Click(object sender, EventArgs e)
        {
            if (main.isAuto() == true)
                return;

            //main.setOutput(OUTPUT.IF_PREV_MC_READY_2, !main.output(OUTPUT.IF_PREV_MC_READY_2));
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void nextOutputIF2_Click(object sender, EventArgs e)
        {
            if (main.isAuto() == true)
                return;

            //main.setOutput(OUTPUT.IF_NEXT_MC_READY_2 ,!main.output(OUTPUT.IF_NEXT_MC_READY_2));
        }
    }
}
