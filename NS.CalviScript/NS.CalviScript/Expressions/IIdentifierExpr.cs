namespace NS.CalviScript
{
    public interface IIdentifierExpr : IExpr
    {
        /// <summary>
        /// Gets the name of the identifier.
        /// </summary>
        string Identifier { get; }

        /// <summary>
        /// Gets the associated <see cref="VarDecl"/> to which this
        /// identifier is statically bound.
        /// </summary>
        VarDeclExpr VarDecl { get; }

    }
}
