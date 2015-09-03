using System;
using System.Collections.Generic;
using PostgRESTSharp.Templates;

namespace PostgRESTSharp.Commands
{
	public interface IGenerateViewScriptsCommandProcessor
	{
		void Process(IEnumerable<IMetaModel> tables, IEnumerable<IViewMetaModel> views, bool splitGeneratedFiles, string viewSchemaOwner, int viewSchemaVersion, string fileName, string outputDirectory);
	}
}

