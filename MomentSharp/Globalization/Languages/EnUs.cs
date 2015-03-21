using System;

namespace MomentSharp.Globalization.Languages
{
    public class EnUs : ILocalize
    {
        private readonly LongDateFormat _longDateFormat;

        public EnUs()
        {
            _longDateFormat = new LongDateFormat
            {
                Lt = "h:mm tt",
                Lts = "h:mm:s tt",
                L = "M/d/yyyy",
                Ll = "MMMM d, yyyy"
            };

            _longDateFormat.Lll = String.Format("MMMM d, yyyy {0}", _longDateFormat.Lt);
            _longDateFormat.Llll = String.Format("dddd, MMMM d, yyyy {0}", _longDateFormat.Lt);
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
                    return String.Format("Today at {0}", dateTime.ToString(LongDateFormat.Lt));
                case Calendar.NextDay:
                    return String.Format("Tomorrow at {0}", dateTime.ToString(LongDateFormat.Lt));
                case Calendar.NextWeek:
                    return String.Format("{0} at {1}", dateTime.ToString("dddd"), dateTime.ToString(LongDateFormat.Lt));
                case Calendar.LastDay:
                    return String.Format("Yesterday at {0}", dateTime.ToString(LongDateFormat.Lt));
                case Calendar.LastWeek:
                    return String.Format("{0} at {1}", dateTime.ToString("dddd"), dateTime.ToString(LongDateFormat.Lt));
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
                    results = "a few seconds";
                    break;
                case RelativeTime.Minute:
                    results = "a minute";
                    break;
                case RelativeTime.Minutes:
                    results = String.Format("{0} minutes", number);
                    break;
                case RelativeTime.Hour:
                    results = "an hour";
                    break;
                case RelativeTime.Hours:
                    results = String.Format("{0} hours", number);
                    break;
                case RelativeTime.Day:
                    results = "a day";
                    break;
                case RelativeTime.Days:
                    results = String.Format("{0} days", number);
                    break;
                case RelativeTime.Month:
                    results = "a month";
                    break;
                case RelativeTime.Months:
                    results = String.Format("{0} months", number);
                    break;
                case RelativeTime.Year:
                    results = "a year";
                    break;
                case RelativeTime.Years:
                    results = String.Format("{0} years", number);
                    break;
            }
            return !showSuffix ? results : String.Format(isFuture ? "in {0}" : "{0} ago", results);
        }
    }
}