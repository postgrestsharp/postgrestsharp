using System;
using System.Collections.Generic;
using System.Linq;
using PostgRESTSharp.Conventions.ViewConventions.EntityRemoval;

namespace PostgRESTSharp.Conventions.ViewConventions.ColumnRemoval
{
    public class EntityRemovalForViewWithCategory : EntityRemovalForViewWithColumnNameContainsConvention
    {
        public override IEnumerable<string> GetKeywordsToSearch()
        {
            yield return "categoryId";
            yield return "categoryDescription";
        }
    }
}
