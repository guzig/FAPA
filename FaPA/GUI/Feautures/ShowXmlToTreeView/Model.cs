using System.Xml;
using Caliburn.Micro;

namespace FaPA.GUI.Feautures.ShowXmlToTreeView
{
    public class Model : PropertyChangedBase
    {
        public Model(XmlDocument xmlDocument)
        {
            _document = xmlDocument;
        }

        private XmlDocument _document;

        public XmlDocument Document
        {
            get { return _document; }
            set
            {
                if (Equals(value, _document)) return;
                _document = value;
                NotifyOfPropertyChange(() => Document);
            }
        }
    }
}
