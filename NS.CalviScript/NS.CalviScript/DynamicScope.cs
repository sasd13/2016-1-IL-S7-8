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

    }
}
