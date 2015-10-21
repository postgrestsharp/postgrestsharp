using System;

namespace PostgRESTSharp.Conventions
{
    public class UsageTypeTableExclusionConvention : ITableExclusionConvention, IExplicitTableConvention
    {
        public bool IsExcluded()
        {
            return true;
        }

        public string DatabaseName
        {
            get { return "halo"; }
        }

        public string SchemaName
        {
            get { return "public"; }
        }

        public string TableName
        {
            get { return "usage_type_enum"; }
        }
    }
}
