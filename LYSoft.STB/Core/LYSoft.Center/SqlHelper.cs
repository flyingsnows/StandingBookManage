using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYSoft.Center
{
    /// <summary>
    /// 操作Sql Server数据库的对象
    /// </summary>
    public class SqlHelper
    {
        /// <summary>
        /// 数据库状态
        /// </summary>
        private bool Connect { get; set; }
        
        /// <summary>
        /// 操作数据库类
        /// </summary>
        public SqlConnection objSqlConnection { get; set; }


        // <summary>
        /// 是否打开数据库  true：是 false：否
        /// </summary>
        private bool IsOpen { get; set; }


        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        public bool ConnectTo()
        {
            try
            {
                if (Connect == false)
                {
                    string Link = "server =47.94.21.167;uid= sa;pwd=btRWDYHW1Y8;database=QIQ;";
                    //string Link = "Server=.;Integrated Security=SSPI;Database=FACE";
                    objSqlConnection = new SqlConnection(Link);
                    objSqlConnection.Open();
                    IsOpen = true;
                    Connect = true;
                }
                return true;
            }
            catch
            {
                Connect = false;
                IsOpen = false;
                return false;
            }
        }

      

        /// <summary>
        /// 执行(增，删，改)语句返回收影响行数
        /// </summary>
        /// <param name="Sql"></param>
        /// <returns></returns>
        public int ExecuteCommand(string Sql)
        {
            try
            {
                if(Connect == false)
                {
                    objSqlConnection.Open();
                }
                SqlCommand comm = new SqlCommand(Sql, objSqlConnection);
                int Count = comm.ExecuteNonQuery(); //受影响行数
                objSqlConnection.Close();
                Connect = false;
                return Count;
            }
            catch(Exception Ex)
            {
                objSqlConnection.Close();
                Connect = false;
                return 0;
            }
            finally
            {
                objSqlConnection.Close();
                Connect = false;
            }
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="Sql"></param>
        /// <returns></returns>
        public void Execute(string Sql)
        {
            try
            {
                if (Connect == false)
                {
                    objSqlConnection.Open();
                }
                SqlCommand comm = new SqlCommand(Sql, objSqlConnection);
                comm.ExecuteNonQuery(); //受影响行数
            }
            catch( Exception Ex)
            {
                objSqlConnection.Close();
                Connect = false;
            }
            finally
            {
                objSqlConnection.Close();
                Connect = false;
            }
        }


        /// <summary>
        /// 返回查询Sql语句的行数
        /// </summary>
        /// <returns></returns>
        public int ExecuteCount(string Sql)
        {
            DataTable M_Table = this.CreateTable(Sql);
            int Count = M_Table.Rows.Count;
            return Count;
        }


        /// <summary>
        /// 查询出来的数据转换成字典类型，Key为Id
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> DataToDictionary(string Sql)
        {
            Dictionary<string, string> DictionaryValues = new Dictionary<string, string>();
            try
            {
                DataTable M_Table = this.CreateTable(Sql);
                for (int i = 0; i < M_Table.Rows.Count; i++)
                {
                    string Id = M_Table.Rows[i].ItemArray[0].ToString();
                    string Values = M_Table.Rows[i].ItemArray[1].ToString();
                    DictionaryValues.Add(Id, Values);
                }
            }
            catch(Exception Ex)
            {
            }
            return DictionaryValues;
        }



        /// <summary>
        /// 查询数据返回DataTable
        /// </summary>
        /// <param name="Sql"></param>
        /// <returns></returns>
        public DataTable CreateTable(string Sql)
        {
            DataTable Table = new DataTable();
            try
            {
                if (Connect == false)
                {
                    objSqlConnection.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter(Sql, objSqlConnection);
                DataSet ds = new DataSet();//创建DataSet实例
                da.Fill(Table);
                objSqlConnection.Close();//关闭数据库
                Connect = false;
                return Table;
            }
            catch(Exception Ex)
            {
                objSqlConnection.Close();
                Connect = false;
                return Table;
            }
        }

        /// <summary>
        /// 根据索引查询该行列的值
        /// </summary>
        /// <param name="Sql">查询语句</param>
        /// <param name="RowIndex">行号</param>
        /// <param name="ColumnIndex">列号</param>
        /// <returns></returns>
        public string ExecuteScalarInt32(string Sql,int RowIndex,int ColumnIndex)
        {
            string str = "";
            try
            {
                DataTable M_Table = this.CreateTable(Sql);
                if (M_Table.Rows.Count <= RowIndex && M_Table.Columns.Count <= ColumnIndex)
                {
                    str = M_Table.Rows[RowIndex].ItemArray[ColumnIndex].ToString();
                }
            }
            catch
            {
                return "";
            }

            return str;
        }

        /// <summary>
        /// 人脸信息
        /// </summary>
        /// <returns></returns>
        public bool InserFaceToByte(byte[] byfil,string StuId,string sql)
        {
            try
            {
                if (Connect == false)
                {
                    objSqlConnection.Open();
                } 
                SqlCommand cmd = new SqlCommand(sql, objSqlConnection);
                cmd.Parameters.Add("@Face", SqlDbType.Image).Value = byfil;
                cmd.Parameters.Add("@StuId", SqlDbType.Int).Value = StuId;
                cmd.Parameters.Add("@CreateTime", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.ExecuteNonQuery();
                objSqlConnection.Close();
                Connect = false;
                return true;
            }
            catch (Exception Ex)
            {
                objSqlConnection.Close();
                Connect = false;
                return false;
            }
        }






        public void ExecuteCommand(string sql,ref int Count)
        {
            DataTable Table = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, objSqlConnection);
                DataSet ds = new DataSet();//创建DataSet实例
                da.Fill(Table);
                objSqlConnection.Close();//关闭数据库
                Count= Table.Rows.Count;
            }
            catch(Exception Ex)
            {
                objSqlConnection.Close();
                Count = Table.Rows.Count;
            }
        }


    }
}
