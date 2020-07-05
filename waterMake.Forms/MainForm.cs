using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace waterMake.Forms
{
    public partial class MainForm : MetroForm
    {
        

        Dictionary<string, string> imagePathDic = new Dictionary<string, string>();
        public MainForm()
        {
            
            InitializeComponent();

        }

        private void LB_DragDrop(object sender, DragEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            listBox1.Items.Clear();
            imagePathDic.Clear();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                if (Path.GetExtension(file).Replace(".", "").ToLower() == "png")
                {
                    string tempFileName = Path.GetFileName(file);
                    listBox1.Items.Add(tempFileName);
                    imagePathDic.Add(tempFileName, file);
                }
            }
        }

        private void LB_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void LB_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tempFileName = (sender as ListBox).SelectedItem.ToString();
            pictureBox1.Image = Image.FromFile(imagePathDic[tempFileName]);
            //pictureBox1.Image = Convert_Text_to_Image("테스트", "굴림", 20, FontStyle.Regular);

        }


        private Bitmap Convert_Text_to_Image(string txt, string fontname, int fontsize, FontStyle fontStyle)
        {
            Bitmap bmp = new Bitmap(1, 1);
            Graphics graphics = Graphics.FromImage(bmp);
            Font font = new Font(fontname, fontsize, fontStyle);
            SizeF stringSize = graphics.MeasureString(txt, font);
            bmp = new Bitmap(bmp, (int)stringSize.Width, (int)stringSize.Height);
            graphics = Graphics.FromImage(bmp);
            graphics.DrawString(txt, font, Brushes.Black, 0, 0);
            font.Dispose();
            graphics.Flush();
            graphics.Dispose();
            return bmp;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void MTC_SelectedIndexChangedㅡㅡ(object sender, EventArgs e)
        {

        }

        private void CB_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.CheckedChanged -= CB_CheckedChanged;
            checkBox2.CheckedChanged -= CB_CheckedChanged;
            var name = (sender as CheckBox).Name;
            Console.WriteLine(name);

            if (name == "checkBox1")
            {
                checkBox2.Checked = false;
            }
            else
            {
                checkBox1.Checked = false;
            }
            checkBox1.CheckedChanged += CB_CheckedChanged;
            checkBox2.CheckedChanged += CB_CheckedChanged;
            TB_TextChanged(textBox1, null);
        }

        private void TB_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                label1.Text = (sender as TextBox).Text + "_파일명";
            }
            else
            {
                label1.Text = "파일명_" + (sender as TextBox).Text;
            }
        }
    }
}
