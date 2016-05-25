namespace NS.CalviScript
{
    public interface IExpr
    {
        T Accept<T>( IVisitor<T> visitor );
    }
}
