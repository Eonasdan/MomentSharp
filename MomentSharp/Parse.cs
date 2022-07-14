using JetBrains.Annotations;

namespace MomentSharp;

/// <summary>
/// Emulates http://momentjs.com/docs/#/parsing/
/// </summary>
[PublicAPI]
public static class Parse
{
    /// <summary>
    ///     Converts javascript/Unix timestamp to DateTime
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
    ///     Converts javascript/Unix timestamp to DateTime
    /// </summary>
    /// <param name="unixTimeStamp">TimeStamp in seconds</param>
    /// <returns>DateTime in UTC</returns>
    public static DateTime UnixToDateTime(this int unixTimeStamp)
    {
        return UnixToDateTime((double) unixTimeStamp);
    }

    /// <summary>
    /// Convert this <paramref name="dateTime" /> object to LocalTime <see cref="System.DateTime" />
    /// </summary>
    /// <param name="dateTime">A Moment Object</param>
    /// <returns>DateTime</returns>
    public static DateTime LocalTime(this DateTime dateTime)
    {
        return dateTime.ToLocalTime();
    }

    /// <summary>
    ///     Converts this <paramref name="dateTime" /> to UTC
    /// </summary>
    /// <param name="dateTime">this DateTime</param>
    /// <param name="fromTimeZoneId">For valid parameters see TimeZoneInfo.GetSystemTimeZones()</param>
    /// <returns><see cref="DateTime"/></returns>
    public static DateTime ToUTC(this DateTime dateTime, string fromTimeZoneId)
    {
        return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, fromTimeZoneId, TimeZoneInfo.Utc.Id);
    }

    /// <summary>
    ///     Converts this <paramref name="dateTime" /> UTC time to another time zone
    /// </summary>
    /// <param name="dateTime">this DateTime</param>
    /// <param name="toTimeZoneId">For valid parameters see TimeZoneInfo.GetSystemTimeZones()</param>
    /// <returns><see cref="DateTime"/></returns>
    public static DateTime ToTimeZone(this DateTime dateTime, string toTimeZoneId)
    {
        dateTime = System.DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);

        return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, toTimeZoneId);
    }
}