using System.Windows.Data;
using System.Xml;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.ShowXmlToTreeView
{
    public class Presenter : AbstractPresenter<Model, View>
    {
        public void Initialize( XmlDocument document )
        {
            Model = new Model() ;
            //Use the XDP that has been created as one of the Window's resources ...
            XmlDataProvider dp = (XmlDataProvider)View.FindResource("xmlDP");
            //... and assign the XDoc to it, using the XDoc's root.
            dp.Document = document;
            dp.XPath = "*";

        }


    }
}
