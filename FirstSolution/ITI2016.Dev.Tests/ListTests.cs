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
                switch( index )
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
