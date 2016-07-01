using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    public class FunctionValue : ValueBase
    {
        public FunctionValue( FunDeclExpr expr )
        {
            FunDecl = expr;
        }

        public FunDeclExpr FunDecl { get; }

        public override bool IsTrue => true;
    }
}
