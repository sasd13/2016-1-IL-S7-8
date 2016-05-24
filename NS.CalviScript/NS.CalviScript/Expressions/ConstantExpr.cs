namespace NS.CalviScript
{
    public class ConstantExpr : IExpr
    {
        public ConstantExpr( int value )
        {
            Value = value;
        }

        public int Value { get; }

        public void Accept( IVisitor visitor )
        {
            visitor.Visit( this );
        }

        public T Accept<T>( IVisitor<T> visitor )
        {
            return visitor.Visit( this );
        }

        public string ToInfixString() => Value.ToString();

        public string ToLispyString() => Value.ToString();
    }
}
