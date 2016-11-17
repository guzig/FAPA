using FaPA.Core;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace FaPA.Data
{
    public class AnagraficaMap : ClassMapping<Anagrafica>
    {
        public AnagraficaMap()
        {
            Id(x => x.Id, map =>
            {
                map.Generator(Generators.HighLow, gmap => gmap.Params(new { max_low = 100 }));
            });

            Discriminator(x =>
            {
                x.Force(true);
                //x.Formula("arbitrary SQL expression");
                x.Insert(true);
                x.Length(50);
                x.NotNullable(true);
                //x.Type(NHibernateUtil.String);
                x.Column( "Discriminator" );
            });

            Property( x => x.CodiceFiscale, d =>
            {
                d.Column( c =>
                {
                    c.Length( 28 );
                    c.NotNullable( true );
                } );
            } );

            Property( x => x.PIva, d =>
            {
                d.Column( c =>
                {
                    c.Length( 28 );
                    c.NotNullable( true );
                } );
            } );

            Property(x => x.Denominazione);
            Property(x => x.Cognome);
            Property(x => x.Nome);
            Property(x => x.Email);
            Property(x => x.Pec);
            Property(x => x.Tel);
            Property(x => x.Fax);
            Property(x => x.Note, d =>
            {
                d.Column(c =>
                {
                    c.Length(1000);
                });
            });
            Property(x => x.Comune, d =>
            {
                d.Column(c =>
                {
                    c.Length(50);
                    c.NotNullable(true);
                });
            });

            Property(x => x.Provincia, d =>
            {
                d.Column(c =>
                {
                    c.Length(50);
                    c.NotNullable(true);
                });
            });

            Property(x => x.Cap, d =>
            {
                d.Column(c =>
                {
                    c.Length(50);
                    c.NotNullable(true);
                });
            });

            Property(x => x.Indirizzo, d =>
            {
                d.Column(c =>
                {
                    c.Length(50);
                    c.NotNullable(true);
                });
            });

            Property(x => x.Civico, d =>
            {
                d.Column(c =>
                {
                    c.Length(25);
                    c.NotNullable(true);
                });
            });

            Property(x => x.Nazione, d =>
            {
                d.Column(c =>
                {
                    c.Length(5);
                    c.NotNullable(true);
                });
            });

        }
    }
}