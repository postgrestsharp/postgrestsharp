using System;
using System.Linq;
using System.Collections.Generic;

namespace PostgRESTSharp
{
	public class MetaModelViewColumn
	{
		public MetaModelViewColumn (string columnName, string storageDataType, long storageDataTypeLength, int order, bool isKeyColumn, string alias, IEnumerable<ViewMetaModelRelation> relatedViews)
		{
			this.ColumnName = columnName;
			this.StorageDataType = storageDataType;
			this.StorageDataTypeLength = storageDataTypeLength;
			this.Order = order;
			this.IsKeyColumn = isKeyColumn;
			this.Alias = alias;
			this.RelatedViews = new List<ViewMetaModelRelation>(relatedViews);
		}

		public string ColumnName { get; protected set; }

        public string StorageDataType { get; protected set; }

		public long StorageDataTypeLength { get; protected set; }

		public int Order { get; protected set; }

		public bool IsKeyColumn { get; protected set; }

		public string Alias { get; protected set; }

		public IEnumerable<ViewMetaModelRelation> RelatedViews { get; protected set; }
	}
}

