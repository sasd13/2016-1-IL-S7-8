using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    /// <summary>
    /// FunCall			-> IDENTIFIER '(' (Expression (',' Expression)*)? ')'
    /// </summary>
    public class FunCallExpr : IExpr
    {
        public FunCallExpr( LookUpExpr name, IReadOnlyList<IExpr> actualParameters )
        {
            Name = name;
            ActualParameters = actualParameters;
        }

        public LookUpExpr Name { get; }

        public IReadOnlyList<IExpr> ActualParameters {  get; }

        public T Accept<T>( IVisitor<T> visitor )
        {
            return visitor.Visit( this );
        }
    }
}
