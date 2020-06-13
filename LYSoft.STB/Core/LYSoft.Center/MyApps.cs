using LYSoft.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYSoft.Center
{
    /// <summary>
    /// 系统全局静态类(必要信息)
    /// </summary>
    public static class MyApps
    {
        
        /// <summary>
        /// 程序当前版本号;
        /// </summary>
        public static string AppVersion { get; set; }
        
        
       
        /// <summary>
        /// 异常日志路径
        /// </summary>
        public static string LogErrorpath { get; set; }

        /// <summary>
        /// 程序日志路径
        /// </summary>
        public static string Logpath { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public static string Imgpath { get; set; }

        /// <summary>
        /// 临时文件路径
        /// </summary>
        public static string Temppath { get; set; }

        /// <summary>
        /// 数据库文件路径
        /// </summary>
        public static string DataSource { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public static string DataName { get; set; }


        public static T_A_DATA_USER User { get; set; }

    }
}
