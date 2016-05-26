using System.Collections.Generic;

namespace NS.CalviScript
{
    public class Parser
    {
        readonly Tokenizer _tokenizer;
        readonly SyntaxicScope _synScope;

        public Parser( Tokenizer tokenizer )
        {
            _tokenizer = tokenizer;
            _synScope = new SyntaxicScope();
            _tokenizer.GetNextToken();
        }

        public IExpr ParseProgram()
        {
            List<IExpr> statements = new List<IExpr>();
            while( !_tokenizer.MatchToken( TokenType.End ) )
            {
                var s = Block( false ) ?? Statement();
                if( s is ErrorExpr ) return s;
                statements.Add( s );
            }
            return statements.Count == 1 && statements[0] is BlockExpr
                    ? (BlockExpr)statements[0]
                    : new BlockExpr( statements );
        }

        IExpr Block( bool expected )
        {
            if( !_tokenizer.MatchToken( TokenType.OpenCurly ) )
            {
                return expected ? new ErrorExpr( "Expected Block." ) : null;
            }
            List<IExpr> statements = new List<IExpr>();
            using( _synScope.OpenScope() )
            {
                while( !_tokenizer.MatchToken( TokenType.CloseCurly ) )
                {
                    var s = Block( false ) ?? Statement();
                    if( s is ErrorExpr ) return s;
                    statements.Add( s );
                }
            }
            return new BlockExpr( statements );
        }

        IExpr Statement()
        {
            IExpr r = VarDecl() 
                        ?? Expr();
            if( r == null )
            {
                return new ErrorExpr( "Expected statement." );
            }
            if( !_tokenizer.MatchToken( TokenType.SemiColon ) )
            {
                return new ErrorExpr( "Expected ; statement terminator." );
            }
            return r;
        }

        IExpr VarDecl()
        {
            if( !_tokenizer.MatchToken( TokenType.Var ) ) return null;
            Token token;
            if( !_tokenizer.MatchToken( TokenType.Identifier, out token ) )
            {
                return CreateErrorExpr( "IDENTIFIER" );
            }

            IExpr eV = _synScope.Declare( token.Value );
            if( eV is ErrorExpr ) return eV;

            return MayBeAssigned( (VarDeclExpr)eV );
        }

        private IExpr MayBeAssigned( IIdentifierExpr v )
        {
            if( !_tokenizer.MatchToken( TokenType.Equal ) ) return v;
            IExpr expr = Expr();
            if( expr == null )
            {
                return CreateErrorExpr( "Expected expression." );
            }
            return new AssignExpr( v, expr );
        }

        public IExpr ParseExpression()
        {
            IExpr expr = Expr();
            Token token;
            if( !_tokenizer.MatchToken( TokenType.End, out token ) )
            {
                expr = CreateErrorExpr( "EOI" );
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
                    return CreateErrorExpr( ":" );
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
                    return CreateErrorExpr( ")" );
                }

                return expr;
            }
            if( _tokenizer.MatchToken( TokenType.Identifier, out token ) )
            {
                return MayBeAssigned( _synScope.Lookup( token.Value ) );
            }

            return new ErrorExpr(
                string.Format(
                    "Unexpected token: {0}.",
                    _tokenizer.CurrentToken.Value ) );
        }

        public static IExpr ParseExpression( string input )
        {
            Tokenizer tokenizer = new Tokenizer( input );
            Parser parser = new Parser( tokenizer );
            return parser.ParseExpression();
        }

        public static IExpr ParseProgram( string input )
        {
            Tokenizer tokenizer = new Tokenizer( input );
            Parser parser = new Parser( tokenizer );
            return parser.ParseProgram();
        }

        ErrorExpr CreateErrorExpr( string expected )
        {
            return new ErrorExpr(
                string.Format(
                    "Expected <{0}>, but <{1}> found.",
                    expected,
                    _tokenizer.CurrentToken.Value ) );
        }
    }
}
