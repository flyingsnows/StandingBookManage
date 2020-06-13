using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LYSoft.Center
{
    public class LoginUserInfo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginAccount { get; set; }

        /// <summary>
        /// 登录时间;
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 登录密码:
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户状态 true:激活 false:禁用
        /// </summary>
        public bool Istate { get; set; }


        /// <summary>
        /// 主机地址;
        /// </summary>
        public string HostAddress { get; set; }

        /// <summary>
        /// 主机名称;
        /// </summary>
        public string HostName { get; set; }
        
        /// <summary>
        /// 是否系统管理员
        /// </summary>
        public bool IsAdministrators { get; set; }

  


      

        /// <summary>
        /// 获取本机IP地址
        /// </summary>
        /// <returns>本机IP地址</returns>
        public string GetLocalIP()
        {
            try
            {
                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IpEntry.AddressList[i].ToString();
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
