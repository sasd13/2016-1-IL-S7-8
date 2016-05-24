using NUnit.Framework;

namespace NS.CalviScript.Tests
{
    [TestFixture]
    public class InfixStringVisitorTests
    {
        [TestCase( "2 + 7 / 12", "(2 + (7 / 12))" )]
        [TestCase( "2 + 7 / -12", "(2 + (7 / -12))" )]
        public void can_stringify( string input, string expected )
        {
            IExpr expr = Parser.Parse( input );
            InfixStringVisitor sut = new InfixStringVisitor();

            sut.Visit( expr );

            Assert.That( sut.Result, Is.EqualTo( expected ) );
        }

        [TestCase( "70 % (50 - 4 * 6)", "(70 % (50 - (4 * 6)))" )]
        [TestCase( "70 % -(50 - 4 * 6)", "(70 % -(50 - (4 * 6)))" )]
        public void generic_impl_can_stringify( string input, string expected )
        {
            IExpr expr = Parser.Parse( input );
            GenericInfixStringVisitor sut = new GenericInfixStringVisitor();

            string result = sut.Visit( expr );

            Assert.That( result, Is.EqualTo( expected ) );
        }
    }
}
