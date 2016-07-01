using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public FunDeclExpr( IReadOnlyList<VarDeclExpr> parameters, BlockExpr body, IReadOnlyList<VarDeclExpr> requiredClosure )
        {
            Parameters = parameters;
            Body = body;
            RequiredClosure = requiredClosure;
        }

        public IReadOnlyList<VarDeclExpr> Parameters { get; }

        public IReadOnlyList<VarDeclExpr> RequiredClosure { get; }

        public BlockExpr Body { get; }

        [DebuggerStepThrough]
        public T Accept<T>( IVisitor<T> visitor )
        {
            return visitor.Visit( this );
        }
    }
}
