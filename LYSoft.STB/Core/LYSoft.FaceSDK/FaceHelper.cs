using Emgu.CV;
using Emgu.CV.Structure;
using Luxand;
using LYSoft.Center;
using LYSoft.DataBase.SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LYSoft.FaceSDK
{
    public class FaceHelper
    {
        static FaceHelper()
        {
            try
            {
                if (FSDK.FSDKE_OK != FSDK.ActivateLibrary("gyYgVWQTSzjiuGB/hH8dKgg0QrrIuhoHdfUCzD9rY+vru3WRZsaezTX6YWj9osdI/cmxY1NSdLkyWuugMPCxUG7/xNLegHLeaUpzVyKpDkaWL8tJIUsIL7xv9bhmgifPbAyTDuxF3VGxXmHkv/L/MStf9kdXV/A1vVvT93QC4vQ="))
                {
                    return;
                }
                FSDK.InitializeLibrary();  //初始化
                FSDKCam.InitializeCapturing();  //初始化
            }
            catch(Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
            }
        }








        //获取人脸特征码
        public static byte[] GetFaceTemplate(FSDK.CImage image, ref FSDK.TFacePosition FacePosition)
        {
            byte[] result = null;
            try
            {
                FacePosition = image.DetectFace();
                if (0 != FacePosition.w)
                {
                    var faceImage = image.CopyRect((int)(FacePosition.xc - Math.Round(FacePosition.w * 0.5)), (int)(FacePosition.yc - Math.Round(FacePosition.w * 0.5)), (int)(FacePosition.xc + Math.Round(FacePosition.w * 0.5)), (int)(FacePosition.yc + Math.Round(FacePosition.w * 0.5)));

                    var FacialFeatures = image.DetectEyesInRegion(ref FacePosition);//眼睛对齐

                    result = image.GetFaceTemplateInRegion(ref FacePosition); //获取特征值
                    faceImage.Dispose();
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
            }
            return result;
        }



        //获取人脸特征码
        public static byte[] GetFaceTemplate(Bitmap img)
        {
            byte[] result = null;
            FSDK.CImage image = new FSDK.CImage(img);
            FSDK.TFacePosition FacePosition = new FSDK.TFacePosition();
            try
            {
                FacePosition = image.DetectFace();
                if (0 != FacePosition.w)
                {
                    var faceImage = image.CopyRect((int)(FacePosition.xc - Math.Round(FacePosition.w * 0.5)), (int)(FacePosition.yc - Math.Round(FacePosition.w * 0.5)), (int)(FacePosition.xc + Math.Round(FacePosition.w * 0.5)), (int)(FacePosition.yc + Math.Round(FacePosition.w * 0.5)));

                    var FacialFeatures = image.DetectEyesInRegion(ref FacePosition);//眼睛对齐

                    result = image.GetFaceTemplateInRegion(ref FacePosition); //获取特征值
                    faceImage.Dispose();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
            }
            return result;
        }




        public static int CompareFeature(Bitmap bitmap, ref float similarity, ref int ryid)
        {
            int result = 0;
            try
            {
                byte[] feature = GetFaceTemplate(bitmap);  //当前人脸特征码
                string sql = @"select ID,TZM from T_A_DATA_STUDENT";
                DataTable tab = SQLiteHelper.QueryDataTable(sql);
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    string base64 = tab.Rows[i]["TZM"].ToString();
                    byte[] tempfea = base64.Base64ToBytes();
                    int istrue = FSDK.MatchFaces(ref tempfea, ref feature, ref similarity);
                    if (similarity >= 0.8)
                    {
                        ryid = tab.Rows[i]["ID"].ObjectToInt();
                        break;
                    }
                }
                result = 1;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
                result = 0;
            }
            return result;
        }






    }
}
