using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI2016.Dev.Tests
{
    [TestFixture]
    public class MyLinqTests
    {

        [Test]
        public void knowing_the_count_of_items_in_any_enumerable()
        {
            List<int> list = new List<int>();
            list.Add( 7 );
            list.Add( 0 );
            list.Add( 99 );

            TestCount( list, 3 );

            list.Add( 9999 );

            TestCount( list, 4 );

            list.RemoveAt( 0 );
            list.RemoveAt( 0 );

            TestCount( list, 2 );

        }

        private void TestCount( IEnumerable<int> container, int expected )
        {
            int count = EnumerableExtension.Count( container );
            Assert.That( count, Is.EqualTo( expected ) );

            int countBetter = container.Count();
            Assert.That( countBetter, Is.EqualTo( expected ) );
        }


        [Test]
        public void where_works()
        {
            //List<int> list = new List<int>( new int[] { 7, 9, 67, 90809 } );
            var list = new List<int>( -7, 8, 67, 90809, 870 );

            var result = list.Where( v => v >= 0 && (v & 1) == 0 );
            using( var eR = result.GetEnumerator() )
            {
                Assert.That( eR.MoveNext() );
                Assert.That( eR.Current, Is.EqualTo( 8 ) );
                Assert.That( eR.MoveNext() );
                Assert.That( eR.Current, Is.EqualTo( 870 ) );
                Assert.That( eR.MoveNext(), Is.False );
            }
        }

    }
}
