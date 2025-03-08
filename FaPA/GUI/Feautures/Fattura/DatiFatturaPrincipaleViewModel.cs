using System;
using WAFEL.Core.CORE.DomainServices.Helpers;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;
using WAFEL.Core.FaPa;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiFatturaPrincipaleViewModel : CrudViewModel<DatiGeneraliType, FatturaPrincipaleType>
    {
        //ctor
        public DatiFatturaPrincipaleViewModel( IRepository repository, DatiGeneraliType instance ) :
            base( repository, instance, f => f.FatturaPrincipale, "Fattura principale", true )
        { }

        protected override object CreateInstance()
        {
            return new FatturaPrincipaleType() {DataFatturaPrincipale = DateTime.Now.Date};
        }

        public override DatiGeneraliType ReadInstance()
        {
            var root = Repository.Read();
            return ( ( WAFEL.Core.CORE.Fattura ) root ).DatiGenerali;
        }

    }
}