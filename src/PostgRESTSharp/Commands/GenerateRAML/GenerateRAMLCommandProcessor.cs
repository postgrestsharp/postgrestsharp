using PostgRESTSharp.Commands.GenerateViewScripts.Templates;
using System.Collections.Generic;
using System.IO;
using System;

namespace PostgRESTSharp.Commands.GenerateRAML
{
    public class GenerateRAMLCommandProcessor : CommandProcessor, IGenerateRAMLCommandProcessor
    {
        public void Process(string baseURI, string title, IEnumerable<IViewMetaModel> views, int viewSchemaVersion, string fileName, string outputDirectory)
        {
            throw new NotImplementedException();
        }
    }
}