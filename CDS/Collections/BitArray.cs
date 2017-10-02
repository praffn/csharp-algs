using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CDS.Collections
{
    public class BitArray : ICollection, IEnumerable<bool>
    {
        private int[] _data;
        public int Count { get; private set; }

        public bool this[int position] => Get(position);
        
        public BitArray(int size = 32)
        {
            if (size < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(size)} must not be less than 0");
            }
            _data = new int[(size - 1) / 32 + 1];
            Count = size;
        }

        public BitArray(params bool[] values) : this(values.Length)
        {
            for (var i = 0; i < values.Length; i++)
            {
                if (values[i])
                {
                    Set(i);
                }
            }
        }

        public void Set(int position)
        {
            var i = position / 32;
            var n = _data[i];
            _data[i] = n | 1 << (position % 32);
        }

        public IEnumerator<bool> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
            {
                var n = _data[i / 32];
                yield return (n & (1 << (i % 32))) != 0;
                
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Empty { get; }
        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public void Not()
        {
            for (var i = 0; i < _data.Length; i++)
            {
                _data[i] = ~_data[i];
            }
        }

        public void And(BitArray ba)
        {
            if (Count != ba.Count)
            {
                throw new ArgumentException("Given BitArray must be of same size");
            }
            for (var i = 0; i < _data.Length; i++)
            {
                _data[i] = _data[i] & ba._data[i];
            }
        }
        
        public void Or(BitArray ba)
        {
            if (Count != ba.Count)
            {
                throw new ArgumentException("Given BitArray must be of same size");
            }
            for (var i = 0; i < _data.Length; i++)
            {
                _data[i] = _data[i] | ba._data[i];
            }
        }

        public void Xor(BitArray ba)
        {
            if (Count != ba.Count)
            {
                throw new ArgumentException("Given BitArray must be of same size");
            }
            for (var i = 0; i < _data.Length; i++)
            {
                _data[i] = _data[i] ^ ba._data[i];
            }
        }

        public bool Get(int position)
        {
            if (position >= Count)
            {
                throw new IndexOutOfRangeException();
            }
            var n = _data[position / 32];
            return (n & (1 << (position % 32))) != 0;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var b in this)
            {
                sb.Append(b ? "1" : "0");
            }
            return sb.ToString();
        }

        public bool[] BoolArray()
        {
            var a = new bool[Count];
            for (var i = 0; i < Count; i++)
            {
                a[i] = this[i];
            }
            return a;
        }
    }
}