namespace NS.CalviScript
{
    public interface IVisitor<T>
    {
        T Visit( BinaryExpr expr );

        T Visit( ConstantExpr expr );

        T Visit( ErrorExpr expr );

        T Visit( UnaryExpr expr );

        T Visit( TernaryExpr expr );

        T Visit( LookUpExpr expr );

        T Visit( VarDeclExpr expr );

        T Visit( ProgramExpr expr );
    }

    public static class IVisitorExtensions
    {
        public static T Visit<T>(this IVisitor<T> @this, IExpr expr )
        {
            return expr.Accept( @this );
        }
    }
}
