using System.Collections.Generic;

namespace PostgRESTSharp.Conventions.ViewConventions.EntityRemoval
{
    public class EntityRemovalForViewWithLegalEntity : EntityRemovalForViewWithColumnNameContainsConvention
    {
        public override IEnumerable<string> GetKeywordsToSearch()
        {
            yield return "legalEntityId";
        }
    }
}
