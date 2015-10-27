using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Conventions.ViewConventions.EntityRemoval
{
    public abstract class EntityRemovalForViewWithColumnNameContainsConvention : IEntityRemovalConvention, IImplicitViewConvention
    {
        public abstract IEnumerable<string> GetKeywordsToSearch();

        public bool IsMatch(IViewMetaModel metaModel)
        {
            return metaModel.Columns.Any(IsColumnMatch);
        }

        public virtual IEnumerable<string> ColumnsToRemove(IViewMetaModel metaModel)
        {
            return metaModel.Columns.Where(IsColumnMatch).Select(a => a.ColumnAlias);
        }

        protected virtual bool IsColumnMatch(ViewMetaModelColumn column)
        {
            return GetKeywordsToSearch().Any(a => column.ColumnAlias.IndexOf(a, StringComparison.OrdinalIgnoreCase) >= 0);}
    }
}
