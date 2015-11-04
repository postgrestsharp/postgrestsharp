using System;
using NUnit.Framework;
using PostgRESTSharp.Commands.GenerateRAML;
using PostgRESTSharp.Commands.GenerateRAML.Maps;
using PostgRESTSharp.Configuration;
using PostgRESTSharp.Conventions;
using PostgRESTSharp.Data;
using PostgRESTSharp.Generator.Tests.GeneratorRegistryTests.Mock;
using PostgRESTSharp.Pgsql;
using StructureMap;

namespace PostgRESTSharp.Generator.Tests.GeneratorRegistryTests
{
    [TestFixture]
    public class TestGeneratorRegistry
    {
        [TestCase(typeof(IConvention), new[] { typeof(DefaultTableExclusionConvention), typeof(DefaultViewInclusionConvention)})]
        [TestCase(typeof(IMapping), new[] { typeof(MethodMapping), typeof(ParamterMapping)})]
        public void Constructor_ShouldAddAllGivenTypes_GivenInterfaceWithMultipleImplementors(Type interfaceType, Type[] concreteTypes)
        {
            var container = new Container(new GeneratorRegistry());
            var instance = container.GetAllInstances(interfaceType);

            Assert.That(instance, Is.Not.Empty);

            foreach (var item in concreteTypes)
            {
                Assert.That(instance, Has.Some.InstanceOf(item));
            }
        }

        [TestCase(typeof(ITestDefaultConventions), typeof(TestDefaultConventions), false)]
        [TestCase(typeof(IConnectionStringConfigurationProvider), typeof(SimpleConnectionStringConfigurationProvider), true)]
        [TestCase(typeof(IDbConnectionProvider), typeof(PgSqlDbConnectionProvider), true)]
        [TestCase(typeof(ITableMetaModelQueryProvider), typeof(PgSqlDataStorageQueryProvider), true)]
        [TestCase(typeof(ITableMetaModelTypeConvertor), typeof(PgSqlDataStorageTypeConvertor), false)]
        [TestCase(typeof(IConventionResolver), typeof(ConventionResolver), true)]
        public void Constructor_ShouldMatchInterfacesToConcreteTypes_GivenInterface_And_GivenConcreteType(Type interfaceType, Type concreteType, bool shouldBeSingleton)
        {
            var container = new Container(new GeneratorRegistry());
            var instance1 = container.TryGetInstance(interfaceType);
            var instance2 = container.TryGetInstance(interfaceType);

            Assert.That(instance1, Is.Not.Null);
            Assert.That(instance1, Is.InstanceOf(concreteType));
            if (shouldBeSingleton)
            {
                Assert.That(instance1, Is.SameAs(instance2), "Type was not registered as singleton");
            }
            else
            {
                Assert.That(instance1, Is.Not.SameAs(instance2), "Type was registered as singleton, when it should return a new instance every time");
            }
        }
    }
}
