using System;

namespace MomentSharp
{
    public static class Parse
    {
        /// <summary>
        /// Converts javascript/Unix timestamp to DateTime
        /// </summary>
        /// <param name="unixTimeStamp">TimeStamp in seconds</param>
        /// <returns>DateTime in UTC</returns>
        public static DateTime UnixToDateTime(this double unixTimeStamp)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dateTime;
        }

        /// <summary>
        /// Converts javascript/Unix timestamp to DateTime
        /// </summary>
        /// <param name="unixTimeStamp">TimeStamp in seconds</param>
        /// <returns>DateTime in UTC</returns>
        public static DateTime UnixToDateTime(this int unixTimeStamp)
        {
            return UnixToDateTime((double)unixTimeStamp);
        }


        /// <summary>
        /// Convert this <paramref name="moment"/> object to a <seealso cref="System.DateTime"/>
        /// </summary>
        /// <param name="moment">A Moment Object</param>
        /// <param name="bubble">Whether or not to bubble <paramref name="moment"/> to the next part. E.g. 90 seconds to 1 minute and 30 seconds. 
        /// If false, will throw exception given the example.</param>
        /// <returns></returns>
        public static DateTime DateTime(this Moment moment, bool bubble = false)
        {
            if (!bubble)
                return new DateTime(moment.Year, moment.Month, moment.Day, moment.Hour, moment.Minute, moment.Second,
                    moment.Millisecond);

            Bubble.Millisecond(ref moment);
            Bubble.Second(ref moment);
            Bubble.Minute(ref moment);
            Bubble.Hour(ref moment);
            Bubble.Day(ref moment);
            Bubble.Month(ref moment);
            return new DateTime(moment.Year, moment.Month, moment.Day, moment.Hour, moment.Minute, moment.Second, moment.Millisecond);
        }

        /// <summary>
        /// Converts this <paramref name="dateTime"/> to a <seealso cref="Moment"/> object
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static Moment Moment(this DateTime dateTime)
        {
            return new Moment
            {
                Year = dateTime.Year,
                Month = dateTime.Month,
                Day = dateTime.Day,
                Hour = dateTime.Hour,
                Minute = dateTime.Minute,
                Second = dateTime.Second,
                Millisecond = dateTime.Millisecond
            };
        }

        /// <summary>
        /// Converts this <paramref name="dateTime"/> to UTC
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="fromTimeZoneId">For valid parameters see TimeZoneInfo.GetSystemTimeZones()</param>
        /// <returns></returns>
        public static DateTime ToUTC(this DateTime dateTime, string fromTimeZoneId)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, fromTimeZoneId, TimeZoneInfo.Utc.Id);
        }

        /// <summary>
        /// Converts this <paramref name="dateTime"/> UTC time to another time zone
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="toTimeZoneId">For valid parameters see TimeZoneInfo.GetSystemTimeZones()</param>
        /// <returns></returns>
        public static DateTime ToTimeZone(this DateTime dateTime, string toTimeZoneId)
        {
            dateTime = System.DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);

            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, toTimeZoneId);
        }
    }
}