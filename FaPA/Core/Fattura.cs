using System;
using System.Collections.Generic;
using System.ComponentModel;
using FaPA.Core.FaPa;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using FaPA.AppServices;
using FaPA.AppServices.CoreValidation;
using FaPA.Infrastructure.Helpers;

namespace FaPA.Core
{
    public class Fattura : BaseEntity, IFlyFetch
    {
        #region DB props

        /// <summary>
        /// anagrafica fornitore
        /// </summary>
        public virtual Anagrafica AnagraficaCedenteDB { get; set; }

        /// <summary>
        /// Anagrafica del committente
        /// </summary>
        public virtual Anagrafica AnagraficaCommittenteDB { get; set; }

        public virtual DateTime DataCaricamentoDB { get; set; }

        public virtual string CigDB { get; set; }

        public virtual string CupDB { get; set; }

        public virtual string CodUfficioDB { get; set; }

        //public virtual string CodSoggettoSDI { get; set; }

        public virtual DateTime DataFatturaDB { get; set; }

        public virtual decimal TotaleFatturaDB { get; set; }

        public virtual string NumeroFatturaDB { get; set; }

        public virtual string RiferimentoAmmDB { get; set; }

        public virtual int ProgFile { get; set; }

        public virtual FormatoTrasmissioneType FormatoTrasmissioneDB { get; set; }

        public virtual string PecDestinatarioDB { get; set; }

        public virtual FatturaElettronicaType FatturaPa { get; set; }

        #endregion
        
        #region Header

        public virtual FatturaElettronicaHeaderType FatturaElettronicaHeader
        {
            get { return FatturaPa.FatturaElettronicaHeader; }
            set { FatturaPa.FatturaElettronicaHeader = value; }
        }

        public virtual TerzoIntermediarioSoggettoEmittenteType TerzoIntermediarioOSoggettoEmittente
        {
            get { return FatturaElettronicaHeader.TerzoIntermediarioOSoggettoEmittente; }
            set { FatturaElettronicaHeader.TerzoIntermediarioOSoggettoEmittente = value; }
        }

        public virtual DatiTrasmissioneType DatiTrasmissione
        {
            get
            {
                if (FatturaPa.FatturaElettronicaHeader.DatiTrasmissione == null)
                {
                    FatturaPa.FatturaElettronicaHeader.DatiTrasmissione = new DatiTrasmissioneType();
                }

                return FatturaPa.FatturaElettronicaHeader.DatiTrasmissione;
            }
            set { FatturaPa.FatturaElettronicaHeader.DatiTrasmissione = value; }
        }

        public virtual RappresentanteFiscaleType RappresentanteFiscale
        {
            get { return FatturaElettronicaHeader.RappresentanteFiscale; }
            set { FatturaElettronicaHeader.RappresentanteFiscale = value; }
        }

        #endregion

        #region DatiGenerali

        public virtual DatiGeneraliType DatiGenerali
        {
            get { return FatturaPa.FatturaElettronicaBody.DatiGenerali; }
            set { FatturaPa.FatturaElettronicaBody.DatiGenerali = value; }
        }

        public virtual DatiDocumentiCorrelatiType[] DatiConvenzione
        {
            get { return DatiGenerali.DatiConvenzione; }
            set { DatiGenerali.DatiConvenzione = value; }
        }

        public virtual DatiDocumentiCorrelatiType[] DatiContratto
        {
            get { return DatiGenerali.DatiContratto; }
            set { DatiGenerali.DatiContratto = value; }
        }

        public virtual DatiDocumentiCorrelatiType[] DatiOrdineAcquisto
        {
            get { return DatiGenerali.DatiOrdineAcquisto; }
            set { DatiGenerali.DatiOrdineAcquisto = value; }
        }

        public virtual DatiDocumentiCorrelatiType[] DatiRicezione
        {
            get { return DatiGenerali.DatiRicezione; }
            set { DatiGenerali.DatiRicezione = value; }
        }

        public virtual DatiDocumentiCorrelatiType[] DatiFattureCollegate
        {
            get { return DatiGenerali.DatiFattureCollegate; }
            set { DatiGenerali.DatiFattureCollegate = value; }
        }

        public virtual DatiSALType[] DatiSAL
        {
            get { return DatiGenerali.DatiSAL; }
            set { DatiGenerali.DatiSAL = value; }
        }

        public virtual DatiDDTType[] DatiDDT
        {
            get { return DatiGenerali.DatiDDT; }
            set { DatiGenerali.DatiDDT = value; }
        }

