using System;
using System.Collections;
using System.Collections.Generic;

namespace CDS.Collections
{
    public class PriorityQueue<T> : IQueue<T>
        where T : IComparable<T>
    {
        public int Count { get; private set; } = 0;
        public bool Empty => Count == 0;

        private T[] _data;

        public PriorityQueue(int initialCapacity = 1)
        {
            _data = new T[initialCapacity];
        }
        
        
        public IEnumerator<T> GetEnumerator()
        {
            var pq = new PriorityQueue<T>();
            for (var i = 1; i <= Count; i++)
                pq.Enqueue(_data[i]);
            while (!pq.Empty)
                yield return pq.Dequeue();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public void Clear()
        {
            _data = new T[1];
            Count = 0;
        }

        public bool Contains(T value)
        {
            for (var i = 1; i <= Count; i++)
            {
                if (_data[i].Equals(value))
                    return true;
            }
            return false;
        }

        public T Dequeue()
        {
            if (Empty)
                throw new InvalidOperationException("Cannot dequeue an empty queue");
            var min = _data[1];
            Exchange(1, Count--);
            Sink(1);
            if ( (Count > 0) && (Count == (_data.Length - 1) / 4))
                Resize(_data.Length / 2);
            return min;
        }

        public void Enqueue(T value)
        {
            if (Count == _data.Length - 1)
                Resize(2 * _data.Length);
            _data[++Count] = value;
            Swim(Count);
        }

        public T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException("Cannot peek an empty queue");
            return _data[1];
        }
        
        // privates
        
        /// <summary>
        /// Resizes the underlying data array to given size
        /// </summary>
        /// <param name="size">Size of new array</param>
        private void Resize(int size)
        {
            var tmp = new T[size];
            for (var i = 1; i <= Count; i++)
                tmp[i] = _data[i];
            _data = tmp;
        }

        /// <summary>
        /// Exchanges values at positions i and j in data array
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void Exchange(int i, int j)
        {
            var tmp = _data[i];
            _data[i] = _data[j];
            _data[j] = tmp;
        }

        /// <summary>
        /// Returns true if item at index i is grater than item at index j
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private bool Greater(int i, int j)
        {
            return _data[i].CompareTo(_data[j]) > 0;
        }

        /// <summary>
        /// Makes item at position k "swim up" while it is greater than the item at its parent
        /// </summary>
        /// <param name="k"></param>
        private void Swim(int k)
        {
            while (k > 1 && Greater(k / 2, k))
            {
                Exchange(k, k / 2);
                k = k / 2;
            }
        }

        /// <summary>
        /// Makes item at position k "sink down" until it is greater than its child
        /// </summary>
        /// <param name="k"></param>
        private void Sink(int k)
        {
            while (2 * k <= Count)
            {
                var j = 2 * k;
                if (j < Count && Greater(j, j + 1))
                    j++;
                if (!Greater(k, j))
                    break;
                Exchange(k, j);
                k = j;
            }
        }
    }
}