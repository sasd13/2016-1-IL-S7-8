using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI2016.Dev.Tests
{
    [TestFixture]
    public class DictionaryTests
    {
        [Test]
        public void dictionary_with_string_keys()
        {
            IDictionary<string, int> dic = new Dictionary<string, int>( StringComparer.InvariantCultureIgnoreCase );
            dic.Add( "Soixante-Dix huit", 78 );
            dic.Add( "sept cent Quatre Vingt", 780 );
            dic.Add( "sept", 7 );
            dic.Add( "cinquante Quatre", 54 );
            dic.Add( "trois mille sept cent douze", 3712 );

            Assert.That( dic["sept"], Is.EqualTo( 7 ) );
            Assert.That( dic["Cinquante Quatre"], Is.EqualTo( 54 ) );
        }

        [Test]
        public void using_TryGetValue()
        {
            IDictionary<int, int> dic = new Dictionary<int,int>();
            dic.Add( 78, 987 );

            {
                int value;
                Assert.That( dic.TryGetValue( 78, out value ) );
                Assert.That( value, Is.EqualTo( 987 ) );
            }
            {
                int value;
                Assert.That( dic.TryGetValue( 78876, out value ), Is.False );
                Assert.That( value, Is.EqualTo( 0 ), "This is the default(Int32) value." );
            }
        }

        [Test]
        public void enumerator_test()
        {
            // Arrange
            IDictionary<int, int> dic = new Dictionary<int, int>();
            dic.Add( 78, 987 );
            dic.Add( 9, 1 );
            dic.Add( 7, 13 );
            // Act
            int sumKey = 0;
            int sumValue = 0;
            foreach( var kv in dic )
            {
                sumKey += kv.Key;
                sumValue += kv.Value;
            }
            // Assert
            Assert.That( sumKey, Is.EqualTo( 78 + 9 + 7 ) );
            Assert.That( sumValue, Is.EqualTo( 987 + 1 + 13 ) );
        }

        [Test]
        public void empty_enumerator_test()
        {
            // Arrange
            IDictionary<int, int> dic = new Dictionary<int, int>();
            // Act
            bool found = false;
            foreach( var kv in dic )
            {
                found = true;
            }
            // Assert
            Assert.That( found, Is.False );
        }


        [Test]
        public void foreach_is_a_syntactic_sugar()
        {
            // Arrange
            IDictionary<int, int> dic = new Dictionary<int, int>();
            dic.Add( 655, 98 );

            // BAD!!!: Without Dispose version.
            {
                IEnumerator<KeyValuePair<int, int>> e = dic.GetEnumerator();
                while( e.MoveNext() )
                {
                    int value = e.Current.Value;
                }
            }
            // With Dispose version (full version: try... finally).
            {
                IEnumerator<KeyValuePair<int, int>> e = dic.GetEnumerator();
                try
                {
                    while( e.MoveNext() )
                    {
                        int value = e.Current.Value;
                    }
                }
                finally
                {
                    e.Dispose();
                }
            }

            // With Dispose version (using using syntax).
            {
                using( IEnumerator<KeyValuePair<int, int>> e = dic.GetEnumerator() )
                {
                    while( e.MoveNext() )
                    {
                        int value = e.Current.Value;
                    }
                }
            }
        }
    }
}
