using System;
using System.Collections.Generic;

namespace PostgRESTSharp.Conventions
{
	public class TableExclusionViewBuildingConvention : AbstractExplicitTableConvention, IViewBuildingConvention
	{
		public TableExclusionViewBuildingConvention (string database, string schemaName, string tableName)
			: base(database, schemaName, tableName)
		{
		}

		public void AddView (IList<IViewMetaModel> viewsCollection, Func<IViewMetaModel> viewBuildingFunc)
		{
			throw new NotImplementedException ();
		}
	}
}

