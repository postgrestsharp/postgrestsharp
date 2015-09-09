using System;
using System.Collections.Generic;

namespace PostgRESTSharp
{
	public interface IViewMetaModelBuilder
	{
		IViewMetaModel BuildModel(ITableMetaModel storageModel, IEnumerable<ITableMetaModel> additionalStorageModels, string viewSchemaName);
	}
}

