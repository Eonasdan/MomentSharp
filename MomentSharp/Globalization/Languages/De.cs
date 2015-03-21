using System;

namespace MomentSharp.Globalization.Languages
{
    public class De : ILocalize
    {
        private readonly LongDateFormat _longDateFormat;

        public De()
        {
            _longDateFormat = new LongDateFormat
            {
                Lt = "HH:mm",
                Lts = "HH:mm:ss",
                L = "dd.MM.yyyy",
                Ll = "d. MMMM yyyy"
            };

            _longDateFormat.Lll = String.Format("d. MMMM yyyy {0}", _longDateFormat.Lt);
            _longDateFormat.Llll = String.Format("dddd, d. MMMM yyyy {0}", _longDateFormat.Lt);
        }

        public LongDateFormat LongDateFormat
        {
            get { return _longDateFormat; }
        }

        public string Translate(Calendar calendar, DateTime dateTime)
        {
            switch (calendar)
            {
                case Calendar.SameDay:
                    return String.Format("Heute um {0}", dateTime.ToString(LongDateFormat.Lt));
                case Calendar.NextDay:
                    return String.Format("Morgen um {0}", dateTime.ToString(LongDateFormat.Lt));
                case Calendar.NextWeek:
                    return String.Format("{0} um {1} Uhr", dateTime.ToString("dddd"), dateTime.ToString(LongDateFormat.Lt));
                case Calendar.LastDay:
                    return String.Format("Gestern um {0} Uhr", dateTime.ToString(LongDateFormat.Lt));
                case Calendar.LastWeek:
                    return String.Format("letzten {0} um {1} Uhr", dateTime.ToString("dddd"), dateTime.ToString(LongDateFormat.Lt));
                case Calendar.SameElse:
                    return dateTime.ToString(LongDateFormat.L);
            }
            return "";
        }

        public string Translate(RelativeTime relativeTime, int number, bool showSuffix, bool isFuture)
        {
            var results = String.Empty;
            switch (relativeTime)
            {
                case RelativeTime.Seconds:
                    results = "ein paar Sekunden";
                    break;
                case RelativeTime.Minute:
                    results = showSuffix ? "einer Minute" : "eine Minute";
                    break;
                case RelativeTime.Minutes:
                    results = String.Format("{0} Minuten", number);
                    break;
                case RelativeTime.Hour:
                    results = showSuffix ? "einer Stunde" : "eine Stunde";
                    break;
                case RelativeTime.Hours:
                    results = String.Format("{0} Stunden", number);
                    break;
                case RelativeTime.Day:
                    results = showSuffix ? "einem Tag" : "ein Tag";
                    break;
                case RelativeTime.Days:
                    results = String.Format("{0} {1}", number, showSuffix ? "Tagen" : "Tage");
                    break;
                case RelativeTime.Month:
                    results = showSuffix ? "einem Monat" : "ein Monat";
                    break;
                case RelativeTime.Months:
                    results = String.Format("{0} {1}", number, showSuffix ? "Monaten" : "Monate");
                    break;
                case RelativeTime.Year:
                    results = showSuffix ? "einem Jahr" : "ein Jahr";
                    break;
                case RelativeTime.Years:
                    results = String.Format("{0} {1}", number, showSuffix ? "Jahren" : "Jahre");
                    break;
            }
            return !showSuffix ? results : String.Format(isFuture ? "in {0}" : "vor {0}", results);
        }
    }
}
