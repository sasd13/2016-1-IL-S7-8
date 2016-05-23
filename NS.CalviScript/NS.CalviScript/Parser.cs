using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    public class Parser
    {
        readonly Tokenizer _tokenizer;

        public Parser( Tokenizer tokenizer )
        {
            _tokenizer = tokenizer;
            _tokenizer.GetNextToken();
        }

        public IExpr ParseOperation()
        {
            IExpr operationExpr = Operation();
            Token token;
            if( !_tokenizer.MatchToken( TokenType.End, out token ) )
            {
                return new ErrorExpr(
                    string.Format(
                        "Expected end of input, but {0} found.",
                        _tokenizer.CurrentToken.Type ) );
            }
            return operationExpr;
        }

        public IExpr Operation()
        {
            Operand();
            Token t = _tokenizer.CurrentToken;
            while( t.Type == TokenType.Plus || t.Type == TokenType.Minus || t.Type == TokenType.Mult || t.Type == TokenType.Div || t.Type == TokenType.Modulo )
            {
                _tokenizer.GetNextToken();
                Operand();
                t = _tokenizer.CurrentToken;
            }
        }

        public IExpr Operand()
        {
            IExpr result;
            if( _tokenizer.CurrentToken.Type == TokenType.Number )
            {
                Token t = _tokenizer.CurrentToken;
                result = new NumberExpr( int.Parse( t.Value ) );
                _tokenizer.GetNextToken();
            }
            else
            {
                result = PrioritizedOperation();
            }
            return result;
        }

        public IExpr PrioritizedOperation()
        {
            Token token;
            if( _tokenizer.MatchToken( TokenType.LeftParenthesis, out token ) )
            {
                Operation();
                if( !_tokenizer.MatchToken( TokenType.RightParenthesis, out token ) )
                {
                    throw new Exception( string.Format( "Unexpected token: {0}", _tokenizer.CurrentToken.Type ) );
                }
            }
            else
            {
                throw new Exception( string.Format( "Unexpected token: {0}", _tokenizer.CurrentToken.Type ) );
            }
        }
    }
}
