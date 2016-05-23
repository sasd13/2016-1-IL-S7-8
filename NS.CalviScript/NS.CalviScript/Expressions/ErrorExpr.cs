namespace NS.CalviScript
{
    public class ErrorExpr : IExpr
    {
        public ErrorExpr( string message )
        {
            Message = message;
        }

        public string Message { get; }

        public void Accept( IVisitor visitor )
        {
            visitor.Visit( this );
        }

        public string ToInfixString() => ToLispyString();

        public string ToLispyString() => string.Format( "[Error {0}]", Message );
    }
}
