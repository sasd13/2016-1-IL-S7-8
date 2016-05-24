using NUnit.Framework;

namespace NS.CalviScript.Tests
{
    [TestFixture]
    public class LispyStringVisitorTests
    {
        [TestCase( "(8 + 10) * (11 % 15)", "[* [+ 8 10] [% 11 15]]" )]
        [TestCase( "(8 + 10) * -(11 % 15)", "[* [+ 8 10] [- [% 11 15]]]" )]
        public void can_stringify( string input, string expected )
        {
            IExpr expr = Parser.Parse( input );
            LispyStringVisitor sut = new LispyStringVisitor();

            sut.Visit( expr );

            Assert.That( sut.Result, Is.EqualTo( expected ) );
        }

        [TestCase( "5 + 10 % 2", "[+ 5 [% 10 2]]" )]
        [TestCase( "-5 + 10 % 2", "[+ [- 5] [% 10 2]]" )]
        public void generic_impl_can_stringify( string input, string expected )
        {
            IExpr expr = Parser.Parse( input );
            GenericLispyStringVisitor sut = new GenericLispyStringVisitor();

            string result = sut.Visit( expr );

            Assert.That( result, Is.EqualTo( expected ) );
        }
    }
}
