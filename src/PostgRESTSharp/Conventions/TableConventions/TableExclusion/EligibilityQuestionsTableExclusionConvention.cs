using System;

namespace PostgRESTSharp.Conventions
{
    public class EligibilityQuestionsTableExclusionConvention : ITableExclusionConvention, IImplicitTableConvention
    {
        public bool IsMatch(ITableMetaModel metaModel)
        {
            if (metaModel.SchemaName.Equals("public", StringComparison.InvariantCultureIgnoreCase)
                && metaModel.DatabaseName.Equals("halo", StringComparison.InvariantCultureIgnoreCase)
                && metaModel.TableName.ToLower().Contains("eligibility_"))
            {
                return true;
            }
            return false;
        }
        public bool IsExcluded()
        {
            return true;
        }

    }

}