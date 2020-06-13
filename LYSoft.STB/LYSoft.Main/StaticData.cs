using LYSoft.ArcCore;
using LYSoft.ArcCore.Entity;
using LYSoft.Center;
using LYSoft.Domain;
using StreamingServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace LYSoft.Main
{
    /// <summary>
    /// 全局静态对象,
    /// </summary>
    public static class StaticData
    {
        /// <summary>
        /// 视频
        /// </summary>
        public static VideoHelper Video = new VideoHelper();
        
        /// <summary>
        /// 软虹人脸识别SDK
        /// </summary>
        public static ArcHelper Arc = new ArcHelper();
        
        

        static StaticData()
        {
            StatrInitDataBase();
        }



       
        private static void Video_FaceEventHandler(Bitmap map)
        {
            if(map != null )
            {
                var model = Arc.PartFaceAgeandSex(map);
                if(model !=null && model.State != -1)
                {
                    //Thread.Sleep(100);
                    //model.Type = 1;
                    //string json = model.JSON();
                    //websocket.SendJson(json);

                    //Thread th = new Thread(FaceMainCompare);
                    //th.IsBackground = true;
                    //th.Start(model);
                }
            }
        }

        /// <summary>
        /// 操作人脸集合锁 
        /// </summary>

        public static object lockdis = new object();

        /// <summary>
        /// 主界面识别
        /// </summary>
        public static void FaceMainCompare(object obj)
        {
            lock (lockdis)
            {
                string json = "";
                FaceEntity face = new FaceEntity();
                FaceEntity util = obj as FaceEntity;
                string image1Feature = util.Feature.BytesToBase64();
                foreach (var item in InitializeComponent.FaceList)
                {
                    float result = Arc.FaceFeatureCompare(image1Feature, item.FeatureBase64);
                    if (result >= 0.8)
                    {
                        face.State = 0;
                        face.Type = 0;
                        face.ImgBase64 = item.ImageBase64;
                        face.ImgNowBase64 = util.ImgBase64;
                        face.RYId = item.Id;
                        face.Sim = (result * 100).ToString("#0.00");
                        json = face.JSON();
                        //websocket.SendJson(json);
                        LogHelper.WriteLog("人脸识别人员编号：" + face.RYId + "   相似度：" + result);
                        return;
                    }
                }
                face.State = 0;
                face.Type = 0;
                face.ImgNowBase64 = util.ImgBase64;
                face.RYId = 0;
                face.Sim = "-1";
                json = face.JSON();
                //websocket.SendJson(json);
            }
        }





        public static void StatrInitDataBase()
        {
            //人脸数据同步的线程
            Thread facedatath = new Thread(InitializeComponent.InitFaceData);
            facedatath.IsBackground = true;
            facedatath.Start();



        }








       









        


    }
}
