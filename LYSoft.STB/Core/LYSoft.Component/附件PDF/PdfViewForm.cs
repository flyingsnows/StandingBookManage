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
    public partial class PdfViewForm : XtraForm
    {
        private long hostId = 0;
        private string fj_class = "无";
        private string userId = "0";
        public PdfViewForm(long ower_host = 0, string fj_class ="无",string ower_user="0")
        {

            InitializeComponent();
            this.hostId = ower_host;
            this.fj_class = fj_class;
            this.userId = ower_user;
            
        }

        public void InithostData() {
            string sql = "SELECT NAME,LOCATION,TYPE FROM T_A_DATA_FILEMG WHERE OWER_HOST=" + "'" + hostId + "'" + " AND fj_CLASS =" + "'" + fj_class + "'" + " AND OWER_USER =" + "'" + userId + "'" + " AND TYPE ='pdf'";
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
            // ItemControl
            /*string sql = @"SELECT ID,HYLCFJ PATH,'T_A_DATA_ZBYJXX' TAB FROM T_A_DATA_ZBYJXX WHERE HYLCFJ LIKE '%.pdf%'
UNION ALL
SELECT ID,FJCL,'T_A_DATA_RYXX' TAB FROM T_A_DATA_RYXX WHERE FJCL LIKE '%.pdf%'";
            //PdfModel
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            List<PdfModel> list = Converts.ConvertToList<PdfModel>(tab);
            ItemControl temp = new ItemControl();

            int width = panelControl1.Width;
            double val =  width.ObjectToDouble() / temp.Width.ObjectToDouble();
            double count = Math.Round(val);
            int index = 0;
            foreach(var item in list)
            {
                ItemControl cons = new ItemControl();
                cons.FileName.Text = item.PATH.Split('\\').LastOrDefault();
                cons.Tag = item;
                int x = 0;
                int y = 10;
                if(index > 0)
                {
                    x = index * cons.Width + 10;
                }else
                {
                    x = index * cons.Width;
                }

                if(index > 7)
                {
                    y = cons.Height + 1;
                }
                cons.Location = new Point() {X= x,Y=y };
                this.panelControl1.Controls.Add(cons);

                index += 1;
                
        }
        */



        }

        private void btn_file_daoru_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "请选择pdf文件";
            dialog.Filter = "pdf文件(*.pdf;)|*.pdf;";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                T_A_DATA_FILEMG model = new T_A_DATA_FILEMG();
                model.location = dialog.FileName;
                model.name = model.location.Split('\\').Last();
                model.fj_class = "无";
                model.ower_host = hostId.ToString();
                model.ower_user = userId;
                model.fj_class = fj_class;
                model.type = "pdf";
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
                if (location.Contains(".pdf"))
                {
                    string apth = Path.Combine(Application.StartupPath, location);
                    string path = Path.Combine(Application.StartupPath, apth);
                    XtraPdfViewer from = new XtraPdfViewer(path);
                    from.ShowDialog();
                }
                else
                {
                    xiaoid.forms.xtraMessage.ShowError("未找到上传的pdf文件或文件不是pdf格式.");
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
            string sql = "DELETE FROM T_A_DATA_FILEMG WHERE OWER_HOST=" + "'" + hostId + "'" + " AND fj_CLASS =" + "'" + fj_class + "'" + " AND OWER_USER=" + "'" + userId + "'" + " AND LOCATION=" + "'" + location + "'" + " AND TYPE ='pdf'";
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
                        if (location.Contains(".pdf"))
                        {
                            string apth = Path.Combine(Application.StartupPath, location);
                            string path = Path.Combine(Application.StartupPath, apth);
                            XtraPdfViewer from = new XtraPdfViewer(path);
                            from.ShowDialog();
                        }
                        else
                        {
                            xiaoid.forms.xtraMessage.ShowError("未找到上传的pdf文件或文件不是pdf格式.");
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
            string sql = "SELECT NAME,LOCATION,TYPE FROM T_A_DATA_FILEMG WHERE NAME LIKE " + "'%" + query + "%'" + " AND OWER_HOST = " + "'" + hostId + "'" + " AND fj_CLASS = " + "'" + fj_class + "'" + " AND OWER_USER = " + "'" + userId + "'" + " AND TYPE ='pdf'";
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            file_grid_control.DataSource = tab;
        }
    }
}
