using DevExpress.XtraEditors;
using LYSoft.Center;
using LYSoft.DataBase.SQLite;
using LYSoft.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LYSoft.Component
{
    public partial class StripForm : XtraForm
    {
        private string ID = "";
        public StripForm(string id="")
        {
            InitializeComponent();
            ID =id;
            dateEdit1.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dateEdit2.Text = DateTime.Now.ToString("yyyy-MM-dd");
            if (id != "")
            {
                InitData();
            }else
            {
                //btn_link_xz.Visible = false;
            }
        }


        private void InitData()
        {
            string sql = "SELECT * FROM T_A_DATA_ZBYJXX WHERE ID =" + ID;
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            T_A_DATA_ZBYJXX model = Converts.Convert<T_A_DATA_ZBYJXX>(tab);
            if (model == null)
            {
                xiaoid.forms.xtraMessage.ShowError("数据获取错误.");
                return;
            }
            textEdit1.Text = model.XMBH;
            textEdit2.Text = model.XMMC;
            dateEdit1.Text = model.LXSJ;
            textEdit3.Text = model.ZBJG;
            dateEdit2.Text = model.YJSJ;
            textEdit4.Text = model.ZBFS;
            textEdit5.Text = model.CHSJ;
            textEdit6.Text = model.ZBDW;
            textEdit8.Text = model.YSJE;
            textEdit7.Text = model.BJJE;
            textEdit10.Text = model.ZBJE;
            memoEdit1.Text = model.BMPJ;
            textEdit12.Text = model.SQBM;
            textEdit11.Text = model.LXR;
            textEdit14.Text = model.LXDH;
            //textEdit13.Text = model.HYLCFJ;
            this.Tag = model;
        }


        private void btn_save_Click(object sender, EventArgs e)
        {
            T_A_DATA_ZBYJXX model = this.Tag as T_A_DATA_ZBYJXX;
            if(model == null)
            {
                model = new T_A_DATA_ZBYJXX();
                model.ID = 0;
            }
            //string fjpath = textEdit13.Text;
            model.XMBH = textEdit1.Text;
            model.XMMC = textEdit2.Text;
            model.LXSJ = dateEdit1.DateTime.ToString("yyyy-MM-dd"); ;
            model.ZBJG = textEdit3.Text;
            model.YJSJ = dateEdit2.DateTime.ToString("yyyy-MM-dd"); ;
            model.ZBFS = textEdit4.Text;
            model.CHSJ = textEdit5.Text;
            model.ZBDW = textEdit6.Text;
            model.YSJE = textEdit8.Text;
            model.BJJE = textEdit7.Text;
            model.ZBJE = textEdit10.Text;
            model.BMPJ = memoEdit1.Text;
            model.SQBM = textEdit12.Text;
            model.LXR = textEdit11.Text;
            model.LXDH = textEdit14.Text;
            string catalog = MyApps.Imgpath + "\\" + DateTime.Now.ToString("yyyy-MM-dd");
            if (!IOHelper.DireExist(catalog))
            {
                IOHelper.CreateDirectory(catalog);
            }
            /*if (fjpath != "")
            {
                //拷贝文件到指定位置
                string topath = Path.Combine(catalog, fjpath.Split(new string[] { "\\" }, StringSplitOptions.None).LastOrDefault());
                if(model.HYLCFJ=="" || model.HYLCFJ == null)
                {
                    if (IOHelper.FileExist(topath))
                    {
                        xiaoid.forms.xtraMessage.ShowError("文件名称重复,请更改文件名后重新上传.");
                        return;
                    }
                }
                IOHelper.CopyFile(fjpath, topath, true);
                fjpath = topath.Split(new string[] { "\\Debug\\" }, StringSplitOptions.None).LastOrDefault();
            }*/
            model.HYLCFJ = "招标议价";
            int count = 0;
            if(model.ID <=0)
            {
                model.CJR = MyApps.User.ID;
                model.CJSJ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                count = model.AddToInt();
            }
            else
            {
                count = model.UpdateNotNull();
            }
            if(count <= 0)
            {
                xiaoid.forms.xtraMessage.ShowError("数据保存错误.");
            }
            else
            {
                this.Close();
            }
        }


        private void btn_fj_link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "请选择文件";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;
                //textEdit13.Text = file;
            }
            else
            {
                xiaoid.forms.xtraMessage.ShowError("您还没选择文件.");
            }
        }

        private void btn_link_xz_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                T_A_DATA_ZBYJXX model = this.Tag as T_A_DATA_ZBYJXX;
                if (model == null)
                {
                    xiaoid.forms.xtraMessage.ShowError("数据获取失败.");
                    return;
                }
                if (model.HYLCFJ != null && model.HYLCFJ != "" && model.HYLCFJ.Contains(".pdf"))
                {
                    string apth = Path.Combine(Application.StartupPath, model.HYLCFJ);
                    string path = Path.Combine(Application.StartupPath, apth);
                    XtraPdfViewer from = new XtraPdfViewer(path);
                    from.ShowDialog();
                }
                else
                {
                    xiaoid.forms.xtraMessage.ShowError("未找到上传的pdf文件或文件不是pdf格式.");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                xiaoid.forms.xtraMessage.ShowError("文件打开异常.");
                return;
            }
        }

        private void StripForm_Load(object sender, EventArgs e)
        {

        }

        private void btn_fjmg_Click(object sender, EventArgs e)
        {
            PdfViewForm from = new PdfViewForm(0, "招标议价",ID);
            from.ShowDialog();
        }
    }
}
