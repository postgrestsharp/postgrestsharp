using StructureMap;
using Synoptic;
using System;

namespace PostgRESTSharp.Generator
{
	public class StructureMapCommandDependencyResolver : IDependencyResolver
	{
		private readonly IContainer _container;

		public StructureMapCommandDependencyResolver(IContainer container)
		{
			_container = container;
		}

		public object Resolve(Type serviceType)
		{
			return _container.GetInstance(serviceType);
		}
	}
}

