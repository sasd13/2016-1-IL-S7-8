﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NS.CalviScript
{
    public class EvalVisitor : IVisitor<ValueBase>
    {
        readonly Dictionary<string, ValueBase> _globalContext;
        readonly DynamicScope _variables;

        public EvalVisitor( Dictionary<string, ValueBase> globalContext = null )
        {
            _globalContext = globalContext ?? new Dictionary<string, ValueBase>();
            _variables = new DynamicScope();
        }

        public ValueBase Visit( BlockExpr expr )
        {
            ValueBase last = UndefinedValue.Default;
            foreach( var s in expr.Statements )
            {
                last = s.Accept( this );
            }
            return last;
        }

        public ValueBase Visit( VarDeclExpr expr )
        {
            _variables.Register( expr, new RefValue( UndefinedValue.Default ) );
            return UndefinedValue.Default;
        }

        public ValueBase Visit( LookUpExpr expr )
        {
            if( expr.VarDecl != null )
            {
                return _variables.FindRegistered(expr.VarDecl);
            }
            else
            {
                ValueBase knownValue;
                if( _globalContext.TryGetValue( expr.Identifier, out knownValue ) )
                {
                    return knownValue;
                }
                return new ErrorValue( "Reference not found: " + expr.Identifier );
            }
        }

        public ValueBase Visit( FunDeclExpr expr )
        {
            var closure = expr.RequiredClosure
                                .Select( v => new ClosureCapture( v, _variables.FindRegistered( v ) ) )
                                .ToArray();
            return new FunctionValue( expr, closure );
        }

        public ValueBase Visit( FunCallExpr expr )
        {
            List<ValueBase> parameterValues = expr.ActualParameters.Select( p => p.Accept( this ) ).ToList();
            ValueBase fO = expr.Name.Accept( this );
            FunctionValue f = fO as FunctionValue;
            if( f == null ) return new ErrorValue( $"{expr.Name.Identifier} is not a function." );
            while( f.FunDecl.Parameters.Count > parameterValues.Count )
            {
                parameterValues.Add( UndefinedValue.Default );
            }
            using( _variables.OpenScope() )
            {
                for( int i = 0; i < f.FunDecl.Parameters.Count; ++i )
                {
                    _variables.Register( f.FunDecl.Parameters[i], new RefValue( parameterValues[i] ) );
                }
                foreach( var c in f.Closure )
                {
                    _variables.Register( c.VarDecl, c.Value );
                }
                return f.FunDecl.Body.Accept( this );
            }
        }

        public ValueBase Visit( AssignExpr expr )
        {
            var e = expr.Expression.Accept( this );
            expr.Left.Accept( this );
            if( expr.Left.VarDecl != null )
            {
                return _variables.FindRegistered( expr.Left.VarDecl ).RealValue = e;
            }
            else
            {
                // if( stricMode )
                //    return new ErrorValue( "Global assignation is disabled in strict mode." );
                return _globalContext[expr.Left.Identifier] = e;
            }
        }

        public ValueBase Visit( UnaryExpr expr )
        {
            ValueBase e = expr.Expr.Accept( this );
            if( !(e is IntegerValue) ) return UndefinedValue.Default;
            IntegerValue val = (IntegerValue)e;
            return IntegerValue.Create( -val.Value );
        }

        public ValueBase Visit( BinaryExpr expr )
        {
            var l = expr.LeftExpr.Accept( this );
            var r = expr.RightExpr.Accept( this );
            if( l is IntegerValue && r is IntegerValue )
            {
                var lC = (IntegerValue)l;
                var rC = (IntegerValue)r;
                switch( expr.Type )
                {
                    case TokenType.Div: return IntegerValue.Create( lC.Value / rC.Value );
                    case TokenType.Mult: return IntegerValue.Create( lC.Value * rC.Value );
                    case TokenType.Minus: return IntegerValue.Create( lC.Value - rC.Value );
                    case TokenType.Plus: return IntegerValue.Create( lC.Value + rC.Value );
                    default:
                        {
                            Debug.Assert( expr.Type == TokenType.Modulo );
                            return IntegerValue.Create( lC.Value % rC.Value );
                        }
                }
            }
            return UndefinedValue.Default;
        }

        public ValueBase Visit( TernaryExpr expr )
        {
            var p = expr.PredicateExpr.Accept( this );
            return p.IsTrue
                    ? expr.TrueExpr.Accept( this )
                    : expr.FalseExpr.Accept( this );
        }

        public ValueBase Visit( WhileExpr expr )
        {
            ValueBase last = UndefinedValue.Default;
            while( expr.Condition.Accept( this ).IsTrue )
            {
                last = expr.Body.Accept( this );
            }
            return last;
        }

        public ValueBase Visit( ConstantExpr expr ) => IntegerValue.Create( expr.Value );

        public ValueBase Visit( ErrorExpr expr ) => new ErrorValue( expr.Message );

        public ValueBase Visit( UndefinedExpr expr ) => UndefinedValue.Default;


    }
}
