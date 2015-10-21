using System;

namespace PostgRESTSharp.Conventions
{
    public class DataTypeTableExclusionConvention : ITableExclusionConvention, IExplicitTableConvention
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
            get { return "data_type_enum"; }
        }
    }
}
