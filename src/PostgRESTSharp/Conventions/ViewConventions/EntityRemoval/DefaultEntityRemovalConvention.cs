using System.Collections.Generic;

namespace PostgRESTSharp.Conventions.ViewConventions.EntityRemoval
{
    public class DefaultEntityRemovalConvention : IEntityRemovalConvention, IDefaultViewConvention
    {
        public IEnumerable<string> ColumnsToRemove(IViewMetaModel metaModel)
        {
            yield break;
        }
    }
}
