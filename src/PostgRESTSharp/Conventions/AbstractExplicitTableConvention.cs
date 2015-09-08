using System;

namespace PostgRESTSharp.Conventions
{
	public abstract class AbstractExplicitTableConvention : IExplicitTableConvention
	{
		public AbstractExplicitTableConvention (string database, string schemaName, string tableName)
		{
			this.DatabaseName = database;
			this.SchemaName = schemaName;
			this.TableName = tableName;
		}

		public string DatabaseName { get; protected set; }

		public string SchemaName { get; protected set; }

		public string TableName { get; protected set; }
	}
}