        public virtual DatiTrasportoType DatiTrasporto
        {
            get { return DatiGenerali.DatiTrasporto; }
            set { DatiGenerali.DatiTrasporto = value; }
        }

        public virtual FatturaPrincipaleType FatturaPrincipale
        {
            get { return DatiGenerali.FatturaPrincipale; }
            set { DatiGenerali.FatturaPrincipale = value; }
        }

        #endregion
        
        #region Committente e Fornitore

        public virtual CedentePrestatoreType CedenteFornitore
        {
            get { return FatturaPa.FatturaElettronicaHeader.CedentePrestatore; }
            set { FatturaPa.FatturaElettronicaHeader.CedentePrestatore = value; }
        }

        public virtual AnagraficaType AnagraficaCedenteFornitore
        {
            get { return CedenteFornitore.DatiAnagrafici.Anagrafica; }
            set { CedenteFornitore.DatiAnagrafici.Anagrafica = value; }
        }

        public virtual RegimeFiscaleType RegimeFiscale
        {
            get { return CedenteFornitore.DatiAnagrafici.RegimeFiscale; }
            set { CedenteFornitore.DatiAnagrafici.RegimeFiscale = value; }
        }

        public virtual CessionarioCommittenteType CessionarioCommittente
        {
            get { return FatturaPa.FatturaElettronicaHeader.CessionarioCommittente; }
            set { FatturaPa.FatturaElettronicaHeader.CessionarioCommittente = value; }
        }

        public virtual AnagraficaType AnagraficaCessionarioCommittente
        {
            get { return CessionarioCommittente.DatiAnagrafici.Anagrafica; }
            set { CessionarioCommittente.DatiAnagrafici.Anagrafica = value; }
        }

        #endregion
        
        #region DatiGeneraliDocumento

        public virtual DatiGeneraliDocumentoType DatiGeneraliDocumento
        {
            get { return FatturaPa.FatturaElettronicaBody.DatiGenerali.DatiGeneraliDocumento; }

            set { FatturaPa.FatturaElettronicaBody.DatiGenerali.DatiGeneraliDocumento = value; }
        }

        public virtual DatiRitenutaType Ritenuta
        {
            get { return DatiGeneraliDocumento.DatiRitenuta; }

            set { DatiGeneraliDocumento.DatiRitenuta = value; }
        }

        public virtual ScontoMaggiorazioneType[] ScontoMaggiorazione
        {
            get { return DatiGeneraliDocumento.ScontoMaggiorazione; }

            set { DatiGeneraliDocumento.ScontoMaggiorazione = value; }
        }

        public virtual DatiCassaPrevidenzialeType CassaPrevidenziale
        {
            get { return DatiGeneraliDocumento.DatiCassaPrevidenziale; }

            set { DatiGeneraliDocumento.DatiCassaPrevidenziale = value; }
        }

        #endregion
        
        #region FatturaElettronicaBody

        public virtual FatturaElettronicaBodyType FatturaElettronicaBody
        {
            get { return FatturaPa.FatturaElettronicaBody; }

            set { FatturaPa.FatturaElettronicaBody = value; }
        }

        public virtual DatiPagamentoType[] DatiPagamento
        {
            get { return FatturaElettronicaBody.DatiPagamento; }
            set { FatturaElettronicaBody.DatiPagamento = value; }
        }

        public virtual DatiBeniServiziType DatiBeniServizi
        {
            get
            {
                return FatturaElettronicaBody.DatiBeniServizi;
            }
            set { FatturaElettronicaBody.DatiBeniServizi = value; }
        }

        public virtual DettaglioLineeType[] DettaglioLinee
        {
            get { return DatiBeniServizi?.DettaglioLinee; }
            set
            {
                if ( DatiBeniServizi  == null)
                    DatiBeniServizi = new DatiBeniServiziType();
                DatiBeniServizi.DettaglioLinee = value;
            }
        }

        //public virtual AllegatiType[] Allegati
        //{
        //    get { return FatturaElettronicaBody.Allegati; }
        //    set { FatturaElettronicaBody.Allegati = value; }
        //}

        //public virtual DatiRiepilogoType[] DatiRiepilogo
        //{
        //    get { return DatiBeniServizi.DatiRiepilogo; }
        //    set { DatiBeniServizi.DatiRiepilogo = value; }
        //}

