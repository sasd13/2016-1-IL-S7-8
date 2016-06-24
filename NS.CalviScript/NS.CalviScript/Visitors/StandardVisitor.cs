using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    public class StandardVisitor : IVisitor<IExpr>
    {
        public IExpr Visit( ErrorExpr expr ) => expr;

        public virtual IExpr Visit( TernaryExpr expr )
        {
            IExpr p = expr.PredicateExpr.Accept( this );
            IExpr t = expr.TrueExpr.Accept( this );
            IExpr f = expr.FalseExpr.Accept( this );
            return p != expr.PredicateExpr || t != expr.TrueExpr || f != expr.FalseExpr
                    ? new TernaryExpr( p, t, f )
                    : expr;
        }

        public virtual IExpr Visit( VarDeclExpr expr ) => expr;

        public virtual IExpr Visit( AssignExpr expr )
        {
            IIdentifierExpr l = (IIdentifierExpr)expr.Left.Accept( this );
            IExpr e = expr.Expression.Accept( this );
            return l != expr.Left || e != expr.Expression
                    ? new AssignExpr( l, e )
                    : expr;
        }

        public virtual IExpr Visit( WhileExpr expr )
        {
            IExpr condition = expr.Condition.Accept( this );
            BlockExpr body = (BlockExpr)expr.Body.Accept( this );
            return condition != expr.Condition || body != expr.Body
                    ? new WhileExpr( condition, body )
                    : expr;
        }

        public virtual IExpr Visit( BlockExpr expr )
        {
            List<IExpr> newContent = null;
            int i = 0;
            foreach( var s in expr.Statements )
            {
                var sT = s.Accept( this );
                if( sT != s )
                {
                    if( newContent == null )
                    {
                        newContent = new List<IExpr>();
                        newContent.AddRange( expr.Statements.Take( i ) );
                    }
                }
                if( newContent != null ) newContent.Add( sT );
                ++i;
            }
            return newContent != null ? new BlockExpr( newContent ) : expr;
        }

        public virtual IExpr Visit( FunDeclExpr expr )
        {
            List<VarDeclExpr> newContent = null;
            int i = 0;
            foreach( var s in expr.Parameters )
            {
                var sT = s.Accept( this );
                if( sT != s )
                {
                    if( newContent == null )
                    {
                        newContent = new List<VarDeclExpr>();
                        newContent.AddRange( expr.Parameters.Take( i ) );
                    }
                }
                if( newContent != null ) newContent.Add( (VarDeclExpr)sT);
                ++i;
            }

            var newParameters = newContent != null ? newContent : expr.Parameters;
            var newBody = (BlockExpr)expr.Body.Accept( this );

            return newParameters != expr.Parameters || newBody != expr.Body 
                    ? new FunDeclExpr( newParameters, newBody ) 
                    : expr;
        }

        public virtual IExpr Visit( LookUpExpr expr ) => expr;

        public virtual IExpr Visit( UnaryExpr expr )
        {
            var e = expr.Expr.Accept( this );
            return e != expr.Expr ? new UnaryExpr( expr.Type, e ) : expr;
        }

        public virtual IExpr Visit( ConstantExpr expr ) => expr;

        public virtual IExpr Visit( BinaryExpr expr )
        {
            var l = expr.LeftExpr.Accept( this );
            var r = expr.RightExpr.Accept( this );
            return l != expr.LeftExpr || r != expr.RightExpr
                    ? new BinaryExpr( expr.Type, l, r )
                    : expr;
        }

        public virtual IExpr Visit( UndefinedExpr expr ) => expr;

    }
}
