using PostgRESTSharp.Commands.GenerateRESTRoutes.Templates;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PostgRESTSharp.Commands.GenerateRESTRoutes
{
    public class GenerateRESTRoutesCommandProcessor : CommandProcessor, IGenerateRESTRoutesCommandProcessor
    {
        public void Process(IEnumerable<IViewMetaModel> views, bool splitGeneratedFiles, string fileName, string outputDirectory, string fileNamespace, string modelNamespace)
        {
            // write out the root route

            // generate the files
            if (splitGeneratedFiles)
            {
                // check that we have a valid prefix and not a sql filename
                if (fileName == GenerateRESTRoutesCommand.DEFAULT_RESTROUTES_FILENAME)
                {
                    fileName = "";
                }

                // we need to generate one file per view
                foreach (var view in views.Where(x => x.HasKey))
                {
                    var restModel = new NancyRESTRoute(view, fileNamespace, modelNamespace);
                    string contents = restModel.TransformText();
                    string viewFileName = Path.Combine(outputDirectory, string.Format("{0}.cs", view.ModelName));
                    this.WriteFileContents(viewFileName, contents);
                }
            }
            else
            {
                var viewScript = new NancyRESTRoutes(views.Where(x => x.HasKey), fileNamespace, modelNamespace);
                string contents = viewScript.TransformText();
                string viewFileName = Path.Combine(outputDirectory, fileName);
                this.WriteFileContents(viewFileName, contents);
            }
        }
    }
}