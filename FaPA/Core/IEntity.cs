using System;

namespace FaPA.Core
{
    public interface IEntity : IEquatable<IEntity>, IComparable<IEntity>, IComparable
    {
        long Id { get; }
    }
}
