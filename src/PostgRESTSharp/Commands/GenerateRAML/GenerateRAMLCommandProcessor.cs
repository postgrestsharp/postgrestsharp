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
using Raml.Parser;

namespace PostgRESTSharp.Commands.GenerateRAML
{
    public class GenerateRAMLCommandProcessor : CommandProcessor, IGenerateRAMLCommandProcessor
    {
        IMapConfiguration mapper;
        IRamlSerializer serializer;
        RamlParser ramlParser;

        public GenerateRAMLCommandProcessor(IMapConfiguration mapper, IRamlSerializer serializer)
        {
            this.mapper = mapper;
            this.serializer = serializer;
            this.ramlParser = new RamlParser();
        }

        public void Process(string baseURI, string title, IEnumerable<IRESTResource> resources, int viewSchemaVersion, string fileName, string outputDirectory, string baseRamlFile, string includedRamlDirectory)
        {
            this.Configure();
            var ramlDocuent = this.CreateNewDocument(baseURI,title,viewSchemaVersion.ToString(),baseRamlFile);
            //why they did it with a array of dictionaries :/
            var baseResources = ramlDocuent.ResourceTypes.FirstOrDefault().Keys;
            
            foreach (IRESTResource restResource in resources)
            {
                var resource = mapper.Transform<IRESTResource,Resource>(restResource);
                RebaseResources(resource, baseResources);
                ramlDocuent.Resources.Add(resource);
            }

            string ramlSerializedDoBument = this.serializer.Serialize(ramlDocuent);
            
            //to do : perform validation on serialized document

            this.WriteFileContents(Path.Combine(outputDirectory, fileName), ramlSerializedDoBument);
        }

        
        private void Configure()
        {
            mapper.ConfigureAllMappings();
        }

        private void RebaseResources(Resource resource, IEnumerable<string> baseResources)
        {
            resource.Type = new Dictionary<string, IDictionary<string, string>>();
            foreach (string baseResourceKey in baseResources)
            {
                resource.Type.Add(baseResourceKey, null);
            }
            foreach (Resource nestedResource in resource.Resources)
            {
                RebaseResources(nestedResource, baseResources);
            }
        }

        private RamlDocument CreateNewDocument(string baseURI, string title, string version, string baseRamlFile)
        {
            var ramlDocuent = Extensions.New(baseURI, title, version);
            if (File.Exists(baseRamlFile)) {
                var baseDocument = ramlParser.LoadAsync(baseRamlFile).Result;
                ramlDocuent.ResourceTypes = baseDocument.ResourceTypes;
            }
            return ramlDocuent;
        }
    }
}