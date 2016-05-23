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

        public IExpr ParseExpression()
        {
            IExpr expr = Expr();
            Token token;
            if( !_tokenizer.MatchToken( TokenType.End, out token ) )
            {
                expr = new ErrorExpr(
                    string.Format(
                        "Expected end of input but {0} found.",
                        token.Type ) );
            }

            return expr;
        }

        IExpr Expr()
        {
            IExpr leftTerm = Term();
            Token token;
            while( _tokenizer.MatchTermOp( out token ) )
            {
                IExpr rightTerm = Term();
                leftTerm = new BinaryExpr( token.Type, leftTerm, rightTerm );
            }

            return leftTerm;
        }

        IExpr Term()
        {
            IExpr leftFactor = Factor();
            Token token;
            while( _tokenizer.MatchFactorOp( out token ) )
            {
                IExpr rightTerm = Factor();
                leftFactor = new BinaryExpr( token.Type, leftFactor, rightTerm );
            }

            return leftFactor;
        }

        IExpr Factor()
        {
            Token token;
            if( _tokenizer.MatchNumber( out token ) )
            {
                return new NumberExpr( int.Parse( token.Value ) );
            }
            if( _tokenizer.MatchToken( TokenType.LeftParenthesis ) )
            {
                IExpr expr = Expr();
                if( !_tokenizer.MatchToken( TokenType.RightParenthesis, out token ) )
                {
                    return new ErrorExpr(
                        string.Format(
                            "Expected right parenthesis, {0} found.",
                            token.Type ) );
                }

                return expr;
            }

            return new ErrorExpr(
                string.Format(
                    "Unexpected token: {0}.",
                    token.Type ) );
        }
    }
}
