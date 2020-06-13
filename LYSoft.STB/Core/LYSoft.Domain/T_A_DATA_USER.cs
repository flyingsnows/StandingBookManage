using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LYSoft.Domain
{
    public class T_A_DATA_USER : BASEDOMAIN
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string YHM { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string DLZH { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string MM { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public Int64 ROLEID { get; set; }
        
    }
}
