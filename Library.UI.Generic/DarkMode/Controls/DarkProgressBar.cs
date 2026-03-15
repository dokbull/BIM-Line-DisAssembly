using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lib.UI.Generic.ControlFunc;
using System.Runtime.InteropServices;
using static System.Windows.Forms.AxHost;
using System.Drawing.Drawing2D;
using static System.Net.Mime.MediaTypeNames;

namespace Lib.UI.Generic.DarkMode.Controls
{
    public partial class DarkProgressBar : ProgressBar, iDarkMode
    {
        #region Define

        public enum EnumTextDisplay
        {
            None,
            Value,
            ValueWithTotal,
            Percent,
        }
        
        #endregion

        #region Construct

        public DarkProgressBar()
        {
            InitializeComponent();

            this.DarkLevel = this.m_DarkLevel;
            this.Cursor = Cursors.Arrow;

            // 사용자 페인팅 활성화
            this.SetStyle(ControlStyles.UserPaint, true);
            // 이중 버퍼링 활성화
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            // 컨트롤을 다시 그릴 수 있도록 갱신
            this.UpdateStyles();
        }


        #endregion

        #region Member

        #endregion

        #region Properties

        protected int m_DarkLevel = 30;
        [Category("Dark Mode")]
        [DisplayName("Dark Level")]
        public int DarkLevel
        {
            get { return this.m_DarkLevel; }
            set { this.m_DarkLevel = DarkModeFunc.SetDarkMode(value, this); }
        }

        private Color m_ProgressColor = Color.FromArgb(70, 70, 70);
        [Category("Progress Bar")]
        public Color ProgressColor
        {
            get { return this.m_ProgressColor; }
            set
            {
                if (this.m_ProgressColor != value)
                {
                    this.m_ProgressColor = value;
                    this.Invalidate(); // 색상 변경 시 컨트롤 다시 그리기
                }
            }
        }


        private Color m_BorderColor = Color.FromArgb(30,30,30);
        [Category("Progress Bar")]
        public Color BorderColor
        {
            get { return this.m_BorderColor; }
            set
            {
                if (this.m_BorderColor != value)
                {
                    this.m_BorderColor = value;
                    this.Invalidate(); // 색상 변경 시 컨트롤 다시 그리기
                }
            }
        }

        [Category("Progress Bar")]
        public int BorderSize { get; set; } = 1;

        [Category("Progress Bar")]
        public EnumTextDisplay ProgressTextDisplay { get; set; } = EnumTextDisplay.None;

        [Category("Progress Bar")]
        public string ProgressText
        {
            get
            {
                switch (this.ProgressTextDisplay)
                {
                    default:
                    case EnumTextDisplay.None: 
                        return string.Empty; 
                    case EnumTextDisplay.Value: 
                        return $"{this.Value}"; 
                    case EnumTextDisplay.ValueWithTotal: 
                        return $"{this.Value} / {this.Maximum}"; 
                    case EnumTextDisplay.Percent:
                        float percent = ((float)this.Value / (float)this.Maximum) * 100;
                        if (float.IsNaN(percent)) { percent = 0; }

                        return percent.ToString("0.00") + " %"; 
                }
            }
        }

        private Color m_ProgressTextColor = Color.White;
        [Category("Progress Bar")]
        public Color ProgressTextColor
        {
            get { return this.m_ProgressTextColor; }
            set
            {
                if (this.m_ProgressTextColor != value)
                {
                    this.m_ProgressTextColor = value;
                    this.Invalidate(); // 색상 변경 시 컨트롤 다시 그리기
                }
            }
        }

        [Category("Progress Bar")]
        public Font ProgressTextFont { get; set; } = new Font("굴림", 9, FontStyle.Regular);


        #endregion

        #region Private Method

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x04;
                //cp.Style &= ~0x00000001; // WS_BORDER 제거
                return cp;

            }
        }
        private SolidBrush brush = null;

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    if (brush == null || brush.Color != this.ForeColor)
        //        brush = new SolidBrush(this.ForeColor);

        //    Rectangle rec = new Rectangle(0, 0, this.Width, this.Height);
        //    if (ProgressBarRenderer.IsSupported)
        //        ProgressBarRenderer.DrawVerticalBar(e.Graphics, rec);

        //    rec.Height = (int)(rec.Height * ((double)Value / Maximum)) - 4;
        //    rec.Width = rec.Width - 4;
        //    e.Graphics.FillRectangle(brush, 2, 2, rec.Width, rec.Height);

        //}

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = this.ClientRectangle;
            Graphics g = e.Graphics;

            // 배경 색상 설정
            using (SolidBrush backgroundBrush = new SolidBrush(Color.FromArgb(this.DarkLevel, this.DarkLevel, this.DarkLevel)))
            {
                g.FillRectangle(backgroundBrush, rect);
            }

            // 진행 바 색상 설정
            //double percent = (double)this.Value / this.Maximum;
            float percent = (float)(this.Value - this.Minimum) / (this.Maximum - this.Minimum);
            rect.Width = (int)(rect.Width * percent);
            using (SolidBrush progressBrush = new SolidBrush(this.ProgressColor))
            {
                g.FillRectangle(progressBrush, rect);
            }

            // 경계선
            using (Pen pen = new Pen(this.BorderColor, this.BorderSize))
            {
                g.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
            }

            // 텍스트 표시
            using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                // ProgressText에 drawstring으로 그려지는 크기
                SizeF textSize = e.Graphics.MeasureString(this.ProgressText, this.ProgressTextFont);

                if (this.ProgressText != string.Empty && rect.Width < textSize.Width)
                {
                    // 배경색의 보색
                    Color textColor = Color.FromArgb(255 - this.DarkLevel, 255 - this.DarkLevel, 255 - this.DarkLevel);

                    using (SolidBrush textBrush = new SolidBrush(textColor))
                    {

                        // Progress Bar 전체 Width에 대한 Center에 표시
                        e.Graphics.DrawString(this.ProgressText, this.ProgressTextFont, textBrush, this.ClientRectangle, sf);
                    }
                }
                else
                {
                    using (SolidBrush textBrush = new SolidBrush(this.ProgressTextColor))
                    {

                        // 이미 진행된 영역의 Width에 대한 Center에 표시
                        e.Graphics.DrawString(this.ProgressText, this.ProgressTextFont, textBrush, rect, sf);
                    }

                }
            }
        }



        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.Style &= ~0x00000001; // WS_BORDER 제거
        //        return cp;
        //    }
        //}

        #endregion

        #region Public Method

        #endregion

        #region Event
        #endregion



    }
}

