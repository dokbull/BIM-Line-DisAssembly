using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;

public partial class CChart : UserControl
{
    CChartSetting.SettingValue m_value = null;

    StripLine m_upperStrip = new StripLine { BorderColor = Color.Red, Text = "upper" } ;
    StripLine m_lowerStrip = new StripLine { BorderColor = Color.Green, Text = "lower" } ;

    string m_path = "";
    
    public CChart()
    {
        InitializeComponent();

        m_path = pathUtil.savePath() + "\\chart\\" + Name.ToString();

        chart1.Series[0].Color = Color.Red;
        chart1.Series[0].BorderWidth = 3;
        chart1.Series[0].ChartType = SeriesChartType.FastLine;
        chart1.Series[0].Name = "value";

        chart1.Series[0].Points.AddY(1);
        chart1.Series[0].Points.Clear();
    }

    private void Ext2MSChart_Load(object sender, EventArgs e)
    {
        m_value = new CChartSetting.SettingValue(this.Name, m_path);
        
        chart1.ChartAreas[0].AxisY.StripLines.Add(m_lowerStrip);
        chart1.ChartAreas[0].AxisY.StripLines.Add(m_upperStrip);

        m_value.loadSetting();
        updateSetting();

        chart1.Legends[0].DockedToChartArea = chart1.ChartAreas[0].Name;

        chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(223, 223, 223);
        chart1.ChartAreas[0].AxisX.Minimum = 1;
        chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Number;
        chart1.ChartAreas[0].AxisX.LabelStyle.Format = "0";

        chart1.ChartAreas[0].AxisX.ScrollBar.Size = 15;
        chart1.ChartAreas[0].AxisX.ScrollBar.BackColor = Color.FromArgb(240, 240, 240);
        chart1.ChartAreas[0].AxisX.ScrollBar.ButtonColor = Color.White;
        chart1.ChartAreas[0].AxisX.ScrollBar.LineColor = Color.Gray;

        chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(223, 223, 223);

        chart1.ChartAreas[0].AxisY.ScrollBar.Size = 15;
        chart1.ChartAreas[0].AxisY.ScrollBar.BackColor = Color.FromArgb(240, 240, 240);
        chart1.ChartAreas[0].AxisY.ScrollBar.ButtonColor = Color.White;
        chart1.ChartAreas[0].AxisY.ScrollBar.LineColor = Color.Gray;

        // Cursor
    }

    private void button1_Click(object sender, EventArgs e)
    {
        CChartSetting form = new CChartSetting(this.Name, m_path);
        DialogResult result = form.ShowDialog();

        if (result == DialogResult.OK)
        {
            m_value.loadSetting();
            updateSetting();
        }
    }

    private void updateSetting()
    {
        Axis axisX = chart1.ChartAreas[0].AxisX;
        Axis axisY = chart1.ChartAreas[0].AxisY;

#if false
        float center = (m_value.axisY.max - m_value.axisY.min)/2;
        float min = m_value.axisY.min - center;
        float max = m_value.axisY.max + center;
#else
        float min = m_value.axisY.min;
        float max = m_value.axisY.max;
#endif

#if true
        if (axisY.Minimum != min)
            axisY.Minimum = min;

        if (axisY.Maximum != max)
            axisY.Maximum = max;
#endif
        //axisY.ScaleView.Zoom(20, 30);// m_value.axisY.min, m_value.axisY.max);

        if (m_lowerStrip.IntervalOffset != m_value.axisY.lower)
            m_lowerStrip.IntervalOffset = m_value.axisY.lower;

        if (m_upperStrip.IntervalOffset != m_value.axisY.upper)
            m_upperStrip.IntervalOffset = m_value.axisY.upper;

        if (axisY.MajorGrid.Interval != m_value.axisY.majorTick)
            axisY.MajorGrid.Interval = m_value.axisY.majorTick;

        if (axisY.MinorGrid.Interval != m_value.axisY.minorTick)
            axisY.MinorGrid.Interval = m_value.axisY.minorTick;

        calcAxisX();
    }

    public void calcAxisX()
    {
        if (m_value == null)
            return;

        int pointCount = chart1.Series[0].Points.Count;
        int count = m_value.axisX.autoScrollCount;

        if (pointCount > count)
            chart1.ChartAreas[0].AxisX.ScaleView.Zoom(pointCount - count, pointCount);
        else
        {
            if (pointCount < count)
                chart1.ChartAreas[0].AxisX.Maximum = count;

            //chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
        }

        try
        {
            chart1.ChartAreas[0].RecalculateAxesScale();
        }
        catch (Exception e)
        {
            Debug.debug("CChart::calcAxisY error:" + e.Message);
        }
    }

    public StripLine upperLimit()
    {
        return m_upperStrip;
    }

    public StripLine lowerLimit()
    {
        return m_lowerStrip;
    }

    private void chart1_Click(object sender, EventArgs e)
    {
    }

    public SeriesCollection Series
    {
        get { return chart1.Series; }
    }

    public ChartAreaCollection ChartAreas
    {
        get { return chart1.ChartAreas; }
    }
}
