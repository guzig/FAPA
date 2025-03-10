﻿using System;
using System.Collections.Generic;
using System.Linq;
using FaPA.Core;
using FaPA.Core.FaPa;
using FaPA.DomainServices.Helpers;
using FaPA.Infrastructure.Helpers;

namespace FaPA.AppServices.CoreValidation
{
    public static class CoreValidatorService
    {
        private static readonly IDictionary<Type, ICoreValidator> Validators = new Dictionary<Type, ICoreValidator>()
        {

            { typeof ( DatiGeneraliDocumentoType ), new DatiGeneraliDocumentoValidator() },
            { typeof ( DatiBolloType ), new DatiCassaBolloValidator() },
            { typeof ( FatturaPrincipaleType ), new FatturaPrincipaleValidator() },
            { typeof ( DatiCassaPrevidenzialeType ), new DatiCassaPrevidenzialeValidator() },
            { typeof ( IdFiscaleType ), new IdFiscaleValidator() },
            { typeof ( AnagraficaType ), new AnagraficaValidator() },
            { typeof ( DatiAnagraficiVettoreType ), new DatiAnagraficiVettoreValidator() },
            { typeof ( IndirizzoType ), new IndirizzoValidator() },
            { typeof ( DatiTrasportoType ), new DatiTrasportoValidator() },
            { typeof ( DettaglioPagamentoType ), new DatiPagamentoValidator() },
            { typeof ( DatiDocumentiCorrelatiType ), new DatiCorrelatiValidator() },
            { typeof ( DatiRitenutaType ), new DatiRitenutaValidator() },
            { typeof ( AltriDatiGestionaliType ), new AltriDatiGestionaliValidator() },
            { typeof ( ScontoMaggiorazioneType ), new ScontoMaggiorazioneValidator() },
            { typeof ( DettaglioLineeType ), new DettaglioLineeValidator() },
            { typeof ( Anagrafica ), new NhCommittenteValidator() },
            { typeof ( Fattura ), new NhFatturaValidator() },
            { typeof ( UserData ), new NhUserDataValidator() },
            { typeof ( DatiSALType ), new DatiSalValidator() },
            { typeof ( DatiDDTType ), new DatiDdtValidator() },

            //TerzoIntermediarioSoggettoEmittenteType
            { typeof ( DatiTrasmissioneType ), new DatiTrasmittenteValidator() },
            { typeof ( DatiAnagraficiTerzoIntermediarioType ), new DatiTerzoIntermdiarioValidator() },
            { typeof ( TerzoIntermediarioSoggettoEmittenteType ), new DatiTerzoIntermdiarioValidator() },
            { typeof ( DatiAnagraficiRappresentanteType ), new RappFiscaleValidator() },
            { typeof ( DatiPagamentoType ), new DatiPagamentoValidator() },




        };

        public static DomainResult Validate( object instance )
        {
            return new DomainResult(GetValidationErrors( instance ));
        }

        public static bool IsValid( object instance )
        {
            if ( instance == null ) return true;
            var validationErrors = GetValidationErrors( instance );
            return IsValid(validationErrors);
        }

        private static bool IsValid(IDictionary<string, List<string>> validationErrors)
        {
            return validationErrors == null || validationErrors.Values.All( s =>
                s.Exists( x => !string.IsNullOrWhiteSpace( x ) ) );
        }
        
        public static IDictionary<string, List<string>> GetValidationErrors( object instance )
        {
            if ( instance == null ) return null;
            var validator = TryGetValidator( instance.NhUnproxyType() );
            return validator?.GetValidationErrors( instance );
        }

        public static IDictionary<string, List<string>> GetValidationErrors( string columnName, object instance )
        {
            if ( instance == null ) return null;
            var validator = TryGetValidator( instance.NhUnproxyType() );
            return validator?.GetValidationErrors( columnName, instance );
        }

        private static ICoreValidator TryGetValidator( Type type )
        {
            return !Validators.ContainsKey( type ) ? null : Validators[type];
        }
    }
}