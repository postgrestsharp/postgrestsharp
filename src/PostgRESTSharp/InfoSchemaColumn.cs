using System;

namespace PostgRESTSharp
{
    public class InfoSchemaColumn
    {
        public string TableCatalog { get; set; }

        public string TableSchema { get; set; }

        public string TableName { get; set; }

        public string ColumnName { get; set; }

        public int OrdinalPosition { get; set; }

        public string ColumnDefault { get; set; }

        public bool IsNullable { get; set; }

        public string DataType { get; set; }

        public Int64? CharacterMaximumLength { get; set; }

        public Int64? NumericPrecision { get; set; }

        public Int64? NumericScale { get; set; }

        public Int64? DatetimePrecision { get; set; }

        public bool IsPrimaryKeyColumn { get; set; }

        public bool IsAutoIncrementColumn { get; set; }

        public bool IsUpdatable { get; set; }

        public bool IsUnique { get; set; }

        public string ColumnComment { get; set; }
    }
}