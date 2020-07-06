using System;
using System.Threading;
using System.Windows.Forms;
using waterMake.Forms;
namespace waterMake.Main
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Mutex mutex = new Mutex(true, "waterMake", out bool bnew);
            if (!bnew) return;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TrayForm());
            mutex.ReleaseMutex();
        }
    }
}
