using System.Collections.Generic;

namespace PostgRESTSharp.Conventions.ViewConventions.EntityRemoval
{
    public class EntityRemovalForViewWithSpv : EntityRemovalForViewWithColumnNameContainsConvention
    {
        public override IEnumerable<string> GetKeywordsToSearch()
        {
            yield return "spvId";
            yield return "spvDescription";
            yield return "spvReportDescription";
            yield return "spvCompanyId";
        }
    }
}
