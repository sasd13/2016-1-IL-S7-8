using System;
using System.Text;

namespace NS.CalviScript
{
    public class LispyStringVisitor : IVisitor<string>
    {
        public string Visit( ErrorExpr expr )
        {
            return string.Format( "[Error {0}]", expr.Message );
        }

        public string Visit( WhileExpr expr )
        {
            return "[while]";
        }

        public string Visit( FunDeclExpr expr )
        {
            return "[function]";
        }

        public string Visit( TernaryExpr expr )
        {
            return string.Format( "[? {0} {1} {2}]",
                expr.PredicateExpr.Accept( this ),
                expr.TrueExpr.Accept( this ),
                expr.FalseExpr.Accept( this ) );
        }

        public string Visit( VarDeclExpr expr )
        {
            return string.Format( "[VD \"{0}\"]", expr.Identifier );
        }

        public string Visit( BlockExpr expr )
        {
            StringBuilder sb = new StringBuilder();
            sb.Append( "[S" );
            foreach( IExpr e in expr.Statements )
            {
                sb.Append( " " );
                sb.Append( e.Accept( this ) );
            }
            sb.Append( "]" );

            return sb.ToString();
        }

        public string Visit( LookUpExpr expr )
        {
            return string.Format( "[LU \"{0}\"]", expr.Identifier );
        }

        public string Visit( AssignExpr expr )
        {
            return string.Format( "\"{0}\" <- \"{1}\"", expr.Left.Accept( this ), expr.Expression.Accept( this ) );
        }

        public string Visit( UndefinedExpr expr ) => "<<Undefined>>";

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
