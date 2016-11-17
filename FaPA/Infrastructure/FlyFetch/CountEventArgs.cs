using System;

namespace FaPA.Infrastructure.FlyFetch
{
    public class CountEventArgs : EventArgs
    {
        public CountEventArgs(int count)
        {
            Count = count;
        }
        public int Count { get; private set; }
    }
}