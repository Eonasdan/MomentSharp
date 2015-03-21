using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomentSharp.Globalization
{
    /// <summary>
    /// Relative Time parts
    /// Meant to emulate: http://momentjs.com/docs/#/displaying/fromnow/
    /// </summary>
    public enum RelativeTime
    {
        Seconds,
        Minute,
        Minutes,
        Hour,
        Hours,
        Day,
        Days,
        Month,
        Months,
        Year,
        Years
    };
}
