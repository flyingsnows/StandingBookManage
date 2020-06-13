using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LYSoft.Domain
{
    public class PdfModel
    {
        /// <summary>
        /// Key
        /// </summary>
        public Int64 ID { get; set; }

        /// <summary>
        /// 附件路径
        /// </summary>
        public string PATH { get; set; }

        /// <summary>
        /// 表名称
        /// </summary>
        public string TAB { get; set; }

    }
}
