using PostgRESTSharp.Commands.GenerateRAML.Transitions;
using PostgRESTSharp.REST;
using Raml.Parser;
using Raml.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Commands.GenerateRAML
{
    public static class Extensions
    {
        public static RamlDocument New(string baseURI,string title,string version)
        {
            RamlDocument document = new RamlDocument();
            document.BaseUri = baseURI;
            document.Title = title;
            document.Version = version;
            return document;
        }

        public static RamlDocument LoadRamlDocument(string fileName)
        {

            if (File.Exists(fileName))
            {
                RamlParser ramlParser = new RamlParser();
                try
                {
                    var ramlDocument = ramlParser.LoadAsync(fileName).Result;
                    return ramlDocument;
                }
                catch (Exception exc)
                {
                    Console.WriteLine("An error has occured while loading raml document");
                    Console.WriteLine(string.Format("filename  : {0}", fileName));
                    Console.WriteLine(exc.Message);
                    if (exc.InnerException!=null)
                        Console.WriteLine(exc.InnerException.Message);
                }
                
            }
            return null;
        }

        public static IEnumerable<string> FindFiles(string includedRamlDirectory,string filter)
        {
            if (Directory.Exists(includedRamlDirectory))
            {
                IEnumerable<string> fileNames = Directory.GetFiles(includedRamlDirectory,filter);
                var childDirs = Directory.GetDirectories(includedRamlDirectory);
                foreach (var child in childDirs)
                {
                    var result = FindFiles(child, filter);
                    fileNames = fileNames.Concat(result);
                }
                return fileNames.Select(x => Path.Combine(includedRamlDirectory, x));
            }
            return Enumerable.Empty<string>();
        }

        public static IEnumerable<RAMLNestedResource> Expand(this IEnumerable<RESTMethod> methods)
        {
            IList<RAMLNestedResource> nestedResources = new List<RAMLNestedResource>();
            foreach (var method in methods)
            {
                foreach (var uriParameter in method.URIParameters)
                {
                    var resource = new RAMLNestedResource(method.Verb, method.VerbDetail, method,uriParameter);
                    nestedResources.Add(resource);
                }
            }
            return nestedResources;
        }

        public static void Merge(ICollection<Resource> baseItems, ICollection<Resource> newItems, IDictionary<string, IDictionary<string, string>> resourceType, IEnumerable<IDictionary<string, ResourceType>> resourceTypes)
        {
            foreach (var resource in newItems)
            {
                if (!baseItems.Any(x => x.RelativeUri == resource.RelativeUri))
                {
                    if (resourceType != null)
                    {
                        resource.Type = resourceType;
                    }
                    baseItems.Add(resource);
                }
                else
                {
                    var newBaseItem = baseItems.Single(x => x.RelativeUri == resource.RelativeUri);
                    Merge(newBaseItem.Resources, resource.Resources, newBaseItem.Type,resourceTypes);
                }
            }
        }
    }
}
