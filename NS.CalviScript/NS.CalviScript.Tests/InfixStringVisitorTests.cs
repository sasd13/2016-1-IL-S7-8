using NUnit.Framework;

namespace NS.CalviScript.Tests
{
    [TestFixture]
    public class InfixStringVisitorTests
    {
        [Test]
        public void can_stringify()
        {
            IExpr expr = Parser.Parse( "2 + 7 / 12" );
            InfixStringVisitor sut = new InfixStringVisitor();

            sut.Visit( expr );

            Assert.That( sut.Result, Is.EqualTo( "(2 + (7 / 12))" ) );
        }

        [Test]
        public void generic_impl_can_stringify()
        {
            IExpr expr = Parser.Parse( "70 % (50 - 4 * 6)" );
            GenericInfixStringVisitor sut = new GenericInfixStringVisitor();

            string result = sut.Visit( expr );

            Assert.That( result, Is.EqualTo( "(70 % (50 - (4 * 6)))" ) );
        }
    }
}
