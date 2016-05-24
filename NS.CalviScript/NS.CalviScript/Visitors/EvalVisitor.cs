using System;

namespace NS.CalviScript
{
    public class EvalVisitor : IVisitor
    {
        public void Visit( ErrorExpr expr )
        {
            throw new InvalidOperationException( expr.Message );
        }

        public void Visit( ConstantExpr expr )
        {
            Result = expr.Value;
        }

        public void Visit( BinaryExpr expr )
        {
            expr.LeftExpr.Accept( this );
            int left = Result;
            expr.RightExpr.Accept( this );
            int right = Result;
            Result = TokenTypeHelpers.Compute( left, right, expr.Type );
        }

        public void Visit( UnaryExpr expr )
        {
            expr.Expr.Accept( this );
            int value = Result;
            Result = TokenTypeHelpers.Compute( value, expr.Type );
        }

        public int Result { get; private set; }
    }

    public class GenericEvalVisitor : IVisitor<int>
    {
        public int Visit( ErrorExpr expr )
        {
            throw new InvalidOperationException( expr.Message );
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
