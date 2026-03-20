using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormMotorDelay : Form
    {
        ProcessMain main = null;
        Label[] m_MotorGridList = null;

        public FormMotorDelay(ProcessMain main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void FormMotorDelay_Load(object sender, EventArgs e)
        {
            gridInit();
            load();
        }

        // Grid Init
        private void gridInit()
        {
            CSourceGrid grid = MotorListGrid;
            string[] motorNames = Enum.GetNames(typeof(MOTOR_DELAY));
            int count = motorNames.Length;

            grid.Selection.EnableMultiSelection = false;
            grid.setRowCol(count + 1, 2, true, true);
            grid.setTextAlignment(DevAge.Drawing.ContentAlignment.MiddleCenter);

            grid.setHeader(new string[]
            {
                "Name",
                "Delay (ms)",
            });

            grid.setHeaderColor(Color.White, Color.Black);
            grid.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);

            MOTOR_DELAY[] values = (MOTOR_DELAY[])Enum.GetValues(typeof(MOTOR_DELAY));
            for (int i = 0; i < count; i++)
            {
                grid.setValue(i + 1, 0, motorNames[i].Replace("_", " "));
                grid.setColors(i + 1, 0, Color.White, Color.Black);
                grid.setValue(i + 1, 1, Conf.delayTime(values[i]).ToString());
            }
        }

        // Save Button Click
        private void saveButton_Click(object sender, EventArgs e)
        {
            save();
            main.writeBottomHistory("Motor Delay parameter change.");
            CMessageBox.showInfo(MessageText.saveMessage);
        }

        // Exit Button Click
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Save
        private void save()
        {
            
        }

        // Get Label Value
        private bool GetLabelValue(Label label, ref double value)
        {
            if (string.IsNullOrEmpty(label.Text)) return false;

            var str = label.Text.Replace(" Sec", "");
            return double.TryParse(str, out value);
        }


        // Load
        private void load()
        {
            
        }
    }
}
