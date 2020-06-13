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
    public partial class XtraSupFrom : XtraForm
    {
        private string ID = "";
        public XtraSupFrom(string id="")
        {
            InitializeComponent();
            radioGroup1.SelectedIndex = 0;
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
            string sql = "SELECT * FROM T_A_DATA_GYSXX WHERE ID=" + ID;
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            T_A_DATA_GYSXX model = Converts.Convert<T_A_DATA_GYSXX>(tab);
            if (model == null)
            {
                xiaoid.forms.xtraMessage.ShowError("数据获取失败.");
                return;
            }
            textEdit1.Text = model.GSMC;
            textEdit2.Text = model.XYDM;
            textEdit4.Text = model.ADDS;
            textEdit9.Text = model.FRDB;
            textEdit10.Text = model.ZCZB;
            dateEdit2.Text = model.CLSJ;
            textEdit11.Text = model.YXRQ;
            memoEdit1.Text = model.JYFW;
            textEdit3.Text = model.ZYFWLX;
            textEdit5.Text = model.LXR;
            textEdit6.Text = model.LXDH;
            textEdit7.Text = model.XGZZ;
            textEdit8.Text = model.HZXM;
            for(int i = 0; i < radioGroup1.Properties.Items.Count; i++)
            {
                string val = radioGroup1.Properties.Items[i].Value as string;
                if (val == model.SFHZ)
                {
                    radioGroup1.SelectedIndex = i;
                }
            }
            this.Tag = model;
        }
        
        
        private void btn_save_Click(object sender, EventArgs e)
        {
            T_A_DATA_GYSXX model =this.Tag as  T_A_DATA_GYSXX;
            if (model == null)
            {
                model = new T_A_DATA_GYSXX();
                model.ID = 0;
            }
            model.GSMC = textEdit1.Text;
            model.XYDM = textEdit2.Text;
            model.ADDS = textEdit4.Text;
            model.FRDB = textEdit9.Text;
            model.ZCZB = textEdit10.Text;
            model.CLSJ = dateEdit2.DateTime.ToString("yyyy-MM-dd"); 
            model.YXRQ = textEdit11.Text; 
            model.SFHZ = radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Value as string;
            model.JYFW = memoEdit1.Text;
            model.ZYFWLX = textEdit3.Text;
            model.LXR = textEdit5.Text;
            model.LXDH = textEdit6.Text;
            model.XGZZ = textEdit7.Text;
            model.HZXM = textEdit8.Text;
            //string fjpath = linkLabel1.Tag?.ToString();
            string catalog = MyApps.Imgpath + "\\" + DateTime.Now.ToString("yyyy-MM-dd");
            if (!IOHelper.DireExist(catalog))
            {
                IOHelper.CreateDirectory(catalog);
            }
            /*if (fjpath != "" && fjpath!=null)
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
          if (model.XGZZFJ == null)
            {
                model.XGZZFJ = "供应商";
            }
            int count = 0;
            if (ID.ObjectToInt() <= 0)
            {
                model.CJR = MyApps.User.ID;
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
            PdfViewForm from = new PdfViewForm(0,"供应商",ID);
            from.ShowDialog();
           
        }
    }
}
