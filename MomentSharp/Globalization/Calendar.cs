using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomentSharp.Globalization
{
    /// <summary>
    /// Calendar parts.
    /// Meant to emulate http://momentjs.com/docs/#/displaying/calendar-time/
    /// </summary>
    public enum Calendar
    {
        SameDay,
        NextDay,
        NextWeek,
        LastDay,
        LastWeek,
        SameElse
    }
}
