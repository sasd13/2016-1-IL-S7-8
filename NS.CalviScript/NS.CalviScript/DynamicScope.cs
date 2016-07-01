using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    class DynamicScope
    {
        readonly Stack<Dictionary<VarDeclExpr,ValueBase>> _values;

        public DynamicScope()
        {
            _values = new Stack<Dictionary<VarDeclExpr, ValueBase>>();
            _values.Push( new Dictionary<VarDeclExpr, ValueBase>() );
        }

        public IDisposable OpenScope() => new ScopeCloser( this );


        class ScopeCloser : IDisposable
        {
            readonly DynamicScope _current;

            public ScopeCloser( DynamicScope s )
            {
                _current = s;
                _current._values.Push( new Dictionary<VarDeclExpr, ValueBase>() );
            }

            public void Dispose()
            {
                _current._values.Pop();
            }
        }

        public void Register( VarDeclExpr expr, ValueBase v = null )
        {
            _values.Peek()[expr] = v ?? UndefinedValue.Default;
        }

        public ValueBase FindRegistered( VarDeclExpr varDecl )
        {
            ValueBase existing = null;
            foreach( var d in _values )
            {
                if( d.TryGetValue( varDecl, out existing ) )
                {
                    return existing;
                }
            }
            throw new Exception( "Variables are necessarily Registered!" );
        }

        public ValueBase SetValue( VarDeclExpr varDecl, ValueBase e )
        {
            foreach( var d in _values )
            {
                if( d.ContainsKey( varDecl ) )
                {
                    d[varDecl] = e;
                    return e;
                }
            }
            throw new Exception( "Variables are necessarily Registered!" );
        }
    }
}
