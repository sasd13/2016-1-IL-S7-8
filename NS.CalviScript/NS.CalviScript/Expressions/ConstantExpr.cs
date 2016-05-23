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

        public string ToInfixString() => Value.ToString();

        public string ToLispyString() => Value.ToString();
    }
}
