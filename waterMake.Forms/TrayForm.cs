using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace waterMake.Forms
{
    public partial class TrayForm : Form
    {
        private static int[] Latest;
        MainForm mf = new MainForm();
        public TrayForm()
        {
            InitializeComponent();

        }

        private void TrayForm_Shown(object sender, EventArgs e)
        {
            Hide();
            mf.Show();
            mf.Activate();
        }



        private void CheckLatestVersion()
        {
            BackgroundWorker versionChecker;
            versionChecker = new BackgroundWorker();
            versionChecker.DoWork += new DoWorkEventHandler(VersionChecker_Dowork);
            versionChecker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(VersionChecker_RunWorkerCompleted);
            Latest = new int[2];
            versionChecker.RunWorkerAsync();
        }

        private void VersionChecker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void VersionChecker_Dowork(object sender, DoWorkEventArgs e)
        {

        }
    }
}
