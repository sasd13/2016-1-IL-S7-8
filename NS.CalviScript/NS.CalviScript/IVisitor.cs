namespace NS.CalviScript
{
    public interface IVisitor
    {
        void Visit( BinaryExpr expr );

        void Visit( ConstantExpr expr );

        void Visit( ErrorExpr expr );
    }

    public static class IVisitorExtensions
    {
        public static void Visit( this IVisitor @this, IExpr expr )
        {
            expr.Accept( @this );
        }
    }
}
