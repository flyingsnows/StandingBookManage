using LYSoft.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LYSoft.Center
{
    /// <summary>
    /// 全局缓存
    /// </summary>
    public class CacheHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static object lockadd = new object();

        /// <summary>
        /// 基础数据
        /// </summary>
        public static List<T_A_DATA_RYXX> Comparer { get; set; }

        /// <summary>
        /// 添加项到集合
        /// </summary>
        public static void Add(List<T_A_DATA_RYXX> list)
        {
            lock (lockadd)
            {
                try
                {
                    if (Comparer == null)
                    {
                        Comparer = new List<T_A_DATA_RYXX>();
                    }
                    foreach (var item in list)
                    {
                        T_A_DATA_RYXX temp = Comparer.Find(o => o.ID == item.ID);
                        if(temp == null)   //将新数据加载到集合中
                        {
                            Comparer.Add(item);
                        }
                    }
                }
                catch(Exception ex)
                {
                    LogHelper.WriteError(ex.ToString());
                }
            }
        }



        public static void Add(T_A_DATA_RYXX model)
        {
            lock (lockadd)
            {
                try
                {
                    if (Comparer == null)
                    {
                        Comparer = new List<T_A_DATA_RYXX>();
                    }
                    T_A_DATA_RYXX temp = Comparer.Find(o => o.ID == model.ID);
                    if (temp == null)   //将新数据加载到集合中
                    {
                        Comparer.Add(model);
                    }
                    else
                    {
                        //更新对象
                        Comparer.Remove(temp);
                        Comparer.Add(model);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteError(ex.ToString());
                }
            }
        }


        /// <summary>
        /// 获取姓名
        /// </summary>
        /// <param name="cph"></param>
        /// <returns></returns>
        //public static string GetRYXX(string cph)
        //{
        //    lock (lockadd)
        //    {
        //        string XM = "";
        //        try
        //        {
        //            XM = Comparer.Find(o => o.CPH == cph)?.XM;
        //        }
        //        catch(Exception ex)
        //        {
        //            LogHelper.WriteError(ex.ToString());
        //        }
        //        return XM;
        //    }
        //}

    }
}
