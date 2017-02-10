﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using FaPA.Core.FaPa;
using System.Linq;
using System.Xml;
using FaPA.AppServices.CoreValidation;

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

        public virtual string NoteDB { get; set; }

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
                if (FatturaElettronicaBody.DatiBeniServizi == null)
                {
                    FatturaElettronicaBody.DatiBeniServizi = new DatiBeniServiziType();
                }

                return FatturaElettronicaBody.DatiBeniServizi;
            }
        }

        public virtual DettaglioLineeType[] DettaglioLinee
        {
            get { return DatiBeniServizi.DettaglioLinee; }
            set { DatiBeniServizi.DettaglioLinee = value; }
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
                        DatiGeneraliDocumento = new DatiGeneraliDocumentoType()
                    }
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
                DatiGeneraliDocumento.ImportoTotaleDocumento = decimal.Parse(string.Format("{0:###0.00}", TotaleFatturaDB));
            }

            if ( e.PropertyName == nameof( AnagraficaCedenteDB ) )
                SyncFornitore();

            if ( e.PropertyName == nameof( AnagraficaCommittenteDB ) )
                SyncCommittente();
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
            //CedenteFornitore.DatiAnagrafici.RegimeFiscale = RegimeFiscaleType.RF01;
            CedenteFornitore.DatiAnagrafici.IdFiscaleIVA = new IdFiscaleType()
            {
                IdCodice = AnagraficaCedenteDB.PIva,
                IdPaese = "IT"
            };
        }

        //private void SyncDatiGeneraliDocumento()
        //{
        //    DatiGeneraliDocumento.Divisa = "EUR";
        //    DatiGeneraliDocumento.Numero = NumeroFatturaDB;
        //    DatiGeneraliDocumento.Data = DataFatturaDB;
        //    DatiGeneraliDocumento.ImportoTotaleDocumento = decimal.Parse( string.Format( "##.000", TotaleFatturaDB ) );
        //    //Causale = new string[] { "causale" } ,
        //}

        private void SetTrasmittente()
        {          
            FatturaElettronicaHeader.DatiTrasmissione = new DatiTrasmissioneType
            {
                CodiceDestinatario = CodUfficioDB,
                FormatoTrasmissione = new FormatoTrasmissioneType(),
                ProgressivoInvio = ProgFile.ToString("00000")
            };
            
            if ( AnagraficaCedenteDB != null )
                FatturaElettronicaHeader.DatiTrasmissione.IdTrasmittente = new IdFiscaleType
                {
                    IdCodice = AnagraficaCedenteDB.CodiceFiscale,
                    IdPaese = "IT"
                };

            if ( AnagraficaCommittenteDB == null ) return;

            CessionarioCommittente.DatiAnagrafici.CodiceFiscale = string.IsNullOrWhiteSpace(AnagraficaCommittenteDB.CodiceFiscale ) ?
                AnagraficaCommittenteDB.PIva : AnagraficaCommittenteDB.CodiceFiscale;

            CessionarioCommittente.DatiAnagrafici.IdFiscaleIVA = new IdFiscaleType()
            {
                IdCodice = AnagraficaCommittenteDB.PIva,
                IdPaese = "IT"
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
            DatiTrasmissione.ProgressivoInvio = ProgFile.ToString("00000");
            SetTrasmittente();
            UpdateRiepilogoIva();
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
            
            //var resultFatturaPa = SerializerHelpers.ValidateFatturaPA(this);

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

        public virtual XmlDocument GetXmlDocument( )
        {
            var proxy = ObjectExplorer.UnProxiedDeep( FatturaPa );
            var xmlData = SerializerHelpers.ObjectToXml( (FatturaElettronicaType) proxy );
            //var document = XDocument.Parse(xmlData);
            var doc = new XmlDocument();
            doc.LoadXml(xmlData);
            doc.InsertBefore(doc.CreateProcessingInstruction("xml-stylesheet",
                "type=\"text/xsl\" href=\"fatturapa_v1.1.xsl\""), doc.DocumentElement);
            return doc;
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

            var fatturaElettronicaType = ObjectExplorer.UnProxiedDeep( FatturaPa );
            var xmlStream = SerializerHelpers.ObjectToXml( ( FatturaElettronicaType ) fatturaElettronicaType );
            other.FatturaPa = SerializerHelpers.XmlToObject( xmlStream );

            other.DomainResult = new DomainResult( DomainResult.Success, DomainResult.Errors );
            other.NumeroFatturaDB = string.Copy( NumeroFatturaDB );
            other.CigDB = CigDB == null ? null : string.Copy( CigDB );
            other.CupDB = CupDB == null ? null : string.Copy( CupDB );
            other.CodUfficioDB = CodUfficioDB == null ? null : string.Copy( CodUfficioDB );
            other.NoteDB = NoteDB == null ? null : string.Copy( NoteDB );
            other.ProgFile = 0;
            other.Version = 0;
            return other;
        }

    }
}
