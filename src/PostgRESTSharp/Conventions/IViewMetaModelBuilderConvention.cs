using System;
using System.Collections.Generic;

namespace PostgRESTSharp.Conventions
{
	public interface IViewMetaModelBuilderConvention : IConvention
	{
		ViewMetaModelBuilderResult BuildModel(IMetaModel storageModel, IEnumerable<IMetaModel> additionalStorageModels, string viewSchemaName);
	}
}

