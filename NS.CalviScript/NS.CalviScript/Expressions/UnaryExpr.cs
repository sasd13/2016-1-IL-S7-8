﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    public class UnaryExpr : IExpr
    {
        public UnaryExpr( TokenType type, IExpr expr )
        {
            Type = type;
            Expr = expr;
        }

        public TokenType Type { get; }

        public IExpr Expr { get; }

        public T Accept<T>( IVisitor<T> visitor )
        {
            return visitor.Visit( this );
        }
    }
}
