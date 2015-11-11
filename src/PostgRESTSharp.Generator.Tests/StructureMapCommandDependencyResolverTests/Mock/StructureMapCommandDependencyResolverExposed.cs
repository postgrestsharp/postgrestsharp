using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;

namespace PostgRESTSharp.Generator.Tests.StructureMapCommandDependencyResolverTests.Mock
{
    public class StructureMapCommandDependencyResolverExposed : StructureMapCommandDependencyResolver
    {
        public bool WasResolvedCalled { get; set; }

        public StructureMapCommandDependencyResolverExposed(IContainer container) : base(container)
        {
        }

        public override object Resolve(Type serviceType)
        {
            var item = base.Resolve(serviceType);
            WasResolvedCalled = true;
            return item;
        }
    }
}
