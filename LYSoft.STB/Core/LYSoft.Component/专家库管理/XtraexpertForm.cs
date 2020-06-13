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
    public partial class XtraexpertForm : XtraForm
    {
        public XtraexpertForm()
        {
            InitializeComponent();
            InitializeGridView();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            WorkListForm_Load(null,null);
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
            dg1.Columns.Add("IMG", xiaoid.forms.xtraDataType.Label);
            dg1.Columns.Add("FJCL", xiaoid.forms.xtraDataType.Label);
            dg1.Columns.Add("XM", "姓名", 140, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("GZDW", "工作单位", 200, xiaoid.forms.xtraDataType.Label);
            dg1.Columns.Add("CSRQ", "出生日期", 125, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("GWZW", "岗位职务", 125, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("CJGZSJ", "参加工作时间", 160, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("ZZMM", "政治面貌", 125, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("XL", "学历", 150, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("SXZY", "所学专业", 115, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("JTZY", "精通专业", 160, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("ZYJSZW", "专业技术职务", 150, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("QDSJ", "取得时间", 150, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("ZYZG", "执业资格", 115, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("SFZJHM", "身份证件号码", 160, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("BGDH", "办公电话", 150, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("LXDH", "手机", 150, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("JG", "籍贯", 115, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("ADDS", "现住址", 115, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("ZYCG", "主要成果", 160, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("CJR", "创建人", 115, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("CJSJ", "创建时间", 156, xiaoid.forms.xtraDataType.LabelCenter);
            dg1.Columns.Add("k1", "-", -1);
            dg1.EndInit();
        }



        private void WorkListForm_Load(object sender, EventArgs e)
        {
            dg1.Rows.Clear();
            string xm = textEdit1.Text;
            string xl = textEdit2.Text;
            string sql = @"SELECT A.ID,XM,GZDW,CSRQ,GWZW,CJGZSJ,ZZMM,XL,SXZY,JTZY,ZYJSZW,QDSJ,ZYZG,SFZJHM,BGDH,LXDH,JG,ADDS,ZYCG,FJCL,IMG,CJSJ,B.YHM CJR FROM
            T_A_DATA_RYXX A LEFT JOIN T_A_DATA_USER B ON A.CJR = B.ID WHERE A.XM LIKE '%{0}%' AND JTZY LIKE '%{1}%' ";
            sql = string.Format(sql, xm,xl);
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            dg1.Bind(tab);

        }

        private void btn_item_xq_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] IndexList = dg1.GetSelectedRows(true);
            int Index = IndexList[0];
            string Id = dg1.GetCellValue(Index, "ID").ToString();
            XtraExpertAddOrEdit from = new XtraExpertAddOrEdit(Id);
            from.ShowDialog();
            WorkListForm_Load(null, null);
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

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            XtraExpertAddOrEdit from = new XtraExpertAddOrEdit();
            from.ShowDialog();
            WorkListForm_Load(null,null);
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
                


                string sql = $"SELECT * FROM T_A_DATA_RYXX WHERE ID IN ({ids})";
                DataTable tab = SQLiteHelper.QueryDataTable(sql);
                List<T_A_DATA_RYXX> list = Converts.ConvertToList<T_A_DATA_RYXX>(tab);
                if (list == null)
                {
                    xiaoid.forms.xtraMessage.ShowError("数据获取失败.");
                    return;
                }
                string path = Path.Combine(Application.StartupPath, @"Template\评标专家库批量导出模板.xls");
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
                foreach (var item in list)
                {
                    T_A_DATA_RYXX model = item;
                    row = sheet.GetRow(index);
                    ICell cell = row.GetCell(0);
                    cell.SetCellValue(xh);

                    cell = row.GetCell(1);
                    cell.SetCellValue(model.XM);
                    cell = row.GetCell(2);
                    cell.SetCellValue(model.SFZJHM);
                    cell = row.GetCell(3);
                    cell.SetCellValue(model.XL);
                    cell = row.GetCell(4);
                    cell.SetCellValue("");
                    cell = row.GetCell(5);
                    cell.SetCellValue(model.ZYJSZW);
                    cell = row.GetCell(6);
                    cell.SetCellValue(model.ZYZG);
                    cell = row.GetCell(7);
                    cell.SetCellValue(model.GZDW);
                    cell = row.GetCell(8);
                    cell.SetCellValue(model.GWZW);
                    cell = row.GetCell(9);
                    cell.SetCellValue(model.JTZY);
                    cell = row.GetCell(10);
                    cell.SetCellValue(model.LXDH);
                    cell = row.GetCell(11);
                    cell.SetCellValue(model.BGDH);
                    xh += 1;
                    index += 1;
                }

                apth = Path.Combine(apth, "评标专家库信息.xls");
                using (FileStream fileStream = File.Open(apth, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    wk.Write(fileStream);
                    fileStream.Close();
                }
                xiaoid.forms.xtraMessage.ShowTip("成功导出评标专家库信息.");
            }
            else
            {
                xiaoid.forms.xtraMessage.ShowError("暂无数据导出");
            }
        }

        //下载附件
        private void btn_item_upfj_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int[] IndexList = dg1.GetSelectedRows(true);
                int Index = IndexList[0];
                string pjcl = dg1.GetCellValue(Index, "FJCL").ToString();
                if (pjcl == "" || pjcl ==null)
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


        //下载照片
        private void btn_item_img_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

                int[] IndexList = dg1.GetSelectedRows(true);
                int Index = IndexList[0];
                string img = dg1.GetCellValue(Index, "IMG").ToString();
                if (img == "")
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

                    if (img != null && img != "")
                    {
                        string path = Path.Combine(Application.StartupPath, img);
                        string topath = Path.Combine(dialog.SelectedPath, img.Split('\\').LastOrDefault());
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

        private void btn_item_openpdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //int[] IndexList = dg1.GetSelectedRows(true);
            //int Index = IndexList[0];
            //string pjcl = dg1.GetCellValue(Index, "FJCL").ToString();
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
            PdfViewForm from = new PdfViewForm(0, "专家", Id);
            from.ShowDialog();
        }

        private void btn_item_delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] IndexList = dg1.GetSelectedRows(true);
            int Index = IndexList[0];
            string Id = dg1.GetCellValue(Index, "ID").ToString();
            string sql = "DELETE FROM T_A_DATA_RYXX WHERE ID=" + Id;
            int count = SQLiteHelper.Execute(sql);
            if (count <= 0)
            {
                xiaoid.forms.xtraMessage.ShowError("数据保存错误.");
            }
            else
            {
                WorkListForm_Load(null,null);
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
                ISheet sheet = wk.GetSheetAt(0);//读取当前表数据   20      
                IRow row;
                for (int i = 0; i <= sheet.LastRowNum; i++)
                {
                    T_A_DATA_RYXX model = new T_A_DATA_RYXX();
                    int index = i + 3;  //实际数据从第三行中出现
                    row = sheet.GetRow(index);
                    if (row == null) continue;
                    //获取每一列的数据,并转换为对应的数据类型.
                    model.XM = row.GetCell(1).ToString();
                    model.SFZJHM = row.GetCell(2).ToString();
                    model.XL = row.GetCell(3).ToString();
                    model.ZYJSZW = row.GetCell(5).ToString();
                    model.ZYZG = row.GetCell(6).ToString();
                    model.GWZW = row.GetCell(8).ToString();
                    model.JTZY = row.GetCell(9).ToString();
                    model.LXDH = row.GetCell(10).ToString();
                    model.BGDH = row.GetCell(11).ToString();
                    model.FJCL = "";
                    model.CJGZSJ = "";
                    model.CSRQ = "";
                    model.IMG = "";
                    model.GZDW = row.GetCell(7).ToString();
                    model.JG = "";
                    model.QDSJ = "";
                    model.SXZY = "";
                    model.ZYCG = "";
                    model.ZZMM = "";
                    model.ADDS = "";
                    model.CJSJ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    model.CJR = MyApps.User.ID;
                    if (model.XM != "")
                    {
                        int count = model.AddToInt();
                    }
                }
                WorkListForm_Load(null, null);
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
            XtraExpertAddOrEdit from = new XtraExpertAddOrEdit(Id);
            from.ShowDialog();
            WorkListForm_Load(null, null);
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
                    string path = Path.Combine(Application.StartupPath, @"Template\评标专家库导入模板.xls");
                    string topath = Path.Combine(dialog.SelectedPath, "评标专家库导入模板.xls");
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


        //个人简历
        private void btn_item_dao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
            string sql = "SELECT * FROM T_A_DATA_RYXX WHERE ID =" + Id;
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            T_A_DATA_RYXX model = Converts.Convert<T_A_DATA_RYXX>(tab);
            if (model == null)
            {
                xiaoid.forms.xtraMessage.ShowError("数据获取失败.");
                return;
            }
            string path = Path.Combine(Application.StartupPath, @"Template\评标专家简历导出模板.xls");
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
            row = sheet.GetRow(2);
            ICell cell = sheet.GetRow(2).GetCell(2);
            cell.SetCellValue(model.XM);
            cell = sheet.GetRow(2).GetCell(4);
            cell.SetCellValue(model.GZDW);

            cell = sheet.GetRow(3).GetCell(2);
            cell.SetCellValue(model.CSRQ);
            cell = sheet.GetRow(3).GetCell(4);
            cell.SetCellValue(model.GWZW);

            cell = sheet.GetRow(4).GetCell(2);
            cell.SetCellValue(model.CJGZSJ);
            cell = sheet.GetRow(4).GetCell(4);
            cell.SetCellValue(model.ZZMM);


            cell = sheet.GetRow(5).GetCell(2);
            cell.SetCellValue(model.XL);
            cell = sheet.GetRow(5).GetCell(4);
            cell.SetCellValue(model.SXZY);
            cell = sheet.GetRow(5).GetCell(8);
            cell.SetCellValue(model.JTZY);

            cell = sheet.GetRow(6).GetCell(2);
            cell.SetCellValue(model.ZYJSZW);
            cell = sheet.GetRow(6).GetCell(4);
            cell.SetCellValue(model.QDSJ);
            cell = sheet.GetRow(6).GetCell(8);
            cell.SetCellValue(model.ZYZG);

            cell = sheet.GetRow(7).GetCell(2);
            cell.SetCellValue(model.SFZJHM);

            cell = sheet.GetRow(8).GetCell(2);
            cell.SetCellValue(model.BGDH);
            cell = sheet.GetRow(8).GetCell(5);
            cell.SetCellValue(model.LXDH);


            cell = sheet.GetRow(9).GetCell(2);
            cell.SetCellValue(model.JG);
            cell = sheet.GetRow(10).GetCell(2);
            cell.SetCellValue(model.ADDS);

            cell = sheet.GetRow(11).GetCell(2);
            cell.SetCellValue(model.ZYCG);

            string imgpath = Path.Combine(Application.StartupPath, model.IMG);
            if (File.Exists(imgpath))
            {
                byte[] bytes = System.IO.File.ReadAllBytes(imgpath);
                int pictureIdx = wk.AddPicture(bytes, PictureType.PNG);
                HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
                HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 1023, 0, 7, 2, 9, 5);
                patriarch.CreatePicture(anchor, pictureIdx);
            }
            
          
            
            apth = Path.Combine(apth,model.XM+"个人简历.xls");
            using (FileStream fileStream = File.Open(apth, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                wk.Write(fileStream);
                fileStream.Close();
            }
            xiaoid.forms.xtraMessage.ShowTip("个人简历导出成功.");
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
                string sql = $"DELETE FROM T_A_DATA_RYXX WHERE ID IN ({ids})";
                int count = SQLiteHelper.Execute(sql);
                if (count >= 0)
                {
                    xiaoid.forms.xtraMessage.ShowInfo("删除成功.");
                    WorkListForm_Load(null, null);
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
