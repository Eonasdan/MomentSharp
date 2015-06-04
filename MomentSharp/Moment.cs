using System;
using System.Threading;
using MomentSharp.Globalization;
using MomentSharp.Globalization.Languages;

namespace MomentSharp
{
    /// <summary>
    /// Moment object which provides support for several DateTime functions that are not built-in to C#
    /// </summary>
    public class Moment
    {
        /// <summary>
        ///     Get's a new Moment defaulting values to DateTime.UtcNow, unless <paramref name="zero" /> is true in which values
        ///     will be set to the min value
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

        /// <summary>
        /// Date's Year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Date's Month
        /// </summary>
        public int Month { get; set; }
        
        /// <summary>
        /// Date's Day
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// Date's Hour
        /// </summary>
        public int Hour { get; set; }

        /// <summary>
        /// Date's Minute
        /// </summary>
        public int Minute { get; set; }

        /// <summary>
        /// Date's Second
        /// </summary>
        public int Second { get; set; }

        /// <summary>
        /// Date's Millisecond
        /// </summary>
        public int Millisecond { get; set; }

        /// <summary>
        /// Local/Language to use
        /// </summary>
        public ILocalize Language { get; set; }

        /// <summary>
        /// Attempts to find the correct <see cref="ILocalize"/> based on the <see cref="Thread.CurrentThread"/> CurrentCulture
        /// </summary>
        /// <returns>ILocalize</returns>
        private static ILocalize SetLanguageByCulture()
        {
            var culture = Thread.CurrentThread.CurrentCulture.ToString().Replace("-", "");
            switch (culture)
            {
                case "enUS":
                    return new EnUs();
                case "de":
                    return new De();
            }
            return new EnUs();
        }
    }
}