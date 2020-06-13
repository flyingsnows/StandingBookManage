using LYSoft.Center;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace LYSoft.DataBase.SQLite
{
    public class SQLiteHelper
    {

        private static SQLiteConnection objSqlConnection = null;

        /// <summary>
        /// 数据库路径
        /// </summary>
        private static string SourcePath
        {
            get
            {
                string result = string.Empty;
                result = Path.Combine(MyApps.DataSource, MyApps.DataName);
                return result;
            }
            set
            {
                SourcePath = value;
            }
        }
        
        
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                string result = string.Empty;
                result = "Data Source = " + SourcePath;
                return result;
            }
            private set
            {
                ConnectionString = value;
            }
        }

        static SQLiteHelper()
        {
            try
            {
                if (!IOHelper.FileExist(SourcePath))
                {
                    SQLiteConnection.CreateFile(SourcePath); //不存则创建数据库
                }
                objSqlConnection = new SQLiteConnection(ConnectionString);
                objSqlConnection.Open();
            }
            catch(Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
            }
         
        }


        /// <summary>
        /// 执行sql语句返回受影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int Execute(string sql)
        {
            int result = -1;
            try
            {
                if (objSqlConnection.State != System.Data.ConnectionState.Open)
                {
                    objSqlConnection.Open();
                }
                SQLiteCommand cmd = new SQLiteCommand(sql, objSqlConnection);
                result = cmd.ExecuteNonQuery(); //受影响行数
            }
            catch(Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
            }
            finally
            {
                objSqlConnection.Close();
            }
            return result;
        }


        /// <summary>
        /// 执行sql语句返回第一列第一行
        /// </summary>
        /// <returns></returns>
        public static object ExecuteToFirst(string sql)
        {
            object result = "";
            try
            {
                if (objSqlConnection.State != System.Data.ConnectionState.Open)
                {
                    objSqlConnection.Open();
                }
                SQLiteCommand cmd = new SQLiteCommand(sql, objSqlConnection);
                result = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
            }
            finally
            {
                objSqlConnection.Close();
            }
            return result;
        }
        
        /// <summary>
        /// 查询数据返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable QueryDataTable(string sql)
        {
            DataTable result = new DataTable();
            try
            {
                if (objSqlConnection.State != System.Data.ConnectionState.Open)
                {
                    objSqlConnection.Open();
                }
                SQLiteCommand cmd = new SQLiteCommand(sql, objSqlConnection);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                adapter.Fill(result); 
            }
            catch(Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
            }
            finally
            {
                objSqlConnection.Close();
            }
            return result;
        }


        /// <summary>
        /// 事务提交数据(批处理)
        /// </summary>
        /// <param name="sqllist">要执行的SQL语句集合</param>
        /// <returns></returns>
        public static bool ExecuteTransaction(List<string> sqllist)
        {
            bool result = false;
            try
            {
                if (objSqlConnection.State != System.Data.ConnectionState.Open)
                {
                    objSqlConnection.Open();
                }
                if (sqllist == null || sqllist.Count == 0) return false;
                SQLiteCommand cmd = objSqlConnection.CreateCommand();
                SQLiteTransaction transaction = objSqlConnection.BeginTransaction();
                cmd.Connection = objSqlConnection;
                cmd.Transaction = transaction;
                int count = 0;
                foreach (var item in sqllist)
                {
                    if (item == "" || item == null) continue;  //最后会回滚事务
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = item;  //要执行的sql语句
                    int x = cmd.ExecuteNonQuery();
                    if (x > 0) count = count + 1;  //记录执行成功次数
                }
                if (sqllist.Count == count)   //sql语句全部执行成功后提交事务到数据库
                {
                    result = true;
                    transaction.Commit(); //提交事务
                }
                else
                {
                    transaction.Rollback(); //回滚
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
            }
            finally
            {
                objSqlConnection.Close();
            }
            return result;
        }


        public static int ExecuteToKey(string sql)
        {
            int result = -1;
            try
            {
                if (objSqlConnection.State != System.Data.ConnectionState.Open)
                {
                    objSqlConnection.Open();
                }
                SQLiteCommand cmd = new SQLiteCommand(sql, objSqlConnection);
                result = cmd.ExecuteScalar().ObjectToInt();

            }
            catch(Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
            }
            return result;
        }






    }
}
