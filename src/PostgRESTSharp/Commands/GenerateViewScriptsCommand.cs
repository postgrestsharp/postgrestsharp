﻿using System;
using System.IO;
using Synoptic;
using PostgRESTSharp;
using System.Collections.Generic;
using System.Linq;
using PostgRESTSharp.Templates;
using PostgRESTSharp.Configuration;

namespace PostgRESTSharp.Commands
{
	[Command(Name = "generateviewscripts", Description = "Generate View Scripts")]	
	public class GenerateViewScriptsCommand
	{
        public const string DEFAULT_VIEWS_FILENAME = "views.sql";
        public const string DEFAULT_VIEW_PREFIX = "view";


        private IMetaModelRetriever dataStorageMetaModelRetriever;
		private IEnumerable<IViewMetaModelBuilderConvention> metaModelBuilderConventions;
        private IViewMetaModelProcessor viewMetaModelProcessor;
        private IConnectionStringConfigurationProvider connectionStringConfigProvider;
		private IGenerateViewScriptsCommandProcessor generateViewScriptsCommandProcessor;

        public GenerateViewScriptsCommand(IConnectionStringConfigurationProvider connectionStringConfigProvider, 
			IMetaModelRetriever dataStorageMetaModelRetriever, IViewMetaModelProcessor viewMetaModelProcessor, 
			IGenerateViewScriptsCommandProcessor generateViewScriptsCommandProcessor,
			IEnumerable<IViewMetaModelBuilderConvention> metaModelBuilderConventions)
		{
			this.dataStorageMetaModelRetriever = dataStorageMetaModelRetriever;
            this.viewMetaModelProcessor = viewMetaModelProcessor;
			this.metaModelBuilderConventions = metaModelBuilderConventions;
            this.connectionStringConfigProvider = connectionStringConfigProvider;
			this.generateViewScriptsCommandProcessor = generateViewScriptsCommandProcessor;
		}

		[CommandAction]
		public void All ([CommandParameter(Prototype = "d|database", Description = "The database name to generate models for.", IsRequired = true)]string database,
			[CommandParameter(Prototype = "i|includedSchemas", Description = "Comma separated list of schemas to include during table discovery.", IsRequired = true)]string includedSchemas,
			[CommandParameter (Prototype = "o|out", Description = "The output directory for the generated scripts.", IsRequired = true)]string outputDirectory,
			[CommandParameter (Prototype = "v|version", Description = "The view schema version.", IsRequired = true, DefaultValue = 1)]int viewSchemaVersion,
			[CommandParameter (Prototype = "s|schemaOwner", Description = "The view schema owner.", IsRequired = true, DefaultValue = "thetruetrade")]string viewSchemaOwner,
            [CommandParameter(Prototype = "p|splitFiles", Description = "Split generated view into a file per view.", IsRequired = true, DefaultValue = "false")]string splitGeneratedFiles,
            [CommandParameter(Prototype = "c|connectionString", Description = "The connection string to use to connect to the database.", IsRequired = true)]string connectionString,
            [CommandParameter(Prototype = "f|fileName", Description = "The filename to use for the generated views sql or the fileName prefix if splitting generated files.", IsRequired = true, DefaultValue ="views.sql")]string fileName

        )
		{
            // setup the connection
            this.connectionStringConfigProvider.ConnectionString = connectionString;

			// get the models to generate
			var tables = dataStorageMetaModelRetriever.RetrieveMetaModels(database, includedSchemas.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries), new string[] { }).Where(x => x.MetaModelType == MetaModelTypeEnum.Table);
            var views = this.viewMetaModelProcessor.ProcessModels(tables, viewSchemaVersion);

			var splitFiles = bool.Parse(splitGeneratedFiles);

			this.generateViewScriptsCommandProcessor.Process (tables, views, splitFiles, viewSchemaOwner, viewSchemaVersion, fileName, outputDirectory);

		}
	}
}

