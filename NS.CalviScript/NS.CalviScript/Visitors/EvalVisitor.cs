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
            return expr.PredicateExpr.Accept( this ) >= 0
                ? expr.TrueExpr.Accept( this )
                : expr.FalseExpr.Accept( this );
        }

        public int Visit( VarDeclExpr expr )
        {
            throw new NotImplementedException();
        }

        public int Visit( BlockExpr expr )
        {
            throw new NotImplementedException();
        }

        public int Visit( LookUpExpr expr )
        {
            throw new NotImplementedException();
        }

        public int Visit( UnaryExpr expr )
        {
            return TokenTypeHelpers.Compute( expr.Expr.Accept( this ), expr.Type );
        }

        public int Visit( AssignExpr expr )
        {
            throw new NotImplementedException();
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
