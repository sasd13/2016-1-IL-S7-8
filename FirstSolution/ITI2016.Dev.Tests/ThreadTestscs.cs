using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ITI2016.Dev.Tests
{
    [TestFixture]
    public class ThreadTestscs
    {
        int _counter;

        [Test]
        public void basic_manipulation()
        {
            _counter = 0;
            Console.WriteLine( "Creatng thread..." );
            Thread t = new Thread( DoSomething );
            t.Start();
            Console.WriteLine( "Running!!" );
            IncrementCounter();
            t.Join();
            Console.WriteLine( "Leaving." );
            Assert.That( _counter, Is.EqualTo( 200 * 1000 ) );
        }

        void DoSomething()
        {
            Console.WriteLine( "I'm doing something...." );
            IncrementCounter();
            Console.WriteLine( "I'm done...." );
        }

        object _lock = new object();

        private void IncrementCounter()
        {
            for( int i = 0; i < 100 * 1000; ++i )
            {
                //lock( _lock )
                //{
                //    ++_counter;
                //}
                Interlocked.Increment( ref _counter );
            }

            Monitor.Enter( _lock );

            Monitor.Exit( _lock );
        }
    }
}
