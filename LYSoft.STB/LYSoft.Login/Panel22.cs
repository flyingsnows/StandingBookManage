using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYSoft.Login
{
    public class Panel22 : DevExpress.XtraEditors.PanelControl
    {
        Image m_bg1;

        /// <summary>
        /// 刷新背景;
        /// </summary>
        public void RefreshBg(Image _bgImage)
        {
            m_bg1 = _bgImage;
            this.DrawBg(null);

        }

        /// <summary>
        /// 显示白色框;
        /// </summary>
        public bool Show980 { get; set; }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            this.DrawBg(e.Graphics);
        }

        private void DrawBg(Graphics _g)
        {

            if (this.Parent == null || this.Parent.BackgroundImage == null)
            {
                return;
            }

            if (m_bg1 == null)
            {
                m_bg1 = this.Parent.BackgroundImage;
                if (m_bg1 == null) return;
            }
            Rectangle _dst = this.ClientRectangle;
            Rectangle _src = new Rectangle(this.Location, this.Size);
            if (_g == null)
            {
                _g = this.CreateGraphics();
            }
            _g.DrawImage(m_bg1, _dst, _src, GraphicsUnit.Pixel);

            if (this.Show980)
            {
                _src = new Rectangle(0, 0, 368, 228);
                _g.DrawImage(Properties.Resources.panel1, _dst, _src, GraphicsUnit.Pixel);
            }

        }
    }
}
