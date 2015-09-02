using System.Collections.Generic;
using System.Linq;

namespace PostgRESTSharp
{
    public class ViewMetaModelProcessor : IViewMetaModelProcessor
    {
        private IViewMetaModelBuilder viewBuilder;

        public ViewMetaModelProcessor(IViewMetaModelBuilder builder)
        {
            this.viewBuilder = builder;
        }

        public IEnumerable<IViewMetaModel> ProcessModels(IEnumerable<IMetaModel> models, int viewSchemaVersion)
        {
            List<IViewMetaModel> views = new List<IViewMetaModel>();
            foreach (var tableModel in models.Where(x => x.MetaModelType == MetaModelTypeEnum.Table))
            {
                var result = viewBuilder.BuildModel(tableModel, models.Where(x => x.DatabaseName != tableModel.DatabaseName && x.SchemaName != tableModel.SchemaName && x.TableName != tableModel.TableName), viewSchemaVersion.ToString());
                if (result != null)
                {
                    views.Add(result);
                }
            }

            // go back any reprocess the view relations

            return views;
        }
    }
}