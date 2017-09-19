using System.Collections.Generic;
using CDS.Collections;

namespace CDS.Collections
{
    public interface IQueue<T> : ICollection, IEnumerable<T>
    {
        bool Contains(T value);
        T Dequeue();
        void Enqueue(T value);
        T Peek();
    }
}