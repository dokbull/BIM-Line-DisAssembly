using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

public partial class ExtMSChart : Chart
{
    public event EventHandler ChartZoom;

    Label m_selectLabel = new Label();
    Button m_selectButton = new Button();
    Button m_zoomInitButton = new Button();

    List<double> m_dataList = new List<double>();
    
    ContextMenu m_contextMenu = new ContextMenu();

    //Series m_series = null;

    int MAX_X_WIDTH = 13000;

    bool m_isPause = false;

    public ExtMSChart()
    {
        InitializeComponent();

        this.SuspendLayout();

        DoubleBuffered = true;

        m_selectLabel.BackColor = Color.FromArgb(90, 128, 0, 0);
        m_selectLabel.AutoSize = false;
        m_selectLabel.BorderStyle = BorderStyle.FixedSingle;
        m_selectLabel.Visible = false;

        this.Controls.Add(m_selectLabel);

        m_selectButton.Text = "확대";
        m_selectButton.Left = this.Width - m_selectButton.Width - 10;
        m_selectButton.Top = this.Top + 5;
        m_selectButton.Anchor = AnchorStyles.Right | AnchorStyles.Top;
        m_selectButton.BackColor = Color.White;
        //m_selectButton.Visible = true;

        m_selectButton.Click += new EventHandler(selectButton_Clicked);

        this.Controls.Add(m_selectButton);
        
        m_zoomInitButton.Text = "초기화";
        m_zoomInitButton.Left = m_selectButton.Left - m_zoomInitButton.Width - 5;
        m_zoomInitButton.Top = m_selectButton.Top;
        m_zoomInitButton.Anchor = AnchorStyles.Right | AnchorStyles.Top;
        m_zoomInitButton.BackColor = Color.White;
        //m_zoomInitButton.Visible = true;

        m_zoomInitButton.Click += new EventHandler(zoomInitButton_Clicked);

        this.Controls.Add(m_zoomInitButton);

        this.MouseWheel += new MouseEventHandler(mouseWheelEvent);

        this.ResumeLayout();

        MenuItem menuPause = new MenuItem("일시 중지");
        MenuItem menuResume = new MenuItem("다시 시작");

        menuPause.Click += new EventHandler(pauseHandler);
        menuResume.Click += new EventHandler(resumeHandler);

        m_contextMenu.MenuItems.Add(menuPause);
        m_contextMenu.MenuItems.Add(menuResume);

        this.ContextMenu = m_contextMenu;

    }

    private void ExtMSChart_MouseMove(object sender, MouseEventArgs e)
    {
        Chart ptrChart = (Chart)sender;
        double selX, selY;
        selX = selY = 0;

        try
        {
            selX = ptrChart.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X);
            selY = ptrChart.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y);

        }
        catch (Exception ex) 
        {
            Debug.warning("ExtMSChart::MouseMove error reason:" + ex.Message + 
                " stackTrace:" + ex.StackTrace);
            return; 
        }
    }

    private void ExtMSChart_Resize(object sender, EventArgs e)
    {
        
    }

    private void selectButton_Clicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        if (ChartZoom != null)
            ChartZoom(button.Parent, e);
    }

    private void zoomInitButton_Clicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        ExtMSChart chart = (ExtMSChart)button.Parent;

        chart.ChartAreas[0].AxisX.ScaleView.ZoomReset(1);
        chart.ChartAreas[0].AxisY.ScaleView.ZoomReset(1);
    }

    public void setZoomButtonText(string text)
    {
        m_selectButton.Text = text;
    }

    public void mouseWheelEvent(object sender, MouseEventArgs e)
    {
        //Debug.debug("######## e:" + e.Delta);

#if false
        if (e.Delta > 0)
            this.ChartAreas[0].AxisY.ScaleView.
        else
            this.ChartAreas[0].AxisY.ScaleView.Position--;
#endif
    }

    private void pauseHandler(object sender, EventArgs e)
    {
        m_isPause = true;

        this.ChartAreas[0].AxisX.Maximum = this.Series[0].Points.Count;
    }

    private void resumeHandler(object sender, EventArgs e)
    {
        m_isPause = false;

        this.ChartAreas[0].RecalculateAxesScale();

        while (this.ChartAreas[0].AxisX.ScaleView.IsZoomed)
            this.ChartAreas[0].AxisX.ScaleView.ZoomReset();

        while (this.ChartAreas[0].AxisY.ScaleView.IsZoomed)
            this.ChartAreas[0].AxisY.ScaleView.ZoomReset();
    }

    public bool pause()
    {
        return m_isPause;
    }

    public void add(double yValue)
    {
        if (this.Series.Count == 0)
            return;

        this.Series[0].Points.Add(yValue);

        int count = this.Series[0].Points.Count;

        if (m_isPause == false)
        {
            if (count < MAX_X_WIDTH)
                this.ChartAreas[0].AxisX.Maximum = count + 1;
        }
    }

    public void add(DataPoint dataPoint)
    {
        if (this.Series.Count == 0)
            return;

        this.Series[0].Points.Add(dataPoint);

        //DataPointCollection points = this.Series[0].Points;

        int count = this.Series[0].Points.Count;

        if (m_isPause == false)
        {
            if (count < MAX_X_WIDTH)
                this.ChartAreas[0].AxisX.Maximum = count + 1;
        }
    }

    public void calculateUI()
    {
    }

    private void InitializeComponent()
    {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // ExtMSChart
            // 
            this.BorderlineColor = System.Drawing.Color.Black;
            this.MouseLeave += new System.EventHandler(this.ExtMSChart_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ExtMSChart_MouseMove_1);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

    }

    private void ExtMSChart_MouseLeave(object sender, EventArgs e)
    {
    }

    private void ExtMSChart_MouseMove_1(object sender, MouseEventArgs e)
    {
    }
}
