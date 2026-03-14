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
    public partial class UCTransferLD : UserControl
    {
        ProcessMain main;
        public void setMain(ProcessMain procMain)
        {
            main = procMain;

        }
        public UCTransferLD(ProcessMain main)
        {
            InitializeComponent();
            this.main = main;
        }
        public UCTransferLD()
        {
            InitializeComponent();
        }
        public void timer_Tick()
        {
            if (main == null)
                return;
            buttonSDVCVRun.updateState();
        }

        public void loadActionButton()
        {
#if false
            buttonSDVCVRun.setEventState(
                main.processTransferLD().CVRun,
                main.processTransferLD().CVAlarm,
                main.processTransferLD().CVInterLock,
                main.processTransferLD().CVRunComplete,
                main.processTransferLD().CVStandby,
                main.processTransferLD().CVStop
                );
            buttonSDVStopperFRDOWN.setEventState(
                main.processTransferLD().StopperDownFR,
                main.processTransferLD().StopperAlarm,
                main.processTransferLD().StopperDOWNInterlock,
                main.processTransferLD().IsStopperDownFR,
                main.processTransferLD().IsStopperUpFR
                );
            buttonSDVStopperFRUP.setEventState(
                main.processTransferLD().StopperUpFR,
                main.processTransferLD().StopperAlarm,
                main.processTransferLD().StopperUPFRInterlock,
                main.processTransferLD().IsStopperUpFR,
                main.processTransferLD().IsStopperDownFR
                );
            buttonSDVStopperRRDOWN.setEventState(
                main.processTransferLD().StopperDownRR,
                main.processTransferLD().StopperAlarm,
                main.processTransferLD().StopperDOWNInterlock,
                main.processTransferLD().IsStopperDownRR,
                main.processTransferLD().IsStopperUpRR
                );
            buttonSDVStopperRRUP.setEventState(
                main.processTransferLD().StopperUpRR,
                main.processTransferLD().StopperAlarm,
                main.processTransferLD().StopperUPRRInterlock,
                main.processTransferLD().IsStopperUpRR,
                main.processTransferLD().IsStopperDownRR
                );
#endif
        }
    }
}
