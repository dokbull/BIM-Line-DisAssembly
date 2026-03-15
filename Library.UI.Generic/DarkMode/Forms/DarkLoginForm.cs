using Lib.UI.Generic.DarkMode.Controls;
using Lib.UI.Generic.Icons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lib.UI.Generic.DarkMode.Forms
{
    public enum EnumLoginLanguage
    {
        Korean,
        English,
    }
    
    public partial class DarkLoginForm : Lib.UI.Generic.DarkMode.Forms.DarkForm
    {
        #region Define

        // milliseconds
        private const int c_PermissionCheckedDisplayTime = 500;

        #endregion

        #region Construct

        public DarkLoginForm(List<string> confirmID, List<string> confirmPassword, bool usePermissionCheckedDisplay, EnumLoginLanguage language)
        {
            InitializeComponent();
            this.InitializeUI(confirmID, confirmPassword, usePermissionCheckedDisplay, language);
        }

        /// <param name="language"></param>
        public DarkLoginForm(string confirmID, string confirmPassword, bool usePermissionCheckedDisplay, EnumLoginLanguage language)
            : this(
                  new List<string>() { confirmID },
                  new List<string>() { confirmPassword },
                  usePermissionCheckedDisplay, 
                  language)
        { }

        public DarkLoginForm(List<string> confirmID, List<string> confirmPassword)
            : this(confirmID, confirmPassword, false, EnumLoginLanguage.English)
        { }

        public DarkLoginForm(string confirmID, string confirmPassword)
            : this(confirmID, confirmPassword, false, EnumLoginLanguage.English)
        { }

        public DarkLoginForm(List<string> confirmID, List<string> confirmPassword, EnumLoginLanguage language)
            : this(confirmID, confirmPassword, false, language)
        { }

        public DarkLoginForm(string confirmID, string confirmPassword, EnumLoginLanguage language)
            : this(confirmID, confirmPassword, false, language)
        { }



        public DarkLoginForm(string confirmID, string confirmPassword, bool usePermissionCheckedDisplay)
            : this(confirmID, confirmPassword, usePermissionCheckedDisplay, EnumLoginLanguage.English)
        { }


        public DarkLoginForm(List<string> confirmID, List<string> confirmPassword, bool usePermissionCheckedDisplay)
            : this(confirmID, confirmPassword, usePermissionCheckedDisplay, EnumLoginLanguage.English)
        { }





        #endregion

        #region Member

        private List<string> m_ConfirmID = new List<string>();
        private List<string> m_ConfirmPassword = new List<string>();


        #endregion

        #region Private Method

        private void InitializeUI(List<string> confirmID, List<string> confirmPassword, bool isDisplayPermissionChecked, EnumLoginLanguage language)
        {
            this.picLockIcon.Visible = isDisplayPermissionChecked;
            if(isDisplayPermissionChecked)
            {
                this.picLockIcon.Image = IconImages.GetImage(EnumPermissionIcons.Lock);
            }
            
            this.m_ConfirmID.AddRange(confirmID.ToArray());
            this.m_ConfirmPassword.AddRange(confirmPassword.ToArray());

            this.Icon = IconImages.GetIcon(EnumLoginKeyIcons.LoginKey);
            this.TitleControlBox = false;
            this.TitleButtonVisible1 = true;
            this.TitleButtonVisible2 = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.OptionBoxAlignment = EnumTitleOptionBoxAlignment.Right;

            this.TitleIcon = IconImages.GetImage(EnumLoginKeyIcons.LoginKey);

            switch (language)
            {
                default:
                case EnumLoginLanguage.Korean:
                    this.lblID.Text = "사용자 이름";
                    this.lblPassword.Text = "비밀번호";

                    this.TitleButtonText2 = "확인";
                    this.TitleButtonText1 = "취소";
                    break;

                case EnumLoginLanguage.English:
                    this.lblID.Text = "ID";
                    this.lblPassword.Text = "Password";

                    this.TitleButtonText2 = "OK";
                    this.TitleButtonText1 = "Cancel";
                    break;
            }

            this.TitleButtonClickEvent += DarkMessageBox_TitleButtonClickEvent;
        }


        private async Task<bool> CheckConfirm()
        {
            try
            {
                if (this.txtID.Text.Trim() == string.Empty)  throw new ArgumentException("ID is empty.");
                if (this.txtPassword.Text.Trim() == string.Empty) throw new ArgumentException("Password is empty.");


                if (this.m_ConfirmID.Contains(this.txtID.Text) == false) throw new ArgumentException("ID is not matched.");
                if (this.m_ConfirmPassword.Contains(this.txtPassword.Text) == false) throw new ArgumentException("Password is not matched.");

                if(this.picLockIcon.Visible)
                {
                    this.picLockIcon.Image = IconImages.GetImage(EnumPermissionIcons.LockOK);
                    await Task.Delay(c_PermissionCheckedDisplayTime);

                    this.picLockIcon.Image = IconImages.GetImage(EnumPermissionIcons.LocKOpen);
                    await Task.Delay(c_PermissionCheckedDisplayTime);
                }

                return true;
            }
            catch
            {
                if (this.picLockIcon.Visible)
                {
                    this.picLockIcon.Image = IconImages.GetImage(EnumPermissionIcons.LockError);
                }

                return false;
            }
        }

        #endregion

        #region Event

        private void FormLogin_Load(object sender, EventArgs e)
        {
            this.FormSizable = false;

            this.txtID.Text = string.Empty;
            this.txtPassword.Text = string.Empty;
            this.txtPassword.UseSystemPasswordChar = true;

            this.ActiveControl = this.txtID;
            this.txtID.Focus();
        }


        private async void DarkMessageBox_TitleButtonClickEvent(Controls.EnumTitleButton button)
        {
            // this.OptionBoxAlignment = EnumTitleOptionAlignment.Right;
            // OptionBoxAlignment = Right 이면, Buttin 1-2의 위치가 반대로 된다.

            switch (button)
            {
                default:
                case EnumTitleButton.Button1: //취소
                    this.DialogResult = DialogResult.Cancel;
                    break;
                case EnumTitleButton.Button2: //확인
                    if (await this.CheckConfirm() == false) { return; }

                    this.DialogResult = DialogResult.OK;
                    break;
            }
        }


        private async void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter) { return; }
            if (await this.CheckConfirm() == false) { return; }

            this.DialogResult = DialogResult.OK;
        }

        #endregion
    }
}
