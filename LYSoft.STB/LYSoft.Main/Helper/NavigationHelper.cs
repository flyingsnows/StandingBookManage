using DevExpress.XtraTabbedMdi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars;
using LYSoft.Main;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;

namespace LYSoft.Main
{
    /// <summary>
    /// 处理功能模块打开关闭的帮助类
    /// </summary>
    public class NavigationHelper
    {

        /// <summary>
        /// 激活指定的子窗口, 如果该子窗口不存在, 则返回false;
        /// </summary>
        /// <param name="Text">窗口标题;</param>
        /// <returns></returns>
        public bool ShowChildForm(string Text, ref XtraTabbedMdiManager TabbedMdiManager)
        {
            for (int i = 0; i < TabbedMdiManager.Pages.Count; i++)
            {
                if (TabbedMdiManager.Pages[i].Text == Text)
                {
                    TabbedMdiManager.SelectedPage = TabbedMdiManager.Pages[i];

                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 显示子窗体
        /// </summary>
        /// <param name="_text"></param>
        /// <param name="_canClose"></param>
        /// <param name="_form"></param>
        /// <param name="_icon"></param>
        /// <param name="TabbedMdiManager"></param>
        /// <param name="MainForm"></param>
        public void ShowChildForm(string _text, bool _canClose, Form _form, Image _icon, ref XtraTabbedMdiManager TabbedMdiManager, RibbonForm MainForm)
        {

            _form.Text = _text;
            _form.MdiParent = MainForm;
            _form.Show();

            int i;
            i = TabbedMdiManager.Pages.Count - 1;
            //mdi1.Pages[i].Image = My.Resources.option_24x24;

            if (_canClose)
            {
                TabbedMdiManager.Pages[i].ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                TabbedMdiManager.Pages[i].ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
            }

            if (_icon != null)
            {
                Image _img;
                _img = CreateTabImage(_icon);
                if (_img != null)
                {
                    TabbedMdiManager.Pages[i].Image = _img;
                    return;
                }
            }
            else
            {
                //使用默认图形;
                TabbedMdiManager.Pages[i].Image = Properties.Resources.win_24x24;
            }
        }

        /// <summary>
        /// 显示子窗体
        /// </summary>
        /// <param name="_text"></param>
        /// <param name="_canClose"></param>
        /// <param name="_form"></param>
        /// <param name="_icon"></param>
        /// <param name="TabbedMdiManager"></param>
        /// <param name="MainForm"></param>
        public void ShowChildForm(string _text, bool _canClose, Form _form, Image _icon, ref XtraTabbedMdiManager TabbedMdiManager, XtraForm MainForm)
        {

            _form.Text = _text;
            _form.MdiParent = MainForm;
            _form.Show();

            int i;
            i = TabbedMdiManager.Pages.Count - 1;
            //mdi1.Pages[i].Image = My.Resources.option_24x24;

            if (_canClose)
            {
                TabbedMdiManager.Pages[i].ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                TabbedMdiManager.Pages[i].ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
            }

            if (_icon != null)
            {
                Image _img;
                _img = CreateTabImage(_icon);
                if (_img != null)
                {
                    TabbedMdiManager.Pages[i].Image = _img;
                    return;
                }
            }
            else
            {
                //使用默认图形;
                TabbedMdiManager.Pages[i].Image = Properties.Resources.win_24x24;
            }
        }

        /// <summary>
        /// 放大图标
        /// </summary>
        /// <param name="_img"></param>
        /// <returns></returns>
        private Image CreateTabImage(Image _img)
        {
            if (_img == null)
            {
                return null;
            }

            if (_img.Width == 24 && _img.Height == 24)
            {
                return _img;
            }

            Bitmap _bmp;
            Graphics _g;

            _bmp = new Bitmap(24, 24, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            _g = Graphics.FromImage(_bmp);
            _g.DrawImage(_img, new Rectangle(0, 0, 24, 24), new Rectangle(0, 0, _img.Width, _img.Height), GraphicsUnit.Pixel);

            _g.Dispose();
            _g = null;

            return _bmp;

        }



    }
}
