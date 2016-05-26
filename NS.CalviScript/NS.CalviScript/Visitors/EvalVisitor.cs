using System;
using System.Collections.Generic;
using System.Diagnostics;

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
            //TODO: Before challenging the global context,
            //      we must first handle the expr.VarDecl associated value
            //      (when expr.VarDecl is not null)...
            int knownValue;
            if( _globalContext.TryGetValue( expr.Identifier, out knownValue ) )
            {
                return new ConstantExpr( knownValue );
            }
            return new ErrorExpr( "Reference not found: " + expr.Identifier );
        }

        public override IExpr Visit( AssignExpr expr )
        {
            var e = expr.Expression.Accept( this );
            // TODO: Set the value associated to expr.Left...
            return e;
        }

        public override IExpr Visit( UnaryExpr expr )
        {
            IExpr e = expr.Expr.Accept( this );
            if( !(e is ConstantExpr) ) return e;
            ConstantExpr val = (ConstantExpr)e;
            return new ConstantExpr( -val.Value );
        }

        public override IExpr Visit( BinaryExpr expr )
        {
            var l = expr.LeftExpr.Accept( this );
            var r = expr.RightExpr.Accept( this );
            if( l is UndefinedExpr ) return l;
            if( r is UndefinedExpr ) return r;

            var lC = (ConstantExpr)l;
            var rC = (ConstantExpr)r;
            switch( expr.Type )
            {
                case TokenType.Div: return new ConstantExpr( lC.Value / rC.Value );
                case TokenType.Mult: return new ConstantExpr( lC.Value * rC.Value );
                case TokenType.Minus: return new ConstantExpr( lC.Value - rC.Value );
                case TokenType.Plus: return new ConstantExpr( lC.Value + rC.Value );
                default:
                    {
                        Debug.Assert( expr.Type == TokenType.Modulo );
                        return new ConstantExpr( lC.Value % rC.Value );
                    }
            }
        }

        public override IExpr Visit( TernaryExpr expr )
        {
            var p = expr.PredicateExpr.Accept( this );
            if( p is ConstantExpr )
            {
                if( ((ConstantExpr)p).Value >= 0 )
                {
                    return expr.TrueExpr.Accept( this );
                }
                return expr.FalseExpr.Accept( this );
            }
            var t = expr.TrueExpr.Accept( this );
            var f = expr.FalseExpr.Accept( this );
            return p != expr.PredicateExpr || t != expr.TrueExpr || f != expr.FalseExpr
                    ? new TernaryExpr( p, t, f )
                    : expr;
        }



    }
}
