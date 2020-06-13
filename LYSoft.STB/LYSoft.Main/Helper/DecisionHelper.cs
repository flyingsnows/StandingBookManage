using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYSoft.Main
{
    /// <summary>
    /// 权限验证
    /// </summary>
    public class DecisionHelper
    {
        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="ribbonControl1">控件</param>
        /// <param name="UserPermission">拥有的菜单</param>
        public void IsPassed(ref RibbonControl ribbonControl1, IList<string> UserPermission)
        {
            foreach (var Item in ribbonControl1.Items)
            {
                //获取当前主界面的所有功能入口
                if (Item.GetType() == typeof(BarButtonItem))
                {
                    BarButtonItem ButtonItem = Item as BarButtonItem;
                    bool IsTrue = UserPermission.Contains(ButtonItem.Caption);
                    if (IsTrue)
                    {
                        ButtonItem.Visibility = BarItemVisibility.Always;
                    }
                    else
                    {
                        ButtonItem.Visibility = BarItemVisibility.Never;
                    }
                }

            }
        }

    }
}
