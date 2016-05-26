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
            if( IsEnd ) return CurrentToken = new Token( TokenType.End );

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
            else if( Peek() == '?' ) result = HandleSimpleToken( TokenType.QuestionMark );
            else if( Peek() == ':' ) result = HandleSimpleToken( TokenType.Colon );
            else if( Peek() == '=' ) result = HandleSimpleToken( TokenType.Equal );
            else if( Peek() == ';' ) result = HandleSimpleToken( TokenType.SemiColon );
            else if( Peek() == '{' ) result = HandleSimpleToken( TokenType.OpenCurly );
            else if( Peek() == '}' ) result = HandleSimpleToken( TokenType.CloseCurly );
            else if( IsNumber ) result = HandleNumber();
            else if( IsIdentifier ) result = HandleIdentifier();
            else result = new Token( TokenType.Error, Read() );

            CurrentToken = result;
            return result;
        }

        public bool MatchTermOp( out Token token )
        {
            return MatchOp( t => t == TokenType.Plus || t == TokenType.Minus, out token );
        }

        public bool MatchFactorOp( out Token token )
        {
            return MatchOp( t => t == TokenType.Mult || t == TokenType.Div || t == TokenType.Modulo, out token );
        }

        bool MatchOp( Func<TokenType, bool> predicate, out Token token )
        {
            if( predicate( CurrentToken.Type ) )
            {
                token = CurrentToken;
                GetNextToken();
                return true;
            }

            token = null;
            return false;
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

        public bool MatchToken( TokenType type )
        {
            Token t;
            return MatchToken( type, out t );
        }

        public bool MatchToken( TokenType type, out Token token )
        {
            if( type == CurrentToken.Type )
            {
                token = CurrentToken;
                GetNextToken();
                return true;
            }

            token = null;
            return false;
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

        bool IsIdentifier => char.IsLetter( Peek() ) || Peek() == '_';

        Token HandleIdentifier()
        {
            Debug.Assert( IsIdentifier );

            StringBuilder sb = new StringBuilder();
            do
            {
                sb.Append( Peek() );
                Forward();
            } while( !IsEnd && ( IsIdentifier || char.IsDigit( Peek() ) ) );

            string identifier = sb.ToString();
            if( identifier == "var" ) return new Token( TokenType.Var, identifier );
            return new Token( TokenType.Identifier, identifier );
        }
    }
}
