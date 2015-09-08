﻿using PostgRESTSharp.Commands.GenerateRESTModels.Templates;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PostgRESTSharp.Commands.GenerateRESTModels
{
    public class GenerateRESTModelsCommandProcessor : CommandProcessor, IGenerateRESTModelsCommandProcessor
    {
		public void Process(IEnumerable<IViewMetaModel> views, bool splitGeneratedFiles, string fileName, string outputDirectory, string fileNamespace, bool isReadOnly)
        {
            // generate the files
            if (splitGeneratedFiles)
            {
                // check that we have a valid prefix and not a sql filename
                if (fileName == GenerateRESTModelsCommand.DEFAULT_RESTMODELS_FILENAME)
                {
                    fileName = "";
                }

                // we need to generate one file per view
                foreach (var view in views.Where(x => x.HasKey))
                {
					var restModel = new NancyRESTModel(view, fileNamespace,isReadOnly);
                    string contents = restModel.TransformText();
                    string viewFileName = Path.Combine(outputDirectory, string.Format("{0}.cs", view.ModelName));
                    this.WriteFileContents(viewFileName, contents);
                }
            }
            else
            {
                var viewScript = new NancyRESTModels(views.Where(x => x.HasKey), fileNamespace);
                string contents = viewScript.TransformText();
                string viewFileName = Path.Combine(outputDirectory, fileName);
                this.WriteFileContents(viewFileName, contents);
            }
        }
    }
}