using NUnit.Framework;

namespace NS.CalviScript.Tests
{
    [TestFixture]
    public class InfixStringVisitorTests
    {
        [TestCase( "70 % (50 - 4 * 6)", "(70 % (50 - (4 * 6)))" )]
        [TestCase( "70 % -(50 - 4 * 6)", "(70 % -(50 - (4 * 6)))" )]
        [TestCase( "5 + (40 + 7 ? 8 - 7 : 13 % 2)", "(5 + ((40 + 7) ? (8 - 7) : (13 % 2)))" )]
        public void generic_impl_can_stringify( string input, string expected )
        {
            IExpr expr = Parser.ParseExpression( input );
            InfixStringVisitor sut = new InfixStringVisitor();

            string result = sut.Visit( expr );

            Assert.That( result, Is.EqualTo( expected ) );
        }
    }
}
