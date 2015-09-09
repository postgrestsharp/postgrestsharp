using System;
using System.Collections.Generic;

namespace PostgRESTSharp.Conventions
{
	public class DefaultViewBuildingConvention : IViewBuildingConvention, IDefaultTableConvention
	{
		public DefaultViewBuildingConvention ()
		{
		}

		public void AddView (IList<IViewMetaModel> viewsCollection, Func<IViewMetaModel> viewBuildingFunc)
		{
			viewsCollection.Add (viewBuildingFunc());
		}
	}
}

