using System.Diagnostics;

namespace NS.CalviScript
{
    public class WhileExpr : IExpr
    {
        public WhileExpr( IExpr condition, BlockExpr body )
        {
            Condition = condition;
            Body = body;
        }

        public IExpr Condition { get; }

        public BlockExpr Body { get; }

        public T Accept<T>( IVisitor<T> visitor ) => visitor.Visit( this );

    }
}
