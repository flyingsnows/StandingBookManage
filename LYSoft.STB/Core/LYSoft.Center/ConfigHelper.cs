using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LYSoft.Center
{
    public class ConfigHelper
    {
        private static Dictionary<string, string> ConfigFileDic = new Dictionary<string, string>();//配置文件保存路径对象

        /// <summary>
        /// 保存配置对象到绝对路径
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool SaveConfig(object obj, string filePath)
        {
            bool result = false;
            string xml = obj.SerializeToXml();
            result = IOHelper.WriteText(filePath, xml, true);
            return result;
        }
        /// <summary>
        /// 从绝对路径读取配置对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T LoadConfig<T>(string filePath) where T : new()
        {
            T obj = new T();
            try
            {
                string xml = IOHelper.ReadText(filePath);
                obj = xml.DeserializeFromXML<T>();
            }
            catch 
            { }
            return obj;
        }
    }
}
