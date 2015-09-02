using System;
using Synoptic;
using PostgRESTSharp;
using System.Collections.Generic;
using System.Linq;

namespace PostgRESTSharp.Commands
{
	[Command(Name = "generateviewscripts", Description = "Generate View Scripts")]	
	public class GenerateViewScriptsCommand
	{
		private IMetaModelRetriever dataStorageMetaModelRetriever;
		private IEnumerable<IViewMetaModelBuilderConvention> metaModelBuilderConventions;

		public GenerateViewScriptsCommand(IMetaModelRetriever dataStorageMetaModelRetriever, IEnumerable<IViewMetaModelBuilderConvention> metaModelBuilderConventions)
		{
			this.dataStorageMetaModelRetriever = dataStorageMetaModelRetriever;
			this.metaModelBuilderConventions = metaModelBuilderConventions;
		}

		[CommandAction]
		public void All ([CommandParameter(Prototype = "d|database", Description = "The database name to generate models for.", IsRequired = true)]string database,
			[CommandParameter(Prototype = "i|includedSchemas", Description = "Comma separated list of schemas to include during datamodel discovery.", IsRequired = true)]string includedSchemas,
			[CommandParameter (Prototype = "o|out", Description = "The output directory for the generated scripts.", IsRequired = true)]string outputDirectory,
			[CommandParameter (Prototype = "v|version", Description = "The view schema version.", IsRequired = true, DefaultValue = 1)]int viewSchemaVersion,
			[CommandParameter (Prototype = "s|schemaOwner", Description = "The view schema owner.", IsRequired = true, DefaultValue = "thetruetrade")]string viewSchemaOwner
		)
		{
			// get the models to generate
			var results = dataStorageMetaModelRetriever.RetrieveMetaModels(database, includedSchemas.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries), new string[] { });
			var tables = results.Where (x => x.MetaModelType == MetaModelTypeEnum.Table);

			ViewMetaModelBuilder viewBuilder = new ViewMetaModelBuilder (this.metaModelBuilderConventions);
			List<IViewMetaModel> views = new List<IViewMetaModel> ();
			foreach (var table in tables) 
			{
				var result = viewBuilder.BuildModel (table, tables.Where (x => x.DatabaseName != table.DatabaseName && x.SchemaName != table.SchemaName && x.TableName != table.TableName), viewSchemaVersion.ToString());
				if (result != null) 
				{
					views.Add (result);
				}

			}			
		}
	}
}

