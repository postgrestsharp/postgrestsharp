using System.Collections.Generic;
using System.Linq;

namespace PostgRESTSharp.Conventions.ViewConventions.ViewFiltering
{

    public class ColumnRemovalForViewWithIsActiveColumn : IColumnRemovalConvention, IImplicitViewConvention
    {

        public bool IsMatch(IViewMetaModel metaModel)
        {
            return metaModel.Columns.FirstOrDefault(x => x.ColumnName.ToLower() == "isactive") != null;
        }
        
        public string ColumnToRemove()
        {
            return "isActive";
        }
    }

}