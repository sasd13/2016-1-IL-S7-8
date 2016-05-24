using System.Diagnostics;

namespace NS.CalviScript
{
    public class BinaryExpr : IExpr
    {
        public BinaryExpr( TokenType type, IExpr left, IExpr right )
        {
            Type = type;
            LeftExpr = left;
            RightExpr = right;
        }

        public TokenType Type { get; }

        public IExpr LeftExpr { get; }

        public IExpr RightExpr { get; }

        public string ToLispyString()
        {
            return string.Format( "[{0} {1} {2}]",
                TokenTypeHelpers.TokenTypeToString( Type ),
                LeftExpr.ToLispyString(),
                RightExpr.ToLispyString() );
        }

        public string ToInfixString()
        {
            return string.Format( "({0} {1} {2})",
                LeftExpr.ToInfixString(),
                TokenTypeHelpers.TokenTypeToString( Type ),
                RightExpr.ToInfixString() );
        }

        public void Accept( IVisitor visitor )
        {
            visitor.Visit( this );
        }

        public T Accept<T>( IVisitor<T> visitor )
        {
            return visitor.Visit( this );
        }
    }
}
