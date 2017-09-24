using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CDS.Collections
{
    public class HashSet<T> : ISet<T>
    {
        private static readonly object Present = new object();

        private HashMap<T, object> _map;

        public int Count => _map.Count;
        public bool Empty => _map.Empty;

        public HashSet()
        {
            _map = new HashMap<T, object>();
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return _map.Select(kvp => kvp.Key).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        
        public void Clear()
        {
            _map.Clear();
        }

        public void Add(T value)
        {
            _map[value] = Present;
        }

        public bool Contains(T value)
        {
            return _map.ContainsKey(value);
        }

        public void Remove(T value)
        {
            _map.Remove(value);
        }

        public ISet<T> Union(ISet<T> set)
        {
            var union = new HashSet<T>();
            foreach (var value in this)
                union.Add(value);
            foreach (var value in set)
                union.Add(value);
            return union;
        }

        public ISet<T> Intersect(ISet<T> set)
        {
            var intersect = new HashSet<T>();
            foreach (var value in this)
                if (set.Contains(value))
                    intersect.Add(value);
            return intersect;
        }

        public ISet<T> Except(ISet<T> set)
        {
            var except = new HashSet<T>();
            foreach (var value in this)
                if (!set.Contains(value))
                    except.Add(value);
            return except;
        }
    }
}