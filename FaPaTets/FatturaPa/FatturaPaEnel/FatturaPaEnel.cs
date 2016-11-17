using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Emule.Core;
using EmulTests.DomainServices.FatturaPa.FatturaPa_11;
using NUnit.Framework;

namespace EmulTests.DomainServices.FatturaPa.FatturaPaEnel
{
    class FatturaPaEnel
    {
        [Test]
        public void CanDeserializeFatturaElettronicaV11_Enel()
        {
            var nomeFile = Environment.GetFolderPath( Environment.SpecialFolder.DesktopDirectory )
                           + @"\EM\TestData\XSD-Fattura\eFattPaEnel\IT06655971007_1AVN.xml";

            var fattura1 = XsdFatturaElettronicaType_V11_Tests.GetObject( nomeFile );

            var bodyTypes = fattura1.FatturaElettronicaBody;

            foreach ( var fatturaElettronicaBodyType in bodyTypes )
            {
                //DatiGeneraliType datiGenerali = fatturaElettronicaBodyType.DatiGenerali;

                //DatiPagamentoType[] datiPagamentoField

                //AllegatiType[] allegatiField

                var dettaglioLinee = fatturaElettronicaBodyType.DatiBeniServizi.DettaglioLinee;

                var fa = fatturaElettronicaBodyType.Allegati;

                if ( fa != null && fa.Any() )
                {
                    var xc = fa[0].Attachment;
                    K(xc);
                }


                foreach ( var dettaglioLinea in dettaglioLinee )
                {
                    var dettaglio = new DettaglioFattura();

                    //dettaglio.CausaleIva
                    var f1 = dettaglioLinea.AliquotaIVA;

                    //dettaglio.Causale
                    var f3 = dettaglioLinea.CodiceArticolo;
                    var f6 = dettaglioLinea.Descrizione;

                    dettaglio.PeriodoFatturazione = PeriodoFatturazione.TryParsePeriodo(
                        dettaglioLinea.DataInizioPeriodo, dettaglioLinea.DataInizioPeriodo );

                    dettaglio.NumeroLinea = dettaglioLinea.NumeroLinea;
                    dettaglio.Prezzo = ( float )dettaglioLinea.PrezzoUnitario;
                    dettaglio.SetQuantità ( ( float )dettaglioLinea.Quantita );
                    //dettaglio.UnitàMisura = dettaglioLinea.UnitaMisura;
                    //dettaglio.Importo = ( float ) dettaglioLinea.PrezzoTotale;


                    var f7 = dettaglioLinea.Natura;

                    var f12 = dettaglioLinea.RiferimentoAmministrazione;
                    var f13 = dettaglioLinea.Ritenuta;
                    var f14 = dettaglioLinea.ScontoMaggiorazione;
                    var f15 = dettaglioLinea.TipoCessionePrestazione;

                    var f2 = dettaglioLinea.AltriDatiGestionali;

                    if (f2 != null && f2.Any())
                    {
                        var xc = f2[0].RiferimentoTesto;
                    }

                }

                var riepilogoLinee = fatturaElettronicaBodyType.DatiBeniServizi.DatiRiepilogo;

                foreach ( var riepilogoLinea in riepilogoLinee )
                {
                    var f1 = riepilogoLinea.AliquotaIVA;
                    var f2 = riepilogoLinea.Arrotondamento;
                    var f3 = riepilogoLinea.EsigibilitaIVA;
                    var f4 = riepilogoLinea.ImponibileImporto;
                    var f5 = riepilogoLinea.Imposta;
                    var f6 = riepilogoLinea.Natura;
                    var f7 = riepilogoLinea.SpeseAccessorie;
                }
            }


        }

        private void K(byte[] cx)
        {
            var stream = cx;
            if ( stream == null || stream.Length == 0 ) return;

            var path = Path.GetTempFileName().Replace( "tmp", "PDF" );

            using ( var fs = File.Create( path ) )
            {
                fs.Write( stream, 0, stream.Length );
            }

            Process.Start( path );

        }
    }
}
