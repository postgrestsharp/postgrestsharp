using System;

namespace PostgRESTSharp.Conventions
{
	public class TableExclusionConvention : AbstractExplicitTableConvention
	{
		public TableExclusionConvention (string database, string schemaName, string tableName)
			: base(database, schemaName, tableName)
		{
		}
	}
}

