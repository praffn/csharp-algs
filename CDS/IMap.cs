using System;

namespace CDS
{
    public interface IMap<in K, V>
    {
        bool Empty { get; }
        int Count { get; }
        
        void Clear();
        bool ContainsKey(K key);
        bool ContainsValue(V value);
        
        V this[K key] { get; set; }
        V Remove(K key);
    }

    public class KeyNotFoundException : Exception
    {
        public KeyNotFoundException(string msg) : base(msg)
        {
        }
    }
}