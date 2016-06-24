using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    /// <summary>
    /// FunDecl -> 'function' '(' (IDENTIFIER (',' IDENTIFIER)*)? ')' Block
    /// </summary>
    public class FunDeclExpr : IExpr
    {
        public FunDeclExpr( IReadOnlyList<VarDeclExpr> parameters, BlockExpr body )
        {
            Parameters = parameters;
            Body = body;
        }

        public IReadOnlyList<VarDeclExpr> Parameters { get; }

        public BlockExpr Body { get; }

        public T Accept<T>( IVisitor<T> visitor )
        {
            return visitor.Visit( this );
        }
    }
}
