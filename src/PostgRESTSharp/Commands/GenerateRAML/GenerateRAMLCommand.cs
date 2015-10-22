using PostgRESTSharp.Configuration;
using PostgRESTSharp.Conventions;
using Synoptic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Commands.GenerateRAML
{
    [Command(Name = "generateraml", Description = "Generate RAML Docs")]
    public class GenerateRAMLCommand
    {
        private ITableMetaModelRetriever dataStorageMetaModelRetriever;

        private IViewMetaModelProcessor viewMetaModelProcessor;
        private IConnectionStringConfigurationProvider connectionStringConfigProvider;
        private IGenerateRAMLCommandProcessor generateRAMLCommandProcessor;
        private IRESTResourceProcessor restResourceProcessor;

        public GenerateRAMLCommand(IConnectionStringConfigurationProvider connectionStringConfigProvider,
            ITableMetaModelRetriever dataStorageMetaModelRetriever, IViewMetaModelProcessor viewMetaModelProcessor,
            IRESTResourceProcessor restResourceProcessor,
            IGenerateRAMLCommandProcessor generateRAMLCommandProcessor)
        {
            this.dataStorageMetaModelRetriever = dataStorageMetaModelRetriever;
            this.viewMetaModelProcessor = viewMetaModelProcessor;
            this.connectionStringConfigProvider = connectionStringConfigProvider;
            this.generateRAMLCommandProcessor = generateRAMLCommandProcessor;
            this.restResourceProcessor = restResourceProcessor;
        }

        [CommandAction]
        public void All(
            [CommandParameter(Prototype = "u|uri", Description = "The default uri for the RAML service.", IsRequired = true)]string uri,
            [CommandParameter(Prototype = "t|title", Description = "The default service title.", IsRequired = true)]string title,
            [CommandParameter(Prototype = "v|version", Description = "The view schema version.", IsRequired = true, DefaultValue = 1)]int viewSchemaVersion,
            [CommandParameter(Prototype = "f|fileName", Description = "The filename to use for the generated RAML document.", IsRequired = true)]string fileName,

            [CommandParameter(Prototype = "c|connectionString", Description = "The connection string to use to connect to the database.", IsRequired = true)]string connectionString,
            [CommandParameter(Prototype = "d|database", Description = "The database name to generate models for.", IsRequired = true)]string database,
            [CommandParameter(Prototype = "i|includedSchemas", Description = "Comma separated list of schemas to include during table discovery.", IsRequired = true)]string includedSchemas,
            
            [CommandParameter(Prototype = "o|out", Description = "The output directory for the generated RAML.", IsRequired = true)]string outputDirectory,
            
            [CommandParameter(Prototype = "r|readOnly", Description = "Generate read only routes.", IsRequired = true, DefaultValue = "true")]string readOnly,

            [CommandParameter(Prototype = "b|baseRaml", Description = "Base raml file to include.", IsRequired = true, DefaultValue = "true")]string baseRamlFile,
            [CommandParameter(Prototype = "e|externalRamlFolder", Description = "Folder containing raml files to import resources.", IsRequired = true, DefaultValue = "true")]string externalRamls,
            [CommandParameter(Prototype = "a|accessRole", Description = "The role the documenation is generated for", IsRequired = true, DefaultValue = "")]string accessRole
        )
        {
            // setup the connection
            this.connectionStringConfigProvider.ConnectionString = connectionString;

            // get the models to generate
            var tables = dataStorageMetaModelRetriever.RetrieveMetaModels(database, includedSchemas.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries), new string[] { }).Where(x => x.MetaModelType == TableMetaModelTypeEnum.Table);
            var views = this.viewMetaModelProcessor.ProcessModels(tables, viewSchemaVersion);
            var resources = this.restResourceProcessor.Process(views.Where(x => x.HasKey), bool.Parse(readOnly));

            this.generateRAMLCommandProcessor.Process(uri, title, resources, viewSchemaVersion, fileName, outputDirectory, baseRamlFile, externalRamls, accessRole);
        }

    }
}
