using NUnit.Framework;

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
            Tokenizer sut = new Tokenizer( "(5 + 70) * 5 / 3 % 7 - 6 ? 50 : 3" );

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
            Token t14 = sut.GetNextToken();
            Token t15 = sut.GetNextToken();
            Token t16 = sut.GetNextToken();
            Token t17 = sut.GetNextToken();

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
            Assert.That( t14.Type, Is.EqualTo( TokenType.QuestionMark ) );
            Assert.That( t15.Type, Is.EqualTo( TokenType.Number ) );
            Assert.That( t15.Value, Is.EqualTo( "50" ) );
            Assert.That( t16.Type, Is.EqualTo( TokenType.Colon ) );
            Assert.That( t17.Type, Is.EqualTo( TokenType.Number ) );
            Assert.That( t17.Value, Is.EqualTo( "3" ) );
        }

        [Test]
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

        [Test]
        public void handle_0()
        {
            Tokenizer sut = new Tokenizer( "0 + 0" );

            Token t1 = sut.GetNextToken();
            Token t2 = sut.GetNextToken();
            Token t3 = sut.GetNextToken();
            Token t4 = sut.GetNextToken();

            Assert.That( t1.Type, Is.EqualTo( TokenType.Number ) );
            Assert.That( t1.Value, Is.EqualTo( "0" ) );
            Assert.That( t2.Type, Is.EqualTo( TokenType.Plus ) );
            Assert.That( t3.Type, Is.EqualTo( TokenType.Number ) );
            Assert.That( t3.Value, Is.EqualTo( "0" ) );
            Assert.That( t4.Type, Is.EqualTo( TokenType.End ) );
        }

        [Test]
        public void handle_numbers_beginning_with_0()
        {
            Tokenizer sut = new Tokenizer( "0 + 0123" );

            Token t1 = sut.GetNextToken();
            Token t2 = sut.GetNextToken();
            Token t3 = sut.GetNextToken();
            Token t4 = sut.GetNextToken();
            Token t5 = sut.GetNextToken();

            Assert.That( t1.Type, Is.EqualTo( TokenType.Number ) );
            Assert.That( t1.Value, Is.EqualTo( "0" ) );
            Assert.That( t2.Type, Is.EqualTo( TokenType.Plus ) );
            Assert.That( t3.Type, Is.EqualTo( TokenType.Error ) );
            Assert.That( t4.Type, Is.EqualTo( TokenType.Number ) );
            Assert.That( t4.Value, Is.EqualTo( "123" ) );
            Assert.That( t5.Type, Is.EqualTo( TokenType.End ) );
        }

        [Test]
        public void handle_unexpected_character()
        {
            Tokenizer sut = new Tokenizer( "25 @ 27" );

            Token t1 = sut.GetNextToken();
            Token t2 = sut.GetNextToken();
            Token t3 = sut.GetNextToken();
            Token t4 = sut.GetNextToken();

            Assert.That( t1.Type, Is.EqualTo( TokenType.Number ) );
            Assert.That( t1.Value, Is.EqualTo( "25" ) );
            Assert.That( t2.Type, Is.EqualTo( TokenType.Error ) );
            Assert.That( t3.Type, Is.EqualTo( TokenType.Number ) );
            Assert.That( t3.Value, Is.EqualTo( "27" ) );
            Assert.That( t4.Type, Is.EqualTo( TokenType.End ) );
        }

        [Test]
        public void handle_identifiers()
        {
            Tokenizer sut = new Tokenizer( "var _1_identifier = 12 * 3;" );

            Token t1 = sut.GetNextToken();
            Token t2 = sut.GetNextToken();
            Token t3 = sut.GetNextToken();
            Token t4 = sut.GetNextToken();
            Token t5 = sut.GetNextToken();
            Token t6 = sut.GetNextToken();
            Token t7 = sut.GetNextToken();
            Token t8 = sut.GetNextToken();

            Assert.That( t1.Type, Is.EqualTo( TokenType.Var ) );
            Assert.That( t2.Type, Is.EqualTo( TokenType.Identifier ) );
            Assert.That( t2.Value, Is.EqualTo( "_1_identifier" ) );
            Assert.That( t3.Type, Is.EqualTo( TokenType.Equal ) );
            Assert.That( t4.Type, Is.EqualTo( TokenType.Number ) );
            Assert.That( t5.Type, Is.EqualTo( TokenType.Mult ) );
            Assert.That( t6.Type, Is.EqualTo( TokenType.Number ) );
            Assert.That( t7.Type, Is.EqualTo( TokenType.SemiColon ) );
            Assert.That( t8.Type, Is.EqualTo( TokenType.End ) );
        }
    }
}
