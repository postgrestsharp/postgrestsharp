using PostgRESTSharp.Text;
using System.Collections.Generic;

namespace PostgRESTSharp
{
    public class SingleRESTViewMetaModelBuilderConvention : IViewMetaModelBuilderConvention
    {
        private ITextUtility textUtility;

        public SingleRESTViewMetaModelBuilderConvention(ITextUtility textUtility)
        {
            this.Level = ViewModelBuilderConventionLevel.Convention;
            this.ConventionType = ViewModelBuilderConventionType.Inclusion;
            this.textUtility = textUtility;
        }

        public ViewMetaModelBuilderResult BuildModel(IMetaModel storageModel, IEnumerable<IMetaModel> additionalStorageModels, string viewSchemaName)
        {
            if (!storageModel.TableName.Contains("$"))
            {
                var model = new ViewMetaModel(storageModel.DatabaseName, viewSchemaName, storageModel.ModelNameCamelCased, this.textUtility.ToCapitalCase(storageModel.TableName));
                // there is only one table involved
                model.SetPrimaryTableSource(storageModel);

                // add the columns
                foreach (var col in storageModel.Columns)
                {
                    model.AddColumn(col, storageModel);
                }

                return new ViewMetaModelBuilderResult(true, model);
            }
            else
            {
                return new ViewMetaModelBuilderResult(false, null);
            }
        }

        public ViewModelBuilderConventionType ConventionType { get; protected set; }

        public ViewModelBuilderConventionLevel Level { get; protected set; }
    }
}