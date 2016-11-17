using System;
using System.Collections.Generic;
using System.Linq;

namespace FaPA.Infrastructure.FlyFetch
{
    class PageInterceptor<T,TColl> : IPageHit where T : new() where TColl:IList<T>,new()
    {
        readonly IPageProvider<T, TColl> _pageFiller;
        readonly int _pageSize;
        readonly TColl _collection;
        readonly HashSet<int> _pagesHit = new HashSet<int>();

        public PageInterceptor(IPageProvider<T,TColl> pageFiller, int pageSize, TColl collection)
        {
            _pageFiller = pageFiller;
            _pageSize = pageSize;
            _collection = collection;
        }
        #region IPageHit Members
        public void Hit(int npage)
        {
            bool fetch = false;
            lock (_pagesHit)
            {
                if (!_pagesHit.Contains(npage))
                {
                    _pagesHit.Add(npage);
                    fetch = true;
                }
            }
            if (fetch)
            {
                EventHandler handler = null;
                handler = ( s,  e) =>
                {
                    lock (_pagesHit)
                    {
                        _pagesHit.Remove(npage);
                    }
                    foreach (var pe in _collection.Skip(npage * _pageSize).Take(_pageSize).OfType<IPageableElement>())
                        pe.Loaded = true;
                    _pageFiller.Completed -= handler;
                };
                _pageFiller.Completed += handler;
                //prepare single item data
                int first = npage * _pageSize;
                for (int i = first; i < first + _pageSize && i < _collection.Count; ++i)
                {
                    _collection[i] = new T();
                }
                _pageFiller.GetAPage(_collection, npage * _pageSize, _pageSize);
            }
        }
        #endregion
    }
}
