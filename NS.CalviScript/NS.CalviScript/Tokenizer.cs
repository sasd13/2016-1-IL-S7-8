using System;
using System.Diagnostics;
using System.Text;

namespace NS.CalviScript
{
    public class Tokenizer
    {
        readonly string _input;
        int _pos;

        public Tokenizer( string input )
        {
            _input = input;
        }

        public Token GetNextToken()
        {
            if( IsEnd ) return new Token( TokenType.End );

            while( IsWhiteSpace || IsComment )
            {
                if( IsWhiteSpace ) HandleWhiteSpaces();
                if( IsComment ) HandleComment();
            }

            Token result;
            if( Peek() == '+' ) result = HandleSimpleToken( TokenType.Plus );
            else if( Peek() == '-' ) result = HandleSimpleToken( TokenType.Minus );
            else if( Peek() == '*' ) result = HandleSimpleToken( TokenType.Mult );
            else if( Peek() == '/' ) result = HandleSimpleToken( TokenType.Div );
            else if( Peek() == '%' ) result = HandleSimpleToken( TokenType.Modulo );
            else if( Peek() == '(' ) result = HandleSimpleToken( TokenType.LeftParenthesis );
            else if( Peek() == ')' ) result = HandleSimpleToken( TokenType.RightParenthesis );
            else if( IsNumber ) result = HandleNumber();
            else result = new Token( TokenType.Error, Read() );

            CurrentToken = result;
            return result;
        }

        public Token CurrentToken { get; private set; }

        Token HandleSimpleToken( TokenType type )
        {
            char c = Peek();
            Forward();
            return new Token( type, c );
        }

        public bool MatchNumber( out Token token )
        {
            return MatchToken( TokenType.Number, out token );
        }

        public bool MatchOperator( out Token token )
        {
            throw new NotImplementedException();
        }

        public bool MatchToken( TokenType type, out Token token )
        {
            throw new NotImplementedException();
        }

        char Read() => _input[ _pos++ ];

        char Peek() => Peek( 0 );

        void Forward() => _pos++;

        char Peek( int offset ) => _input[ _pos + offset ];

        public bool IsEnd => _pos >= _input.Length;

        bool IsComment => _pos < _input.Length - 1 && Peek() == '/' && Peek( 1 ) == '/';

        void HandleComment()
        {
            Debug.Assert( IsComment );

            do
            {
                Forward();
            } while( !IsEnd && Peek() != '\r' && Peek() != '\n' );
        }

        bool IsWhiteSpace => char.IsWhiteSpace( Peek() );

        void HandleWhiteSpaces()
        {
            Debug.Assert( IsWhiteSpace );

            do
            {
                Forward();
            } while( !IsEnd && IsWhiteSpace );
        }

        bool IsNumber => char.IsDigit( Peek() );

        Token HandleNumber()
        {
            Debug.Assert( IsNumber );

            if( Peek() == '0' )
            {
                Forward();
                if( !IsEnd && IsNumber ) return new Token( TokenType.Error, Peek() );
                return new Token( TokenType.Number, '0' );
            }

            StringBuilder sb = new StringBuilder();
            do
            {
                sb.Append( Peek() );
                Forward();
            } while( !IsEnd && IsNumber );

            return new Token( TokenType.Number, sb.ToString() );
        }
    }
}
