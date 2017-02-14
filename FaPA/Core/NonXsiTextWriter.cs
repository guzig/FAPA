using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace FaPA.Core
{
    public class NonXsiTextWriter : XmlTextWriter
    {

        public NonXsiTextWriter( TextWriter w ) : base( w ) { }
        public NonXsiTextWriter( Stream w, Encoding encoding ) : base( w, encoding ) { }
        public NonXsiTextWriter( string filename, Encoding encoding ) : base( filename, encoding ) { }

        bool _skip = false;

        public override void WriteStartAttribute( string prefix, string localName, string ns )
        {
            if ( ns == XmlSchema.InstanceNamespace  ) // Omits XSD and XSI declarations. 
            {
                _skip = true; return;
            } 
            base.WriteStartAttribute( prefix, localName, ns );
        }

        public override void WriteString( string text )
        {
            if ( _skip ) return;
            base.WriteString( text );
        }

        public override void WriteEndAttribute()
        {
            if ( _skip )
            {
                // Reset the flag, so we keep writing. 
                _skip = false; return;
            }
            base.WriteEndAttribute();
        }
    }
}