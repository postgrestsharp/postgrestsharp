namespace PostgRESTSharp.Conventions
{
    public class ViewNameForEnumTablesConvention : IViewNamingConvention, IImplicitTableConvention
    {
        public string DetermineViewName(ITableMetaModel metaModel)
        {
            return metaModel.ModelNameCamelCased.Replace("Enum", "");
        }

        public bool IsMatch(ITableMetaModel metaModel)
        {
            return metaModel.TableName.ToLower().EndsWith("enum");
        }
    }
}