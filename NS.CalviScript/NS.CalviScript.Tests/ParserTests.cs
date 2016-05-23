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
        [TestCase( "2 * 3 + 5", "[+ [* 2 3] 5]" )]
        [TestCase( "(11 + 7) / 15 % 8", "[% [/ [+ 11 7] 15] 8]" )]
        [TestCase( "10 + (11 + 50)", "[+ 10 [+ 11 50]]" )]
        [TestCase( "2 + 3 * 5", "[+ 2 [* 3 5]]" )]
        public void parse_simple_expr( string input, string expected )
        {
            Tokenizer tokenizer = new Tokenizer( input );
            Parser sut = new Parser( tokenizer );

            IExpr expr = sut.ParseExpression();

            Assert.That( expr.ToLispyString(), Is.EqualTo( expected ) );
        }
    }
}
