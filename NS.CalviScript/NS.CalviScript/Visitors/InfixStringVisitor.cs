using System;
using System.Diagnostics;

namespace NS.CalviScript
{
    public class InfixStringVisitor : IVisitor
    {
        public void Visit( BinaryExpr expr )
        {
            expr.LeftExpr.Accept( this );
            string left = Result;
            expr.RightExpr.Accept( this );
            string right = Result;
            Result = string.Format( "({0} {1} {2})",
                left,
                TokenTypeHelpers.TokenTypeToString( expr.Type ),
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
    }

    public class GenericInfixStringVisitor : IVisitor<string>
    {
        public string Visit( ErrorExpr expr )
        {
            throw new NotImplementedException();
        }

        public string Visit( ConstantExpr expr )
        {
            throw new NotImplementedException();
        }

        public string Visit( BinaryExpr expr )
        {
            throw new NotImplementedException();
        }
    }
}
