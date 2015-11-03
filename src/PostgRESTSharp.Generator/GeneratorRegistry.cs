using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostgRESTSharp.Commands.GenerateRAML;
using PostgRESTSharp.Configuration;
using PostgRESTSharp.Conventions;
using PostgRESTSharp.Data;
using PostgRESTSharp.Pgsql;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace PostgRESTSharp.Generator
{
    public class GeneratorRegistry : Registry
    {
        public GeneratorRegistry()
        {
            Scan(a =>
            {
                a.WithDefaultConventions();
                a.AssembliesFromApplicationBaseDirectory();
                a.AddAllTypesOf<IConvention>();
                a.AddAllTypesOf<IMapping>();
            });

            For<IConnectionStringConfigurationProvider>().Singleton().Use<SimpleConnectionStringConfigurationProvider>();
            For<IDbConnectionProvider>().Singleton().Use<PgSqlDbConnectionProvider>();
            For<ITableMetaModelQueryProvider>().Singleton().Use<PgSqlDataStorageQueryProvider>();
            For<IConventionResolver>().Singleton().Use<ConventionResolver>();

            For<ITableMetaModelTypeConvertor>().Use<PgSqlDataStorageTypeConvertor>();
        }
    }
}
