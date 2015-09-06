namespace PostgRESTSharp
{
    public class MetaModelViewColumn
    {
        public MetaModelViewColumn(string columnName, string storageDataType, long storageDataTypeLength, string modelDataType, int order, bool isKeyColumn, bool isPrimaryKeyColumn, IMetaModel table, MetaModelColumn tableColumn)
        {
            this.ColumnName = columnName;
            this.StorageDataType = storageDataType;
            this.StorageDataTypeLength = storageDataTypeLength;
            this.ModelDataType = modelDataType;
            this.Order = order;
            this.IsKeyColumn = isKeyColumn;
            this.Table = table;
            this.TableColumn = tableColumn;
			this.IsPrimaryKeyColumn = isPrimaryKeyColumn;
        }

        public string ColumnName { get; protected set; }

        public string ModelDataType { get; protected set; }

        public string StorageDataType { get; protected set; }

        public long StorageDataTypeLength { get; protected set; }

        public int Order { get; protected set; }

        public bool IsKeyColumn { get; protected set; }

		public bool IsPrimaryKeyColumn { get; protected set; }

        public ViewMetaModelRelation RelatedView { get; protected set; }

        public MetaModelColumn TableColumn { get; protected set; }

        public IMetaModel Table { get; protected set; }

        public void AddRelation(ViewMetaModelRelation viewRelation)
        {
            this.RelatedView = viewRelation;
        }
    }
}