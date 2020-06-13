using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LYSoft.Domain
{
    public class T_A_DATA_JZXX
    {
        /// <summary>
        /// ID
        /// </summary>
        [MyStringKey("主键", 4, true)]
        public int ID { get; set; }

        /// <summary>
        /// 人员
        /// </summary>
        public int RYID { get; set; }

        /// <summary>
        /// 就诊科室
        /// </summary>
        public string JZKS { get; set; }

        /// <summary>
        /// 就诊时间
        /// </summary>
        public string JZSJ { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public string TIME { get; set; }

    }
}
