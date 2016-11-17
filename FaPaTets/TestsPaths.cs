using System;

namespace FaPaTets
{
    public static class TestsPaths
    {
        const string PathTestData = @"\TestData";
        const string BasePath = "FaPA";

        //public static string Path_FPA_P7M { get } = TestDataBaseFullNamePath + @"\FatturePA\fatturePA_p7m";
        //public static string Path_FPA_Pdf { get; } = TestDataBaseFullNamePath + @"\FatturePA\fatturePA_pdf";
        public static string Path_FPA_Xml { get; } = TestDataRootPath + @"\FatturePA\fatturePA_xml";

        public static string TestDataRootPath
        {
            get
            {
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                var partIndex = baseDirectory.IndexOf( BasePath, StringComparison.Ordinal );

                if ( partIndex < 0 )
                    return null;

                return baseDirectory.Substring( 0, partIndex ) + BasePath + PathTestData;

            }
        }

    }
}
