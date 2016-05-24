using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Result = Compute( left, right, expr.Type );
        }

        private int Compute( int left, int right, TokenType type )
        {
            switch( type )
            {
                case TokenType.Plus:
                    return left + right;
                case TokenType.Minus:
                    return left - right;
                case TokenType.Mult:
                    return left * right;
                case TokenType.Div:
                    return left / right;
                case TokenType.Modulo:
                    return left % right;
                default:
                    throw new ArgumentException( "Argument is not an operator" );
            }
        }

        public int Result { get; private set; }
    }
}
