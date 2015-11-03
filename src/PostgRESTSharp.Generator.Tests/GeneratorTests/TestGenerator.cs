using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using PostgRESTSharp.Commands.GenerateViewScripts;
using PostgRESTSharp.Conventions;
using StructureMap;
using StructureMap.Configuration.DSL;
using Synoptic;

namespace PostgRESTSharp.Generator.Tests.GeneratorTests
{
    [TestFixture]
    public class TestGenerator
    {
        [Test]
        public void Process_GivenEmptyArguments_ShouldConfigureContainer()
        {
            var container = Substitute.For<IContainer>();

            var runner = Substitute.For<CommandRunner>();
            var generator = new Generator(container, runner);
            generator.Process(new string[0]);

            container.Received(1).Configure(Arg.Any<Action<ConfigurationExpression>>());
        }

        [Test]
        public void Process_GivenEmptyArguments_ShouldHaveRetrievedTheConventionResolver()
        {
            var container = Substitute.For<IContainer>();

            var runner = Substitute.For<CommandRunner>();
            var generator = new Generator(container, runner);
            generator.Process(new string[0]);

            container.Received(1).GetInstance<IConventionResolver>();
        }

        [Test]
        public void Process_GivenEmptyArguments_ShouldHaveCalledInitialiseOnTheConventionResolver()
        {
            var container = Substitute.For<IContainer>();

            var conventions = Enumerable.Empty<IConvention>();
            var conventionResolver = Substitute.For<IConventionResolver>();

            container
                .GetInstance<IConventionResolver>()
                .Returns(conventionResolver);

            container.GetAllInstances<IConvention>()
                .Returns(conventions);

            var runner = Substitute.For<CommandRunner>();
            var generator = new Generator(container, runner);
            generator.Process(new string[0]);

            conventionResolver.Received(1).Initialise(Arg.Is(conventions));
        }

        [Test]
        public void Process_GivenEmptyArguments_ShouldInitliasedTheCommandRunner()
        {
            var container = Substitute.For<IContainer>();
            var runner = Substitute.For<CommandRunner>();

            var generator = new Generator(container, runner);
            var args = new string[0];
            generator.Process(args);

            runner.Received(1).WithDependencyResolver(Arg.Any<StructureMapCommandDependencyResolver>());
            runner.Received(1).WithCommandsFromAssembly(typeof(GenerateViewScriptsCommand).Assembly);
            runner.Received(1).Run(args);
        }
    }
}
