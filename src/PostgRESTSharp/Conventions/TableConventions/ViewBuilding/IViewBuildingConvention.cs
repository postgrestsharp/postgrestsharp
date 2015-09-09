using System;
using System.Collections.Generic;

namespace PostgRESTSharp.Conventions
{
	public interface IViewBuildingConvention : ITableConvention
	{
        IViewMetaModel BuildModel(IMetaModel storageModel, IEnumerable<IMetaModel> additionalStorageModels, string viewSchemaName);
    }
}

