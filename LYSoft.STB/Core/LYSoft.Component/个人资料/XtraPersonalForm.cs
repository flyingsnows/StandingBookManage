using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Luxand;
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
    public partial class XtraPersonalForm : XtraForm
    {
        string path = "";
        private Int64 ID =0;
        string tzm = "";
        public XtraPersonalForm()
        {
            InitializeComponent();
            radioGroup1.SelectedIndex = 0;
            InitData();
        }


        public void InitData()
        {
            string sql = "SELECT * FROM T_A_DATA_STUDENT WHERE CJR =" +MyApps.User.ID;
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            if(tab.Rows.Count <= 0)
            {
                return;
            }
            T_A_DATA_STUDENT model = Converts.Convert<T_A_DATA_STUDENT>(tab);
            textEdit1.Text = model.XH;
            textEdit2.Text = model.XM;
            dateEdit1.Text = model.CSRQ;
            dateEdit2.Text = model.RXRQ;
            textEdit6.Text = model.SZXY;
            textEdit7.Text = model.SZBJ;
            textEdit8.Text = model.LXDH;
            textBox1.Text = model.TZM;
            path = model.IMG;
            tzm = model.TZM;
            string path1  = Path.Combine(Application.StartupPath, model.IMG);
            pictureBox1.Image = Image.FromFile(path1);
            for(int i = 0; i < radioGroup1.Properties.Items.Count; i++)
            {
                string val = radioGroup1.Properties.Items[i].Value as string;
                if(val == model.XB)
                {
                    radioGroup1.SelectedIndex = i;
                }
            }
            ID = model.ID;
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
            if(data == null)
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


        //保存
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (tzm == "")
            {
                xiaoid.forms.xtraMessage.ShowError("请先获取特征码.");
                return;
            }
            T_A_DATA_STUDENT model = new T_A_DATA_STUDENT();
            model.ID = ID;
            model.XH = textEdit1.Text;
            model.XM = textEdit2.Text;
            model.XB = radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Value as string;
            model.CSRQ = dateEdit1.Text;
            model.RXRQ = dateEdit2.Text;
            model.SZXY = textEdit6.Text;
            model.SZBJ = textEdit7.Text;
            model.LXDH = textEdit8.Text;
            model.TZM = tzm;
            model.CJR = MyApps.User.ID;
            model.TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            model.IMG =  path.Split(new string[] { "\\Debug\\" }, StringSplitOptions.None).LastOrDefault();
            int count = 0;
            if (model.ID <= 0)
            {
                string sql = model.AddToSQL() + "   ;select last_insert_rowid()";
                int key = SQLiteHelper.ExecuteToKey(sql);
                sql = $"UPDATE T_A_DATA_USER SET RYID={key} WHERE ID =" + MyApps.User.ID;
                SQLiteHelper.Execute(sql);
                count = 1;
            }
            else
            {
                count = model.UpdateNotNull();
            }
            if (count >= 0)
            {
                xiaoid.forms.xtraMessage.ShowTip("数据保存成功.");
                return;
            }
        }
    }
}
