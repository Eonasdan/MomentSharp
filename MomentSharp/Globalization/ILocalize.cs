using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace MomentSharp.Globalization
{
    /// <summary>
    /// Base class for implementing language translations.
    /// </summary>
    public interface ILocalize
    {
        LongDateFormat LongDateFormat { get;}

        string Translate(Calendar calendar, DateTime dateTime);

        string Translate(RelativeTime relativeTime, int number, bool showSuffix, bool isFuture);
    }

    /// <summary>
    /// Extra for formats from Momentjs. Some of these are may already exist in DateTime.ToString(*)
    /// </summary>
    public class LongDateFormat
    {
        public string Lt { get; set; }
        public string Lts { get; set; }
        public string L { get; set; }
        public string Ll { get; set; }
        public string Lll { get; set; }
        public string Llll { get; set; }
    }
}
