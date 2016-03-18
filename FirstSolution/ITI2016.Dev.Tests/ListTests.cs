using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI2016.Dev.Tests
{
    [TestFixture]
    public class ListTests
    {

        [Test]
        public void adding_items()
        {
            List<int> list = new List<int>();
            Assert.That( list.Count, Is.EqualTo( 0 ) );
            Assert.Throws<IndexOutOfRangeException>( () => Console.Write( list[-1] ) );
            Assert.Throws<IndexOutOfRangeException>( () => Console.Write( list[0] ) );
            list.Add( 8 );
            Assert.That( list.Count, Is.EqualTo( 1 ) );
            Assert.That( list[0], Is.EqualTo( 8 ) );
            Assert.Throws<IndexOutOfRangeException>( () => Console.Write( list[1] ) );
            list.Add( -1 );
            Assert.That( list.Count, Is.EqualTo( 2 ) );
            Assert.That( list[0], Is.EqualTo( 8 ) );
            Assert.That( list[1], Is.EqualTo( -1 ) );
            Assert.Throws<IndexOutOfRangeException>( () => Console.Write( list[2] ) );
            list.Add( 0 );
            Assert.That( list.Count, Is.EqualTo( 3 ) );
            Assert.That( list[0], Is.EqualTo( 8 ) );
            Assert.That( list[1], Is.EqualTo( -1 ) );
            Assert.That( list[2], Is.EqualTo( 0 ) );
            Assert.Throws<IndexOutOfRangeException>( () => Console.Write( list[3] ) );
            list.Add( 987 );
            Assert.That( list.Count, Is.EqualTo( 4 ) );
            Assert.That( list[0], Is.EqualTo( 8 ) );
            Assert.That( list[1], Is.EqualTo( -1 ) );
            Assert.That( list[2], Is.EqualTo( 0 ) );
            Assert.That( list[3], Is.EqualTo( 987 ) );
            Assert.Throws<IndexOutOfRangeException>( () => Console.Write( list[4] ) );
            list.Add( 765678 );
            Assert.That( list.Count, Is.EqualTo( 5 ) );
            Assert.That( list[0], Is.EqualTo( 8 ) );
            Assert.That( list[1], Is.EqualTo( -1 ) );
            Assert.That( list[2], Is.EqualTo( 0 ) );
            Assert.That( list[3], Is.EqualTo( 987 ) );
            Assert.That( list[4], Is.EqualTo( 765678 ) );
            Assert.Throws<IndexOutOfRangeException>( () => Console.Write( list[5] ) );
            list.Add( 7868 );
            Assert.That( list.Count, Is.EqualTo( 6 ) );
            Assert.That( list[0], Is.EqualTo( 8 ) );
            Assert.That( list[1], Is.EqualTo( -1 ) );
            Assert.That( list[2], Is.EqualTo( 0 ) );
            Assert.That( list[3], Is.EqualTo( 987 ) );
            Assert.That( list[4], Is.EqualTo( 765678 ) );
            Assert.That( list[5], Is.EqualTo( 7868 ) );
            Assert.Throws<IndexOutOfRangeException>( () => Console.Write( list[6] ) );
        }

        [Test]
        public void removing_items()
        {
            List<int> list = new List<int>();
            list.Add( 1 );
            list.Add( 2 );
            list.Add( 3 );
            list.Add( 4 );
            list.Add( 5 );
            list.Add( 6 );
            Assert.That( list.Count, Is.EqualTo( 6 ) );
            list.RemoveAt( 3 );
            Assert.That( list.Count, Is.EqualTo( 5 ) );
            Assert.That( list[0], Is.EqualTo( 1 ) );
            Assert.That( list[1], Is.EqualTo( 2 ) );
            Assert.That( list[2], Is.EqualTo( 3 ) );
            Assert.That( list[3], Is.EqualTo( 5 ) );
            Assert.That( list[4], Is.EqualTo( 6 ) );

            list.RemoveAt( 0 );
            Assert.That( list.Count, Is.EqualTo( 4 ) );
            Assert.That( list[0], Is.EqualTo( 2 ) );
            Assert.That( list[1], Is.EqualTo( 3 ) );
            Assert.That( list[2], Is.EqualTo( 5 ) );
            Assert.That( list[3], Is.EqualTo( 6 ) );

            list.RemoveAt( 3 );
            Assert.That( list.Count, Is.EqualTo( 3 ) );
            Assert.That( list[0], Is.EqualTo( 2 ) );
            Assert.That( list[1], Is.EqualTo( 3 ) );
            Assert.That( list[2], Is.EqualTo( 5 ) );
        }

        [Test]
        public void inserting_items()
        {
            List<int> list = new List<int>();
            list.InsertAt( 0, 876 );
            Assert.That( list.Count, Is.EqualTo( 1 ) );
            Assert.That( list[0], Is.EqualTo( 876 ) );

            list.InsertAt( 0, -5 );
            Assert.That( list.Count, Is.EqualTo( 2 ) );
            Assert.That( list[0], Is.EqualTo( -5 ) );
            Assert.That( list[1], Is.EqualTo( 876 ) );

            list.InsertAt( 1, 98 );
            Assert.That( list.Count, Is.EqualTo( 3 ) );
            Assert.That( list[0], Is.EqualTo( -5 ) );
            Assert.That( list[1], Is.EqualTo( 98 ) );
            Assert.That( list[2], Is.EqualTo( 876 ) );

            list.InsertAt( 3, -1000 );
            Assert.That( list.Count, Is.EqualTo( 4 ) );
            Assert.That( list[0], Is.EqualTo( -5 ) );
            Assert.That( list[1], Is.EqualTo( 98 ) );
            Assert.That( list[2], Is.EqualTo( 876 ) );
            Assert.That( list[3], Is.EqualTo( -1000 ) );

            list.InsertAt( 3, 12 );
            Assert.That( list.Count, Is.EqualTo( 5 ) );
            Assert.That( list[0], Is.EqualTo( -5 ) );
            Assert.That( list[1], Is.EqualTo( 98 ) );
            Assert.That( list[2], Is.EqualTo( 876 ) );
            Assert.That( list[3], Is.EqualTo( 12 ) );
            Assert.That( list[4], Is.EqualTo( -1000 ) );
        }

        [Test]
        public void using_our_enumerator_on_an_empty_list()
        {
            List<int> list = new List<int>();
            IEnumerator<int> e = list.GetEnumerator();
            Assert.That( e.MoveNext(), Is.False );
            Assert.Throws<InvalidOperationException>( () => { int x = e.Current; } );
        }

        [Test]
        public void using_our_enumerator()
        {
            List<int> list = new List<int>();
            list.Add( 8 );
            list.Add( -1 );
            list.Add( 0 );
            list.Add( 987 );
            IEnumerator<int> e = list.GetEnumerator();
            int index = 0;
            while( e.MoveNext() )
            {
                switch( index++ )
                {
                    case 0: Assert.That( e.Current == 8 ); break;
                    case 1: Assert.That( e.Current == -1 ); break;
                    case 2: Assert.That( e.Current == 0 ); break;
                    case 3: Assert.That( e.Current == 987 ); break;
                }
            }
            Assert.Throws<InvalidOperationException>( () => Console.Write( e.Current ) );
        }



    }
}
