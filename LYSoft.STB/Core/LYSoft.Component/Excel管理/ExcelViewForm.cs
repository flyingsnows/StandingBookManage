using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
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
    public partial class ExcelViewForm : XtraForm
    {
        private long hostId = 0;
        private string fj_class = "无";
        private string userId = "0";
        public ExcelViewForm(long ower_host = 0, string fj_class ="无",string ower_user="0")
        {

            InitializeComponent();
            this.hostId = ower_host;
            this.fj_class = fj_class;
            this.userId = ower_user;
            
        }

        public void InithostData() {
            string sql = "SELECT NAME,LOCATION,TYPE FROM T_A_DATA_FILEMG WHERE OWER_HOST="+"'" + hostId + "'" +" AND fj_CLASS =" + "'" + fj_class +"'" +" AND OWER_USER =" + "'" +userId+ "'" + " AND TYPE like 'xls%'";
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            file_grid_control.DataSource= tab;
            

        }

        public void InituserData()
        {
        }

        private void PdfViewForm_Load(object sender, EventArgs e)
        {
            //gridView1.PopulateColumns();
            if (hostId != 0)
            {
                InithostData();
            }
            if (userId != "0")
            {
                InithostData();
            }
          
        }

        private void btn_file_daoru_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "请选择Excel文件";
            dialog.Filter = "word文件(*.xls;*.xlsx;)|*.xls;*.xlsx";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                T_A_DATA_FILEMG model = new T_A_DATA_FILEMG();
                model.location = dialog.FileName;
                model.name = model.location.Split('\\').Last();
                model.fj_class = "无";
                model.ower_host = hostId.ToString();
                model.ower_user = userId;
                model.fj_class = fj_class;
                model.type = model.location.Split('.').Last();
                int count = model.AddToInt();
                if (count <= 0)
                {
                    xiaoid.forms.xtraMessage.ShowError("文件导入错误.");
                }
                else {
                    InithostData();
                }               
            }
        }

        private void btn_file_open_Click(object sender, EventArgs e)
        {
            string location = (string)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "location");
            try
            {
                T_A_DATA_ZBYJXX model = this.Tag as T_A_DATA_ZBYJXX;
                if (location == null || location == "")
                {
                    xiaoid.forms.xtraMessage.ShowError("数据获取失败.");
                    return;
                }
                if (location.Contains(".xls") || location.Contains(".xlsx"))
                {
                    string apth = Path.Combine(Application.StartupPath, location);
                    string path = Path.Combine(Application.StartupPath, apth);
                    //RichEditControl form = new RichEditControl();
                    //form.LoadDocument(path);
                    //form.ShowDialog();
                    //from.ShowDialog();
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.UseShellExecute = true;
                    p.StartInfo.FileName = path;
                    p.Start();
                }
                else
                {
                    xiaoid.forms.xtraMessage.ShowError("未找到上传的Excel文件或文件不是Excel格式.");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                xiaoid.forms.xtraMessage.ShowError("文件打开异常.");
                return;
            }
        }

        private void btn_file_delete_Click(object sender, EventArgs e)
        {
            string location = (string)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "location");
            string sql = "DELETE FROM T_A_DATA_FILEMG WHERE OWER_HOST=" +"'"+ hostId + "'" + " AND fj_CLASS =" + "'" + fj_class + "'" + " AND OWER_USER=" +"'" + userId + "'" + " AND LOCATION=" + "'" + location+ "'" + " AND TYPE LIKE 'xls%'";
            int count = SQLiteHelper.Execute(sql);
            if (count >= 0)
            {
                InithostData();
            }
            else {
                xiaoid.forms.xtraMessage.ShowError("文件删除失败.");
            }
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = gridView1.CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                //判断光标是否在行范围内 
                if (hInfo.InRow)
                {
                    //取得选定行信息 
                    string location = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "location").ToString();
                    //string nodeName = gridView1.GetRowCellValue(gridView1.GetSelectedRows()[0], "nodeName").ToString();
                    try
                    {
                        T_A_DATA_ZBYJXX model = this.Tag as T_A_DATA_ZBYJXX;
                        if (location == null || location == "")
                        {
                            xiaoid.forms.xtraMessage.ShowError("数据获取失败.");
                            return;
                        }
                        if (location.Contains(".xls") || location.Contains(".xlsx"))
                        {
                            string apth = Path.Combine(Application.StartupPath, location);
                            string path = Path.Combine(Application.StartupPath, apth);
                            //RichEditControl form = new RichEditControl();
                            //form.LoadDocument(path);
                            //form.ShowDialog();
                            //from.ShowDialog();
                            System.Diagnostics.Process p = new System.Diagnostics.Process();
                            p.StartInfo.UseShellExecute = true;
                            p.StartInfo.FileName = path;
                            p.Start();
                        }
                        else
                        {
                            xiaoid.forms.xtraMessage.ShowError("未找到上传的Excel文件或文件不是Excel格式.");
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteError(ex.ToString());
                        xiaoid.forms.xtraMessage.ShowError("文件打开异常.");
                        return;
                    }
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string query = text_search.Text;
            string sql = "SELECT NAME,LOCATION,TYPE FROM T_A_DATA_FILEMG WHERE NAME LIKE " + "'%"+query+"%'" + " AND OWER_HOST = "+"'" + hostId + "'" +" AND fj_CLASS = " + "'" + fj_class +"'" +" AND OWER_USER = " + "'" +userId+"'" + " AND TYPE like 'xls%'";
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            file_grid_control.DataSource = tab;
        }
    }
}
