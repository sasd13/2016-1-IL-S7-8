using NUnit.Framework;

namespace NS.CalviScript.Tests
{
    [TestFixture]
    public class EvalVisitorTests
    {
        [TestCase( "(3 + 7) / (5 - 2)", 3 )]
        [TestCase( "(3 + 7) / -(5 - 2)", -3 )]
        public void can_evaluate_expression( string input, int expected )
        {
            IExpr expr = Parser.Parse( input );
            EvalVisitor sut = new EvalVisitor();

            sut.Visit( expr );

            Assert.That( sut.Result, Is.EqualTo( expected ) );
        }

        [TestCase( "2 + 7 / 2 - 3", 2 )]
        [TestCase( "2 + -7 / 2 - 3", -4 )]
        public void generic_impl_can_evaluate_expression( string input, int expected )
        {
            IExpr expr = Parser.Parse( input );
            GenericEvalVisitor sut = new GenericEvalVisitor();

            int result = sut.Visit( expr );

            Assert.That( result, Is.EqualTo( expected ) );
        }
    }
}
