using DevExpress.XtraBars.Helpers;
using DevExpress.XtraEditors;
using LYSoft.Center;
using LYSoft.Component;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LYSoft.Main
{
    public partial class RibbonFormMain : XtraForm
    {

        /// <summary>
        /// 功能模块打开逻辑帮助类
        /// </summary>
        NavigationHelper NavHelper;
        long userId = 0;
        public RibbonFormMain(long userId=0)
        {
            this.userId = userId; 
            InitializeComponent();
            NavHelper = new NavigationHelper();
        }

        private void RibbonFormMain_Load(object sender, EventArgs e)
        {
            
        }

     


        
     
        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (NavHelper.ShowChildForm(btn_User.Caption, ref TabbedMdiManager))
            {
                return;
            }

            XtraUserInfoform XtraFrom = new XtraUserInfoform();
            XtraFrom.MdiParent = this;
            NavHelper.ShowChildForm(btn_User.Caption, true, XtraFrom, btn_User.SmallImage, ref TabbedMdiManager, this);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bar_UpdatePaw_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            XtraUpdateUser XtraFrom = new XtraUpdateUser();
            XtraFrom.ShowDialog();
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bar_logout_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DialogResult _ret = xiaoid.forms.xtraMessage.ShowQuestion("要注销当前用户吗?");
            if (_ret != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }
            Application.DoEvents(); //处理win消息
            this.Close();
           Environment.Exit(0); //强制关闭所有线程和进程

        }

     
        private void btn_Book_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (NavHelper.ShowChildForm(btn_Book.Caption, ref TabbedMdiManager))
            {
                return;
            }
            XtrasupplierForm XtraFrom = new XtrasupplierForm();
            XtraFrom.MdiParent = this;
            NavHelper.ShowChildForm(btn_Book.Caption, true, XtraFrom, btn_Book.SmallImage, ref TabbedMdiManager, this);
        }

        
        private void bar_Item_Borr_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (NavHelper.ShowChildForm(bar_Item_Borr.Caption, ref TabbedMdiManager))
            {
                return;
            }
            XtraexpertForm XtraFrom = new XtraexpertForm();
            XtraFrom.MdiParent = this;
            NavHelper.ShowChildForm(bar_Item_Borr.Caption, true, XtraFrom, bar_Item_Borr.SmallImage, ref TabbedMdiManager, this);
        }
        
     

        private void btn_item_jt_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (NavHelper.ShowChildForm(btn_item_jt.Caption, ref TabbedMdiManager))
            {
                return;
            }
            XtraBiddingForm XtraFrom = new XtraBiddingForm();
            XtraFrom.MdiParent = this;
            NavHelper.ShowChildForm(btn_item_jt.Caption, true, XtraFrom, btn_item_jt.SmallImage, ref TabbedMdiManager, this);

        }

        private void RibbonFormMain_Resize(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
        }

        private void RibbonFormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void btn_file_pdf_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (NavHelper.ShowChildForm(btn_file_mg.Caption, ref TabbedMdiManager))
            {
                return;
            }
            XtrafilesFrom XtraFrom = new XtrafilesFrom();
            XtraFrom.MdiParent = this;
            NavHelper.ShowChildForm(btn_file_mg.Caption, true, XtraFrom, btn_file_mg.SmallImage, ref TabbedMdiManager, this);
        }
    }
}
