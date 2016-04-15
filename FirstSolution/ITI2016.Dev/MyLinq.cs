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
        public static IEnumerable<T> Where<T>( this IEnumerable<T> container, Func<T,bool> predicate )
        {
            return new EWhere<T>( container, predicate );
        }

    }


}
