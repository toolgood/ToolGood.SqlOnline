/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace ToolGood.Common.Utils
{
    public class VerifyCode
    {
        /// <summary>
        /// 生成随机码
        /// </summary>
        /// <param name="CodeLength"></param>
        /// <param name="Letters"></param>
        /// <returns></returns>
        public string GenerateCaptchaCode(int CodeLength = 4, string Letters = "2346789ABCDEFGHJKLMNPRTUVWXYZ")
        {
            var rand = new Random();
            var maxRand = Letters.Length - 1;

            var sb = new StringBuilder();
            for (var i = 0; i < CodeLength; i++) {
                var index = rand.Next(maxRand);
                sb.Append(Letters[index]);
            }

            return sb.ToString();
        }


        /// <summary>
        /// 噪点颜色
        /// </summary>
        public List<Color> NoiseColors = new List<Color>(){
            Color.LightBlue,Color.LightCoral,Color.LightCyan,Color.LightGoldenrodYellow,
            Color.LightGray,Color.LightGreen,Color.LightPink,Color.LightSalmon,
            Color.LightSkyBlue,Color.LightSteelBlue,Color.LightYellow
        };
        /// <summary>
        /// 显示字体
        /// </summary>
        public List<string> ShowFonts = new List<string>(){
            "Arial", "Georgia", "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "宋体", "楷体", "黑体"
        };


        /// <summary>
        /// 显示颜色
        /// </summary>
        public List<Color> ShowColors = new List<Color>(){
            Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple
        };
        /// <summary>
        /// 字体大小
        /// </summary>
        public int FontSize = 23;
        /// <summary>
        /// 边框
        /// </summary>
        public int Padding = 2;

        public Image CreateImage(string code)
        {
            int fSize = FontSize;
            int fWidth = fSize + Padding;

            int imageWidth = (int)(code.Length * fWidth) + 4 + Padding * 2;
            int imageHeight = (int)(fSize * 1.5 + Padding);

            Bitmap image = new Bitmap(imageWidth, imageHeight);
            Graphics g = Graphics.FromImage(image);
            g.SetClip(new Rectangle(0, 0, imageWidth, imageHeight));
            g.Clear(Color.White);
            var NoiseCount = imageWidth * imageHeight / 16;

            DrawNoisePoints(g, NoiseCount, NoiseColors);
            DrawNoiseLines(g, fSize, NoiseColors);
            DrawString(g, code);
            //DrawFrame(g);
            g.Dispose();

            return TwistImage(image);
        }

        public byte[] CreateImageBytes(string code)
        {
            var img = CreateImage(code);
            using (MemoryStream stream = new MemoryStream()) {
                img.Save(stream, ImageFormat.Jpeg);
                return stream.ToArray();
            }
        }

        #region 私有方法
        private Bitmap TwistImage(Bitmap img)
        {
            Random rand = new Random();
            var d = rand.NextDouble() * 2 + 2;
            var r = rand.NextDouble() * 2 * Math.PI;

            var bit = TwistImage(img, true, d, r);
            using (var g1 = Graphics.FromImage(bit)) {
                g1.SetClip(new Rectangle(0, 0, img.Width, img.Height));
                DrawFrame(g1);
            }
            return bit;
        }

        private void DrawString(Graphics g, string code)
        {
            int fSize = FontSize;

            int fWidth = fSize + Padding;
            var rand = new Random();
            for (int i = 0; i < code.Length; i++) {
                var cindex = rand.Next(ShowColors.Count - 1);
                var findex = rand.Next(ShowFonts.Count - 1);

                var f = new Font(ShowFonts[findex], fSize, FontStyle.Bold);
                var b = new SolidBrush(ShowColors[cindex]);
                int top = rand.Next(fSize / 8) + fSize / 10;
                var left = i * fWidth + 2;
                g.DrawString(code.Substring(i, 1), f, b, left, top);
            }
        }

        private void DrawNoisePoints(Graphics g, int count, List<Color> colors)
        {
            var gwidth = g.ClipBounds.Width;
            var gheight = g.ClipBounds.Height;

            Random random = new Random();
            for (int i = 0; i < count; i++) {
                int x = random.Next((int)gwidth);
                int y = random.Next((int)gheight);
                Color c = colors[random.Next(colors.Count)];
                g.DrawRectangle(new Pen(c), x, y, 1, 1);
            }
        }
        private void DrawNoiseLines(Graphics g, int count, List<Color> colors)
        {
            var width = (int)g.ClipBounds.Width;
            var height = (int)g.ClipBounds.Height;

            Random random = new Random();
            for (int i = 0; i < count; i++) {
                int x1 = random.Next(0, width / 2);
                int y1 = random.Next(height / 4, height / 4 * 3);
                int x2 = random.Next(width / 2, width);
                int y2 = random.Next(height / 4, height / 4 * 3);
                Color c = colors[random.Next(colors.Count)];
                g.DrawLine(new Pen(c), x1, y1, x2, y2);
            }
        }
        private void DrawFrame(Graphics g)
        {
            var gwidth = g.ClipBounds.Width;
            var gheight = g.ClipBounds.Height;
            g.DrawRectangle(new Pen(Color.Gainsboro), 0, 0, gwidth - 1, gheight - 1);
        }
        //private const double PI = 3.1415926535897932384626433832795;
        private const double PI2 = 6.283185307179586476925286766559;

        /// <summary>
        /// 正弦曲线Wave扭曲图片（Edit By 51aspx.com）
        /// </summary>
        /// <param name="srcBmp">图片路径</param>
        /// <param name="bXDir">如果扭曲则选择为True</param>
        /// <param name="dMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        /// <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>
        /// <returns></returns>
        private Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);

            // 将位图背景填充为白色
            Graphics graph = Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();

            double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;

            for (int i = 0; i < destBmp.Width; i++) {
                for (int j = 0; j < destBmp.Height; j++) {
                    double dx = bXDir ? (PI2 * (double)j) / dBaseAxisLen : (PI2 * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);
                    // 取得当前点的颜色
                    int nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    int nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                        && nOldY >= 0 && nOldY < destBmp.Height) {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }

            return destBmp;
        }


        #endregion

    }

}
