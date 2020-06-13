using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LYSoft.Domain
{
    public class T_A_DATA_KQXX : BASEDOMAIN
    {
        /// <summary>
        /// 课程名称
        /// </summary>
        public string KCMC { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string KSSJ { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string JSSJ { get; set; }

        /// <summary>
        /// 相似度
        /// </summary>
        public string XSD { get; set; }

        /// <summary>
        /// 人员id
        /// </summary>
        public Int64 RYID { get; set; }


        /// <summary>
        /// 打卡图片
        /// </summary>
        public string IMG { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CJSJ { get; set; }


        /// <summary>
        /// 创建人
        /// </summary>
        public Int64 CJR { get; set; }

    }
}
