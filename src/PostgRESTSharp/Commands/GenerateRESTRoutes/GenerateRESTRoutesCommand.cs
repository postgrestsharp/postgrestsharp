using System;
using System.Collections.Generic;
using PostgRESTSharp.Configuration;
using PostgRESTSharp.Conventions;
using Synoptic;
using System.Linq;

namespace PostgRESTSharp.Commands.GenerateRESTRoutes
{
	[Command(Name = "generaterestroutes", Description = "Generate REST Routes")]
	public class GenerateRESTRoutesCommand
	{
		public const string DEFAULT_RESTROUTES_FILENAME = "RestModules.cs";
		public const string DEFAULT_RESTROUTE_PREFIX = "RestModule";

		private ITableMetaModelRetriever dataStorageMetaModelRetriever;
		private IViewMetaModelProcessor viewMetaModelProcessor;
        private IRESTResourceProcessor restResourceProcessor;
        private IConnectionStringConfigurationProvider connectionStringConfigProvider;
		private IGenerateRESTRoutesCommandProcessor generateRESTRoutesCommandProcessor;

		public GenerateRESTRoutesCommand(IConnectionStringConfigurationProvider connectionStringConfigProvider,
			ITableMetaModelRetriever dataStorageMetaModelRetriever, IViewMetaModelProcessor viewMetaModelProcessor,
            IRESTResourceProcessor restResourceProcessor,
			IGenerateRESTRoutesCommandProcessor generateRESTRoutesCommandProcessor)
		{
			this.dataStorageMetaModelRetriever = dataStorageMetaModelRetriever;
			this.viewMetaModelProcessor = viewMetaModelProcessor;
			this.connectionStringConfigProvider = connectionStringConfigProvider;
			this.generateRESTRoutesCommandProcessor = generateRESTRoutesCommandProcessor;
            this.restResourceProcessor = restResourceProcessor;
		}

		[CommandAction]
		public void All([CommandParameter(Prototype = "d|database", Description = "The database name to generate routes for.", IsRequired = true)]string database,
			[CommandParameter(Prototype = "i|includedSchemas", Description = "Comma separated list of schemas to include during table discovery.", IsRequired = true)]string includedSchemas,
			[CommandParameter(Prototype = "o|out", Description = "The output directory for the generated routes.", IsRequired = true)]string outputDirectory,
			[CommandParameter(Prototype = "v|version", Description = "The view schema version.", IsRequired = true, DefaultValue = 1)]int viewSchemaVersion,
			[CommandParameter(Prototype = "s|schemaOwner", Description = "The view schema owner.", IsRequired = true, DefaultValue = "thetruetrade")]string viewSchemaOwner,
			[CommandParameter(Prototype = "p|splitFiles", Description = "Split generated REST routes into a file per model.", IsRequired = true, DefaultValue = "false")]string splitGeneratedFiles,
			[CommandParameter(Prototype = "c|connectionString", Description = "The connection string to use to connect to the database.", IsRequired = true)]string connectionString,
			[CommandParameter(Prototype = "f|fileName", Description = "The filename to use for the generated routes or the fileName prefix if splitting generated files.", IsRequired = true, DefaultValue = DEFAULT_RESTROUTES_FILENAME)]string fileName,
			[CommandParameter(Prototype = "n|namespace", Description = "The namespace used for the generated routes.", IsRequired =true)]string fileNamespace,
            [CommandParameter(Prototype = "m|modelNamespace", Description = "The model namespace used for the generated routes.", IsRequired = true)]string modelNamespace,
			[CommandParameter(Prototype = "r|readOnly", Description = "Generate read only routes.", IsRequired = true, DefaultValue = "true")]string readOnly,
            [CommandParameter(Prototype = "e|errorHandlingMode", Description = "The error handling mode to use for the generated routes [none | standard]")]string errorHandlingMode
        )
		{
			// setup the connection
			this.connectionStringConfigProvider.ConnectionString = connectionString;

			// get the models to generate
			var tables = dataStorageMetaModelRetriever.RetrieveMetaModels(database, includedSchemas.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries), new string[] { }).Where(x => x.MetaModelType == TableMetaModelTypeEnum.Table);
			var views = this.viewMetaModelProcessor.ProcessModels(tables, viewSchemaVersion).Where(x => x.HasViewKey);
            var resources = this.restResourceProcessor.Process(views.Where(x => x.HasKey), bool.Parse(readOnly));

			var splitFiles = bool.Parse(splitGeneratedFiles);

			this.generateRESTRoutesCommandProcessor.Process(resources, splitFiles, fileName, outputDirectory, fileNamespace, modelNamespace, errorHandlingMode);
		}
	}
}