        #endregion

        
        public virtual void Init()
        {
            DataCaricamentoDB = DateTime.Now;
            FatturaPa = new FatturaElettronicaType
            {
                FatturaElettronicaBody = new FatturaElettronicaBodyType
                {
                    DatiGenerali = new DatiGeneraliType
                    {
                        DatiGeneraliDocumento = new DatiGeneraliDocumentoType() {Data = DataFatturaDB}
                    },
                    DatiBeniServizi = new DatiBeniServiziType()
                },
                FatturaElettronicaHeader = new FatturaElettronicaHeaderType()
                {
                    CessionarioCommittente = new CessionarioCommittenteType()
                    {
                        DatiAnagrafici = new DatiAnagraficiCessionarioType()
                        {
                            Anagrafica = new AnagraficaType()
                        }
                    },
                    CedentePrestatore = new CedentePrestatoreType()
                    {
                        DatiAnagrafici = new DatiAnagraficiCedenteType()
                        {
                            Anagrafica = new AnagraficaType()
                        }
                    }
                } 
            };

            DatiGeneraliDocumento.Divisa = "EUR";
            
        }

        public override PropertyChangedEventHandler PropertyChangedEventHandler => OnPropChanged;
        
        private void OnPropChanged( object sender, PropertyChangedEventArgs e )
        {
            if (FatturaPa == null)
                return;

            IsValidating = false;

            if ( e.PropertyName == nameof( FormatoTrasmissioneDB ) )
            {
                DatiTrasmissione.FormatoTrasmissione = FormatoTrasmissioneDB;
            }

            if (e.PropertyName == nameof(NumeroFatturaDB))
            {
                DatiGeneraliDocumento.Numero = NumeroFatturaDB;
            }

            if (e.PropertyName == nameof(DataFatturaDB))
            {
                DatiGeneraliDocumento.Data = DataFatturaDB;
            }

            if (e.PropertyName == nameof(TotaleFatturaDB))
            {
                if (string.IsNullOrWhiteSpace(DatiGeneraliDocumento.Divisa))
                {
                    DatiGeneraliDocumento.Divisa = "EUR";
                }
                DatiGeneraliDocumento.ImportoTotaleDocumento = decimal.Parse( string.Format( "{0:0.00}", TotaleFatturaDB ) );
                //TotaleFatturaDB.ToString(CultureInfo.InvariantCulture) ;
            }

            if (e.PropertyName == nameof(AnagraficaCedenteDB))
                SyncFornitore();

            if (e.PropertyName == nameof(AnagraficaCommittenteDB))
            {
                SyncCommittente();
            }
            IsValidating = true;

        }


        private void SyncCommittente()
        {
            if ( AnagraficaCommittenteDB == null ) return;

            var anag = new AnagraficaType
            {
                Denominazione = AnagraficaCommittenteDB.Denominazione,
                Cognome = AnagraficaCommittenteDB.Cognome,
                Nome = AnagraficaCommittenteDB.Nome
            };

            CessionarioCommittente.Sede = SyncSede(AnagraficaCommittenteDB );
            //if ( CessionarioCommittente.DatiAnagrafici == null )
            //{
                CessionarioCommittente.DatiAnagrafici = new DatiAnagraficiCessionarioType();
            //}
            CessionarioCommittente.DatiAnagrafici.Anagrafica = anag;

            if ( !string.IsNullOrWhiteSpace(AnagraficaCommittenteDB.CodiceFiscale) )
                CessionarioCommittente.DatiAnagrafici.CodiceFiscale = AnagraficaCommittenteDB.CodiceFiscale;

            CessionarioCommittente.DatiAnagrafici.IdFiscaleIVA = new IdFiscaleType()
            {
                IdCodice = AnagraficaCommittenteDB.PIva,
                IdPaese = AnagraficaCommittenteDB.Nazione
            };

            if ( FormatoTrasmissioneDB == FormatoTrasmissioneType.FPR12 &&
                 string.IsNullOrWhiteSpace( DatiTrasmissione.PECDestinatario ) )
            {
                DatiTrasmissione.PECDestinatario = PecDestinatarioDB;
            }
        }

