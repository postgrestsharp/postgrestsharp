using PostgRESTSharp.Conventions;
using System.Collections.Generic;

namespace PostgRESTSharp
{
    public class ViewMetaModelBuilder : IViewMetaModelBuilder
    {
        private IConventionResolver conventionResolver;

        public ViewMetaModelBuilder(IConventionResolver conventionResolver)
        {
            this.conventionResolver = conventionResolver;
        }

        public IViewMetaModel BuildModel(ITableMetaModel storageModel, IEnumerable<ITableMetaModel> additionalStorageModels, string viewSchemaName)
        {
            var viewBuilder = this.conventionResolver.ResolveTableConvention<IViewBuildingConvention>(storageModel);
            return viewBuilder.BuildModel(storageModel, additionalStorageModels, viewSchemaName);
        }
    }
}