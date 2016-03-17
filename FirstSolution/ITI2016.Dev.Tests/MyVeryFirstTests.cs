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


        [Test]
        // Version n°0:
        //[ExpectedException(typeof(IndexOutOfRangeException))]
        public void check_that_index_is_controlled_Version_1()
        {
            try
            {
                int x = new IPV4( 87987 )[5];

                new IPV4( 87987 ).SetByte( 5, 99 );

                string msg = string.Format( "This SHOULD have thrown a {0}", "IndexOutOfRangeException" );
                throw new AssertionException( msg );
            }
            catch( IndexOutOfRangeException ex )
            {
            }
        }

        abstract class ExceptionChecker
        {
            public void Run()
            {
                try
                {
                    DoAction();
                    string msg = string.Format( "This SHOULD have thrown a {0}", "IndexOutOfRangeException" );
                    throw new AssertionException( msg );
                }
                catch( IndexOutOfRangeException ex )
                {
                }
            }

            protected abstract void DoAction();
        }

        class IndexControl1 : ExceptionChecker
        {
            protected override void DoAction()
            {
                int x = new IPV4( 87987 )[5];
            }
        }

        class IndexControl2 : ExceptionChecker
        {
            protected override void DoAction()
            {
                new IPV4( 87987 ).SetByte( 5, 98 );
            }
        }

        [Test]
        public void check_that_index_is_controlled_Version_2()
        {
            new IndexControl1().Run();
            new IndexControl2().Run();
        }

        abstract class ExceptionCheckerVERSION0<T>
        {
            public void Run()
            {
                try
                {
                    DoAction();
                }
                catch( Exception ex )
                {
                    if( ex is T ) return;
                }
                string msg = string.Format( "This SHOULD have thrown a {0}", typeof( T ).Name );
                throw new AssertionException( msg );
            }

            protected abstract void DoAction();
        }

        abstract class ExceptionChecker<T> where T : Exception
        {
            public void Run()
            {
                try
                {
                    DoAction();
                    string msg = string.Format( "This SHOULD have thrown a {0}", typeof( T ).Name );
                    throw new AssertionException( msg );
                }
                catch( T )
                {
                }
            }

            protected abstract void DoAction();
        }

        class V3IndexControl1 : ExceptionChecker<IndexOutOfRangeException>
        {
            protected override void DoAction()
            {
                int x = new IPV4( 87987 )[5];
            }
        }

        class V3IndexControl2 : ExceptionChecker<IndexOutOfRangeException>
        {
            protected override void DoAction()
            {
                new IPV4( 87987 ).SetByte( 5, 98 );
            }
        }

        [Test]
        public void check_that_index_is_controlled_Version_3()
        {
            new V3IndexControl1().Run();
            new V3IndexControl2().Run();
        }

        [Test]
        public void check_that_index_is_controlled_Version_4()
        {
            ASSERTThrows<IndexOutOfRangeException>( TestGetter );
            ASSERTThrows<IndexOutOfRangeException>( TestSetter );
        }

        private void ASSERTThrows<T>( Action shouldThrow ) where T : Exception
        {
            try
            {
                shouldThrow();
                string msg = string.Format( "This SHOULD have thrown a {0}", typeof( T ).Name );
                throw new AssertionException( msg );
            }
            catch( T )
            {
            }
        }

        static void TestGetter()
        {
            int x = new IPV4( 87987 )[5];
        }

        static void TestSetter()
        {
            new IPV4( 87987 ).SetByte( 5, 98 );
        }

        [Test]
        public void check_that_index_is_controlled_Version_5()
        {
            ASSERTThrows<IndexOutOfRangeException>( delegate() { int x = new IPV4( 87987 )[5]; } );
            ASSERTThrows<IndexOutOfRangeException>( delegate() { new IPV4( 87987 ).SetByte( 5, 98 ); } );
        }


    }
}
