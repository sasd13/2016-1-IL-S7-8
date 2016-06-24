using System;
using System.Linq;

namespace NS.CalviScript
{
    public class InfixStringVisitor : IVisitor<string>
    {
        public string Visit( ErrorExpr expr )
        {
            return string.Format( "[Error {0}]", expr.Message ); ;
        }

        public string Visit( TernaryExpr expr )
        {
            return string.Format( "({0} ? {1} : {2})",
                expr.PredicateExpr.Accept( this ),
                expr.TrueExpr.Accept( this ),
                expr.FalseExpr.Accept( this ) );
        }


        public string Visit( FunDeclExpr expr )
        {
            return $"function({string.Join( ", ", expr.Parameters.Select( p => p.Accept( this ) ) )}){expr.Body.Accept(this)}";
        }

        public string Visit( FunCallExpr expr )
        {
            return $"{expr.Name}( {string.Join( ", ", expr.ActualParameters.Select( p => p.Accept(this) ))} )";
        }

        public string Visit( VarDeclExpr expr )
        {
            return $"var {expr.Identifier}";
        }

        public string Visit( WhileExpr expr )
        {
            return $"while( {expr.Condition.Accept( this )} ) {expr.Body.Accept(this)}";
        }

        public string Visit( BlockExpr expr )
        {
            return "{" + string.Join( " ", expr.Statements.Select( s => s.Accept( this ) ) ) + "}";
        }

        public string Visit( LookUpExpr expr )
        {
            return expr.Identifier;
        }

        public string Visit( AssignExpr expr )
        {
            return string.Format( "{0} = {1}", expr.Left.Accept( this ), expr.Expression.Accept( this ) );
        }

        public string Visit( UnaryExpr expr )
        {
            return string.Format( "{0}{1}",
                TokenTypeHelpers.TokenTypeToString( expr.Type ),
                expr.Expr.Accept( this ) );
        }

        public string Visit( ConstantExpr expr )
        {
            return expr.Value.ToString();
        }

        public string Visit( UndefinedExpr expr ) => "<<Undefined>>";

        public string Visit( BinaryExpr expr )
        {
            return string.Format( "({0} {1} {2})",
                expr.LeftExpr.Accept( this ),
                TokenTypeHelpers.TokenTypeToString( expr.Type ),
                expr.RightExpr.Accept( this ) );
        }
    }
}
