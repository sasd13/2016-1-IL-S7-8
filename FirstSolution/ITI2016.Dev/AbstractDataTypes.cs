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
