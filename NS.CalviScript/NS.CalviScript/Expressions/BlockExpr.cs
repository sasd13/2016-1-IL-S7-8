using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    public class BlockExpr : IExpr
    {
        public BlockExpr( IReadOnlyList<IExpr> statements )
        {
            Statements = statements;
        }

        public IReadOnlyList<IExpr> Statements { get; }

        public T Accept<T>( IVisitor<T> visitor )
        {
            return visitor.Visit( this );
        }
    }
}
