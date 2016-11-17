using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace FaPA.Infrastructure.FlyFetch
{
    public static class CollectionFactory
    {
        private static readonly object _stocksLock = new object();

        public static void Create<T, TColl>(int pageSize, IPageProvider<T,TColl> pageProvider, ICountProvider countProvider,
            Action<TColl> created) where T : new() where TColl : IList<T>, new()
        {
            if (null == countProvider)
                throw new ArgumentNullException("countProvider");
            if (null == pageProvider)
                throw new ArgumentNullException("pageProvider");
            var collection = new TColl();
            BindingOperations.EnableCollectionSynchronization(collection, _stocksLock);
            countProvider.CountAvailable+=(s,e)=>
            {
                //HashSet<int> fetchingPages = new HashSet<int>();
                for (int i = 0; i < e.Count;i+=pageSize)
                {
                    var proxy = ProxyFactory.CreateProxy<T>(new PageInterceptor<T,TColl>(pageProvider,pageSize,collection));
                    (proxy as IPageableElement).PageIndex = i / pageSize;
                    for (int j = i; j < i + pageSize && j<e.Count;++j )
                        collection.Add(proxy);
                }
                created(collection);
            };
            countProvider.GetCount();
            
        }

        //public static void Merge<T, TColl>(TColl source, TColl dest, int destPageSize)
        //    where T : new()
        //    where TColl : IList<T>
        //{
        //    int relativePage = (dest.Count/destPageSize);
        //    foreach (var t in dest)
        //    {
        //        var item = t as IPageProvider<T,TColl>;
        //        item.RelativePosition = relativePage;
        //        source.Add(t);
        //    }
        //}
    }
}
