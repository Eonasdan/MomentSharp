namespace MomentSharp.Globalization.Languages;

/// <summary>
///  Localization for Chinese (Zh-Cn)
/// </summary>
public class ZhCn : ILocalize
{
    /// <summary>
    /// Chinese localization implementation constructor
    /// </summary>
    public ZhCn()
    {
        LongDateFormat = new LongDateFormat
        {
            Lt = "HH:mm",
            Lts = "HH:mm:ss",
            L = "yyyy-MM-dd",
            LL = "yyyy年MMMdd日"
        };

        LongDateFormat.LLL = $"yyyy年MMMdd日 {LongDateFormat.Lt}";
        LongDateFormat.LLLL = $"yyyy年MMMdd日 dddd {LongDateFormat.Lt}";
    }

    /// <summary>
    /// Localized short hand format strings. See http://momentjs.com/docs/#localized-formats
    /// </summary>
    public LongDateFormat LongDateFormat { get; set; }


    /// <summary>
    /// Localized <see cref="Calendar"/> parts for <paramref name="dateTime"/>
    /// </summary>
    /// <param name="calendar">Calendar Part</param>
    /// <param name="dateTime">DateTime to use in format string</param>
    /// <returns>Localized string e.g. Today at 9:00am</returns>
    public string Translate(Calendar calendar, DateTime dateTime)
    {
        return calendar switch
        {
            Calendar.SameDay => $"今天 {dateTime.ToString(LongDateFormat.Lt)}",
            Calendar.NextDay => $"明天 {dateTime.ToString(LongDateFormat.Lt)}",
            Calendar.NextWeek => $"{dateTime:dddd} {dateTime.ToString(LongDateFormat.Lt)}",
            Calendar.LastDay => $"昨天 {dateTime.ToString(LongDateFormat.Lt)}",
            Calendar.LastWeek => $"{dateTime:dddd} {dateTime.ToString(LongDateFormat.Lt)}",
            Calendar.SameElse => dateTime.ToString(LongDateFormat.L),
            _ => ""
        };
    }


    /// <summary>
    /// Localize <see cref="RelativeTime"/>. This is meant to emulate how MomentJs allows localization of RelativeTime
    /// </summary>
    /// <param name="relativeTime"><see cref="RelativeTime"/></param>
    /// <param name="number">Difference amount</param>
    /// <param name="showSuffix">Should suffix? e.g. "ago"</param>
    /// <param name="isFuture">Difference is in the future or not. e.g. Yesterday vs Tomorrow</param>
    /// <returns>Localized relative time e.g.: 5 seconds ago</returns>
    public string Translate(RelativeTime relativeTime, int number, bool showSuffix, bool isFuture)
    {
        var results = relativeTime switch
        {
            RelativeTime.Seconds => "几秒",
            RelativeTime.Minute => "1 分钟",
            RelativeTime.Minutes => $"{number} 分钟",
            RelativeTime.Hour => "1 小时",
            RelativeTime.Hours => $"{number} 小时",
            RelativeTime.Day => "1 天",
            RelativeTime.Days => $"{number} 天",
            RelativeTime.Month => "1 个月",
            RelativeTime.Months => $"{number} 个月",
            RelativeTime.Year => "1 年",
            RelativeTime.Years => $"{number} 年",
            _ => string.Empty
        };
        return !showSuffix ? results : string.Format(isFuture ? "{0}内" : "{0}前", results);
    }
}