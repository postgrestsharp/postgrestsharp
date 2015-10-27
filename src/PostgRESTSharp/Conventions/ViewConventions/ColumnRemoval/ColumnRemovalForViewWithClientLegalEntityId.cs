using System;
using System.Linq;

namespace PostgRESTSharp.Conventions.ViewConventions.ColumnRemoval
{
    public class ColumnRemovalForViewWithClientLegalEntityId : IColumnRemovalConvention, IImplicitViewConvention
    {
        public bool IsMatch(IViewMetaModel metaModel)
        {
            return metaModel.Columns.Any(a => a.ColumnName.IndexOf(ColumnToRemove(), StringComparison.OrdinalIgnoreCase) >= 0);
        }
        
        public string ColumnToRemove()
        {
            return "clientlegalentityId";
        }
    }
}
