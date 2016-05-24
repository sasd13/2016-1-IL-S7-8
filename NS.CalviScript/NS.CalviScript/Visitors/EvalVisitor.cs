using System;

namespace NS.CalviScript
{
    public class EvalVisitor : IVisitor<int>
    {
        public int Visit( ErrorExpr expr )
        {
            throw new InvalidOperationException( expr.Message );
        }

        public int Visit( TernaryExpr expr )
        {
            throw new NotImplementedException();
        }

        public int Visit( UnaryExpr expr )
        {
            return TokenTypeHelpers.Compute( expr.Expr.Accept( this ), expr.Type );
        }

        public int Visit( ConstantExpr expr )
        {
            return expr.Value;
        }

        public int Visit( BinaryExpr expr )
        {
            return TokenTypeHelpers.Compute(
                expr.LeftExpr.Accept( this ),
                expr.RightExpr.Accept( this ),
                expr.Type );
        }
    }
}
