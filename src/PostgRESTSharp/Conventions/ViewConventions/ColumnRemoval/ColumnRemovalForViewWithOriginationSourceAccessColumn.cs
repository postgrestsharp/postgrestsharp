using System;
using System.Linq;

namespace PostgRESTSharp.Conventions.ViewConventions.ColumnRemoval
{
    public class ColumnRemovalForViewWithOriginationSourceAccessColumn : IColumnRemovalConvention, IImplicitViewConvention
    {
        public bool IsMatch(IViewMetaModel metaModel)
        {
            return metaModel.Columns.Any(x => x.ColumnName.Equals(ColumnToRemove(), StringComparison.OrdinalIgnoreCase));
        }
        
        public string ColumnToRemove()
        {
            return "originationsourceAccess";
        }
    }
}
