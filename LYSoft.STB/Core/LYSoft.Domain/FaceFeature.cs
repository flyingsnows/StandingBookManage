using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LYSoft.Domain
{
    /// <summary>
    /// 人脸特征码实体
    /// </summary>
    public class FaceFeature
    {
        /// <summary>
        /// 人员Id
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 特征码Base64
        /// </summary>
        public string FeatureBase64 { get; set; }
        
        /// <summary>
        /// 图片Base64
        /// </summary>
        public string ImageBase64 { get; set; }

    }
}
