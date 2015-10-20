namespace PostgRESTSharp
{
    public class ViewMetaModelColumn
    {
        public ViewMetaModelColumn(string columnName, string columnAlias, string storageDataType, 
            long storageDataTypeLength, string modelDataType, 
            int order, bool isKeyColumn, bool isPrimaryKeyColumn,
            bool isUniqueColumn, bool isComplexType, IJoinRelationModel joinRelationModel,
            ITableMetaModel table, TableMetaModelColumn tableColumn)
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
            this.ColumnAlias = columnAlias;
            this.IsComplexType = isComplexType;
            this.JoinRelationModel = joinRelationModel;
        }

        public string ColumnName { get; protected set; }

        public string ColumnAlias { get; protected set; }

        public string ModelDataType { get; protected set; }

        public string StorageDataType { get; protected set; }

        public long StorageDataTypeLength { get; protected set; }

        public int Order { get; protected set; }

        public bool IsKeyColumn { get; protected set; }

		public bool IsPrimaryKeyColumn { get; protected set; }

        public bool IsUniqueColumn { get; protected set; }

        public string Description { get; protected set; }

        public bool IsHidden { get; protected set; }

        public bool IsComplexType { get; protected set; }

        public IJoinRelationModel JoinRelationModel { get; protected set; } 

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