using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LYSoft.ArcCore.Entity
{
    /// <summary>
    /// 人脸识别信息实体  (增删改查界面用)
    /// </summary>
    public class FaceEntity
    {
        /// <summary>
        /// 年龄
        /// </summary>
        public int Ages { get; set; }

        /// <summary>
        /// 性別 0：男 1：女 2：未知 -1：识别失败
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 图片文件(采集)
        /// </summary>
        public string ImgBase64 { get; set; }
        
        /// <summary>
        /// 当前摄像头采集的图片
        /// </summary>
        public string ImgNowBase64 { get; set; }
        
        /// <summary>
        /// 人员ID
        /// </summary>
        public int RYId { get; set; }

        /// <summary>
        /// 相似度
        /// </summary>
        public string Sim { get; set; }

        /// <summary>
        ///人脸特征码
        /// </summary>
        public byte[] Feature { get; set; }
        
        /// <summary>
        /// 状态  0：识别成功 -1：识别失败
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 分类 0：人脸比对
        /// </summary>
        public int Type { get; set; }


        /// <summary>
        /// 图片中人脸数量
        /// </summary>
       public int FaceNum { get; set; }

    }


   

}
