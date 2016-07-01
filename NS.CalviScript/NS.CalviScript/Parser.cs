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

        IExpr Block( bool expected, bool openScope = true )
        {
            if( !_tokenizer.MatchToken( TokenType.OpenCurly ) )
            {
                return expected ? new ErrorExpr( "Expected Block." ) : null;
            }
            List<IExpr> statements = new List<IExpr>();
            using( openScope ? _synScope.OpenScope() : null )
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
                        ?? While( false )
                        ?? Expr();
            if( r == null )
            {
                return new ErrorExpr( "Expected statement." );
            }
            _tokenizer.MatchToken( TokenType.SemiColon );
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
            var funDecl = FunDecl( expected: false );
            if( funDecl != null ) return funDecl;

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
                string identifierName = token.Value;
                if( _tokenizer.MatchToken(TokenType.LeftParenthesis ) )
                {
                    var parameters = new List<IExpr>();
                    while( !_tokenizer.MatchToken( TokenType.RightParenthesis ) )
                    {
                        IExpr e = Expr();
                        if( e == null || e is ErrorExpr ) return e;
                        parameters.Add( e );
                        _tokenizer.MatchToken( TokenType.Comma );
                    }
                    return new FunCallExpr( _synScope.Lookup( identifierName ), parameters );
                }
                return MayBeAssigned( _synScope.Lookup( identifierName ) );
            }

            return new ErrorExpr(
                string.Format(
                    "Unexpected token: {0}.",
                    _tokenizer.CurrentToken.Value ) );
        }

        IExpr FunDecl( bool expected )
        {
            if( !_tokenizer.MatchToken( TokenType.Function ) )
            {
                return expected ? CreateErrorExpr( "function" ) : null;
            }
            if( !_tokenizer.MatchToken( TokenType.LeftParenthesis ) )
                return CreateErrorExpr( "(" );

            using( _synScope.OpenScope() )
            {
                var parameters = new List<VarDeclExpr>();
                while( _tokenizer.CurrentToken.Type == TokenType.Identifier )
                {
                    string identifierName = _tokenizer.CurrentToken.Value;
                    IExpr declOrError = _synScope.Declare( identifierName );
                    if( declOrError is ErrorExpr ) return declOrError;
                    parameters.Add( (VarDeclExpr)declOrError );

                    if( _tokenizer.GetNextToken().Type != TokenType.Comma ) break;
                    _tokenizer.GetNextToken();
                }
                if( !_tokenizer.MatchToken( TokenType.RightParenthesis ) )
                    return CreateErrorExpr( ")" );

                BlockExpr body = Block( expected: true, openScope: false ) as BlockExpr;
                if( body == null ) return null;

                return new FunDeclExpr( parameters, body );
            }
        }


        IExpr While( bool expected )
        {
            if( !_tokenizer.MatchToken( TokenType.While ) )
            {
                return expected ? CreateErrorExpr( "while" ) : null; 
            }
            if( !_tokenizer.MatchToken( TokenType.LeftParenthesis ) )
                return CreateErrorExpr( "(" );
            IExpr condition = Expr();
            if( !_tokenizer.MatchToken( TokenType.RightParenthesis ) )
                return CreateErrorExpr( ")" );
            IExpr body = Block( true );
            if( body == null || body is ErrorExpr) return body;
            return new WhileExpr( condition, (BlockExpr)body );
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
