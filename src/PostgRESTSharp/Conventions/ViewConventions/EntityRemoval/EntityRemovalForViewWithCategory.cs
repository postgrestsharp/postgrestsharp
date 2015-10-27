using System.Collections.Generic;

namespace PostgRESTSharp.Conventions.ViewConventions.EntityRemoval
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
