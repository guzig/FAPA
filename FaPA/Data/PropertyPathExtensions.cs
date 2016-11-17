using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NHibernate.Mapping.ByCode;

namespace FaPA.Data
{
    // NHibernate mapping-by-code naming convention resembling Fluent's
    // See the blog post: http://notherdev.blogspot.com/2012/01/mapping-by-code-naming-convention.html

    public static class PropertyPathExtensions
    {
        public static Type Owner( this PropertyPath member )
        {
            return member.GetRootMember().DeclaringType;
        }

        public static Type CollectionElementType( this PropertyPath member )
        {
            return member.LocalMember.GetPropertyOrFieldType().DetermineCollectionElementOrDictionaryValueType();
        }

        public static MemberInfo OneToManyOtherSideProperty( this PropertyPath member )
        {
            return member.CollectionElementType().GetFirstPropertyOfType( member.Owner() );
        }

        public static string ManyToManyIntermediateTableName( this PropertyPath member )
        {
            return String.Join(
                ModelMapperWithNamingConventions.ManyToManyIntermediateTableInfix,
                member.ManyToManySidesNames().OrderBy( x => x )
            );
        }

        private static IEnumerable<string> ManyToManySidesNames( this PropertyPath member )
        {
            yield return member.Owner().Name;
            yield return member.CollectionElementType().Name;
        }
    }

}
