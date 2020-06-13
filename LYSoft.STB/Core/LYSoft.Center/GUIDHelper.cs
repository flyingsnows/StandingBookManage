using System;
using System.Collections.Generic;
using System.Text;

namespace Elane.App.Util.Common
{
    public class GUIDHelper
    {
        /// <summary>
        /// GUID字符大写
        /// </summary>
        /// <returns></returns>
        public static string GetGuidUper()
        {
            string result = string.Empty;
            Guid gid = Guid.NewGuid();
            result = gid.ToString();
            result = result.ToUpper();
            return result;
        }

        /// <summary>
        /// GUID字母小写
        /// </summary>
        /// <returns></returns>
        public static string GetGuidLower()
        {
            string result = string.Empty;
            Guid gid = Guid.NewGuid();
            result = gid.ToString();
            result = result.ToLower();
            return result;
        }

        /// <summary>
        /// GUID字母大写无-
        /// </summary>
        /// <returns></returns>
        public static string GetNOGuidUper()
        {
            string result = string.Empty;
            Guid gid = Guid.NewGuid();
            result = gid.ToString();
            result = result.Trim('-');
            result = result.ToUpper();
            return result;
        }

        /// <summary>
        /// GUID字母小写无-
        /// </summary>
        /// <returns></returns>
        public static string GetNOGuidLower()
        {
            string result = string.Empty;
            Guid gid = Guid.NewGuid();
            result = gid.ToString();
            result = result.Trim('-');
            result = result.ToLower();
            return result;
        }
    }
}
