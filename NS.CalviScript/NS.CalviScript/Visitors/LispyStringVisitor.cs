using System;

namespace NS.CalviScript
{
    public class LispyStringVisitor : IVisitor<string>
    {
        public string Visit( ErrorExpr expr )
        {
            return string.Format( "[Error {0}]", expr.Message );
        }

        public string Visit( TernaryExpr expr )
        {
            return string.Format( "[? {0} {1} {2}]",
                expr.PredicateExpr.Accept( this ),
                expr.TrueExpr.Accept( this ),
                expr.FalseExpr.Accept( this ) );
        }

        public string Visit( UnaryExpr expr )
        {
            return string.Format( "[{0} {1}]",
                TokenTypeHelpers.TokenTypeToString( expr.Type ),
                expr.Expr.Accept( this ) );
        }

        public string Visit( ConstantExpr expr )
        {
            return expr.Value.ToString();
        }

        public string Visit( BinaryExpr expr )
        {
            return string.Format( "[{0} {1} {2}]",
                TokenTypeHelpers.TokenTypeToString( expr.Type ),
                expr.LeftExpr.Accept( this ),
                expr.RightExpr.Accept( this ) );
        }
    }
}
