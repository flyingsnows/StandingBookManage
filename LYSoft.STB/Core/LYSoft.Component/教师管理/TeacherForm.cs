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
    public partial class TeacherForm : XtraForm
    {
        public TeacherForm()
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
            dg1.Columns.Add("GH", "工号", 140, xiaoid.forms.xtraDataType.Label);
            dg1.Columns.Add("XM", "姓名", 140, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("XB", "性别", 85, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("CSRQ", "出生日期", 145, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("RZRQ", "入职日期", 145, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("SZXY", "所在学院", 160, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("LXDH", "联系电话", 160, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("TIME", "创建时间", 156, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("k1", "-", -1);
            dg1.EndInit();
        }







        private void TeacherForm_Load(object sender, EventArgs e)
        {
            dg1.Rows.Clear();
            string sql = @"SELECT ID,XM,GH,XB,CSRQ,RZRQ,SZXY,LXDH,TIME FROM T_A_DATA_TEACHER WHERE XM LIKE '%{0}%'";
            sql = string.Format(sql, textEdit1.Text);
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            dg1.Bind(tab);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            TeacherForm_Load(null,null);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            TeaAddOrEdit from = new TeaAddOrEdit("0");
            from.ShowDialog();
            TeacherForm_Load(null, null);
        }

        private void dg1_MouseUp(object sender, MouseEventArgs e)
        {
            if (dg1.Rows.Count > 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    popupMenu1.ShowPopup(Control.MousePosition);
                }
            }
        }

        private void btn_item_delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] IndexList = dg1.GetSelectedRows(true);
            int Index = IndexList[0];
            string Id = dg1.GetCellValue(Index, "ID").ToString();
            string sql = "DELETE FROM T_A_DATA_TEACHER WHERE ID =" + Id;
            int count = SQLiteHelper.Execute(sql);
            if(count <= 0)
            {
                xiaoid.forms.xtraMessage.ShowError("数据保存错误.");
            }
            else
            {
                TeacherForm_Load(null, null);
            }
        }

        private void btn_item_edit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] IndexList = dg1.GetSelectedRows(true);
            int Index = IndexList[0];
            string Id = dg1.GetCellValue(Index, "ID").ToString();
            TeaAddOrEdit from = new TeaAddOrEdit(Id);
            from.ShowDialog();
            TeacherForm_Load(null, null);
        }
    }
}
