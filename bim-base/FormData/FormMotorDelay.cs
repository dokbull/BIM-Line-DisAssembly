using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace bim_base
{
    public partial class FormMotorDelay : Form
    {
        ProcessMain main = null;
        CSourceGrid motorGrid = null;
        string[] motorNames = null;
        int count = 0;

        public FormMotorDelay(ProcessMain main)
        {
            InitializeComponent();
            this.main = main;
        }

        // Motor Delay Data Load
        private void motorDelay_Load(object sender, EventArgs e)
        {
            gridInit();
            load();
        }

        // grid Initialize
        private void gridInit()
        {
            motorGrid = MotorListGrid;
            motorNames = Enum.GetNames(typeof(MOTOR_DELAY));
            count = motorNames.Length;

            motorGrid.Selection.EnableMultiSelection = false;
            motorGrid.setRowCol(10, 2, true, true);
            motorGrid.setTextAlignment(DevAge.Drawing.ContentAlignment.MiddleCenter);
            motorGrid.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);

            MOTOR_DELAY[] values = (MOTOR_DELAY[])Enum.GetValues(typeof(MOTOR_DELAY));

            setInputGrid(motorNames, motorGrid);
        }

        // Set Input Grid
        private void setInputGrid(string[] name, CSourceGrid grid)
        {
            for (int i = 0; i < grid.ColumnsCount; i++)
            {
                grid.setValue(i, 0, name[i].Replace("_", " "));
                grid.setColors(i, 0, Color.White, Color.Black);
                grid.setValue(i, 1, $"0.0 Sec");

                for (int j = 1; j < grid.ColumnsCount; j++)
                {
                    var controller = new CellClickController();
                    controller.CellClicked += OnDelayValueGrid_Click;
                    grid.cell(j, i).AddController(controller);
                }
            }
        }

        // Delay Value Click
        private void OnDelayValueGrid_Click(object sender, DataGridViewCellEventArgs e)
        {
            
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

        // Load
        private void load()
        {
            
        }
    }
}
