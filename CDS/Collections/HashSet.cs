﻿using System;
using System.Collections;
using System.Collections.Generic;

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
            throw new System.NotImplementedException();
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
            throw new NotImplementedException();
        }

        public ISet<T> Intersect(ISet<T> set)
        {
            throw new System.NotImplementedException();
        }

        public ISet<T> Except(ISet<T> set)
        {
            throw new System.NotImplementedException();
        }
    }
}