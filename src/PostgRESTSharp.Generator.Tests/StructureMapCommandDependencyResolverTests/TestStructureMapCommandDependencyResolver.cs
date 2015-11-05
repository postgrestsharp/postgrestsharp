using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using StructureMap;

namespace PostgRESTSharp.Generator.Tests.StructureMapCommandDependencyResolverTests
{
    [TestFixture]
    public class TestStructureMapCommandDependencyResolver
    {
        private readonly Mutex blocker = new Mutex(false, "TestStructureMapCommandDependencyResolver");

        [Test]
        public void Constructor_ShouldResolveAnInstance_GivenAnInstanceTypeToResolve()
        {
            try
            {
                blocker.WaitOne(TimeSpan.FromSeconds(10D), false);

                var container = Substitute.For<IContainer>();
                var resolver = new StructureMapCommandDependencyResolver(container);

                var serviceType = typeof(object);
                resolver.Resolve(serviceType);

                container.Received(1).GetInstance(serviceType);

            }
            finally
            {
                blocker.ReleaseMutex();
            }
        }
    }
}
