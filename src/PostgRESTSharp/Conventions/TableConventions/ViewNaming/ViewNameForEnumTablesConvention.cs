namespace PostgRESTSharp.Conventions
{
    public class ViewNameForEnumTablesConvention : IViewNamingConvention, IImplicitTableConvention
    {
        public string DetermineViewName(IMetaModel metaModel)
        {
            return metaModel.ModelNameCamelCased.Replace("Enum", "");
        }

        public bool IsMatch(IMetaModel metaModel)
        {
            return metaModel.TableName.ToLower().EndsWith("enum");
        }
    }
}