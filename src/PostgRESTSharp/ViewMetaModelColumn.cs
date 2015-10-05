namespace PostgRESTSharp
{
    public class ViewMetaModelColumn
    {
        public ViewMetaModelColumn(string columnName, string storageDataType, long storageDataTypeLength, string modelDataType, int order, bool isKeyColumn, bool isPrimaryKeyColumn, bool isUniqueColumn, ITableMetaModel table, TableMetaModelColumn tableColumn)
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
            this.IsUniqueColumn = isUniqueColumn;
            this.IsHidden = false;
        }

        public string ColumnName { get; protected set; }

        public string ModelDataType { get; protected set; }

        public string StorageDataType { get; protected set; }

        public long StorageDataTypeLength { get; protected set; }

        public int Order { get; protected set; }

        public bool IsKeyColumn { get; protected set; }

		public bool IsPrimaryKeyColumn { get; protected set; }

        public bool IsUniqueColumn { get; protected set; }

        public string Description { get; protected set; }

        public bool IsHidden { get; protected set; }

        public ViewMetaModelRelation RelatedView { get; protected set; }

        public TableMetaModelColumn TableColumn { get; protected set; }

        public ITableMetaModel Table { get; protected set; }

        public void AddRelation(ViewMetaModelRelation viewRelation)
        {
            this.RelatedView = viewRelation;
        }

        public void SetColumnToHidden()
        {
            this.IsHidden = true;
        }

        public void SetColumnToVisisble()
        {
            this.IsHidden = false;
        }

    }
}