using System;
using System.Collections.Generic;

namespace PostgRESTSharp.Commands.GenerateRESTRoutes
{
	public interface IGenerateRESTRoutesCommandProcessor
	{
		void Process(IEnumerable<IViewMetaModel> views, bool splitGeneratedFiles, string fileName, string outputDirectory, string fileNamespace, string modelNamespace, bool isReadOnly);
	}
}

