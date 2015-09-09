using System;
using System.Collections.Generic;

namespace PostgRESTSharp.Conventions
{
	public interface IViewBuildingConvention : ITableConvention
	{
		void AddView(IList<IViewMetaModel> viewsCollection, Func<IViewMetaModel> viewBuildingFunc);
	}
}

