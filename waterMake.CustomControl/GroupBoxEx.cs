using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace waterMake.CustomControl
{
    public class GroupBoxEx : GroupBox
    {
        private Color borderColor = Color.Black;

        public enum titlealign
        {
            Left = 0,
            Right = 1,
            Center = 2
        }
        private titlealign basetitlealign = titlealign.Center;
        [DefaultValue(typeof(titlealign), "Center")]
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; this.Invalidate(); }
        }
        private Color textColor = Color.Black;
        [DefaultValue(typeof(Color), "Black")]

        public Color TextColor
        {
            get { return textColor; }
            set { textColor = value; this.Invalidate(); }
        }

        public titlealign TitleAlign
        {
            get { return basetitlealign; }
            set { basetitlealign = value; this.Invalidate(); }
        }




        protected override void OnPaint(PaintEventArgs e)
        {
            GroupBoxState state = base.Enabled ? GroupBoxState.Normal :
                GroupBoxState.Disabled;
            //TextFormatFlags flags = TextFormatFlags.PreserveGraphicsTranslateTransform |
            //                        TextFormatFlags.PreserveGraphicsClipping |
            //                        TextFormatFlags.TextBoxControl |
            //                        TextFormatFlags.HorizontalCenter |
            //                        TextFormatFlags.WordBreak;
            titlealign flags = titlealign.Center;

            Color titleColor = this.TextColor;

            //if (!this.ShowKeyboardCues)
            //    flags |= TextFormatFlags.HidePrefix;
            if (this.TitleAlign == titlealign.Left)
                flags = titlealign.Left;
            if (this.TitleAlign == titlealign.Right)
                flags = titlealign.Right;
            if (this.TitleAlign == titlealign.Center)
                flags = titlealign.Center;

            if (!this.Enabled)
                titleColor = SystemColors.GrayText;
            DrawUnthemedGroupBoxWithText(e.Graphics, new Rectangle(0, 0, base.Width,
                base.Height), this.Text, this.Font, titleColor, flags, state);
            RaisePaintEvent(this, e);
        }
        private void DrawUnthemedGroupBoxWithText(Graphics g, Rectangle bounds,
            string groupBoxText, Font font, Color titleColor,
            titlealign flags, GroupBoxState state)
        {

            Rectangle rectangle = bounds;
            rectangle.Width -= 8;
            Size size = TextRenderer.MeasureText(g, groupBoxText, font, new Size(rectangle.Width, rectangle.Height), TextFormatFlags.Default);
            rectangle.Width = size.Width;
            rectangle.Height = size.Height;
            if (flags == titlealign.Left)
                rectangle.X += 5;
            else if (flags == titlealign.Right)
                rectangle.X = (bounds.Width - rectangle.Width) - 5; // 센터
            else if (flags == titlealign.Center)
                rectangle.X = ((bounds.X + bounds.Width) - rectangle.Width) / 2; // 센터
            TextRenderer.DrawText(g, groupBoxText, font, rectangle, titleColor, TextFormatFlags.Default);
            if (rectangle.Width > 0)
                rectangle.Inflate(2, 0);
            using (var pen = new Pen(this.BorderColor))
            {
                //System.Console.WriteLine("이미지그리기 시작");
                int num = bounds.Top + (font.Height / 2);
                g.DrawLine(pen, bounds.Left, num - 1, bounds.Left, bounds.Height - 2); // 왼쪽라인
                                                                                       //System.Console.WriteLine(string.Format("x1 = {0}\t\t\ty1 = {1}", bounds.Left - bounds.Left, (bounds.Height - 2) - (num - 1)));

                g.DrawLine(pen, bounds.Left, bounds.Height - 2, bounds.Width - 2, bounds.Height - 2); // 아래라인
                                                                                                      //System.Console.WriteLine(string.Format("x1 = {0}\t\t\ty1 = {1}", (bounds.Width - 2) - bounds.Left, (bounds.Height - 2) - (bounds.Height - 2)));

                g.DrawLine(pen, bounds.Left, num - 1, rectangle.X + 3, num - 1);  // 글씨 왼쪽라인
                                                                                  //System.Console.WriteLine(string.Format("x1 = {0}\t\t\ty1 = {1}", (rectangle.X - 3) - bounds.Left, (num - 1) - (num - 1)));

                g.DrawLine(pen, rectangle.X + rectangle.Width - 5, num - 1, bounds.Width - 2, num - 1); // 글씨 오른쪽라인
                                                                                                        //System.Console.WriteLine(string.Format("x1 = {0}\t\t\ty1 = {1}", (bounds.Width - 2) - (rectangle.X + rectangle.Width + 2), (num - 1) - (num - 1)));

                g.DrawLine(pen, bounds.Width - 2, num - 1, bounds.Width - 2, bounds.Height - 2); // 오른쪽라인
                                                                                                 //System.Console.WriteLine(string.Format("x1 = {0}\t\t\ty1 = {1}", (bounds.Width - 2) - (bounds.Width - 2), (bounds.Height - 2) - (num - 1)));



                //Left 
                //g.DrawLine(pen, bounds.Location, new Point(bounds.X, bounds.Y + bounds.Height));
                ////Right 
                //g.DrawLine(pen, new Point(bounds.X + bounds.Width, bounds.Y), new Point(bounds.X + bounds.Width, bounds.Y + bounds.Height));
                ////Bottom 
                //g.DrawLine(pen, new Point(bounds.X, bounds.Y + bounds.Height), new Point(bounds.X + bounds.Width, bounds.Y + bounds.Height));
                ////Top1 
                //g.DrawLine(pen, new Point(bounds.X, bounds.Y), new Point(bounds.X + this.Padding.Left, bounds.Y));
                ////Top2 
                //g.DrawLine(pen, new Point(bounds.X + this.Padding.Left + (int)(rectangle.Width), bounds.Y), new Point(bounds.X + bounds.Width, bounds.Y));

                //System.Console.WriteLine("이미지그리기 끝" + "\r\n");

            }
        }
    }
}
