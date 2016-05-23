using System.Diagnostics;

namespace NS.CalviScript
{
    public class LispyStringVisitor : IVisitor
    {
        public void Visit( BinaryExpr expr )
        {
            expr.LeftExpr.Accept( this );
            string left = Result;
            expr.RightExpr.Accept( this );
            string right = Result;
            Result = string.Format( "[{0} {1} {2}]",
                TokenTypeToString( expr.Type ),
                left,
                right );
        }

        public void Visit( ConstantExpr expr )
        {
            Result = expr.Value.ToString();
        }

        public void Visit( ErrorExpr expr )
        {
            Result = string.Format( "[Error {0}]", expr.Message );
        }

        public string Result { get; private set; }

        string TokenTypeToString( TokenType t )
        {
            if( t == TokenType.Plus ) return "+";
            else if( t == TokenType.Minus ) return "-";
            else if( t == TokenType.Mult ) return "*";
            else if( t == TokenType.Div ) return "/";
            else
            {
                Debug.Assert( t == TokenType.Modulo );
                return "%";
            }
        }
    }
}
