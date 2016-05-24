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
            Result = TokenTypeHelpers.Compute( left, right, expr.Type );
        }

        public int Result { get; private set; }
    }

    public class GenericEvalVisitor : IVisitor<int>
    {
        public int Visit( ErrorExpr expr )
        {
            throw new InvalidOperationException( expr.Message );
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
