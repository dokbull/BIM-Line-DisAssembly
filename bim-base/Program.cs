using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bim_base
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>

        static Mutex mutex = new Mutex(true, "se_base");

        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true)) //중복 실행 방지
            {
                // 커스텀 디렉토리 적용
                pathUtil.setCustomPath("C:\\FA\\" + Common.TITLE);
                Common.setCustomPath(pathUtil.savePath());

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
            }
            else
            {
                CMessageBox msgBox = new CMessageBox("Wingplate", "Program is already running.\r\n프로그램이 이미 실행중입니다.", MessageBoxButtons.OK);
                msgBox.ShowDialog();
            }
        }
    }
}
