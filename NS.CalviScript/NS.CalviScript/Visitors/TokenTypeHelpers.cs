using System.Diagnostics;

namespace NS.CalviScript
{
    static class TokenTypeHelpers
    {
        internal static string TokenTypeToString( TokenType t )
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
