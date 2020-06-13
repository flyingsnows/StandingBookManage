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
    public partial class XtraBiddingForm : XtraForm
    {
        public XtraBiddingForm()
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
            dg1.Columns.Add("Check", "选择", 75, xiaoid.forms.xtraDataType.CheckBoxEdit);
            dg1.Columns.Add("HYLCFJ", xiaoid.forms.xtraDataType.Label);
            dg1.Columns.Add("XMBH", "项目编号", 140, xiaoid.forms.xtraDataType.Label);
            dg1.Columns.Add("XMMC", "项目名称", 160, xiaoid.forms.xtraDataType.Label);
            dg1.Columns.Add("LXSJ", "立项时间", 85, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("ZBJG", "招标机构", 145, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("YJSJ", "议价时间", 145, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("ZBFS", "招标方式", 145, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("CHSJ", "参会商家", 160, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("ZBDW", "中标单位", 160, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("YSJE", "预算金额", 160, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("BJJE", "报价金额", 160, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("ZBJE", "中标金额", 140, xiaoid.forms.xtraDataType.Label);
           // dg1.Columns.Add("BMPJ", "部门评价", 140, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("SQBM", "申请部门", 85, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("LXR", "部门联系人", 145, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("LXDH", "联系电话", 145, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("CJR", "审核人", 160, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("CJSJ", "创建时间", 156, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("k1", "-", -1);
            dg1.EndInit();
        }



        private void simpleButton2_Click(object sender, EventArgs e)
        {
            StripForm from = new StripForm();
            from.ShowDialog();
            XtrastripForm_Load(null,null);
        }

        private void XtrastripForm_Load(object sender, EventArgs e)
        {
            string xmmc = textEdit1.Text;
            dg1.Rows.Clear();
            string sql = @"SELECT A.ID,XMBH,XMMC,LXSJ,ZBJG,YJSJ,ZBFS,CHSJ,ZBDW,YSJE,BJJE,ZBJE,BMPJ,SQBM,LXR,LXDH,HYLCFJ,B.YHM CJR,CJSJ FROM  T_A_DATA_ZBYJXX A LEFT JOIN T_A_DATA_USER B ON A.CJR = B.ID WHERE A.XMMC LIKE '%{0}%' " ;
            sql = string.Format(sql, xmmc);
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            dg1.Bind(tab);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            XtrastripForm_Load(null,null);
        }

     


        private void dg1_MouseUp(object sender, MouseEventArgs e)
        {
            if (dg1.Rows.Count > 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (MyApps.User.ROLEID != 3)  //学生不显示右键菜单
                    {
                        popupMenu1.ShowPopup(Control.MousePosition);
                    }
                }
            }
        }

        private void btn_item_edit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] IndexList = dg1.GetSelectedRows(true);
            int Index = IndexList[0];
            string Id = dg1.GetCellValue(Index, "ID").ToString();
            StripForm from = new StripForm(Id);
            from.ShowDialog();
            XtrastripForm_Load(null, null);
        }

        //导出议帐test
        private void btn_item_export_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
            string sql = "SELECT * FROM T_A_DATA_ZBYJXX WHERE ID =" + Id;
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            T_A_DATA_ZBYJXX model = Converts.Convert<T_A_DATA_ZBYJXX>(tab);
            if (model == null)
            {
                xiaoid.forms.xtraMessage.ShowError("数据获取失败.");
                return;
            }
            string path = Path.Combine(Application.StartupPath, @"Template\招标议价谈判登记模板.xls");
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
            cell.SetCellValue(model.XMBH);

            cell = sheet.GetRow(2).GetCell(1);
            cell.SetCellValue(model.XMMC);

            cell = sheet.GetRow(3).GetCell(1);
            cell.SetCellValue(model.LXSJ);



            cell = sheet.GetRow(3).GetCell(4);
            cell.SetCellValue(model.ZBJG);
            cell = sheet.GetRow(4).GetCell(1);
            cell.SetCellValue(model.YJSJ);
            cell = sheet.GetRow(4).GetCell(4);
            cell.SetCellValue(model.ZBFS);


            cell = sheet.GetRow(5).GetCell(1);
            cell.SetCellValue(model.CHSJ);
            cell = sheet.GetRow(6).GetCell(1);
            cell.SetCellValue(model.ZBDW);


            cell = sheet.GetRow(7).GetCell(1);
            cell.SetCellValue(model.YSJE);


            cell = sheet.GetRow(7).GetCell(4);
            cell.SetCellValue(model.BJJE);

            cell = sheet.GetRow(8).GetCell(1);
            cell.SetCellValue(model.ZBJE);
            cell = sheet.GetRow(8).GetCell(4);
            cell.SetCellValue(model.SQBM);


            cell = sheet.GetRow(9).GetCell(1);
            cell.SetCellValue(model.BMPJ);
            cell = sheet.GetRow(10).GetCell(1);
            cell.SetCellValue(model.LXR);
            cell = sheet.GetRow(10).GetCell(4);
            cell.SetCellValue(model.LXDH);

            apth = Path.Combine(apth, "招标议价谈判登记表.xls");
            using (FileStream fileStream = File.Open(apth, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                wk.Write(fileStream);
                fileStream.Close();
            }
            xiaoid.forms.xtraMessage.ShowTip("招标议价谈判登记表导出成功.");

        }

        private void btn_item_xz_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int[] IndexList = dg1.GetSelectedRows(true);
                int Index = IndexList[0];
                string pjcl = dg1.GetCellValue(Index, "HYLCFJ").ToString();
                if(pjcl=="" || pjcl == null)
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

        private void btn_item_openfile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //int[] IndexList = dg1.GetSelectedRows(true);
            //int Index = IndexList[0];
            //string pjcl = dg1.GetCellValue(Index, "HYLCFJ").ToString();
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
            PdfViewForm from = new PdfViewForm(0, "招标议价", Id);
            from.ShowDialog();
        }



        //删除
        private void btn_item_delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] IndexList = dg1.GetSelectedRows(true);
            int Index = IndexList[0];
            string Id = dg1.GetCellValue(Index, "ID").ToString();
            string sql = "DELETE FROM T_A_DATA_ZBYJXX WHERE ID="+ Id;
            int count = SQLiteHelper.Execute(sql);
            if(count <= 0)
            {
                xiaoid.forms.xtraMessage.ShowError("数据保存错误.");
            }
            else
            {
                XtrastripForm_Load(null,null);
            }
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




                string sql =$"SELECT * FROM T_A_DATA_ZBYJXX   WHERE ID IN ({ids})";
                DataTable tab = SQLiteHelper.QueryDataTable(sql);
                List<T_A_DATA_ZBYJXX> list = Converts.ConvertToList<T_A_DATA_ZBYJXX>(tab);
                if (list == null)
                {
                    xiaoid.forms.xtraMessage.ShowError("数据获取失败.");
                    return;
                }
                string path = Path.Combine(Application.StartupPath, @"Template\招标议价登记台账批量导出模板.xlsx");
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
                foreach (var item in list)
                {
                    T_A_DATA_ZBYJXX model = item;
                    row = sheet.GetRow(index);
                    ICell cell = row.GetCell(0);
                    cell.SetCellValue(xh);

                    cell = row.GetCell(1);
                    cell.SetCellValue(model.XMBH);
                    cell = row.GetCell(2);
                    cell.SetCellValue(model.XMMC);
                    cell = row.GetCell(3);
                    cell.SetCellValue(model.LXSJ);
                    cell = row.GetCell(4);
                    cell.SetCellValue(model.ZBJG);
                    cell = row.GetCell(5);
                    cell.SetCellValue(model.YJSJ);
                    cell = row.GetCell(6);
                    cell.SetCellValue(model.ZBFS);
                    cell = row.GetCell(7);
                    cell.SetCellValue(model.CHSJ);
                    cell = row.GetCell(8);
                    cell.SetCellValue(model.ZBDW);
                    cell = row.GetCell(9);
                    cell.SetCellValue(model.YSJE);
                    cell = row.GetCell(10);
                    cell.SetCellValue(model.BJJE);
                    cell = row.GetCell(11);
                    cell.SetCellValue(model.ZBJE);
                    cell = row.GetCell(12);
                    cell.SetCellValue(model.BMPJ);
                    cell = row.GetCell(13);
                    cell.SetCellValue(model.SQBM);
                    cell = row.GetCell(14);
                    cell.SetCellValue(model.LXR);
                    cell = row.GetCell(15);
                    cell.SetCellValue(model.LXDH);
                    xh += 1;
                    index += 1;
                }

                apth = Path.Combine(apth, "招标议价台账信息.xlsx");
                using (FileStream fileStream = File.Open(apth, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    wk.Write(fileStream);
                    fileStream.Close();
                }
                xiaoid.forms.xtraMessage.ShowTip("招标议价台账.");
            }
            else
            {
                xiaoid.forms.xtraMessage.ShowError("暂无数据导出");
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
                for (int i = 0; i <= sheet.LastRowNum; i++)
                {
                    T_A_DATA_ZBYJXX model = new T_A_DATA_ZBYJXX();
                    int index = i + 2;  //实际数据从第三行中出现
                    row = sheet.GetRow(index);
                    if (row == null) continue;
                    //获取每一列的数据,并转换为对应的数据类型.
                    model.XMBH = row.GetCell(1) == null ? "" : row.GetCell(1).ToString();
                    model.XMMC = row.GetCell(2) == null ? "" : row.GetCell(2).ToString();
                    model.LXSJ = row.GetCell(3) == null ? "" : row.GetCell(3).ToString();
                    model.ZBJG = row.GetCell(4) == null ? "" : row.GetCell(4).ToString();
                    model.YJSJ = row.GetCell(5) == null ? "" : row.GetCell(5).ToString();
                    model.ZBFS = row.GetCell(6) == null ? "" : row.GetCell(6).ToString();
                    model.CHSJ = row.GetCell(7) == null ? "" : row.GetCell(7).ToString();
                    model.ZBDW = row.GetCell(8) == null ? "" : row.GetCell(8).ToString();
                    model.YSJE = row.GetCell(9) == null ? "" : row.GetCell(9).ToString();
                    model.BJJE = row.GetCell(10) == null ? "" : row.GetCell(10).ToString();
                    model.ZBJE = row.GetCell(11) == null ? "" : row.GetCell(11).ToString();
                    model.BMPJ = row.GetCell(12) == null ? "" : row.GetCell(12).ToString();
                    model.SQBM = row.GetCell(13) == null ? "" : row.GetCell(13).ToString();
                    model.LXR = row.GetCell(14) == null ? "" : row.GetCell(14).ToString();
                    model.LXDH = row.GetCell(15)==null?"":row.GetCell(15).ToString();
                    model.HYLCFJ = "";
                    model.CJSJ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    model.CJR = MyApps.User.ID;
                    if (model.XMBH != "" && model.XMMC != "")
                    {
                        int count = model.AddToInt();
                    }
                }
                XtrastripForm_Load(null, null);
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
            StripForm from = new StripForm(Id);
            from.ShowDialog();
            XtrastripForm_Load(null, null);
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
                    string path = Path.Combine(Application.StartupPath, @"Template\招标议价登记台账导入模板.xlsx");
                    string topath = Path.Combine(dialog.SelectedPath, "招标议价登记台账导入模板.xlsx");
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

        
        //delete
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
                string sql = $"DELETE FROM T_A_DATA_ZBYJXX WHERE ID IN ({ids})";
                int count = SQLiteHelper.Execute(sql);
                if (count >= 0)
                {
                    xiaoid.forms.xtraMessage.ShowInfo("删除成功.");
                    XtrastripForm_Load(null, null);
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
