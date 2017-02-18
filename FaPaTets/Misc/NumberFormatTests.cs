using System;
using System.Globalization;
using NUnit.Framework;

namespace FaPaTets.Misc
{
    class NumberFormatTests
    {
        [Test]
        public void Test1()
        {
            var customCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            decimal d = (decimal) 5.5;
            decimal imp = decimal.Parse(string.Format("{0:0.00}", d));
            Console.Write(imp);

        }
        
    }
}
