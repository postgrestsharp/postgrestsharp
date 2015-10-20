using System;

namespace PostgRESTSharp.Conventions
{
    public class CalculatorQuestionsTableExclusionConvention : ITableExclusionConvention, IImplicitTableConvention
    {
        public bool IsMatch(ITableMetaModel metaModel)
        {
            if (metaModel.SchemaName.Equals("public", StringComparison.InvariantCultureIgnoreCase)
                && metaModel.DatabaseName.Equals("halo", StringComparison.InvariantCultureIgnoreCase)
                && metaModel.TableName.ToLower().Contains("calculator_"))
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