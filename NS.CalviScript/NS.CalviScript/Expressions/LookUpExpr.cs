using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    public class LookUpExpr : IExpr, IIdentifierExpr
    {
        public LookUpExpr( string identifier, VarDeclExpr varDecl )
        {
            Identifier = identifier;
            VarDecl = varDecl;
        }

        public string Identifier { get; }

        public VarDeclExpr VarDecl { get; }

        public T Accept<T>( IVisitor<T> visitor )
        {
            return visitor.Visit( this );
        }
    }
}
