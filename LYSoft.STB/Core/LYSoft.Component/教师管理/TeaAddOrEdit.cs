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
    public partial class TeaAddOrEdit : XtraForm
    {
        private string tzm = "";
        private string path = "";
        private int ID = 0;
        private long CJR = 0;
        public TeaAddOrEdit(string id)
        {
            InitializeComponent();
            ID = id.ObjectToInt();
            textEdit1.Text = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            if (radioGroup1.Properties.Items.Count > 0)
            {
                radioGroup1.SelectedIndex = 0;
            }
            InitData();
        }

        private void InitData()
        {
            if (ID <= 0) return;
            string sql = "SELECT * FROM T_A_DATA_TEACHER WHERE ID=" + ID;
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            if (tab.Rows.Count <= 0)
            {
                return;
            }
            T_A_DATA_TEACHER model = Converts.Convert<T_A_DATA_TEACHER>(tab);
            ID = model.ID.ObjectToInt();
            textEdit1.Text = model.GH;
            textEdit2.Text = model.XM;
            dateEdit2.Text = model.CSRQ;
            dateEdit1.Text = model.RZRQ;
            textEdit3.Text = model.LXDH;
            textEdit4.Text = model.SZXY;
            path = Path.Combine(Application.StartupPath, model.IMG);
            pictureBox1.Image = Image.FromFile(path);
            for (int i = 0; i < radioGroup1.Properties.Items.Count; i++)
            {
                string val = radioGroup1.Properties.Items[i].Value as string;
                if (val == model.XB)
                {
                    radioGroup1.SelectedIndex = i;
                }
            }
            CJR = model.CJR;
        }



        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (tzm == "")
            {
                xiaoid.forms.xtraMessage.ShowError("请先获取人脸特征码.");
                return;
            }

            T_A_DATA_TEACHER model = new T_A_DATA_TEACHER();
            model.ID = ID;
            model.GH = textEdit1.Text;
            model.XM = textEdit2.Text;
            model.CSRQ = dateEdit2.Text;
            model.XB = radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Value as string;
            model.RZRQ = dateEdit1.Text;
            model.LXDH = textEdit3.Text;
            model.SZXY = textEdit4.Text;
            model.TZM = tzm;
            model.IMG = path.Split(new string[] { "\\Debug\\" }, StringSplitOptions.None).LastOrDefault();
            int count = 0;
            if (model.ID <= 0)
            {
                model.TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                T_A_DATA_USER user = new T_A_DATA_USER();
                user.YHM = model.XM;
                user.DLZH = model.GH;
                user.ROLEID = 2;
                user.MM = "123456";
                string sql = user.AddToSQL() + "   ;select last_insert_rowid()";
                int key = SQLiteHelper.ExecuteToKey(sql);
                model.CJR = key;
                sql = model.AddToSQL() + "   ;select last_insert_rowid()";
                int ryid = SQLiteHelper.ExecuteToKey(sql);
                user.ID = key;
                user.UpdateNotNull(); //更新外键
            }
            else
            {
                model.CJR = CJR;
                count = model.UpdateNotNull();
            }

            xiaoid.forms.xtraMessage.ShowTip("数据保存成功.");
            this.Close();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Bitmap img = this.cameraControl1.TakeSnapshot();
            string catalog = MyApps.Imgpath + "\\" + DateTime.Now.ToString("yyyy-MM-dd");
            if (!IOHelper.DireExist(catalog))
            {
                IOHelper.CreateDirectory(catalog);
            }
            path = Path.Combine(catalog, DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png");
            img.Save(path);
            byte[] data = FaceHelper.GetFaceTemplate(img);
            if (data == null)
            {
                xiaoid.forms.xtraMessage.ShowError("特征码获取失败,请重试.");
                return;
            }
            pictureBox1.Image = img;   //获取特征码成功才显示抓拍图
            if (data.Length >= 1040)
            {
                textBox1.Text = data.BytesToBase64();
                tzm = textBox1.Text;
            }
        }
    }
}
