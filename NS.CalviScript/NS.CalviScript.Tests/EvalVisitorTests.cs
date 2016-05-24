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
            IExpr expr = Parser.Parse( "(3 + 7) / (5 - 2)" );
            EvalVisitor sut = new EvalVisitor();

            sut.Visit( expr );

            Assert.That( sut.Result, Is.EqualTo( 3 ) );
        }

        [Test]
        public void generic_impl_can_evaluate_expression()
        {
            IExpr expr = Parser.Parse( "2 + 7 / 2 - 3" );
            GenericEvalVisitor sut = new GenericEvalVisitor();

            int result = sut.Visit( expr );

            Assert.That( result, Is.EqualTo( 2 ) );
        }
    }
}
