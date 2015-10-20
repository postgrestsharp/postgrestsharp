using System;

namespace PostgRESTSharp.Conventions
{
    public class DeclationQuestionsTableExclusionConvention : ITableExclusionConvention, IImplicitTableConvention
    {
        public bool IsMatch(ITableMetaModel metaModel)
        {
            if (metaModel.SchemaName.Equals("public", StringComparison.InvariantCultureIgnoreCase)
                && metaModel.DatabaseName.Equals("halo", StringComparison.InvariantCultureIgnoreCase)
                && metaModel.TableName.ToLower().Contains("declaration_"))
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