using System;
using System.Windows.Forms;

namespace waterMake.Forms
{
    public partial class TrayForm : Form
    {
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
    }
}
