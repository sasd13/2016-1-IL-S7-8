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
            if (IsEnd) return new Token(TokenType.End);

            while (IsWhiteSpace || IsComment)
            {
                if (IsWhiteSpace) HandleWhiteSpace();
                if (IsComment) HandleComment();
            }

            Token result;
            if (Peek() == '+') result = new Token(TokenType.Plus);
            else if (Peek() == '-') result = new Token(TokenType.Minus);
            else if (Peek() == '*') result = new Token(TokenType.Mult);
            else if (Peek() == '/') result = new Token(TokenType.Div);
            else if (Peek() == '%') result = new Token(TokenType.Modulo);
            else if (Peek() == '(') result = new Token(TokenType.LeftParenthesis);
            else if (Peek() == ')') result = new Token(TokenType.RightParenthesis);
            else if (IsNumber) result = HandleNumber();
            else result = new Token(TokenType.None);

            return result;
        }

        char Read() => _input[ _pos++ ];

        char Peek() => Peek( 0 );

        void Forward() => _pos++;

        char Peek(int offset) => _input[_pos + offset];

        public bool IsEnd => _pos >= _input.Length;

        bool IsComment => _pos < _input.Length - 1 && Peek() == '/' && Peek(1) == '/';

        void HandleComment()
        {
            Debug.Assert(IsComment);

            do
            {
                Forward();
            } while ( !IsEnd && Peek() != '\r' && Peek() != '\n' );
        }

        bool IsWhiteSpace => char.IsWhiteSpace(Peek());

        void HandleWhiteSpace()
        {
            Debug.Assert(IsWhiteSpace);

            do
            {
                Forward();
            } while ( !IsEnd && IsWhiteSpace );
        }

        bool IsNumber => char.IsDigit(Peek()) && Peek() != '0';

        Token HandleNumber()
        {
            Debug.Assert(IsNumber);

            StringBuilder sb = new StringBuilder();
            do
            {
                sb.Append(Peek());
                Forward();
            } while (!IsEnd && char.IsDigit(Peek()));

            return new Token(TokenType.Number, sb.ToString());
        }
    }
}
