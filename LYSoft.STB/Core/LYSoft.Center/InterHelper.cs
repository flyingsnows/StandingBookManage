using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace LYSoft.Center
{
    /// <summary>
    /// 网络 端口 通信
    /// </summary>
    public class InterHelper
    {
        /// <summary>
        /// 获取本地第一个可用的端口号
        /// </summary>
        /// <returns></returns>
        public int GetLocalFirstProt()
        {
            IList<int> ProtList = GetSystemProtList();
            for (int i = 5000; i < 6000; i++)
            {
                if (!ProtList.Contains(i))  //包含在里面已经暂用
                {
                    return i;
                }
            }

            return -1;
        }
        


        /// <summary>
        /// 获取操作系统已用的端口号
        /// </summary>
        /// <returns></returns>
        private IList<int> GetSystemProtList()
        {
            //获取本地计算机的网络连接和通信统计数据的信息
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

            //返回本地计算机上的所有Tcp监听程序
            IPEndPoint[] ipsTCP = ipGlobalProperties.GetActiveTcpListeners();

            //返回本地计算机上的所有UDP监听程序
            IPEndPoint[] ipsUDP = ipGlobalProperties.GetActiveUdpListeners();

            //返回本地计算机上的Internet协议版本4(IPV4 传输控制协议(TCP)连接的信息。
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            IList<int> AllPorts = new List<int>(); 
            foreach (IPEndPoint ep in ipsTCP) AllPorts.Add(ep.Port);
            foreach (IPEndPoint ep in ipsUDP) AllPorts.Add(ep.Port);
            foreach (TcpConnectionInformation conn in tcpConnInfoArray) AllPorts.Add(conn.LocalEndPoint.Port);

            return AllPorts;
        }







    }
}
