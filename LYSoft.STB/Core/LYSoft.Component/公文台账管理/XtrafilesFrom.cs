using DevExpress.XtraEditors;
using LYSoft.Center;
using LYSoft.DataBase.SQLite;
using LYSoft.Domain;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
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
    public partial class XtrafilesFrom : XtraForm
    {
        public XtrafilesFrom()
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
            dg1.SetOddRowBackColor(Color.Green);
            dg1.Columns.Add("ID", xiaoid.forms.xtraDataType.IntegerEdit);
            dg1.Columns.Add("XGZZFJ", xiaoid.forms.xtraDataType.Label);
            dg1.Columns.Add("Check", "选择", 75, xiaoid.forms.xtraDataType.CheckBoxEdit);
            dg1.Columns.Add("WJBH", "文件编号", 200, xiaoid.forms.xtraDataType.Label);
            dg1.Columns.Add("WJMC", "文件名称", 180, xiaoid.forms.xtraDataType.Label);
            dg1.Columns.Add("XFBM", "下发部门", 165, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("XFSJ", "下发时间", 125, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("XGYWBM", "相关业务部门", 145, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("GWZL", "公文地址", 145, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("UID", "所属用户", 145, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("CJSJ", "创建时间", 145, xiaoid.forms.xtraDataType.LabelCenter);
            //dg1.Columns.Add("k1", "-", -1);
            dg1.EndInit();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            XtraStudentForm_Load(null,null);
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
        private void XtraStudentForm_Load(object sender, EventArgs e)
        {
            dg1.Rows.Clear();
            string mc = textEdit1.Text;
            string fw = textEdit2.Text;
            string sql = @"SELECT A.ID,WJBH,WJMC,XFBM,XFSJ,XGYWBM,GWZL,UID,CJSJ FROM T_A_DATA_GWGL A LEFT JOIN T_A_DATA_USER B ON A.UID = B.ID WHERE A.WJBH LIKE '%{0}%' AND A.WJMC LIKE '%{1}%'";
            sql = string.Format(sql, mc,fw);
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            dg1.Bind(tab);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            XtraFileFrom from = new XtraFileFrom();
            from.ShowDialog();
            XtraStudentForm_Load(null,null);
        }

        private void btn_item_edit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] IndexList = dg1.GetSelectedRows(true);
            int Index = IndexList[0];
            string Id = dg1.GetCellValue(Index, "ID").ToString();
            XtraFileFrom from = new XtraFileFrom(Id);
            from.ShowDialog();
            XtraStudentForm_Load(null, null);
        }

        private void btn_exl_Click(object sender, EventArgs e)
        {
            if (dg1.Rows.Count > 0)
            {
                string apth = "";
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "请选择文件保存路径";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(dialog.SelectedPath))
                    {
                        xiaoid.forms.xtraMessage.ShowError("文件夹路径不能为空.");
                        return;
                    }
                    apth = dialog.SelectedPath;
                }

                string ids = "";
               int [] indexs = dg1.GetCheckedRows("Check");
                if(indexs.Length == 0)
                {
                    xiaoid.forms.xtraMessage.ShowError("您还没选中要导出的数据.");
                    return;
                }
                foreach (var item in indexs)
                {
                    ids += dg1.Rows[item]["ID"].ToString()+",";
                }
                ids = ids.TrimEnd(',');
                string sql = $"SELECT * FROM T_A_DATA_GWGL  WHERE ID IN ({ids})" ;
                DataTable tab = SQLiteHelper.QueryDataTable(sql);
                List<T_A_DATA_GWGL> list = Converts.ConvertToList<T_A_DATA_GWGL>(tab);
                if (list == null)
                {
                    xiaoid.forms.xtraMessage.ShowError("数据获取失败.");
                    return;
                }
                string path = Path.Combine(Application.StartupPath, @"Template\公文批量导出模板.xlsx");
                IWorkbook wk = null;
                string extension = System.IO.Path.GetExtension(path);//GetExtension获取Excel的扩展名
                FileStream fs = File.OpenRead(path);
                if (extension.Equals(".xls"))
                {
                    wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                }
                else
                {
                    wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中
                }
                fs.Close();
                IRow row;
                ISheet sheet = wk.GetSheetAt(0);//读取当前表数据   20    
                int index = 2;
                int xh = 1;  //序号
                foreach(var item in list)
                {
                    T_A_DATA_GWGL model = item;
                    row = sheet.GetRow(index);
                    ICell cell = row.GetCell(0);
                    cell.SetCellValue(xh);

                    cell = row.GetCell(1);
                    cell.SetCellValue(model.WJBH);
                    cell = row.GetCell(2);
                    cell.SetCellValue(model.WJMC);
                    cell = row.GetCell(3);
                    cell.SetCellValue(model.XFBM);
                    cell = row.GetCell(4);
                    cell.SetCellValue(model.XFSJ);
                    cell = row.GetCell(5);
                    cell.SetCellValue(model.XGYWBM);
                    xh += 1;
                    index += 1;
                }

                apth = Path.Combine(apth, "公文管理台账信息.xlsx");
                using (FileStream fileStream = File.Open(apth, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    wk.Write(fileStream);
                    fileStream.Close();
                }
                xiaoid.forms.xtraMessage.ShowTip("公文管理台账信息导出成功.");
            }
            else
            {
                xiaoid.forms.xtraMessage.ShowError("暂无数据导出");
            }
        }

        private void btn_item_delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] IndexList = dg1.GetSelectedRows(true);
            int Index = IndexList[0];
            string Id = dg1.GetCellValue(Index, "ID").ToString();
            string sql = "DELETE FROM T_A_DATA_GWGL WHERE ID=" + Id;
            int count = SQLiteHelper.Execute(sql);
            if (count <= 0)
            {
                xiaoid.forms.xtraMessage.ShowError("数据保存错误.");
            }
            else
            {
                XtraStudentForm_Load(null,null);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "请选择文件";
            dialog.Filter = "xls文件(*.xls;*.xlsx;)|*.xls;*.xlsx;";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;
                IWorkbook wk = null;
                string extension = System.IO.Path.GetExtension(file);//GetExtension获取Excel的扩展名
                FileStream fs = File.OpenRead(file);
                if (extension.Equals(".xls"))
                {
                    wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                }
                else
                {
                    wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中
                }
                fs.Close();
                ISheet sheet = wk.GetSheetAt(0);//读取当前表数据   20            GetIndexRow();//获取【指标、科目、数据】的行数列数
                IRow row;
                List<T_A_DATA_GWGL> list = new List<T_A_DATA_GWGL>();
                for (int i = 0; i <= sheet.LastRowNum; i++)
                {
                    T_A_DATA_GWGL model = new T_A_DATA_GWGL();
                    int index = i + 2;  //实际数据从第二行中出现
                    row = sheet.GetRow(index);
                    if (row == null) continue;
                    //获取每一列的数据,并转换为对应的数据类型.
                    model.WJBH = row.GetCell(1).ToString();
                    model.WJMC = row.GetCell(2).ToString();
                    model.XFBM = row.GetCell(3).ToString();
                    // row.GetCell(4).
                    if (row.GetCell(4).CellType == CellType.Numeric)
                    {
                        //NPOI中数字和日期都是NUMERIC类型的，这里对其进行判断是否是日期类型
                        if (DateUtil.IsValidExcelDate(row.GetCell(4).NumericCellValue))//日期类型
                        {
                            model.XFSJ = row.GetCell(4).DateCellValue.ToString("yyyy年MM月dd日");
                        }
                        else//其他数字类型
                        {
                            model.XFSJ = row.GetCell(4).NumericCellValue.ToString();
                        }
                    }
                    else if (row.GetCell(4).CellType == CellType.Blank)//空数据类型
                    {
                        model.XFSJ = "";
                    }

                    else //其他类型都按字符串类型来处理
                    {
                        model.XFSJ = row.GetCell(4).StringCellValue;
                    }
                    model.XGYWBM = row.GetCell(5).ToString();
                    model.CJSJ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    model.UID = MyApps.User.ID;
                    model.GWZL = "";
                    if(model.WJBH!="" && model.WJMC !="")
                    {
                        int count = model.AddToInt();
                    }
                }
                XtraStudentForm_Load(null, null);
            }
            else
            {
                xiaoid.forms.xtraMessage.ShowError("您还没选择导入文件.");
            }



        }

        private void dg1_DoubleClick(object sender, EventArgs e)
        {
            int[] IndexList = dg1.GetSelectedRows(true);
            int Index = IndexList[0];
            string Id = dg1.GetCellValue(Index, "ID").ToString();
            XtraFileFrom from = new XtraFileFrom(Id);
            from.ShowDialog();
            XtraStudentForm_Load(null, null);
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "请选择文件保存路径";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(dialog.SelectedPath))
                    {
                        xiaoid.forms.xtraMessage.ShowError("文件夹路径不能为空.");
                        return;
                    }
                    string path = Path.Combine(Application.StartupPath, @"Template\公文管理台账导入模板.xls");
                    string topath = Path.Combine(dialog.SelectedPath, "公文管理台账导入模板.xls");
                    IOHelper.CopyFile(path, topath, true);
                    xiaoid.forms.xtraMessage.ShowTip("文件下载成功.");

                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                xiaoid.forms.xtraMessage.ShowError("文件下载异常.");
                return;
            }
        }


        private void btn_item_open_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] IndexList = dg1.GetSelectedRows(true);
            int Index = IndexList[0];
            string pjcl = dg1.GetCellValue(Index, "GWZL").ToString();
            if (pjcl != null && pjcl != "" && pjcl.Contains(".pdf"))
            {
                string path = Path.Combine(Application.StartupPath, pjcl);
                XtraPdfViewer from = new XtraPdfViewer(path);
                from.ShowDialog();
            }
            else if (pjcl.Contains(".doc") || pjcl.Contains(".docx"))
            {
                string apth = Path.Combine(Application.StartupPath, pjcl);
                string path = Path.Combine(Application.StartupPath, apth);
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.FileName = path;
                p.Start();
            }
            else if (pjcl.Contains(".xls") || pjcl.Contains(".xlsx"))
            {
                string apth = Path.Combine(Application.StartupPath, pjcl);
                string path = Path.Combine(Application.StartupPath, apth);
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.FileName = path;
                p.Start();
            }
            else
            {
                xiaoid.forms.xtraMessage.ShowError("未找到上传的文件或文件格式错误.");
            }
        }

        private void btn_item_down_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int[] IndexList = dg1.GetSelectedRows(true);
                int Index = IndexList[0];
                string pjcl = dg1.GetCellValue(Index, "GWZL").ToString();
                if (pjcl == "" || pjcl == null)
                {
                    xiaoid.forms.xtraMessage.ShowError("未找到上传的文件或文件格式错误.");
                    return;
                }
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "请选择文件保存路径";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(dialog.SelectedPath))
                    {
                        xiaoid.forms.xtraMessage.ShowError("文件夹路径不能为空.");
                        return;
                    }


                    if (pjcl != null && pjcl != "")
                    {
                        string path = Path.Combine(Application.StartupPath, pjcl);
                        string topath = Path.Combine(dialog.SelectedPath, pjcl.Split('\\').LastOrDefault());
                        IOHelper.CopyFile(path, topath, true);
                        xiaoid.forms.xtraMessage.ShowTip("文件下载成功.");
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                xiaoid.forms.xtraMessage.ShowError("文件下载异常.");
                return;
            }
        }
        //批量删除
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (dg1.Rows.Count > 0)
            {
                string ids = "";
                int[] indexs = dg1.GetCheckedRows("Check");
                if (indexs.Length == 0)
                {
                    xiaoid.forms.xtraMessage.ShowError("您还没选中要导出的数据.");
                    return;
                }
                foreach (var item in indexs)
                {
                    ids += dg1.Rows[item]["ID"].ToString() + ",";
                }
                ids = ids.TrimEnd(',');
                string sql = $"DELETE FROM T_A_DATA_GWGL  WHERE ID IN ({ids})";
                int count = SQLiteHelper.Execute(sql);
                if (count >= 0)
                {
                    xiaoid.forms.xtraMessage.ShowInfo("删除成功.");
                    XtraStudentForm_Load(null, null);
                }
                else
                {
                    xiaoid.forms.xtraMessage.ShowError("删除失败.");
                    return;
                }
            }
            else
            {
                xiaoid.forms.xtraMessage.ShowError("暂无数据导出");
            }
        }
    }
}
