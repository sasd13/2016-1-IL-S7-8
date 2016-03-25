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
            IDictionary<int, int> dic = null;

            int value;
            if( dic.TryGetValue( 78, out value ))
            {

            }
            else
            {
                Assert.That( value, Is.EqualTo( 0 ) );
            }
        }

    }
}
