using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI2016.Dev
{
    public static class EnumerableExtension
    {
        public static int Count<T>( this IEnumerable<T> container )
        {
            int count = 0;
            using( var e = container.GetEnumerator() )
            {
                while( e.MoveNext() ) count++;
            }
            return count;
        }

        class EWhere<T> : IEnumerable<T>
        {
            readonly IEnumerable<T> _container;
            readonly Func<T, bool> _predicate;

            public EWhere( IEnumerable<T> container, Func<T, bool> predicate )
            {
                _container = container;
                _predicate = predicate;
            }

            class E : IEnumerator<T>
            {
                readonly EWhere<T> _holderE;
                readonly IEnumerator<T> _inSource;

                public E( EWhere<T> h )
                {
                    _holderE = h;
                    _inSource = _holderE._container.GetEnumerator();
                }

                public T Current => _inSource.Current;

                public bool MoveNext()
                {
                    while( _inSource.MoveNext() )
                    {
                        if( _holderE._predicate( _inSource.Current ) ) return true;
                    }
                    return false;
                }

                public void Dispose() {}

            }

            public IEnumerator<T> GetEnumerator()
            {
                return new E( this );
            }
        }

        /// <summary>
        /// This is the Selection operator.
        /// </summary>
        /// <typeparam name="T">Type of the items.</typeparam>
        /// <param name="container">This enumerable.</param>
        /// <param name="predicate">Function used to filter container's items.</param>
        /// <returns>
        /// Filtered container: only items for which <paramref name="predicate"/> 
        /// evaluates to true are kept.
        /// </returns>
        public static IEnumerable<T> Where<T>( this IEnumerable<T> container, Func<T, bool> predicate )
        {
            return new EWhere<T>( container, predicate );
        }

        class ESelect<T, TResult> : IEnumerable<TResult>
        {
            readonly IEnumerable<T> _container;
            readonly Func<T, TResult> _proj;

            public ESelect( IEnumerable<T> container, Func<T, TResult> proj )
            {
                _container = container;
                _proj = proj;
            }

            class E : IEnumerator<TResult>
            {
                readonly ESelect<T,TResult> _holderE;
                readonly IEnumerator<T> _inSource;
                TResult _current;
                bool _currentHasBeenComputed;

                public E( ESelect<T,TResult> h )
                {
                    _holderE = h;
                    _inSource = _holderE._container.GetEnumerator();
                }

                public TResult Current
                {
                    get
                    {
                        if( !_currentHasBeenComputed )
                        {
                            _current = _holderE._proj( _inSource.Current );
                            _currentHasBeenComputed = true;
                        }
                        return _current;
                    }
                }
                public bool MoveNext()
                {
                    _currentHasBeenComputed = false;
                    return _inSource.MoveNext();
                }

                public void Dispose() { }

            }

            public IEnumerator<TResult> GetEnumerator()
            {
                return new E( this );
            }
        }


        public static IEnumerable<TResult> Select<T, TResult>( this IEnumerable<T> container, Func<T, TResult> projection )
        {
            return new ESelect<T, TResult>( container, projection );
        }

    }


}
