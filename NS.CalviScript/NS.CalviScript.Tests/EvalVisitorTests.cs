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
            var globalContext = new Dictionary<string, ValueBase>();
            EvalVisitor sut = new EvalVisitor( globalContext );

            ValueBase result = sut.Visit( expr );
            Assert.That( result, Is.InstanceOf<IntegerValue>() );
            Assert.That( ((IntegerValue)result).Value, Is.EqualTo( expected ) );
        }

        [Test]
        public void access_to_the_context()
        {
            IExpr expr = Parser.ParseProgram( "x;" );
            var globalContext = new Dictionary<string, ValueBase>();
            globalContext.Add( "x", IntegerValue.Create( 3712 ) );
            EvalVisitor sut = new EvalVisitor( globalContext );
            var result = sut.Visit( expr );
            Assert.That( result, Is.InstanceOf<IntegerValue>() );
            Assert.That( ((IntegerValue)result).Value, Is.EqualTo( 3712 ) );
        }

        [TestCase( "x;", 3712 )]
        [TestCase( "x+10;", 3712 + 10 )]
        [TestCase( "(x*x)+10;", 3712 * 3712 + 10 )]
        [TestCase( "var a = 3;", 3 )]
        [TestCase( "var a = 3 + x;", 3712 + 3 )]
        [TestCase( "var a = 3 + x; var b = a + 7;", 3712 + 3 + 7 )]
        [TestCase( @"
                        var a = 3 + x;
                        { 
                            var a;
                            a = 4;
                        } 
                        var b = a + 17;",
            3712 + 3 + 17 )]
        [TestCase( @"
                        var a = 3;
                        var collector = 0;
                        while( a )
                        { 
                            a = a - 1;
                            collector = collector + 10;
                        }",
            40 )]
        [TestCase( @"
                        var a = 3;
                        var collector = 0;
                        while( a )
                        { 
                            a = a - 1;
                            collector = collector + 10;
                        } 
                        collector;",
            40 )]
        public void real_eval_tests_with_x_equals_3712( string program, int expectedValue )
        {
            IExpr expr = Parser.ParseProgram( program );
            var globalContext = new Dictionary<string, ValueBase>();
            globalContext.Add( "x", IntegerValue.Create( 3712 ) );
            EvalVisitor sut = new EvalVisitor( globalContext );
            var result = sut.Visit( expr );
            Assert.That( result, Is.InstanceOf<IntegerValue>() );
            Assert.That( ((IntegerValue)result).Value, Is.EqualTo( expectedValue ) );
        }

        [TestCase(
            @"
            var f = function(a) { a + 10; }
            f(3);
            ", 13
            )]
        [TestCase(
            @"
            var X = function(b) 
            { 
                var r;
                var a = 3;
                while( b - 1 )
                {
                    a = a + 10;
                    var a = a + b;
                    b = b - 1;
                    r = a;
                }
                r;    
            }
            X(2);
            ", 24
            )]
        [TestCase(
            @"
            var add10 = function(a) { a + 10; }
            var add15 = function(a) { add10(a) + 5; }
            add10(add15(100));
            ", 125
            )]
        [TestCase(
            @"
            var recurse;
            recurse = function(a) { a ? a + recurse(a-1) : 0; }
            recurse(3);
            ", 6
            )]
        [TestCase(
            @"
            var f1 = function(a) { a + 10; }
            var f2 = function(a,f) { a + f(a); }
            f2(3,f1);
            ", 16
            )]
        [TestCase(
            @"
                var fA = function(p) { function() { p; } };
                var f = fA( 3712 );
                f();
            ", 3712
            )]
        [TestCase(
            @"
                var fA = function(p) { function() { p; } };
                var f1 = fA( 3712 );
                var f2 = fA( -1 );
                f1() + f2();
            ", 3711
            )]


        public void functions_definition_and_call( string program, int expectedValue )
        {
            IExpr expr = Parser.ParseProgram( program );
            EvalVisitor sut = new EvalVisitor();
            var result = sut.Visit( expr );
            Assert.That( result, Is.InstanceOf<IntegerValue>() );
            Assert.That( ((IntegerValue)result).Value, Is.EqualTo( expectedValue ) );
        }


    }
}
