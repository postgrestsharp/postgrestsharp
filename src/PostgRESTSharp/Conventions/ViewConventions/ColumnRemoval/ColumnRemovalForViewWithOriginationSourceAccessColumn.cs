using System.Collections.Generic;
using System.Linq;

namespace PostgRESTSharp.Conventions.ViewConventions.ViewFiltering
{

    public class ColumnRemovalForViewWithOriginationSourceAccessColumn : IColumnRemovalConvention, IImplicitViewConvention
    {

        public bool IsMatch(IViewMetaModel metaModel)
        {
            return metaModel.Columns.FirstOrDefault(x => x.ColumnName.ToLower() == "originationsourceaccess") != null;
        }
        
        public string ColumnToRemove()
        {
            return "originationsourceAccess";
        }
    }

}