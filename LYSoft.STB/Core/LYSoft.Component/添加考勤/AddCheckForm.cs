using DevExpress.XtraEditors;
using LYSoft.Center;
using LYSoft.DataBase.SQLite;
using LYSoft.Domain;
using LYSoft.FaceSDK;
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
    public partial class AddCheckForm : XtraForm
    {
        string temppath = "";
        public AddCheckForm()
        {
            InitializeComponent();
            dateEdit1.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dateEdit2.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        //识别
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Bitmap img = this.cameraControl1.TakeSnapshot();
            float similarity = 0;
            int ryid = 0;
            int result =  FaceHelper.CompareFeature(img,ref similarity,ref ryid);
            if(result <= 0)
            {
                xiaoid.forms.xtraMessage.ShowError("人脸识别异常,请重试.");
                return;
            }
            textEdit6.Text = similarity.ToString();  //相似度
            pictureBox1.Image = img;
            string catalog = MyApps.Imgpath + "\\" + DateTime.Now.ToString("yyyy-MM-dd");
            if (!IOHelper.DireExist(catalog))
            {
                IOHelper.CreateDirectory(catalog);
            }
            temppath = Path.Combine(catalog, DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png");
            img.Save(temppath);  //保存当前图片

            string sql = "SELECT * FROM T_A_DATA_STUDENT WHERE ID =" + ryid;
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            if (tab.Rows.Count <= 0)
            {
                xiaoid.forms.xtraMessage.ShowError("未找到该人脸详细信息,请重试.");
                return;
            }
            T_A_DATA_STUDENT model = Converts.Convert<T_A_DATA_STUDENT>(tab);
            textEdit1.Text = model.XH;
            textEdit2.Text = model.XM;
            textEdit3.Text = model.SZXY;
            textEdit4.Text = model.SZBJ;
            this.Tag = model;
            string path1 = Path.Combine(Application.StartupPath, model.IMG);
            pictureBox2.Image = Image.FromFile(path1);

        }


        //保存
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            T_A_DATA_KQXX model = new T_A_DATA_KQXX();
            T_A_DATA_STUDENT std = this.Tag as T_A_DATA_STUDENT;
            if(std == null)
            {
                xiaoid.forms.xtraMessage.ShowError("请先识别人脸.");
                return;
            }
            if (textEdit5.Text == "")
            {
                xiaoid.forms.xtraMessage.ShowError("请输入课程名称.");
                return;
            }
            model.KCMC = textEdit5.Text;
            model.KSSJ = dateEdit1.Text +" "+ timeEdit1.Text;
            model.JSSJ = dateEdit2.Text + " " + timeEdit2.Text;
            model.RYID = std.ID;
            model.XSD = textEdit6.Text;
            model.CJSJ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            model.CJR = MyApps.User.ID;
            model.ID = 0;
            model.IMG = model.IMG = temppath.Split(new string[] { "\\Debug\\" }, StringSplitOptions.None).LastOrDefault();
            int count = model.AddToInt();
            if(count <= 0)
            {
                xiaoid.forms.xtraMessage.ShowError("数据保存错误.");
            }
            else
            {
                xiaoid.forms.xtraMessage.ShowTip("打卡成功.");
                textEdit5.Text = "";
            }
        }
    }
}
