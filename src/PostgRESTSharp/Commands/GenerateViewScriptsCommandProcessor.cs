using System;
using System.Collections.Generic;
using PostgRESTSharp.Templates;
using System.IO;

namespace PostgRESTSharp.Commands
{
	public class GenerateViewScriptsCommandProcessor : CommandProcessor, IGenerateViewScriptsCommandProcessor
	{
		public GenerateViewScriptsCommandProcessor ()
		{
		}

		public void Process(IEnumerable<IMetaModel> tables, IEnumerable<IViewMetaModel> views, bool splitGeneratedFiles, string viewSchemaOwner, int viewSchemaVersion, string fileName, string outputDirectory)
		{
			// generate the files
			if(splitGeneratedFiles)
			{
				// check that we have a valid prefix and not a sql filename
				if(fileName == GenerateViewScriptsCommand.DEFAULT_VIEWS_FILENAME)
				{
					fileName = GenerateViewScriptsCommand.DEFAULT_VIEW_PREFIX;
				}


				// we need to generate one file per view
				foreach(var view in views)
				{
					var viewScript = new ViewScript(view, viewSchemaOwner, viewSchemaVersion);
					string contents = viewScript.TransformText();
					string viewFileName = Path.Combine(outputDirectory, string.Format("{0}_{1}.sql", fileName,view.ViewName));
					this.WriteFileContents (viewFileName, contents);
				}
			}
			else
			{
				var viewScript = new ViewsScript(views, viewSchemaOwner, viewSchemaVersion);
				string contents = viewScript.TransformText();
				string viewFileName = Path.Combine(outputDirectory, fileName);
				this.WriteFileContents (viewFileName, contents);
			}
		}
	}
}

