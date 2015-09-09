namespace PostgRESTSharp.Conventions
{
    public interface IViewNamingConvention : ITableConvention
    {
        string DetermineViewName(ITableMetaModel metaModel);

        string DetermineViewModelName(ITableMetaModel metaModel);

        string DetermineViewPluralisedModelName(ITableMetaModel metaModel);
    }
}