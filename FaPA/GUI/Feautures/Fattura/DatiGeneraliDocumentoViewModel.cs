using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiGeneraliDocumentoViewModel : EditWorkSpaceViewModel<DatiGeneraliType, DatiGeneraliDocumentoType>
    {
        //ctor
        public DatiGeneraliDocumentoViewModel( IRepository repository, DatiGeneraliType instance ) :
            base( repository, instance, f => f.DatiGeneraliDocumento, "Dati documento", true )
        { }

        public override object Read()
        {
            var root = Repository.Read();
            Instance = ( ( Core.Fattura ) root ).DatiGenerali;
            var userProp = GetterProp( Instance );
            return userProp;
        }

        protected override bool CanDeleteEntity( object obj )
        {
            return false;
        }
    }

}