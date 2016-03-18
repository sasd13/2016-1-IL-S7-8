using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI2016.Dev
{
    public class List<T> : IList<T>
    {
        T[] _array;
        int _count;

        public List()
        {
            _array = new T[4];
        }


        public T this[int i]
        {
            get
            {
                if( i < 0 || i >= _count ) throw new InvalidOperationException();
                return _array[i];
            }

            set
            {
                if( i < 0 || i >= _count ) throw new InvalidOperationException();
                _array[i] = value;
            }
        }

        public int Count => _count;

        public void Add( T e )
        {
            Debug.Assert( _count <= _array.Length, "This is an INVARIANT !!!!!!!" );
            if( _count == _array.Length )
            {
                T[] t = new T[_count + 1];
                for( int i = 0; i < _count; ++i )
                {
                    t[i] = _array[i];
                }
                _array = t;
            }
            Debug.Assert( _count < _array.Length );
            _array[_count++] = e;
        }

        public void Clear()
        {
            if( !typeof(T).IsValueType )
            {
                for( int i = 0; i < _count; ++i ) _array[i] = default(T);
            }
            _count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void InsertAt( int i, T e )
        {
            throw new NotImplementedException();
        }

        public void RemoveAt( int i )
        {
            throw new NotImplementedException();
        }
    }
}
