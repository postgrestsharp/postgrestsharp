using System;
using System.Collections.Generic;

namespace PostgRESTSharp.Conventions
{
	public interface IViewBuildingConvention : ITableConvention
	{
        IViewMetaModel BuildModel(ITableMetaModel storageModel, IEnumerable<ITableMetaModel> additionalStorageModels, string viewSchemaName);
    }
}

