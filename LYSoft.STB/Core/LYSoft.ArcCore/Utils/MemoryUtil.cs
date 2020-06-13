using ArcSoftFace.SDKModels;
using System;
using System.Runtime.InteropServices;

namespace ArcSoftFace.Utils
{
    public class MemoryUtil
    {
        /// <summary>
        /// 申请内存
        /// </summary>
        /// <param name="len">内存长度(单位:字节)</param>
        /// <returns>内存首地址</returns>
        public static IntPtr Malloc(int len)
        {
            return Marshal.AllocHGlobal(len);
        }

        /// <summary>
        /// 释放ptr托管的内存
        /// </summary>
        /// <param name="ptr">托管指针</param>
        public static void Free(IntPtr ptr)
        {
            Marshal.FreeHGlobal(ptr);
        }

        /// <summary>
        /// 将字节数组的内容拷贝到托管内存中
        /// </summary>
        /// <param name="source">元数据</param>
        /// <param name="startIndex">元数据拷贝起始位置</param>
        /// <param name="destination">托管内存</param>
        /// <param name="length">拷贝长度</param>
        public static void Copy(byte[] source, int startIndex, IntPtr destination, int length)
        {
            Marshal.Copy(source, startIndex, destination, length);
        }

        /// <summary>
        /// 将托管内存的内容拷贝到字节数组中
        /// </summary>
        /// <param name="source">托管内存</param>
        /// <param name="destination">目标字节数组</param>
        /// <param name="startIndex">拷贝起始位置</param>
        /// <param name="length">拷贝长度</param>
        public static void Copy(IntPtr source, byte[] destination, int startIndex, int length)
        {
            Marshal.Copy(source, destination, startIndex, length);
        }

        /// <summary>
        /// 将ptr托管的内存转化为结构体对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="ptr">托管指针</param>
        /// <returns>转化后的对象</returns>
        public static T PtrToStructure<T>(IntPtr ptr)
        {
            // return Marshal.PtrToStructure<T>(ptr); LY
            return (T)Marshal.PtrToStructure(ptr,typeof(T));
        }

        /// <summary>
        /// 将结构体对象复制到ptr托管的内存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="ptr"></param>
        public static void StructureToPtr<T>(T t,IntPtr ptr) {
            Marshal.StructureToPtr(t,ptr,false);
        }

        /// <summary>
        /// 获取类型的大小
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>类型的大小</returns>
        public static int SizeOf<T>()
        {
            T t = default(T);
            //return Marshal.SizeOf(T);
            return Marshal.SizeOf(t);  //LY
           
        }
    }

    /// <summary>
    /// 扩展方法
    /// </summary>
    public static partial class ArcPtrHelper
    {
        private static object ptrlock = new object();
        /// <summary>
        /// 将特征码byte[]封装成结构ASF_FaceFeature，再将此结构封装成指针IntPtr
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public static IntPtr ObjectToIntPtr(this byte[] feature)
        {
            lock (ptrlock)
            {
                IntPtr pLocalFeature = IntPtr.Zero;
                ASF_FaceFeature localFeature = new ASF_FaceFeature();
                try
                {
                    localFeature.feature = MemoryUtil.Malloc(feature.Length);
                    MemoryUtil.Copy(feature, 0, localFeature.feature, feature.Length);
                    localFeature.featureSize = feature.Length;
                    pLocalFeature = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_FaceFeature>());
                    MemoryUtil.StructureToPtr(localFeature, pLocalFeature);
                }
                catch { }
                finally
                {
                    MemoryUtil.Free(localFeature.feature);
                }
                return pLocalFeature;
            }

        }


    }
}
