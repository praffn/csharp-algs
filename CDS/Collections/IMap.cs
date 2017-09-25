using System;
using System.Collections.Generic;

namespace CDS.Collections
{
    public interface IMap<TKey, TValue> : ICollection, IEnumerable<KeyValuePair<TKey, TValue>>
    {
        bool ContainsKey(TKey key);
        bool ContainsValue(TValue value);

        bool TryGet(TKey key, out TValue value);
        
        TValue this[TKey key] { get; set; }
        TValue Remove(TKey key);
    }

    public class KeyNotFoundException : Exception
    {
        public KeyNotFoundException(string msg) : base(msg)
        {
        }
    }
    
    public struct KeyValuePair<TKey, TValue>
    {
        public TKey Key { get; }
        public TValue Value { get; }

        public KeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}