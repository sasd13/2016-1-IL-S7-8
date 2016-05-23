using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NS.CalviScript.Tests
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void SimpleTest()
        {
            Tokenizer tokenizer = new Tokenizer( "2 * 3 + 5" );
            Parser sut = new Parser( tokenizer );

            IExpr expr = sut.ParseOperation();

            Assert.That( expr.ToLispyString(), Is.EqualTo( "[+ [* 2 3] 5]" ) );
        }
    }
}
