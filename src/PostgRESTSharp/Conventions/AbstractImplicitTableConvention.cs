using System;

namespace PostgRESTSharp.Conventions
{
	public abstract class AbstractImplicitTableConvention : IImplicitTableConvention
	{
		public AbstractImplicitTableConvention ()
		{
		}

		public abstract bool IsMatch (IMetaModel metaModel);
	}
}

