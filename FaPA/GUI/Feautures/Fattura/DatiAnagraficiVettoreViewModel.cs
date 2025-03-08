using FaPA.GUI.Controls;
using FaPA.Infrastructure;
using System.ComponentModel;
using WAFEL.Core.FaPa;

namespace FaPA.GUI.Feautures.Fattura
{
    public class DatiAnagraficiVettoreViewModel : CrudViewModel<DatiTrasportoType, DatiAnagraficiVettoreType>
    {
        //ctor
        public DatiAnagraficiVettoreViewModel( IRepository repository, DatiTrasportoType instance ) :
            base( repository, instance, f => f.DatiAnagraficiVettore, "Anagrafici", true )
        {
        }

        public override DatiTrasportoType ReadInstance()
        {
           var root = Repository.Read();
           return ( ( WAFEL.Core.CORE.Fattura ) root ).DatiTrasporto;
        }

        protected override object CreateInstance()
        {
            var instance = new DatiAnagraficiVettoreType()
            {
                IdFiscaleIVA = new IdFiscaleType() {IdPaese = "IT"},
                Anagrafica = new AnagraficaType()
            };

            Instance.DatiAnagraficiVettore = instance;

            return instance;
        }

        protected override void HookChanged( INotifyPropertyChanged poco )
        {
            var entity = poco as DatiAnagraficiVettoreType;
            if ( entity == null ) return;
            base.HookChanged( ( INotifyPropertyChanged) entity );
            base.HookChanged( ( INotifyPropertyChanged)entity.Anagrafica );
            base.HookChanged( ( INotifyPropertyChanged ) entity.IdFiscaleIVA );
        }
    }
}