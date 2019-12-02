using System;
using System.Collections.Generic;
using System.Text;

namespace LeoProject.Infrastructure.Helpers
{
    public class DateTimeHelper
    {
        /// <summary>
        /// 将10位时间戳转时间
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static long ToUnixTimestamp(DateTime target)
        {
            return Convert.ToInt64((TimeZoneInfo.ConvertTimeToUtc(target) -
                                    new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
        }
    }
}
