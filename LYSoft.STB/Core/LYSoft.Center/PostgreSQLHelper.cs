using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LYSoft.Center
{
    /// <summary>
    /// PostgreSQLHelper操作对象
    /// </summary>
    public class PostgreSQLHelper
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return "PORT=5432;DATABASE=test;HOST=localhost;PASSWORD=000000;USER ID=postgres";
            }
            set
            {
                ConnectionString = value;
            }
        }

        /// <summary>
        /// 是否打开数据库进行操作  true：是 false：否
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 是否连接到数据库
        /// </summary>
        public bool IsConnect { get; set; }

        /// <summary>
        /// 数据库操作对象
        /// </summary>
        private NpgsqlConnection sqlConnection { get; set; }

        

        /// <summary>
        /// 连接到数据库
        /// </summary>
        public PostgreSQLHelper(string connstring="")
        {
            if(connstring!= "")
            {
                ConnectionString = connstring;
            }
            try
            {
                if (!IsConnect)
                {
                    sqlConnection = new NpgsqlConnection(ConnectionString);
                    sqlConnection.Open();
                    IsConnect = true;
                    IsOpen = true; //标识已打开数据库连接
                }
            }
            catch(Exception ex)
            {
                IsConnect = false;
            }
        }

        /// <summary>
        /// 根据sql 语句查询表
        /// </summary>
        /// <returns></returns>
        public DataTable CreateTale(string sql)
        {
            DataTable NP_Table = new DataTable();
            try
            {
                if (!IsOpen)
                {
                    sqlConnection.Open(); //打开数据库连接
                }
                NpgsqlDataAdapter sqldap = new NpgsqlDataAdapter(sql, sqlConnection);
                sqldap.Fill(NP_Table);
                this.IsClose();
                return NP_Table;
            }
            catch(Exception ex)
            {
                this.IsClose();
                return NP_Table;
            }
        }


        public int Execute(string sql)
        {
            int n = -1;
            try
            {
                if (!IsOpen)
                {
                    sqlConnection.Open(); //打开数据库连接
                }
                NpgsqlCommand SqlCommand = new NpgsqlCommand(sql, sqlConnection);
                n = SqlCommand.ExecuteNonQuery();
                this.IsClose();
                return n;
            }
            catch (Exception ex)
            {
                this.IsClose();
                return n;
            }
        }


        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        private void IsClose()
        {
            sqlConnection.Close();
            IsOpen = false;
        }

    }
}
