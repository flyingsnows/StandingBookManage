using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Collections;
using System.Reflection;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace LYSoft.Center
{
    /// <summary>
    /// 拓展方法之模型相关分部
    /// </summary>
    public static partial class Extends
    {
        #region JSON
        /// <summary>
        /// 将字符串反序列化为模型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns> 
        public static T JSON2<T>(this string s) where T : new()
        {
            T t = new T();
            try
            {
                if (!string.IsNullOrEmpty(s))
                {
                    fastJSON.JSON.Parameters.UseEscapedUnicode = false;
                    fastJSON.JSON.Parameters.UsingGlobalTypes = false;
                    fastJSON.JSON.Parameters.UseExtensions = false;
                    return fastJSON.JSON.ToObject<T>(s);
                }
            }
            catch
            {
            }

            return t;
        }
        /// <summary>
        /// 将模型序列化为字符串
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string JSON(this object t)
        {
            string tmp = string.Empty;
            try
            {
                if (t != null)
                {
                    fastJSON.JSON.Parameters.UseEscapedUnicode = false;
                    fastJSON.JSON.Parameters.UsingGlobalTypes = false;
                    fastJSON.JSON.Parameters.UseExtensions = false;
                    //fastJSON.JSON.ToNiceJSON
                    tmp = fastJSON.JSON.ToJSON(t);
                }
            }
            catch { }
            return tmp;
        }
        public static string JSON(this object t, Stream stream)
        {
            string tmp = string.Empty;
            try
            {
                if (t != null)
                {
                    fastJSON.JSON.Parameters.UseEscapedUnicode = false;
                    fastJSON.JSON.Parameters.UsingGlobalTypes = false;
                    fastJSON.JSON.Parameters.UseExtensions = false;
                    //fastJSON.JSON.ToNiceJSON
                    tmp = fastJSON.JSON.ToJSON(t);
                    byte[] array = Encoding.UTF8.GetBytes(tmp);
                    stream.Write(array, 0, array.Length);
                }
            }
            catch { }
            return tmp;
        }
        #endregion

        #region XML转换相关
        /// <summary>
        /// XML序列化
        /// </summary>
        /// <param name="obj">序列对象</param>
        /// <returns>序列结果</returns>
        public static string SerializeToXml(this object obj)
        {
            string result = string.Empty;
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
                MemoryStream stream = new MemoryStream();
                xmlSerializer.Serialize(stream, obj);
                stream.Position = 0;
                StreamReader reader = new StreamReader(stream);
                result = reader.ReadToEnd();
                stream.Close();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;

        }

        /// <summary>
        /// XML反序列化
        /// </summary>
        /// <param name="type">目标类型(Type类型)</param>
        /// <param name="filePath">XML文件路径</param>
        /// <returns>序列对象</returns>
        public static T DeserializeFromXML<T>(this string XMLString) where T : new()
        {
            T t = new T();
            try
            {
                using (StringReader sr = new StringReader(XMLString))
                {
                    XmlSerializer xmldes = new XmlSerializer(t.GetType());
                    t = (T)xmldes.Deserialize(sr);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
            }
            return t;
        }
        #endregion

        public static Bitmap ResizeImage(this Image imgToResize, Size size)
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
