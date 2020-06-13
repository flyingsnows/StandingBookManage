using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LYSoft.Domain
{
    public class BASEDOMAIN
    {
        [MyStringKey("主键", 4, true)]
        public Int64 ID { get; set; }
    }
}
