using FaPA.Core;
using NUnit.Framework;

namespace FaPaTets
{
    class MappingTests
    {
        [Test]
        public void MapAnagraficaTest()
        {

            //Mapper.CreateMap<FaPA.Core.Anagrafica, AnagraficaDto>().
            //    ForMember(l => l.Comune, opt => opt.ResolveUsing<CustomResolver>());
            
            ConfigAnagraficaMap();

            var source = new Anagrafica()
            {
                Comune = "Cropani"
            };

            //var toDto = Mapper.Map<Anagrafica, Anagrafica>(source);

            //Assert.AreEqual("Cropani", toDto.Comune.Denominazione);

            //var dto = new AnagraficaDto() { Comune = new Comune() { Denominazione = "Cropani" } };

            //var toForn = Mapper.Map<AnagraficaDto, Anagrafica>( dto );
            //Assert.AreEqual("Cropani", toForn.Comune);

            //var toComm = Mapper.Map<AnagraficaDto, Anagrafica>(dto);
            //Assert.AreEqual("Cropani", toComm.Comune);

        }

        private static void ConfigAnagraficaMap()
        {
            //var _comuni = new ReferenceDataFactory().GetReferenceCollection<Comune>().DistinctBy(c => c.Denominazione).
            //    ToDictionary(k => k.Denominazione, v => v);

            //Mapper.CreateMap<Anagrafica, AnagraficaDto>().
            //    ForMember(x => x.IsNotifying, opts => opts.Ignore()).
            //    ForMember(x => x.Comune, opts => opts.MapFrom(src => _comuni[src.Comune]));

            //Mapper.CreateMap<Anagrafica, AnagraficaDto>().
            //    ForMember(x => x.IsNotifying, opts => opts.Ignore()).
            //    ForMember(x => x.Comune, opts => opts.MapFrom(src => _comuni[src.Comune]));

            //Mapper.CreateMap<AnagraficaDto, Anagrafica>().
            //    ForMember(x => x.Fatture, opts => opts.Ignore()).
            //    ForMember(x => x.Cap, opts => opts.Ignore()).
            //    ForMember(x => x.Comune, opts => opts.MapFrom(src => src.Comune.Denominazione)).
            //    ForMember(x => x.Provincia, opts => opts.MapFrom(src => src.Comune.DenominazioneProvincia));

            //Mapper.CreateMap<AnagraficaDto, Anagrafica>().
            //    ForMember(x => x.Fatture, opts => opts.Ignore()).
            //    ForMember(x => x.Cap, opts => opts.Ignore()).
            //    ForMember(x => x.Comune, opts => opts.MapFrom(src => src.Comune.Denominazione)).
            //    ForMember(x => x.Provincia, opts => opts.MapFrom(src => src.Comune.DenominazioneProvincia));

            //Mapper.AssertConfigurationIsValid();
        }

        //private static void ConfigureAutomapperFattura()
        //{
        //    Mapper.CreateMap<DatiRitenutaType, DatiRitenutaType>().ConvertUsing(new RitenutaTypeConverter());
        //    Mapper.CreateMap<AltriDatiGestionaliType, AltriDatiGestionaliType>().ConvertUsing(new AltriDatiGestionaliTypeConverter());

        //    Mapper.CreateMap<Fattura, FatturaDto>().
        //        ForMember(x => x.IsNotifying, opts => opts.Ignore()).
        //        ForMember(x => x.CassaPrevidenziale, opts => opts.
        //            MapFrom(src => src.CassaPrevidenziale != null && src.CassaPrevidenziale.ImportoContributoCassa > 0
        //                ? src.CassaPrevidenziale : null));

        //    Mapper.CreateMap<FatturaDto, Fattura>().
        //        ForMember(x => x.Ritenuta, opts => opts.MapFrom(src => src.Ritenuta)).
        //        ForMember(x => x.CassaPrevidenziale, opts => opts.
        //            MapFrom(src =>src.CassaPrevidenziale != null && src.CassaPrevidenziale.ImportoContributoCassa > 0
        //                        ? src.CassaPrevidenziale : null)).
        //        AfterMap((src, dest) =>
        //        {
        //            dest.DatiGeneraliDocumento.Data = src.DataCaricamentoDB;
        //            dest.DatiGeneraliDocumento.ImportoTotaleDocumento = src.TotaleFatturaDB;
        //            dest.DatiGeneraliDocumento.Numero = src.NumeroFatturaDB;
        //        });

        //    Mapper.AssertConfigurationIsValid();
        //}

        //[Test]
        //public void MapFatturaTest()
        //{
        //    //BootStrapper.Initialize();
        //    //var session = BootStrapper.SessionFactory.OpenSession();
        //    //!= null && src.Ritenuta.ImportoRitenuta > 0 ? src.Ritenuta : null

        //    ConfigureAutomapperFattura();

        //    Anagrafica fornitore = new Anagrafica() {};
        //    Anagrafica committente = new Anagrafica();
        //    var toCore = new Fattura()
        //    {
        //        AnagraficaCommittenteDB = (Anagrafica) committente,
        //        AnagraficaCedenteDB = (Anagrafica) fornitore,
        //        DataFatturaDB = new DateTime(2016,1,1)
        //    };
        //    toCore.Init();
        //    var toDto = Mapper.Map<Fattura, FatturaDto>( toCore );

