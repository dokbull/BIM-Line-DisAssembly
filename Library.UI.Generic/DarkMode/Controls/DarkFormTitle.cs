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

namespace Lib.UI.Generic.DarkMode.Controls
{
    public delegate void DarkFormTitleMinimumButtonClickEventHandler();
    public delegate void DarkFormTitleMaximumButtonClickEventHandler();
    public delegate void DarkFormTitleCloseButtonClickEventHandler();
    public delegate void DarkFormTitleMouseDownEventHandler(MouseEventArgs e);
    public delegate void DarkFormTitleMouseMoveEventHandler(MouseEventArgs e);
    public delegate void DarkFormTitleMouseUpEventHandler(MouseEventArgs e);
    public delegate void DarkFormTitleDoubleClickEventHandler();
    public delegate void DarkFormTitleButtonClickEventHandler(EnumTitleButton button);

    internal partial class DarkFormTitle: UserControl, iDarkMode
    {
        #region Define

        [Category("Dark Mode")]
        public event DarkFormTitleMinimumButtonClickEventHandler MinimumButtonClickEvent;
        [Category("Dark Mode")]
        public event DarkFormTitleMaximumButtonClickEventHandler MaximumButtonClickEvent;
        [Category("Dark Mode")]
        public event DarkFormTitleCloseButtonClickEventHandler CloseButtonClickEvent;
        [Category("Dark Mode")]
        public event DarkFormTitleMouseDownEventHandler TitleMouseDownEvent;
        [Category("Dark Mode")]
        public event DarkFormTitleMouseMoveEventHandler TitleMouseMoveEvent;
        [Category("Dark Mode")]
        public event DarkFormTitleMouseUpEventHandler TitleMouseUpEvent;
        [Category("Dark Mode")]
        public event DarkFormTitleDoubleClickEventHandler TitleDoubleClickEvent;
        [Category("Dark Mode")]
        public event DarkFormTitleButtonClickEventHandler TitleButtonClickEvent;

        #endregion

        #region Construct

        public DarkFormTitle()
        {
            InitializeComponent();

            this.DarkLevel = this.m_DarkLevel;

            this.ButtonVisible1 = false;
            this.LabelVisible = false;

            this.btnTitleButton1.Tag = EnumTitleButton.Button1;
            this.btnTitleButton2.Tag = EnumTitleButton.Button2;

        }

        #endregion

        #region Member 

        private int m_DarkLevel = 0;

        private bool m_ShowIcon = false;
        private Image m_IconImage = null;
        private string m_TitleText = string.Empty;

        private bool m_OptionBoxVisible = false;
        private EnumTitleOptionBoxAlignment m_OptionBoxAlignment = EnumTitleOptionBoxAlignment.Left;
        
        private bool m_TextBoxVisible = false;
        private string m_TextBoxText = string.Empty;
        private int m_TextBoxWidth = 21;

        private bool m_ButtonVisible1 = false;
        private bool m_ButtonVisible2 = false;
        private string m_ButtonText1 = "Button1";
        private string m_ButtonText2 = "Button2";

        private bool m_LabelVisible = false;
        private string m_LabelText = "Label";

        private bool m_ControlboxVisible = false;
        private bool m_MinBoxVisible = false;
        private bool m_MaxBoxVisible = false;
        private bool m_CloseBoxVisible = false;

        #endregion

        #region Properties

        [Category("Dark Mode")]
        public int DarkLevel
        {
            get
            {
                return this.m_DarkLevel;
            }
            set
            {

                this.m_DarkLevel = DarkModeFunc.SetDarkMode(value, this);

                this.pnlTitle.DarkLevel = DarkModeFunc.SetDarkMode(value, this.pnlTitle);
                this.picIcon.DarkLevel = DarkModeFunc.SetDarkMode(value, this.picIcon);
                this.lblTitleText.DarkLevel = DarkModeFunc.SetDarkMode(value, this.lblTitleText);

                this.pnlOption.DarkLevel = DarkModeFunc.SetDarkMode(value, this.pnlOption);
                this.txtTitleTextBox.DarkLevel = DarkModeFunc.SetDarkMode(value + 40, this.txtTitleTextBox);
                this.btnTitleButton1.DarkLevel = DarkModeFunc.SetDarkMode(value, this.btnTitleButton1);
                this.btnTitleButton2.DarkLevel = DarkModeFunc.SetDarkMode(value, this.btnTitleButton2);
                this.lblTitleLabel.DarkLevel = DarkModeFunc.SetDarkMode(value + 40, this.lblTitleLabel);

                this.pnlControlBox.DarkLevel = DarkModeFunc.SetDarkMode(value, this.pnlControlBox);
                this.btnMinBox.DarkLevel = DarkModeFunc.SetDarkMode(value, this.btnMinBox);
                this.btnMaxBox.DarkLevel = DarkModeFunc.SetDarkMode(value, this.btnMaxBox);
                this.btnClose.DarkLevel = DarkModeFunc.SetDarkMode(value, this.btnClose);
            }
        }

        [Category("Title")]
        public bool ShowIcon
        {
            get
            {
                return this.m_ShowIcon;
            }
            set
            {
                this.m_ShowIcon = value;
                this.picIcon.Visible = value;
            }
        }

        [Category("Title")]
        public Image IconImage
        {
            get
            {
                return this.m_IconImage;
            }
            set
            {
                this.picIcon.Image = value;
                this.m_IconImage = this.picIcon.Image;
            }
        }


        [Category("Title")]
        public string TitleText
        {
            get
            {
                return this.m_TitleText;
            }
            set
            {
                this.m_TitleText = value;
                this.lblTitleText.Text = value;
            }
        }

