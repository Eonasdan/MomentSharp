using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
