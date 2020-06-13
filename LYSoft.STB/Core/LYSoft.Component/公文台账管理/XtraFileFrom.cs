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
    public partial class XtraFileFrom : XtraForm
    {
        private string ID = "";
        public XtraFileFrom(string id="")
        {
            InitializeComponent();
            ID = id;
            if(id != "")
            {
                InitData();
            }else
            {
                //linkLabel2.Visible = false;
            }
        }

        public void InitData()
        {
            string sql = "SELECT * FROM T_A_DATA_GWGL WHERE ID=" + ID;
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            T_A_DATA_GWGL model = Converts.Convert<T_A_DATA_GWGL>(tab);
            if (model == null)
            {
                xiaoid.forms.xtraMessage.ShowError("数据获取失败.");
                return;
            }
            text_wjbh.Text = model.WJBH;
            text_wjmc.Text = model.WJMC;
            text_xfbm.Text = model.XFBM;
            date_xfsj.Text = model.XFSJ;
            text_xgywbm.Text = model.XGYWBM;
            text_gwlj.Text = model.GWZL;
            this.Tag = model;
        }
        
        
        private void btn_save_Click(object sender, EventArgs e)
        {
            T_A_DATA_GWGL model =this.Tag as  T_A_DATA_GWGL;
            if (model == null)
            {
                model = new T_A_DATA_GWGL();
                model.ID = 0;
            }
            model.WJBH = text_wjbh.Text;
            model.WJMC = text_wjmc.Text;
            model.XFBM = text_xfbm.Text;
            model.XFSJ = date_xfsj.DateTime.ToString("yyyy-MM-dd"); 
            model.XGYWBM = text_xgywbm.Text;
            model.GWZL = text_gwlj.Text;
            //string fjpath = linkLabel1.Tag?.ToString();
            /*
             * 
             * string catalog = MyApps.Imgpath + "\\" + DateTime.Now.ToString("yyyy-MM-dd");
            if (!IOHelper.DireExist(catalog))
            {
                IOHelper.CreateDirectory(catalog);
            }if (fjpath != "" && fjpath!=null)
            {
                //拷贝文件到指定位置
                string topath = Path.Combine(catalog, fjpath.Split(new string[] { "\\" }, StringSplitOptions.None).LastOrDefault());
                if (IOHelper.FileExist(topath))
                {
                    if (model.XGZZFJ == "" || model.XGZZFJ == null)
                    {
                        xiaoid.forms.xtraMessage.ShowError("文件名称重复,请更改文件名后重新上传.");
                        return;
                    }
                }
                IOHelper.CopyFile(fjpath, topath, true);
                fjpath = topath.Split(new string[] { "\\Debug\\" }, StringSplitOptions.None).LastOrDefault();
            }*/
            //model.XGZZFJ = fjpath;
            int count = 0;
            if (ID.ObjectToInt() <= 0)
            {
                model.UID = MyApps.User.ID;
                model.CJSJ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                count = model.AddToInt();
            }
            else
            {
                count = model.UpdateNotNull();
            }
            if (count <= 0)
            {
                xiaoid.forms.xtraMessage.ShowError("数据保存失败");
                return;
            }
            else
            {
                this.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "请选择文件";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;
                //linkLabel1.Tag = file;
                //textEdit7.Text = file;
            }
            else
            {
                xiaoid.forms.xtraMessage.ShowError("您还没选择文件.");
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            T_A_DATA_GYSXX model = this.Tag as T_A_DATA_GYSXX;
            if (model == null)
            {
                xiaoid.forms.xtraMessage.ShowError("数据获取失败.");
                return;
            }
            if (model.XGZZFJ != null && model.XGZZFJ != "" && model.XGZZFJ.Contains(".pdf"))
            {
                string apth = Path.Combine(Application.StartupPath, model.XGZZFJ);
                string path = Path.Combine(Application.StartupPath, apth);
                XtraPdfViewer from = new XtraPdfViewer(path);
                from.ShowDialog();
            }
            else
            {
                xiaoid.forms.xtraMessage.ShowError("未找到上传的pdf文件或文件不是pdf格式.");
            }
        }

        private void btn_fjmg_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "请选择Excel文件";
            dialog.Filter = "上传文件(*.xls;*.xlsx;*.doc;*.docx;*.pdf)|*.xls;*.xlsx;*.doc;*.docx;*.pdf;";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                text_gwlj.Text= dialog.FileName;
                if (text_gwlj.Text == "")
                {
                    xiaoid.forms.xtraMessage.ShowError("文件未导入.");
                }
                else {
                    xiaoid.forms.xtraMessage.ShowInfo("文件上传成功.");
                }
            }
        }

        private void btn_opengw_Click(object sender, EventArgs e)
        {
            string pjcl = text_gwlj.Text;
            if (pjcl != null && pjcl != "" && pjcl.Contains(".pdf"))
            {
                string path = Path.Combine(Application.StartupPath, pjcl);
                XtraPdfViewer from = new XtraPdfViewer(path);
                from.ShowDialog();
            }
            else if (pjcl.Contains(".doc") || pjcl.Contains(".docx"))
            {
                string apth = Path.Combine(Application.StartupPath, pjcl);
                string path = Path.Combine(Application.StartupPath, apth);
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.FileName = path;
                p.Start();
            }
            else if (pjcl.Contains(".xls") || pjcl.Contains(".xlsx"))
            {
                string apth = Path.Combine(Application.StartupPath, pjcl);
                string path = Path.Combine(Application.StartupPath, apth);
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.FileName = path;
                p.Start();
            }
            else
            {
                xiaoid.forms.xtraMessage.ShowError("未找到上传的文件或文件格式错误.");
            }
        }
    }
}
