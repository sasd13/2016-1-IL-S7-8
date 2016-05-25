namespace NS.CalviScript
{
    public class ErrorExpr : IExpr
    {
        public ErrorExpr( string message )
        {
            Message = message;
        }

        public string Message { get; }

        public T Accept<T>( IVisitor<T> visitor )
        {
            return visitor.Visit( this );
        }
    }
}
