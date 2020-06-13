using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LYSoft.Component
{
    public partial class XtraPdfViewer : XtraForm
    {
        public XtraPdfViewer(string path)
        {
            InitializeComponent();
            try
            {
                this.pdfViewer1.LoadDocument(path);  //加载pdf文件显示
            }
            catch(Exception ex)
            {
                xiaoid.forms.xtraMessage.ShowError("文件打开错误.");
            }
          
        }
    }
}
