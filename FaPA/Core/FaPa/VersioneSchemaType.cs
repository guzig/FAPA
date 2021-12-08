using System;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public enum VersioneSchemaType
    {
        [XmlEnum( "FPA12" )]
        Item11,
        [XmlEnum("FPR12")]
        Item12
    }
}