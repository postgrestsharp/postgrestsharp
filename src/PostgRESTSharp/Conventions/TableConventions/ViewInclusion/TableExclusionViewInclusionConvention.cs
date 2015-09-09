using System;
using System.Collections.Generic;

namespace PostgRESTSharp.Conventions
{
	public class TableExclusionViewInclusionConvention : AbstractExplicitTableConvention, IViewInclusionConvention
	{
		public TableExclusionViewInclusionConvention(string database, string schemaName, string tableName)
			: base(database, schemaName, tableName)
		{
		}

		public void AddView (IList<IViewMetaModel> viewsCollection, Func<IViewMetaModel> viewBuildingFunc)
		{
			// do nothing we don't add a view if matched
		}
	}
}

