using System.Collections.Generic;
using System.Linq;

namespace PostgRESTSharp.Conventions.ViewConventions.ViewFiltering
{

    public class ColumnRemovalForViewWithOriginationSourceOwnerColumn : IColumnRemovalConvention, IImplicitViewConvention
    {

        public bool IsMatch(IViewMetaModel metaModel)
        {
            return metaModel.Columns.FirstOrDefault(x => x.ColumnName.ToLower() == "originationsourceowner") != null;
        }
        
        public string ColumnToRemove()
        {
            return "originationsourceOwner";
        }
    }

}