using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    public class VarDeclExpr : IExpr
    {
        public VarDeclExpr( string identifier, IExpr expr )
        {
            Identifier = identifier;
            Expr = expr;
        }

        public string Identifier { get; }

        public IExpr Expr { get; }

        public T Accept<T>( IVisitor<T> visitor )
        {
            return visitor.Visit( this );
        }
    }
}
