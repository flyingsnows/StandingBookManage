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
    public partial class XtraExpertAddOrEdit : XtraForm
    {
        private string ID = "";
        public XtraExpertAddOrEdit(string id ="")
        {
            InitializeComponent();
            ID = id;
            if(id != "")
            {
                InitData();
            }else
            {
                //btn_link_xz.Visible = false;
            }
        }


        public void InitData()
        {
            string sql = "SELECT * FROM T_A_DATA_RYXX WHERE ID =" + ID;
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            T_A_DATA_RYXX model = Converts.Convert<T_A_DATA_RYXX>(tab);
            if(model == null)
            {
                xiaoid.forms.xtraMessage.ShowError("数据获取失败.");
                return;
            }
            textEdit1.Text = model.XM;
            textEdit2.Text = model.GZDW;
            dateEdit1.Text = model.CSRQ;
            textEdit4.Text = model.GWZW;
            dateEdit2.Text = model.CJGZSJ;
            textEdit3.Text = model.ZZMM;
            textEdit6.Text = model.XL;
            textEdit5.Text = model.SXZY;
            textEdit7.Text = model.JTZY;
            textEdit10.Text = model.ZYJSZW;
            textEdit9.Text = model.QDSJ;
            textEdit8.Text = model.ZYZG;
            textEdit11.Text = model.SFZJHM;
            textEdit13.Text = model.BGDH;
            textEdit12.Text = model.LXDH;
            textEdit14.Text = model.JG;
            textEdit15.Text = model.ADDS;
            textEdit16.Text = model.ZYCG;
            //textEdit17.Text = model.FJCL;
            if(model.IMG !=null && model.IMG != "")  //加载图片
            {
                if (IOHelper.FileExist(model.IMG))
                {
                    string path = Path.Combine(Application.StartupPath, model.IMG);
                    pictureBox1.Image = Image.FromFile(path);
                }
            }
            this.Tag = model;
        }


        //上传图片
        private void btn_img_link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "请选择图像文件";
            dialog.Filter = "图像文件(*.jpg;*.jpg;*.jpeg;*.gif;*.png)|*.jpg;*.jpeg;*.gif;*.png";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;
                pictureBox1.Image = Image.FromFile(file); //加载到界面显示
            }else
            {
                xiaoid.forms.xtraMessage.ShowError("您还没选择头像图片.");
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            T_A_DATA_RYXX model = this.Tag as T_A_DATA_RYXX;
            if (model == null)
            {
                model = new T_A_DATA_RYXX();
                model.ID = 0;
            }
            string path = "";
            //string fjpath = textEdit17.Text;
            model.XM = textEdit1.Text;
            model.GZDW = textEdit2.Text;
            model.CSRQ = dateEdit1.DateTime.ToString("yyyy-MM-dd");
            model.GWZW = textEdit4.Text;
            model.CJGZSJ = dateEdit2.DateTime.ToString("yyyy-MM-dd"); ;
            model.ZZMM = textEdit3.Text;
            model.XL = textEdit6.Text;
            model.SXZY = textEdit5.Text;
            model.JTZY = textEdit7.Text;
            model.ZYJSZW = textEdit10.Text;
            model.QDSJ = textEdit9.Text;
            model.ZYZG = textEdit8.Text;
            model.SFZJHM = textEdit11.Text;
            model.BGDH = textEdit13.Text;
            model.LXDH = textEdit12.Text;
            model.JG = textEdit14.Text;
            model.ADDS = textEdit15.Text;
            model.ZYCG = textEdit16.Text;
            string catalog = MyApps.Imgpath + "\\" + DateTime.Now.ToString("yyyy-MM-dd");
            if (!IOHelper.DireExist(catalog))
            {
                IOHelper.CreateDirectory(catalog);
            }
            /*if (fjpath != "")
            {
                //拷贝文件到指定位置
                string topath = Path.Combine(catalog, fjpath.Split(new string[] { "\\" }, StringSplitOptions.None).LastOrDefault());
                if (IOHelper.FileExist(topath))
                {
                    if(model.FJCL==""|| model.FJCL == null)
                    {
                        xiaoid.forms.xtraMessage.ShowError("文件名称重复,请更改文件名后重新上传.");
                        return;
                    }
                }
                IOHelper.CopyFile(fjpath, topath, true);
                fjpath = topath.Split(new string[] { "\\Debug\\" }, StringSplitOptions.None).LastOrDefault();
            }*/
            if (pictureBox1.Image != null)  //保存图片在文件根目录
            {
                Bitmap img = pictureBox1.Image as Bitmap;
                path = Path.Combine(catalog, DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png");
                img.Save(path);
                path = path.Split(new string[] { "\\Debug\\" }, StringSplitOptions.None).LastOrDefault();
            }
            //model.FJCL = fjpath;  //附件材料路径
            model.FJCL = "专家";
            model.IMG = path; //照片相对路径
            int count = 0;
            if (model.ID <= 0)
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
                xiaoid.forms.xtraMessage.ShowError("数据保存错误.");
                return;
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
                //textEdit17.Text = file;
            }
            else
            {
                xiaoid.forms.xtraMessage.ShowError("您还没选择文件.");
            }
        }

        //下载附件
        private void btn_link_xz_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                try
                {
                    T_A_DATA_RYXX model = this.Tag as T_A_DATA_RYXX;
                    if (model == null)
                    {
                        xiaoid.forms.xtraMessage.ShowError("数据获取失败.");
                        return;
                    }
                    if (model.FJCL != null && model.FJCL != "" && model.FJCL.Contains(".pdf"))
                    {
                        string apth = Path.Combine(Application.StartupPath, model.FJCL);
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
            catch(Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                xiaoid.forms.xtraMessage.ShowError("文件打开异常.");
                return;
            }
        }

        private void btn_fjmg_Click(object sender, EventArgs e)
        {
            PdfViewForm from = new PdfViewForm(0, "专家",ID);
            from.ShowDialog();
        }
    }
}
