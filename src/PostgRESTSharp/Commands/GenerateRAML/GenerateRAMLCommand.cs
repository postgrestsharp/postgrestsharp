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
        private IMetaModelRetriever dataStorageMetaModelRetriever;

        private IViewMetaModelProcessor viewMetaModelProcessor;
        private IConnectionStringConfigurationProvider connectionStringConfigProvider;
        private IGenerateRAMLCommandProcessor generateRAMLCommandProcessor;
        private IRESTResourceProcessor restResourceProcessor;

        public GenerateRAMLCommand(IConnectionStringConfigurationProvider connectionStringConfigProvider,
            IMetaModelRetriever dataStorageMetaModelRetriever, IViewMetaModelProcessor viewMetaModelProcessor,
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
        public void All([CommandParameter(Prototype = "d|database", Description = "The database name to generate models for.", IsRequired = true)]string database,
            [CommandParameter(Prototype = "i|includedSchemas", Description = "Comma separated list of schemas to include during table discovery.", IsRequired = true)]string includedSchemas,
            [CommandParameter(Prototype = "o|out", Description = "The output directory for the generated RAML.", IsRequired = true)]string outputDirectory,
            [CommandParameter(Prototype = "v|version", Description = "The view schema version.", IsRequired = true, DefaultValue = 1)]int viewSchemaVersion,
            [CommandParameter(Prototype = "c|connectionString", Description = "The connection string to use to connect to the database.", IsRequired = true)]string connectionString,
            [CommandParameter(Prototype = "f|fileName", Description = "The filename to use for the generated RAML document.", IsRequired = true)]string fileName,
            [CommandParameter(Prototype = "r|readOnly", Description = "Generate read only routes.", IsRequired = true, DefaultValue = "true")]string readOnly

        )
        {
            // setup the connection
            this.connectionStringConfigProvider.ConnectionString = connectionString;

            // get the models to generate
            var tables = dataStorageMetaModelRetriever.RetrieveMetaModels(database, includedSchemas.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries), new string[] { }).Where(x => x.MetaModelType == MetaModelTypeEnum.Table);
            var views = this.viewMetaModelProcessor.ProcessModels(tables, viewSchemaVersion);
            var resources = this.restResourceProcessor.Process(views.Where(x => x.HasKey), bool.Parse(readOnly));

            //this.generateRAMLCommandProcessor.Process(views, viewSchemaVersion, fileName, outputDirectory);
        }

    }
}
