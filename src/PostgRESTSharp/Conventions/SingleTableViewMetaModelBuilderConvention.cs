using PostgRESTSharp.Text;
using System.Collections.Generic;

namespace PostgRESTSharp.Conventions
{
	public class SingleTableViewMetaModelBuilderConvention : IViewMetaModelBuilderConvention, IDefaultTableConvention
    {
        private ITextUtility textUtility;

		public SingleTableViewMetaModelBuilderConvention(ITextUtility textUtility)
        {
            this.Level = ViewModelBuilderConventionLevel.Convention;
            this.ConventionType = ViewModelBuilderConventionType.Inclusion;
            this.textUtility = textUtility;
        }

        public ViewMetaModelBuilderResult BuildModel(IMetaModel storageModel, IEnumerable<IMetaModel> additionalStorageModels, string viewSchemaName)
        {
            var model = new ViewMetaModel(storageModel.DatabaseName, viewSchemaName, storageModel.ModelNameCamelCased, 
                this.textUtility.ToCapitalCase(storageModel.TableName),
                this.textUtility.ToPluralCapitalCase(storageModel.TableName));
            // there is only one table involved
            model.SetPrimaryTableSource(storageModel);

            // add the columns
            foreach (var col in storageModel.Columns)
            {
                model.AddColumn(col, storageModel);
            }

            return new ViewMetaModelBuilderResult(true, model);
        }
    }
}