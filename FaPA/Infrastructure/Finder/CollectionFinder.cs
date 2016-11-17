using System;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace FaPA.Infrastructure.Finder
{
    public abstract class CollectionFinder : ObjectFinder
    {
        private readonly string _propName ;


        protected CollectionFinder(Type rootType, JoinType joinType, Action<string> callBackOnCriteria, string propName ) :
            base( rootType, joinType, callBackOnCriteria, propName)
        {
            _propName = propName;
        }

        protected override bool CreateDetachedQuery( DetachedCriteria detachedCriteria )
        {
            try
            {
                var subCriteria = CriteriaTransformer.Clone( QueryCriteria.DetachedCriteria ); 

                if ( !base.CreateDetachedQuery( subCriteria ) )
                    return false;

                DetachedQueryCriteria = detachedCriteria ?? DetachedCriteria.For( RootType );

                if ( DetachedQueryCriteria.Alias != null && DetachedQueryCriteria.Alias == "root" )
                {
                    var rootTypeName = DetachedQueryCriteria.GetRootEntityTypeIfAvailable().Name;

                    subCriteria.SetFetchMode( _propName, FetchMode.Eager ).
                        SetProjection( Projections.Id() );

                    if ( !string.IsNullOrWhiteSpace( rootTypeName) && rootTypeName == _propName )
                        subCriteria.Add( Restrictions.EqProperty( _propName + ".Id", DetachedQueryCriteria.Alias + ".Id" ) );
                    else
                        subCriteria.Add( Restrictions.EqProperty( _propName + ".Id", DetachedQueryCriteria.Alias + "." + _propName + ".Id" ) );

                    DetachedQueryCriteria.Add( Subqueries.Exists( subCriteria ) );

                    return true;
                }

                var criteria = subCriteria.GetCriteriaByPath(_propName) ?? subCriteria.CreateCriteria(_propName);

                criteria.SetProjection( Projections.Id() ).Add( Restrictions.EqProperty( "Id", _propName + AliasPrefix + ".Id" ) );

                DetachedQueryCriteria.Add( Subqueries.Exists( criteria ) );

                return true;

            }
            catch ( Exception )
            {
                return false;
            }
        }

    }
}