using PostgRESTSharp.Commands.GenerateViewScripts;
﻿using PostgRESTSharp.Commands.GenerateRAML;
﻿using System.Diagnostics;
using PostgRESTSharp.Configuration;
using PostgRESTSharp.Conventions;
using PostgRESTSharp.Data;
using PostgRESTSharp.Pgsql;
using StructureMap;
using StructureMap.Graph;
using Synoptic;

namespace PostgRESTSharp.Generator
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            var container = new Container(new GeneratorRegistry());

            // seed the convention resolver
            var conventionResolver = container.GetInstance<IConventionResolver>();
            conventionResolver.Initialise(container.GetAllInstances<IConvention>());

            var resolver = new StructureMapCommandDependencyResolver(container);

            // register the available commands
            new CommandRunner()
                .WithDependencyResolver(resolver)
                .WithCommandsFromAssembly(typeof(GenerateViewScriptsCommand).Assembly)
                .Run(args);
            
            #if DEBUG
            System.Console.WriteLine("Done");
            #endif
        }
    }
}