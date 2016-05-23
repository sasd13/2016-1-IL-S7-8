using NUnit.Framework;

namespace NS.CalviScript.Tests
{
    [TestFixture]
    public class InfixStringVisitorTests
    {
        [Test]
        public void can_stringify()
        {
            Tokenizer tokenizer = new Tokenizer( "2 + 7 / 12" );
            Parser parser = new Parser( tokenizer );
            IExpr expr = parser.ParseExpression();
            InfixStringVisitor sut = new InfixStringVisitor();

            sut.Visit( expr );

            Assert.That( sut.Result, Is.EqualTo( "(2 + (7 / 12))" ) );
        }
    }
}
