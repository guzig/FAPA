using System.Collections.Generic;

namespace FaPA.Infrastructure.FlyFetch
{
    public interface IGetByIdsQueryObject
    {
        List<long> AllowedIds { get; }
        
    }
}