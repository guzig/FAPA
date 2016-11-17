using System;

namespace FaPA.Infrastructure.FlyFetch
{
    public class GetbyIdsCountProvider : ICountProvider
    {
        public readonly int PageSize;
        public GetbyIdsCountProvider(int pageSize)
        {
            PageSize = pageSize;
        }
        public void GetCount()
        {
            CountAvailable(this, new CountEventArgs(PageSize));
        }

        public event EventHandler<CountEventArgs> CountAvailable;
    }
}