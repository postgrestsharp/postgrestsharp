using System;
using System.Collections.Generic;
using System.Linq;

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
			bool wasExcluded = false;
			// check for an exclusion convention first, do table level versus convention level first
			foreach(var exclusion in this.metaModelBuilderConventions.Where(x=>x.ConventionType == ViewModelBuilderConventionType.Exclusion).OrderByDescending(y=>y.ConventionType))
			{
				var result = exclusion.BuildModel (storageModel, additionalStorageModels, viewSchemaName);
				if (result.WasHandled) 
				{
					wasExcluded = true;
				}
			}

			// if there was no exclusion we can process the storageModel, do table level versus convention level first
			if (!wasExcluded) 
			{
				foreach (var discovery in this.metaModelBuilderConventions.Where(x=>x.ConventionType == ViewModelBuilderConventionType.Inclusion).OrderByDescending(y=>y.ConventionType)) 
				{
					var result = discovery.BuildModel (storageModel, additionalStorageModels, viewSchemaName);
					if (result.WasHandled) 
					{
						return result.Result;
					}
				}

			}

			return null;
		}
	}
}

