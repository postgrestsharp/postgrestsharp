using PostgRESTSharp.REST;
using System;
using System.Collections.Generic;

namespace PostgRESTSharp.Commands.GenerateRAML
{
	public interface IGenerateRAMLCommandProcessor
    {
        void Process(string baseURI, string title, IEnumerable<IRESTResource> resources, int viewSchemaVersion, string fileName, string outputDirectory,string baseRamlFile,string includedRamlDirectory);
	}
}

