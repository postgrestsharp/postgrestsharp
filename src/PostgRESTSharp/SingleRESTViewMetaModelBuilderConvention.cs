using System;
using System.Collections.Generic;

namespace PostgrestSharp
{
	public class SingleRESTViewMetaModelBuilderConvention : IViewMetaModelBuilderConvention
	{
		public SingleRESTViewMetaModelBuilderConvention ()
		{
			this.Level = ViewModelBuilderConventionLevel.Convention;
			this.ConventionType = ViewModelBuilderConventionType.Inclusion;
		}

		public ViewMetaModelBuilderResult BuildModel (IMetaModel storageModel, IEnumerable<IMetaModel> additionalStorageModels, string viewSchemaName)
		{
			if (!storageModel.TableName.Contains ("$")) 
			{
				var model = new ViewMetaModel (storageModel.DatabaseName, viewSchemaName, storageModel.ModelNameCamelCased);
				// there is only one table involved
				model.SetPrimaryTableSource(storageModel);

				// add the columns
				foreach (var col in storageModel.Columns) 
				{
					model.AddColumn (col, storageModel);
				}

				return new ViewMetaModelBuilderResult (true, model);
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

