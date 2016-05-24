using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NS.CalviScript.Tests
{
    [TestFixture]
    public class EvalVisitorTests
    {
        [Test]
        public void can_evaluate_expression()
        {
            Tokenizer tokenizer = new Tokenizer( "(3 + 7) / (5 - 2)" );
            Parser parser = new Parser( tokenizer );
            IExpr expr = parser.ParseExpression();
            EvalVisitor sut = new EvalVisitor();

            sut.Visit( expr );

            Assert.That( sut.Result, Is.EqualTo( 3 ) );
        }
    }
}
