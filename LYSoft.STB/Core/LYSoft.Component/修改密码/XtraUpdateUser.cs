using DevExpress.XtraEditors;
using LYSoft.Center;
using LYSoft.DataBase.SQLite;
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
    public partial class XtraUpdateUser : XtraForm
    {
        public XtraUpdateUser()
        {
            InitializeComponent();
            InitData();
        }

        public void InitData()
        {
            textEdit1.Text = MyApps.User.YHM;
            textEdit2.Text = MyApps.User.DLZH;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string mm = textEdit3.Text;
            string qrmm = textEdit4.Text;
            if(mm=="" || qrmm == "")
            {
                xiaoid.forms.xtraMessage.ShowError("请输入密码.");
                return;
            }
            if (mm != qrmm)
            {
                xiaoid.forms.xtraMessage.ShowError("前后密码不匹配.");
                return;
            }
            string sql = $"UPDATE T_A_DATA_USER SET MM ='{mm}' WHERE ID =" + MyApps.User.ID;
            int count = SQLiteHelper.Execute(sql);
            if(count <= 0)
            {
                xiaoid.forms.xtraMessage.ShowError("s数据保存失败.");
                return;
            }
            else
            {
                xiaoid.forms.xtraMessage.ShowTip("数据保存成功.");
                MyApps.User.MM = mm; //更新缓存中的值
                this.Close();
            }

        }
    }
}
