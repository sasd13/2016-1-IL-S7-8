using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    class SyntaxicScope
    {
        readonly Stack<Dictionary<string, VarDeclExpr>> _scopes;

        public SyntaxicScope()
        {
            _scopes = new Stack<Dictionary<string, VarDeclExpr>>();
            _scopes.Push( new Dictionary<string, VarDeclExpr>() );
        }

        public IExpr Declare( string identifier )
        {
            VarDeclExpr existing;
            if( _scopes.Peek().TryGetValue( identifier, out existing ) )
            {
                return new ErrorExpr( "Duplicate identifier declaration: " + identifier );
            }
            existing = new VarDeclExpr( identifier );
            _scopes.Peek().Add( identifier, existing );
            return existing;
        }

        public LookUpExpr Lookup( string identifier )
        {
            VarDeclExpr existing = null;
            foreach( var d in _scopes )
            {
                if( d.TryGetValue( identifier, out existing ) ) break;
            }
            return new LookUpExpr( identifier, existing );        
        }

        public IDisposable OpenScope() => new ScopeCloser( this );


        class ScopeCloser : IDisposable
        {
            readonly SyntaxicScope _current;

            public ScopeCloser( SyntaxicScope s )
            {
                _current = s;
                _current._scopes.Push( new Dictionary<string, VarDeclExpr>() );
            }

            public void Dispose()
            {
                _current._scopes.Pop();
            }
        }

    }
}
