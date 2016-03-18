using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI2016.Dev.Tests
{
    [TestFixture]
    public class ComplexityMeasureTests
    {

        [Test]
        public void testing_list_implementation()
        {
            Stopwatch w = new Stopwatch();

            int start = 1000;
            int step = 10000;
            int count = 10;

            for( int n = start; count-- > 0; n += step )
            {
                List<int> list = new List<int>();
                //
                // GC.Collect is actually useful here.
                // Without it, every 5 steps, the GC is working and impacts performance...
                //
                // GC.Collect();
                w.Restart();
                for( int i = 0; i < n; i++ )
                {
                    list.Add( 987 );
                }
                w.Stop();

                Console.WriteLine( "{0}\t{1}", n, w.ElapsedTicks );
            }
        }

    }
}
