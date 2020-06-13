using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace LYSoft.Center
{
    /// <summary>
    /// 实体转换类
    /// </summary>
    public class Converts
    {
        /// <summary>
        /// 将DataTable转换为实体列表
        /// 作者: 李炎
        /// 创建时间: 2019年9月13日
        /// </summary> <T>
        /// <param name="dt">待转换的DataTable</param>
        /// <returns></returns>
        public static List<T> ConvertToList<T>(DataTable dt) where T : new()
        {
            // 定义集合  
            var list = new List<T>();

            if (0 == dt.Rows.Count)
            {
                return list;
            }

            //遍历DataTable中所有的数据行  
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    var entity = new T();

                    // 获得此模型的公共属性  
                    var propertys = entity.GetType().GetProperties();

                    //遍历该对象的所有属性  
                    foreach (var p in propertys)
                    {
                        //将属性名称赋值给临时变量
                        string tmpName = p.Name;

                        //检查DataTable是否包含此列（列名==对象的属性名）    
                        if (dt.Columns.Contains(tmpName))
                        {
                            // 判断此属性是否有Setter
                            if (!p.CanWrite)
                            {
                                continue; //该属性不可写，直接跳出
                            }

                            //取值  
                            var value = dr[tmpName];

                            //如果非空，则赋给对象的属性  
                            if (value != System.DBNull.Value)
                            {
                                p.SetValue(entity, value, null);
                            }
                        }
                    }
                    //对象添加到泛型集合中  
                    list.Add(entity);
                }
                catch (Exception ex)
                {

                }


            }

            return list;
        }

        /// <summary>
        /// 将DataTable转换为实体
        /// 作者: 李炎
        /// 创建时间: 2019年11月15日
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static T Convert<T>(DataTable dt) where T : new()
        {
            // 定义集合  
            var list = new List<T>();

            if (0 == dt.Rows.Count)
            {
                return list.FirstOrDefault();
            }

            //遍历DataTable中所有的数据行  
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    var entity = new T();

                    // 获得此模型的公共属性  
                    var propertys = entity.GetType().GetProperties();

                    //遍历该对象的所有属性  
                    foreach (var p in propertys)
                    {
                        //将属性名称赋值给临时变量
                        string tmpName = p.Name;

                        //检查DataTable是否包含此列（列名==对象的属性名）    
                        if (dt.Columns.Contains(tmpName))
                        {
                            // 判断此属性是否有Setter
                            if (!p.CanWrite)
                            {
                                continue; //该属性不可写，直接跳出
                            }

                            //取值  
                            var value = dr[tmpName];

                            //如果非空，则赋给对象的属性  
                            if (value != DBNull.Value)
                            {
                                p.SetValue(entity, value, null);
                            }
                        }
                    }
                    //对象添加到泛型集合中  
                    list.Add(entity);
                }
                catch (Exception ex)
                {
                }
            }

            return list.FirstOrDefault();
        }


    }
}