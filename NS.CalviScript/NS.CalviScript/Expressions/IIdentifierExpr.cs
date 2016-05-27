namespace NS.CalviScript
{
    public interface IIdentifierExpr : IExpr
    {
        string Identifier { get; }

        VarDeclExpr VarDecl { get; }

    }
}
