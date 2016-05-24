using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    public class EvalVisitor : IVisitor
    {
        public void Visit( ErrorExpr expr )
        {
            throw new NotImplementedException();
        }

        public void Visit( ConstantExpr expr )
        {
            throw new NotImplementedException();
        }

        public void Visit( BinaryExpr expr )
        {
            throw new NotImplementedException();
        }

        public int Result { get; private set; }
    }
}
