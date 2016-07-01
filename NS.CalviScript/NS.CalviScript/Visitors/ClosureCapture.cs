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
        public readonly ValueBase Value;

        public ClosureCapture( VarDeclExpr d, ValueBase v )
        {
            VarDecl = d;
            Value = v;
        }
    }
}
