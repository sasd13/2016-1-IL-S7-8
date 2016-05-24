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
        [TestCase( "2 * -7", "[* 2 [- 7]]" )]
        [TestCase( "5 + 7 ? -8 * 2 : (7 * 3 ? 0 : 15)", "[? [+ 5 7] [* [- 8] 2] [? [* 7 3] 0 15]]" )]
        [TestCase( "5 + 7 ? -8 * 2 : 7 * 3 ? 0 : 15", "[? [+ 5 7] [* [- 8] 2] [? [* 7 3] 0 15]]" )]
        public void parse_simple_expr( string input, string expected )
        {
            Tokenizer tokenizer = new Tokenizer( input );
            Parser sut = new Parser( tokenizer );

            IExpr expr = sut.ParseExpression();

            LispyStringVisitor visitor = new LispyStringVisitor();
            Assert.That( visitor.Visit( expr ), Is.EqualTo( expected ) );
        }
    }
}
