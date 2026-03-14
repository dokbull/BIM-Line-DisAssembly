using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace bim_base
{
    public partial class FormOutputTactime : Form
    {
        private DateTime lastOutputTime;
        private bool firstOutput = true;
        private int tactIndex = 0;
        private List<double> tactList = new List<double>();
        System.Windows.Forms.Timer uiTimer = new System.Windows.Forms.Timer();
        private string dataFile = Path.Combine(Application.StartupPath, "TactData.json");
        public static FormOutputTactime Instance { get; private set; }
        public FormOutputTactime(ProcessMain procMain)
        {
            Instance = this;
            InitializeComponent();
            Point check = FormMain.GetLocation();
            this.Location = new Point(FormMain.GetLocation().X + (1280 - this.Width) / 2, FormMain.GetLocation().Y + (1024 - this.Height) / 2); 
            
            InitGrid(dgv1, 1);
            InitGrid(dgv2, 11);
            InitGrid(dgv3, 21);
            LoadTactData();
            UpdateStatistics();

            uiTimer.Interval = 200;
            uiTimer.Tick += (s, e) => { /* placeholder for future realtime UI updates */ };
            uiTimer.Start();
        }
        private void LoadTactData()
        {
            if (File.Exists(dataFile))
            {
                try
                {
                    string json = File.ReadAllText(dataFile);
                    tactList = JsonConvert.DeserializeObject<List<double>>(json) ?? new List<double>();
                    // Fill grids
                    tactIndex = 0;
                    foreach (var t in tactList)
                    {
                        WriteTimeToGrid(tactIndex % 30, t);
                        tactIndex++;
                    }
                    if (tactList.Count > 0)
                    {
                        firstOutput = false;
                        lastOutputTime = DateTime.Now; // or set lastOutputTime = DateTime.Now - TimeSpan.FromSeconds(tactList.Last());
                    }
                }
                catch { tactList = new List<double>(); }
            }
        }

        private void SaveTactData()
        {
            try
            {
                string json = JsonConvert.SerializeObject(tactList, Formatting.Indented);
                File.WriteAllText(dataFile, json);
            }
            catch { /* handle exception if needed */ }
        }
        private void InitGrid(DataGridView dgv, int startNo)
        {
            dgv.ColumnCount = 2;
            dgv.Columns[0].Name = "No";
            dgv.Columns[1].Name = "Time (s)";

            dgv.Columns[0].Width = 68;
            dgv.Columns[1].Width = 128;

            dgv.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[1].DefaultCellStyle.Font = new Font("Segoe UI", 16, FontStyle.Bold);

            dgv.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 76;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 16, FontStyle.Bold);

            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 16);
            dgv.RowTemplate.Height = 53;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            for (int i = 0; i < 10; i++)
            {
                dgv.Rows.Add(startNo + i, "");
            }

            dgv.BackgroundColor = Color.FromArgb(250, 250, 250);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
        }


        // Simulate an output event. Call this when a product is output.
        public void OnProductOutput()
        {
            DateTime now = DateTime.Now;

            if (firstOutput)
            {
                firstOutput = false;
                lastOutputTime = now;
                return;
            }
            double tact = (now - lastOutputTime).TotalSeconds;
            lastOutputTime = now;
            lblCurrent.Text = tact.ToString("0.0") + " s";

            tactList.Add(tact);
            WriteTimeToGrid(tactIndex % 30, tact);
            tactIndex++;
            UpdateStatistics();
            SaveTactData();
        }

        private void WriteTimeToGrid(int index, double time)
        {
            DataGridView target;
            int row;
            if (index < 10)
            {
                target = dgv1;
                row = index;
            }
            else if (index < 20)
            {
                target = dgv2;
                row = index - 10;
            }
            else
            {
                target = dgv3;
                row = index - 20;
            }

            target.Rows[row].Cells[1].Value = time.ToString("0.0");
            target.Rows[row].Cells[1].Style.ForeColor = Color.FromArgb(12, 84, 160); // blue-ish
            target.Rows[row].Cells[1].Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
        }

        private void UpdateStatistics()
        {
            if (tactList.Count == 0)
            {
                lblMax.Text = "-";
                lblMin.Text = "-";
                lblAve.Text = "-";
                lblCurrent.Text = "-";
                return;
            }

            lblMax.Text = tactList.Max().ToString("0.0") + " s";
            lblMin.Text = tactList.Min().ToString("0.0") + " s";
            lblAve.Text = tactList.Average().ToString("0.0") + " s";
        }      
        private void btnReset_Click(object sender, EventArgs e)
        {            
            tactList.Clear();
            tactIndex = 0;
            firstOutput = true;
            lastOutputTime = DateTime.MinValue;
            // clear grids
            ClearGrid(dgv1);
            ClearGrid(dgv2);
            ClearGrid(dgv3);
            UpdateStatistics();
            SaveTactData();
        }

        private void ClearGrid(DataGridView dgv)
        {
            foreach (DataGridViewRow r in dgv.Rows)
                r.Cells[1].Value = "";
        }                
        private void BT_EXIT_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FormOutputTactime_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveTactData();
        }       
    }
}
