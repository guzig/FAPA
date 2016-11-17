using AutoMapper;
using FaPA.Core.FaPa;

namespace FaPA.Infrastructure.Dto
{
    public class RitenutaTypeConverter : ITypeConverter<DatiRitenutaType, DatiRitenutaType>
    {
        public DatiRitenutaType Convert( ResolutionContext context )
        {
            var dest = context.DestinationValue as DatiRitenutaType;
            var source = context.SourceValue as DatiRitenutaType;

            if (source == null || source.ImportoRitenuta <= 0) return null;

            if (dest != null)
            {
                dest.ImportoRitenuta = source.ImportoRitenuta;
                dest.AliquotaRitenuta = source.AliquotaRitenuta;
                dest.CausalePagamento = source.CausalePagamento;
                dest.TipoRitenuta = source.TipoRitenuta;
            }
            else
            {
                return new DatiRitenutaType()
                {
                    ImportoRitenuta = source.ImportoRitenuta,
                    AliquotaRitenuta = source.AliquotaRitenuta,
                    CausalePagamento = source.CausalePagamento,
                    TipoRitenuta = source.TipoRitenuta
                };
            }

            return null;
        }
    }   
}
