using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LYSoft.Domain
{
    /// <summary>
    /// 标记属性特性对象
    /// </summary>
    /// <returns></returns>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class MyStringKeyAttribute : Attribute
    {
        public MyStringKeyAttribute(string displayName, int maxLength, bool iskey)
        {
            this.Length = maxLength;
            this.DisplayName = displayName;
            this.IsKey = iskey;
        }
        //显示的名称，对外是只读的，所以不能通过可选参数来赋值，必须在构造函数中对其初始化。
        public string DisplayName { get; private set; }

        //长度最大值，对外是只读的，所以不能通过可选参数来赋值，必须在构造函数中对其初始化。
        public int Length { get; private set; }

        /// <summary>
        /// 是否为key
        /// </summary>
        public bool IsKey { get; private set; }
    }
}
