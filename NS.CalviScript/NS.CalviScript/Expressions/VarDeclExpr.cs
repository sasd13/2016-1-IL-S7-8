﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    public class VarDeclExpr : IExpr, IIdentifierExpr
    {
        public VarDeclExpr( string identifier )
        {
            Identifier = identifier;
        }

        public string Identifier { get; }

        public T Accept<T>( IVisitor<T> visitor ) => visitor.Visit( this );
    }
}