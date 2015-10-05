using System.Collections.Generic;
using System.Linq;

namespace PostgRESTSharp.Conventions.ViewConventions.ViewFiltering
{

    public class ViewFilteringForViewWithIsActiveColumn : IViewFilteringConvention, IImplicitViewConvention
    {

        public bool IsMatch(IViewMetaModel metaModel)
        {
            return metaModel.Columns.FirstOrDefault(x => x.ColumnName.ToLower() == "isactive") != null;
        }

        public IEnumerable<IViewFilterElement> FilterElements()
        {
            return new List<IViewFilterElement>{ new ViewFilterElement("{0}.is_active","", "") };
        }
    }

}