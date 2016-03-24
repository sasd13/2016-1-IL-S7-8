using ITI2016.Dev;
using System;

namespace ITI2016.Dev
{
    class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        List<KeyValuePair<TKey, TValue>> _list;

        public Dictionary()
        {
            _list = new List<KeyValuePair<TKey, TValue>>();
        }

        public TValue this[TKey k]
        {
            get
            {
                foreach (KeyValuePair<TKey, TValue> keyValuePair in _list)
                {
                    if (keyValuePair.Key.Equals(k))
                    {
                        return keyValuePair.Value;
                    }
                }
                
                throw new ArgumentException();
            }

            set
            {
                bool setted = false;
                IEnumerator<KeyValuePair<TKey, TValue>> enumerator = GetEnumerator();
                int index = -1;
                while (enumerator.MoveNext())
                {
                    index++;
                    if (enumerator.Current.Key.Equals(k))
                    {
                        _list[index] = new KeyValuePair<TKey, TValue>(k, value);
                        setted = true;
                        break;
                    }
                }

                if (!setted)
                {
                    performAdd(k, value);
                }
            }
        }

        private void performAdd(TKey k, TValue v)
        {
            _list.Add(new KeyValuePair<TKey, TValue>(k, v));
        }

        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        public IEnumerable<TKey> Keys
        {
            get
            {
                List<TKey> enumerable = new List<TKey>();
                foreach (KeyValuePair<TKey, TValue> keyValuePair in _list)
                {
                    enumerable.Add(keyValuePair.Key);
                }

                return enumerable;
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                List<TValue> enumerable = new List<TValue>();
                foreach (KeyValuePair<TKey, TValue> keyValuePair in _list)
                {
                    enumerable.Add(keyValuePair.Value);
                }

                return enumerable;
            }
        }

        public void Add(TKey k, TValue v)
        {
            if (ContainsKey(k))
            {
                throw new InvalidOperationException();
            }

            performAdd(k, v);
        }

        public bool ContainsKey(TKey k)
        {
            foreach (KeyValuePair<TKey, TValue> keyValuePair in _list)
            {
                if (keyValuePair.Key.Equals(k))
                {
                    return true;
                }
            }

            return false;
        }

        public bool ContainsValue(TValue v)
        {
            foreach (KeyValuePair<TKey, TValue> keyValuePair in _list)
            {
                if (keyValuePair.Value.Equals(v))
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            IEnumerator<KeyValuePair<TKey, TValue>> enumerator = GetEnumerator();
            int index = -1;
            while (enumerator.MoveNext())
            {
                index++;
                if (enumerator.Current.Key.Equals(key))
                {
                    _list.RemoveAt(index);
                    return true;
                }
            }

            return false;
        }
    }
}
