namespace NS.CalviScript
{
    public interface IExpr
    {
        string ToLispyString();

        string ToInfixString();

        void Accept( IVisitor visitor );

        T Accept<T>( IVisitor<T> visitor );
    }
}
