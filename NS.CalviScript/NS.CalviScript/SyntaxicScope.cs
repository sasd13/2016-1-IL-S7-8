using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    class SyntaxicScope
    {
        Dictionary<string, VarDeclExpr> _scope;

        public SyntaxicScope()
        {
            _scope = new Dictionary<string, VarDeclExpr>();
        }

        public IExpr Declare( string identifier )
        {
            VarDeclExpr existing;
            if( _scope.TryGetValue( identifier, out existing ) )
            {
                return new ErrorExpr( "Duplicate identifier declaration: " + identifier );
            }
            existing = new VarDeclExpr( identifier );
            _scope.Add( identifier, existing );
            return existing;
        }

        internal LookUpExpr Lookup( string identifier )
        {
            VarDeclExpr existing;
            _scope.TryGetValue( identifier, out existing );
            return new LookUpExpr( identifier, existing );        
        }

        class ScopeCloser : IDisposable
        {
            readonly SyntaxicScope _current;

            public ScopeCloser( SyntaxicScope s )
            {
                _current = s;
                // DO IT
            }

            public void Dispose()
            {
                // UNDO IT
            }
        }

        internal IDisposable OpenScope() => new ScopeCloser( this );

    }
}
