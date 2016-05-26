using NUnit.Framework;
using System.Collections.Generic;

namespace NS.CalviScript.Tests
{
    [TestFixture]
    public class EvalVisitorTests
    {
        [TestCase( "2 + 7 / 2 - 3", 2 )]
        [TestCase( "2 + -7 / 2 - 3", -4 )]
        [TestCase( "50 - 70 ? 30 + 2 : 50 * 4", 200 )]
        public void generic_impl_can_evaluate_expression( string input, int expected )
        {
            IExpr expr = Parser.Parse( input );
            var globalContext = new Dictionary<string, int>();
            EvalVisitor sut = new EvalVisitor( globalContext );

            IExpr result = sut.Visit( expr );
            Assert.That( result, Is.InstanceOf<ConstantExpr>() );
            Assert.That( (( ConstantExpr)result).Value, Is.EqualTo( expected ) );
        }
    }
}
