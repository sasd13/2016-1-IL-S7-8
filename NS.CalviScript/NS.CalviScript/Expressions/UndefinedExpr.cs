namespace NS.CalviScript
{
    public class UndefinedExpr : IExpr
    {
        public static UndefinedExpr Default = new UndefinedExpr();

        UndefinedExpr()
        {
        }

        public T Accept<T>( IVisitor<T> visitor ) => visitor.Visit( this );

    }
}
