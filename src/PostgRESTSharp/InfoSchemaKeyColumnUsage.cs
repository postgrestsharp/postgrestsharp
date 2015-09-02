using System;

namespace PostgRESTSharp
{
    public class InfoSchemaKeyColumnUsage
    {
        public string ConstraintCatalog { get; set; }

        public string ConstraintSchema { get; set; }

        public string ConstraintName { get; set; }

        public string TableCatalog { get; set; }

        public string TableSchema { get; set; }

        public string TableName { get; set; }

        public string ColumnName { get; set; }

        public string ConstraintType { get; set; }

        public int OrdinalPosition { get; set; }

        public Int64 PositionInUniqueConstraint { get; set; }
    }
}