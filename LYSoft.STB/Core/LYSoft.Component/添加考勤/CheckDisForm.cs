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
    public partial class CheckDisForm : XtraForm
    {
        public CheckDisForm(T_A_DATA_KQXX data)
        {
            InitializeComponent();
            InitData(data);
        }

        public void InitData(T_A_DATA_KQXX model)
        {
            string sql = "SELECT * FROM T_A_DATA_STUDENT WHERE ID=" + model.RYID;
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            T_A_DATA_STUDENT data = Converts.Convert<T_A_DATA_STUDENT>(tab);
            if (data == null)
            {
                xiaoid.forms.xtraMessage.ShowError("获取数据失败,请重试.");
                return;
            }
            else
            {
                textEdit1.Text = data.XH;
                textEdit2.Text = data.XM;
                textEdit3.Text = data.SZXY;
                textEdit4.Text = data.SZBJ;
                textEdit5.Text = model.KCMC;
                textEdit7.Text = model.KSSJ;
                textEdit8.Text = model.JSSJ;
                textEdit6.Text = model.XSD;
                //pictureBox1
                string path = Path.Combine(Application.StartupPath, model.IMG);
                pictureBox1.Image = Image.FromFile(path);

                path = Path.Combine(Application.StartupPath, data.IMG);
                pictureBox2.Image = Image.FromFile(path);

            }
        }
    }
}
