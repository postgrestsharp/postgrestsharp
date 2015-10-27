using System.Collections.Generic;

namespace PostgRESTSharp.Conventions.ViewConventions.EntityRemoval
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
