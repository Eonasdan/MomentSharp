using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Globalization;

namespace MomentSharp.Tests
{
    [TestClass]
    public class Parse
    {
        [TestMethod]
        public void String()
        {
            var expected = new DateTime(1995, 12, 25);
            var actual = new Moment(true) { Year = 1995, Month = 12, Day = 25 }.DateTime();

            Assert.AreEqual(expected, actual, "Not the same");
        }

        [TestMethod]
        public void ToUTC()
        {
            //Eastern Standard Time
        }

        [TestMethod]
        public void CalendarTest()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");
            var m = new Moment(); 
            var str = m.Calendar();
            Assert.AreEqual(str.IndexOf("今天"), 0, "should be Today");

            var str2 = m.Calendar(DateTime.Now.AddDays(-1)); 
            Assert.AreEqual(str2.IndexOf("明天"), 0, "should be Tomorrow");

            var str3 = m.Calendar(DateTime.Now.AddDays(1));
            Assert.AreEqual(str3.IndexOf("昨天"), 0, "should be Yesterday");

        }
    }
}
