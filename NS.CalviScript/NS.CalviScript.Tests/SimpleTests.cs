﻿using NUnit.Framework;

namespace NS.CalviScript.Tests
{
    [TestFixture]
    public class TokenizerTests
    {
        [TestCase( "+", TokenType.Plus )]
        [TestCase( "(", TokenType.LeftParenthesis )]
        public void parse_string_containing_1_token( string input, TokenType expected )
        {
            Tokenizer sut = new Tokenizer( input );
            Token token = sut.GetNextToken();
            Assert.That( token.Type, Is.EqualTo( expected ) );
        }

        [Test]
        public void parse_4_tokens()
        {
            Tokenizer sut = new Tokenizer( "+())" );
            Token t1 = sut.GetNextToken();
            Token t2 = sut.GetNextToken();
            Token t3 = sut.GetNextToken();
            Token t4 = sut.GetNextToken();

            Assert.That( t1.Type, Is.EqualTo( TokenType.Plus ) );
            Assert.That( t2.Type, Is.EqualTo( TokenType.LeftParenthesis ) );
            Assert.That( t3.Type, Is.EqualTo( TokenType.RightParenthesis ) );
            Assert.That( t4.Type, Is.EqualTo( TokenType.RightParenthesis ) );
        }

        [Test]
        public void parse_string_with_whitespace()
        {
            Tokenizer sut = new Tokenizer( "+\t (\r\n  ) " );

            Token t1 = sut.GetNextToken();
            Token t2 = sut.GetNextToken();
            Token t3 = sut.GetNextToken();

            Assert.That( t1.Type, Is.EqualTo( TokenType.Plus ) );
            Assert.That( t2.Type, Is.EqualTo( TokenType.LeftParenthesis ) );
            Assert.That( t3.Type, Is.EqualTo( TokenType.RightParenthesis ) );
        }

        [Test]
        public void parse_numbers()
        {
            Tokenizer sut = new Tokenizer( "(10 + 59) + 12 )" );

            Token t1 = sut.GetNextToken();
            Token t2 = sut.GetNextToken();
            Token t3 = sut.GetNextToken();
            Token t4 = sut.GetNextToken();
            Token t5 = sut.GetNextToken();
            Token t6 = sut.GetNextToken();
            Token t7 = sut.GetNextToken();
            Token t8 = sut.GetNextToken();

            Assert.That( t1.Type, Is.EqualTo( TokenType.LeftParenthesis ) );
            Assert.That( t2.Type, Is.EqualTo( TokenType.Number ) );
            Assert.That( t2.Value, Is.EqualTo( "10" ) );
            Assert.That( t3.Type, Is.EqualTo( TokenType.Plus ) );
            Assert.That( t4.Type, Is.EqualTo( TokenType.Number ) );
            Assert.That( t4.Value, Is.EqualTo( "59" ) );
            Assert.That( t5.Type, Is.EqualTo( TokenType.RightParenthesis ) );
            Assert.That( t6.Type, Is.EqualTo( TokenType.Plus ) );
            Assert.That( t7.Type, Is.EqualTo( TokenType.Number ) );
            Assert.That( t7.Value, Is.EqualTo( "12" ) );
            Assert.That( t8.Type, Is.EqualTo( TokenType.RightParenthesis ) );
        }

        [Test]
        public void parse_all_tokens()
        {
            Tokenizer sut = new Tokenizer( "(5 + 70) * 5 / 3 % 7 - 6" );

            Token t1 = sut.GetNextToken();
            Token t2 = sut.GetNextToken();
            Token t3 = sut.GetNextToken();
            Token t4 = sut.GetNextToken();
            Token t5 = sut.GetNextToken();
            Token t6 = sut.GetNextToken();
            Token t7 = sut.GetNextToken();
            Token t8 = sut.GetNextToken();
            Token t9 = sut.GetNextToken();
            Token t10 = sut.GetNextToken();
            Token t11 = sut.GetNextToken();
            Token t12 = sut.GetNextToken();
            Token t13 = sut.GetNextToken();

            Assert.That( t1.Type, Is.EqualTo( TokenType.LeftParenthesis ) );
            Assert.That( t2.Type, Is.EqualTo( TokenType.Number ) );
            Assert.That( t2.Value, Is.EqualTo( "5" ) );
            Assert.That( t3.Type, Is.EqualTo( TokenType.Plus ) );
            Assert.That( t4.Type, Is.EqualTo( TokenType.Number ) );
            Assert.That( t4.Value, Is.EqualTo( "70" ) );
            Assert.That( t5.Type, Is.EqualTo( TokenType.RightParenthesis ) );
            Assert.That( t6.Type, Is.EqualTo( TokenType.Mult ) );
            Assert.That( t7.Type, Is.EqualTo( TokenType.Number ) );
            Assert.That( t7.Value, Is.EqualTo( "5" ) );
            Assert.That( t8.Type, Is.EqualTo( TokenType.Div ) );
            Assert.That( t9.Type, Is.EqualTo( TokenType.Number ) );
            Assert.That( t9.Value, Is.EqualTo( "3" ) );
            Assert.That( t10.Type, Is.EqualTo( TokenType.Modulo ) );
            Assert.That( t11.Type, Is.EqualTo( TokenType.Number ) );
            Assert.That( t11.Value, Is.EqualTo( "7" ) );
            Assert.That( t12.Type, Is.EqualTo( TokenType.Minus ) );
            Assert.That( t13.Type, Is.EqualTo( TokenType.Number ) );
            Assert.That( t13.Value, Is.EqualTo( "6" ) );
        }

        public void handle_comments()
        {
            Tokenizer sut = new Tokenizer( @"2 + // Comment
5" );
            Token t1 = sut.GetNextToken();
            Token t2 = sut.GetNextToken();
            Token t3 = sut.GetNextToken();
            Token t4 = sut.GetNextToken();

            Assert.That( t1.Type, Is.EqualTo( TokenType.Number ) );
            Assert.That( t2.Type, Is.EqualTo( TokenType.Plus ) );
            Assert.That( t3.Type, Is.EqualTo( TokenType.Number ) );
            Assert.That( t4.Type, Is.EqualTo( TokenType.End ) );
        }
    }
}
