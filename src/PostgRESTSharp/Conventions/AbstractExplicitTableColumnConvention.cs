using System;

namespace PostgRESTSharp.Conventions
{
	public abstract class AbstractExplicitTableColumnConvention : AbstractExplicitTableConvention, IExplicitTableColumnConvention
	{
		public AbstractExplicitTableColumnConvention (string database, string schemaName, string tableName, string columnName)
			: base(database, schemaName, tableName)
		{
			this.ColumnName = columnName;
		}

		public string ColumnName { get; protected set; }
	}
}

