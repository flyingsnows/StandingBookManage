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
    public partial class XtrasupplierForm : XtraForm
    {
        public XtrasupplierForm()
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
            dg1.Columns.Add("GSMC", "公司名称", 200, xiaoid.forms.xtraDataType.Label);
            dg1.Columns.Add("XYDM", "统一社会信用代码", 180, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("ADDS", "企业所在地", 165, xiaoid.forms.xtraDataType.Label);
            dg1.Columns.Add("FRDB", "法定代表人", 125, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("ZCZB", "注册资本", 125, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("CLSJ", "成立时间", 125, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("YXRQ", "营业执照有效期", 125, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("SFHZ", "是否合作过", 85, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("JYFW", "经营范围", 200, xiaoid.forms.xtraDataType.Label);
            dg1.Columns.Add("ZYFWLX", "主要服务类型", 200, xiaoid.forms.xtraDataType.Label);
            dg1.Columns.Add("LXR", "企业联系人", 125, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("LXDH", "联系电话", 125, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("XGZZ", "相关资质材料", 145, xiaoid.forms.xtraDataType.Label);
            dg1.Columns.Add("HZXM", "已与公司合作项目", 160, xiaoid.forms.xtraDataType.Label);
            dg1.Columns.Add("CJR", "创建人", 160, xiaoid.forms.xtraDataType.LabelCenter);
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
            string sql = @"SELECT A.ID,GSMC,XYDM,ADDS,FRDB,ZCZB,CLSJ,XGZZFJ,YXRQ,SFHZ,JYFW,ZYFWLX,LXR,LXDH,XGZZ,HZXM,B.YHM CJR,A.CJSJ  FROM T_A_DATA_GYSXX A LEFT JOIN T_A_DATA_USER B ON A.CJR = B.ID WHERE A.ZYFWLX LIKE '%{0}%' AND A.JYFW LIKE '%{1}%'";
            sql = string.Format(sql, mc,fw);
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            dg1.Bind(tab);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            XtraSupFrom from = new XtraSupFrom();
            from.ShowDialog();
            XtraStudentForm_Load(null,null);
        }

        private void btn_item_edit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] IndexList = dg1.GetSelectedRows(true);
            int Index = IndexList[0];
            string Id = dg1.GetCellValue(Index, "ID").ToString();
            XtraSupFrom from = new XtraSupFrom(Id);
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
                string sql = $"SELECT * FROM T_A_DATA_GYSXX  WHERE ID IN ({ids})" ;
                DataTable tab = SQLiteHelper.QueryDataTable(sql);
                List<T_A_DATA_GYSXX> list = Converts.ConvertToList<T_A_DATA_GYSXX>(tab);
                if (list == null)
                {
                    xiaoid.forms.xtraMessage.ShowError("数据获取失败.");
                    return;
                }
                string path = Path.Combine(Application.StartupPath, @"Template\供商库批量导出模板.xls");
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
                int index = 3;
                int xh = 1;  //序号
                foreach(var item in list)
                {
                    T_A_DATA_GYSXX model = item;
                    row = sheet.GetRow(index);
                    ICell cell = row.GetCell(0);
                    cell.SetCellValue(xh);

                    cell = row.GetCell(1);
                    cell.SetCellValue(model.GSMC);
                    cell = row.GetCell(2);
                    cell.SetCellValue(model.XYDM);
                    cell = row.GetCell(3);
                    cell.SetCellValue("");
                    cell = row.GetCell(4);
                    cell.SetCellValue(model.ADDS);
                    cell = row.GetCell(5);
                    cell.SetCellValue(model.FRDB);
                    cell = row.GetCell(6);
                    cell.SetCellValue(model.ZCZB);
                    cell = row.GetCell(7);
                    cell.SetCellValue(model.CLSJ);
                    cell = row.GetCell(8);
                    cell.SetCellValue(model.YXRQ);
                    cell = row.GetCell(9);
                    cell.SetCellValue(model.JYFW);
                    cell = row.GetCell(10);
                    cell.SetCellValue(model.ZYFWLX);
                    cell = row.GetCell(11);
                    cell.SetCellValue(model.HZXM);
                    cell = row.GetCell(12);
                    cell.SetCellValue(model.XGZZ);
                    cell = row.GetCell(13);
                    cell.SetCellValue(model.LXR);
                    cell = row.GetCell(14);
                    cell.SetCellValue(model.LXDH);
                    xh += 1;
                    index += 1;
                }

                apth = Path.Combine(apth, "供商库信息.xls");
                using (FileStream fileStream = File.Open(apth, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    wk.Write(fileStream);
                    fileStream.Close();
                }
                xiaoid.forms.xtraMessage.ShowTip("供商库信息导出成功.");
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
            string sql = "DELETE FROM T_A_DATA_GYSXX WHERE ID=" + Id;
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
            dialog.Title = "请选择图像文件";
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
                List<T_A_DATA_GYSXX> list = new List<T_A_DATA_GYSXX>();
                for (int i = 0; i <= sheet.LastRowNum; i++)
                {
                    T_A_DATA_GYSXX model = new T_A_DATA_GYSXX();
                    int index = i + 3;  //实际数据从第三行中出现
                    row = sheet.GetRow(index);
                    if (row == null) continue;
                    //获取每一列的数据,并转换为对应的数据类型.
                    model.GSMC = row.GetCell(1).ToString();
                    model.XYDM = row.GetCell(2).ToString();
                    model.ADDS = row.GetCell(4).ToString();
                    model.FRDB = row.GetCell(5).ToString();
                    model.ZCZB = row.GetCell(6).ToString();
                    if (row.GetCell(7).CellType == CellType.Numeric)
                    {
                        //NPOI中数字和日期都是NUMERIC类型的，这里对其进行判断是否是日期类型
                        if (DateUtil.IsValidExcelDate(row.GetCell(7).NumericCellValue))//日期类型
                        {
                            model.CLSJ = row.GetCell(7).DateCellValue.ToString("yyyy年MM月dd日");
                        }
                        else//其他数字类型
                        {
                            model.CLSJ = row.GetCell(7).NumericCellValue.ToString();
                        }
                    }
                    else if (row.GetCell(7).CellType == CellType.Blank)//空数据类型
                    {
                        model.CLSJ = "";
                    }
                    
                    else //其他类型都按字符串类型来处理
                    {
                        model.CLSJ = row.GetCell(7).StringCellValue;
                    }
                    
                    
                    model.JYFW = row.GetCell(9).ToString();
                    model.LXR = row.GetCell(13).ToString();
                    model.LXDH = row.GetCell(14).ToString();
                    model.XGZZ = row.GetCell(12).ToString();
                    model.CJSJ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    model.CJR = MyApps.User.ID;
                    model.SFHZ = "";
                    model.ZYFWLX = row.GetCell(10).ToString();
                    model.HZXM = row.GetCell(11).ToString();
                    model.YXRQ = row.GetCell(8).ToString(); 
                    model.XGZZFJ = "供应商";
                    if(model.GSMC!="" && model.ZCZB !=""  && model.CLSJ != "")
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
            XtraSupFrom from = new XtraSupFrom(Id);
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
                    string path = Path.Combine(Application.StartupPath, @"Template\供商库导入模板.xls");
                    string topath = Path.Combine(dialog.SelectedPath, "供商库导入模板.xls");
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

        private void btn_item_gys_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
            
            int[] IndexList = dg1.GetSelectedRows(true);
            int Index = IndexList[0];
            string Id = dg1.GetCellValue(Index, "ID").ToString();
            string sql = "SELECT * FROM T_A_DATA_GYSXX WHERE ID =" + Id;
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            T_A_DATA_GYSXX model = Converts.Convert<T_A_DATA_GYSXX>(tab);
            if (model == null)
            {
                xiaoid.forms.xtraMessage.ShowError("数据获取失败.");
                return;
            }
            string path = Path.Combine(Application.StartupPath, @"Template\供应商简历模板.xls");
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
            ISheet sheet = wk.GetSheetAt(0);//读取当前表数据   20    
           
            ICell cell = sheet.GetRow(1).GetCell(1);
            cell.SetCellValue(model.GSMC);

            cell = sheet.GetRow(2).GetCell(1);
            cell.SetCellValue(model.XYDM);

            cell = sheet.GetRow(3).GetCell(1);
            cell.SetCellValue(model.ADDS);
           
          

            cell = sheet.GetRow(4).GetCell(1);
            cell.SetCellValue(model.FRDB);
            cell = sheet.GetRow(4).GetCell(3);
            cell.SetCellValue(model.ZCZB);
            cell = sheet.GetRow(4).GetCell(5);
            cell.SetCellValue(model.CLSJ);


            cell = sheet.GetRow(5).GetCell(1);
            cell.SetCellValue(model.YXRQ);
            cell = sheet.GetRow(5).GetCell(5);
            cell.SetCellValue(model.SFHZ);
          

            cell = sheet.GetRow(6).GetCell(1);
            cell.SetCellValue(model.JYFW);
         

            cell = sheet.GetRow(7).GetCell(1);
            cell.SetCellValue(model.ZYFWLX);

            cell = sheet.GetRow(8).GetCell(1);
            cell.SetCellValue(model.LXR);
            cell = sheet.GetRow(8).GetCell(5);
            cell.SetCellValue(model.LXDH);


            cell = sheet.GetRow(9).GetCell(1);
            cell.SetCellValue(model.XGZZ);
            cell = sheet.GetRow(10).GetCell(1);
            cell.SetCellValue(model.HZXM);
            
            apth = Path.Combine(apth, "供应商简历.xls");
            using (FileStream fileStream = File.Open(apth, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                wk.Write(fileStream);
                fileStream.Close();
            }
            xiaoid.forms.xtraMessage.ShowTip("供应商简历导出成功.");

        }

        private void btn_item_open_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //int[] IndexList = dg1.GetSelectedRows(true);
            //int Index = IndexList[0];
            //string pjcl = dg1.GetCellValue(Index, "XGZZFJ").ToString();
            //if (pjcl != null && pjcl != "" && pjcl.Contains(".pdf"))
            //{
            //    string path = Path.Combine(Application.StartupPath, pjcl);
            //    XtraPdfViewer from = new XtraPdfViewer(path);
            //    from.ShowDialog();
            //}
            //else
            //{
            //    xiaoid.forms.xtraMessage.ShowError("未找到上传的pdf文件或文件不是pdf格式.");
            //}
            int[] IndexList = dg1.GetSelectedRows(true);
            int Index = IndexList[0];
            string Id = dg1.GetCellValue(Index, "ID").ToString();
            PdfViewForm from = new PdfViewForm(0, "供应商", Id);
            from.ShowDialog();
        }

        private void btn_item_down_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int[] IndexList = dg1.GetSelectedRows(true);
                int Index = IndexList[0];
                string pjcl = dg1.GetCellValue(Index, "XGZZFJ").ToString();
                if (pjcl == "" || pjcl == null)
                {
                    xiaoid.forms.xtraMessage.ShowError("未找到上传的pdf文件或文件不是pdf格式.");
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

        private void btn_delete_Click(object sender, EventArgs e)
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
                string sql = $"DELETE FROM T_A_DATA_GYSXX WHERE ID IN ({ids})";
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
