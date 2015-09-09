namespace PostgRESTSharp.Conventions
{
    public interface IViewNamingConvention : ITableConvention
    {
        string DetermineViewName(IMetaModel metaModel);
    }
}