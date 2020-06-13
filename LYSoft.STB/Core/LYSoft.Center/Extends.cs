using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace LYSoft.Center
{
    /// <summary>
    /// 拓展方法之通用分部
    /// </summary>
    public static partial class Extends
    {
        static Extends()
        {
            

        }
        #region 转Int
        /// <summary>
        /// 将object类型转换成int类型
        /// </summary>
        /// <param name="temp">目标对象</param>
        /// <param name="defalutValue">失败时的替换值</param>
        /// <returns>失败转为默认值</returns>
        public static int ObjectToInt(this object temp, int defalutValue)
        {
            int result = -1;

            try
            {
                result = Convert.ToInt32(temp);
            }
            catch
            {
                result = defalutValue;
            }
            return result;
        }

        /// <summary>
        /// 将object类型转换成int类型
        /// </summary>
        /// <param name="temp">目标对象</param>
        /// <returns>失败转为-1</returns>
        public static int ObjectToInt(this object o)
        {
            return ObjectToInt(o, -1);
        }

        #endregion
        #region 转Double
        /// <summary>
        /// 将object类型转换成double类型
        /// </summary>
        /// <param name="temp">目标对象</param>
        /// <param name="defalutValue">失败时的替换值</param>
        /// <returns>失败转为默认值</returns>
        public static double ObjectToDouble(this object temp, double defalutValue)
        {
            double result = -1;

            try
            {
                result = Convert.ToDouble(temp);
            }
            catch
            {
                result = defalutValue;
            }
            return result;
        }

        /// <summary>
        /// 将object类型转换成double类型
        /// </summary>
        /// <param name="temp">目标对象</param>
        /// <returns>失败转为-1</returns>
        public static double ObjectToDouble(this object o)
        {
            return ObjectToDouble(o, -1);
        }

        #endregion
        #region 转Bool



        /// <summary>
        /// 将object类型转换成bool类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static bool ObjectToBool(this object o, bool defaultValue)
        {
            bool reslut = false;
            try
            {
                reslut = Convert.ToBoolean(o);
            }
            catch
            {
                reslut = defaultValue;
            }
            return reslut;
        }

        /// <summary>
        /// 将object类型转换成bool类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <returns></returns>
        public static bool ObjectToBool(this object o)
        {
            return ObjectToBool(o, false);
        }

        #endregion

        #region 转DateTime



        /// <summary>
        /// 将object类型转换成datetime类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static DateTime ObjectToDateTime(this object o, DateTime defaultValue)
        {
            DateTime result = DateTime.MinValue;
            try
            {
                if (o != null)
                {
                    string tempString = o.ToString();
                    if (!string.IsNullOrEmpty(tempString))
                    {
                        if (!tempString.Contains("-") && !tempString.Contains("/") && !tempString.Contains(":") && !tempString.Contains(" "))
                        {
                            string format = string.Empty;
                            if (tempString.Length == 8)
                            {
                                format = "yyyyMMdd";
                            }
                            if (tempString.Length == 14)
                            {
                                format = "yyyyMMddHHmmss";
                            }
                            if (tempString.Length == 18)
                            {
                                format = "yyyyMMddHHmmssffff";
                            }
                            result = DateTime.ParseExact(tempString, format, System.Globalization.CultureInfo.CurrentCulture);
                        }
                        else
                        {
                            result = Convert.ToDateTime(tempString);
                        }
                    }
                }
                else
                {
                    result = defaultValue;
                }
            }
            catch
            {


            }

            return result;
        }

        /// <summary>
        /// 将object类型转换成datetime类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <returns></returns>
        public static DateTime ObjectToDateTime(this object o)
        {
            return ObjectToDateTime(o, DateTime.Now);
        }

        public static string DateTimeToNoBDString(this DateTime time)
        {
            return time.ToString("yyyyMMddHHmmssffff");

        }
        #endregion

        #region 转Decimal

        /// <summary>
        /// 将object类型转换成decimal类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static decimal ObjectToDecimal(this object o, decimal defaultValue)
        {
            decimal result = 0m;
            try
            {
                result = Convert.ToDecimal(o);
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// 将object类型转换成decimal类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <returns></returns>
        public static decimal ObjectToDecimal(this object o)
        {
            return ObjectToDecimal(o, 0m);
        }

        #endregion

        #region 转Version
        public static Version ObjectToVersion(this object o)
        {
            Version reslut = new Version("0.0.0");
            try
            {
                reslut = new Version(o.ToString());
            }
            catch
            {

            }
            return reslut;
        }


        #endregion







        #region 特殊日期处理
        ///// <summary>
        ///// DateTime转时间字符串
        ///// </summary>
        ///// <param name="datetime"></param>
        ///// <param name="isWithMillisecond"></param>
        ///// <returns></returns>
        //public static string DataTimeToString(this DateTime datetime, bool isWithMillisecond = false)
        //{
        //    string result = string.Empty;
        //    if (isWithMillisecond)
        //    {
        //        result = datetime.ToString("yyyy-MM-dd HH:mm:ss:ffff");
        //    }
        //    else
        //    {
        //        result = datetime.ToString("yyyy-MM-dd HH:mm:ss");
        //    }
        //    return result;

        //}

        /// <summary>
        /// 日期转换成unix时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string DateTimeToUnixTimestamp(this DateTime dateTime)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (dateTime - startTime).TotalSeconds.ToString();
        }

        /// <summary>
        /// 时间戳转换为DateTime
        /// </summary>
        /// <param name="timeString"></param>
        /// <returns></returns>
        public static DateTime UnixTimestampToDateTime(this string timeString)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeString + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        #endregion

        #region 加密相关
        /// <summary>
        /// MD5 32位加密（标准大写）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Md532(this string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("X2");
            }
            return pwd;
        }

        /// <summary>
        /// 进行DES加密。
        /// </summary>
        /// <param name="pToEncrypt">要加密的字符串。</param>
        /// <param name="sKey">密钥，且必须为8位。</param>
        /// <returns>以Base64格式返回的加密字符串。</returns>
        public static string DESEncrypt(this string pToEncrypt, string sKey)
        {
            if (sKey.Length > 8)
            {
                sKey = sKey.Substring(0, 8);
            }
            else
            {
                if (sKey.Length < 8)
                {
                    sKey = sKey.PadRight(8, 'x');
                }
            }
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Convert.ToBase64String(ms.ToArray());
                ms.Close();
                return str;
            }
        }


        // <summary>
        // 进行DES解密。
        // </summary>
        // <param name="pToDecrypt">要解密的以Base64</param>
        // <param name="sKey">密钥，且必须为8位。</param>
        // <returns>已解密的字符串。</returns>
        public static string DESDecrypt(this string pToDecrypt, string sKey)
        {
            string result = string.Empty;
            try
            {
                if (sKey.Length > 8)
                {
                    sKey = sKey.Substring(0, 8);
                }
                else
                {
                    if (sKey.Length < 8)
                    {
                        sKey = sKey.PadRight(8, 'x');
                    }
                }
                byte[] inputByteArray = Convert.FromBase64String(pToDecrypt);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                    des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        cs.Close();
                    }
                    string str = Encoding.UTF8.GetString(ms.ToArray());
                    ms.Close();
                    result = str;
                }
            }
            catch
            {


            }
            return result;
        }



        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="code"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string EncodeBase64(this string code, Encoding encoding)
        {
            string encode = "";
            if (!string.IsNullOrEmpty(code))
            {
                byte[] bytes = encoding.GetBytes(code);
                try
                {
                    encode = Convert.ToBase64String(bytes);
                }
                catch
                {
                    encode = code;
                }
            }
            return encode;
        }
        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="code"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string DecodeBase64(this string code, Encoding encoding)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = encoding.GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }
        #endregion

        /// <summary>
        /// 判断字符串是否是IP地址
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static bool IsIP(this string temp)
        {
            bool result = false;
            IPAddress ip;
            if (IPAddress.TryParse(temp, out ip))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// base64转byte[]
        /// </summary>
        /// <param name="base64str"></param>
        /// <returns></returns>
        public static byte[] Base64ToBytes(this string base64str)
        {

            byte[] result = null;
            try
            {
                result = Convert.FromBase64String(base64str);
            }
            catch
            {

            }
            return result;
        }

        /// <summary>
        /// base64转Bitmap
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static Bitmap Base64ToBitmap(this string base64)
        {
            Bitmap result = null;
            try
            {
                byte[] Bytes = Base64ToBytes(base64);
                MemoryStream stream = new MemoryStream(Bytes);
                result = new Bitmap(stream);
            }
            catch{ }
            return result;
        }



        /// <summary>
        /// object转bitmap
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Bitmap ObjectToBitmap(this object obj)
        {
            Bitmap result = null;
            try
            {
                string base64 = obj as string;
                byte[] Bytes = Base64ToBytes(base64);
                MemoryStream stream = new MemoryStream(Bytes);
                result = new Bitmap(stream);
            }
            catch { }
            return result;
        }


        /// <summary>
        /// byte[] 转base64
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BytesToBase64(this byte[] bytes)
        {
            string result = string.Empty;
            try
            {
                result = Convert.ToBase64String(bytes);
            }
            catch
            {

            }
            return result;
        }

        /// <summary>
        /// Image 转base64
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string ImageToBase64(this Image img)
        {
            string result = string.Empty;
            try
            {
                byte[] imgdata = img.ImageToByte();
                result = Convert.ToBase64String(imgdata);
            }
            catch(Exception ex) { }
            return result;
        }

        public static string BitMapToBase64(this Bitmap img)
        {
            string result = string.Empty;
            try
            {
                byte[] imgdata = img.ImageToByte();
                result = Convert.ToBase64String(imgdata);
            }
            catch { }
            return result;
        }

        /// <summary>
        /// Image 转byte[]
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] ImageToByte(this Image img)
        {
            byte[] result = null;
            if (!img.Equals(null))
            {
                using (MemoryStream mostream = new MemoryStream())
                {
                    Bitmap bmp = new Bitmap(img);
                    bmp.Save(mostream, System.Drawing.Imaging.ImageFormat.Bmp);//将图像以指定的格式存入缓存内存流

                    result = new byte[mostream.Length];
                    mostream.Position = 0;//设置留的初始位置
                    mostream.Read(result, 0, result.Length);
                }
            }
            return result;
        }




        public static string Base64ToWeb(this string base64)
        {

            string result = string.Empty;
            try
            {
                result = base64.Replace("+", "%2B");
            }
            catch
            {

            }
            return result;
        }
        public static string Base64FromWeb(this string base64)
        {

            string result = string.Empty;
            try
            {
                result = base64.Replace("%2B", "+");
            }
            catch
            {

            }
            return result;
        }

        ///// <summary>
        ///// 将字符串反序列化为模型
        ///// </summary>
        ///// <param name="s">目标字符串</param>
        ///// <param name="defaultValue">默认值</param>
        ///// <returns></returns> 
        //public static T JSON2<T>(this string s) where T : new()
        //{
        //    T t = new T();
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(s))
        //        {
        //            fastJSON.JSON.Parameters.UseEscapedUnicode = false;
        //            fastJSON.JSON.Parameters.UsingGlobalTypes = false;
        //            fastJSON.JSON.Parameters.UseExtensions = false;
        //            return fastJSON.JSON.ToObject<T>(s);
        //        }
        //    }
        //    catch
        //    {
        //    }

        //    return t;
        //}


        /// <summary>
        /// byte[] 转 IntPtr
        /// </summary>
        /// <param name="mybyte"></param>
        /// <returns></returns>
        public static IntPtr ByteToIntPtr(this byte[] mybyte)
        {
            IntPtr ptr = IntPtr.Zero;
            try
            {
                if (mybyte == null)
                {
                    ptr = IntPtr.Zero;
                }
                else
                {
                    byte[] da = mybyte;
                    ptr = Marshal.AllocHGlobal(da.Length);
                    Marshal.Copy(da, 0, ptr, da.Length);
                }
            }
            catch { }
            return ptr;
        }


    }
}
