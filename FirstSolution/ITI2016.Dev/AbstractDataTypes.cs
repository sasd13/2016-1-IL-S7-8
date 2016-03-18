using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI2016.Dev
{

    public interface IEnumerable<T>
    {
        IEnumerator<T> GetEnumerator();
    }

    public interface IReadOnlyCollection<T> : IEnumerable<T>
    {
        int Count { get; }
    }

    public interface IReadOnlyList<T> : IReadOnlyCollection<T>
    {
        T this[int i] { get; }
    }

    public interface IList<T> : IReadOnlyList<T>
    {
        new T this[int i] { get; set; }

        void Add( T e );

        void InsertAt( int i, T e );

        void RemoveAt( int i );

        void Clear();
    }

    public interface ISet<T> : IReadOnlyCollection<T>
    {
        void Add( T e );

        bool Contains( T e );

        void Remove( T e );
    }

    public struct KeyValuePair<TKey,TValue>
    {
        public readonly TKey Key;
        public readonly TValue Value;

        public KeyValuePair( TKey key, TValue value )
        {
            Key = key;
            Value = value;
        }
    }

    public interface IDictionary<TKey, TValue> : IReadOnlyCollection<KeyValuePair<TKey,TValue>>
    {
        IEnumerable<TKey> Keys { get; }

        IEnumerable<TValue> Values { get; }

        bool ContainsKey( TKey k );

        /// <summary>
        /// Checks whether this dictionary contains the given value.
        /// Caution: this is an O(n) operation!
        /// </summary>
        /// <param name="v">The value to lookup.</param>
        /// <returns>Tru when found, false otherwise.</returns>
        bool ContainsValue( TValue v );

        /// <summary>
        /// Removes a key/value pair.
        /// </summary>
        /// <param name="key">The key to remove.</param>
        /// <returns>True if the key has been found and removed, false otherwise.</returns>
        bool Remove( TKey key );

        /// <summary>
        /// Gets or sets the value associated to the given key.
        /// When getting, the key MUST exist otherwise a <see cref="KeyNotFoundException"/> is 
        /// thrown.
        /// </summary>
        /// <param name="k">The key.</param>
        /// <returns>The associated value.</returns>
        TValue this[TKey k] { get; set; }

        /// <summary>
        /// Adds a key/value pair. The key MUST NOT exist otherwise an exception is thrown.
        /// </summary>
        /// <param name="k">The key to add.</param>
        /// <param name="v">The associated value.</param>
        void Add( TKey k, TValue v );


    }

    public interface IEnumerator<T>
    {
        bool MoveNext();

        T Current { get; }
    }

    public interface IEnumeratorJava<T>
    {
        bool HasNext();

        T GetNext();

    }



}
