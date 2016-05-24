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

        public IExpr ParseProgram()
        {
            throw new System.NotImplementedException();
        }

        IExpr Statement()
        {
            throw new System.NotImplementedException();
        }

        IExpr VarDecl()
        {
            throw new System.NotImplementedException();
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
            IExpr expr = MathExpr();
            if( _tokenizer.MatchToken( TokenType.QuestionMark ) )
            {
                IExpr trueExpr = Expr();
                if( !_tokenizer.MatchToken( TokenType.Colon ) )
                {
                    return new ErrorExpr(
                        string.Format(
                            "Expected <:>, but <{0}> found.",
                            _tokenizer.CurrentToken ) );
                }
                IExpr falseExpr = Expr();
                expr = new TernaryExpr( expr, trueExpr, falseExpr );
            }

            return expr;
        }

        IExpr MathExpr()
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
            bool isMinusExpr = _tokenizer.MatchToken( TokenType.Minus );
            IExpr expr = PositiveFactor();
            if( isMinusExpr ) expr = new UnaryExpr( TokenType.Minus, expr );

            return expr;
        }

        IExpr PositiveFactor()
        {
            Token token;
            if( _tokenizer.MatchNumber( out token ) )
            {
                return new ConstantExpr( int.Parse( token.Value ) );
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
            if( _tokenizer.MatchToken( TokenType.Identifier, out token ) )
            {
                return new LookUpExpr( token.Value );
            }

            return new ErrorExpr(
                string.Format(
                    "Unexpected token: {0}.",
                    token.Type ) );
        }

        public static IExpr Parse( string input )
        {
            Tokenizer tokenizer = new Tokenizer( input );
            Parser parser = new Parser( tokenizer );
            return parser.ParseExpression();
        }
    }
}