        private void SyncFornitore()
        {
            if ( AnagraficaCedenteDB == null ) return;

            var anag = new AnagraficaType
            {
                Denominazione = AnagraficaCedenteDB.Denominazione,
                Cognome = string.IsNullOrWhiteSpace(AnagraficaCedenteDB.Cognome) ? null : AnagraficaCedenteDB.Cognome,
                Nome = string.IsNullOrWhiteSpace(AnagraficaCedenteDB.Nome) ? null : AnagraficaCedenteDB.Nome,
            };

            CedenteFornitore.Sede = SyncSede(AnagraficaCedenteDB);
            if ( CedenteFornitore.DatiAnagrafici == null )
            {
                CedenteFornitore.DatiAnagrafici = new DatiAnagraficiCedenteType();
            }
            CedenteFornitore.DatiAnagrafici.Anagrafica = anag;
            CedenteFornitore.DatiAnagrafici.CodiceFiscale = AnagraficaCedenteDB.CodiceFiscale;
            CedenteFornitore.DatiAnagrafici.RegimeFiscale = RegimeFiscale;
            CedenteFornitore.DatiAnagrafici.IdFiscaleIVA = new IdFiscaleType()
            {
                IdCodice = AnagraficaCedenteDB.PIva,
                IdPaese = AnagraficaCedenteDB.Nazione
            };

            if ( string.IsNullOrWhiteSpace( RiferimentoAmmDB ) &&
                 !string.IsNullOrWhiteSpace( CedenteFornitore.RiferimentoAmministrazione ) )
                CedenteFornitore.RiferimentoAmministrazione = RiferimentoAmmDB;
        }

        private void SetTrasmittente() {          

            FatturaElettronicaHeader.DatiTrasmissione = new DatiTrasmissioneType
            {

                FormatoTrasmissione = FormatoTrasmissioneDB,
                ProgressivoInvio = ProgFile.ToString("00000")
            };

            if ( FormatoTrasmissioneDB == FormatoTrasmissioneType.FPR12 )
            {
                FatturaElettronicaHeader.DatiTrasmissione.CodiceDestinatario = AnagraficaCommittenteDB.CodSoggettoSDI;
                
                if (FatturaElettronicaHeader.DatiTrasmissione.CodiceDestinatario== "0000000")
                {
                    FatturaElettronicaHeader.DatiTrasmissione.PECDestinatario = PecDestinatarioDB;
                }
            }
            else{
                FatturaElettronicaHeader.DatiTrasmissione.CodiceDestinatario = AnagraficaCommittenteDB.CodUfficioPa;
                FatturaElettronicaHeader.DatiTrasmissione.CodiceDestinatario = CodUfficioDB;
            }
       
            if ( AnagraficaCedenteDB != null )
            {
                FatturaElettronicaHeader.DatiTrasmissione.IdTrasmittente = new IdFiscaleType
                {
                    IdCodice = AnagraficaCedenteDB.CodiceFiscale,
                    IdPaese = AnagraficaCedenteDB.Nazione
                };
            }

            SyncFornitore();
            SyncCommittente();
        }

        private static IndirizzoType SyncSede( Anagrafica source )
        {
            var dest = new IndirizzoType
            {
                CAP = source.Cap,
                Comune = source.Comune,
                Indirizzo = source.Indirizzo,
                NumeroCivico = source.Civico,
                Provincia = source.Provincia,
                Nazione = source.Nazione
            };
            return dest;
        }

        public virtual void SyncFatturaPa()
        {
            DatiTrasmissione.FormatoTrasmissione = FormatoTrasmissioneDB;
            DatiGeneraliDocumento.Numero = NumeroFatturaDB;
            DatiGeneraliDocumento.Data = DataFatturaDB;
            if (string.IsNullOrWhiteSpace(DatiGeneraliDocumento.Divisa))
            {
                DatiGeneraliDocumento.Divisa = "EUR";
            }
            DatiGeneraliDocumento.ImportoTotaleDocumento = decimal.Parse(string.Format("{0:0.00}", TotaleFatturaDB));

            SetTrasmittente();
            UpdateRiepilogoIva();
            CedenteFornitore.RiferimentoAmministrazione = RiferimentoAmmDB;
        }

