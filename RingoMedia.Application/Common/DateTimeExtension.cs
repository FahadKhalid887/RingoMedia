using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingoMedia.Application.Common
{
    internal static class DateTimeExtension
    {
        public static DateTime ConvertUtcToTimeZone(this DateTime dateTime)
        {
            string timeZoneId = TimeZone.CurrentTimeZone.StandardName;

            // Get the time zone information for the specified time zone
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            // Convert the given DateTime to the target time zone
            DateTime targetDateTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, targetTimeZone);

            return targetDateTime;
        }
    }
}
