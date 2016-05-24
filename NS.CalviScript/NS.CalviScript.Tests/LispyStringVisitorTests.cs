using NUnit.Framework;

namespace NS.CalviScript.Tests
{
    [TestFixture]
    public class LispyStringVisitorTests
    {
        [Test]
        public void can_stringify()
        {
            Tokenizer tokenizer = new Tokenizer( "(8 + 10) * (11 % 15)" );
            Parser parser = new Parser( tokenizer );
            IExpr expr = parser.ParseExpression();
            LispyStringVisitor sut = new LispyStringVisitor();

            sut.Visit( expr );

            Assert.That( sut.Result, Is.EqualTo( "[* [+ 8 10] [% 11 15]]" ) );
        }

        [Test]
        public void generic_impl_can_stringify()
        {
            Tokenizer tokenizer = new Tokenizer( "5 + 10 % 2" );
            Parser parser = new Parser( tokenizer );
            IExpr expr = parser.ParseExpression();
            GenericLispyStringVisitor sut = new GenericLispyStringVisitor();

            string result = sut.Visit( expr );

            Assert.That( result, Is.EqualTo( "[+ 5 [% 10 2]]" ) );
        }
    }
}
