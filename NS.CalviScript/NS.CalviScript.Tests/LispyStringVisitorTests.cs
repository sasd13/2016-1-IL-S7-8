using NUnit.Framework;

namespace NS.CalviScript.Tests
{
    [TestFixture]
    public class LispyStringVisitorTests
    {
        [Test]
        public void can_stringify()
        {
            IExpr expr = Parser.Parse( "(8 + 10) * (11 % 15)" );
            LispyStringVisitor sut = new LispyStringVisitor();

            sut.Visit( expr );

            Assert.That( sut.Result, Is.EqualTo( "[* [+ 8 10] [% 11 15]]" ) );
        }

        [Test]
        public void generic_impl_can_stringify()
        {
            IExpr expr = Parser.Parse( "5 + 10 % 2" );
            GenericLispyStringVisitor sut = new GenericLispyStringVisitor();

            string result = sut.Visit( expr );

            Assert.That( result, Is.EqualTo( "[+ 5 [% 10 2]]" ) );
        }
    }
}
