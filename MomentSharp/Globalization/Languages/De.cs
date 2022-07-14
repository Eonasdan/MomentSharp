namespace MomentSharp.Globalization.Languages;

/// <summary>
/// Localization for German (De)
/// </summary>
public class De : ILocalize
{
    /// <summary>
    /// German localization implementation constructor
    /// </summary>
    public De()
    {
        LongDateFormat = new LongDateFormat
        {
            Lt = "HH:mm",
            Lts = "HH:mm:ss",
            L = "dd.MM.yyyy",
            LL = "d. MMMM yyyy"
        };

        LongDateFormat.LLL = $"d. MMMM yyyy {LongDateFormat.Lt}";
        LongDateFormat.LLLL = $"dddd, d. MMMM yyyy {LongDateFormat.Lt}";
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
            Calendar.SameDay => $"Heute um {dateTime.ToString(LongDateFormat.Lt)}",
            Calendar.NextDay => $"Morgen um {dateTime.ToString(LongDateFormat.Lt)}",
            Calendar.NextWeek => $"{dateTime:dddd} um {dateTime.ToString(LongDateFormat.Lt)} Uhr",
            Calendar.LastDay => $"Gestern um {dateTime.ToString(LongDateFormat.Lt)} Uhr",
            Calendar.LastWeek =>
                $"letzten {dateTime.ToString("dddd")} um {dateTime.ToString(LongDateFormat.Lt)} Uhr",
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
            RelativeTime.Seconds => "ein paar Sekunden",
            RelativeTime.Minute => showSuffix ? "einer Minute" : "eine Minute",
            RelativeTime.Minutes => $"{number} Minuten",
            RelativeTime.Hour => showSuffix ? "einer Stunde" : "eine Stunde",
            RelativeTime.Hours => $"{number} Stunden",
            RelativeTime.Day => showSuffix ? "einem Tag" : "ein Tag",
            RelativeTime.Days => $"{number} {(showSuffix ? "Tagen" : "Tage")}",
            RelativeTime.Month => showSuffix ? "einem Monat" : "ein Monat",
            RelativeTime.Months => $"{number} {(showSuffix ? "Monaten" : "Monate")}",
            RelativeTime.Year => showSuffix ? "einem Jahr" : "ein Jahr",
            RelativeTime.Years => $"{number} {(showSuffix ? "Jahren" : "Jahre")}",
            _ => string.Empty
        };
        return !showSuffix ? results : string.Format(isFuture ? "in {0}" : "vor {0}", results);
    }
}