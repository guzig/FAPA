using System.Windows.Data;
using System.Xml;
using FaPA.Infrastructure;
using FaPA.Infrastructure.Utils;

namespace FaPA.GUI.Feautures.ShowXmlToTreeView
{
    public class Presenter : AbstractPresenter<Model, View>
    {
        public void Initialize( XmlDocument document )
        {
            Model = new Model(document);

        }

        public void OnLoaded()
        {
            ShowCursor.Show();
            //Use the XDP that has been created as one of the Window's resources ...
            var dp = (XmlDataProvider)View.FindResource("xmlDP");
            //... and assign the XDoc to it, using the XDoc's root.
            dp.Document = Model.Document;
            dp.XPath = "*";
        }

    }
}
