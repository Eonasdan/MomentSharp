namespace MomentSharp.Globalization.Languages;

/// <summary>
///  Localization for English (US,American) (En-US)
/// </summary>
public class EnUs : ILocalize
{
    /// <summary>
    /// English localization implementation constructor
    /// </summary>
    public EnUs()
    {
        LongDateFormat = new LongDateFormat
        {
            Lt = "h:mm tt",
            Lts = "h:mm:s tt",
            L = "M/d/yyyy",
            LL = "MMMM d, yyyy"
        };

        LongDateFormat.LLL = $"MMMM d, yyyy {LongDateFormat.Lt}";
        LongDateFormat.LLLL = $"dddd, MMMM d, yyyy {LongDateFormat.Lt}";
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
            Calendar.SameDay => $"Today at {dateTime.ToString(LongDateFormat.Lt)}",
            Calendar.NextDay => $"Tomorrow at {dateTime.ToString(LongDateFormat.Lt)}",
            Calendar.NextWeek => $"{dateTime:dddd} at {dateTime.ToString(LongDateFormat.Lt)}",
            Calendar.LastDay => $"Yesterday at {dateTime.ToString(LongDateFormat.Lt)}",
            Calendar.LastWeek => $"{dateTime:dddd} at {dateTime.ToString(LongDateFormat.Lt)}",
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
            RelativeTime.Seconds => "a few seconds",
            RelativeTime.Minute => "a minute",
            RelativeTime.Minutes => $"{number} minutes",
            RelativeTime.Hour => "an hour",
            RelativeTime.Hours => $"{number} hours",
            RelativeTime.Day => "a day",
            RelativeTime.Days => $"{number} days",
            RelativeTime.Month => "a month",
            RelativeTime.Months => $"{number} months",
            RelativeTime.Year => "a year",
            RelativeTime.Years => $"{number} years",
            _ => string.Empty
        };
        return !showSuffix ? results : string.Format(isFuture ? "in {0}" : "{0} ago", results);
    }
}