using System;
using System.Collections;
using System.Collections.Generic;

namespace CDS.Collections
{
    public class ArrayList<T> : IList<T>
    {
        private T[] _data;

        public int Count { get; private set; }
        public bool Empty => Count == 0;

        public ArrayList(int initialCapacity = 16)
        {
            _data = new T[initialCapacity];
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
                yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        
        public void Clear()
        {
            Count = 0;
            _data = new T[16];
        }

        public void Add(T element)
        {
            if (Count == _data.Length)
                Resize(_data.Length * 2);
            _data[Count++] = element;
        }

        public T Get(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();
            return _data[index];
        }

        public bool Contains(T element)
        {
            for (var i = 0; i < Count; i++)
                if (_data[i].Equals(element))
                    return true;
            return false;
        }

        public void Remove(T element)
        {
            RemoveAt(IndexOf(element));
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();
            for (var i = index; i < Count - 1; i++)
            {
                _data[i] = _data[i + 1];
            }
            Count--;
        }

        public int IndexOf(T element)
        {
            for (var i = 0; i < Count; i++)
                if (_data[i].Equals(element))
                    return i;
            return -1;
        }

        public T this[int index] => Get(index); 

        private void Resize(int newSize)
        {
            var tmp = new T[newSize];
            for (var i = 0; i < Count; i++)
            {
                tmp[i] = _data[i];
            }
            _data = tmp;
        }
    }
}