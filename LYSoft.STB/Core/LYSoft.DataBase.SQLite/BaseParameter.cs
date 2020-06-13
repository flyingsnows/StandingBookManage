using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LYSoft.DataBase.SQLite
{
    /// <summary>
    /// 写特殊数据的参数集合
    /// </summary>
    public class BaseParameter
    {
        /// <summary>
        /// 参数名称 @
        /// </summary>
        public string Parame { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public SqlDbType DbType { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object DbValue { get; set; }
    }
}
