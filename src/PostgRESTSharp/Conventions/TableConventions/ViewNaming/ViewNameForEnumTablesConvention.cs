using System;
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
            int index = inputString.LastIndexOf("enum", StringComparison.CurrentCultureIgnoreCase);
            if (index > 0)
            {
                return inputString.Substring(0, index) + inputString.Substring(index + 3, inputString.Length - (index + 4));
            }
            return inputString;
        }
    }
}