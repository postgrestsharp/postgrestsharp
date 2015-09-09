using System;
using System.Collections.Generic;

namespace PostgRESTSharp.Conventions
{
	public interface IViewBuildingConvention : ITableConvention
	{
        IViewMetaModel BuildModel(IViewMetaModel viewToBuild, ITableMetaModel storageModel, IEnumerable<ITableMetaModel> additionalStorageModels);
    }
}

