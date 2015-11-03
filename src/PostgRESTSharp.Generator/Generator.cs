using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostgRESTSharp.Commands.GenerateViewScripts;
using PostgRESTSharp.Conventions;
using StructureMap;
using Synoptic;

namespace PostgRESTSharp.Generator
{
    public class Generator
    {
        private readonly IContainer container;
        private readonly CommandRunner commandRunner;

        public Generator(IContainer container, CommandRunner commandRunner)
        {
            this.container = container;
            this.commandRunner = commandRunner;
        }

        public void Process(string[] args)
        {
            container.Configure(a => a.AddRegistry(new GeneratorRegistry()));

            // seed the convention resolver
            var conventionResolver = container.GetInstance<IConventionResolver>();
            conventionResolver.Initialise(container.GetAllInstances<IConvention>());

            var resolver = new StructureMapCommandDependencyResolver(container);

            // register the available commands
            commandRunner
                .WithDependencyResolver(resolver)
                .WithCommandsFromAssembly(typeof(GenerateViewScriptsCommand).Assembly)
                .Run(args);
        }
    }
}
