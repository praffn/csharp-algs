using System.Collections.Generic;

namespace CDS.Collections
{
    public interface ISet<T> : ICollection, IEnumerable<T>
    {
        void Add(T value);
        bool Contains(T value);
        void Remove(T value);


        ISet<T> Union(ISet<T> set);
        ISet<T> Intersect(ISet<T> set);
        ISet<T> Except(ISet<T> set);
    }
}