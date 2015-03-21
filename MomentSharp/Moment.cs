using System;
using MomentSharp.Globalization;
using MomentSharp.Globalization.Languages;

namespace MomentSharp
{
    public class Moment
    {
        /// <summary>
        /// Get's a new Moment defaulting values to DateTime.UtcNow, unless <param name="zero"></param> is true in which values will be set to the min value
        /// </summary>
        /// <param name="zero">use min values instead of UtcNow</param>
        public Moment(bool zero = false)
        {
            if (zero)
            {
                Year = DateTime.MinValue.Year;
                Month = 1;
                Day = 1;
                Hour = 0;
                Minute = 0;
                Second = 0;
                Millisecond = 0;
            }
            else
            {
                var now = DateTime.UtcNow;
                Year = now.Year;
                Month = now.Month;
                Day = now.Day;
                Hour = now.Hour;
                Minute = now.Minute;
                Second = now.Second;
                Millisecond = now.Millisecond;
            }
            Language = SetLanguageByCulture();
        }

        private static ILocalize SetLanguageByCulture()
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString().Replace("-", "");
            switch (culture)
            {
                case "enUS":
                    return new EnUs();
                case "de":
                    return new De();
            }
            return new EnUs();
        }

        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public int Millisecond { get; set; }

        public ILocalize Language { get; set; }
    }
}
