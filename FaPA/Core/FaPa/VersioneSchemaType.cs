using System;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public enum VersioneSchemaType
    {
        [XmlEnum( "1.1" )]
        Item11
    }
}