        private void UpdateRiepilogoIva()
        {
            if ( DettaglioLinee == null || !DettaglioLinee.Any() ) return;

            var sum = DettaglioLinee.Sum(i=>i.PrezzoTotale);
            if ( CassaPrevidenziale != null ){
                CassaPrevidenziale.ImportoContributoCassa = sum * ( CassaPrevidenziale.AlCassa / 100 );
            }
            

            var riepilogo = DettaglioLinee.GroupBy( k => new { A= k.AliquotaIVA, N= k.Natura}, g => g).
                ToDictionary(k => k.Key, g => g.Sum(i=>i.PrezzoTotale));
            DatiBeniServizi.DatiRiepilogo = new DatiRiepilogoType[ riepilogo.Count ];
            int x = 0;
            decimal constnt = 100;
            foreach (var item in riepilogo)
            {
                var riepilogoAliquota = new DatiRiepilogoType
                {
                    AliquotaIVA = item.Key.A,
                    Natura = item.Key.N,
                    ImponibileImporto = item.Value,
                    Imposta = item.Value * ( item.Key.A / constnt ),
                    EsigibilitaIVA = EsigibilitaIVAType.N,
                    ArrotondamentoSpecified = false
                };
                DatiBeniServizi.DatiRiepilogo[x++] = riepilogoAliquota;
            }

            if( CassaPrevidenziale != null && DatiBeniServizi.DatiRiepilogo.Any())
            {
                DatiBeniServizi.DatiRiepilogo.Last().ImponibileImporto += CassaPrevidenziale.ImportoContributoCassa;
            }
        }


        public override DomainResult Validate()
        {
            //DomainResultFatturaPA = new DomainResult( new Dictionary<string, IEnumerable<string>>()
            //{ { nameof(FatturaPa), new[] { resultFatturaPa } } } );

            var err = new Dictionary<string, List<string>>();

            //omitt prop name to force validation al instance level
            GetPersistentErrors( err );

            DomainResult = new DomainResult( !err.Any(), err );

            return DomainResult;
        }

        public override DomainResult ValidatePropertyValue( string prop )
        {
            var err = new Dictionary<string, List<string>>();
            
            GetPersistentErrors( err );

            DomainResult = new DomainResult( !err.Any(), err );

            return DomainResult;

        }

        protected virtual string GetXmlStream()
        {
            var unProxy = ObjectExplorer.UnProxiedDeepCopy( FatturaPa );
            return SerializerHelpers.ObjectToXml( ( FatturaElettronicaType ) unProxy );
        }

        public virtual XmlDocument GetXmlDocFatturaPA( )
        {
            var xmlData = GetXmlStream() ;
            var doc = new XmlDocument();
            doc.LoadXml(xmlData);

            var xslPath =  DatiTrasmissione.FormatoTrasmissione == FormatoTrasmissioneType.FPA12 
                ? StoreAccess.FatturaPaXslPaSchema : StoreAccess.FatturaPaXslOrdSchema;

            doc.InsertBefore(doc.CreateProcessingInstruction("xml-stylesheet",
                $"type=\"text/xsl\" href=\"{xslPath}\"" ), doc.DocumentElement);

            return doc;
        }

        public virtual string ValidateByXsdFatturaPA(  )
        {
            var xmlStream = GetXmlStream();
            var document = XDocument.Parse( xmlStream );
            var sb = new StringBuilder();
            document.Validate( SerializerHelpers.FatturaPaXmlSchema, ( o, e ) =>
            {
                sb.Append( e.Message );
            } );
            return sb.ToString();
        }


        bool IFlyFetch.TryUnproxyFlyFetch
        {
            get
            {
                var forceToLoad = NumeroFatturaDB;
                return Id != 0;
            } 
        }
        
        public virtual Fattura Copy()
        {
            var copy = new Fattura(); 
            
            copy.FatturaPa = ( FatturaElettronicaType ) ObjectExplorer.UnProxiedDeepCopy( this.FatturaPa );
            copy.AnagraficaCedenteDB = ( Anagrafica ) AnagraficaCedenteDB.Unproxy();
            copy.AnagraficaCommittenteDB = ( Anagrafica ) AnagraficaCommittenteDB.Unproxy();
            copy.DomainResult = new DomainResult( DomainResult.Success, DomainResult.Errors );
            copy.NumeroFatturaDB = string.Copy( NumeroFatturaDB );
            copy.DataFatturaDB = DataFatturaDB;
            copy.TotaleFatturaDB = TotaleFatturaDB;
            copy.CigDB = CigDB == null ? null : string.Copy( CigDB );
            copy.CupDB = CupDB == null ? null : string.Copy( CupDB );
            copy.CodUfficioDB = CodUfficioDB == null ? null : string.Copy( CodUfficioDB );
            copy.PecDestinatarioDB = PecDestinatarioDB == null ? null : string.Copy( PecDestinatarioDB );
            copy.DataCaricamentoDB = DateTime.Now.Date;
            copy.RiferimentoAmmDB = RiferimentoAmmDB == null ? null : string.Copy( RiferimentoAmmDB );
            copy.FormatoTrasmissioneDB = FormatoTrasmissioneDB;
            copy.ProgFile = 0;
            copy.Version = 0;
            return copy;
        }

    }
}
