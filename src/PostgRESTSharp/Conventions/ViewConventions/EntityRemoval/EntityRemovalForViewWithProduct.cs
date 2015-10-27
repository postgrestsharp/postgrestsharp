using System;
using System.Collections.Generic;
using System.Linq;
using PostgRESTSharp.Conventions.ViewConventions.EntityRemoval;

namespace PostgRESTSharp.Conventions.ViewConventions.ColumnRemoval
{
    public class EntityRemovalForViewWithProduct : EntityRemovalForViewWithColumnNameContainsConvention
    {
        public override IEnumerable<string> GetKeywordsToSearch()
        {
            yield return "productId";
            yield return "productDescription";
        }
    }
}
