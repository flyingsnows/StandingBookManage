using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace LYSoft.Center
{
    /// <summary>
    /// IO操作类
    /// </summary>
    public class IOHelper
    {
        public static bool FileExist(string apath)
        {
            return File.Exists(apath);
        }

        public static bool DireExist(string apath)
        {
            return Directory.Exists(apath);
        }

        public static string GetAPath(string mPath)
        {
            string result = string.Empty;
            result = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + mPath;
            return result;
        }

        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }


        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }
        public static void DeleteDirectory(string path)
        {
            Directory.Delete(path, true);
        }
        public static void DeleteFile(string path)
        {
            File.Delete(path);
        }


        public static byte[] ReadFile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            try
            {
                byte[] buffur = new byte[fs.Length];
                fs.Read(buffur, 0, (int)fs.Length);

                return buffur;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                return null;
            }
            finally
            {
                if (fs != null)
                {
                    //关闭资源
                    fs.Close();
                }
            }
        }








        /// <summary>
        /// 保存字符串到绝对路径文本
        /// </summary>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool WriteText(string path, string content, bool isNew = false)
        {
            bool result = false;
            try
            {
                lock (path)
                {
                    string temp = path;
                    if (isNew)
                    {
                        File.Delete(temp);
                    }
                    if (CreatePath(temp))
                    {
                        StreamWriter sw = File.AppendText(temp);
                        sw.WriteLine(content);
                        sw.Flush();
                        sw.Close();
                        result = true;
                    }
                }
            }
            catch
            {

            }
            return result;
        }
        /// <summary>
        /// 根据绝对路径创建文件夹和文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static bool CreatePath(string path)
        {
            bool result = false;
            try
            {

                if (!File.Exists(path))
                {
                    CreateDirectoryByFilePath(path);
                    FileInfo tmpfile = new FileInfo(path);
                    FileStream fs = tmpfile.Create();
                    fs.Close();
                }
                result = true;
            }
            catch
            {


            }

            return result;
        }
        private static void CreateDirectoryByFilePath(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    string dirpath = filePath.Substring(0, filePath.LastIndexOf('\\'));
                    string[] pathes = dirpath.Split('\\');
                    if (pathes.Length > 1)
                    {
                        string path = pathes[0];
                        for (int i = 1; i < pathes.Length; i++)
                        {
                            path += "\\" + pathes[i];
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 保存日志
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool SaveLog(string content,string soucepath)
        {
            DateTime now = DateTime.Now;
            string path = Path.Combine(soucepath , now.ToString("yyyyMMdd") + ".txt");
            return WriteText(path, content);
        }

        /// <summary>
        /// 保存错误
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool SaveError(string content, string soucepath)
        {
            DateTime now = DateTime.Now;
            string path = Path.Combine(soucepath , now.ToString("yyyyMMdd") + ".txt");
            return WriteText(path, content);
        }

        /// <summary>
        /// 保存字符串到文本
        /// </summary>
        /// <param name="apath"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool SaveTextAPath(string apath, string content)
        {
            bool result = false;
            if (CreatePath(apath))
            {
                StreamWriter sw = File.AppendText(apath);
                sw.WriteLine(content);
                sw.Flush();
                sw.Close();
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 读取文件内容到文本中
        /// </summary>
        /// <param name="apath"></param>
        /// <returns></returns>
        public static string ReadText(string apath)
        {
            string result = string.Empty;
            StreamReader sr = new StreamReader(apath, Encoding.Default);
            string line = string.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                result += line;
            }
            sr.Close();
            return result;
        }
        public static byte[] ReadAllByte(string apath)
        {
            byte[] result = null;
            if (IOHelper.FileExist(apath))
            {

                result = File.ReadAllBytes(apath);
            }

            return result;
        }
        public static void CopyFile(string fromPath, string toPath, bool isOver = false)
        {
            try
            {
                if (!File.Exists(fromPath))
                {
                    return;

                }
                if (isOver)
                {
                    if (File.Exists(toPath))
                    {
                        File.Delete(toPath);
                    }
                }
                if (!File.Exists(toPath))
                {
                    File.Copy(fromPath, toPath);
                }
            }
            catch
            {

            }

        }
        public static void CopyDir(string fromDir, string toDir, bool isOver = false)
        {
            try
            {
                if (!Directory.Exists(fromDir))
                    return;

                if (!Directory.Exists(toDir))
                {
                    Directory.CreateDirectory(toDir);
                }

                string[] files = Directory.GetFiles(fromDir);
                foreach (string formFileName in files)
                {
                    string fileName = Path.GetFileName(formFileName);
                    string toFileName = Path.Combine(toDir, fileName);
                    if (isOver)
                    {
                        if (File.Exists(toFileName))
                        {
                            File.Delete(toFileName);
                        }
                    }
                    if (!File.Exists(toFileName))
                    {
                        File.Copy(formFileName, toFileName);
                    }
                }
                string[] fromDirs = Directory.GetDirectories(fromDir);
                foreach (string fromDirName in fromDirs)
                {
                    string dirName = Path.GetFileName(fromDirName);
                    string toDirName = Path.Combine(toDir, dirName);
                    CopyDir(fromDirName, toDirName, isOver);
                }
            }
            catch { }
        }

        public static void SaveFile(string path, byte[] temp)
        {
            if (FileExist(path))
            {
                DeleteFile(path);

            }
            FileStream fs = new FileStream(path, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(temp, 0, temp.Length);
            fs.Flush();//数据写入图片文件
            fs.Close();
        }
        public static void AppendFile(string path, byte[] temp)
        {
            if (!FileExist(path))
            {
                return;

            }
            FileStream fs = new FileStream(path, FileMode.Append);

            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(temp, 0, temp.Length);

            fs.Flush();//数据写入图片文件

            fs.Close();

        }
    }

}
