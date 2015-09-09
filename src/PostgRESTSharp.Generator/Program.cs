using PostgRESTSharp.Commands.GenerateViewScripts;
using PostgRESTSharp.Configuration;
using PostgRESTSharp.Data;
using PostgRESTSharp.Pgsql;
using StructureMap;
using StructureMap.Graph;
using Synoptic;
using PostgRESTSharp.Conventions;

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
                        });

                    x.For<IConnectionStringConfigurationProvider>().Singleton().Use<SimpleConnectionStringConfigurationProvider>();
                    x.For<IDbConnectionProvider>().Singleton().Use<PgSqlDbConnectionProvider>();
                    x.For<IMetaModelQueryProvider>().Singleton().Use<PgSqlDataStorageQueryProvider>();
                    x.For<IMetaModelTypeConvertor>().Use<PgSqlDataStorageTypeConvertor>();
                    x.For<IConventionResolver>().Singleton().Use<ConventionResolver>();
                });

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