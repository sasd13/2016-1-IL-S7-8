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
    }
}
