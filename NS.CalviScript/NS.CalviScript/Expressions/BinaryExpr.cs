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

        public T Accept<T>( IVisitor<T> visitor )
        {
            return visitor.Visit( this );
        }
    }
}
