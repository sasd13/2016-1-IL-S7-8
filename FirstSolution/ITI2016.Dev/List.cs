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
            if( _count < _array.Length )
            {
                _array[_count++] = e;
            }
            else
            {
                Debug.Assert( _count == _array.Length, "This is obvious!" );
                T[] t = new T[_count + 1];
                for( int i = 0; i < _count; ++i )
                {
                    t[i] = _array[i];
                }
                t[_count++] = e;
                _array = t;
            }
        }

        public void Clear()
        {
            throw new NotImplementedException();
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
