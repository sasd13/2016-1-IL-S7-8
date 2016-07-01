using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    public class FunctionValue : ValueBase
    {
        public FunctionValue( FunDeclExpr expr, IReadOnlyList<ClosureCapture> closure )
        {
            FunDecl = expr;
            Closure = closure;
        }

        public FunDeclExpr FunDecl { get; }

        public IReadOnlyList<ClosureCapture> Closure { get; }

        public override bool IsTrue => true;
    }
}
