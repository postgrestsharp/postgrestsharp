using PostgRESTSharp.Commands.GenerateViewScripts.Templates;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PostgRESTSharp.Commands.GenerateViewScripts
{
    public class GenerateViewScriptsCommandProcessor : CommandProcessor, IGenerateViewScriptsCommandProcessor
    {
        public void Process(IEnumerable<ITableMetaModel> tables, IEnumerable<IViewMetaModel> views, bool splitGeneratedFiles, string viewSchemaOwner, int viewSchemaVersion, string fileName, string outputDirectory)
        {
            // generate the files
            if (splitGeneratedFiles)
            {
                // check that we have a valid prefix and not a sql filename
                if (fileName == GenerateViewScriptsCommand.DEFAULT_VIEWS_FILENAME)
                {
                    fileName = GenerateViewScriptsCommand.DEFAULT_VIEW_PREFIX;
                }

                // we need to generate one file per view
                foreach (var view in views)
                {
                    if (!view.IsExclused)
                    {
                        var viewScript = new ViewScript(view, viewSchemaOwner, viewSchemaVersion);
                        string contents = viewScript.TransformText();
                        string viewFileName = Path.Combine(outputDirectory, string.Format("{0}_{1}.sql", fileName, view.ViewName));
                        this.WriteFileContents(viewFileName, contents);
                    }
                    else
                    {
                        string viewFileName = Path.Combine(outputDirectory, string.Format("{0}_{1}.sql", fileName, view.ViewName));
                        if (File.Exists(viewFileName))
                        {
                            File.Delete(viewFileName);
                        }
                    }
                }
            }
            else
            {
                var viewScript = new ViewsScript(views.Where(x => !x.IsExclused), viewSchemaOwner, viewSchemaVersion);
                string contents = viewScript.TransformText();
                string viewFileName = Path.Combine(outputDirectory, fileName);
                this.WriteFileContents(viewFileName, contents);
            }
        }
    }
}