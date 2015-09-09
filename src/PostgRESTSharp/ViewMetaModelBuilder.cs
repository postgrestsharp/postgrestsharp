using System;
using System.Collections.Generic;
using System.Linq;
using PostgRESTSharp.Conventions;

namespace PostgRESTSharp
{
	public class ViewMetaModelBuilder : IViewMetaModelBuilder
	{
		private IEnumerable<IViewMetaModelBuilderConvention> metaModelBuilderConventions;

		public ViewMetaModelBuilder (IEnumerable<IViewMetaModelBuilderConvention> metaModelBuilderConventions)
		{
			this.metaModelBuilderConventions = metaModelBuilderConventions;
		}

		public IViewMetaModel BuildModel (IMetaModel storageModel, IEnumerable<IMetaModel> additionalStorageModels, string viewSchemaName)
		{

			foreach (var discovery in this.metaModelBuilderConventions) 
			{
				var result = discovery.BuildModel (storageModel, additionalStorageModels, viewSchemaName);
				if (result.WasHandled) 
				{
					return result.Result;
				}
			}

			return null;
		}
	}
}

