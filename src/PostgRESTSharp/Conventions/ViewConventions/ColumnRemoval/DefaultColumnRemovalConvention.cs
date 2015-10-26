namespace PostgRESTSharp.Conventions.ViewConventions.ColumnRemoval
{
    public class DefaultColumnRemovalConvention : IColumnRemovalConvention, IDefaultViewConvention
    {
        public string ColumnToRemove()
        {
            return "";
        }
    }
}
