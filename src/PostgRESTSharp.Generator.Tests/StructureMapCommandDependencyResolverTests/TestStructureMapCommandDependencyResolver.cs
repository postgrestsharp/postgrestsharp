using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using PostgRESTSharp.Generator.Tests.StructureMapCommandDependencyResolverTests.Mock;
using StructureMap;

namespace PostgRESTSharp.Generator.Tests.StructureMapCommandDependencyResolverTests
{
    [TestFixture]
    public class TestStructureMapCommandDependencyResolver
    {
        [Test]
        public void Constructor_ShouldResolveAnInstance_GivenAnInstanceTypeToResolve()
        {
            var container = Substitute.For<IContainer>();
            var resolver = new StructureMapCommandDependencyResolverExposed(container);

            var serviceType = typeof(object);
            resolver.Resolve(serviceType);

            Assert.That(resolver.WasResolvedCalled);
        }
    }
}
