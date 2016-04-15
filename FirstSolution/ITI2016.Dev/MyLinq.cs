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

                public E( EWhere<T> h )
                {
                    _holderE = h;
                }

                public T Current
                {
                    get
                    {
                        throw new NotImplementedException();
                    }
                }

                public bool MoveNext()
                {
                    throw new NotImplementedException();
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
