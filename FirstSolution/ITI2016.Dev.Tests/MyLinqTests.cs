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
            var list = new List<int>( -7, 8, 67, 90809, 870, -6 );

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

        [Test]
        public void selection_and_projection_and_selection()
        {
            //List<int> list = new List<int>( new int[] { 7, 9, 67, 90809 } );
            var list = new List<int>( -7, 8, 67, -50, 90809, 870, -6, 90, -4, 8006 );

            var positive = list.Where( v => v >= 0 );
            var positiveString = positive.Select( v => v.ToString() );
            var withZero = positiveString.Where( s => s.Contains( '0' ) );
            var backToInt = withZero.Select( s => int.Parse( s ) );

            var backToInt2 = list.Where( v => v >= 0 )
                            .Select( v => v.ToString() )
                            .Where( s => s.Contains( '0' ) )
                            .Select( s => int.Parse( s ) );

            var backToInt3 = from v in list
                             where v >= 0
                             let s = v.ToString()
                             where s.Contains( '0' )
                             select int.Parse( s );


            var backToInt4 = EnumerableExtension.Select(
                                EnumerableExtension.Where(
                                    EnumerableExtension.Select(
                                        EnumerableExtension.Where( list, v => v >= 0 ),
                                        v => v.ToString() ),
                                    s => s.Contains( '0' ) ),
                                s => int.Parse( s ) );

            using( var eR = backToInt.GetEnumerator() )
            {
                Assert.That( eR.MoveNext() );
                Assert.That( eR.Current, Is.EqualTo( 90809 ) );
                Assert.That( eR.MoveNext() );
                Assert.That( eR.Current, Is.EqualTo( 870 ) );
                Assert.That( eR.MoveNext() );
                Assert.That( eR.Current, Is.EqualTo( 90 ) );
                Assert.That( eR.MoveNext() );
                Assert.That( eR.Current, Is.EqualTo( 8006 ) );
                Assert.That( eR.MoveNext(), Is.False );
            }
        }

        [Test]
        public void useful_linq_methods()
        {
            //List<int> list = new List<int>( new int[] { 7, 9, 67, 90809 } );
            var list = new List<int>( -7, 8, 67, -50, 90809, 870, -6, 90, -4, 8006 );

            SubMethod( new int[] { 7, 9, 67, 90809 } );

        }

        private void SubMethod( System.Collections.Generic.IEnumerable<int> list )
        {
            var e = Enumerable.Empty<IPV4>();
            var firstDaysOfYears = Enumerable.Range( 1, 2000 )
                                        .Select( y => new DateTime( y, 1, 1 ).DayOfWeek );
            foreach( var dayName in firstDaysOfYears )
            {
                Console.WriteLine( dayName );
            }

            if( list.Count() == 0 )
            {
                //.... BAD !!!!!!! Prefer Any below.
            }
            if( list.Any( i => (i & 1) != 0 ) )
            {
                //....
            }
            foreach( var inFirstHundred in list.Take( 100 ) )
            {
                //....
            }
            foreach( var inFirstHundredAfter20 in list.Skip( 20 ).Take( 100 ) )
            {
                //....
            }
            foreach( var ordered in list.OrderBy( Identity ) )
            {
                //....
            }
            foreach( var ordered in list.OrderByDescending( Identity ) )
            {
                //....
            }
            //foreach( var users in list.OrderByDescending( u => u.LastName + '|' + u.FirstName ) ) )
            //{
            //    //....
            //}
            //foreach( var users in list
            //                        .OrderByDescending( u => u.LastName )
            //                        .AndThenBy( u => u.FirstName ) ) )
            //{
            //    //....
            //}
        }

        static T Identity<T>( T x ) => x;

    }


}
