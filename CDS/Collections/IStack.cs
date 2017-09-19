using System.Collections.Generic;

namespace CDS.Collections
{
    public interface IStack<T> : ICollection, IEnumerable<T>
    {
        T Peek();
        T Pop();
        void Push(T value);
        bool Contains(T value);
    }
}