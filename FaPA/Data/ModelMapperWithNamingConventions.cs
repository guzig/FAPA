using System.Collections.Generic;
using System.Reflection;
using NHibernate.Mapping.ByCode;

namespace FaPA.Data
{
    public class ModelMapperWithNamingConventions : ConventionModelMapper
    {
        public const string ForeignKeyColumnPostfix = "_id";
        public const string ManyToManyIntermediateTableInfix = "To";
        public const char ElementColumnTrimmedPluralPostfix = 's';

        private readonly List<MemberInfo> _ignoredMembers = new List<MemberInfo>();

        public ModelMapperWithNamingConventions()
        {
            BeforeMapManyToOne += ( inspector, member, customizer ) =>
                customizer.Column( member.LocalMember.Name + ForeignKeyColumnPostfix );
            BeforeMapManyToMany += ( inspector, member, customizer ) =>
                customizer.Column( member.CollectionElementType().Name + ForeignKeyColumnPostfix );
            BeforeMapElement += ( inspector, member, customizer ) =>
                customizer.Column( member.LocalMember.Name.TrimEnd( ElementColumnTrimmedPluralPostfix ) );
            BeforeMapJoinedSubclass += ( inspector, type, customizer ) =>
                customizer.Key( k => k.Column( type.BaseType.Name + ForeignKeyColumnPostfix ) );

            BeforeMapSet += BeforeMappingCollectionConvention;
            BeforeMapBag += BeforeMappingCollectionConvention;
            BeforeMapList += BeforeMappingCollectionConvention;
            BeforeMapIdBag += BeforeMappingCollectionConvention;
            BeforeMapMap += BeforeMappingCollectionConvention;

            BeforeMapComponent += DisableComponentParentAutomapping;

            IsPersistentProperty( ( m, d ) => !_ignoredMembers.Contains( m ) );
        }

        private void DisableComponentParentAutomapping( IModelInspector inspector, PropertyPath member, 
            IComponentAttributesMapper customizer )
        {
            var parentMapping = member.LocalMember.GetPropertyOrFieldType().GetFirstPropertyOfType( member.Owner() );
            DisableAutomappingFor( parentMapping );
        }

        private void DisableAutomappingFor( MemberInfo member )
        {
            if ( member != null )
                _ignoredMembers.Add( member );
        }

        private void BeforeMappingCollectionConvention( IModelInspector inspector, PropertyPath member, 
            ICollectionPropertiesMapper customizer )
        {
            if ( inspector.IsManyToManyKey( member.LocalMember ) )
                customizer.Table( member.ManyToManyIntermediateTableName() );

            customizer.Key( k => k.Column( DetermineKeyColumnName( inspector, member ) ) );
        }

        private static string DetermineKeyColumnName( IModelInspector inspector, PropertyPath member )
        {
            var otherSideProperty = member.OneToManyOtherSideProperty();
            if ( inspector.IsOneToMany( member.LocalMember ) && otherSideProperty != null )
                return otherSideProperty.Name + ForeignKeyColumnPostfix;

            return member.Owner().Name + ForeignKeyColumnPostfix;
        }
    }
}