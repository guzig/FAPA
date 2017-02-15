﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using FaPA.Core.FaPa;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
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

        public virtual DateTime DataFatturaDB { get; set; }

        public virtual decimal TotaleFatturaDB { get; set; }

        public virtual string NumeroFatturaDB { get; set; }

        public virtual string RiferimentoAmmDB { get; set; }

        public virtual int ProgFile { get; set; }

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
            get { return DatiBeniServizi.DettaglioLinee; }
            set
            {
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
            if ( FatturaPa == null )
                return;       

            if ( e.PropertyName == nameof(NumeroFatturaDB))
            {
                DatiGeneraliDocumento.Numero = NumeroFatturaDB;
            }

            if (e.PropertyName == nameof(DataFatturaDB))
            {
                DatiGeneraliDocumento.Data = DataFatturaDB;
            }

            if (e.PropertyName == nameof(TotaleFatturaDB))
            {
                if ( string.IsNullOrWhiteSpace( DatiGeneraliDocumento.Divisa ) )
                {
                    DatiGeneraliDocumento.Divisa = "EUR";
                }
                DatiGeneraliDocumento.ImportoTotaleDocumento = decimal.Parse(string.Format("{0:###0.00}", TotaleFatturaDB));
            }

            if ( e.PropertyName == nameof( AnagraficaCedenteDB ) )
                SyncFornitore();

            if ( e.PropertyName == nameof( AnagraficaCommittenteDB ) )
            {
                IsValidating = false;
                SyncCommittente();
                IsValidating = true;
            }
        }


        private void SyncCommittente()
        {
            var anag = new AnagraficaType
            {
                Denominazione = AnagraficaCommittenteDB.Denominazione,
                Cognome = AnagraficaCommittenteDB.Cognome,
                Nome = AnagraficaCommittenteDB.Nome
            };

            CessionarioCommittente.Sede = SyncSede(AnagraficaCommittenteDB );
            if ( CessionarioCommittente.DatiAnagrafici == null )
            {
                CessionarioCommittente.DatiAnagrafici = new DatiAnagraficiCessionarioType();
            }
            CessionarioCommittente.DatiAnagrafici.Anagrafica = anag;
            CessionarioCommittente.DatiAnagrafici.CodiceFiscale = AnagraficaCommittenteDB.CodiceFiscale;
        }

        private void SyncFornitore()
        {
            var anag = new AnagraficaType
            {
                Denominazione = AnagraficaCedenteDB.Denominazione,
                Cognome = AnagraficaCedenteDB.Cognome,
                Nome = AnagraficaCedenteDB.Nome
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

        private void SetTrasmittente()
        {          
            FatturaElettronicaHeader.DatiTrasmissione = new DatiTrasmissioneType
            {
                CodiceDestinatario = CodUfficioDB,
                FormatoTrasmissione = FormatoTrasmissioneType.SDI11,
                ProgressivoInvio = ProgFile.ToString("00000")
            };
       
            if ( AnagraficaCedenteDB != null )
            {
                FatturaElettronicaHeader.DatiTrasmissione.IdTrasmittente = new IdFiscaleType
                {
                    IdCodice = AnagraficaCedenteDB.CodiceFiscale,
                    IdPaese = AnagraficaCedenteDB.Nazione
                };
            }

            if ( AnagraficaCommittenteDB == null ) return;

            CessionarioCommittente.DatiAnagrafici.CodiceFiscale = string.IsNullOrWhiteSpace(AnagraficaCommittenteDB.CodiceFiscale ) ?
                AnagraficaCommittenteDB.PIva : AnagraficaCommittenteDB.CodiceFiscale;

            CessionarioCommittente.DatiAnagrafici.IdFiscaleIVA = new IdFiscaleType()
            {
                IdCodice = AnagraficaCommittenteDB.PIva,
                IdPaese = AnagraficaCommittenteDB.Nazione
            };
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
            SetTrasmittente();
            UpdateRiepilogoIva();
            CedenteFornitore.RiferimentoAmministrazione = RiferimentoAmmDB;
        }

        private void UpdateRiepilogoIva()
        {
            if ( DettaglioLinee == null || !DettaglioLinee.Any() ) return;

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
        }


        public override DomainResult Validate()
        {
            //DomainResultFatturaPA = new DomainResult( new Dictionary<string, IEnumerable<string>>()
            //{ { nameof(FatturaPa), new[] { resultFatturaPa } } } );

            var err = new Dictionary<string, IEnumerable<string>>();

            //if ( !string.IsNullOrWhiteSpace( resultFatturaPa ) )
            //{
            //    err.Add( nameof( FatturaPa ), new[] { resultFatturaPa } );
            //}

            GetPersistentErrors( err );

            DomainResult = new DomainResult( !err.Any(), err );

            return DomainResult;
        }

        public override DomainResult ValidatePropertyValue( string prop )
        {
            var err = new Dictionary<string, IEnumerable<string>>();

            //if ( !string.IsNullOrWhiteSpace( resultFatturaPa ) )
            //{
            //    err.Add( nameof( FatturaPa ), new[] { resultFatturaPa } );
            //}

           
            GetPersistentErrors( err, prop );

            DomainResult = new DomainResult( !err.Any(), err );

            return DomainResult;

        }

        public virtual XmlDocument GetXmlFatturaPA( )
        {
            var xmlData = GetXmlStream();
            var doc = new XmlDocument();
            doc.LoadXml(xmlData);
            doc.InsertBefore(doc.CreateProcessingInstruction("xml-stylesheet",
                "type=\"text/xsl\" href=\"fatturapa_v1.1.xsl\""), doc.DocumentElement);
            return doc;
        }

        protected virtual string GetXmlStream()
        {
            var unProxy = ObjectExplorer.UnProxiedDeepCopy( FatturaPa );
            return SerializerHelpers.ObjectToXml( ( FatturaElettronicaType ) unProxy );
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
            var other = ( Fattura ) this.MemberwiseClone();
            other.Id = 0;

            other.FatturaPa = FatturaPa.DeepCopy();
            AnagraficaCedenteDB = ( Anagrafica ) other.AnagraficaCedenteDB.Unproxy();
            AnagraficaCommittenteDB = ( Anagrafica ) other.AnagraficaCommittenteDB.Unproxy();
            other.DomainResult = new DomainResult( DomainResult.Success, DomainResult.Errors );
            other.NumeroFatturaDB = string.Copy( NumeroFatturaDB );
            other.CigDB = CigDB == null ? null : string.Copy( CigDB );
            other.CupDB = CupDB == null ? null : string.Copy( CupDB );
            other.CodUfficioDB = CodUfficioDB == null ? null : string.Copy( CodUfficioDB );
            other.RiferimentoAmmDB = RiferimentoAmmDB == null ? null : string.Copy( RiferimentoAmmDB );
            other.ProgFile = 0;
            other.Version = 0;
            return other;
        }

    }
}
