namespace NS.CalviScript
{
    public class Token
    {
        public Token( TokenType type )
            : this( type, string.Empty )
        {
        }

        public Token( TokenType type, char c )
            : this( type, c.ToString() )
        {
        }

        public Token( TokenType type, string value )
        {
            Type = type;
            Value = value;
        }

        public TokenType Type { get; }

        public string Value { get; }
    }
}