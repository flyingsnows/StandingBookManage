using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LYSoft.Center
{
    /// <summary>
    /// JS内置对象
    /// (包含监听地址,端口,当前登录用户信息,等等)
    /// </summary>
    public class PageConfig
    {
        /// <summary>
        /// 当前监听的地址
        /// </summary>
        public string BaseUrl { get; set; }
        
        /// <summary>
        /// 当前监听的端口
        /// </summary>
        public int Prot { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        

        /// <summary>
        /// 计算机名称
        /// </summary>
        public string Computer { get; set; }


        /// <summary>
        /// 登陆账号
        /// </summary>
        public string LoginCode { get; set; }



        /// <summary>
        /// 当前登录时间
        /// </summary>
        public DateTime Time { get; set; }

    }
}
