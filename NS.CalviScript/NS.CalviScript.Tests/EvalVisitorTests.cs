using NUnit.Framework;

namespace NS.CalviScript.Tests
{
    [TestFixture]
    public class EvalVisitorTests
    {
        [TestCase( "2 + 7 / 2 - 3", 2 )]
        [TestCase( "2 + -7 / 2 - 3", -4 )]
        public void generic_impl_can_evaluate_expression( string input, int expected )
        {
            IExpr expr = Parser.Parse( input );
            EvalVisitor sut = new EvalVisitor();

            int result = sut.Visit( expr );

            Assert.That( result, Is.EqualTo( expected ) );
        }
    }
}
