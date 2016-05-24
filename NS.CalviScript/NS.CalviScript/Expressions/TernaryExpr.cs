namespace NS.CalviScript
{
    public class TernaryExpr
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
    }
}
