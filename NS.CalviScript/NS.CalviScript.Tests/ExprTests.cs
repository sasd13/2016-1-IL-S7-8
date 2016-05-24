using NUnit.Framework;

namespace NS.CalviScript.Tests
{
    [TestFixture]
    public class ExprTests
    {
        [Test]
        public void can_stringify()
        {
            IExpr expr = new BinaryExpr(
                TokenType.Plus,
                new BinaryExpr(
                    TokenType.Plus,
                    new ConstantExpr( 2 ),
                    new ConstantExpr( 7 ) ),
                new BinaryExpr(
                    TokenType.Mult,
                    new UnaryExpr(
                        TokenType.Minus,
                        new ConstantExpr( 5 ) ),
                    new ConstantExpr( 8 ) ) );

            string result = expr.ToInfixString();

            Assert.That( result, Is.EqualTo( "((2 + 7) + (-5 * 8))" ) );
        }
    }
}
