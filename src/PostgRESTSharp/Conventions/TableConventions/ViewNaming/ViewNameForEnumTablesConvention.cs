using PostgRESTSharp.Text;

namespace PostgRESTSharp.Conventions
{
    public class ViewNameForEnumTablesConvention : IViewNamingConvention, IImplicitTableConvention
    {
        private ITextUtility textUtility;

        public ViewNameForEnumTablesConvention(ITextUtility textUtility)
        {
            this.textUtility = textUtility;
        }

        public string DetermineViewName(ITableMetaModel metaModel)
        {
            return this.Process(metaModel.ModelNameCamelCased);
        }

        public string DetermineViewModelName(ITableMetaModel metaModel)
        {
            return this.textUtility.ToCapitalCase(this.Process(metaModel.TableName));
        }

        public string DetermineViewPluralisedModelName(ITableMetaModel metaModel)
        {
            return this.textUtility.ToPluralCapitalCase(this.Process(metaModel.TableName));
        }

        public bool IsMatch(ITableMetaModel metaModel)
        {
            return metaModel.TableName.ToLower().EndsWith("enum");
        }

        private string Process(string inputString)
        {
            return inputString.Replace("Enum", "").Replace("enum", "");
        }
    }
}