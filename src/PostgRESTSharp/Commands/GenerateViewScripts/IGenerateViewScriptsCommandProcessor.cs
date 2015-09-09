﻿using System;
using System.Collections.Generic;

namespace PostgRESTSharp.Commands.GenerateViewScripts
{
	public interface IGenerateViewScriptsCommandProcessor
	{
		void Process(IEnumerable<ITableMetaModel> tables, IEnumerable<IViewMetaModel> views, bool splitGeneratedFiles, string viewSchemaOwner, int viewSchemaVersion, string fileName, string outputDirectory);
	}
}

