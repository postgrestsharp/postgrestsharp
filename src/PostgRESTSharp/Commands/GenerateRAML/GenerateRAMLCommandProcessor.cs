using PostgRESTSharp.Commands.GenerateViewScripts.Templates;
using System.Collections.Generic;
using System.IO;

namespace PostgRESTSharp.Commands.GenerateRAML
{
    public class GenerateRAMLCommandProcessor : CommandProcessor, IGenerateRAMLCommandProcessor
    {
        public void Process(IEnumerable<IViewMetaModel> views, int viewSchemaVersion, string fileName, string outputDirectory)
        {
            // generate the RAML

        }
    }
}