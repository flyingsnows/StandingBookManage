using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYSoft.Center
{
    /// <summary>
    /// 操作Excel 文档的对象
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// 数据库连接状态  属性
        /// </summary>
        public bool Connect { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
       public  string connstring = "";

        /// <summary>
        /// 操作数据库类  属性
        /// </summary>
        public OleDbConnection ObjSqlConnection { get; set; }

        public bool ConnectTo(string Path)
        {
            try
            {
                if (Connect == false)
                {
                    connstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1';";
                    ObjSqlConnection = new OleDbConnection(connstring);
                    ObjSqlConnection.Open();
                    Connect = true;
                }
                return true;
            }
            catch(Exception ex)                                  //try....catch....  异常处理
            {
                Connect = false;
                return false;
            }
        }


        public DataTable CreateTable(string sql)
        {
            DataTable sheetsName = ObjSqlConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" }); //得到所有sheet的名字
            string firstSheetName = sheetsName.Rows[0][2].ToString(); //得到第一个sheet的名字
            sql = string.Format(sql, firstSheetName); //查询字符串                    //string sql = string.Format("SELECT * FROM [{0}] WHERE [日期] is not null", firstSheetName); //查询字符串
            OleDbDataAdapter ada = new OleDbDataAdapter(sql, connstring);
            DataSet set = new DataSet();
            ada.Fill(set);
            return set.Tables[0];
        }

        public DataTable CreateTables(string sql)
        {
            DataTable sheetsName = ObjSqlConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" }); //得到所有sheet的名字
            string firstSheetName = sheetsName.Rows[0][2].ToString(); //得到第一个sheet的名字



            sql = string.Format(sql, firstSheetName); //查询字符串                    //string sql = string.Format("SELECT * FROM [{0}] WHERE [日期] is not null", firstSheetName); //查询字符串
            OleDbDataAdapter ada = new OleDbDataAdapter(sql, connstring);
            DataSet set = new DataSet();
            ada.Fill(set);
            return set.Tables[0];
        }

    }
}
