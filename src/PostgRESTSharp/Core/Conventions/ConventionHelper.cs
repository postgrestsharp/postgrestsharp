using System.Collections.Generic;
using System.Linq;

namespace PostgRESTSharp.Core.Conventions
{
    public static class ConventionHelper
    {
        public static IEnumerable<DuplicateItem> GetListOfDuplicateModelColumns(IViewMetaModel metaModel)
        {
            return metaModel.Columns.GroupBy(x => x.ColumnName)
                .Select(group => new DuplicateItem { ColumnName = @group.Key, Count = @group.ToList().Count })
                .Where(x => x.Count > 1).ToList();
        }

        public static IEnumerable<DuplicateItem> GetListOfDuplicateModelColumnsForSpecificName(IViewMetaModel metaModel, string columnName)
        {
            return GetListOfDuplicateModelColumns(metaModel)
                .Where(x => x.ColumnName.ToLower().Equals(columnName.ToLower())).ToList();
        }

    }
    
}