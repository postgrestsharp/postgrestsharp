using System;
using System.Collections.Generic;

namespace PostgRESTSharp
{
	public interface IViewMetaModelBuilderConvention
	{
		ViewModelBuilderConventionType ConventionType { get; }

		ViewModelBuilderConventionLevel Level { get; }

		ViewMetaModelBuilderResult BuildModel(IMetaModel storageModel, IEnumerable<IMetaModel> additionalStorageModels, string viewSchemaName);
	}
}

