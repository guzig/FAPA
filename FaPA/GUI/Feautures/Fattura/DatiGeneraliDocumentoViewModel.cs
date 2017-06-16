using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

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
            return ( ( Core.Fattura ) root ).DatiGenerali;
        }

        protected override bool CanDeleteEntity( object obj )
        {
            return false;
        }
    }

}