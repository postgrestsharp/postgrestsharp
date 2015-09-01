using System;
using System.Collections.Generic;

namespace PostgrestSharp
{
	public interface IViewMetaModelBuilder
	{
		IViewMetaModel BuildModel(IMetaModel storageModel, IEnumerable<IMetaModel> additionalStorageModels, string viewSchemaName);
	}
}

