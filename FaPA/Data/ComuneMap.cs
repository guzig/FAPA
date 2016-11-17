using FaPA.Core;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace FaPA.Data
{
    public class ComuneMap : ClassMapping<Comune>
    {
        public ComuneMap()
        {
            Id( x => x.Id, map =>
            {
                map.Generator( Generators.HighLow, gmap => gmap.Params( new { max_low = 100 } ) );
            } );

            Property(x => x.Cap, d =>
            {
                d.Column(c =>
                {//c.SqlType( "date" );//c.Index( "datefattcrmnt" );
                    c.Length(5);
                });
            });
            Property( x => x.CodiceAlpha, d =>
            {
                d.Column( c =>
                {
                    c.NotNullable( true );
                } );
            } );
            Property( x => x.CodiceCittàMetropolitana, d =>
            {
                d.Column( c =>
                {
                    c.NotNullable( true );
                } );
            } );
            Property( x => x.CodiceComune, d =>
            {
                d.Column( c =>
                {
                    c.NotNullable( true );
                } );
            } );
            Property( x => x.CodiceProvincia, d =>
            {
                d.Column( c =>
                {
                    c.NotNullable( true );
                } );
            } );
            Property( x => x.CodiceRegione, d =>
            {
                d.Column( c =>
                {
                    c.NotNullable( true );
                } );
            } );
            Property( x => x.Denominazione, d =>
            {
                d.Column( c =>
                {
                    c.NotNullable( true );
                    c.Index("denmcmne");
                } );
            } );
            Property( x => x.DenominazioneProvincia, d =>
            {
                d.Column( c =>
                {
                    c.NotNullable( true );
                } );
            } );
            Property( x => x.FlagComuneCapoluogo, d =>
            {
                d.Column( c =>
                {
                    c.NotNullable( true );
                } );
            } );
            Property( x => x.NomeCittàMetropolitana, d =>
            {
                d.Column( c =>
                {
                    c.NotNullable( true );
                } );
            } );
            Property( x => x.NomeRegione, d =>
            {
                d.Column( c =>
                {
                    c.NotNullable( true );
                } );
            } );

            Property( x => x.SiglaAuto, d =>
            {
                d.Column( c =>
                {
                    c.NotNullable( true );
                } );
            } );

            Property( x => x.CodiceCatastale, d =>
            {
                d.Column( c =>
                {
                    c.Length( 25 );
                    c.Index( "ComuneCodiceCatastaleIdx" );
                    c.UniqueKey( "ComuneCodiceCatastalekey" );
                    c.NotNullable( true );
                } );
            } );



        }
    }
}
