using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CDS.Collections
{
    public class ChainMap<TKey, TValue> : IMap<TKey, TValue>
    {

        private LinkedList<ChainMapNode>[] _seperateChainMap;

        public TValue this[TKey key]
        {
            get
            {
                var hash = Hash(key);
                var list = _seperateChainMap[hash];

                if (list == null) throw new KeyNotFoundException($"Key '{key}' does not exists in map");

                foreach (var node in list)
                {
                    if (node.Key.Equals(key)) return node.Value;
                }

                throw new KeyNotFoundException($"Key '{key}' does not exists in map");
            }

            set
            {
                var hash = Hash(key);

                var list = _seperateChainMap[hash];
                if (list == null)
                {
                    list = new LinkedList<ChainMapNode>();
                    _seperateChainMap[hash] = list;
                }
                
                foreach (var node in list)
                {
                    if (node.Key.Equals(key))
                    {
                        node.Value = value;
                        return;
                    }
                }

                list.Add(new ChainMapNode { Key = key, Value = value } );
                Count++;
            }
        }

        public int Count { get; private set; } = 0;

        public bool Empty => Count == 0;

        public ChainMap(int initialCapacity = 16)
        {
            _seperateChainMap = new LinkedList<ChainMapNode>[initialCapacity];
        }

        public void Clear()
        {
            _seperateChainMap = new LinkedList<ChainMapNode>[16];
            Count = 0;
        }

        public bool ContainsKey(TKey key)
        {
            var hash = Hash(key);
            var list = _seperateChainMap[hash];

            if (list == null) return false;

            foreach(var node in list)
            {
                if (node.Key.Equals(key)) return true;
            }

            return false;
        }

        public bool ContainsValue(TValue value)
        {
            foreach(var list in _seperateChainMap)
            {
                if (list == null) continue;

                foreach(var node in list)
                {
                    if (node.Value.Equals(value)) return true;
                }
            }
            return false;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var list in _seperateChainMap)
            {
                if (list == null) continue;
                foreach (var node in list)
                {
                    yield return new KeyValuePair<TKey, TValue>(node.Key, node.Value);
                }
            }
        }

        public ISet<TKey> KeySet()
        {
            ISet<TKey> set = new HashSet<TKey>();

            foreach (var list in _seperateChainMap)
            {
                if (list == null) continue;
                foreach (var node in list)
                {
                    set.Add(node.Key);
                }
            }
            return set;
        }

        public TValue Remove(TKey key)
        {
            var hash = Hash(key);
            var list = _seperateChainMap[hash];

            if (list == null) return default(TValue);

            foreach(var node in list)
            {
                if (node.Key.Equals(key))
                {
                    list.Remove(node);
                    Count--;
                    return node.Value;
                }
            }

            return default(TValue);
        }

        public bool TryGet(TKey key, out TValue value)
        {
            if (ContainsKey(key))
            {
                value = this[key];
                return true;
            }
            value = default(TValue);
            return false;
        }

        // helpers
        private int Hash(TKey key)
        {
            return (key.GetHashCode() & 0x7fffffff) % _seperateChainMap.Length;
        }

        public IList<TValue> Values()
        {
            IList<TValue> values = new ArrayList<TValue>();

            foreach(var list in _seperateChainMap)
            {
                if (list == null) continue;
                foreach (var node in list)
                {
                    values.Add(node.Value);
                }
            }
            return values;

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class ChainMapNode
        {
            internal TKey Key { get; set; }
            internal TValue Value { get; set; }
        }

    }
}
