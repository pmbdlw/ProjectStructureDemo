using System;
using System.Globalization;
namespace WebApi.Extensions
{
    public static class GuidExtentions
    {
        /// <summary>
        /// 转为有序的GUID
        /// 注：长度为50字符
        /// </summary>
        /// <param name="guid">新的GUID</param>
        /// <returns></returns>
        public static string ToSequentialGuid(this Guid guidExt)
        {
            var timeStr = (DateTime.Now.Ticks / 10000).ToString("x8", CultureInfo.CurrentCulture);
            var newGuid = $"{timeStr.PadLeft(13, '0')}-{guidExt}";
            return newGuid;
        }
    }
}
