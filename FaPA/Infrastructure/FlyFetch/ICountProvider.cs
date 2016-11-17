using System;

namespace FaPA.Infrastructure.FlyFetch
{
    public interface ICountProvider
    {
        void GetCount();
        event EventHandler<CountEventArgs> CountAvailable;
    }
}
