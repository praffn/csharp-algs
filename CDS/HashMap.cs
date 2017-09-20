using System;

namespace CDS
{
    public class HashMap<K, V> : IMap<K, V>
    {
        public int Count { get; private set; } = 0;
        public bool Empty => Count == 0;

        private int _m;
        private K[] _keys;
        private V[] _values;

        public HashMap(int initialCapacity = 4)
        {
            _m = initialCapacity;
            _keys = new K[_m];
            _values = new V[_m];
        }
        
        public void Clear()
        {
            _m = 4;
            _keys = new K[_m];
            _values = new V[_m];
            Count = 0;
        }

        public bool ContainsKey(K key)
        {
            try
            {
                return this[key] != null;
            }
            catch
            {
                return false;
            }
        }

        public bool ContainsValue(V value)
        {
            foreach (var item in _values)
                if (item != null && item.Equals(value))
                    return true;
            return false;
        }

        public V this[K key]
        {
            get
            {
                var i = Hash(key);
                while (_keys[i] != null)
                {
                    if (_keys[i].Equals(key))
                        return _values[i];
                    i = (i + 1) % _m;
                }
                throw new KeyNotFoundException($"Key '{key}' does not exists in map");
            }
            set
            {
                if (Count >= _m / 2)
                    Resize(2 * _m);
            
                var i = Hash(key);
                while (_keys[i] != null)
                {
                    if (_keys[i].Equals(key))
                    {
                        _values[i] = value;
                        return;
                    }
                    i = (i + 1) % _m;
                }
                _keys[i] = key;
                _values[i] = value;
                Count++;
            }
        }
        
        public V Remove(K key)
        {
            throw new NotImplementedException();
        }
        
        // helpers
        private int Hash(K key)
        {
            return (key.GetHashCode() & 0x7fffffff) % _m;
        }

        private void Resize(int size)
        {
            var tmp = new HashMap<K, V>(size);
            for (var i = 0; i < _m; i++)
            {
                if (_keys[i] != null)
                    tmp[_keys[i]] = _values[i];
            }
            _keys = tmp._keys;
            _values = tmp._values;
            _m = size;
        }
    }
}