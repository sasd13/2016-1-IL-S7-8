using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    public class ErrorExpr : IExpr
    {
        public ErrorExpr( string message )
        {
            Message = message;
        }

        public string Message { get; }

        public string ToLispyString() => string.Format( "[Error {0}]", Message );
    }
}
