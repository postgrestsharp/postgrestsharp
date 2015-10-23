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
using AutoMapper.Internal;
using Microsoft.SqlServer.Server;
using Raml.Parser;

namespace PostgRESTSharp.Commands.GenerateRAML
{
    public class GenerateRAMLCommandProcessor : CommandProcessor, IGenerateRAMLCommandProcessor
    {
        IMapConfiguration mapper;
        IRamlSerializer serializer;

        public GenerateRAMLCommandProcessor(IMapConfiguration mapper, IRamlSerializer serializer)
        {
            this.mapper = mapper;
            this.serializer = serializer;
        }

        public void Process(string baseURI, string title, IEnumerable<IRESTResource> resources, int viewSchemaVersion, string fileName, string outputDirectory, 
            string baseRamlFile, string includedRamlDirectory, string accessRole, string baseResourceSecurity)
        {
            this.Configure();
            var ramlDocuent = this.CreateNewDocument(baseURI,title,viewSchemaVersion.ToString(),baseRamlFile);
            
            var baseResources = ramlDocuent.ResourceTypes.Count() > 0 ? ramlDocuent.ResourceTypes.FirstOrDefault().Keys : null;
            
            foreach (IRESTResource restResource in resources)
            {
                if (ShouldEntityBeIncluded(restResource, accessRole))
                {
                    var resource = mapper.Transform<IRESTResource, Resource>(restResource);
                    RebaseResources(resource, baseResources);
                    ramlDocuent.Resources.Add(resource);
                }
            }

            ImportExternalRAMLResources(ramlDocuent, includedRamlDirectory);

            string ramlSerializedDoBument = this.serializer.Serialize(ramlDocuent, baseResourceSecurity);
            
            this.WriteFileContents(Path.Combine(outputDirectory, fileName), ramlSerializedDoBument);
        }

        private bool ShouldEntityBeIncluded(IRESTResource restResource, string requiredRole)
        {
            if (requiredRole.Length == 0)
            {
                return !restResource.IsExcluded;
            }

            return !restResource.IsExcluded && restResource.AccessClaims.Contains(requiredRole);

        }


        private void Configure()
        {
            mapper.ConfigureAllMappings();
        }

        private void RebaseResources(Resource resource, IEnumerable<string> baseResources)
        {
            if (resource.Type == null)
            {
                resource.Type = new Dictionary<string, System.Collections.Generic.IDictionary<string, string>>();
            }

            if (baseResources != null)
            {
                foreach (string baseResourceKey in baseResources)
                {
                    resource.Type.Add(baseResourceKey, null);
                }
            }

            foreach (Resource nestedResource in resource.Resources)
            {
                RebaseResources(nestedResource, baseResources);
            }
        }

        private RamlDocument CreateNewDocument(string baseURI, string title, string version, string baseRamlFile)
        {
            var ramlDocuent = Extensions.New(baseURI, title, version);
            var baseRaml = Extensions.LoadRamlDocument(baseRamlFile);
            if (baseRaml != null)
            {
                ramlDocuent.ResourceTypes = baseRaml.ResourceTypes;
                ramlDocuent.SecuritySchemes = baseRaml.SecuritySchemes;
                ramlDocuent.SecuredBy = baseRaml.SecuredBy;
            }
            return ramlDocuent;
        }

        private void ImportExternalRAMLResources(RamlDocument generatedRamlDoc,string includedRamlDirectory)
        {
            var ramlFiles = Extensions.FindFiles(includedRamlDirectory, "*.raml");
            foreach (var ramlFile in ramlFiles)
            {
                var loadedRamlFile = Extensions.LoadRamlDocument(ramlFile);
                if (loadedRamlFile!=null)
                {
                    if (loadedRamlFile.SecuritySchemes.Count() > 0)
                    {
                        generatedRamlDoc.SecuritySchemes = generatedRamlDoc.SecuritySchemes.Concat(loadedRamlFile.SecuritySchemes);
                    }

                    if (loadedRamlFile.SecuredBy.Count() > 0)
                    {
                        generatedRamlDoc.SecuredBy = generatedRamlDoc.SecuredBy.Concat(loadedRamlFile.SecuredBy);
                    }

                    if (loadedRamlFile.ResourceTypes.Count() > 0)
                    {
                        foreach(var resourceType in loadedRamlFile.ResourceTypes)
                        {
                            if(!generatedRamlDoc.ResourceTypes.Any(x=>x.First().Key == resourceType.First().Key))
                            {
                                generatedRamlDoc.ResourceTypes = generatedRamlDoc.ResourceTypes.Concat(new IDictionary<string, ResourceType>[1] { resourceType });
                            }
                        }
                    }

                    Extensions.Merge(generatedRamlDoc.Resources, loadedRamlFile.Resources,null, generatedRamlDoc.ResourceTypes);
                }
            }
        }
    }
}