using System;
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

        internal static int Compute( int left, int right, TokenType type )
        {
            switch( type )
            {
                case TokenType.Plus:
                    return left + right;
                case TokenType.Minus:
                    return left - right;
                case TokenType.Mult:
                    return left * right;
                case TokenType.Div:
                    return left / right;
                case TokenType.Modulo:
                    return left % right;
                default:
                    throw new ArgumentException( "Argument is not an operator" );
            }
        }
    }
}
