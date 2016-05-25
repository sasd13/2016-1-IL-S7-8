using System;

namespace NS.CalviScript
{
    public class TernaryExpr : IExpr
    {
        public TernaryExpr( IExpr predicateExpr, IExpr trueExpr, IExpr falseExpr )
        {
            PredicateExpr = predicateExpr;
            TrueExpr = trueExpr;
            FalseExpr = falseExpr;
        }

        public IExpr PredicateExpr { get; }

        public IExpr TrueExpr { get; }

        public IExpr FalseExpr { get; }

        public T Accept<T>( IVisitor<T> visitor )
        {
            return visitor.Visit( this );
        }
    }
}
