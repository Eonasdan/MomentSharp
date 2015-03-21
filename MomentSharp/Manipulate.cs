using System;
using System.Globalization;
using Pure = JetBrains.Annotations.PureAttribute;

namespace MomentSharp
{
    public static class Manipulate
    {
        /// <summary>
        /// Get the start of <paramref name="part"/> from DateTime.UtcNow. E.g. StartOf(DateTimeParts.Year) 
        /// return a new <seealso cref="System.DateTime"/> at the start of the current year.
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public static DateTime StartOf(DateTimeParts part)
        {
            return StartOf(DateTime.UtcNow, part);
        }

        /// <summary>
        ///  Get the start of this <paramref name="dateTime"/> at <paramref name="part"/>. E.g. DateTime.Now.StartOf(DateTimeParts.Year) 
        /// return a new <seealso cref="System.DateTime"/> at the start of the current year.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="part"></param>
        /// <returns></returns>
        public static DateTime StartOf(this DateTime dateTime, DateTimeParts part)
        {
            switch (part)
            {
                case DateTimeParts.Year:
                    return new DateTime(dateTime.Year, 1, 1);
                case DateTimeParts.Month:
                    return new DateTime(dateTime.Year, dateTime.Month, 1);
                case DateTimeParts.Quarter:
                    return new DateTime(dateTime.Year, ((dateTime.Month - 1) / 3) * 3 + 1, 1);
                case DateTimeParts.Week:
                    dateTime = dateTime.StartOf(DateTimeParts.Day);
                    var ci = CultureInfo.CurrentCulture;
                    var first = (int)ci.DateTimeFormat.FirstDayOfWeek;
                    var current = (int)dateTime.DayOfWeek;
                    return first <= current ?
                      dateTime.AddDays(-1 * (current - first)) :
                      dateTime.AddDays(first - current - 7);
                case DateTimeParts.Day:
                    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, 0);
                case DateTimeParts.Hour:
                    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0, 0);
                case DateTimeParts.Minute:
                    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0, 0);
                case DateTimeParts.Second:
                    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, 0);
                default:
                    throw new ArgumentException("No valid part was provided", "part");
            }
        }

        /// <summary>
        /// Get the end of <paramref name="part"/> from DateTime.UtcNow. E.g. EndOf(DateTimeParts.Year) 
        /// return a new <seealso cref="System.DateTime"/> at the end of the current year.
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public static DateTime EndOf(DateTimeParts part)
        {
            return EndOf(DateTime.UtcNow, part);
        }

        /// <summary>
        /// Get the end of <paramref name="part"/> from DateTime.UtcNow. E.g. EndOf(DateTimeParts.Year) 
        /// return a new <seealso cref="System.DateTime"/> at the end of the current year.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="part"></param>
        /// <returns></returns>
        public static DateTime EndOf(this DateTime dateTime, DateTimeParts part)
        {
            switch (part)
            {
                case DateTimeParts.Year:
                    return StartOf(dateTime,DateTimeParts.Year).AddYears(1).AddSeconds(-1);
                case DateTimeParts.Month:
                    return StartOf(dateTime, DateTimeParts.Month).AddMonths(1).AddSeconds(-1);
                case DateTimeParts.Quarter:
                    return StartOf(dateTime, DateTimeParts.Quarter).AddMonths(3).AddSeconds(-1);
                case DateTimeParts.Week:
                    return StartOf(dateTime, DateTimeParts.Week).AddDays(7).AddSeconds(-1);
                case DateTimeParts.Day:
                    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59, 0);
                case DateTimeParts.Hour:
                    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 59, 59, 0);
                case DateTimeParts.Minute:
                    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 59, 0);
                case DateTimeParts.Second:
                    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, 999);
                default:
                    throw new ArgumentException("No valid part was provided", "part");
            }
        }
        
        /// <summary>
        /// Set the Year of this <paramref name="dateTime"/> to <paramref name="year"/>, leaving all other all other parts the same.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        [Pure]
        public static DateTime SetYear(this DateTime dateTime, int year)
        {
            return new DateTime(year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
        }

        /// <summary>
        /// Set the Year of this <paramref name="dateTime"/> to <paramref name="month"/>, leaving all other all other parts the same.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="month"></param>
        /// <param name="bubble">If set to true, it will bubble up the next part. E.g. 90 seconds becomes 1:30 minutes</param>
        /// <returns></returns>
        [Pure]
        public static DateTime SetMonth(this DateTime dateTime, int month, bool bubble = false)
        {
            if (!bubble)
            return new DateTime(dateTime.Year, month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);

            var moment = dateTime.Moment();
            moment.Month = month;

            return moment.DateTime(true);
        }
        
        /// <summary>
        /// Set the Year of this <paramref name="dateTime"/> to <paramref name="day"/>, leaving all other all other parts the same.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="day"></param>
        /// <param name="bubble">If set to true, it will bubble up the next part. E.g. 90 seconds becomes 1:30 minutes</param>
        /// <returns></returns>
        [Pure]
        public static DateTime SetDay(this DateTime dateTime, int day, bool bubble = false)
        {
            if (!bubble)
                return new DateTime(dateTime.Year, dateTime.Month, day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);

            var moment = dateTime.Moment();
            moment.Day = day;

            return moment.DateTime(true);
        }

        /// <summary>
        /// Set the Year of this <paramref name="dateTime"/> to <paramref name="hour"/>, leaving all other all other parts the same.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="hour"></param>
        /// <param name="bubble">If set to true, it will bubble up the next part. E.g. 90 seconds becomes 1:30 minutes</param>
        /// <returns></returns>
        [Pure]
        public static DateTime SetHour(this DateTime dateTime, int hour, bool bubble = false)
        {
            if (!bubble)
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);

            var moment = dateTime.Moment();
            moment.Hour = hour;

            return moment.DateTime(true);
        }

        /// <summary>
        /// Set the Year of this <paramref name="dateTime"/> to <paramref name="minute"/>, leaving all other all other parts the same.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="minute"></param>
        /// <param name="bubble">If set to true, it will bubble up the next part. E.g. 90 seconds becomes 1:30 minutes</param>
        /// <returns></returns>
        [Pure]
        public static DateTime SetMinute(this DateTime dateTime, int minute, bool bubble = false)
        {
            if (!bubble)
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, minute, dateTime.Second, dateTime.Millisecond);

            var moment = dateTime.Moment();
            moment.Minute = minute;

            return moment.DateTime(true);
        }

        /// <summary>
        /// Set the Year of this <paramref name="dateTime"/> to <paramref name="second"/>, leaving all other all other parts the same.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="second"></param>
        /// <param name="bubble">If set to true, it will bubble up the next part. E.g. 90 seconds becomes 1:30 minutes</param>
        /// <returns></returns>
        [Pure]
        public static DateTime SetSecond(this DateTime dateTime, int second, bool bubble = false)
        {
            if (!bubble)
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, second, dateTime.Millisecond);

            var moment = dateTime.Moment();
            moment.Second = second;

            return moment.DateTime(true);
        }

        /// <summary>
        /// Set the Year of this <paramref name="dateTime"/> to <paramref name="millisecond"/>, leaving all other all other parts the same.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="millisecond"></param>
        /// <param name="bubble">If set to true, it will bubble up the next part. E.g. 90 seconds becomes 1:30 minutes</param>
        /// <returns></returns>
        [Pure]
        public static DateTime SetMillisecond(this DateTime dateTime, int millisecond, bool bubble = false)
        {
            if (!bubble)
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, millisecond);

            var moment = dateTime.Moment();
            moment.Millisecond = millisecond;

            return moment.DateTime(true);
        }

    }
}
