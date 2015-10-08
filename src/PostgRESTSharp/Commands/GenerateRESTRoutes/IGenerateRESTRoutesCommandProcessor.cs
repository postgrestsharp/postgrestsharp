using PostgRESTSharp.REST;
using System;
using System.Collections.Generic;

namespace PostgRESTSharp.Commands.GenerateRESTRoutes
{
	public interface IGenerateRESTRoutesCommandProcessor
	{
		void Process(IEnumerable<RESTResource> resources, bool splitGeneratedFiles, string fileName, string outputDirectory, string fileNamespace, string modelNamespace, string errorHandlingMode);
	}
}

