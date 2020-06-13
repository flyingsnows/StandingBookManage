using Emgu.CV;
using Emgu.CV.Structure;
using Luxand;
using LYSoft.ArcCore;
using LYSoft.ArcCore.Entity;
using LYSoft.Center;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace StreamingServer
{
    public class VideoHelper
    {
        private ImageStreamingServer imageServer;

        private Image<Bgr, byte> m_Bitmap;  //当前摄像头前的图片
        
        int cameraHandle = 0;

        public delegate void GetFaceinfoChangingEventHandler(Bitmap map);

        //获取到人脸的事件
        public event GetFaceinfoChangingEventHandler FaceEventHandler;


        private object lockimg = new object();

        public VideoHelper()
        {
            try
            {
                if (FSDK.FSDKE_OK != FSDK.ActivateLibrary("gyYgVWQTSzjiuGB/hH8dKgg0QrrIuhoHdfUCzD9rY+vru3WRZsaezTX6YWj9osdI/cmxY1NSdLkyWuugMPCxUG7/xNLegHLeaUpzVyKpDkaWL8tJIUsIL7xv9bhmgifPbAyTDuxF3VGxXmHkv/L/MStf9kdXV/A1vVvT93QC4vQ="))
                {
                    return;
                }
                FSDK.InitializeLibrary();
                FSDKCam.InitializeCapturing();
                string[] cameraList;
                int count;
                string cameraName = ""; //摄像头名称
                FSDKCam.GetCameraList(out cameraList, out count);

                if (0 == count)
                {
                    return;
                }
                if (cameraList.Length > 1)
                {
                    cameraName = cameraList[1];
                }
                else
                {
                    cameraName = cameraList[0];
                }
                FSDKCam.VideoFormatInfo[] formatList;
                FSDKCam.GetVideoFormatList(ref cameraName, out formatList, out count);
                int r = FSDKCam.OpenVideoCamera(ref cameraName, ref cameraHandle);
                if (r != FSDK.FSDKE_OK)
                {
                    return;
                }

                Thread th = new Thread(this.GetImages);
                th.IsBackground = true;
                th.Start();

                Thread th1 = new Thread(GuishBitmap);
                th1.IsBackground = true;
                th1.Start();
            }
            catch
            {
            }
        }

        //锁
        private object imglock = new object();
        public void GetImages()
        {
            while (true)
            {
                try
                {
                    lock (imglock)
                    {
                        Int32 imageHandle = 0;
                        if (FSDK.FSDKE_OK != FSDKCam.GrabFrame(cameraHandle, ref imageHandle)) // grab the current frame from the camera
                        {
                            return;
                        }
                       
                        FSDK.CImage image = new FSDK.CImage(imageHandle);
                        using (Bitmap temp = new Bitmap(image.ToCLRImage()))
                        {
                            m_Bitmap = new Image<Bgr, byte>(temp);
                        }

                        FSDK.FreeImage(image.ImageHandle);
                        image.Dispose();
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    GC.Collect();
                    Thread.Sleep(10);
                }
            }
          
        }
        
        /// <summary>
        /// 启动推流到html
        /// </summary>
        public void StartCamera()
        {
            imageServer = new ImageStreamingServer(this.LinkImages());
            imageServer.Start(12018);
        }
        

        private IEnumerable<byte[]> LinkImages()
        {
            while (true)
            {
                lock (imglock)
                {
                    yield return m_Bitmap.ToJpegData();
                }
                Thread.Sleep(10);
            }
            yield break;
        }

        /// <summary>
        /// 保存一张图片在本地
        /// </summary>
        public void SaveImage()
        {
            string path = Path.Combine(MyApps.Temppath, DateTime.Now.ToString("yyyyMMddHHmmss")+ ".png");
            if (IOHelper.FileExist(path))  //删除存在的图片
            {
                IOHelper.DeleteFile(path);
            }
            using (Bitmap temp = new Bitmap(m_Bitmap.ToBitmap()))
            {
                Bitmap img =null;
                if (temp.Width !=640 && temp.Height != 480) //转换图片大小
                {
                    img = temp.ResizeImage(new Size { Width = 640, Height = 480 });
                }
                else   
                {
                    img = temp;
                }
                img.Save(path, ImageFormat.Png);
            }
        }

        public Bitmap GetImage()
        {
            Bitmap result = null;
            lock (imglock)
            {
                try
                {
                    Bitmap temp = new Bitmap(m_Bitmap.ToBitmap());
                    if (temp.Width != 640 && temp.Height != 480) //转换图片大小
                    {
                        result = temp.ResizeImage(new Size { Width = 640, Height = 480 });
                    }
                    else
                    {
                        result = temp;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteError(ex.ToString());
                }
            }
            return result;
        }

        public void GuishBitmap()
        {
            while (true)
            {
                try
                {
                    if (m_Bitmap != null)
                    {
                        Bitmap temp = new Bitmap(m_Bitmap.ToBitmap());
                        FaceEventHandler?.Invoke(temp);
                        temp.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteError(ex.ToString());
                }
                finally
                {
                    Thread.Sleep(10);
                }
            }
        }


    }
}
