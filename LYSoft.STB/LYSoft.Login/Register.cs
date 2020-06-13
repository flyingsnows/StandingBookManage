using DevExpress.XtraEditors;
using LYSoft.DataBase.SQLite;
using LYSoft.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LYSoft.Login
{
    public partial class Register : XtraForm
    {
        public Register()
        {
            InitializeComponent();
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "Metropolis";
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string yhm = textEdit1.Text;
            string dlzh = textEdit2.Text;
            string mm = textEdit3.Text;
            string qrmm = textEdit4.Text;
            if (dlzh == "")
            {
                xiaoid.forms.xtraMessage.ShowError("请输入登录账号.");
                return;
            }
            if (mm != qrmm)
            {
                xiaoid.forms.xtraMessage.ShowError("前后密码不匹配.");
                return;
            }

            string sql = $"SELECT * FROM T_A_DATA_USER WHERE DLZH ='{dlzh}'";
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            if (tab.Rows.Count > 0)
            {
                xiaoid.forms.xtraMessage.ShowError("已存在登录账号，请重试.");
                return;
            }

            T_A_DATA_USER model = new T_A_DATA_USER();
            model.DLZH = dlzh;
            model.MM = mm;
            model.YHM = yhm;
            model.ROLEID = 1;
            int count = model.AddToInt();
            if (count <= 0)
            {
                xiaoid.forms.xtraMessage.ShowError("数据保存错误.");
                return;
            }
            else
            {
                xiaoid.forms.xtraMessage.ShowTip("注册成功，请牢记密码.");
                this.Close();
            }
        }
    }
}
