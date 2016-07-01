using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    public class ClosureCapture
    {
        public readonly VarDeclExpr VarDecl;
        public readonly RefValue Value;

        public ClosureCapture( VarDeclExpr d, RefValue v )
        {
            VarDecl = d;
            Value = v;
        }
    }
}
