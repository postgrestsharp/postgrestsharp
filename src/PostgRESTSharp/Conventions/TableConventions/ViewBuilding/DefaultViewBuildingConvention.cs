using PostgRESTSharp.Text;
using System.Collections.Generic;

namespace PostgRESTSharp.Conventions
{
	public class DefaultViewBuildingConvention : IViewBuildingConvention, IDefaultTableConvention
    {
        public IViewMetaModel BuildModel(IViewMetaModel viewToBuild, ITableMetaModel storageModel, IEnumerable<ITableMetaModel> additionalStorageModels)
        {
            // there is only one table involved
            viewToBuild.SetPrimaryTableSource(storageModel);

            // add the columns
            foreach (var col in storageModel.Columns)
            {
                viewToBuild.AddColumn(col, storageModel);
            }

            return viewToBuild;
        }
    }
}