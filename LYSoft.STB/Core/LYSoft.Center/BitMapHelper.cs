using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace LYSoft.Center
{
    /// <summary>
    /// 位图处理帮助类
    /// </summary>
    public class BitMapHelper
    {
        public static Image GetThumbnail(string base64String, int width, int height)
        {
            Image result = null;
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {

                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);

                using (Image image = Image.FromStream(ms, true))
                {

                    Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);

                    result = image.GetThumbnailImage(width, height, myCallback, IntPtr.Zero);
                }
                //using (Bitmap bit = new Bitmap(Image.FromStream(ms, true)))
                //{
                //    result = GetThumbnail(bit, height, width);
                //}
            }
            return result;
        }

        public static Bitmap GetThumbnail(Bitmap b, int destHeight, int destWidth)
        {
            System.Drawing.Image imgSource = b;
            System.Drawing.Imaging.ImageFormat thisFormat = imgSource.RawFormat;
            int sW = 0, sH = 0;
            // 按比例缩放           
            int sWidth = imgSource.Width;
            int sHeight = imgSource.Height;
            if (sHeight > destHeight || sWidth > destWidth)
            {
                if ((sWidth * destHeight) > (sHeight * destWidth))
                {
                    sW = destWidth;
                    sH = (destWidth * sHeight) / sWidth;
                }
                else
                {
                    sH = destHeight;
                    sW = (sWidth * destHeight) / sHeight;
                }
            }
            else
            {
                sW = sWidth;
                sH = sHeight;
            }
            Bitmap outBmp = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.Transparent);
            // 设置画布的描绘质量         
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(imgSource, new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
            g.Dispose();
            // 以下代码为保存图片时，设置压缩质量     
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;
            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;
            imgSource.Dispose();
            return outBmp;
        }
        private static bool ThumbnailCallback()
        {
            return false;
        }
        public static bool ThumbailSave(string sFile, string tFile, int destHeight, int destWidth)
        {
            bool result = false;
            try
            {
                Bitmap map = (Bitmap)Image.FromFile(sFile);
                Bitmap temp = GetThumbnail(map, destHeight, destWidth);
                temp.Save(tFile);
                result = true;
            }
            catch { }
            return result;

        }

        public static bool IsImage(string filePath)
        {
            bool result = false;
            try
            {
                Image map = Bitmap.FromFile(filePath);
                result = true;
            }
            catch { }
            return result;

        }

        /// <summary>
        /// 通过FileStream 来打开文件，这样就可以实现不锁定Image文件，到时可以让多用户同时访问Image文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Image ReadImageFile(string path)
        {
            Image result = null;
            using (FileStream fs = File.OpenRead(path))
            {
                int filelength = 0;
                filelength = (int)fs.Length; //获得文件长度 
                byte[] image = new byte[filelength]; //建立一个字节数组 
                fs.Read(image, 0, filelength); //按字节流读取 
                result = Image.FromStream(fs);
            }
            return result;
        }

        #region Base64转换
        public static string BitmapToBase64(Bitmap map)
        {
            string result = string.Empty;
            MemoryStream ms = new MemoryStream();
            map.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] arr = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(arr, 0, (int)ms.Length);
            ms.Close();
            result = Convert.ToBase64String(arr);
            return result;
        }

        public static string BitmapToBase64(Image map)
        {
            string result = string.Empty;
            MemoryStream ms = new MemoryStream();
            map.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] arr = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(arr, 0, (int)ms.Length);
            ms.Close();
            result = Convert.ToBase64String(arr);
            return result;
        }


        public static Bitmap Base64ToBitmap(string baseStr)
        {
            Bitmap map = null;
            try
            {
                byte[] arr = Convert.FromBase64String(baseStr);
                using (MemoryStream ms = new MemoryStream(arr, 0, arr.Length))
                {

                    // Convert byte[] to Image
                    ms.Write(arr, 0, arr.Length);
                    map = new Bitmap(Image.FromStream(ms, true));
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return map;
        }
        public static Bitmap BytesToImage(byte[] bytes)

        {
            Bitmap img = null;
            try
            {
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    img = (Bitmap)Image.FromStream(ms);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return img;

        }
        #endregion
        public static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            return null;
        }

        public static Bitmap CutImage(Image baseImage, Rectangle rect, int dstwidth, int dstheight)
        {
            Bitmap result = null;
            try
            {
                using (Bitmap map = new Bitmap(dstwidth, dstheight))
                {
                    using (Graphics gr = Graphics.FromImage(map))
                    {
                        //public void DrawImage(Image image, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit);
                        gr.DrawImage(baseImage, new Rectangle(0, 0, dstwidth, dstheight), rect, GraphicsUnit.Pixel);
                        result = (Bitmap)map.Clone();
                    }
                }
            }
            catch
            {


            }
            return result;
        }




    }
}
