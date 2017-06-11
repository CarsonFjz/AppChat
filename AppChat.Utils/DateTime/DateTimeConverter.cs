using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Utils
{
    public class DateTimeConverter
    {
        public static DateTime IntToDateTime(int timestamp)
        {
            return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds(timestamp);
        }

        public static long DateTimeToInt(DateTime datetime)
        {
            return (datetime.ToUniversalTime().Ticks - new DateTime(1970, 1, 1).Ticks) / 10000000;
        }
    }
}
