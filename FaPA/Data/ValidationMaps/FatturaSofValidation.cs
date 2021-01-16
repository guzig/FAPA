using System;
using FaPA.Core;
using FaPA.DomainServices;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Validator.Cfg.Loquacious;

namespace FaPA.Data.ValidationMaps
{
    public class FatturaSofValidation : ValidationDef<Core.Fattura>
    {
        //note:use of ValidateInstance.By because : "NHV was designed more thinking about properties-constraints than Business-Rules"
        //see:http://fabiomaulo.blogspot.it/2010/01/nhibernatevalidator-changing-validation.html
        public FatturaSofValidation()
        {
            ValidateInstance.By((fattura, context) =>
            {
                var isValid = true;

                if (fattura.DataFatturaDB == DateTime.MinValue)
                {
                    context.AddInvalid<Fattura, DateTime>("Data fattura non valida", p => p.DataFatturaDB);
                    isValid = false;
                }

                if ( fattura.AnagraficaCedenteDB == null )
                {
                    context.AddInvalid<Fattura, Anagrafica>( "Specificare un fornitore ",
                        p => p.AnagraficaCedenteDB );
                    isValid = false;
                }
                else
                {
                    if ( string.IsNullOrWhiteSpace( fattura.AnagraficaCedenteDB.PIva) )
                    {
                        context.AddInvalid<Fattura, Anagrafica>( "Il foritore non ha una p.iva ",
                            p => p.AnagraficaCedenteDB );
                        isValid = false;
                    }
                }

                if ( !IsUniqueProgFile( fattura ) ) {

                    context.AddInvalid<Fattura, int>( "Progressivo invio non valido ", p => p.ProgFile );
                    isValid = false;
                }

                if ( !isValid || IsUniqueFattura( fattura ) ) return isValid;

                const string error = "Numero, data, e, fornitore, fattura non sono unici";
                context.AddInvalid<Fattura, string>(error, p => p.NumeroFatturaDB);
                context.AddInvalid<Fattura, DateTime>(error, p => p.DataFatturaDB);
                context.AddInvalid<Fattura, Anagrafica>(error, p => p.AnagraficaCedenteDB);

                return false;
            });

            //Define(l => l.DettagliFattura).HasValidElements();
            //Define(l => l.DataCaricamento).Satisfy(d => d != DateTime.MinValue && d != DateTime.MaxValue).WithMessage("Data caricamento fattura è un campo richiesto"); ;
            Define( l => l.PecDestinatarioDB ).IsEmail();
            Define( l => l.CigDB ).MaxLength(15).WithMessage( "Lunghezza massima 15 caratteri" );
            Define( l => l.CupDB ).MaxLength( 15 ).WithMessage( "Lunghezza massima 15 caratteri" );
            Define(l => l.CodUfficioDB).NotNullableAndNotEmpty().WithMessage("Specificare un codice ufficio PA");
            Define(l => l.NumeroFatturaDB).NotNullableAndNotEmpty().WithMessage("Specificare un numero fattura");
            Define(f => f.AnagraficaCedenteDB).NotNullable().WithMessage("Specificare un fornitore");
            Define(f => f.AnagraficaCommittenteDB).NotNullable().WithMessage("Specificare un committente");
            Define(f => f.DataFatturaDB).Satisfy(d => d != DateTime.MinValue && d != DateTime.MaxValue).WithMessage("Data fattura è un campo richiesto");
        }

        private static bool IsUniqueFattura( Fattura fatt )
        {
            if ( fatt.AnagraficaCedenteDB == null || string.IsNullOrWhiteSpace( fatt.NumeroFatturaDB )  ||
                 fatt.DataFatturaDB == DateTime.MinValue || fatt.DataFatturaDB == DateTime.MaxValue)
                return false;

            object isLock = 0;
            int result;
            lock (isLock)
            {
                using (var tx = NHibernateStaticContainer.Session.BeginTransaction())
                {
                    result = NHibernateStaticContainer.Session.QueryOver<Fattura>().
                        Where( f => f.DataFatturaDB == fatt.DataFatturaDB ).
                        And( f => f.NumeroFatturaDB == fatt.NumeroFatturaDB ).
                        And( f=> f.AnagraficaCedenteDB.Id == fatt.AnagraficaCedenteDB.Id ).
                        And( f => f.Id != fatt.Id ).
                        Select( Projections.Count<Fattura>( f => f.Id ) ).Cacheable().CacheMode(CacheMode.Normal)
                        .FutureValue<int>().Value;

                    //var criteria = Session.CreateCriteria(typeof(Fattura));
                    //criteria.Add(Restrictions.Eq("DataFatturaDB", fattura.DataFatturaDB));
                    //criteria.Add(Restrictions.Eq("NumFattura", fattura.NumFattura));
                    //criteria.Add(Restrictions.Not(Restrictions.IdEq(fattura.Id)));
                    //criteria.SetProjection(Projections.Count(Projections.Id()));
                    //criteria.SetCacheMode(CacheMode.Normal);
                    //criteria.SetCacheable(true);
                    //var result = criteria.UniqueResult<int>() == 0;
                    tx.Commit();
                }
            }

            return result == 0;
        }

        private static bool IsUniqueProgFile( Fattura fatt ) {
            
            if ( fatt.ProgFile == 0 )
                return false;

            object isLock = 0;
            int result;
            lock ( isLock ) {
                using ( var tx = NHibernateStaticContainer.Session.BeginTransaction() ) {
                    result = NHibernateStaticContainer.Session.QueryOver<Fattura>().
                        And( f => f.AnagraficaCedenteDB.Id == fatt.AnagraficaCedenteDB.Id ).
                        And( f => f.ProgFile == fatt.ProgFile ).
                        And( f => f.Id != fatt.Id ).
                        Select( Projections.Count<Fattura>( f => f.Id ) ).Cacheable().CacheMode( CacheMode.Normal )
                        .FutureValue<int>().Value;
                    tx.Commit();
                }
            }

            return result == 0;
        }
    }
}
