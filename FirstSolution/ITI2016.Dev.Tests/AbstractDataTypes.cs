using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI2016.Dev.Tests
{

    interface IEnumerable<T>
    {
        IEnumerator<T> GetEnumerator();
    }

    interface IReadOnlyCollection<T>
    {
        int Count { get; }

        IEnumerator<T> GetEnumerator();
    }

    interface IReadOnlyList<T> : IReadOnlyCollection<T>
    {
        T this[int i] { get; }
    }

    interface IList<T> : IReadOnlyList<T>
    {
        new T this[int i] { get; set; }

        void Add( T e );

        void InsertAt( int i, T e );

        void RemoveAt( int i );
    }

    interface ISet<T> : IReadOnlyCollection<T>
    {
        void Add( T e );

        bool Contains( T e );

        void Remove( T e );
    }

    interface IEnumerator<T>
    {
        bool MoveNext();

        T Current { get; }
    }

    interface IEnumeratorJava<T>
    {
        bool HasNext();

        T GetNext();

    }



}
