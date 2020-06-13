using DevExpress.XtraEditors;
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
    public partial class XtraUserInfoform : XtraForm
    {
        public XtraUserInfoform()
        {
            InitializeComponent();
            InitializeGridView();
        }

        /// <summary>
        /// 初始化列
        /// </summary>
        public void InitializeGridView()
        {
            dg1.BeginInit();
            dg1.Columns.Add("ID", xiaoid.forms.xtraDataType.IntegerEdit);
            dg1.Columns.Add("YHM", "用户名", 140, xiaoid.forms.xtraDataType.Label);
            dg1.Columns.Add("DLZH", "登录账号", 140, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("MM", "密码", 160, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("ROLEID", "角色名称", 160, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("k1", "-", -1);
            dg1.EndInit();
        }




        private void simpleButton1_Click(object sender, EventArgs e)
        {
            XtraUserInfoform_Load(null,null);
        }

        private void XtraUserInfoform_Load(object sender, EventArgs e)
        {
            dg1.Rows.Clear();
            string sql = @"SELECT ID,YHM,DLZH,MM,CASE WHEN ROLEID =1 THEN '系统管理员' WHEN ROLEID=2 THEN '教师' WHEN ROLEID =3 THEN '学生' END ROLEID FROM T_A_DATA_USER
where YHM LIKE '%{0}%'";
            sql = string.Format(sql, textEdit1.Text);
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            dg1.Bind(tab);

        }
    }
}
