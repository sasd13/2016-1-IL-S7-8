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
            IExpr expr = Parser.ParseExpression( input );
            var globalContext = new Dictionary<string, int>();
            EvalVisitor sut = new EvalVisitor( globalContext );

            IExpr result = sut.Visit( expr );
            Assert.That( result, Is.InstanceOf<ConstantExpr>() );
            Assert.That( (( ConstantExpr)result).Value, Is.EqualTo( expected ) );
        }

        [Test]
        public void access_to_the_context()
        {
            IExpr expr = Parser.ParseProgram( "x;" );
            var globalContext = new Dictionary<string, int>();
            globalContext.Add( "x", 3712 );
            EvalVisitor sut = new EvalVisitor( globalContext );
            IExpr result = sut.Visit( expr );
            Assert.That( result, Is.InstanceOf<ConstantExpr>() );
            Assert.That( ((ConstantExpr)result).Value, Is.EqualTo( 3712 ) );
        }

        [TestCase( "x;", 3712 )]
        [TestCase( "x+10;", 3712 + 10 )]
        [TestCase( "(x*x)+10;", 3712 * 3712 + 10 )]
        [TestCase( "var a = 3;", 3 )]
        [TestCase( "var a = 3 + x;", 3712 + 3 )]
        [TestCase( "var a = 3 + x; var b = a + 7;", 3712 + 3 + 7 )]
        public void real_eval_tests_with_x_equals_3712( string program, int exptectedValue )
        {
            IExpr expr = Parser.ParseProgram( program );
            var globalContext = new Dictionary<string, int>();
            globalContext.Add( "x", 3712 );
            EvalVisitor sut = new EvalVisitor( globalContext );
            IExpr result = sut.Visit( expr );
            Assert.That( result, Is.InstanceOf<ConstantExpr>() );
            Assert.That( ((ConstantExpr)result).Value, Is.EqualTo( exptectedValue ) );
        }

    }
}
