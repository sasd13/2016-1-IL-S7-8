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
            throw new InvalidOperationException(expr.Message);
        }

        public void Visit( ConstantExpr expr )
        {
            Result = expr.Value;
        }

        public void Visit( BinaryExpr expr )
        {
            expr.LeftExpr.Accept(this);
            int left = Result;
            expr.RightExpr.Accept(this);
            int right = Result;

            Compute(left, right, expr.Type);
        }

        public int Result { get; private set; }

        public void Compute(int left, int right, TokenType tokenType)
        {
            switch (tokenType)
            {
                case TokenType.Plus:
                    Result = left + right;
                    break;
                case TokenType.Minus:
                    Result = left - right;
                    break;
                case TokenType.Mult:
                    Result = left * right;
                    break;
                case TokenType.Div:
                    Result = left / right;
                    break;
                case TokenType.Modulo:
                    Result = left % right;
                    break;
                default:
                    throw new ArgumentException(string.Format("'{0}' is not a valid operator", tokenType));
            }
        }
    }

    public class GenericEvalVisitor : IVisitor<int>
    {
        public int Visit( ErrorExpr expr )
        {
            throw new NotImplementedException();
        }

        public int Visit( ConstantExpr expr )
        {
            throw new NotImplementedException();
        }

        public int Visit( BinaryExpr expr )
        {
            throw new NotImplementedException();
        }
    }
}
