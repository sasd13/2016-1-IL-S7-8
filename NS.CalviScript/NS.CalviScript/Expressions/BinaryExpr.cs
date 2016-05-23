﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    public class BinaryExpr : IExpr
    {
        public IExpr LeftExpr { get; }

        public IExpr RightExpr { get; }

        public string ToLispyString() => string.Format("[{0} {1} {2}]", LeftExpr.ToLispyString(), RightExpr.ToLispyString());
    }
}
