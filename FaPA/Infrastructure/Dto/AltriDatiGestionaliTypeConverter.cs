using AutoMapper;
using FaPA.Core.FaPa;

namespace FaPA.Infrastructure.Dto
{
    public class AltriDatiGestionaliTypeConverter : ITypeConverter<AltriDatiGestionaliType, AltriDatiGestionaliType>
    {
        public AltriDatiGestionaliType Convert(ResolutionContext context)
        {
            var dest = context.DestinationValue as AltriDatiGestionaliType;
            var source = context.SourceValue as AltriDatiGestionaliType;

            if ( string.IsNullOrWhiteSpace( source?.TipoDato ) )
                return null;

            if (dest != null)
            {
                dest.TipoDato = source.TipoDato;
                dest.RiferimentoData = source.RiferimentoData;
                dest.RiferimentoNumero = source.RiferimentoNumero;
                dest.RiferimentoTesto = source.RiferimentoTesto;
            }
            else
            {
                return new AltriDatiGestionaliType()
                {
                    TipoDato = source.TipoDato,
                    RiferimentoData = source.RiferimentoData,
                    RiferimentoNumero = source.RiferimentoNumero,
                    RiferimentoTesto = source.RiferimentoTesto
                };
            }

            return null;
        }
    }
}