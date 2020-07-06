using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using waterMake.Settings;
using FontFamily = System.Drawing.FontFamily;

namespace waterMake.Forms
{
    public partial class MainForm : MetroForm
    {
        

        FontDialog fd = new FontDialog();
        Dictionary<string, string> imagePathDic = new Dictionary<string, string>();
        public MainForm()
        {
            fd.ShowEffects = false;
            fd.Font = new Font(Settings.Settings.FontName, Settings.Settings.FontSize, (FontStyle)GetFontStyle(Settings.Settings.FontStyle));
            InitializeComponent();
            textBox1.Text = Settings.Settings.FontColor.ToString("X");
            numericUpDown1.Value = Settings.Settings.FontOutline;
            checkBox3.Checked = Settings.Settings.Auto;
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
                var tempPathExt = Path.GetExtension(file).Replace(".", "").ToLower();
                if (tempPathExt.Contains("png") || tempPathExt.Contains("jpg"))
                {
                    if (checkBox3.Checked)
                    {
                        var image1 = Image.FromFile(file);
                        var image2 = Convert_Text_to_Image(textBox2.Text);

                        var image3 = OverWriteShape(image2, image1, new Size(100, 100));
                        image1.Dispose();
                        image2.Dispose();
                        image3.Save($"[워터마크]{Path.GetFileNameWithoutExtension(file)}.png");
                        image3.Dispose();
                    }
                    else
                    {
                        string tempFileName = Path.GetFileName(file);
                        listBox1.Items.Add(tempFileName);
                        imagePathDic.Add(tempFileName, file);
                    }
                }
            }
        }

        private void LB_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
        
        private void LB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            var tempFileName = (sender as ListBox).SelectedItem.ToString();
            pictureBox1.Image = Image.FromFile(imagePathDic[tempFileName]);
            //pictureBox1.Image = Convert_Text_to_Image("테스트", "굴림", 20, FontStyle.Regular);

        }

        /// <summary>
		/// 이미지를 합성합니다.
		/// </summary>
		/// <param name="Shape">위에 합성할 이미지 클래스</param>
		/// <param name="background">배경으로 쓰일 이미지 클래스</param>
		/// <param name="Pos">합성될 이미지가 위치할 좌표. Shape의 중심점기준</param>
		/// <returns></returns>
		public Image OverWriteShape(Image Shape, Image background, Size Pos)
        {
            Image img = new Bitmap(background);
            PointF ImagePos = new PointF(10, 10);


            Graphics formGraphics = Graphics.FromImage(img);


            formGraphics.DrawImage(Shape, ImagePos);

            formGraphics.Save();
            formGraphics.Dispose();
            return img;
        }

        /// <summary>
        /// 문자열을 이미지로 바꿔줍니다
        /// </summary>
        /// <param name="txt">문자열</param>
        /// <param name="fontname">폰트이름</param>
        /// <param name="fontsize">폰트사이즈</param>
        /// <param name="fontStyle">폰트스타일</param>
        /// <returns></returns>
        private Bitmap Convert_Text_to_Image(string txt)
        {
            Bitmap bmp = new Bitmap(1, 1);
            Graphics graphics = Graphics.FromImage(bmp);
            SizeF stringSize = graphics.MeasureString(txt, fd.Font);
            bmp = new Bitmap(bmp, (int)(stringSize.Width * 1.2) , (int)(stringSize.Height * 1.2));
            graphics = Graphics.FromImage(bmp);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            //graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            //Brush testBrush = new SolidBrush(Color.FromArgb(125, Color.Black));
            //graphics.DrawString(txt, fd.Font, testBrush, 0, 0);
            
            //SizeF a;
            //a = TextRenderer.MeasureText(txt, fd.Font);
            //TextRenderer.DrawText(graphics, txt, Font, new Point(0, 0), fd.Color);
            //graphics.DrawRectangle(new Pen(fd.Color), 0, 0, a.Width, a.Height);

            // assuming g is the Graphics object on which you want to draw the text
            GraphicsPath p = new GraphicsPath();
            p.AddString(
                txt,             // text to draw
                new FontFamily(fd.Font.Name),  // or any other font family
                (int)fd.Font.Style,      // font style (bold, italic, etc.)
                (float)fd.Font.Height * fd.Font.FontFamily.GetEmHeight(fd.Font.Style) / fd.Font.FontFamily.GetLineSpacing(fd.Font.Style),       // em size
                new Point(0, 0),              // location where to draw text
                new StringFormat());          // set options here (e.g. center alignment)
            graphics.DrawPath(new Pen(Color.FromArgb(Settings.Settings.FontColor), (float)numericUpDown1.Value), p);
            // + g.FillPath if you want it filled as well

            //font.Dispose();
            graphics.Flush();
            graphics.Dispose();

            


            return CropImage(bmp);
        }

        private int GetFontStyle(int style)
        {
            if (style == 3)
            {
                return 1|2;
            }
            return style;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
            Settings.Settings.Auto = (sender as CheckBox).Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            var tempFileName = listBox1.SelectedItem.ToString();
            var image1 = Image.FromFile(imagePathDic[tempFileName]);
            var image2 = Convert_Text_to_Image(textBox2.Text);
            //image2.Save("test2.png");
            
            var image3 = OverWriteShape(image2, image1, new Size(100, 100));
            image1.Dispose();
            image2.Dispose();
            image3.Save($"[워터마크]{Path.GetFileNameWithoutExtension(imagePathDic[listBox1.SelectedItem.ToString()])}.png");
            image3.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    Settings.Settings.FontName = fd.Font.Name;
                    Settings.Settings.FontStyle = (int)fd.Font.Style;
                    Settings.Settings.FontSize = (int)fd.Font.Size;
                }
            }
            catch (Exception e1)
            {

            }
        }
        

        private Bitmap CropImage(Bitmap bitmap)
        {
            var w = bitmap.Width;
            var h = bitmap.Height;

            var left = w / 2;
            var right = w / 2;
            var top = h / 2;
            var bottom = h / 2;

            var color = 0x000000;

            for (int i1 = 0; i1 < h; i1++) // h
            {
                for (int i2 = 0; i2 < w; i2++) // w
                {
                    //Console.WriteLine(bitmap.GetPixel(i2, i1).ToArgb());
                    if (bitmap.GetPixel(i2, i1).ToArgb() != color)
                    {
                        if (left > i2)
                        {
                            left = i2;
                        }
                        if (right < i2)
                        {
                            right = i2;
                        }
                        if (top > i1)
                        {
                            top = i1;
                        }
                        if (bottom < i1)
                        {
                            bottom = i1;
                        }
                    }
                }
            }
            left -= 3;
            right += 3;
            top -= 3;
            bottom += 3;


            var target = new Bitmap(right - left, bottom - top);
            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(bitmap,
                  new RectangleF(0, 0, right - left, bottom - top),
                  new RectangleF(left, top, right - left, bottom - top),
                  GraphicsUnit.Pixel);
            }
            return target;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).TextLength == 8)
            {
                var tempstr = "ABCDEFabcdef0123456789";
                foreach (var c in (sender as TextBox).Text.Trim())
                {
                    if (!tempstr.Contains(c))
                    {
                        return;
                    }
                }

                Settings.Settings.FontColor = int.Parse(textBox1.Text, System.Globalization.NumberStyles.HexNumber);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Settings.Settings.FontOutline = (int)(sender as NumericUpDown).Value;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Settings.Settings.WaterMarkString = (sender as TextBox).Text;
        }
    }
}
