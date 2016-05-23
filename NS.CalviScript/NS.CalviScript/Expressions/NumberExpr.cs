using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    public class NumberExpr : IExpr
    {
        public NumberExpr( int value )
        {
            Value = value;
        }

        public int Value { get; }

        public string ToLispyString() => Value.ToString();
    }
}
