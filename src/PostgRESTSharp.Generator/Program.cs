using PostgRESTSharp.Commands.GenerateViewScripts;
﻿using PostgRESTSharp.Commands.GenerateRAML;
﻿using System.Diagnostics;
using PostgRESTSharp.Commands.GenerateViewScripts;
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

            IContainer container = new Container(x =>
                {
                    x.Scan(y =>
                        {
                            y.LookForRegistries();
                            y.WithDefaultConventions();
                            y.AssembliesFromApplicationBaseDirectory();
                            y.AddAllTypesOf<IConvention>();
                            y.AddAllTypesOf<IMapping>();
                        });

                    x.For<IConnectionStringConfigurationProvider>().Singleton().Use<SimpleConnectionStringConfigurationProvider>();
                    x.For<IDbConnectionProvider>().Singleton().Use<PgSqlDbConnectionProvider>();
                    x.For<ITableMetaModelQueryProvider>().Singleton().Use<PgSqlDataStorageQueryProvider>();
                    x.For<ITableMetaModelTypeConvertor>().Use<PgSqlDataStorageTypeConvertor>();
                    x.For<IConventionResolver>().Singleton().Use<ConventionResolver>();
                });

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