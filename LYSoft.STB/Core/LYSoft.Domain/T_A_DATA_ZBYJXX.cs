using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LYSoft.Domain
{
    public class T_A_DATA_ZBYJXX : BASEDOMAIN
    {
        public string XMBH { get; set; }//编号    
        public string XMMC { get; set; }//项目名称
        public string LXSJ { get; set; }//立项时间
        public string ZBJG { get; set; }//招标机构
        public string YJSJ { get; set; }//议价时间
        public string ZBFS { get; set; }//招标方式
        public string CHSJ { get; set; }//参会商家
        public string ZBDW { get; set; }//中标单位
        public string YSJE { get; set; }//预算金额
        public string BJJE { get; set; }//报价金额
        public string ZBJE { get; set; }//中标金额
        public string BMPJ { get; set; }//原为部门评价，后客户改为议价情况说明
        public string SQBM { get; set; }//申请部门
        public string LXR { get; set; }//联系人
        public string LXDH { get; set; }//联系电话
        public string HYLCFJ { get; set; }//会议流程附件
        public Int64 CJR { get; set; }//
        public string CJSJ { get; set; }//创建时间

    }
}
