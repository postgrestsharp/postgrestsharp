using System.Collections.Generic;

namespace PostgRESTSharp
{
    public interface IViewMetaModelProcessor
    {
        IEnumerable<IViewMetaModel> ProcessModels(IEnumerable<IMetaModel> models, int viewSchemaVersion);
    }
}