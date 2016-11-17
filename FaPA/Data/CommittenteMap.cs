using FaPA.Core;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace FaPA.Data
{
    public class CommittenteMap : SubclassMapping<Committente>
    {
        public CommittenteMap()
        {
            DiscriminatorValue("committente");

            Set(x => x.Fatture, s =>
            {
                s.Inverse(true); // Is collection inverse?
                s.Fetch(CollectionFetchMode.Join); // or CollectionFetchMode.Select, CollectionFetchMode.Subselect
                s.BatchSize(20);
                s.Lazy(CollectionLazy.Lazy);
                s.Cascade(Cascade.None); //set cascade strategy
                //s.Key(k => k.Column(col => col.Name("FatturaId"))); //foreign key in Comment table
            }, a => a.OneToMany());
        }
    }
}