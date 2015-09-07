using PostgRESTSharp.Configuration;
using Synoptic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Commands.GenerateRESTModels
{
    [Command(Name = "generaterestmodels", Description = "Generate REST Models")]
    public class GenerateRESTModelsCommand
    {
        public const string DEFAULT_RESTMODELS_FILENAME = "RestModels.cs";
        public const string DEFAULT_RESTMODEL_PREFIX = "RestModel";

        private IMetaModelRetriever dataStorageMetaModelRetriever;
        private IEnumerable<IViewMetaModelBuilderConvention> metaModelBuilderConventions;
        private IViewMetaModelProcessor viewMetaModelProcessor;
        private IConnectionStringConfigurationProvider connectionStringConfigProvider;
        private IGenerateRESTModelsCommandProcessor generateRESTModelsCommandProcessor;

        public GenerateRESTModelsCommand(IConnectionStringConfigurationProvider connectionStringConfigProvider,
            IMetaModelRetriever dataStorageMetaModelRetriever, IViewMetaModelProcessor viewMetaModelProcessor,
            IGenerateRESTModelsCommandProcessor generateRESTModelsCommandProcessor,
            IEnumerable<IViewMetaModelBuilderConvention> metaModelBuilderConventions)
        {
            this.dataStorageMetaModelRetriever = dataStorageMetaModelRetriever;
            this.viewMetaModelProcessor = viewMetaModelProcessor;
            this.metaModelBuilderConventions = metaModelBuilderConventions;
            this.connectionStringConfigProvider = connectionStringConfigProvider;
            this.generateRESTModelsCommandProcessor = generateRESTModelsCommandProcessor;
        }

        [CommandAction]
        public void All([CommandParameter(Prototype = "d|database", Description = "The database name to generate models for.", IsRequired = true)]string database,
            [CommandParameter(Prototype = "i|includedSchemas", Description = "Comma separated list of schemas to include during table discovery.", IsRequired = true)]string includedSchemas,
            [CommandParameter(Prototype = "o|out", Description = "The output directory for the generated scripts.", IsRequired = true)]string outputDirectory,
            [CommandParameter(Prototype = "v|version", Description = "The view schema version.", IsRequired = true, DefaultValue = 1)]int viewSchemaVersion,
            [CommandParameter(Prototype = "s|schemaOwner", Description = "The view schema owner.", IsRequired = true, DefaultValue = "thetruetrade")]string viewSchemaOwner,
            [CommandParameter(Prototype = "p|splitFiles", Description = "Split generated REST models into a file per model.", IsRequired = true, DefaultValue = "false")]string splitGeneratedFiles,
            [CommandParameter(Prototype = "c|connectionString", Description = "The connection string to use to connect to the database.", IsRequired = true)]string connectionString,
            [CommandParameter(Prototype = "f|fileName", Description = "The filename to use for the generated models or the fileName prefix if splitting generated files.", IsRequired = true, DefaultValue = DEFAULT_RESTMODELS_FILENAME)]string fileName,
            [CommandParameter(Prototype = "n|namespace", Description = "The namespace used for the generated models.", IsRequired =true)]string fileNamespace,
			[CommandParameter(Prototype = "r|readOnly", Description = "Generate read only models.", IsRequired = true, DefaultValue = "true")]string readOnly
        )
        {
            // setup the connection
            this.connectionStringConfigProvider.ConnectionString = connectionString;

            // get the models to generate
            var tables = dataStorageMetaModelRetriever.RetrieveMetaModels(database, includedSchemas.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries), new string[] { }).Where(x => x.MetaModelType == MetaModelTypeEnum.Table);
            var views = this.viewMetaModelProcessor.ProcessModels(tables, viewSchemaVersion).Where(x=>x.HasViewKey);

            var splitFiles = bool.Parse(splitGeneratedFiles);

			this.generateRESTModelsCommandProcessor.Process(views, splitFiles, fileName, outputDirectory, fileNamespace, bool.Parse(readOnly));
        }
    }
}
