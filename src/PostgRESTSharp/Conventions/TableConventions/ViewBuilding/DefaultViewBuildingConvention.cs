using AutoMapper.Internal;
using PostgRESTSharp.Conventions.ViewConventions.ViewFiltering;
using PostgRESTSharp.Text;
using System.Collections.Generic;

namespace PostgRESTSharp.Conventions
{
	public class DefaultViewBuildingConvention : IViewBuildingConvention, IDefaultTableConvention
    {

        private IConventionResolver conventionResolver;

        public DefaultViewBuildingConvention(IConventionResolver conventionResolver)
        {
            this.conventionResolver = conventionResolver;
        }
        
        public IViewMetaModel BuildModel(IViewMetaModel viewToBuild, ITableMetaModel storageModel, IEnumerable<ITableMetaModel> additionalStorageModels)
        {
            // there is only one table involved
            viewToBuild.SetPrimaryTableSource(storageModel);

            // add the columns
            foreach (var col in storageModel.Columns)
            {
                viewToBuild.AddColumn(col, storageModel);
            }

            foreach (var filteringConvention in conventionResolver.ResolveViewConventions<IViewFilteringConvention>(viewToBuild))
            {
                viewToBuild.AddFilterElements(filteringConvention.FilterElements());
                viewToBuild.FilterElements.Each(x => x.SetTableName(viewToBuild.PrimarySource.TableName));
            }

            foreach (var columnRemovalConvention in conventionResolver.ResolveViewConventions<IColumnRemovalConvention>(viewToBuild))
            {
                viewToBuild.SetColumnToHidden(columnRemovalConvention.ColumnToRemove());
            }
            
            return viewToBuild;
        }
    }
}