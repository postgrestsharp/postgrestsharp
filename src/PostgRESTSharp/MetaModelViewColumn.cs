using System.Collections.Generic;

namespace PostgRESTSharp
{
    public class MetaModelViewColumn
    {
        public MetaModelViewColumn(string columnName, string storageDataType, long storageDataTypeLength, int order, bool isKeyColumn, IMetaModel table, MetaModelColumn tableColumn, IEnumerable<ViewMetaModelRelation> relatedViews)
        {
            this.ColumnName = columnName;
            this.StorageDataType = storageDataType;
            this.StorageDataTypeLength = storageDataTypeLength;
            this.Order = order;
            this.IsKeyColumn = isKeyColumn;
            this.RelatedViews = new List<ViewMetaModelRelation>(relatedViews);
            this.Table = table;
            this.TableColumn = tableColumn;
        }

        public string ColumnName { get; protected set; }

        public string StorageDataType { get; protected set; }

        public long StorageDataTypeLength { get; protected set; }

        public int Order { get; protected set; }

        public bool IsKeyColumn { get; protected set; }

        public IEnumerable<ViewMetaModelRelation> RelatedViews { get; protected set; }

        public MetaModelColumn TableColumn { get; protected set; }

        public IMetaModel Table { get; protected set; }
    }
}