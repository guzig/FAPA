using System;
using System.Collections;
using NHibernate.Criterion;

namespace FaPA.Infrastructure.Finder
{
    public class FinderConfirmSearchEventArgs : EventArgs
    {
        public string AllowedGridProperties { get; set; }

        public IList Collection { get; set; }

        public bool IsIdSelection { get; private set; }

        public readonly DetachedCriteria DetachedCriteria;

        public FinderConfirmSearchEventArgs(IList result, DetachedCriteria detachedCriteria, string allowedGridProperties)
        {
            Collection = result;
            DetachedCriteria = detachedCriteria;
            AllowedGridProperties = allowedGridProperties;
        }

        public FinderConfirmSearchEventArgs(IList result, DetachedCriteria detachedQueryCriteria, bool isIdSelection, string allowedGridProperties)
            : this(result, detachedQueryCriteria, allowedGridProperties)
        {
            IsIdSelection = isIdSelection;
        }

        public FinderConfirmSearchEventArgs(IList result, DetachedCriteria detachedQueryCriteria)
        {
            Collection = result;
            DetachedCriteria = detachedQueryCriteria;
        }
    }
}
