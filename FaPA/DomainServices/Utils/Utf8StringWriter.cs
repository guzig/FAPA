using System.IO;
using System.Text;


namespace FaPA.DomainServices.Utils
{
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
        // this is a newer C# feature, if it gives you compile errors, use this instead:
        // public override Encoding Encoding { get { return Encoding.UTF8; } }

        //public Utf8StringWriter(string path, bool append):base(path, append)
        //{
            
        //}
    }

}
