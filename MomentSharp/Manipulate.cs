using System.Globalization;
using JetBrains.Annotations;

namespace MomentSharp;

/// <summary>
/// Emulating http://momentjs.com/docs/#/manipulating/
/// </summary>
[PublicAPI]
public static class Manipulate
{
    /// <summary>
    ///     Get the start of <paramref name="part" /> from DateTime.UtcNow. E.g. StartOf(DateTimeParts.Year)
    ///     return a new <see cref="System.DateTime" /> at the start of the current year.
    /// </summary>
    /// <param name="part"><see cref="DateTimeParts"/></param>
    /// <returns>DateTime at the start of give <paramref name="part"/></returns>
    public static DateTime StartOf(DateTimeParts part)
    {
        return StartOf(DateTime.UtcNow, part);
    }

    /// <summary>
    ///     Get the start of this <paramref name="dateTime" /> at <paramref name="part" />. E.g.
    ///     DateTime.Now.StartOf(DateTimeParts.Year)
    ///     return a new <see cref="System.DateTime" /> at the start of the current year.
    /// </summary>
    /// <param name="dateTime">this DateTime</param>
    /// <param name="part"><see cref="DateTimeParts"/></param>
    /// <returns>DateTime at the start of give <paramref name="part"/></returns>
    public static DateTime StartOf(this DateTime dateTime, DateTimeParts part)
    {
        switch (part)
        {
            case DateTimeParts.Year:
                return new DateTime(dateTime.Year, 1, 1);
            case DateTimeParts.Month:
                return new DateTime(dateTime.Year, dateTime.Month, 1);
            case DateTimeParts.Quarter:
                return new DateTime(dateTime.Year, (dateTime.Month - 1) / 3 * 3 + 1, 1);
            case DateTimeParts.Week:
                dateTime = dateTime.StartOf(DateTimeParts.Day);
                var ci = CultureInfo.CurrentCulture;
                var first = (int)ci.DateTimeFormat.FirstDayOfWeek;
                var current = (int)dateTime.DayOfWeek;
                return first <= current
                    ? dateTime.AddDays(-1 * (current - first))
                    : dateTime.AddDays(first - current - 7);
            case DateTimeParts.Day:
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, 0);
            case DateTimeParts.Hour:
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0, 0);
            case DateTimeParts.Minute:
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0,
                    0);
            case DateTimeParts.Second:
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute,
                    dateTime.Second, 0);
            case DateTimeParts.Millisecond:
            case DateTimeParts.None:
            default:
                throw new ArgumentException("No valid part was provided", nameof(part));
        }
    }

    /// <summary>
    ///     Get the end of <paramref name="part" /> from DateTime.UtcNow. E.g. EndOf(DateTimeParts.Year)
    ///     return a new <see cref="System.DateTime" /> at the end of the current year.
    /// </summary>
    /// <param name="part"><see cref="DateTimeParts"/></param>
    /// <returns>DateTime at the end of give <paramref name="part"/></returns>
    public static DateTime EndOf(DateTimeParts part)
    {
        return EndOf(DateTime.UtcNow, part);
    }

    /// <summary>
    ///     Get the end of <paramref name="part" /> from DateTime.UtcNow. E.g. EndOf(DateTimeParts.Year)
    ///     return a new <see cref="System.DateTime" /> at the end of the current year.
    /// </summary>
    /// <param name="dateTime">this DateTime</param>
    /// <param name="part"><see cref="DateTimeParts"/></param>
    /// <returns>DateTime at the end of give <paramref name="part"/></returns>
    public static DateTime EndOf(this DateTime dateTime, DateTimeParts part)
    {
        return part switch
        {
            DateTimeParts.Year => StartOf(dateTime, DateTimeParts.Year).AddYears(1).AddSeconds(-1),
            DateTimeParts.Month => StartOf(dateTime, DateTimeParts.Month).AddMonths(1).AddSeconds(-1),
            DateTimeParts.Quarter => StartOf(dateTime, DateTimeParts.Quarter).AddMonths(3).AddSeconds(-1),
            DateTimeParts.Week => StartOf(dateTime, DateTimeParts.Week).AddDays(7).AddSeconds(-1),
            DateTimeParts.Day => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59, 0),
            DateTimeParts.Hour => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 59, 59,
                0),
            DateTimeParts.Minute => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour,
                dateTime.Minute, 59, 0),
            DateTimeParts.Second => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour,
                dateTime.Minute, dateTime.Second, 999),
            _ => throw new ArgumentException("No valid part was provided", nameof(part))
        };
    }
}