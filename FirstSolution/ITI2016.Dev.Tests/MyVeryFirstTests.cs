using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI2016.Dev.Tests
{

    [TestFixture]
    public class MyVeryFirstTests
    {
        [Test]
        public void Console_is_available_in_a_test_method()
        {
            Console.WriteLine( "Hello World!" );
        }

        [Test]
        public void Basic_numeric_types_manipulations()
        {
            // Arrange
            byte b1 = 76;
            byte b2 = 32;

            // Act
            byte b3 = (byte)(b1 + b2);

            // Assert
            Assert.That( b3, Is.EqualTo( 108 ) );
        }


        [TestCase( 130, -126 )]
        [TestCase( 131, -125 )]
        [TestCase( 132, -124 )]
        [TestCase( 100, 100 )]
        public void How_signed_integers_work( byte positive, sbyte negative )
        {
            // Arrange
            byte b = positive;

            // Act
            sbyte sb = (sbyte)b;

            // Assert
            Assert.That( sb, Is.EqualTo( negative ) );
        }

        [Test]
        public void manipulating_IPV4_addresses()
        {
            IPV4 a = new IPV4( 76678976 );
            IPV4 b = new IPV4( 250, 89, 43, 210 );
            Assert.That( b[0], Is.EqualTo( 210 ) );
            Assert.That( b[1], Is.EqualTo( 43 ) );
            Assert.That( b[2], Is.EqualTo( 89 ) );
            Assert.That( b[3], Is.EqualTo( 250 ) );
            Assert.That( b.ToString(), Is.EqualTo( "250.89.43.210" ) );
        }

        [TestCase( 0, "250.89.43.0" )]
        [TestCase( 1, "250.89.0.210" )]
        [TestCase( 2, "250.0.43.210" )]
        [TestCase( 3, "0.89.43.210" )]
        public void clearing_bytes_in_an_IPV4_addresse( int index, string expected )
        {
            IPV4 a = new IPV4( 250, 89, 43, 210 );
            IPV4 a1 = a.ClearByte( index );
            Assert.That( a1.ToString(), Is.EqualTo( expected ) );
        }

        [TestCase( 0, 67, "250.89.43.67" )]
        [TestCase( 1, 255, "250.89.255.210" )]
        [TestCase( 2, 34, "250.34.43.210" )]
        [TestCase( 3, 128, "128.89.43.210" )]
        public void setting_bytes_in_an_IPV4_addresse( int index, byte value, string expected )
        {
            IPV4 a = new IPV4( 250, 89, 43, 210 );
            IPV4 a1 = a.SetByte( index, value );
            Assert.That( a1.ToString(), Is.EqualTo( expected ) );
        }



    }
}
