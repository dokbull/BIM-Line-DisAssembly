using Lib.UI.Generic.ControlFunc;
using Lib.UI.Generic.DarkMode.Controls;
using Lib.UI.Generic.Icons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lib.UI.Generic.DarkMode.Forms
{
    public partial class DarkForm : Form, iDarkMode
    {
        #region Define

        [Category("Dark Mode")]
        public event DarkFormTitleButtonClickEventHandler TitleButtonClickEvent;

        #endregion

        #region Construct

        public DarkForm()
        {
            InitializeComponent();

            this.DarkLevel = this.m_DarkLevel;

            this.Title.CloseButtonClickEvent += Title_CloseButtonClickEvent;
            this.Title.MaximumButtonClickEvent += Title_MaximumButtonClickEvent;
            this.Title.MinimumButtonClickEvent += Title_MinimumButtonClickEvent;
            this.Title.TitleMouseDownEvent += Title_TitleMouseDownEvent;
            this.Title.TitleMouseMoveEvent += Title_TitleMouseMoveEvent;
            this.Title.TitleMouseUpEvent += Title_TitleMouseUpEvent;
            this.Title.TitleDoubleClickEvent += Title_TitleDoubleClickEvent;
            this.Title.TitleButtonClickEvent += Title_TitleButtonClickEvent;
        }



        #endregion


        #region Member 

        private int m_DarkLevel = 0;
        private (bool isMoving, int OriginDarkLevel) m_MovingState = (false, 0);

        private int m_FormEdgeLineWidth = 2;

        #endregion

        #region Member : Form 크기 조정

        private Point _mouseDownLocation;
        private bool _isResizing = false;
        private const int ResizeBorderWidth = 10;

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        private const int HTLEFT = 0xA;
        private const int HTRIGHT = 0xB;
        private const int HTTOP = 0xC;
        private const int HTTOPLEFT = 0xD;
        private const int HTTOPRIGHT = 0xE;
        private const int HTBOTTOM = 0xF;
        private const int HTBOTTOMLEFT = 0x10;
        private const int HTBOTTOMRIGHT = 0x11;

        #endregion

        #region Properties

        [Category("Dark Mode")]
        public int DarkLevel
        {
            get { return this.m_DarkLevel; }
            set { this.m_DarkLevel = DarkModeFunc.SetDarkMode(value, this); }
        }

        [Category("Dark Mode")]
        public int TitleDarkLevel
        {
            get { return this.Title.DarkLevel; }
            set { this.Title.DarkLevel = DarkModeFunc.SetDarkMode(value, this.Title); }
        }


        [Category("Dark Form")]
        public bool FormSizable { get; set; } = true;

        [Category("Dark Form - Title")]
        public bool TitleVisible
        {
            get { return this.Title.Visible; }
            set { this.Title.Visible = value; }
        }

        [Category("Dark Form - Title")]
        public new bool ShowIcon
        {
            get { return this.Title.ShowIcon; }
            set { this.Title.ShowIcon = value; }
        }

        [Category("Dark Form - Title")]
        public Image TitleIcon
        {
            get { return this.Title.IconImage; }
            set { this.Title.IconImage = value; }

        }

        [Category("Dark Form - Title")]
        public string TitleText
        {
            get { return this.Title.TitleText; }
            set { this.Title.TitleText = value; }
        }

        [Category("Dark Form - Option Box")]
        public bool OptionBox
        {
            get { return this.Title.OptionBox; }
            set { this.Title.OptionBox = value; }
        }

        [Category("Dark Form - Option Box")]
        [Description("Option Box안에 있는 버튼과 Label의 정렬 방향.\r\n" +
            "Left 일 때의 정렬 순서 = Button1, Button2, Label\r\n" +
            "Right 일 때의 정렬 순서 = Label, Button2, Button1")]        
        public EnumTitleOptionBoxAlignment OptionBoxAlignment
        {
            get { return this.Title.OptionBoxAlignment; }
            set { this.Title.OptionBoxAlignment = value; }
        }

        [Category("Dark Form - Option Box")]
        public bool TitleButtonVisible1
        {
            get { return this.Title.ButtonVisible1; }
            set { this.Title.ButtonVisible1 = value; }
        }

        [Category("Dark Form - Option Box")]
        public string TitleButtonText1
        {
            get { return this.Title.ButtonText1; }
            set { this.Title.ButtonText1 = value; }
        }

        [Category("Dark Form - Option Box")]
        public bool TitleButtonVisible2
        {
            get { return this.Title.ButtonVisible2; }
            set { this.Title.ButtonVisible2 = value; }
        }

        [Category("Dark Form - Option Box")]
        public string TitleButtonText2
        {
            get { return this.Title.ButtonText2; }
            set { this.Title.ButtonText2 = value; }
        }

        [Category("Dark Form - Option Box")]
        public bool TitleLabelVisible
        {
            get { return this.Title.LabelVisible; }
            set { this.Title.LabelVisible = value; }
        }


        [Category("Dark Form - Option Box")]
        public string TitleLabelText
        {
            get { return this.Title.LabelText; }
            set { this.Title.LabelText = value; }
        }


        [Category("Dark Form - Control Box")]
        public bool TitleControlBox
        {
            get { return this.Title.ControlBoxVisible; }
            set { this.Title.ControlBoxVisible = value; }
        }

        [Category("Dark Form - Control Box")]
        public bool TitleMaximumBox
        {
            get { return this.Title.MaximumBox; }
            set { this.Title.MaximumBox = value; }
        }

        [Category("Dark Form - Control Box")]
        public bool TitleMinimumBox
        {
            get { return this.Title.MinimumBox; }
            set { this.Title.MinimumBox = value; }
        }

        [Category("Dark Form - Control Box")]
        public bool TitleCloseBox
        {
            get { return this.Title.CloseBox; }
            set { this.Title.CloseBox = value; }
        }

        #endregion


        #region Private Method : Form 크기 조정

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            _isResizing = false;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (this.FormSizable == false) return;

            if (m.Msg == WM_NCHITTEST && m.Result == (IntPtr)HTCLIENT)
            {
                Point cursor = this.PointToClient(Cursor.Position);

                if (cursor.X <= ResizeBorderWidth && cursor.Y <= ResizeBorderWidth)
                    m.Result = (IntPtr)HTTOPLEFT;
                else if (cursor.X >= this.ClientSize.Width - ResizeBorderWidth && cursor.Y <= ResizeBorderWidth)
                    m.Result = (IntPtr)HTTOPRIGHT;
                else if (cursor.X <= ResizeBorderWidth && cursor.Y >= this.ClientSize.Height - ResizeBorderWidth)
                    m.Result = (IntPtr)HTBOTTOMLEFT;
                else if (cursor.X >= this.ClientSize.Width - ResizeBorderWidth && cursor.Y >= this.ClientSize.Height - ResizeBorderWidth)
                    m.Result = (IntPtr)HTBOTTOMRIGHT;
                else if (cursor.X <= ResizeBorderWidth)
                    m.Result = (IntPtr)HTLEFT;
                else if (cursor.X >= this.ClientSize.Width - ResizeBorderWidth)
                    m.Result = (IntPtr)HTRIGHT;
                else if (cursor.Y <= ResizeBorderWidth)
                    m.Result = (IntPtr)HTTOP;
                else if (cursor.Y >= this.ClientSize.Height - ResizeBorderWidth)
                    m.Result = (IntPtr)HTBOTTOM;
            }
        }

        #endregion

        #region Private Method

        private void SwitchWindowState()
        {
            switch (this.WindowState)
            {
                default:
                case FormWindowState.Normal:
                case FormWindowState.Minimized:
                    this.WindowState = FormWindowState.Maximized;
                    break;
                case FormWindowState.Maximized:
                    this.WindowState = FormWindowState.Normal;
                    break;
            }
        }

        #endregion


        #region Event : Title Drag 시 Form 이동

        private void Title_TitleMouseMoveEvent(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if(this.m_MovingState.isMoving == false)
                {
                    this.m_MovingState.isMoving = true;
                    this.m_MovingState.OriginDarkLevel = this.Title.DarkLevel;
                }

                this.Location = new Point(
                    this.Location.X + e.X - _mouseDownLocation.X,
                    this.Location.Y + e.Y - _mouseDownLocation.Y
                );
            }
        }

        private void Title_TitleMouseDownEvent(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseDownLocation = e.Location;
            }
        }

        private void Title_TitleMouseUpEvent(MouseEventArgs e)
        {
            if (this.m_MovingState.isMoving)
            {
                this.m_MovingState.isMoving = false;
                this.Title.DarkLevel = this.m_MovingState.OriginDarkLevel;
                this.OnPaint(new PaintEventArgs(this.CreateGraphics(), new Rectangle()));
            }
        }

        private void DarkForm_Move(object sender, EventArgs e)
        {
            if (this.m_MovingState.isMoving)
            {
                this.Title.DarkLevel = 255;
                this.OnPaint(new PaintEventArgs(this.CreateGraphics(), new Rectangle()));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (Pen redPen = new Pen(
                (this.m_MovingState.isMoving ? Color.Red : DarkModeFunc.GetDarkModeColor(this.m_DarkLevel).BackColor),
                this.m_FormEdgeLineWidth))
            {
                Debug.WriteLine(redPen.Color.ToString());
                e.Graphics.DrawRectangle(
                    redPen,
                    new Rectangle(0, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1)
                );
            }
        }

        #endregion


        #region Event

        private void Title_MinimumButtonClickEvent()
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Title_MaximumButtonClickEvent()
        {
            this.SwitchWindowState();
        }

        private void Title_TitleDoubleClickEvent()
        {
            this.SwitchWindowState();
        }


        private void Title_CloseButtonClickEvent()
        {
            this.Close();
        }

        private void Title_TitleButtonClickEvent(EnumTitleButton button)
        {
            this.TitleButtonClickEvent?.Invoke(button);
        }


        private void DarkForm_Load(object sender, EventArgs e)
        {
            this.Text = this.Title.TitleText;
        }



        private void DarkMainPanel_ControlAdded(object sender, ControlEventArgs e)
        {

            this.Title.SendToBack();
            this.MainPanel.BringToFront();
        }
        #endregion
    }
}
