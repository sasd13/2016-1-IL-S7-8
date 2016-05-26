using System;
using System.Collections.Generic;

namespace NS.CalviScript
{
    public class EvalVisitor : StandardVisitor
    {
        readonly Dictionary<string, int> _globalContext;

        public EvalVisitor( Dictionary<string, int> globalContext )
        {
            _globalContext = globalContext;
        }

        public override IExpr Visit( BlockExpr expr )
        {
            IExpr last = UndefinedExpr.Default;
            foreach( var s in expr.Statements )
            {
                last = s.Accept( this );
            }
            return last;
        }

        public override IExpr Visit( LookUpExpr expr )
        {
            int knownValue;
            if( _globalContext.TryGetValue( expr.Identifier, out knownValue ) )
            {
                return new ConstantExpr( knownValue );
            }
            return new ErrorExpr( "Reference not found: " + expr.Identifier );
        }
    }
}
