using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    class DynamicScope
    {
        readonly Stack<Dictionary<VarDeclExpr,RefValue>> _values;

        public DynamicScope()
        {
            _values = new Stack<Dictionary<VarDeclExpr, RefValue>>();
            _values.Push( new Dictionary<VarDeclExpr, RefValue>() );
        }

        public IDisposable OpenScope() => new ScopeCloser( this );


        class ScopeCloser : IDisposable
        {
            readonly DynamicScope _current;

            public ScopeCloser( DynamicScope s )
            {
                _current = s;
                _current._values.Push( new Dictionary<VarDeclExpr, RefValue>() );
            }

            public void Dispose()
            {
                _current._values.Pop();
            }
        }

        public void Register( VarDeclExpr expr, RefValue v )
        {
            _values.Peek()[expr] = v;
        }

        public RefValue FindRegistered( VarDeclExpr varDecl )
        {
            RefValue existing = null;
            foreach( var d in _values )
            {
                if( d.TryGetValue( varDecl, out existing ) )
                {
                    return existing;
                }
            }
            throw new Exception( "Variables are necessarily Registered!" );
        }

    }
}
