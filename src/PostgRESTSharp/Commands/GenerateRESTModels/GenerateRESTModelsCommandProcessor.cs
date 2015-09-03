using PostgRESTSharp.Commands.GenerateRESTModels.Templates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Commands.GenerateRESTModels
{
    public class GenerateRESTModelsCommandProcessor : CommandProcessor, IGenerateRESTModelsCommandProcessor
    {
        public void Process(IEnumerable<IViewMetaModel> views, bool splitGeneratedFiles, string fileName, string outputDirectory, string fileNamespace)
        {
            // generate the files
            if (splitGeneratedFiles)
            {
                // check that we have a valid prefix and not a sql filename
                if (fileName == GenerateRESTModelsCommand.DEFAULT_RESTMODELS_FILENAME)
                {
                    fileName = GenerateRESTModelsCommand.DEFAULT_RESTMODEL_PREFIX;
                }

                // we need to generate one file per view
                foreach (var view in views)
                {
                    var restModel = new NancyRESTModel(view, fileNamespace);
                    string contents = restModel.TransformText();
                    string viewFileName = Path.Combine(outputDirectory, string.Format("{0}_{1}.cs", fileName, view.ModelName));
                    this.WriteFileContents(viewFileName, contents);
                }
            }
            else
            {
                var viewScript = new NancyRESTModels(views, fileNamespace);
                string contents = viewScript.TransformText();
                string viewFileName = Path.Combine(outputDirectory, fileName);
                this.WriteFileContents(viewFileName, contents);
            }
        }
    }
}
