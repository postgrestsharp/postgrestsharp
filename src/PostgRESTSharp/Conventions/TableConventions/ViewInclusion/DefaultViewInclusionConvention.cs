using System;
using System.Collections.Generic;

namespace PostgRESTSharp.Conventions
{
	public class DefaultViewInclusionConvention : IViewInclusionConvention, IDefaultTableConvention
	{
		public void AddView (IList<IViewMetaModel> viewsCollection, Func<IViewMetaModel> viewBuildingFunc)
		{
			viewsCollection.Add (viewBuildingFunc());
		}
	}
}

