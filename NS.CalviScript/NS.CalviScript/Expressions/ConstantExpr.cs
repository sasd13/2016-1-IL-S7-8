namespace NS.CalviScript
{
    public class ConstantExpr : IExpr
    {
        public ConstantExpr( int value )
        {
            Value = value;
        }

        public int Value { get; }

        public T Accept<T>( IVisitor<T> visitor )
        {
            return visitor.Visit( this );
        }
    }
}
