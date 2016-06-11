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
            Token result = null;
            while (!IsEnd() &&( IsWhiteSpace || IsComment || IsMultiLineComment ))
            {
                if( IsWhiteSpace ) HandleWhiteSpaces();
                if( IsComment ) HandleComment();
                if (IsMultiLineComment) result = HandleMultiLineComment();
                if (result != null) return CurrentToken = result;
            }

            if (IsEnd()) return CurrentToken = new Token(TokenType.End);

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

        /// <summary>
        /// Moves forward two times.
        /// </summary>
        void SkipMultilineComment()
        {
            Forward();
            Forward();
        }

        char Peek( int offset ) => _input[ _pos + offset ];

        /// <summary>
        /// Checks if the current position has reached the End Of Input (EOI).
        /// </summary>
        /// <returns>True if the current position is greater than or equal to the input length.</returns>
        public bool IsEnd() => IsEnd(0);

        /// <summary>
        /// Checks if the current position plus an offset has reached the End Of Input (EOI).
        /// </summary>
        /// <param name="offset">True if the current position plus the offset is greater than or equal to the input length.</param>
        /// <returns></returns>
        public bool IsEnd(int offset) => _pos + offset >= _input.Length;

        bool IsComment => _pos < _input.Length - 1 && Peek() == '/' && Peek( 1 ) == '/';

        /// <summary>
        /// Checks if there is a multi line comment ahead in the input.
        /// </summary>
        bool IsMultiLineComment => !IsEnd(1) && Peek() == '/' && Peek(1) == '*';

        void HandleComment()
        {
            Debug.Assert( IsComment );

            do
            {
                Forward();
            } while( !IsEnd() && Peek() != '\r' && Peek() != '\n' );
        }

        /// <summary>
        /// Moves forward until the closing muliline comment is found.
        /// Supports nested multiline comments with recursive calls.
        /// Multilines comments must be closed before the end of file.
        /// </summary>
        /// <returns>A new <see cref="Token"/> of type <see cref="TokenType.Error"/>
        /// if the EOI is reached before the last closing comment.</returns>
        Token HandleMultiLineComment()
        {
            Debug.Assert(IsMultiLineComment);

            Token error = null;
            SkipMultilineComment();
            while (!IsEnd(1) && !(Peek() == '*' && Peek(1) == '/'))
            {
                if (IsMultiLineComment)
                {
                    if ((error = HandleMultiLineComment()) != null)
                        return error;
                }
                else
                    Forward();
            }

            if (IsEnd(1))
                error = new Token(TokenType.Error, "Expected <*/>, but <EOI> found.");
            if (error == null)
                SkipMultilineComment();

            return error;
        }

        bool IsWhiteSpace => char.IsWhiteSpace( Peek() );

        void HandleWhiteSpaces()
        {
            Debug.Assert( IsWhiteSpace );

            do
            {
                Forward();
            } while( !IsEnd() && IsWhiteSpace );
        }

        bool IsNumber => char.IsDigit( Peek() );

        /// <summary>
        /// Create a new <see cref="Token"/> and moves forward.
        /// The number should be a zero only, or start with a digit from 1 to 9 followed by any number of digit from 0 to 9.
        /// A number must not be immediately followed by an identifer.
        /// </summary>
        /// <returns>A new <see cref="Token"/>.</returns>
        Token HandleNumber()
        {
            Debug.Assert( IsNumber );

            if( Peek() == '0' )
            {
                Forward();
                if( !IsEnd() && (IsNumber || IsIdentifier) ) return new Token( TokenType.Error, "0" + Peek() );
                return new Token( TokenType.Number, '0' );
            }

            StringBuilder sb = new StringBuilder();
            do
            {
                sb.Append( Peek() );
                Forward();
            } while( !IsEnd() && IsNumber );

            if (!IsEnd() && IsIdentifier)
            {
                sb.Append(Peek());
                return new Token(TokenType.Error, sb.ToString());
            }

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
            } while( !IsEnd() && ( IsIdentifier || char.IsDigit( Peek() ) ) );

            string identifier = sb.ToString();
            if( identifier == "var" ) return new Token( TokenType.Var, identifier );
            if( identifier == "while" ) return new Token( TokenType.While, identifier );
            return new Token( TokenType.Identifier, identifier );
        }
    }
}
