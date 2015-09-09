namespace PostgRESTSharp.Conventions
{
    public interface IViewNamingConvention : ITableConvention
    {
        string DetermineViewName(ITableMetaModel metaModel);
    }
}