using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Conventions.ViewConventions.EntityRemoval
{
    public interface IEntityRemovalConvention : IViewConvention
    {
        IEnumerable<string> ColumnsToRemove(IViewMetaModel metaModel);
    }
}
