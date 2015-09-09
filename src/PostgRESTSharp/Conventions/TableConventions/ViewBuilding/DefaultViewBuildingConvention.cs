using PostgRESTSharp.Text;
using System.Collections.Generic;

namespace PostgRESTSharp.Conventions
{
	public class DefaultViewBuildingConvention : IViewBuildingConvention, IDefaultTableConvention
    {
        private ITextUtility textUtility;
        private IConventionResolver conventionResolver;

        public DefaultViewBuildingConvention(ITextUtility textUtility, IConventionResolver conventionResolver)
        {
            this.textUtility = textUtility;
            this.conventionResolver = conventionResolver;
        }

        public IViewMetaModel BuildModel(ITableMetaModel storageModel, IEnumerable<ITableMetaModel> additionalStorageModels, string viewSchemaName)
        {
            var viewNamingConvention = this.conventionResolver.ResolveTableConvention<IViewNamingConvention>(storageModel);
            var model = new ViewMetaModel(storageModel.DatabaseName, viewSchemaName, viewNamingConvention.DetermineViewName(storageModel), 
                this.textUtility.ToCapitalCase(storageModel.TableName),
                this.textUtility.ToPluralCapitalCase(storageModel.TableName));
            // there is only one table involved
            model.SetPrimaryTableSource(storageModel);

            // add the columns
            foreach (var col in storageModel.Columns)
            {
                model.AddColumn(col, storageModel);
            }

            return model;
        }
    }
}