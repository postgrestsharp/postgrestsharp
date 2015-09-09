using System.Collections.Generic;

namespace PostgRESTSharp
{
    public interface IViewMetaModelProcessor
    {
        IEnumerable<IViewMetaModel> ProcessModels(IEnumerable<ITableMetaModel> models, int viewSchemaVersion);
    }
}