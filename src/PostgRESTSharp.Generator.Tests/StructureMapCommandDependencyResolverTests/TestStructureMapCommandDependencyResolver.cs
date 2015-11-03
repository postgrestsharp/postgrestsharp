using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using StructureMap;

namespace PostgRESTSharp.Generator.Tests.StructureMapCommandDependencyResolverTests
{
    [TestFixture]
    public class TestStructureMapCommandDependencyResolver
    {
        private static readonly object containerLock = new object();

        [Test]
        public void Constructor_ShouldResolveAnInstance_GivenAnInstanceTypeToResolve()
        {
            lock (containerLock)
            {
                var container = Substitute.For<IContainer>();
                var resolver = new StructureMapCommandDependencyResolver(container);

                var serviceType = typeof(object);
                resolver.Resolve(serviceType);

                container.Received(1).GetInstance(serviceType);
            }
        }
    }
}
