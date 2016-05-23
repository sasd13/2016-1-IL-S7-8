using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.CalviScript
{
    public class BinaryExpr : IExpr
    {
        public BinaryExpr( TokenType type, IExpr left, IExpr right )
        {
            Type = type;
            LeftExpr = left;
            RightExpr = right;
        }

        public TokenType Type { get; }

        public IExpr LeftExpr { get; }

        public IExpr RightExpr { get; }

        public string ToLispyString()
        {
            return string.Format( "[{0} {1} {2}]",
                TokenTypeToString( Type ),
                LeftExpr.ToLispyString(),
                RightExpr.ToLispyString() );
        }

        string TokenTypeToString(TokenType t)
        {
            if( t == TokenType.Plus ) return "+";
            else if( t == TokenType.Minus ) return "-";
            else if( t == TokenType.Mult ) return "*";
            else if( t == TokenType.Div ) return "/";
            else
            {
                Debug.Assert( t == TokenType.Modulo );
                return "%";
            }
        }
    }
}
