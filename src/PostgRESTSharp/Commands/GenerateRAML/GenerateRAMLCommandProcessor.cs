using PostgRESTSharp.Commands.GenerateViewScripts.Templates;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using PostgRESTSharp.REST;
using Raml.Parser.Expressions;
using PostgRESTSharp.RAML;
using PostgRESTSharp.Commands.GenerateRAML.Maps;
using AutoMapper;

namespace PostgRESTSharp.Commands.GenerateRAML
{
    public class GenerateRAMLCommandProcessor : CommandProcessor, IGenerateRAMLCommandProcessor
    {
        IMapConfiguration mapper;

        public GenerateRAMLCommandProcessor(IMapConfiguration mapper)
        {
            this.mapper = mapper;
        }

        public void Process(string baseURI, string title, IEnumerable<IRESTResource> resources, int viewSchemaVersion, string fileName, string outputDirectory)
        {
            mapper.ConfigureAllMappings();
            
            var ramlDocuent = Extensions.New(baseURI, title, viewSchemaVersion.ToString());


            foreach (IRESTResource restResource in resources)
            {
                var resource = mapper.Transform<IRESTResource,Resource>(restResource);
                ramlDocuent.Resources.Add(resource);
            }

            RamlSerializer serializer = new RamlSerializer();
            string ramlSerializedDoBument = serializer.Serialize(ramlDocuent);
        }
    }
}