        [Category("Option Box")]
        public bool OptionBox
        {
            get
            {
                return this.m_OptionBoxVisible;
            }
            set
            {
                this.m_OptionBoxVisible = value;
                this.pnlOption.Visible = value;
            }
        }

        [Category("Option Box")]
        [Description("Option Box안에 있는 버튼과 Label의 정렬 방향.\r\n" +
            "Left 일 때의 정렬 순서 = Button1, Button2, Label\r\n" +
            "Right 일 때의 정렬 순서 = Label, Button2, Button1")]
        public EnumTitleOptionBoxAlignment OptionBoxAlignment
        {
            get
            {
                return this.m_OptionBoxAlignment;
            }
            set
            {
                this.m_OptionBoxAlignment = value;

                switch (value)
                {
                    default:
                    case EnumTitleOptionBoxAlignment.Left:
                        this.pnlOption.FlowDirection = FlowDirection.LeftToRight;
                        break;
                    case EnumTitleOptionBoxAlignment.Right:
                        this.pnlOption.FlowDirection = FlowDirection.RightToLeft;
                        break;
                }
            }
        }

        [Category("Option Box")]
        public bool TextBoxVisible
        {
            get
            {
                return this.m_TextBoxVisible;
            }
            set
            {
                this.m_TextBoxVisible = value;
                this.txtTitleTextBox.Visible = value;
            }
        }

        [Category("Option Box")]
        public string TextBoxText
        {
            get
            {
                return this.m_TextBoxText;
            }
            set
            {
                this.m_TextBoxText = value;
                this.txtTitleTextBox.Text = value;
            }
        }

        [Category("Option Box")]
        public int TextBoxWidth
        {
            get
            {
                return this.m_TextBoxWidth;
            }
            set
            {
                this.m_TextBoxWidth = value;
                this.txtTitleTextBox.Width = value;
            }
        }

        [Category("Option Box")]
        public bool ButtonVisible1
        {
            get
            {
                return this.m_ButtonVisible1;
            }
            set
            {
                this.m_ButtonVisible1 = value;
                this.btnTitleButton1.Visible = value;
            }
        }

        [Category("Option Box")]
        public bool ButtonVisible2
        {
            get
            {
                return this.m_ButtonVisible2;
            }
            set
            {
                this.m_ButtonVisible2 = value;
                this.btnTitleButton2.Visible = value;
            }
        }

        [Category("Option Box")]
        public string ButtonText1
        {
            get
            {
                return this.m_ButtonText1;
            }
            set
            {
                this.m_ButtonText1 = value;
                this.btnTitleButton1.Text = value;
            }
        }

        [Category("Option Box")]
        public string ButtonText2
        {
            get
            {
                return this.m_ButtonText2;
            }
            set
            {
                this.m_ButtonText2 = value;
                this.btnTitleButton2.Text = value;
            }
        }

        [Category("Option Box")]
        public bool LabelVisible
        {
            get
            {
                return this.m_LabelVisible;
            }
            set
            {
                this.m_LabelVisible = value;
                this.lblTitleLabel.Visible = value;
            }
        }

        [Category("Option Box")]
        public string LabelText
        {
            get
            {
                return this.m_LabelText;
            }
            set
            {
                this.m_LabelText = value;
                this.lblTitleLabel.Text = value;
            }
        }


        [Category("Control Box")]
        public bool ControlBoxVisible
        {
            get { return this.m_ControlboxVisible; }
            set
            {
                this.m_ControlboxVisible = value;
                this.pnlControlBox.Visible = value;
            }
        }

        [Category("Control Box")]
        public bool MaximumBox
        {
            get { return this.m_MaxBoxVisible; }
            set
            {
                this.m_MaxBoxVisible = value;
                this.btnMaxBox.Visible = value;
            }
        }

        [Category("Control Box")]
        public bool MinimumBox
        {
            get { return this.m_MinBoxVisible; }
            set
            {
                this.m_MinBoxVisible = value;
                this.btnMinBox.Visible = value;
            }
        }

        [Category("Control Box")]
        public bool CloseBox
        {
            get { return this.m_CloseBoxVisible; }
            set
            {
                this.m_CloseBoxVisible = value;
                this.btnClose.Visible = value;
            }
        }

        #endregion

        #region Event

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.CloseButtonClickEvent?.Invoke();
        }

        private void btnMaxBox_Click(object sender, EventArgs e)
        {
            this.MaximumButtonClickEvent?.Invoke();
        }

        private void btnMinBox_Click(object sender, EventArgs e)
        {
            this.MinimumButtonClickEvent?.Invoke();
        }

        private void Title_MouseDown(object sender, MouseEventArgs e)
        {
            this.TitleMouseDownEvent?.Invoke(e); 
        }

        private void Title_MouseMove(object sender, MouseEventArgs e)
        {
            this.TitleMouseMoveEvent?.Invoke(e);
        }

        private void Title_DoubleClickEvent(object sender, EventArgs e)
        {
            this.TitleDoubleClickEvent?.Invoke();
        }

        private void Title_MouseUp(object sender, MouseEventArgs e)
        {
            this.TitleMouseUpEvent?.Invoke(e);
        }


        private void btnTitleButton2_Click(object sender, EventArgs e)
        {
            if ((sender is DarkButton btn) == false) return;
            if (btn.Tag == null) return;
            if ((btn.Tag is EnumTitleButton buttonNo) == false) return;

            this.TitleButtonClickEvent?.Invoke(buttonNo);
        }
        
        #endregion
    }
}