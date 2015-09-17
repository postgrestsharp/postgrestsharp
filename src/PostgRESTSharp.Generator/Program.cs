using System.Diagnostics;
using PostgRESTSharp.Commands.GenerateViewScripts;
using PostgRESTSharp.Configuration;
using PostgRESTSharp.Data;
using PostgRESTSharp.Pgsql;
using StructureMap;
using StructureMap.Graph;
using Synoptic;
using PostgRESTSharp.Conventions;
using PostgRESTSharp.Commands.GenerateRAML.Maps;

namespace PostgRESTSharp.Generator
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {

            Debugger.Launch();
            Debugger.Break();

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

            System.Console.WriteLine("Press any key to continue...");

            System.Console.ReadKey();
        }
    }
}