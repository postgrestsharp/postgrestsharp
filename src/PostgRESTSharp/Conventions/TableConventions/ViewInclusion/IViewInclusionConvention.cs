using System;
using System.Collections.Generic;

namespace PostgRESTSharp.Conventions
{
	public interface IViewInclusionConvention : ITableConvention
	{
		void AddView(IList<IViewMetaModel> viewsCollection, Func<IViewMetaModel> viewBuildingFunc);
	}
}

