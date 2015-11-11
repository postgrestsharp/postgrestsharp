using StructureMap;
using Synoptic;
using System;

namespace PostgRESTSharp.Generator
{
    public class StructureMapCommandDependencyResolver : IDependencyResolver
    {
        private readonly IContainer container;

        public StructureMapCommandDependencyResolver(IContainer container)
        {
            this.container = container;
        }

        public virtual object Resolve(Type serviceType)
        {
            return container.GetInstance(serviceType);
        }
    }
}
