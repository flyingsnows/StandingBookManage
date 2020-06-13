using DevExpress.XtraEditors;
using LYSoft.Center;
using LYSoft.DataBase.SqlServer;
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
    public partial class QueryListForm : XtraForm
    {
        int TYPE = -1;
        public QueryListForm(string cph,string xm,int type)
        {
            InitializeComponent();
            CPH.Text = cph;
            XM.Text = xm;
            TYPE = type;
            InitializeGridView();
            InitializeData();
            if(type == 0)
            {
                this.Text = "进货明细";
            }
            else
            {
                this.Text = "出货明细";
            }
        }
        
        public void InitializeGridView()
        {
            dg1.BeginInit();
            dg1.Columns.Add("RYID", xiaoid.forms.xtraDataType.IntegerEdit);
            dg1.Columns.Add("CPH", "车号", 140, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("XM", "姓名", 140, xiaoid.forms.xtraDataType.Label);
            dg1.Columns.Add("MZ", "毛重", 160, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("PZ", "皮重", 160, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("JG", "价格", 140, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("JZ", "净重", 140, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("TIME", "时间", 220, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.EndInit();
        }


        public void InitializeData()
        {
            dg1.Rows.Clear();
            string stime = dateEdit1.Text == "" ? "" : dateEdit1.Text + " " + timeEdit1.Text;
            string etime = dateEdit2.Text == "" ? "" : dateEdit2.Text + " " + timeEdit2.Text;
            string sql = @"SELECT A.ID RYID,A.XM,A.CPH,B.MZ,B.PZ,B.JG,B.MZ - B.PZ JZ,B.[TIME] FROM T_A_SYSTEM_JCXX A 
LEFT JOIN T_A_SYSTEM_CRKXX B ON A.ID = B.RYID
WHERE A.XM LIKE '%{0}%' AND A.CPH LIKE '%{1}%' AND B.LX={4}  AND (''='{2}' OR B.[TIME]>='{2}')  AND (''='{3}' OR B.[TIME]<='{3}')
ORDER BY B.[TIME] DESC";
            sql = string.Format(sql, XM.Text, CPH.Text, stime, etime,TYPE);
            DataTable tab = SqlHelper.QueryDataTable(sql);
            dg1.Bind(tab);
        }

        private void btn_query_Click(object sender, EventArgs e)
        {
            InitializeData();
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            try
            {
                if(dg1.Rows.Count <= 0)
                {
                    DevExpress.xtraMessage.ShowExclamation("暂无数据导出.");
                    return;
                }
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "请选择文件保存路径";
                string foldPath = "";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    foldPath = dialog.SelectedPath;
                }
                if (foldPath == "")
                {
                    DevExpress.xtraMessage.ShowError("请选择导出文件保存路径");
                    return;
                }

                if(TYPE == 0)
                {
                    foldPath = foldPath + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "进货统计.xls";
                }
                else
                {
                    foldPath = foldPath + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "出货统计.xls";
                }
                //foldPath = foldPath+"\\" + GUIDHelper.GetGuidUper() + ".xls";
                string path = dg1.ExportFile(foldPath);
                DevExpress.xtraMessage.ShowTip("导出完成.\n" + path);
            }
            catch(Exception ex)
            {
                DevExpress.xtraMessage.ShowError(dg1.ErrorMessage);
            }
        }
    }
}
