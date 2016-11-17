using FaPA.Core;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace FaPA.Data
{
    public class FatturaMap : ClassMapping<Fattura>
    {
        public FatturaMap()
        {
            Id(x => x.Id, map =>
            {
                map.Generator(Generators.HighLow, gmap => gmap.Params(new { max_low = 100 }));
            });

            Property(x => x.FatturaPa, x =>
            {
                x.Type(typeof(FatturaPaType), null);
                x.NotNullable(true);
            });
            
            Property( x => x.ProgFile, x =>
            {
                x.Generated( PropertyGeneration.Always );
                x.Update( false );
                x.Insert( false );
                x.Unique( true  );
                x.Column( c =>
                {
                    
                    c.SqlType("int IDENTITY(1,1)");
                    c.Index( "ProgFileIdx" );
                    c.Length(5);
                    c.NotNullable( true );
                } );
            } );

            Property(x => x.DataCaricamentoDB, d =>
            {
                d.Column(c =>
                {
                    c.SqlType("date");
                    c.Index("datefattcrmnt");
                    c.NotNullable(true);
                });
            });

            Property(x => x.NumeroFatturaDB, d =>
            {
                d.Column(c =>
                {
                    c.Length(25);
                    c.Index("numfatt");
                    c.UniqueKey("numfattkey");
                    c.NotNullable(true);
                });
            });

            Property(x => x.DataFatturaDB, d =>
            {
                d.Column(c =>
                {
                    c.SqlType("date");
                    c.Index("datefatt");
                    c.UniqueKey( "numfattkey" );
                    c.NotNullable(true);
                });
            });

            Property(x => x.TotaleFatturaDB, m =>
            {
                m.Column(c =>
                {
                    c.Precision(9);
                    c.NotNullable(true);
                    c.Scale(2);
                });
            });

            Property(x => x.CigDB, d =>
            {
                d.Column(c =>
                {
                    c.Length(15);
                    c.Index("cig");
                    //c.UniqueKey("cig");
                    //c.NotNullable(true);
                });
            });

            Property(x => x.CupDB, d =>
            {
                d.Column(c =>
                {
                    c.Length(15);
                    //c.Index("cup");
                    //c.UniqueKey("cig");
                    //c.NotNullable(true);
                });
            });

            Property(x => x.NoteDB, d =>
            {
                d.Column(c =>
                {
                    c.Length(1000);
                });
            });

            Property(x => x.CodUfficioDB, d =>
            {
                d.Column(c =>
                {
                    c.Length(6);
                    c.Index("coduff");
                    //c.UniqueKey("cig");
                    c.NotNullable(true);
                });
            });


            ManyToOne(x => x.AnagraficaCedenteDB, m =>
            {
                //m.Cascade(Cascade.All | Cascade.None | Cascade.Persist | Cascade.Remove);
                m.Fetch(FetchKind.Join);
                m.Lazy(LazyRelation.Proxy);
                m.UniqueKey( "numfattkey" );
                m.NotNullable(true);
            });

            ManyToOne(x => x.AnagraficaCommittenteDB, m =>
            {
                //m.Cascade(Cascade.All | Cascade.None | Cascade.Persist | Cascade.Remove);
                m.Fetch(FetchKind.Join);
                m.Lazy(LazyRelation.Proxy);
                m.NotNullable(true);
                //m.UniqueKey( "numfatt" );
            });

        }
    }
}
