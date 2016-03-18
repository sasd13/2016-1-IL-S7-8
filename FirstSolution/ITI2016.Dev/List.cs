﻿using System;
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
                if( i < 0 || i >= _count ) throw new IndexOutOfRangeException();
                return _array[i];
            }

            set
            {
                if( i < 0 || i >= _count ) throw new IndexOutOfRangeException();
                _array[i] = value;
            }
        }

        public int Count => _count;

        public void Add( T e )
        {
            Debug.Assert( _count <= _array.Length, "This is an INVARIANT !!!!!!!" );
            EnsureEnoughSpace();
            _array[_count++] = e;
        }

        private void EnsureEnoughSpace()
        {
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
            return new ListEnumerator(this);
        }

        public void InsertAt( int i, T e )
        {
            if( i < 0 || i > _count ) throw new IndexOutOfRangeException();
            EnsureEnoughSpace();
            int lenToCopy = _count++ - i;
            if( lenToCopy > 0 ) Array.Copy( _array, i, _array, i + 1, lenToCopy );
            _array[i] = e;
        }

        public void RemoveAt( int i )
        {
            if( i < 0 || i >= _count ) throw new IndexOutOfRangeException();
            Debug.Assert( _count >= 1 );
            Array.Copy( _array, i + 1, _array, i, --_count - i );
            _array[_count] = default( T );
        }

        private class ListEnumerator : IEnumerator<T>
        {
            readonly List<T> list;
            int current_index;

            public ListEnumerator(List<T> m_list)
            {
                list = m_list;
                current_index = -1;
            }

            public T Current
            {
                get
                {
                    if (current_index == -1 || current_index == list._count) throw new InvalidOperationException();

                    return list[current_index];
                }
            }

            public bool MoveNext()
            {
                if (list._count == 0 || current_index == list._count) return false;
                current_index++;

                return true;
            }
        }
    }
}
