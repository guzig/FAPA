using NUnit.Framework;

namespace FaPaTets.FatturaPa.FatturaPa_11
{
    class XsdFatturaElettronicaTypeV11ExtendedTest
    {
        [Test]
        public void CanDeserializeFatturaElettronicaV11Type3()
        {
            var nomeFile = TestsPaths.TestDataRootPath + @"\XSD-Fattura\XSD_Fattura_Sample\IT01234567890_11003.xml";

            var fattura1 = HelpersFaPA.GetFatturaPA(nomeFile);

            var  bodyTypes = fattura1.FatturaElettronicaBody;

            //foreach (var fatturaElettronicaBodyType in bodyTypes)
            //{
            //    //DatiGeneraliType datiGenerali = fatturaElettronicaBodyType.DatiGenerali;

            //    //DatiPagamentoType[] datiPagamentoField

            //    //AllegatiType[] allegatiField

            //    var dettaglioLinee = fatturaElettronicaBodyType.DatiBeniServizi.DettaglioLinee;

            //    //foreach (var dettaglioLinea in dettaglioLinee)
            //    //{
            //    //    var dettaglio = new DettaglioFattura();

            //    //    //dettaglio.CausaleIva
            //    //    var f1 = dettaglioLinea.AliquotaIVA;

            //    //    //dettaglio.Causale
            //    //    var f3 = dettaglioLinea.CodiceArticolo;
            //    //    var f6 = dettaglioLinea.Descrizione;

            //    //    dettaglio.PeriodoFatturazione = PeriodoFatturazione.TryParsePeriodo(
            //    //        dettaglioLinea.DataInizioPeriodo, dettaglioLinea.DataInizioPeriodo);

            //    //    dettaglio.NumeroLinea = dettaglioLinea.NumeroLinea;
            //    //    dettaglio.Prezzo = (float)dettaglioLinea.PrezzoUnitario;
            //    //    dettaglio.SetQuantità( (float) dettaglioLinea.Quantita );
            //    //    //dettaglio.UnitàMisura = dettaglioLinea.UnitaMisura;
            //    //    //dettaglio.Importo = ( float ) dettaglioLinea.PrezzoTotale;


            //    //    var f7 = dettaglioLinea.Natura;

            //    //    var f12 = dettaglioLinea.RiferimentoAmministrazione;
            //    //    var f13 = dettaglioLinea.Ritenuta;
            //    //    var f14 = dettaglioLinea.ScontoMaggiorazione;
            //    //    var f15 = dettaglioLinea.TipoCessionePrestazione;

            //    //    //var f2 = dettaglioLinea.AltriDatiGestionali.First();

            //    //}

            //    var riepilogoLinee = fatturaElettronicaBodyType.DatiBeniServizi.DatiRiepilogo;

            //    foreach (var riepilogoLinea in riepilogoLinee)
            //    {
            //        var f1 = riepilogoLinea.AliquotaIVA;
            //        var f2 = riepilogoLinea.Arrotondamento;
            //        var f3 = riepilogoLinea.EsigibilitaIVA;
            //        var f4 = riepilogoLinea.ImponibileImporto;
            //        var f5 = riepilogoLinea.Imposta;
            //        var f6 = riepilogoLinea.Natura;
            //        var f7 = riepilogoLinea.SpeseAccessorie;
            //    }
            //}


        }    
    }
}