        //    Assert.AreEqual(new DateTime(2016, 1, 1), toDto.DataFatturaDB);
        //    Assert.IsNull(toCore.Ritenuta);

        //    toDto.Init();
        //    toDto.Ritenuta = new DatiRitenutaType();

        //    Mapper.Map<FatturaDto, Fattura>(toDto, toCore);
        //    Assert.IsNull(toCore.Ritenuta);

        //    toDto.Ritenuta.ImportoRitenuta = 1;

        //    Mapper.Map<FatturaDto, Fattura>( toDto, toCore );

        //    Assert.AreEqual( 1, toCore.Ritenuta.ImportoRitenuta );
        //}

        //[Test]
        //public void MapFatturaTestDB()
        //{
        //    BootStrapper.Initialize();
        //    var session = BootStrapper.SessionFactory.OpenSession();
        //    //!= null && src.Ritenuta.ImportoRitenuta > 0 ? src.Ritenuta : null

        //    ConfigureAutomapperFattura();
        //    ConfigAnagraficaMap();

        //    var s = session.QueryOver<Anagrafica>().List<Anagrafica>();

        //    var s1 = session.QueryOver<Anagrafica>().Where(a => a.Id == 32768).Cacheable().
        //        SingleOrDefault<Anagrafica>();

        //    var toCore = QueryOver.Of<Fattura>().
        //            Fetch(f => f.AnagraficaCedenteDB).Eager.Fetch(f => f.AnagraficaCommittenteDB).Eager.
        //            Where(f => f.Id== 229376).GetExecutableQueryOver(session).SingleOrDefault<Fattura>();
            
        //    var toDto = Mapper.Map<Fattura, FatturaDto>(toCore);

        //    //Assert.AreEqual(new DateTime(2016, 1, 1), toDto.DataFatturaDB);
        //    //Assert.IsNull(toCore.Ritenuta);

        //    toDto.Init();
        //    toDto.Ritenuta = new DatiRitenutaType();

        //    Mapper.Map<FatturaDto, Fattura>(toDto, toCore);

        //}

        //[Test]
        //public void MapDatiTrasmittente()
        //{
        //    Mapper.CreateMap<IdFiscaleType, IdFiscaleType>();
        //    Mapper.CreateMap<ContattiTrasmittenteType, ContattiTrasmittenteType>();

        //    Mapper.CreateMap<DatiTrasmissioneType, DatiTrasmissioneDto>().
        //        ForMember( s => s.Id, opts => opts.Ignore() ).
        //        ForMember( s => s.Version, opts => opts.Ignore() ).
        //        ForMember( x => x.IsNotifying, opts => opts.Ignore() ).
        //        ForMember( x => x.Email, o => o.Condition( s => s.ContattiTrasmittente != null ) ).
        //        ForMember( x => x.Telefono, o => o.Condition( s => s.ContattiTrasmittente != null ) ).
        //        ForMember( x => x.Email, o => o.MapFrom( s => s.ContattiTrasmittente.Email ) ).
        //        ForMember( x => x.Telefono, o => o.MapFrom( s => s.ContattiTrasmittente.Telefono ) );

        //    Mapper.CreateMap<DatiTrasmissioneDto, DatiTrasmissioneType>().
        //        //ForMember( x => x.ContattiTrasmittente, o => o.Condition( rc =>
        //        //{
        //        //    var dto = ( DatiTrasmissioneTypeDto ) rc.Parent.SourceValue;
        //        //    return !string.IsNullOrWhiteSpace( dto?.Email ) ||
        //        //           !string.IsNullOrWhiteSpace( dto?.Telefono );
        //        //} ) ).
        //        //.ForMember( dest => dest.TypeIndicator, src => src.ResolveUsing( new TypeIndicatorResolver() ) );
        //        ForMember(d => d.ContattiTrasmittente, src => src.ResolveUsing( (x,y) =>
        //        {
        //            var source = ( DatiTrasmissioneDto ) x.Context.SourceValue;
        //            return !string.IsNullOrWhiteSpace( source?.Email ) ||
        //                   !string.IsNullOrWhiteSpace( source?.Telefono )
        //                ? new ContattiTrasmittenteType() {Email = source.Email, Telefono = source.Telefono}
        //                : null;
        //        } ));
                
        //        //opt => opt.MapFrom( r =>
        //        //  new ContattiTrasmittenteType() { Telefono = r.Telefono, Email = r.Email } ) );

        //    Mapper.AssertConfigurationIsValid();

        //    var poco = new DatiTrasmissioneType()
        //    {
        //        IdTrasmittente = new IdFiscaleType() { IdPaese = "IT", IdCodice = "GZZGPP71D15D181H"},
        //        FormatoTrasmissione = FormatoTrasmissioneType.SDI11,
        //        CodiceDestinatario = "UFZEB5",
        //        ContattiTrasmittente = new ContattiTrasmittenteType() { Telefono = "1", Email = "email@gmail.com"}
        //    };

        //    var dt = Mapper.Map<DatiTrasmissioneType, DatiTrasmissioneDto>( poco );
        //    poco.ContattiTrasmittente = null;
        //    Mapper.Map( dt, poco );
        //    Assert.IsNotNull( poco.ContattiTrasmittente );

        //    dt.Telefono = null;
        //    dt.Email = null;
        //    Mapper.Map( dt, poco );
        //    Assert.IsNull( poco.ContattiTrasmittente );
        //}

    }
}
