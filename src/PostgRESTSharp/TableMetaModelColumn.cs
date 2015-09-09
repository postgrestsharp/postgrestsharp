using System;
using System.Collections.Generic;
using System.Linq;

namespace PostgRESTSharp
{
    public class TableMetaModelColumn
    {
        private string[] readonlyColumns = new string[] { "created", "modified" };

        public TableMetaModelColumn(string columnName, bool isPrimaryKeyColumn, bool isAutoIncrementColumn, bool isNullable, bool isUpdateable, bool isUnique, int order,
            string storageDataType, Int64 storageDataTypeLength, string fieldName, string fieldNameCamelCased, string fieldDataType)
        {
            this.ColumnName = columnName;
            this.IsPrimaryKeyColumn = isPrimaryKeyColumn;
            this.IsAutoIncrementColumn = isAutoIncrementColumn;
            this.IsNullable = isNullable;
            this.IsUpdateable = readonlyColumns.Any(x => x == columnName) ? false : isUpdateable;
            this.IsUnique = isUnique;
            this.Order = order;
            this.StorageDataType = storageDataType;
            this.StorageDataTypeLength = storageDataTypeLength;
            this.FieldName = fieldName;
            this.FieldNameCamelCased = fieldNameCamelCased;
            this.FieldDataType = fieldDataType;
        }

        public string ColumnName { get; protected set; }

        public bool IsPrimaryKeyColumn { get; protected set; }

        public bool IsAutoIncrementColumn { get; protected set; }

        public bool IsNullable { get; protected set; }

        public bool IsUpdateable { get; protected set; }

        public bool IsUnique { get; protected set; }

        public int Order { get; protected set; }

        public string StorageDataType { get; protected set; }

        public Int64 StorageDataTypeLength { get; protected set; }

        public string FieldName { get; protected set; }

        public string FieldNameCamelCased { get; protected set; }

        public string FieldDataType { get; protected set; }
    }
}