using System.Collections.Generic;
using System.Linq;

namespace PostgRESTSharp.Conventions.ViewConventions.ViewFiltering
{
    public class ViewFilteringForViewWithRowLevelSecurity : IViewFilteringConvention, IImplicitViewConvention
    {
        public bool IsMatch(IViewMetaModel metaModel)
        {
            return metaModel.Columns.FirstOrDefault(x => x.ColumnName.ToLower() == "originationsourceaccess") != null;
        }

        public IEnumerable<IViewFilterElement> FilterElements()
        {
            return new List<IViewFilterElement> { new ViewFilterElement("current_setting('user_vars.user_id')", "=", "Any({0}.originationsource_access)") };
        }
    }
}