using System;
using FaPA.Core;

namespace FaPaTets.DbSetUp
{
    public static class DataTestFactory
    {
        public static Fattura GetFattura()
        {
            var fornitore = new Anagrafica();
            fornitore.Denominazione = "Comune di Isola di Capo Rizzuto";
            fornitore.CodiceFiscale = "81004130795";
            fornitore.PIva = "01939480792";
            fornitore.Comune = "Isola di Capo Rizzuto";
            fornitore.Cap = "88841";
            fornitore.Civico = "01";
            fornitore.Provincia = "KR";
            fornitore.Nazione = "IT";
            fornitore.Indirizzo = "Piazza Falcone e Borsellino";

            var committente = new Anagrafica();

            committente.Denominazione = "Anagrafica 1";
            committente.CodiceFiscale = "00304310790";
            committente.PIva = "00304310790";
            committente.Comune = "Cropani";
            committente.Cap = "88051";
            committente.Civico = "01";
            committente.Provincia = "CZ";
            committente.Nazione = "IT";
            committente.Indirizzo = "Via Roma";

            var fattura = new Fattura();
            fattura.Init();
            fattura.AnagraficaCedenteDB = fornitore;
            fattura.AnagraficaCommittenteDB = committente;
            fattura.DataFatturaDB = DateTime.Now;
            fattura.TotaleFatturaDB = 100;
            fattura.CodUfficioDB = "UFZEB5";
            fattura.NumeroFatturaDB = "130";

            return fattura;
        }
    }
}