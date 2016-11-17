using System;
using System.Collections.Generic;

namespace FaPA.Infrastructure.FlyFetch
{
    public interface IPageProvider<T,TColl>  where TColl:IList<T>
    {
        void GetAPage(TColl collection, int first, int count);
        event EventHandler Completed;
    }
}
