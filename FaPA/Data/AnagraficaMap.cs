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
                    c.Length( 16 );
                    //c.NotNullable( true );
                } );
            } );

            Property( x => x.PIva, d =>
            {
                d.Column( c =>
                {
                    c.Length( 11 );
                    //c.NotNullable( true );
                    c.Unique( true );
                    c.Check( "" );
                } );
            } );

            Property( x => x.Denominazione, d =>
            {
                d.Column( c =>
                {
                    c.Length( 80 );
                    c.Index( "denomidx" );
                } );
            } );

            Property( x => x.Cognome, d =>
            {
                d.Column( c =>
                {
                    c.Length( 60 );
                    c.Index( "cognomidx" );
                } );
            } );

            Property( x => x.Nome, d =>
            {
                d.Column( c =>
                {
                    c.Length( 60 );
                    c.Index( "nomidx" );
                } );
            } );

            Property( x => x.CodUfficioPa, d =>
            {
                d.Column( c =>
                {
                    c.Length( 7 );
                } );
            } );

            Property( x => x.Email, d =>
            {
                d.Column( c =>
                {
                    c.Length( 256 );
                } );
            } );

            Property( x => x.Pec, d =>
            {
                d.Column( c =>
                {
                    c.Length( 256 );
                } );
            } );


            Property( x => x.Tel, d =>
            {
                d.Column( c =>
                {
                    c.Length( 12 );
                } );
            } );

            Property( x => x.Fax, d =>
            {
                d.Column( c =>
                {
                    c.Length( 12 );
                } );
            } );

            Property(x => x.Comune, d =>
            {
                d.Column(c =>
                {
                    c.Length(60);
                    c.NotNullable(true);
                });
            });

            Property(x => x.Provincia, d =>
            {
                d.Column(c =>
                {
                    c.Length(2);
                    c.NotNullable(true);
                });
            });

            Property(x => x.Cap, d =>
            {
                d.Column(c =>
                {
                    c.Length(5);
                    c.NotNullable(true);
                });
            });

            Property(x => x.Indirizzo, d =>
            {
                d.Column(c =>
                {
                    c.Length(60);
                    c.NotNullable(true);
                });
            });

            Property(x => x.Civico, d =>
            {
                d.Column(c =>
                {
                    c.Length(8);
                    c.NotNullable(true);
                });
            });

            Property(x => x.Nazione, d =>
            {
                d.Column(c =>
                {
                    c.Length(2);
                    c.NotNullable(true);
                });
            });

            Property( x => x.RifAmministrazione, d =>
            {
                d.Column( c =>
                {
                    c.Length( 20 );
                } );
            } );

            Set( x => x.Fatture, s =>
            {
                s.Inverse( true ); // Is collection inverse?
                s.Fetch( CollectionFetchMode.Join ); // or CollectionFetchMode.Select, CollectionFetchMode.Subselect
                s.BatchSize( 20 );
                s.Lazy( CollectionLazy.Lazy );
                s.Cascade( Cascade.None ); //set cascade strategy
                //s.Key(k => k.Column(col => col.Name("FatturaId"))); //foreign key in Comment table
            }, a => a.OneToMany() );

        }
    }
}