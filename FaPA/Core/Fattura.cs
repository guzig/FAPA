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
        
        public virtual FatturaElettronicaHeaderType FatturaElettronicaHeader
        {
            get
            {
                return FatturaPa.FatturaElettronicaHeader;
            }
            set { FatturaPa.FatturaElettronicaHeader = value; }
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
            set
            {
                FatturaPa.FatturaElettronicaHeader.DatiTrasmissione = value;
            }
        }


        public virtual FatturaElettronicaBodyType FatturaElettronicaBody
        {
            get
            {
                return FatturaPa.FatturaElettronicaBody;
            }

            set { FatturaPa.FatturaElettronicaBody = value; }
        }

        public virtual DatiPagamentoType[] DatiPagamento
        {
            get
            {
                return FatturaElettronicaBody.DatiPagamento;
            }
            set { FatturaElettronicaBody.DatiPagamento = value; }
        }

        public virtual DatiBeniServiziType DatiBeniServizi
        {
            get
            {
                if ( FatturaElettronicaBody.DatiBeniServizi == null )
                {
                    FatturaElettronicaBody.DatiBeniServizi = new DatiBeniServiziType();
                }

                return FatturaElettronicaBody.DatiBeniServizi;
            }
        }

        public virtual DettaglioLineeType[] DettaglioLinee
        {
            get
            {
                return DatiBeniServizi.DettaglioLinee;
            }
            set
            {
                DatiBeniServizi.DettaglioLinee = value;
            }
        }


        public virtual DatiGeneraliType DatiGenerali
        {
            get
            {
                return FatturaPa.FatturaElettronicaBody.DatiGenerali;
            }
            set { FatturaPa.FatturaElettronicaBody.DatiGenerali = value; }
        }
        
        public virtual DatiDocumentiCorrelatiType[] DatiConvenzione
        {
            get
            {
                return DatiGenerali.DatiConvenzione;
            }
            set { DatiGenerali.DatiConvenzione = value; }
        }

        public virtual DatiDocumentiCorrelatiType[] DatiContratto
        {
            get
            {
                return DatiGenerali.DatiContratto;
            }
            set { DatiGenerali.DatiContratto = value; }
        }

        public virtual DatiDocumentiCorrelatiType[] DatiOrdineAcquisto
        {
            get
            {
                return DatiGenerali.DatiOrdineAcquisto;
            }
            set { DatiGenerali.DatiOrdineAcquisto = value; }
        }

        public virtual DatiDocumentiCorrelatiType[] DatiRicezione
        {
            get
            {
                return DatiGenerali.DatiRicezione;
            }
            set
            {
                DatiGenerali.DatiRicezione = value;
            }
        }

        public virtual DatiDocumentiCorrelatiType[] DatiFattureCollegate
        {
            get
            {
                return DatiGenerali.DatiFattureCollegate;
            }
            set
            {
                DatiGenerali.DatiFattureCollegate = value;
            }
        }

        public virtual DatiSALType[] DatiSAL
        {
            get { return DatiGenerali.DatiSAL; } 
            set { DatiGenerali.DatiSAL=value;}
        }

        public virtual DatiDDTType[] DatiDDT
        {
            get
            {
                return DatiGenerali.DatiDDT;
            }
            set
            {
                DatiGenerali.DatiDDT = value;
            }
        }

        public virtual DatiTrasportoType DatiTrasporto
        {
            get
            {
                return DatiGenerali.DatiTrasporto;
            }
            set
            {
                DatiGenerali.DatiTrasporto = value;
            }
        }

        public virtual FatturaPrincipaleType FatturaPrincipale
        {
            get
            {
                return DatiGenerali.FatturaPrincipale;
            }
            set
            {
                DatiGenerali.FatturaPrincipale = value;
            }
        }



        public virtual DatiGeneraliDocumentoType DatiGeneraliDocumento
        {
            get
            {
                return FatturaPa.FatturaElettronicaBody.DatiGenerali.DatiGeneraliDocumento;
            }

            set { FatturaPa.FatturaElettronicaBody.DatiGenerali.DatiGeneraliDocumento = value; }
        }

        public virtual DatiRitenutaType Ritenuta
        {
            get
            {
                return DatiGeneraliDocumento.DatiRitenuta;
            }

            set
            {
                DatiGeneraliDocumento.DatiRitenuta = value;
            }
        }

        public virtual ScontoMaggiorazioneType[] ScontoMaggiorazione
        {
            get
            {
                return DatiGeneraliDocumento.ScontoMaggiorazione;
            }

            set { DatiGeneraliDocumento.ScontoMaggiorazione = value; }
        }

        public virtual DatiCassaPrevidenzialeType CassaPrevidenziale
        {
            get
            {
                return DatiGeneraliDocumento.DatiCassaPrevidenziale;
            }

            set
            {
                DatiGeneraliDocumento.DatiCassaPrevidenziale = value;
            }
        }





        public virtual CedentePrestatoreType CedenteFornitore
        {
            get
            {
                if (FatturaPa.FatturaElettronicaHeader == null)
                {
                    FatturaPa.FatturaElettronicaHeader = new FatturaElettronicaHeaderType();
                }

                if ( FatturaPa.FatturaElettronicaHeader.CedentePrestatore == null )
                {
                    FatturaPa.FatturaElettronicaHeader.CedentePrestatore = new CedentePrestatoreType();
                }

                return FatturaPa.FatturaElettronicaHeader.CedentePrestatore;
            }
        }

        public virtual AnagraficaType AnagraficaCedenteFornitore
        {
            get
            {
                if ( CedenteFornitore.DatiAnagrafici == null )
                {
                    CedenteFornitore.DatiAnagrafici = new DatiAnagraficiCedenteType();
                    CedenteFornitore.DatiAnagrafici.Anagrafica = new AnagraficaType();
                    return CedenteFornitore.DatiAnagrafici.Anagrafica;
                }
                if ( CedenteFornitore.DatiAnagrafici.Anagrafica == null )
                {
                    CedenteFornitore.DatiAnagrafici.Anagrafica = new AnagraficaType();
                    return CedenteFornitore.DatiAnagrafici.Anagrafica;
                }
                return CedenteFornitore.DatiAnagrafici.Anagrafica;
            }
        }

        public virtual RegimeFiscaleType RegimeFiscale
        {
            get { return CedenteFornitore.DatiAnagrafici.RegimeFiscale; }
            set { CedenteFornitore.DatiAnagrafici.RegimeFiscale = value; }
        }



        public virtual CessionarioCommittenteType CessionarioCommittente
        {
            get
            {
                if (FatturaPa.FatturaElettronicaHeader == null)
                {
                    FatturaPa.FatturaElettronicaHeader = new FatturaElettronicaHeaderType();
                }

                return FatturaPa.FatturaElettronicaHeader.CessionarioCommittente ??
                       (FatturaPa.FatturaElettronicaHeader.CessionarioCommittente = new CessionarioCommittenteType());
            }
        }

        public virtual AnagraficaType AnagraficaCessionarioCommittente
        {
            get
            {
                if ( CessionarioCommittente.DatiAnagrafici == null )
                {
                    CessionarioCommittente.DatiAnagrafici = new DatiAnagraficiCessionarioType();
                    CessionarioCommittente.DatiAnagrafici.Anagrafica = new AnagraficaType();
                    return CessionarioCommittente.DatiAnagrafici.Anagrafica;
                }

                if ( CessionarioCommittente.DatiAnagrafici.Anagrafica == null )
                {
                    CessionarioCommittente.DatiAnagrafici.Anagrafica = new AnagraficaType();
                    return CessionarioCommittente.DatiAnagrafici.Anagrafica;
                }
                return CessionarioCommittente.DatiAnagrafici.Anagrafica;
            }
        }

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
                }
            };
            
            DatiGeneraliDocumento.Divisa = "EUR";

            //getLazyInstnce = fattura.DatiRiepilogo;
            object getLazyInstnce = DettaglioLinee;
            getLazyInstnce = AnagraficaCedenteFornitore;
            getLazyInstnce = AnagraficaCessionarioCommittente;
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

        public virtual void SetTrasmittente()
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

        public virtual XmlDocument GetXmlDocument( )
        {
            HydrateFatturaPa();
            var proxy = ObjectExplorer.UnProxiedDeep( FatturaPa );
            var xmlData = SerializerHelpers.ObjectToXml( (FatturaElettronicaType) proxy );
            //var document = XDocument.Parse(xmlData);
            var doc = new XmlDocument();
            doc.LoadXml(xmlData);
            doc.InsertBefore(doc.CreateProcessingInstruction("xml-stylesheet",
                "type=\"text/xsl\" href=\"fatturapa_v1.1.xsl\""), doc.DocumentElement);
            return doc;
        }

        private void HydrateFatturaPa()
        {
            SetTrasmittente();
            HydrateRiepilogo();
        }

        private void HydrateRiepilogo()
        {
            if ( DettaglioLinee == null || !DettaglioLinee.Any() ) return;

            var riepilogo = DettaglioLinee.GroupBy(k => k.AliquotaIVA, g => g).
                ToDictionary(k => k.Key, g => g.Sum(i=>i.PrezzoTotale));
            DatiBeniServizi.DatiRiepilogo = new DatiRiepilogoType[ riepilogo.Count ];
            int x = 0;
            decimal constnt = 100;
            foreach (var item in riepilogo)
            {
                var riepilogoAliquota = new DatiRiepilogoType
                {
                    AliquotaIVA = item.Key,
                    ImponibileImporto = item.Value,
                    Imposta = item.Value * ( item.Key / constnt ),
                    EsigibilitaIVA = EsigibilitaIVAType.S,
                    ArrotondamentoSpecified = false,
                    EsigibilitaIVASpecified = true,
                    RiferimentoNormativo= "IVA non soggetta: regime fiscale forfettario ai sensi dell'art.1 L. 190/2014"
                };
                DatiBeniServizi.DatiRiepilogo[x++] = riepilogoAliquota;
            }
        }

        public override DomainResult Validate()
        {
            HydrateFatturaPa();
            
            //DomainResult = new DomainResult( false, new Dictionary<string, IEnumerable<string>>()
            //{
            //    {"NumeroFatturaDB", new [] {"a"} },
            //    {"DataFatturaDB", new [] {"a"} } 
            //} );

            //var resultFatturaPa = SerializerHelpers.ValidateFatturaPA(this);

            var err = new Dictionary<string, IEnumerable<string>>();

            //if ( !string.IsNullOrWhiteSpace( resultFatturaPa ) )
            //{
            //    err.Add( nameof( FatturaPa ), new[] { resultFatturaPa } );
            //}

            GetPersistentErrors( err );

            DomainResult = new DomainResult(!err.Any(), err);

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
        
        bool IFlyFetch.TryUnproxyFlyFetch
        {
            get
            {
                var forceToLoad = NumeroFatturaDB;
                return Id != 0;
            } 
        }


    }
}
