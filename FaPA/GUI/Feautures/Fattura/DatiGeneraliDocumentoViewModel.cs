using WAFEL.Core.CORE.DomainServices.Helpers;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;
using WAFEL.Core.FaPa;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiGeneraliDocumentoViewModel : CrudViewModel<DatiGeneraliType, DatiGeneraliDocumentoType>
    {
        //ctor
        public DatiGeneraliDocumentoViewModel( IRepository repository, DatiGeneraliType instance ) :
            base( repository, instance, f => f.DatiGeneraliDocumento, "Dati documento", true )
        { }

        public override DatiGeneraliType ReadInstance()
        {
            var root = Repository.Read();
            return ( ( WAFEL.Core.CORE.Fattura ) root ).DatiGenerali;
        }

        protected override bool CanDeleteEntity( object obj )
        {
            return false;
        }
    }

}