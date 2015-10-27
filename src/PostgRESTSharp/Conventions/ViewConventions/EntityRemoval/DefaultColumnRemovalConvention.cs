using System;
using System.Collections.Generic;
using PostgRESTSharp.Conventions.ViewConventions.EntityRemoval;

namespace PostgRESTSharp.Conventions.ViewConventions.ColumnRemoval
{
    public class DefaultEntityRemovalConvention : IEntityRemovalConvention, IDefaultViewConvention
    {
        public IEnumerable<string> ColumnsToRemove(IViewMetaModel metaModel)
        {
            yield break;
        }
    }
}
