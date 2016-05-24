using NUnit.Framework;

namespace NS.CalviScript.Tests
{
    [TestFixture]
    public class LispyStringVisitorTests
    {
        [TestCase( "5 + 10 % 2", "[+ 5 [% 10 2]]" )]
        [TestCase( "-5 + 10 % 2", "[+ [- 5] [% 10 2]]" )]
        [TestCase( "-5 + 10 % 2 ? 0 : 50 * 2", "[? [+ [- 5] [% 10 2]] 0 [* 50 2]]" )]
        public void generic_impl_can_stringify( string input, string expected )
        {
            IExpr expr = Parser.Parse( input );
            LispyStringVisitor sut = new LispyStringVisitor();

            string result = sut.Visit( expr );

            Assert.That( result, Is.EqualTo( expected ) );
        }
    }
}
