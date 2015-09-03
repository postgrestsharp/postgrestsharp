using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Commands.GenerateRESTModels
{
    public interface IGenerateRESTModelsCommandProcessor
    {
        void Process(IEnumerable<IViewMetaModel> views, bool splitGeneratedFiles, string fileName, string outputDirectory, string fileNamespace);
    }
}
