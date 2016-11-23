using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class TrasmittenteTabViewModel : EditWorkSpaceViewModel<Core.Fattura, DatiTrasmissioneType>
    {

        //ctor
        public TrasmittenteTabViewModel( IRepository repository, Core.Fattura instance ) :
            base( repository, instance, (Core.Fattura f) => f.DatiTrasmissione, "Trasmittente", false )
        {
            IsCloseable = false;
        }

        //protected override void MapToDto()
        //{
        //    var dto = new DatiTrasmissioneDto();
        //    var poco = UserProperty as DatiTrasmissioneType;
        //    if ( poco != null && poco.IdTrasmittente == null )
        //    {
        //        Instance.SetTrasmittente();
        //    }
        //    Mapper.Map(UserProperty, dto);
        //    Dto = dto;
        //    BeginEdit();
        //}
    }
}