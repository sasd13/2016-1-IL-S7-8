namespace NS.CalviScript
{
    public interface IExpr
    {
        string ToLispyString();

        string ToInfixString();

        void Accept( IVisitor visitor );
    }
}
