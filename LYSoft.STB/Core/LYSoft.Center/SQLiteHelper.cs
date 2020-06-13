using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace LYSoft.Center
{
    /// <summary>
    /// SQLite操作对象
    /// </summary>
    public class SQLiteHelper
    {
        //数据库连接
        SQLiteConnection m_dbConnection;
        
        /// <summary>
        /// 是否打开数据库  true：是 false：否
        /// </summary>
        private bool IsOpen { get; set; }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        public void InitializeDataSource(string path)
        {
            try
            {
                string datapath = Path.Combine(path , MyApps.DataName);
                if (File.Exists(datapath))
                {
                    goto cc; //存在库中 直接连接数据库
                }

                SQLiteConnection.CreateFile(datapath);
                cc: m_dbConnection = new SQLiteConnection(string.Format("Data Source ={0};Version=3;", datapath));
                m_dbConnection.Open();
                this.IsOpen = true;
            }
            catch (Exception ex)
            {
                this.IsOpen = false;
            }
        }


        /// <summary>
        /// 创建数据库表对象
        /// </summary>
        private void CreateDataTable()
        {
            try
            {
                SQLiteCommand command = null;
                if (!IsOpen)
                {
                    m_dbConnection.Open(); //打开数据库连接
                }
                
                string sql = string.Format(@"select * from sqlite_master where type = 'table' and name = '{0}' ","UserInfo");
                bool IsTrue = Exists(sql);
                if (!IsTrue)  //不存在则创建表 UserInfo
                {

                    string sqluser = @"Create Table UserInfo(
	                                    [ID]  integer PRIMARY KEY autoincrement,
	                                    [UserName]         varchar(50),
	                                    [LoginAccount]         varchar(50),
	                                    [LoginTime]         datetime,
	                                    [Password]         varchar(50),
	                                    [Istate]         int
                                    );";
                    if (!IsOpen)
                    {
                        m_dbConnection.Open(); //打开数据库连接
                        IsOpen = true;
                    }
                    command = new SQLiteCommand(sqluser, m_dbConnection);
                    command.ExecuteNonQuery();
                }

                //是否存在图片信息表
                sql = string.Format(@"select * from sqlite_master where type = 'table' and name = '{0}' ", "PersonnelInfo");
                IsTrue = Exists(sql);
                if (!IsTrue)  //不存在则创建表 UserInfo
                {

                    string sqluser = @"Create Table PersonnelInfo(
 [ID]  integer PRIMARY KEY autoincrement,
 [Name]         varchar(200),
 [Sex]             int,
 [Ages]            int,
 [FaseBaseImg64]   text,
 [FingerBaseImg64] text,
 [CreateTime]      datetime      
)";
                    if (!IsOpen)
                    {
                        m_dbConnection.Open(); //打开数据库连接
                        IsOpen = true;
                    }
                    command = new SQLiteCommand(sqluser, m_dbConnection);
                    command.ExecuteNonQuery();
                }
                
                m_dbConnection.Close();
                IsOpen = false;
            }
            catch(Exception ex)
            {
                m_dbConnection.Close();
                IsOpen = false;
            }
        }


        /// <summary>
        /// 执行语句
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        public void Execute(string sql)
        {
            try
            {
                if (!IsOpen)
                {
                    m_dbConnection.Open(); //打开数据库连接
                }
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                m_dbConnection.Close();
                IsOpen = false;  //标识数据库已关闭
            }
            catch(Exception ex)
            {
                m_dbConnection.Close();
                IsOpen = false; //标识数据库已关闭
            }
        }

        public int ExecuteList(IList<string> SQLLIST)
        {
            int ret = -1;
            try
            {
                if (!IsOpen)
                {
                    m_dbConnection.Open(); //打开数据库连接
                }

                foreach(var sql in SQLLIST) //循环SQL语句
                {
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                }
              
                m_dbConnection.Close();
                IsOpen = false;  //标识数据库已关闭
                ret = 0;
            }
            catch (Exception ex)
            {
                m_dbConnection.Close();
                IsOpen = false; //标识数据库已关闭
                ret = -1;
            }
            return ret;
        }



        public DataTable CreateTable(string sql)
        {
            DataTable data = new DataTable();
            try
            {
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                adapter.Fill(data);
                m_dbConnection.Close();
                IsOpen = false;  //标识数据库已关闭
                return data;
            }
            catch(Exception ex)
            {
                m_dbConnection.Close();
                IsOpen = false;  //标识数据库已关闭
                return data;
            }
        }

        /// <summary>
        /// 是否存在对象 true：存在 false:不存在
        /// </summary>
        /// <returns></returns>
        public bool Exists(string sql)
        {
            try
            {
                DataTable K_Table = CreateTable(sql);
                if(K_Table == null || K_Table.Rows.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

        }


    }
}
