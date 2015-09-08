using System;
using System.Collections.Generic;

namespace PostgRESTSharp.Commands.GenerateRAML
{
	public interface IGenerateRAMLCommandProcessor
    {
		void Process(string baseURI, string title, IEnumerable<IViewMetaModel> views, int viewSchemaVersion, string fileName, string outputDirectory);
	}
}

