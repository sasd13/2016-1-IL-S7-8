using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    class SyntaxicScope
    {
        class Scope
        {
            public readonly Dictionary<string, VarDeclExpr> Values;
            public readonly List<VarDeclExpr> ClosureRequired;
            public bool IsFunctionScope => ClosureRequired != null;

            public Scope( bool isFunctionScope )
            {
                Values = new Dictionary<string, VarDeclExpr>();
                if( isFunctionScope ) ClosureRequired = new List<VarDeclExpr>();
            }
        }

        readonly Stack<Scope> _scopes;

        public SyntaxicScope()
        {
            _scopes = new Stack<Scope>();
            _scopes.Push( new Scope( false ) );
        }

        public IExpr Declare( string identifier )
        {
            VarDeclExpr existing;
            if( _scopes.Peek().Values.TryGetValue( identifier, out existing ) )
            {
                return new ErrorExpr( "Duplicate identifier declaration: " + identifier );
            }
            existing = new VarDeclExpr( identifier );
            _scopes.Peek().Values.Add( identifier, existing );
            return existing;
        }

        public LookUpExpr Lookup( string identifier )
        {
            VarDeclExpr existing = null;
            Scope requiresClosure = null;
            foreach( Scope s in _scopes )
            {
                if( s.Values.TryGetValue( identifier, out existing ) ) break;
                if( s.IsFunctionScope && requiresClosure == null )
                {
                    requiresClosure = s;
                }
            }
            if( requiresClosure != null && existing != null )
            {
                requiresClosure.ClosureRequired.Add( existing );
            }
            return new LookUpExpr( identifier, existing );        
        }

        public IReadOnlyList<VarDeclExpr> CurrentClosureRequired => _scopes.Peek().ClosureRequired;

        public IDisposable OpenScope( bool isFunctionScope ) => new ScopeCloser( this, isFunctionScope );


        class ScopeCloser : IDisposable
        {
            readonly SyntaxicScope _current;

            public ScopeCloser( SyntaxicScope s, bool isFunctionScope )
            {
                _current = s;
                _current._scopes.Push( new Scope( isFunctionScope ) );
            }

            public void Dispose()
            {
                _current._scopes.Pop();
            }
        }

    }
}
