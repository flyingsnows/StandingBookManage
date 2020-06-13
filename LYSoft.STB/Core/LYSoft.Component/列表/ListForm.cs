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
    public partial class ListForm : XtraForm
    {
        public ListForm()
        {
            InitializeComponent();
            InitializeForms();
            InitializeData();
        }

        private void InitializeForms()
        {
            dg1.BeginInit();
            dg1.Columns.Add("ID", xiaoid.forms.xtraDataType.LongEdit);
            dg1.Columns.Add("XM", "姓名", 80, xiaoid.forms.xtraDataType.Label);
            dg1.Columns.Add("SFZJHM", "身份证件号码", 150, xiaoid.forms.xtraDataType.Label);
            dg1.Columns.Add("XB", "性别", 75, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("NL", "年龄", 75, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("CSRQ", "出生日期", 125, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("JZKS", "就诊科室", 125, xiaoid.forms.xtraDataType.Label);
            dg1.Columns.Add("JZSJ", "就诊时间", 200, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("k1", "-", 100);
            dg1.EndInit();
        }


        public void InitializeData()
        {
            string txt = textEdit1.Text;
            dg1.Rows.Clear();
            string sql = @"SELECT A.ID,A.XM,A.SFZJHM,A.XB,A.NL,A.CSRQ,B.JZKS,B.JZSJ FROM T_A_DATA_RYXX A 
LEFT JOIN T_A_DATA_JZXX  B ON A.ID = B.RYID  WHERE A.XM LIKE '%{0}%'";
            sql = string.Format(sql, txt);
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            dg1.Bind(tab);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            InitializeData();
        }
    }
}
