namespace NS.CalviScript
{
    public interface IVisitor
    {
        void Visit( BinaryExpr expr );

        void Visit( ConstantExpr expr );

        void Visit( ErrorExpr expr );

        void Visit( UnaryExpr expr );
    }

    public interface IVisitor<T>
    {
        T Visit( BinaryExpr expr );

        T Visit( ConstantExpr expr );

        T Visit( ErrorExpr expr );

        T Visit( UnaryExpr expr );
    }

    public static class IVisitorExtensions
    {
        public static void Visit( this IVisitor @this, IExpr expr )
        {
            expr.Accept( @this );
        }

        public static T Visit<T>(this IVisitor<T> @this, IExpr expr )
        {
            return expr.Accept( @this );
        }
    }
}
