namespace PostgRESTSharp.Conventions.ViewConventions.ColumnRemoval
{
    public interface IColumnRemovalConvention : IViewConvention
    {
        string ColumnToRemove();
    }
}
