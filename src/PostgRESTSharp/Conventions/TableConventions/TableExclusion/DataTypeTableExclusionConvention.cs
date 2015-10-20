using System;

namespace PostgRESTSharp.Conventions
{
    public class DataTypeTableExclusionConvention : ITableExclusionConvention, IExplicitTableConvention
    {
        public bool IsExcluded()
        {
            return true;
        }

        public string DatabaseName => "halo";

        public string SchemaName => "public";

        public string TableName => "data_type_enum";

    }
}