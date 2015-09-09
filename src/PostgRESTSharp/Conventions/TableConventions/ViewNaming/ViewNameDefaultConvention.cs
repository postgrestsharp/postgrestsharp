using PostgRESTSharp.Text;

namespace PostgRESTSharp.Conventions
{
    public class ViewNameDefaultConvention : IViewNamingConvention, IDefaultTableConvention
    {
        private ITextUtility textUtility;

        public ViewNameDefaultConvention(ITextUtility textUtility)
        {
            this.textUtility = textUtility;
        }

        public string DetermineViewName(ITableMetaModel metaModel)
        {
            return metaModel.ModelNameCamelCased;
        }

        public string DetermineViewModelName(ITableMetaModel metaModel)
        {
            return this.textUtility.ToCapitalCase(metaModel.TableName);
        }

        public string DetermineViewPluralisedModelName(ITableMetaModel metaModel)
        {
            return this.textUtility.ToPluralCapitalCase(metaModel.TableName);
        }
    }
}