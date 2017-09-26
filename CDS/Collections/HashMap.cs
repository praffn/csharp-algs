using System.Collections;
using System.Collections.Generic;

namespace CDS.Collections
{
    public class HashMap<TKey, TValue> : IMap<TKey, TValue>
    {
        public int Count { get; private set; }
        public bool Empty => Count == 0;

        private int _m;
        private TKey[] _keys;
        private TValue[] _values;

        public HashMap(int initialCapacity = 4)
        {
            _m = initialCapacity;
            _keys = new TKey[_m];
            _values = new TValue[_m];
        }
        
        public void Clear()
        {
            _m = 4;
            _keys = new TKey[_m];
            _values = new TValue[_m];
            Count = 0;
        }

        public bool ContainsKey(TKey key)
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

        public bool ContainsValue(TValue value)
        {
            foreach (var item in _values)
                if (item != null && item.Equals(value))
                    return true;
            return false;
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

        public TValue this[TKey key]
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
        
        public TValue Remove(TKey key)
        {
            if (!ContainsKey(key))
                return default(TValue);
            var i = Hash(key);

            while (!key.Equals(_keys[i]))
                i = (i + 1) % _m;

            var ret = _values[i];
            _keys[i] = default(TKey);
            _values[i] = default(TValue);

            i = (i + 1) % _m;
            while (_keys[i] != null)
            {
                var rehashKey = _keys[i];
                var rehashVal = _values[i];
                _keys[i] = default(TKey);
                _values[i] = default(TValue);
                Count--;
                this[rehashKey] = rehashVal;
                i = (i + 1) % _m;
            }
            Count--;
            
            if (Count > 0 && Count <= _m / 8)
                Resize(_m / 2);
            
            return ret;
        }

        public ISet<TKey> KeySet()
        {
            var keySet = new HashSet<TKey>();
            foreach (var key in _keys)
                if (key != null)
                    keySet.Add(key);
            return keySet;
        }

        public IList<TValue> Values()
        {
            var values = new ArrayList<TValue>();
            foreach (var key in KeySet())
                values.Add(this[key]);
            return values;
        }

        // helpers
        private int Hash(TKey key)
        {
            return (key.GetHashCode() & 0x7fffffff) % _m;
        }

        private void Resize(int size)
        {
            var tmp = new HashMap<TKey, TValue>(size);
            for (var i = 0; i < _m; i++)
            {
                if (_keys[i] != null)
                    tmp[_keys[i]] = _values[i];
            }
            _keys = tmp._keys;
            _values = tmp._values;
            _m = size;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (var i = 0; i < _m; i++)
            {
                if (_keys[i] != null)
                {
                    yield return new KeyValuePair<TKey, TValue>(_keys[i], _values[i]);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}