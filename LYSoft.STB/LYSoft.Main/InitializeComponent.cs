using LYSoft.Center;
using LYSoft.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;

namespace LYSoft.Main
{
    /// <summary>
    /// 全局数据
    /// </summary>
    public class InitializeComponent
    {
        /// <summary>
        /// 人脸特征集合
        /// </summary>
        public static List<FaceFeature> FaceList = new List<FaceFeature>();
        
        /// <summary>
        /// 人脸数据的锁
        /// </summary>
        private static object lockface = new object();

        public static void InitFaceData()
        {
            lock (lockface)
            {
                LogHelper.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-> 开始人脸数据同步");
                DataTable table = null;
                try
                {
                    //string sql = @"select a.ID,b.TZM,b.ImgBase64,a.CJSJ from T_A_DATA_RYXX a 
                    //           left join T_A_DATA_FACE b on a.ID = b.RYId where TZM is not null
                    //           and b.ID in (select b.ID from T_A_DATA_FACE b group by b.RYId)";

                    //table = SQLiteHelper.QueryDataTable(sql);
                    //for (int i = 0; i < table.Rows.Count; i++)
                    //{
                    //    FaceFeature face = new FaceFeature();
                    //    face.Id = table.Rows[i]["ID"].ObjectToInt();
                    //    face.FeatureBase64 = table.Rows[i]["TZM"].ToString();
                    //    face.ImageBase64 = table.Rows[i]["ImgBase64"].ToString();
                    //    FaceList.Add(face);
                    //}
                }
                catch (Exception ex)
                {
                    LogHelper.WriteError(ex.ToString());
                }
                finally
                {
                    LogHelper.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-> 人脸数据同步完成,新增数量：" + table.Rows.Count);
                    Thread.Sleep(15000);  //半分钟同步一次
                }
            }
            

        }

        /// <summary>
        /// 添加人脸特征
        /// </summary>
        public static void AddFace(int id,string feature,string img)
        {
            lock (lockface)
            {
                FaceFeature face = new FaceFeature();
                face.Id = id;
                face.FeatureBase64 = feature;
                face.ImageBase64 = img;
                FaceList.Add(face);
            }
        }




    }
}
