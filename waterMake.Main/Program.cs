using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using waterMake.Forms;
namespace waterMake.Main
{
    static class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool ChangeWindowMessageFilter(uint message, uint dwFlag);

        private const uint WM_DROPFILES = 0x233;
        private const uint WM_COPYDATA = 0x004A;
        private const uint WM_COPYGLOBALDATA = 0x0049;
        private const uint MSGFLT_ADD = 1;

        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ChangeWindowMessageFilter(WM_DROPFILES, MSGFLT_ADD);
            ChangeWindowMessageFilter(WM_COPYDATA, MSGFLT_ADD);
            ChangeWindowMessageFilter(WM_COPYGLOBALDATA, MSGFLT_ADD);

            Mutex mutex = new Mutex(true, "waterMake", out bool bnew);
            if (!bnew) return;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TrayForm());
            mutex.ReleaseMutex();
        }
    }
}
