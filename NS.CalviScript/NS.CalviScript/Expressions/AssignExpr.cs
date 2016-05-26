using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    public class AssignExpr : IExpr
    {
        public AssignExpr( IIdentifierExpr left, IExpr e )
        {
            Left = left;
            Expression = e;
        }

        public IIdentifierExpr Left { get; }

        public IExpr Expression { get; }

        public T Accept<T>( IVisitor<T> visitor ) => visitor.Visit( this );
    }
}
