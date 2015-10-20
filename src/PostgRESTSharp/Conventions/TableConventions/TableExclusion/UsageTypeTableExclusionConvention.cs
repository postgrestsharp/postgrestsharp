using System;

namespace PostgRESTSharp.Conventions
{
    public class UsageTypeTableExclusionConvention : ITableExclusionConvention, IExplicitTableConvention
    {
        public bool IsExcluded()
        {
            return true;
        }

        public string DatabaseName => "halo";

        public string SchemaName => "public";

        public string TableName => "usage_type_enum";

    }
}