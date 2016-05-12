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
            char current;
            while( char.IsWhiteSpace( current = _input[ _pos ] ) ) _pos++;
            Token result;
            if( current == '+' ) result = new Token( TokenType.Plus );
            else if( current == '(' ) result = new Token( TokenType.LeftParenthesis );
            else if( current == ')' ) result = new Token( TokenType.RightParenthesis );
            else if( char.IsDigit( current ) ) return GetNextNumberToken();
            else result = new Token( TokenType.None );
            _pos++;
            return result;
        }

        Token GetNextNumberToken()
        {
            Debug.Assert( char.IsDigit( _input[ _pos ] ) );

            StringBuilder sb = new StringBuilder();
            do
            {
                sb.Append( _input[ _pos ] );
                _pos++;
            } while( char.IsDigit( _input[ _pos ] ) );
            return new Token( TokenType.Number, sb.ToString() );
        }
    }
}
