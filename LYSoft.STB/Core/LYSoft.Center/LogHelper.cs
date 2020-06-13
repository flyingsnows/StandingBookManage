using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace LYSoft.Center
{
    public class LogHelper
    {
        public static void PrintLog(string msg)
        {
            StackTrace trace = new StackTrace();
            StackFrame frame = trace.GetFrame(1);//1代表上级，2代表上上级，以此类推  
            MethodBase method = frame.GetMethod();
            String className = method.ReflectedType.Name;
            string fileName = frame.GetFileName();
            string threadID = Process.GetCurrentProcess().Id.ToString();
            string lineNo = frame.GetFileLineNumber().ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("[{0}]   Log   线程ID:{1}   源文件:{2}   行号:{3}   函数名:{4} --> {5}", 
                DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss"), threadID, fileName, lineNo, method.Name, msg));
            Console.WriteLine(sb.ToString());
        }
        public static void WriteLog(string msg)
        {
            StackTrace trace = new StackTrace();
            StackFrame frame = trace.GetFrame(1);//1代表上级，2代表上上级，以此类推  
            MethodBase method = frame.GetMethod();
            String className = method.ReflectedType.Name;
            string fileName = frame.GetFileName();
            string threadID = Process.GetCurrentProcess().Id.ToString();
            string lineNo = frame.GetFileLineNumber().ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("[{0}]   Log   线程ID:{1}   源文件:{2}   行号:{3}   函数名:{4} --> {5}", DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss"), threadID, fileName, lineNo, method.Name, msg));
            Console.WriteLine(sb.ToString());
            IOHelper.SaveLog(sb.ToString(), MyApps.Logpath);
        }
        public static void PrintError(string msg)
        {
            StackTrace trace = new StackTrace();
            StackFrame frame = trace.GetFrame(2);//1代表上级，2代表上上级，以此类推  
            MethodBase method = frame.GetMethod();
            String className = method.ReflectedType.Name;
            string fileName = frame.GetFileName();
            string threadID = Process.GetCurrentProcess().Id.ToString();
            string lineNo = frame.GetFileLineNumber().ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("[{0}]   Error   线程ID:{1}   源文件:{2}   行号:{3}   函数名:{4} --> {5}", DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss"), threadID, fileName, lineNo, method.Name, msg));
            Console.WriteLine(sb.ToString());
        }
        public static void WriteError(string msg)
        {
            StackTrace trace = new StackTrace();
            StackFrame frame = trace.GetFrame(1);//1代表上级，2代表上上级，以此类推  
            MethodBase method = frame.GetMethod();
            String className = method.ReflectedType.Name;
            string fileName = frame.GetFileName();
            string threadID = Process.GetCurrentProcess().Id.ToString();
            string lineNo = frame.GetFileLineNumber().ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("[{0}]   Error   线程ID:{1}   源文件:{2}   行号:{3}   函数名:{4} --> {5}", DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss"), threadID, fileName, lineNo, method.Name, msg));
            Console.WriteLine(sb.ToString());
            IOHelper.SaveError(sb.ToString(),MyApps.LogErrorpath);
        }
    }
}
