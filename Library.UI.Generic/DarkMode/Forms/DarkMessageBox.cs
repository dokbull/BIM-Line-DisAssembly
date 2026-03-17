using Lib.UI.Generic.DarkMode.Controls;
using Lib.UI.Generic.Icons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Windows.Forms;

namespace Lib.UI.Generic.DarkMode.Forms
{
    public partial class DarkMessageBox : Lib.UI.Generic.DarkMode.Forms.DarkForm
    {
        public DarkMessageBox(string _TitleText)
        {
            InitializeComponent();

            this.TitleControlBox = false;
            this.TitleText = _TitleText;
            this.TitleButtonVisible1 = false;
            this.TitleButtonVisible2 = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.TitleButtonClickEvent += DarkMessageBox_TitleButtonClickEvent;
        }

        public DarkMessageBox(string _TitleText, Dictionary<string, string> _SelectedItems) : this(_TitleText)
        {
            InitializeComponent();

            this.InitializeAddItems(_SelectedItems);
        }


        private EnumMessageBoxButtons m_Buttons = EnumMessageBoxButtons.OK;

        private bool InitializeAddItems(Dictionary<string, string> _Items)
        {
            if (_Items == null || _Items.Count == 0)
                return false;

            this.cmbSelect.Visible = true;

            foreach (var item in _Items)
            {
                this.cmbSelect.Items.Add(new DarkMessageBoxItem(item.Key, item.Value));
            }

            this.cmbSelect.SelectedIndex = 0;

            return true;
        }

        public string Message
        {
            get { return this.lblMessage.Text; }
            set { this.lblMessage.Text = value; }
        }

        public DarkMessageBoxItem SelectedItem { get; set; } = new DarkMessageBoxItem();

        public static DarkMessageBox CreateMessageBox(string _TitleText, EnumMessageBoxIcons icon, string message, EnumMessageBoxButtons buttons)
        {
            DarkMessageBox msgBox = new DarkMessageBox(_TitleText);

            msgBox.picIcon.Image = IconImages.GetImage(icon);
            msgBox.Icon = IconImages.GetIcon(icon);
            msgBox.lblMessage.Text = message;
            msgBox.m_Buttons = buttons;

            // OptionBoxAlignment = Right 이면, Buttin 1-2의 위치가 반대로 된다.
            msgBox.OptionBoxAlignment = EnumTitleOptionBoxAlignment.Right;
            switch (buttons)
            {
                case EnumMessageBoxButtons.OK:
                    msgBox.TitleButtonVisible1 = false;
                    msgBox.TitleButtonVisible2 = true;

                    msgBox.TitleButtonText2 = "OK";
                    break;
                case EnumMessageBoxButtons.OKCancel:
                    msgBox.TitleButtonVisible1 = true;
                    msgBox.TitleButtonVisible2 = true;

                    msgBox.TitleButtonText1 = "Cancel";
                    msgBox.TitleButtonText2 = "OK";
                    break;
                case EnumMessageBoxButtons.YesNo:
                    msgBox.TitleButtonVisible1 = true;
                    msgBox.TitleButtonVisible2 = true;

                    msgBox.TitleButtonText1 = "No";
                    msgBox.TitleButtonText2 = "Yes";
                    break;
                default:
                    break;
            }

            return msgBox;
        }

        public static DarkMessageBox CreateMessageBox(string _TitleText, EnumMessageBoxIcons _Icon, string _Message, EnumMessageBoxButtons _Buttons, Dictionary<string, string> _SelectedItems)
        {
            DarkMessageBox msgBox = DarkMessageBox.CreateMessageBox(_TitleText, _Icon, _Message, _Buttons);
            msgBox.InitializeAddItems(_SelectedItems);

            return msgBox;
        }

        private void DarkMessageBox_TitleButtonClickEvent(Controls.EnumTitleButton button)
        {
            // this.OptionBoxAlignment = EnumTitleOptionAlignment.Right;
            // OptionBoxAlignment = Right 이면, Buttin 1-2의 위치가 반대로 된다.

            switch (this.m_Buttons)
            {
                case EnumMessageBoxButtons.OK:
                    this.DialogResult = DialogResult.OK;
                    break;

                case EnumMessageBoxButtons.OKCancel:
                    //this.TitleButtonText1 = "Cancel";
                    //this.TitleButtonText2 = "OK";
                    switch (button)
                    {
                        default:
                        case EnumTitleButton.Button1:
                            this.DialogResult = DialogResult.Cancel;
                            break;
                        case EnumTitleButton.Button2:
                            this.DialogResult = DialogResult.OK;
                            break;
                    }
                    break;

                case EnumMessageBoxButtons.YesNo:
                    //this.TitleButtonText1 = "No";
                    //this.TitleButtonText2 = "Yes";
                    switch (button)
                    {
                        default:
                        case EnumTitleButton.Button1:
                            this.DialogResult = DialogResult.No;
                            break;
                        case EnumTitleButton.Button2:
                            this.DialogResult = DialogResult.Yes;
                            break;
                    }
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        private void cmbSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbSelect.SelectedItem == null) return;

            this.SelectedItem = this.cmbSelect.SelectedItem as DarkMessageBoxItem;
        }
    }

    public class DarkMessageBoxItem
    {
        public DarkMessageBoxItem()
        {
        }

        public DarkMessageBoxItem(string id, string value)
        {
            this.ID = id;
            this.Text = value;
        }
        public string ID { get; set; } = "0";
        public string Text { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{this.ID} : {this.Text}";
        }

    }
}
