using PostgRESTSharp.Commands.GenerateRESTRoutes.Templates;
using PostgRESTSharp.REST;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PostgRESTSharp.Commands.GenerateRESTRoutes
{
    public class GenerateRESTRoutesCommandProcessor : CommandProcessor, IGenerateRESTRoutesCommandProcessor
    {
		public void Process(IEnumerable<RESTResource> resources, bool splitGeneratedFiles, string fileName, string outputDirectory, string fileNamespace, string modelNamespace)
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
                foreach (var resource in resources)
                {
					var restRoute = new NancyRESTRoute(resource, fileNamespace, modelNamespace);
                    string contents = restRoute.TransformText();
                    string viewFileName = Path.Combine(outputDirectory, string.Format("{0}.cs", resource.ModelName));
                    this.WriteFileContents(viewFileName, contents);
                }
            }
            else
            {
                //var restRoutes = new NancyRESTRoutes(resources, fileNamespace, modelNamespace);
                //string contents = restRoutes.TransformText();
                //string viewFileName = Path.Combine(outputDirectory, fileName);
                //this.WriteFileContents(viewFileName, contents);
            }
        }
    }
}