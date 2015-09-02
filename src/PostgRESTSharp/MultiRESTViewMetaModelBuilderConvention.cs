using System;
using System.Collections.Generic;

namespace PostgRESTSharp
{
	public class MultiRESTViewMetaModelBuilderConvention : IViewMetaModelBuilderConvention
	{
		public MultiRESTViewMetaModelBuilderConvention()
		{
			this.Level = ViewModelBuilderConventionLevel.Convention;
			this.ConventionType = ViewModelBuilderConventionType.Inclusion;
		}

		public ViewMetaModelBuilderResult BuildModel (IMetaModel storageModel, IEnumerable<IMetaModel> additionalStorageModels, string viewSchemaName)
		{
			if (storageModel.TableName.Contains ("$")) 
			{
				var model = new ViewMetaModel (storageModel.DatabaseName, viewSchemaName, storageModel.ModelNameCamelCased);
                // there are multiple tables involved

                return new ViewMetaModelBuilderResult(true, model);
            } 
			else 
			{
				return new ViewMetaModelBuilderResult (false, null);
			}
		}

		public ViewModelBuilderConventionType ConventionType { get; protected set; }

		public ViewModelBuilderConventionLevel Level  { get; protected set; }

	}
}

