using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LYSoft.Domain
{
    public class T_A_DATA_STUDENT : BASEDOMAIN
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string XM { get; set; }

        /// <summary>
        /// 学号
        /// </summary>
        public string XH { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string XB { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public string CSRQ { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RXRQ { get; set; }

        /// <summary>
        /// 所在学院
        /// </summary>
        public string SZXY { get; set; }

        /// <summary>
        /// 所在班级
        /// </summary>
        public string SZBJ { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string LXDH { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        public string IMG { get; set; }

        /// <summary>
        /// 特征码
        /// </summary>
        public string TZM { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string TIME { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        public Int64 CJR { get; set; }

    }
}
