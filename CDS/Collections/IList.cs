using System.Collections.Generic;

namespace CDS.Collections
{
    public interface IList<T> : ICollection, IEnumerable<T>
    {
        void Add(T element);
        T Get(int index);
        bool Contains(T element);
        void Remove(T element);
        void RemoveAt(int index);
        int IndexOf(T element);
        
        T this[int index] { get; }
    }
}