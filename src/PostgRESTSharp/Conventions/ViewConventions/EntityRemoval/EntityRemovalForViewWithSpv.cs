using System;
using System.Collections.Generic;
using System.Linq;
using PostgRESTSharp.Conventions.ViewConventions.EntityRemoval;

namespace PostgRESTSharp.Conventions.ViewConventions.ColumnRemoval
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
