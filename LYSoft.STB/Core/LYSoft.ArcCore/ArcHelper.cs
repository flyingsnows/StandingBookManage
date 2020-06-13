using ArcSoftFace.Entity;
using ArcSoftFace.SDKModels;
using ArcSoftFace.SDKUtil;
using ArcSoftFace.Utils;
using LYSoft.ArcCore.Entity;
using LYSoft.Center;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace LYSoft.ArcCore
{
    public static class ArcHelper
    {
        #region 参数定义
        /// <summary>
        /// 引擎Handle
        /// </summary>
        private static IntPtr pImageEngine = IntPtr.Zero;


        /// <summary>
        /// RGB视频引擎 FR Handle 处理   FR和图片引擎分开，减少强占引擎的问题
        /// </summary>
        private static IntPtr pVideoRGBImageEngine = IntPtr.Zero;

        #endregion

        #region 初始化引擎
        /// <summary>
        /// 初始化引擎
        /// </summary>
        public static void InitEngines()
        {
            //读取配置文件
            AppSettingsReader reader = new AppSettingsReader();
            string appId = (string)reader.GetValue("APP_ID", typeof(string));
            string sdkKey64 = (string)reader.GetValue("SDKKEY64", typeof(string));
            string sdkKey32 = (string)reader.GetValue("SDKKEY32", typeof(string));
            //rgbCameraIndex = (int)reader.GetValue("RGB_CAMERA_INDEX", typeof(int));
            //irCameraIndex = (int)reader.GetValue("IR_CAMERA_INDEX", typeof(int));
            //判断CPU位数
            var is64CPU = Environment.Is64BitProcess;
            if (string.IsNullOrWhiteSpace(appId) || string.IsNullOrWhiteSpace(is64CPU ? sdkKey64 : sdkKey32))
            {
                //禁用相关功能按钮
                // ControlsEnable(false, chooseMultiImgBtn, matchBtn, btnClearFaceList, chooseImgBtn);
                // MessageBox.Show(string.Format("请在App.config配置文件中先配置APP_ID和SDKKEY{0}!", is64CPU ? "64" : "32"));
                return;
            }

            //在线激活引擎    如出现错误，1.请先确认从官网下载的sdk库已放到对应的bin中，2.当前选择的CPU为x86或者x64
            int retCode = 0;
            try
            {
                retCode = ASFFunctions.ASFActivation(appId, is64CPU ? sdkKey64 : sdkKey32);
            }
            catch (Exception ex)
            {
                //禁用相关功能按钮
                // ControlsEnable(false, chooseMultiImgBtn, matchBtn, btnClearFaceList, chooseImgBtn);
                if (ex.Message.Contains("无法加载 DLL"))
                {
                    // MessageBox.Show("请将sdk相关DLL放入bin对应的x86或x64下的文件夹中!");
                }
                else
                {
                    //MessageBox.Show("激活引擎失败!");
                }
                return;
            }
            Console.WriteLine("Activate Result:" + retCode);

            //初始化引擎
            uint detectMode = DetectionMode.ASF_DETECT_MODE_IMAGE;
            //Video模式下检测脸部的角度优先值
            //int videoDetectFaceOrientPriority = ASF_OrientPriority.ASF_OP_0_HIGHER_EXT;
            //Image模式下检测脸部的角度优先值
            int imageDetectFaceOrientPriority = ASF_OrientPriority.ASF_OP_0_ONLY;//ASF_OP_0_ONLY;
            //人脸在图片中所占比例，如果需要调整检测人脸尺寸请修改此值，有效数值为2-32
            int detectFaceScaleVal = 16;
            //最大需要检测的人脸个数
            int detectFaceMaxNum = 5;
            //引擎初始化时需要初始化的检测功能组合
            int combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_AGE | FaceEngineMask.ASF_GENDER | FaceEngineMask.ASF_FACE3DANGLE;
            //初始化引擎，正常值为0，其他返回值请参考http://ai.arcsoft.com.cn/bbs/forum.php?mod=viewthread&tid=19&_dsign=dbad527e
            retCode = ASFFunctions.ASFInitEngine(detectMode, imageDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pImageEngine);
            Console.WriteLine("InitEngine Result:" + retCode);
            //AppendText((retCode == 0) ? "引擎初始化成功!\n" : string.Format("引擎初始化失败!错误码为:{0}\n", retCode));
            if (retCode != 0)
            {
                //禁用相关功能按钮
            }

            //初始化视频模式下人脸检测引擎
            //uint detectModeVideo = DetectionMode.ASF_DETECT_MODE_VIDEO;
            //int combinedMaskVideo = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION;
            //retCode = ASFFunctions.ASFInitEngine(detectModeVideo, videoDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMaskVideo, ref pVideoEngine);

           // RGB视频专用FR引擎
            detectFaceMaxNum = 1;
            combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_LIVENESS;
            retCode = ASFFunctions.ASFInitEngine(detectMode, imageDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pVideoRGBImageEngine);

            //IR视频专用FR引擎
            //combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_IR_LIVENESS;
            //retCode = ASFFunctions.ASFInitEngine(detectMode, imageDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pVideoIRImageEngine);

        }
        #endregion

        #region 识别获取到的图片
        /// <summary>
        /// 识别获取到的图片
        /// </summary>
        public static void ArcDiscernImg(Image Img)
        {
            //判断引擎是否初始化成功

            IntPtr image1Feature = IntPtr.Zero;

            if (pImageEngine == IntPtr.Zero)
            {
                InitEngines();
            }
            if (Img != null)
            {

                int Width = Img.Width != 640 ? 640 : Img.Width;
                int Height = Img.Height != 480 ? 480 : Img.Height;
                //形参给实参
                Image srcImage = Img;
                if (srcImage.Width > 640 || srcImage.Height > 480)
                {
                    srcImage = ImageUtil.ScaleImage(srcImage, 640, 480);
                }
                if (srcImage == null)
                {
                    LogHelper.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-> " + "图片缩放失败");
                    return;
                }

                //调整图像宽度，需要宽度为4的倍数
                if (srcImage.Width % 4 != 0)
                {
                    srcImage = ImageUtil.ScaleImage(srcImage, srcImage.Width - (srcImage.Width % 4), srcImage.Height);
                }
                //调整图片数据，非常重要
                ImageInfo imageInfo = ImageUtil.ReadBMP(srcImage);
                if (imageInfo == null)
                {
                    LogHelper.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-> " + "调整图片数据失败");
                    return;
                }

                //人脸检测
                ASF_MultiFaceInfo multiFaceInfo = FaceUtil.DetectFace(pImageEngine, imageInfo);
                //年龄检测
                int retCode_Age = -1;
                ASF_AgeInfo ageInfo = FaceUtil.AgeEstimation(pImageEngine, imageInfo, multiFaceInfo, out retCode_Age);
                //性别检测
                int retCode_Gender = -1;
                ASF_GenderInfo genderInfo = FaceUtil.GenderEstimation(pImageEngine, imageInfo, multiFaceInfo, out retCode_Gender);

                //3DAngle检测
                int retCode_3DAngle = -1;
                ASF_Face3DAngle face3DAngleInfo = FaceUtil.Face3DAngleDetection(pImageEngine, imageInfo, multiFaceInfo, out retCode_3DAngle);

                MemoryUtil.Free(imageInfo.imgData);

                if (multiFaceInfo.faceNum < 1)
                {
                    srcImage = ImageUtil.ScaleImage(srcImage, Width, Height);
                    image1Feature = IntPtr.Zero;
                    LogHelper.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-> " + "未检测出人脸");
                    return;
                }

                MRECT temp = new MRECT();
                int ageTemp = 0;
                int genderTemp = 0;
                int rectTemp = 0;

                //标记出检测到的人脸
                for (int i = 0; i < multiFaceInfo.faceNum; i++)
                {
                    MRECT rect = MemoryUtil.PtrToStructure<MRECT>(multiFaceInfo.faceRects + MemoryUtil.SizeOf<MRECT>() * i);
                    int orient = MemoryUtil.PtrToStructure<int>(multiFaceInfo.faceOrients + MemoryUtil.SizeOf<int>() * i);
                    int age = 0;

                    if (retCode_Age != 0)
                    {
                        LogHelper.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-> " + "人脸检测失败");
                    }
                    else
                    {
                        age = MemoryUtil.PtrToStructure<int>(ageInfo.ageArray + MemoryUtil.SizeOf<int>() * i);
                    }

                    int gender = -1;
                    if (retCode_Gender != 0)
                    {
                        LogHelper.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-> " + "性别检测失败");
                    }
                    else
                    {
                        gender = MemoryUtil.PtrToStructure<int>(genderInfo.genderArray + MemoryUtil.SizeOf<int>() * i);
                    }

                    int face3DStatus = -1;
                    float roll = 0f;
                    float pitch = 0f;
                    float yaw = 0f;
                    if (retCode_3DAngle != 0)
                    {
                        LogHelper.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-> " + "3DAngle检测失败");
                    }
                    else
                    {
                        //角度状态 非0表示人脸不可信
                        face3DStatus = MemoryUtil.PtrToStructure<int>(face3DAngleInfo.status + MemoryUtil.SizeOf<int>() * i);
                        //roll为侧倾角，pitch为俯仰角，yaw为偏航角
                        roll = MemoryUtil.PtrToStructure<float>(face3DAngleInfo.roll + MemoryUtil.SizeOf<float>() * i);
                        pitch = MemoryUtil.PtrToStructure<float>(face3DAngleInfo.pitch + MemoryUtil.SizeOf<float>() * i);
                        yaw = MemoryUtil.PtrToStructure<float>(face3DAngleInfo.yaw + MemoryUtil.SizeOf<float>() * i);
                    }

                    int rectWidth = rect.right - rect.left;
                    int rectHeight = rect.bottom - rect.top;

                    //查找最大人脸
                    if (rectWidth * rectHeight > rectTemp)
                    {
                        rectTemp = rectWidth * rectHeight;
                        temp = rect;
                        ageTemp = age;
                        genderTemp = gender;
                    }
                }

                ASF_SingleFaceInfo singleFaceInfo = new ASF_SingleFaceInfo();
                //提取人脸特征
                image1Feature = FaceUtil.ExtractFeature(pImageEngine, srcImage, out singleFaceInfo);

                //读取特征信息转换为结构体
                ASF_FaceFeature FaceFeature = MemoryUtil.PtrToStructure<ASF_FaceFeature>(image1Feature + MemoryUtil.SizeOf<ASF_FaceFeature>() * 0);
                byte[] feature = new byte[FaceFeature.featureSize];  //读取特征到byte[]
                MemoryUtil.Copy(FaceFeature.feature, feature, 0, FaceFeature.featureSize);


                //获取缩放比例
                float scaleRate = ImageUtil.getWidthAndHeight(srcImage.Width, srcImage.Height, Width, Height);
                //缩放图片
                srcImage = ImageUtil.ScaleImage(srcImage, Width, Height);

                //添加标记
                srcImage = ImageUtil.MarkRectAndString(srcImage, (int)(temp.left * scaleRate), (int)(temp.top * scaleRate), (int)(temp.right * scaleRate) - (int)(temp.left * scaleRate), (int)(temp.bottom * scaleRate) - (int)(temp.top * scaleRate), ageTemp, genderTemp, Width);

                //保存在本地
                //srcImage.Save(@"F:\ArcfaceDemo_CSharp_2.2-master\ArcSoftFace\ArcSoftFace\bin\Debug\x86\102p0.png");
                //显示标记后的图像
                //picImageCompare.Image = srcImage;
            }
        }
        #endregion

        private static object lockpar = new object();

        #region 识别年龄和性别
        public static FaceEntity PartFaceAgeandSex(Image Img)
        {
            FaceEntity Face = new FaceEntity();
            IntPtr image1Feature = IntPtr.Zero;
            lock (lockpar)
            {
                try
                {
                    Face.State = -1;
                    int age = 0;  //年龄
                    int gender = -1; //性别
                    ASF_FaceFeature FaceFeature = new ASF_FaceFeature();  //人脸结构体

                    

                    if (pImageEngine == IntPtr.Zero) //判断引擎是否初始化成功
                    {
                        InitEngines();
                    }
                    if (Img != null)
                    {
                        int Width = Img.Width != 640 ? 640 : Img.Width;
                        int Height = Img.Height != 480 ? 480 : Img.Height;
                        //形参给实参
                        Image srcImage = Img;
                        if (srcImage.Width > 1536 || srcImage.Height > 1536)
                        {
                            srcImage = ImageUtil.ScaleImage(srcImage, 1536, 1536);
                        }
                        if (srcImage == null)
                        {
                            LogHelper.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-> " + "图片缩放失败");
                            return Face;
                        }

                        //调整图像宽度，需要宽度为4的倍数
                        if (srcImage.Width % 4 != 0)
                        {
                            srcImage = ImageUtil.ScaleImage(srcImage, srcImage.Width - (srcImage.Width % 4), srcImage.Height);
                        }
                        //调整图片数据，非常重要
                        ImageInfo imageInfo = ImageUtil.ReadBMP(srcImage);
                        if (imageInfo == null)
                        {
                            LogHelper.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-> " + "调整图片数据失败");
                            return Face;
                        }

                        //人脸检测
                        ASF_MultiFaceInfo multiFaceInfo = FaceUtil.DetectFace(pImageEngine, imageInfo);

                        //找最大人脸
                        ASF_SingleFaceInfo maxFace = FaceUtil.GetMaxFace(multiFaceInfo);


                        //年龄检测
                        int retCode_Age = -1;
                        ASF_AgeInfo ageInfo = FaceUtil.AgeEstimation(pImageEngine, imageInfo, multiFaceInfo, out retCode_Age);
                        //性别检测
                        int retCode_Gender = -1;
                        ASF_GenderInfo genderInfo = FaceUtil.GenderEstimation(pImageEngine, imageInfo, multiFaceInfo, out retCode_Gender);

                        ////3DAngle检测
                        //int retCode_3DAngle = -1;
                        //ASF_Face3DAngle face3DAngleInfo = FaceUtil.Face3DAngleDetection(pImageEngine, imageInfo, multiFaceInfo, out retCode_3DAngle);
                        //释放内存
                        MemoryUtil.Free(imageInfo.imgData);
                       
                        //图片中人脸数量
                        Face.FaceNum = multiFaceInfo.faceNum;

                        //图像中是否有人脸或不止一张人脸
                        if (multiFaceInfo.faceNum > 1 || multiFaceInfo.faceNum==0)
                        {
                            return Face;
                        }

                        //MRECT rect = MemoryUtil.PtrToStructure<MRECT>(multiFaceInfo.faceRects + MemoryUtil.SizeOf<MRECT>() * 0);
                        //int orient = MemoryUtil.PtrToStructure<int>(multiFaceInfo.faceOrients + MemoryUtil.SizeOf<int>() * 0);
                        if (retCode_Age != 0)
                        {
                            LogHelper.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-> " + "人脸检测失败");
                        }
                        else
                        {
                            age = MemoryUtil.PtrToStructure<int>(ageInfo.ageArray + MemoryUtil.SizeOf<int>() * 0);
                        }

                        if (retCode_Gender != 0)
                        {
                            LogHelper.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-> " + "性别检测失败");
                        }
                        else
                        {
                            gender = MemoryUtil.PtrToStructure<int>(genderInfo.genderArray + MemoryUtil.SizeOf<int>() * 0);
                        }
                        
                        //提取人脸特征
                        image1Feature = FaceUtil.ExtractFeature(pImageEngine, srcImage, maxFace);

                        //读取特征信息转换为结构体
                        FaceFeature = MemoryUtil.PtrToStructure<ASF_FaceFeature>(image1Feature);
                        

                    }
                    
                    byte[] feature = new byte[FaceFeature.featureSize];
                    MemoryUtil.Copy(FaceFeature.feature, feature, 0, FaceFeature.featureSize);
                    
                    Face.Ages = age;
                    Face.Sex = gender;
                    Face.ImgBase64 = Img.ImageToBase64();
                    Face.State = 0;
                    Face.Feature = feature;
                    MemoryUtil.Free(FaceFeature.feature);
                }
                catch(Exception ex)
                {
                    LogHelper.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-> " + " 人脸信息识别失败");
                    Face.State = -1;
                }
                finally
                {
                    if(Img != null)
                    {
                        Img.Dispose();
                    }
                    if(image1Feature != null)
                    {
                        MemoryUtil.Free(image1Feature);
                    }
                    Thread.Sleep(10);
                    GC.Collect();
                }
            }
            return Face;
        }

        #endregion

        #region 提取人脸特征值
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Img"></param>
        /// <returns>byte[] 人脸特征码,  null 特征码获取失败</returns>
        public static byte[] FaceFeature(Image Img)
        {
            byte[] feature = null;
            IntPtr image1Feature = IntPtr.Zero;
            try
            {
                ASF_FaceFeature FaceFeature = new ASF_FaceFeature();
                //判断引擎是否初始化成功
                if (pImageEngine == IntPtr.Zero)
                {
                    InitEngines();
                }
                if (Img != null)
                {

                    int Width = Img.Width != 640 ? 640 : Img.Width;
                    int Height = Img.Height != 480 ? 480 : Img.Height;
                    //形参给实参
                    Image srcImage = Img;
                    if (srcImage.Width > 640 || srcImage.Height > 480)
                    {
                        srcImage = ImageUtil.ScaleImage(srcImage, 640, 480);
                    }
                    if (srcImage == null)
                    {
                        LogHelper.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-> " + "图片缩放失败");
                        return null;
                    }

                    //调整图像宽度，需要宽度为4的倍数
                    if (srcImage.Width % 4 != 0)
                    {
                        srcImage = ImageUtil.ScaleImage(srcImage, srcImage.Width - (srcImage.Width % 4), srcImage.Height);
                    }

                    //调整图片数据，非常重要
                    ImageInfo imageInfo = ImageUtil.ReadBMP(srcImage);
                    if (imageInfo == null)
                    {
                        LogHelper.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-> " + "调整图片数据失败");
                        return null;
                    }

                    //人脸检测
                    ASF_MultiFaceInfo multiFaceInfo = FaceUtil.DetectFace(pImageEngine, imageInfo);
                    //图像中是否有人脸
                    if (multiFaceInfo.faceNum > 1)
                    {
                        return null;
                    }
                    ASF_SingleFaceInfo singleFaceInfo = new ASF_SingleFaceInfo();
                    //提取人脸特征
                    image1Feature = FaceUtil.ExtractFeature(pImageEngine, srcImage, out singleFaceInfo);
                    //读取特征信息转换为结构体
                    FaceFeature = MemoryUtil.PtrToStructure<ASF_FaceFeature>(image1Feature + MemoryUtil.SizeOf<ASF_FaceFeature>() * 0);
                    feature = new byte[FaceFeature.featureSize];  //读取特征到byte[]
                    MemoryUtil.Copy(FaceFeature.feature, feature, 0, FaceFeature.featureSize);
                }

            }
            catch { }
            return feature;
        }

        #endregion

        #region 两特征码比对

        public static object lockobj = new object();
        /// <summary>
        /// 人脸特征码对比
        /// </summary>
        /// <param name="image1Feature"></param>
        /// <param name="image2Feature"></param>
        /// <returns></returns>
        public static float FaceFeatureCompare(IntPtr image1Feature, IntPtr image2Feature)
        {
            float similarity = 0f;
            lock (lockobj)
            {
                try
                {
                    int result = ASFFunctions.ASFFaceFeatureCompare(pImageEngine, image1Feature, image2Feature, ref similarity);
                    if (result != 0)
                    {
                        LogHelper.WriteError(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "->" + "软虹人脸识别失败，错误代码：" + result);
                    }

                }
                catch (Exception ex)
                {
                    LogHelper.WriteError(ex.ToString());
                }
                finally
                {
                    MemoryUtil.Free(image1Feature);
                    MemoryUtil.Free(image2Feature);
                    GC.Collect();
                }
            }
            return similarity;
        }




        public static float FaceFeatureCompare(string image1Feature, string image2Feature)
        {
            float similarity = 0f;
            lock (lockobj)
            {
                IntPtr imga = image1Feature.Base64ToBytes().ObjectToIntPtr();
                IntPtr imgb = image2Feature.Base64ToBytes().ObjectToIntPtr();
                try
                {
                    int result = ASFFunctions.ASFFaceFeatureCompare(pImageEngine, imga, imgb, ref similarity);
                    if (result != 0)
                    {
                        LogHelper.WriteError(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "->" + "软虹人脸识别失败，错误代码：" + result); ;
                    }

                }
                catch (Exception ex)
                {
                    LogHelper.WriteError(ex.ToString());
                }
                finally
                {
                    MemoryUtil.Free(imga);
                    MemoryUtil.Free(imgb);
                    GC.Collect();
                }
            }
            return similarity;
        }








        public static float FaceCompare(string nowimg,string image2Feature)
        {
            float similarity = 0f;
            Bitmap bitmap = null;
            IntPtr ImgB = IntPtr.Zero;
            IntPtr feature = IntPtr.Zero;
            try
            {
             
                bitmap = nowimg.ObjectToBitmap();
                //检测人脸，得到Rect框
                ASF_MultiFaceInfo multiFaceInfo = FaceUtil.DetectFace(pVideoRGBImageEngine, bitmap);

                if (multiFaceInfo.faceNum == 0) //没有人脸
                {
                    return similarity;
                }
                //得到最大人脸
                ASF_SingleFaceInfo maxFace = FaceUtil.GetMaxFace(multiFaceInfo);

                
                //提取人脸特征
                feature = FaceUtil.ExtractFeature(pVideoRGBImageEngine, bitmap, maxFace);
                ImgB = image2Feature.Base64ToBytes().ObjectToIntPtr();

                int result = ASFFunctions.ASFFaceFeatureCompare(pVideoRGBImageEngine, feature, ImgB, ref similarity);
                if (result != 0)
                {
                    LogHelper.WriteError(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "->" + "软虹人脸识别失败，错误代码：" + result); ;
                }
         
            }
            catch { }
            finally
            {
               
                bitmap.Dispose();
                MemoryUtil.Free(ImgB);
                MemoryUtil.Free(feature);
            }
            return similarity;
        }


        #endregion
        
    }
}
