using LYSoft.Center;
using LYSoft.DataBase.SQLite;
using LYSoft.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LYSoft.DataBase.SQLite
{
    public static partial class ModelToSqlExtends
    {
        #region 对象数据入库

        /// <summary>
        /// INSERT 到数据库
        /// </summary>
        /// <param name="model">对象</param>
        /// <returns>受影响行数</returns>
        public static string Add<T>(this T model)
        {
            string result = "";
            try
            {
                string sql = "INSERT INTO {0} ({1})VALUES({2})";
                // 获得此模型的公共属性  
                var propertys = model.GetType().GetProperties();
                StringBuilder columnlist = new StringBuilder();
                StringBuilder values = new StringBuilder();
                string tab = model.GetType().Name;
                //遍历该对象的所有属性  
                foreach (var p in propertys)
                {
                    //获取自定义字段特性
                    var attr = p.GetCustomAttributes(typeof(MyStringKeyAttribute), true).FirstOrDefault() as MyStringKeyAttribute;
                    if (attr == null || attr.IsKey == false)
                    {
                        columnlist.Append(p.Name + ",");
                        values.Append("'" + p.GetValue(model, null).ToString() + "',");
                    }
                }
                string column = columnlist.ToString().TrimEnd(',');
                string value = values.ToString().TrimEnd(',');
                sql = string.Format(sql, tab, column, value); //表名称(实体名称)
                //result = SQLiteHelper.ExecuteToKey(sql).ToString();
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                result = "";
            }
            return result;
        }


        /// <summary>
        /// INSERT 到数据库
        /// </summary>
        /// <param name="model">对象</param>
        /// <returns>受影响行数</returns>
        public static int AddToInt<T>(this T model)
        {
            int result = -1;
            try
            {
                string sql = "INSERT OR IGNORE INTO {0} ({1})VALUES({2})";
                // 获得此模型的公共属性  
                var propertys = model.GetType().GetProperties();
                StringBuilder columnlist = new StringBuilder();
                StringBuilder values = new StringBuilder();
                string tab = model.GetType().Name;
                //遍历该对象的所有属性  
                foreach (var p in propertys)
                {
                    string val = p.GetValue(model, null).ToString() == null ? "" : p.GetValue(model, null).ToString();
                    //获取自定义字段特性
                    var attr = p.GetCustomAttributes(typeof(MyStringKeyAttribute), true).FirstOrDefault() as MyStringKeyAttribute;
                    if (attr == null || attr.IsKey == false)
                    {
                        columnlist.Append(p.Name + ",");
                        values.Append("'" + val + "',");
                    }
                }
                string column = columnlist.ToString().TrimEnd(',');
                string value = values.ToString().TrimEnd(',');
                sql = string.Format(sql, tab, column, value); //表名称(实体名称)
                result = SQLiteHelper.Execute(sql);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                result = -1;
            }
            return result;
        }




        /// <summary>
        /// UPDATE 到数据库
        /// </summary>
        /// <param name="model">对象</param>
        /// <returns>受影响行数</returns>
        public static int Update<T>(this T model)
        {
            int result = -1;
            try
            {
                string sql = "UPDATE {0} SET {1} WHERE {2} ";
                // 获得此模型的公共属性  
                var propertys = model.GetType().GetProperties();
                StringBuilder values = new StringBuilder();
                string where = ""; //更新条件
                string tab = model.GetType().Name;

                //遍历该对象的所有属性  
                foreach (var p in propertys)
                {
                    string val= p.GetValue(model, null) == null ? "" : p.GetValue(model, null).ToString();
                    if (val == "") continue;  //忽略没有值的属性
                    //获取自定义字段特性
                    var attr = p.GetCustomAttributes(typeof(MyStringKeyAttribute), true).FirstOrDefault() as MyStringKeyAttribute;
                    if (attr == null || attr.IsKey == false)
                    {
                        string keyvalue = p.Name + "=" + "'" + val + "',";
                        values.Append(keyvalue);
                    }
                    else if (attr != null && attr.IsKey)  //主键
                    {
                        where = p.Name + "=" + "'" + p.GetValue(model, null).ToString() + "',";
                    }
                }
                string value = values.ToString().TrimEnd(',');
                where = where.ToString().TrimEnd(',');
                sql = string.Format(sql, tab, value, where);
                result = SQLiteHelper.Execute(sql);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                result = -1;
            }
            return result;
        }



        public static int UpdateNotNull<T>(this T model)
        {
            int result = -1;
            try
            {
                string sql = "UPDATE {0} SET {1} WHERE {2} ";
                // 获得此模型的公共属性  
                var propertys = model.GetType().GetProperties();
                StringBuilder values = new StringBuilder();
                string where = ""; //更新条件
                string tab = model.GetType().Name;

                //遍历该对象的所有属性  
                foreach (var p in propertys)
                {
                    //获取自定义字段特性
                    var attr = p.GetCustomAttributes(typeof(MyStringKeyAttribute), true).FirstOrDefault() as MyStringKeyAttribute;
                    var val = p.GetValue(model, null).ToString();
                    if (val != "")
                    {
                        if (attr == null || attr.IsKey == false)
                        {
                            string keyvalue = p.Name + "=" + "'" + p.GetValue(model, null).ToString() + "',";
                            values.Append(keyvalue);
                        }
                        else if (attr != null && attr.IsKey)  //主键
                        {
                            where = p.Name + "=" + "'" + val + "',";
                        }
                    }
                }
                string value = values.ToString().TrimEnd(',');
                where = where.ToString().TrimEnd(',');
                sql = string.Format(sql, tab, value, where);
                result = SQLiteHelper.Execute(sql);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                result = -1;
            }
            return result;
        }





        /// <summary>
        /// DELETE   到数据库
        /// </summary>
        /// <param name="model">对象</param>
        /// <returns>受影响行数</returns>
        public static int Delete<T>(T model)
        {
            int result = -1;
            try
            {
                string sql = "DELETE FROM {0} WHERE {1} ";
                string tab = model.GetType().Name;
                string where = "";
                var propertys = model.GetType().GetProperties();
                //遍历该对象的所有属性  
                foreach (var p in propertys)
                {
                    //获取自定义字段特性
                    var attr = p.GetCustomAttributes(typeof(MyStringKeyAttribute), true).FirstOrDefault() as MyStringKeyAttribute;

                    if (attr != null && attr.IsKey)  //主键
                    {
                        where = p.Name + "=" + "'" + p.GetValue(model, null).ToString() + "',";
                    }
                }

                where = where.ToString().TrimEnd(',');
                sql = string.Format(sql, tab, where);
                result = SQLiteHelper.Execute(sql);
            }
            catch (Exception ex)
            {
                result = -1;
            }
            return result;
        }



        #endregion



        #region 对象生成SQL语句
        /// <summary>
        /// 生成INSERT 语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">对象</param>
        /// <returns>SQL语句</returns>
        public static string AddToSQL<T>(this T model)
        {
            string result = "";
            try
            {
                string sql = "INSERT INTO {0} ({1})VALUES({2})";
                // 获得此模型的公共属性  
                var propertys = model.GetType().GetProperties();
                StringBuilder columnlist = new StringBuilder();
                StringBuilder values = new StringBuilder();
                string tab = model.GetType().Name;
                //遍历该对象的所有属性  
                foreach (var p in propertys)
                {
                    //获取自定义字段特性
                    var attr = p.GetCustomAttributes(typeof(MyStringKeyAttribute), true).FirstOrDefault() as MyStringKeyAttribute;
                    if (attr == null || attr.IsKey == false)
                    {
                        columnlist.Append(p.Name + ",");
                        values.Append("'" + p.GetValue(model, null).ToString() + "',");
                    }
                }
                string column = columnlist.ToString().TrimEnd(',');
                string value = values.ToString().TrimEnd(',');
                result = string.Format(sql, tab, column, value); //表名称(实体名称)
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                result = "";
            }
            return result;
        }


        /// <summary>
        /// 生成UPDATE 语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">对象</param>
        /// <returns>SQL语句</returns>
        public static string UpdateToSQL<T>(T model)
        {
            string result ="";
            try
            {
                string sql = "UPDATE {0} SET {1} WHERE {2} ";
                // 获得此模型的公共属性  
                var propertys = model.GetType().GetProperties();
                StringBuilder values = new StringBuilder();
                string where = ""; //更新条件
                string tab = model.GetType().Name;

                //遍历该对象的所有属性  
                foreach (var p in propertys)
                {
                    //获取自定义字段特性
                    var attr = p.GetCustomAttributes(typeof(MyStringKeyAttribute), true).FirstOrDefault() as MyStringKeyAttribute;
                    if (attr == null || attr.IsKey == false)
                    {
                        string keyvalue = p.Name + "=" + "'" + p.GetValue(model, null).ToString() + "',";
                        values.Append(keyvalue);
                    }
                    else if (attr != null && attr.IsKey)  //主键
                    {
                        where = p.Name + "=" + "'" + p.GetValue(model, null).ToString() + "',";
                    }
                }
                string value = values.ToString().TrimEnd(',');
                where = where.ToString().TrimEnd(',');
                result = string.Format(sql, tab, value, where);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                result = "";
            }
            return result;
        }

        /// <summary>
        /// 生成DELETE 语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">对象</param>
        /// <returns>SQL语句</returns>
        public static string DeleteToSQL<T>(T model)
        {
            string result = "";
            try
            {
                string sql = "DELETE FROM {0} WHERE {1} ";
                string tab = model.GetType().Name;
                string where = "";
                var propertys = model.GetType().GetProperties();
                //遍历该对象的所有属性  
                foreach (var p in propertys)
                {
                    //获取自定义字段特性
                    var attr = p.GetCustomAttributes(typeof(MyStringKeyAttribute), true).FirstOrDefault() as MyStringKeyAttribute;

                    if (attr != null && attr.IsKey)  //主键
                    {
                        where = p.Name + "=" + "'" + p.GetValue(model, null).ToString() + "',";
                    }
                }

                where = where.ToString().TrimEnd(',');
                result = string.Format(sql, tab, where);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                result = "";
            }
            return result;
        }

        #endregion
    }



    
}
