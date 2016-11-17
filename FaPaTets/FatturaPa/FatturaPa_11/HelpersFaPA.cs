using System.Xml;
using System.Xml.Serialization;
using FaPA.Core.FaPa;

namespace FaPaTets.FatturaPa.FatturaPa_11
{
    public static class HelpersFaPA
    {
        public static FatturaElettronicaType GetFatturaPA(string nomeFile)
        {
            FatturaElettronicaType wrapper;
            var ser1 = new XmlSerializer(typeof(FatturaElettronicaType));
            using (var reader = XmlReader.Create(nomeFile))
            {
                wrapper = (FatturaElettronicaType)ser1.Deserialize(reader);
            }
            return wrapper;
        }
    }
}