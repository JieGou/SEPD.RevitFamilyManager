
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SEPD.CommunicationModule;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SEPD.RevitFamilyManager
{
    public static class getFilePics
    {
        public static string getFilePic0(string filePath)
        {

            //MessageBox.Show(System.Reflection.Assembly.GetExecutingAssembly().Location);
            //MessageBox.Show("检测PICs"+filePath);
            string savePath = null;
            try
            {
                //string savePicPath = Path.GetDirectoryName(filePath);
                string savePicName = Path.GetFileName(filePath);
                savePath = filePath.Replace(savePicName, ReviewLocalFamily.getFileMD5Hash(filePath)) + ".png";


                ShellFile shellFile = ShellFile.FromFilePath(filePath);

                Bitmap shellThumb = shellFile.Thumbnail.ExtraLargeBitmap;

                shellThumb = resizeImage((Image)shellThumb, new Size(120, 120));

                shellThumb.Save(savePath, ImageFormat.Png);
            }
            catch (Exception de)
            {
                MessageBox.Show(de.ToString());
            }

            return savePath;

        }

        //调整图片大小
        public static Bitmap resizeImage(Image imgToResize, Size size)
        {
            //获取图片宽度
            int sourceWidth = imgToResize.Width;
            //获取图片高度
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //计算宽度的缩放比例
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //计算高度的缩放比例
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //期望的宽度
            int destWidth = (int)(sourceWidth * nPercent);
            //期望的高度
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //绘制图像
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return b;
        }


    }



}

