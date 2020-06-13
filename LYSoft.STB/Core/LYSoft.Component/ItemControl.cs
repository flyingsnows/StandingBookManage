using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LYSoft.Domain;
using System.IO;
using LYSoft.Center;

namespace LYSoft.Component
{
    public partial class ItemControl : UserControl
    {
        public ItemControl()
        {
            InitializeComponent();
        }

        //单击打开
        private void ItemControl_Click(object sender, EventArgs e)
        {
            PdfModel model = this.Tag as PdfModel;
            if (model == null)
            {
                xiaoid.forms.xtraMessage.ShowError("获取数据错误");
                return;
            }
            string fjlj = model.PATH;
            string path = Path.Combine(Application.StartupPath, fjlj);
            if (IOHelper.FileExist(path) &&path.Contains(".pdf"))
            {
                XtraPdfViewer from = new XtraPdfViewer(path);
                from.ShowDialog();
            }
            else
            {
                xiaoid.forms.xtraMessage.ShowError("获取文件或文件格式错误.");
                return;
            }
          
        }
    }
}
