using System;
using System.Globalization;
using System.Threading;
using FaPA.DomainServices.Utils;
using NUnit.Framework;

namespace FaPaTets.Misc
{
    class NumberFormatTests
    {
        [Test]
        public void Test1()
        {
            string value = "1,50";
            var dd = decimal.Parse( value.Replace( "€", "" ).Replace( "$", "" ) );

            //NumberFormatInfo nfi = new NumberFormatInfo();
            //nfi.NumberDecimalSeparator = ".";


            //Thread.CurrentThread.CurrentCulture = new CultureInfo( "en-US" );

            Console.Write(dd.ToString("0.000"), CultureInfo.InvariantCulture );

        }

    }
}
